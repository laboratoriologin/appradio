using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using GloboFM.Phone.View.Resources;
using Microsoft.Phone.BackgroundAudio;
using Silverlight.Media;
using System.Windows.Media;
using System.Globalization;
using GloboFM.Phone.View.Util;

namespace GloboFM.Phone.View
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "When the page is unloaded, the ShoutcastMediaStreamSource is disposed.")]
    public partial class MainPage : PhoneApplicationPage
    {
        private RadioStream shoutcastStream;

        /// <summary>
        /// Representa um sream do Shoutcast
        /// </summary>
        private ShoutcastMediaStreamSource source = new ShoutcastMediaStreamSource(new Uri("http://sh1.upx.com.br:8098/", UriKind.RelativeOrAbsolute));


        /// <summary>
        /// Interrompe a atualização do status caso algum erro ocorra
        /// </summary>
        private bool errorOccured;

        /// <summary>
        /// Obtem o elemento Media da página
        /// </summary>
        private MediaElement MediaPlayer
        {
            get { return this.Resources["mediaPlayer"] as MediaElement; }
        }

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void MediaElement_BufferingProgressChanged(object sender, RoutedEventArgs e)
        {
            UpdateStatus();
        }

        private void MediaElement_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            errorOccured = true;
            txtStatus.Text = string.Format(CultureInfo.InvariantCulture, "Error:  {0}", e.ErrorException.Message);
        }

        private void MediaElement_CurrentStateChanged(object sender, RoutedEventArgs e)
        {
            UpdateStatus();
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            if (MediaPlayer.CurrentState != MediaElementState.Paused)
            {
                ResetMediaPlayer();

                //Uri uri = new Uri(@"http://sh1.upx.com.br:8096/");
                //source = new ShoutcastMediaStreamSource(uri, true);
                //source.MetadataChanged += source_MetadataChanged;
                shoutcastStream = new RadioStream("http://sh1.upx.com.br:8098");
                shoutcastStream.StreamSetupComplete += new RadioStream.StreamSetupCompleteHandler(shoutcastStream_StreamSetupComplete);

                MediaPlayer.SetSource(source);
            }
            MediaPlayer.Play();
            pbCarregando.IsIndeterminate = true;
        }

        private void shoutcastStream_StreamSetupComplete(object sender, EventArgs args)
        {
            //Prevent cross-thread issues
            System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                MediaPlayer.SetSource(shoutcastStream);
                //this.titleTxt.Text += shoutcastStream.StationName;
                //this.descriptionTxt.Text += shoutcastStream.Genre;

                //this.titleTxt.Visibility = System.Windows.Visibility.Visible;
                //this.descriptionTxt.Visibility = System.Windows.Visibility.Visible;

            });

        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            if (MediaPlayer.CurrentState == MediaElementState.Playing || MediaPlayer.CurrentState == MediaElementState.Opening || MediaPlayer.CurrentState == MediaElementState.Buffering)
                MediaPlayer.Pause();
        }

        void source_MetadataChanged(object sender, RoutedEventArgs e)
        {
            UpdateStatus();
        }

        private void UpdateStatus()
        {
            if (errorOccured)
                return;
            else
            {
                MediaElementState state = MediaPlayer.CurrentState;
                string status = string.Empty;

                switch (state)
                {
                    case MediaElementState.Buffering:
                        status = string.Format(CultureInfo.InvariantCulture, "Buffering...{0:0%}", this.MediaPlayer.BufferingProgress);
                        break;
                    case MediaElementState.Playing:
                        status = string.Format(CultureInfo.InvariantCulture, "Title: {0}", this.source.CurrentMetadata.Title);
                        if (status.Trim() != "Title:")
                            pbCarregando.IsIndeterminate = false;
                        break;
                    default:
                        status = state.ToString();
                        break;
                }

                txtStatus.Text = status;
            }
        }

        private void ResetMediaPlayer()
        {
            if ((this.MediaPlayer.CurrentState != MediaElementState.Stopped) && (this.MediaPlayer.CurrentState != MediaElementState.Closed))
            {
                this.MediaPlayer.Stop();
                this.MediaPlayer.Source = null;
                this.source.Dispose();
                this.source = null;
            }

            this.errorOccured = false;
        }

        private void PageUnloaded(object sender, EventArgs e)
        {
            ResetMediaPlayer();
        }

    }
}