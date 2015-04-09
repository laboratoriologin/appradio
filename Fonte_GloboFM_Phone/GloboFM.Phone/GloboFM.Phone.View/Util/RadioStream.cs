using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GloboFM.Phone.View.Util
{
    public class RadioStream : MediaStreamSource
    {
        private const int MP3_HEADER_SIZE = 4; //bytes

        public MemoryStream _radioStream; //change to private, only public for debugging
        private HttpWebRequest _downloader;
        private MP3Frame _mp3Frame; //mp3 frame metadata
        private WaveFormatEx _wfx;
        private int _mp3FrameHeaderLength = 4;
        private MediaStreamDescription _audioDescription;
        private long _currentFrameStartPosition;
        private int _currentFrameSize;
        private Int64 _currentTimeStamp = 0;
        private Stream r = null;

        public bool ContinueStreaming { get; set; }
        public short BitRate { get; set; }
        public string Genre { get; private set; }
        public string StationName { get; private set; }

        public delegate void StreamSetupCompleteHandler(object sender, System.EventArgs args);
        public event StreamSetupCompleteHandler StreamSetupComplete;



        public RadioStream(string uri)
        {
            _radioStream = new MemoryStream();
            _downloader = WebRequest.CreateHttp(uri);
            _downloader.AllowReadStreamBuffering = false;
            _downloader.BeginGetResponse(new AsyncCallback(GetAsyncData), _downloader); //start filling the stream
            ContinueStreaming = true;

        }

        /// <summary>
        /// Handles the callback from the thread fetching the data from stream.
        /// </summary>
        /// <param name="result">The response from the shoutcast server</param>
        private void GetAsyncData(IAsyncResult result)
        {
            try
            {
                HttpWebRequest request = result.AsyncState as HttpWebRequest;
                HttpWebResponse response = request.EndGetResponse(result) as HttpWebResponse;
                r = response.GetResponseStream();

                StreamReader headerReader = new StreamReader(r);
                bool headerIsDone = false;
                while (!headerIsDone)
                {
                    string headerLine = headerReader.ReadLine();

                    if (headerLine.StartsWith("icy-name:"))
                    {
                        StationName = headerLine.Substring(9);
                    }
                    else if (headerLine.StartsWith("icy-genre:"))
                    {
                        Genre = headerLine.Substring(10);
                    }
                    else if (headerLine.StartsWith("icy-br:"))
                    {
                        BitRate = short.Parse(headerLine.Substring(7));
                    }
                    else if (headerLine.Equals(""))
                        headerIsDone = true;
                }

                r = headerReader.BaseStream;

                //find the first SYNC bits
                //SYNC bits has the first 11 bits set to 1
                //Todo: update code to check for 11 bits instead of 8
                byte syncBit = 0xFF;
                byte readByte;
                while (!(readByte = (byte)r.ReadByte()).Equals(syncBit))
                    continue;

                //SYNC byte found, build the header
                byte[] mp3HeaderData = new byte[_mp3FrameHeaderLength];


                mp3HeaderData[0] = readByte;
                for (int i = 1; i < 4; i++) //first byte already read, read the remaining 3
                    mp3HeaderData[i] = (byte)r.ReadByte();

                _currentFrameStartPosition = r.Position;

                _mp3Frame = new MP3Frame(mp3HeaderData);

                _wfx = new WaveFormatEx();
                _wfx.FormatTag = 85;
                _wfx.Channels = (short)((_mp3Frame.Channels == MP3Frame.Channel.SingleChannel) ? 1 : 2);
                _wfx.SamplesPerSec = _mp3Frame.SamplingRate;
                _wfx.AvgBytesPerSec = _mp3Frame.Bitrate / 8;
                _wfx.BlockAlign = 1;
                _wfx.BitsPerSample = 0;
                _wfx.Size = 12;

                StreamSetupComplete(this, new System.EventArgs()); //stream setup complete

                byte read;
                while (ContinueStreaming)
                {
                    read = (byte)r.ReadByte();
                    _radioStream.WriteByte(read);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// Not fully implemented yet
        /// </summary>
        protected override void CloseMedia()
        {
            _downloader.Abort();
        }

        /// <summary>
        /// Not implemented yet
        /// </summary>
        /// <param name="diagnosticKind"></param>
        protected override void GetDiagnosticAsync(MediaStreamSourceDiagnosticKind diagnosticKind)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Called by the mediaelement to fetch a sample from the stream.
        /// </summary>
        /// <param name="mediaStreamType">The type of data expected by the mediaelement. Will be Audio only for shoutcast.</param>
        protected override void GetSampleAsync(MediaStreamType mediaStreamType)
        {
            Dictionary<MediaSampleAttributeKeys, string> emptyDict = new Dictionary<MediaSampleAttributeKeys, string>();
            MediaStreamSample audioSample = null;
            long progress = _currentFrameStartPosition + _currentFrameSize;

            while ((progress) >= _radioStream.Length) //if the 
            {
                this.ReportGetSampleProgress(1 - (progress - _radioStream.Position));
                System.Threading.Thread.Sleep(500);
            }
            audioSample = new MediaStreamSample(
                    _audioDescription,
                    _radioStream,
                    _currentFrameStartPosition,
                    _currentFrameSize,
                    _currentTimeStamp,
                    emptyDict);

            //if (audioSample.Count > 0)
            //{
            this.ReportGetSampleCompleted(audioSample);
            //}
            _currentFrameStartPosition += _currentFrameSize;
            _currentTimeStamp += _wfx.AudioDurationFromBufferSize((uint)_currentFrameSize);

        }

        /// <summary>
        /// Sets up the mediastreamsource for the mediaelement, providing it with the information about the audio it needs.
        /// </summary>
        protected override void OpenMediaAsync()
        {
            Dictionary<MediaSourceAttributesKeys, string> availableMediaStreams = new Dictionary<MediaSourceAttributesKeys, string>();
            Dictionary<MediaStreamAttributeKeys, string> mediaStreamAttributes = new Dictionary<MediaStreamAttributeKeys, string>();
            List<MediaStreamDescription> mediaStreamDescriptions = new List<MediaStreamDescription>();

            mediaStreamAttributes[MediaStreamAttributeKeys.CodecPrivateData] = "" + _wfx.ToHexString() + _mp3Frame.ToHexString();


            _audioDescription = new MediaStreamDescription(MediaStreamType.Audio, mediaStreamAttributes);
            mediaStreamDescriptions.Add(_audioDescription);


            availableMediaStreams[MediaSourceAttributesKeys.CanSeek] = false.ToString(); //forward stream
            availableMediaStreams[MediaSourceAttributesKeys.Duration] = TimeSpan.FromMinutes(0).Ticks.ToString(CultureInfo  .InvariantCulture); //0 = continous stream
            _currentFrameSize = _mp3Frame.FrameSize;
            this.ReportOpenMediaCompleted(availableMediaStreams, mediaStreamDescriptions);
        }

        /// <summary>
        /// Not fully implemented.
        /// </summary>
        /// <param name="seekToTime"></param>
        protected override void SeekAsync(long seekToTime)
        {
            this.ReportSeekCompleted(seekToTime);
        }

        protected override void SwitchMediaStreamAsync(MediaStreamDescription mediaStreamDescription)
        {
            throw new NotImplementedException();
        }
    }
}