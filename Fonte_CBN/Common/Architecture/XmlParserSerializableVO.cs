
using RadioPlayer.Beans.ValueObject.ValueObject.Xml;
using RadioPlayer.Beans.ValueObject.ValueObject.Xml.AgendaCulturalXML;
using RadioPlayer.Beans.ValueObject.ValueObject.Xml.AgendaCulturalXML.Cinema.Cartaz;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.System.Threading;

namespace RadioPlayer.Common.Architecture
{
    /// <summary>
    /// Classe serializa o xml em SerializableVO
    /// </summary>
    public class XmlParserSerializableVO
    {
        #region Atríbutos e propriedades

        /// <summary>
        /// Gets or sets - propriedade do tipo StorageFile
        /// </summary>
        private static StorageFile Arquivo { get; set; }
        #endregion

        #region Métodos

        /// <summary>
        /// Método que salva o arquivo serializado
        /// </summary>
        /// <typeparam name="T">Passagem por pilha</typeparam>
        /// <param name="file">Nome do arquivo para salvar os dados em xml</param>
        /// <param name="objChannel">object objChannel</param>
        /// <returns>Tarefa ThreadPool.RunAsync</returns>
        public static async Task SaveAsync<T>(string file, object objChannel)
        {
            Arquivo = await ApplicationData.Current.LocalFolder.CreateFileAsync(Util.NameFile(file, true), CreationCollisionOption.ReplaceExisting);
            try
            {
                await XmlParserSerializableVO.SaveAsyncPrv<T>(Util.NameFile(file, true), objChannel);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        /// <summary>
        /// Método que atualiza o arquivo serializado
        /// </summary>
        /// <typeparam name="T">Passagem por pilha</typeparam>
        /// <param name="file">Nome do arquivo para salvar os dados em xml</param>
        /// <param name="objChannel">object objChannel</param>
        /// <returns>Tarefa ThreadPool.RunAsync</returns>
        public static async Task RestoreAsync<T>(string file, object objChannel)
        {
            if (await Util.ExistsFileAsync(file))
            {
                try
                {
                    await XmlParserSerializableVO.RestoreAsyncPrv<T>(file, objChannel);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message, e);
                }
            }
            else
            {
                throw new Exception("Arquivo não existe");
            }
        }

        /// <summary>
        /// Método assíncrono salvar arquivo
        /// </summary>
        /// <typeparam name="T">Passagem por pilha</typeparam>
        /// <param name="file">Nome do arquivo para salvar os dados em xml</param>
        /// <param name="objChannel">object objChannel</param>
        /// <returns>Tarefa ThreadPool.RunAsync</returns>
        private static async Task SaveAsyncPrv<T>(string file, object objChannel)
        {
            try
            {
                StorageFile sessionFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(file, CreationCollisionOption.ReplaceExisting);
                IRandomAccessStream sessionRandomAccess = await sessionFile.OpenAsync(FileAccessMode.ReadWrite);
                IOutputStream sessionOutputStream = sessionRandomAccess.GetOutputStreamAt(0);

                if (typeof(T).Equals(typeof(ChannelVO)))
                {
                    ChannelVO channelVO = (ChannelVO)objChannel;
                    var serializer = new XmlSerializer(typeof(ChannelVO), new Type[] { typeof(T) });
                    serializer.Serialize(sessionOutputStream.AsStreamForWrite(), channelVO);
                }
                else if (typeof(T).Equals(typeof(ChannelAgendaCulturalVO)))
                {
                    ChannelAgendaCulturalVO channelAgendaCulturalVO = (ChannelAgendaCulturalVO)objChannel;
                    var serializer = new XmlSerializer(typeof(ChannelAgendaCulturalVO), new Type[] { typeof(T) });
                    serializer.Serialize(sessionOutputStream.AsStreamForWrite(), channelAgendaCulturalVO);
                }
                else if (typeof(T).Equals(typeof(ChannelCinemaVO)))
                {
                    ChannelCinemaVO channelCinemaVO = (ChannelCinemaVO)objChannel;
                    var serializer = new XmlSerializer(typeof(ChannelCinemaVO), new Type[] { typeof(T) });
                    serializer.Serialize(sessionOutputStream.AsStreamForWrite(), channelCinemaVO);
                }

                sessionRandomAccess.Dispose();
                await sessionOutputStream.FlushAsync();
                sessionOutputStream.Dispose();

                StorageFile fileTemp = await ApplicationData.Current.LocalFolder.GetFileAsync(file);
                await fileTemp.RenameAsync(Util.NameFile(file, false), NameCollisionOption.ReplaceExisting);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao gravar o aquivo na máquina.", e);
            }
        }

        /// <summary>
        /// Método assíncrono restaura arquivo
        /// </summary>
        /// <typeparam name="T">Passagem por pilha</typeparam>
        /// <param name="file">Nome do arquivo para salvar os dados em xml</param>
        /// <param name="objChannel">object objChannel</param>
        /// <returns>Tarefa ThreadPool.RunAsync</returns>
        private static async Task RestoreAsyncPrv<T>(string file, object objChannel)
        {
            StorageFile sessionFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(file, CreationCollisionOption.OpenIfExists);

            if (sessionFile == null)
            {
                throw new Exception("Erro ao tentar abrir o arquivo.");
            }

            try
            {
                IInputStream sessionInputStream = await sessionFile.OpenReadAsync();

                if (typeof(T).Equals(typeof(ChannelVO)))
                {
                    var serializer = new XmlSerializer(typeof(ChannelVO), new Type[] { typeof(T) });
                    ChannelVO serChannelVO = (ChannelVO)serializer.Deserialize(sessionInputStream.AsStreamForRead());
                    ChannelVO channelVO = (ChannelVO)objChannel;

                    //// TODO: achar uma forma de clonar o objeto sem perder a referencia do channel.
                    channelVO.Title = serChannelVO.Title;
                    channelVO.Description = serChannelVO.Description;
                    channelVO.Id = serChannelVO.Id;
                    channelVO.Image = serChannelVO.Image;
                    channelVO.LastBuildDate = serChannelVO.LastBuildDate;
                    channelVO.Link = serChannelVO.Link;
                    channelVO.ListaItem = serChannelVO.ListaItem;
                    channelVO.NomeChannel = serChannelVO.NomeChannel;
                    channelVO.PubDate = serChannelVO.PubDate;
                    channelVO.Url = serChannelVO.Url;
                }
                else if (typeof(T).Equals(typeof(ChannelAgendaCulturalVO)))
                {
                    var serializer = new XmlSerializer(typeof(ChannelAgendaCulturalVO), new Type[] { typeof(T) });
                    ChannelAgendaCulturalVO serChannelVO = (ChannelAgendaCulturalVO)serializer.Deserialize(sessionInputStream.AsStreamForRead());
                    ChannelAgendaCulturalVO channeAgendaCulturallVO = (ChannelAgendaCulturalVO)objChannel;

                    //// TODO: achar uma forma de clonar o objeto sem perder a referencia do channel.
                    channeAgendaCulturallVO.Title = serChannelVO.Title;
                    channeAgendaCulturallVO.Description = serChannelVO.Description;
                    channeAgendaCulturallVO.Id = serChannelVO.Id;
                    channeAgendaCulturallVO.LastBuildDate = serChannelVO.LastBuildDate;
                    channeAgendaCulturallVO.Link = serChannelVO.Link;
                    channeAgendaCulturallVO.ListaItem = serChannelVO.ListaItem;
                    channeAgendaCulturallVO.NomeChannel = serChannelVO.NomeChannel;
                    channeAgendaCulturallVO.PubDate = serChannelVO.PubDate;
                    channeAgendaCulturallVO.Url = serChannelVO.Url;
                }
                else if (typeof(T).Equals(typeof(ChannelCinemaVO)))
                {
                    var serializer = new XmlSerializer(typeof(ChannelCinemaVO), new Type[] { typeof(T) });
                    ChannelCinemaVO serChannelVO = (ChannelCinemaVO)serializer.Deserialize(sessionInputStream.AsStreamForRead());
                    ChannelCinemaVO channeCinemaVO = (ChannelCinemaVO)objChannel;

                    //// TODO: achar uma forma de clonar o objeto sem perder a referencia do channel.
                    channeCinemaVO.Title = serChannelVO.Title;
                    channeCinemaVO.Description = serChannelVO.Description;
                    channeCinemaVO.Id = serChannelVO.Id;
                    channeCinemaVO.LastBuildDate = serChannelVO.LastBuildDate;
                    channeCinemaVO.Link = serChannelVO.Link;
                    channeCinemaVO.ListaItem = serChannelVO.ListaItem;
                    channeCinemaVO.NomeChannel = serChannelVO.NomeChannel;
                    channeCinemaVO.PubDate = serChannelVO.PubDate;
                }

                sessionInputStream.Dispose();
            }
            catch (Exception)
            {
                throw new Exception("Erro ao ler os dados do aquivo.");
            }
        }
        #endregion
    }
}
