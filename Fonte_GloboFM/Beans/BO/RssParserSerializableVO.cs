
using RadioPlayer.Beans.ValueObject.ValueObject.Xml;
using RadioPlayer.Common.Architecture;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.System.Threading;

namespace RadioPlayer.Beans.BO
{
    public class XmlParserSerializableVO
    {

        #region Atríbutos e propriedades
        /// <summary>
        /// Atríbuto privado Lista ProdutoVO 
        /// </summary>
        private static ObservableCollection<CategoriaVO> data = new ObservableCollection<CategoriaVO>();

        /// <summary>
        /// Gets or sets - propriedade Data do tipo Lista ProdutoVO
        /// </summary>
        public static ObservableCollection<CategoriaVO> Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }

        /// <summary>
        /// Gets or sets - propriedade do tipo StorageFile
        /// </summary>
        public static StorageFile Arquivo { get; set; }
        #endregion

        #region Métodos

        /// <summary>
        /// Método que salva o arquivo serializado
        /// </summary>
        /// <typeparam name="T">Passagem por pilha</typeparam>
        /// <returns>Tarefa ThreadPool.RunAsync</returns>
        public async Task Salva<T>()
        {
            if (await VerificaArquivoExisteAsync(Constantes.CaminhoLocal))
            {
                await ThreadPool.RunAsync((sender) => XmlParserSerializableVO.SalvaAsync<T>().Wait(), WorkItemPriority.Normal);
            }
            else
            {
                Arquivo = await ApplicationData.Current.LocalFolder.CreateFileAsync(Constantes.CaminhoLocal, CreationCollisionOption.ReplaceExisting);
                await ThreadPool.RunAsync((sender) => XmlParserSerializableVO.SalvaAsync<T>().Wait(), WorkItemPriority.Normal);
            }
        }

        /// <summary>
        /// Método que atualiza o arquivo serializado
        /// </summary>
        /// <typeparam name="T">Passagem por pilha</typeparam>
        /// <returns>Tarefa ThreadPool.RunAsync</returns>
        public async Task Restaura<T>()
        {
            if (await VerificaArquivoExisteAsync(Constantes.CaminhoLocal))
            {
                await ThreadPool.RunAsync((sender) => XmlParserSerializableVO.RestauraAsync<T>().Wait(), WorkItemPriority.Normal);
            }
            else
            {
                Arquivo = await ApplicationData.Current.LocalFolder.CreateFileAsync(Constantes.CaminhoLocal);
            }
        }

        /// <summary>
        /// Método assíncrono verifica se o arquivo existe
        /// </summary>
        /// <param name="nomeArquivo">Nome do arquivo</param>
        /// <returns>Tarefa booleana</returns>
        public async Task<bool> VerificaArquivoExisteAsync(string nomeArquivo)
        {
            try
            {
                await ApplicationData.Current.LocalFolder.GetFileAsync(nomeArquivo);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Método assíncrono salvar arquivo
        /// </summary>
        /// <typeparam name="T">Passagem por pilha</typeparam>
        /// <returns>Tarefa assíncrona</returns>
        private static async Task SalvaAsync<T>()
        {
            StorageFile sessionFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(Constantes.CaminhoLocal, CreationCollisionOption.ReplaceExisting);
            IRandomAccessStream sessionRandomAccess = await sessionFile.OpenAsync(FileAccessMode.ReadWrite);
            IOutputStream sessionOutputStream = sessionRandomAccess.GetOutputStreamAt(0);

            var serializer = new XmlSerializer(typeof(ObservableCollection<CategoriaVO>), new Type[] { typeof(T) });

            serializer.Serialize(sessionOutputStream.AsStreamForWrite(), data);
            sessionRandomAccess.Dispose();
            await sessionOutputStream.FlushAsync();
            sessionOutputStream.Dispose();
        }

        /// <summary>
        /// Método assíncrono restaura arquivo
        /// </summary>
        /// <typeparam name="T">Passagem por pilha</typeparam>
        /// <returns>Tarefa assíncrona</returns>
        private static async Task RestauraAsync<T>()
        {
            StorageFile sessionFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(Constantes.CaminhoLocal, CreationCollisionOption.OpenIfExists);
            if (sessionFile == null)
            {
                return;
            }
            IInputStream sessionInputStream = await sessionFile.OpenReadAsync();

            var serializer = new XmlSerializer(typeof(ObservableCollection<CategoriaVO>), new Type[] { typeof(T) });
            data = (ObservableCollection<CategoriaVO>)serializer.Deserialize(sessionInputStream.AsStreamForRead());
            sessionInputStream.Dispose();
        }
        #endregion
    }
}
