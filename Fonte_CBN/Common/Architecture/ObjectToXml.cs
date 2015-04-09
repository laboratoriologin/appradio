using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace RadioPlayer.Common.Architecture
{
   public class ObjectToXml
    {
       private static object data;

            #region Restore
            /// <summary>
            /// Efetua leitura do arquivo xml na StorageFile retornando o conteudo do xml em objeto
            /// </summary>
            /// <typeparam name="T">Tipo do objeto</typeparam>
            /// <param name="filename">Nome do arquivo, quando não informando utilizara o nome da classe + ".xml</param>
            /// <returns></returns>        
            static async public Task<T> Restore<T>(string filename = null)
            {
                await Windows.System.Threading.ThreadPool.RunAsync((sender) => ObjectToXml.RestoreAsync<T>(filename).Wait(), Windows.System.Threading.WorkItemPriority.Normal);
                return (T)data;
            }

            static async private Task RestoreAsync<T>(string filename)
            {
                try
                {
                    if (string.IsNullOrEmpty(filename))
                    {
                        filename = typeof(T).Name + ".xml";
                    }

                    IReadOnlyList<StorageFile> storageFileList = await ApplicationData.Current.LocalFolder.GetFilesAsync();
                    StorageFile sessionFile = (from StorageFile s in storageFileList where s.Name == filename select s).FirstOrDefault();
                    if (sessionFile == null)
                    {
                        data = default(T);
                    }

                    using (Stream fileStream = await sessionFile.OpenStreamForReadAsync())
                    {
                        var sessionSerializer = new DataContractSerializer(typeof(List<object>), new Type[] { typeof(T) });
                        data = (T)sessionSerializer.ReadObject(fileStream);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            #endregion

            #region Save
            /// <summary>
            /// Salva o conteudo do objeto em arquivo xml na StorageFile        
            /// </summary>
            /// <param name="obj">Objeto a ser salavo</param>
            /// <param name="filename">Nome do arquivo, quando não informando utilizara o nome da classe + ".xml"</param>
            /// <returns></returns>
            static async public Task Save(object obj, string filename = null)
            {
                try
                {
                    await Windows.System.Threading.ThreadPool.RunAsync((sender) => ObjectToXml.SaveAsync(obj, filename).Wait(), Windows.System.Threading.WorkItemPriority.Normal);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            static async private Task SaveAsync(object obj, string filename)
            {
                if (string.IsNullOrEmpty(filename))
                {
                    filename = obj.GetType().Name + ".xml";
                }

                MemoryStream sessionData = new MemoryStream();
                DataContractSerializer serializer = new DataContractSerializer(typeof(List<object>), new Type[] { obj.GetType() });
                serializer.WriteObject(sessionData, obj);

                StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
                using (Stream fileStream = await file.OpenStreamForWriteAsync())
                {
                    sessionData.Seek(0, SeekOrigin.Begin);
                    await sessionData.CopyToAsync(fileStream);
                    await fileStream.FlushAsync();
                }
            }
            #endregion
        }
    
}
