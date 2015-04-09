using RadioPlayer.Beans;
using RadioPlayer.Beans.BO;
using RadioPlayer.Beans.ValueObject;
using RadioPlayer.Beans.ValueObject.ValueObject.View.Principal;
using RadioPlayer.Beans.ValueObject.Xml.Salvador;
using RadioPlayer.Model.Beans;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Data.Xml.Dom;
using Windows.Networking.Connectivity;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace RadioPlayer.Common.Architecture
{
    /// <summary>
    /// Classe de utilizadades do sistema
    /// </summary>
    public static class Util
    {
        private static ObservableCollection<ItemVO> lstItemVO = new ObservableCollection<ItemVO>();

        private static ItemVO itemVO = new ItemVO();

        /// <summary>
        /// Atríbuto grupo notícia.
        /// </summary>
        private static GrupoNoticiasItemBLL grupoNoticiasItemBLL = new GrupoNoticiasItemBLL(1);

        /// <summary>
        /// Atríbuto main.
        /// </summary>
        private static MainPage main = new MainPage();

        /// <summary>
        /// Método para converter hexa para brush.
        /// </summary>
        /// <param name="hexaColor">Color em hexadecimal.</param>
        /// <returns>Color Brush</returns>
        public static SolidColorBrush GetColorFromHexa(string hexaColor)
        {
            return new SolidColorBrush(
                Color.FromArgb(
                    255,
                    Convert.ToByte(hexaColor.Substring(1, 2), 16),
                    Convert.ToByte(hexaColor.Substring(3, 2), 16),
                    Convert.ToByte(hexaColor.Substring(5, 2), 16)
                )
            );
        }

        /// <summary>
        /// Método para converter List em ObservableCollection.
        /// </summary>
        /// <typeparam name="T">Tipo IEnumerable</typeparam>
        /// <param name="enumerableList">Tipo IEnumerable</param>
        /// <returns>ObservableCollection fortemente</returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerableList)
        {
            if (enumerableList != null)
            {
                //create an emtpy observable collection object
                var observableCollection = new ObservableCollection<T>();

                //loop through all the records and add to observable collection object
                foreach (var item in enumerableList)
                    observableCollection.Add(item);

                //return the populated observable collection
                return observableCollection;
            }
            return null;
        }

        /// <summary>
        /// Método assíncrono verifica se o arquivo existe
        /// </summary>
        /// <param name="nomeArquivo">Nome do arquivo</param>
        /// <returns>Tarefa booleana</returns>
        public static async Task<bool> ExistsFileAsync(string nomeArquivo)
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
        /// Property that returns the connection profile [ ie, availability of Internet ]
        /// Interface type can be [ 1,6,9,23,24,37,71,131,144 ]
        /// 1 - > Some other type of network interface.
        /// 6 - > An Ethernet network interface.
        /// 9 - > A token ring network interface.
        /// 23 -> A PPP network interface.
        /// 24 -> A software loopback network interface.
        /// 37 -> An ATM network interface.
        /// 71 -> An IEEE 802.11 wireless network interface.
        /// 131 -> A tunnel type encapsulation network interface.
        /// 144 -> An IEEE 1394 (Firewire) high performance serial bus network interface.
        /// </summary>
        public static bool IsConnectedToNetwork
        {
            get
            {
                var profile = NetworkInformation.GetInternetConnectionProfile();
                if (profile != null)
                {
                    if (!profile.ProfileName.Contains("Emulator"))
                    {
                        var interfaceType = profile.NetworkAdapter.IanaInterfaceType;
                        return interfaceType == 71 || interfaceType == 6;
                    }
                    else
                        return false;
                }
                return false;
            }
        }

        /// <summary>
        /// Método que cria o nome do arquivo ne parão.
        /// </summary>
        /// <param name="nomeChannel">nome do channel(categoria da url)</param>
        /// <param name="id">id do channel</param>
        /// <returns>nome do arquivo no padrão</returns>
        public static string NameFile(string nomeChannel, int id)
        {
            return RemoverAcentos(nomeChannel) + "_" + id.ToString() + ".xml";
        }

        /// <summary>
        /// Método que retira caracteres especiais da string
        /// </summary>
        /// <param name="input">String Html</param>
        /// <returns>String Html sem caracteres especiais</returns>
        public static string ReplaceSpecialCharacters(string input)
        {
            input = Regex.Replace(input, "&nbsp;", " ");
            input = Regex.Replace(input, "&lt;", " ");
            input = Regex.Replace(input, "&gt;", " ");
            input = Regex.Replace(input, "&amp;", " ");
            input = Regex.Replace(input, "&cent;", " ");
            input = Regex.Replace(input, "&pound;", " ");
            input = Regex.Replace(input, "&yen;", " ");
            input = Regex.Replace(input, "&euro;", " ");
            input = Regex.Replace(input, "&sect;", " ");
            input = Regex.Replace(input, "&copy;", " ");
            input = Regex.Replace(input, "&reg;", " ");
            input = Regex.Replace(input, "&trade;", " ");
            input = Regex.Replace(input, "\t", string.Empty);
            input = Regex.Replace(input, "\n", string.Empty);

            return input;
        }

        /// <summary>
        /// Método que coloca e retira o temp no arquivo.
        /// </summary>
        /// <param name="nameFile">Nome do arquivo completo</param>
        /// <param name="temp">Caso true insere o .temp no fim do arquivo, caso false retira o .temp.</param>
        /// <returns>O nome do aruqivo.</returns>
        public static string NameFile(string nameFile, bool temp)
        {
            if (temp)
            {
                return nameFile + ".temp";
            }
            else
            {
                return nameFile.Substring(0, nameFile.Length - 5);
            }
        }

        /// <summary>
        /// Metodo para compartilhar no Facebook.
        /// </summary>
        /// <param name="linkNoticia">Link do item a ser compartilhado</param>
        /// <returns>Link de compartilhamento</returns>
        public static string ShareFacebook(string linkNoticia)
        {
            string linkDefaultShareFacebook = "http://www.facebook.com/sharer/sharer.php?u=";

            string linkToShare = linkDefaultShareFacebook + linkNoticia;

            return linkToShare;
        }

        /// <summary>
        /// Metodo para compartilhar no Twitter.
        /// </summary>
        /// <param name="linkItem">Link do item a ser compartilhado</param>
        /// <param name="tituloNoticia">Titulo do item a ser compartilhado</param>
        /// <returns>Link de compartilhamento</returns>
        public static string ShareTwitter(string linkItem, string tituloNoticia)
        {
            string linkDefaultShareTwitter = "http://twitter.com/share?url=";

            string linkToShare = linkDefaultShareTwitter + linkItem + "&text=" + tituloNoticia;

            return linkToShare;
        }

        /// <summary>
        /// Metodo para formatar datas de eventos culturais.
        /// </summary>
        /// <param name="dataOriginal">Data vinda do xml</param>
        /// <param name="dataMes">Retornar somente dia e mês</param>
        /// <returns>Data formatada</returns>
        public static string FormatarData(string dataOriginal, bool dataMes)
        {
            string dia = string.Empty;
            string mes = string.Empty;

            switch (dataOriginal.Substring(0, dataOriginal.IndexOf(",")))
            {
                case "Mon":
                    dia = "Segunda";
                    break;
                case "Tue":
                    dia = "Terça";
                    break;
                case "Wed":
                    dia = "Quarta";
                    break;
                case "Thu":
                    dia = "Quinta";
                    break;
                case "Fri":
                    dia = "Sexta";
                    break;
                case "Sat":
                    dia = "Sábado";
                    break;
                case "Sun":
                    dia = "Domingo";
                    break;
            }

            switch (dataOriginal.Substring(dataOriginal.IndexOf(",") + 5, 3))
            {
                case "Jan":
                    mes = "Janeiro";
                    break;
                case "Feb":
                    mes = "Fevereiro";
                    break;
                case "Mar":
                    mes = "Março";
                    break;
                case "Apr":
                    mes = "Abril";
                    break;
                case "May":
                    mes = "Maio";
                    break;
                case "Jun":
                    mes = "Junho";
                    break;
                case "Jul":
                    dia = "Julho";
                    break;
                case "Aug":
                    mes = "Agosto";
                    break;
                case "Sep":
                    mes = "Setembro";
                    break;
                case "Oct":
                    mes = "Outubro";
                    break;
                case "Nov":
                    mes = "Novembro";
                    break;
                case "Dec":
                    mes = "Dez";
                    break;
            }

            string dataFormatada = string.Empty;

            if (!dataMes)
            {
                dataFormatada = dia + ", " + dataOriginal.Substring(dataOriginal.IndexOf(",") + 2, 3) + "de " + mes + " de " + dataOriginal.Substring(dataOriginal.IndexOf(",") + 8, 5) + ", " + dataOriginal.Substring(dataOriginal.IndexOf(",") + 14, 5);
            }
            else
            {
                dataFormatada = dia + ", " + dataOriginal.Substring(dataOriginal.IndexOf(",") + 2, 3) + "de " + mes;
            }

            return dataFormatada;
        }

        /// <summary>
        /// Função para achar Cor do canal da notícia clicada.
        /// </summary>
        /// <param name="idCanal">Id do canal da notícia clicada</param>
        /// <returns>Retorna a Cor relacionada para o canal</returns>
        public static string AcharCorCanal(int idCanal)
        {
            string corCanal = string.Empty;

            switch (idCanal)
            {
                ////Noticias
                case 22:
                case 2:
                case 12:
                case 13:
                case 14:
                case 152:
                case 1:
                    corCanal = "#FF142E";
                    break;
                ////Entretenimento
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                case 51:
                case 96:
                case 88:
                case 97:
                case 32:
                case 36:
                    corCanal = "#CC1D3C";
                    break;
                ////Esportes
                case 3:
                case 4:
                case 48:
                case 49:
                case 25:
                    corCanal = "#406700";
                    break;
            }

            return corCanal;
        }

        /// <summary>
        /// Função para achar Cor do rádio clicada.
        /// </summary>
        /// <param name="idCanal">Id do canal da rádio clicada</param>
        /// <returns>Retorna a Cor relacionada para o rádio</returns>
        public static void AtualizarImagemBotoes(int idRadio)
        {

            Constantes.imgPauseFlag = Constantes.imgPauseBranco;
            Constantes.imgFavoritoDesabilitadoFlag = Constantes.imgFavoritoDesabilitadoBranco;
            Constantes.imgFavoritoFlag = Constantes.imgFavoritoBranco;

            Constantes.imgPlayFlag = Constantes.imgPlayBranco;
            Constantes.imgAvancarFlag = Constantes.imgAvancarBranco;
            Constantes.imgAvancarDesabilitadoFlag = Constantes.imgAvancarDesabilitadoBranco;
            Constantes.imgRetrocederFlag = Constantes.imgRetrocederBranco;
            Constantes.imgRetrocederDesabilitadoFlag = Constantes.imgRetrocederDesabilitadoBranco;
            main.UpdateLayout();

        }

        /// <summary>
        /// Remove acentos
        /// </summary>
        /// <param name="strTexto">Texto que deve ser tratado</param>
        /// <returns>string sem acentos</returns>
        private static string RemoverAcentos(string strTexto)
        {
            strTexto = strTexto.ToString();
            strTexto = Regex.Replace(strTexto, "[ÁÀÂÃ]", "A");
            strTexto = Regex.Replace(strTexto, "[ÉÈÊ]", "E");
            strTexto = Regex.Replace(strTexto, "[Í]", "I");
            strTexto = Regex.Replace(strTexto, "[ÓÒÔÕ]", "O");
            strTexto = Regex.Replace(strTexto, "[ÚÙÛÜ]", "U");
            strTexto = Regex.Replace(strTexto, "[Ç]", "C");

            strTexto = Regex.Replace(strTexto, "[áàâã]", "a");
            strTexto = Regex.Replace(strTexto, "[éèê]", "e");
            strTexto = Regex.Replace(strTexto, "[í]", "i");
            strTexto = Regex.Replace(strTexto, "[óòôõ]", "o");
            strTexto = Regex.Replace(strTexto, "[úùûü]", "u");
            strTexto = Regex.Replace(strTexto, "[ç]", "c");

            strTexto = Regex.Replace(strTexto, "[ ]", "_");

            return strTexto;
        }

        public async static void EditarColor(string color)
        {
            //Salvar objeto em xml 

            DataSourceRadio.Cor.Cor = color;

            await ObjectToXml.Save(DataSourceRadio.Cor.Cor);

            //Ler xml retornando objeto 
            DataSourceRadio.Cor = await ObjectToXml.Restore<ColorVO>();

        }




        public async static void SaveBoot(string filename, bool flagClick)
        {
            try
            {
                if (flagClick)
                {
                    Constantes.strEstacao = filename;
                }
                else
                {
                    filename = Constantes.strEstacao;
                }
                XmlDocument dom = new XmlDocument();

                StorageFile st = await Windows.Storage.KnownFolders.DocumentsLibrary.CreateFileAsync("bootTheme.xml", CreationCollisionOption.ReplaceExisting);

                using (Stream fileStream = await st.OpenStreamForWriteAsync())
                {
                    XmlElement x12 = dom.CreateElement("name");
                    x12.InnerText = filename;
                    dom.AppendChild(x12);
                }
                if (dom.SaveToFileAsync(st).AsTask().Status == TaskStatus.RanToCompletion)
                {
                    if (dom.SaveToFileAsync(st).AsTask().IsCompleted)
                    {
                        await dom.SaveToFileAsync(st);
                    }
                }
            }

            catch (Exception)
            {

                Util.SaveBoot(Constantes.strEstacao, false);
            }
        }

        /// <summary>
        /// Método assincrono estático para verificar a cor por xml.
        /// </summary>
        /// <param name="color">Descrição da cor</param>
        public async static void VerificarCorXml(string color)
        {
            DataSourceRadio.Cor = await ObjectToXml.Restore<ColorVO>();

            if (DataSourceRadio.Cor.Cor != color)
            {
                if (DataSourceRadio.Cor.Cor.Equals("#FFFFFF"))
                {
                    Util.AtualizarImagemBotoes(1);
                }
                else
                {
                    Util.AtualizarImagemBotoes(0);
                }
            }
        }

        /// <summary>
        /// Método assincrono estático para verificar a cor.
        /// </summary>
        /// <param name="color">Descrição da cor</param>
        public static void VerificarCor(string color)
        {
            if (color.Equals("#FFFFFF"))
            {
                Util.AtualizarImagemBotoes(1);
            }
            else
            {
                Util.AtualizarImagemBotoes(0);
            }
        }

        //public async static void VerificarBoot()
        //{
        //    try
        //    {
        //        Constantes.strEstacao = Constantes.ResourceBoot.GetString("boot");
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}

        public static string DecodeFromUtf8(this string utf8String)
        {
            // copy the string as UTF-8 bytes.
            byte[] utf8Bytes = new byte[utf8String.Length];
            for (int i = 0; i < utf8String.Length; ++i)
            {
                //Debug.Assert( 0 <= utf8String[i] && utf8String[i] <= 255, "the char must be in byte's range");
                utf8Bytes[i] = (byte)utf8String[i];
            }

            return Encoding.UTF8.GetString(utf8Bytes, 0, utf8Bytes.Length);
        }

        public async static void VerificarBootXml()
        {


            try
            {
                //// load xml file

                StorageFile storageFile = Windows.Storage.KnownFolders.DocumentsLibrary.GetFileAsync("bootTheme.xml").AsTask().Result;
                XmlDocument dom = new XmlDocument();
                Windows.Data.Xml.Dom.XmlLoadSettings loadSettings = new Windows.Data.Xml.Dom.XmlLoadSettings();
                loadSettings.ProhibitDtd = false; // sample
                loadSettings.ResolveExternals = false; // sample
                XmlDocument doc = XmlDocument.LoadFromFileAsync(storageFile).AsTask().Result;

                using (Stream fileStream = storageFile.OpenStreamForReadAsync().Result)
                {
                    XmlElement x12 = doc.GetElementById("name");
                    DataSourceRadio.ObjBootThemeVO.strBoot = doc.InnerText;
                    Constantes.strEstacao = doc.InnerText;
                }
            }

            catch (Exception)
            {
                SaveBoot("1", false);
            }

        }

        public static BitmapImage ImageFromRelativePath(FrameworkElement parent, string path)
        {
            var uri = new Uri(parent.BaseUri, path);
            BitmapImage result = new BitmapImage();
            result.UriSource = uri;
            return result;
        }

        public static void LoadUIDataBiding()
        {
            int[] idBahiaFM = { (int)Constantes.EnumNoticias.Salvador, (int)Constantes.EnumNoticias.nemTeConto };
            int[] idBahiaFMSul = { (int)Constantes.EnumNoticias.Salvador, (int)Constantes.EnumNoticias.nemTeConto };
            int[] idGlobo = { (int)Constantes.EnumNoticias.transito, (int)Constantes.EnumNoticias.noticias, (int)Constantes.EnumNoticias.musica };
            int[] idCBN = { (int)Constantes.EnumNoticias.noticias, (int)Constantes.EnumNoticias.noticiaMundo };


            DataSourceRadio.LstCategoriaBahiaFM = Util.ToObservableCollection(grupoNoticiasItemBLL.LoadDataTipoNoticia(idBahiaFM, "#FFFFFF"));
            DataSourceRadio.LstCategoriaBahiaFMSul = Util.ToObservableCollection(grupoNoticiasItemBLL.LoadDataTipoNoticia(idBahiaFMSul, "#FFFFFF"));
            DataSourceRadio.LstCategoriaGloboFM = Util.ToObservableCollection(grupoNoticiasItemBLL.LoadDataTipoNoticia(idGlobo, "#FFFFFF"));
            DataSourceRadio.LstCategoriaCBN = Util.ToObservableCollection(grupoNoticiasItemBLL.LoadDataTipoNoticia(idCBN, "#FFFFFF"));

            grupoNoticiasItemBLL.LoadDataGrupoNoticias(1, 22);
            grupoNoticiasItemBLL.LoadDataGrupoNoticias(1, 1);
            grupoNoticiasItemBLL.LoadDataGrupoNoticias(6, 0);
        }


        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        //Criar função para utilizar a API
        public static bool IsConnectedToInternet()
        {

            int Desc;
            return InternetGetConnectedState(out Desc, 0);

        }

        /// <summary>
        /// Método para veridicar se conexão existe.
        /// </summary>
        /// <returns></returns>
        public static bool Conexao()
        {
            return IsConnectedToInternet();
        }

        public static ObservableCollection<ItemVO> CarregaGridSalvador()
        {
            lstItemVO = new ObservableCollection<ItemVO>();



            if (DataSourceRadio.ListaItemSalvador.Count > 0)
            {
                foreach (var itemSalvadorVO in DataSourceRadio.ListaItemSalvador.ToList())
                {
                    itemVO = new ItemVO();
                    itemVO.CorLetraNoticiaRadio = Constantes.strCor;
                    itemVO.CorLetraNoticiaRadioBahiaFM = Constantes.strCor;
                    itemVO.CorLetraNoticiaRadio = Constantes.strCor;
                    itemVO.ImgCorMais = Constantes.strCor;

                    itemVO.IdChannel = 12;
                    itemVO.CorStackPanelItem = Constantes.strCor;
                    if (itemSalvadorVO.Guid != null)
                    {
                        itemVO.Guid = Convert.ToInt32(itemSalvadorVO.Guid);

                        if (itemSalvadorVO.Title != null)
                        {
                            itemVO.Titulo = itemSalvadorVO.Title;
                        }
                        else
                        {
                            itemVO.Titulo = string.Empty;
                        }

                        if (itemSalvadorVO.Description != null)
                        {
                            if (itemSalvadorVO.Description.Length > 150)
                            {
                                itemVO.SubTitulo = itemSalvadorVO.Description.Substring(0, 150) + "  ...";
                            }
                            else
                            {
                                itemVO.SubTitulo = itemSalvadorVO.Description;
                            }
                        }
                        else
                        {
                            itemVO.Titulo = string.Empty;
                        }

                        if (itemSalvadorVO.Link != null)
                        {
                            itemVO.Link = itemSalvadorVO.Link;
                        }
                        else
                        {
                            itemVO.Link = string.Empty;
                        }



                        itemVO.Descricao = itemSalvadorVO.Content;
                        // ... Input string.
                        string input = itemSalvadorVO.Description;
                        ObservableCollection<ImageChannelSalvadorVO> lstImageChannelSalvadorVO = new ObservableCollection<ImageChannelSalvadorVO>();
                        ImageChannelSalvadorVO objImageSalvadorVO = new ImageChannelSalvadorVO();

                        itemVO.ImagemFundo = new RadioPlayer.Beans.ValueObject.ValueObject.Xml.ImageVO();

                        if (itemSalvadorVO.ListaImage.Count > 0)
                        {
                            foreach (var itemImage in itemSalvadorVO.ListaImage)
                            {

                                if (itemImage.Url != null)
                                {
                                    itemVO.ImagemFundo.Url = itemImage.Url;
                                }


                                itemVO.ImgCorMais = "ms-appx:///Assets/+.png";
                            }
                        }
                        else
                        {
                            itemVO.HasPodCast = true;
                        }
                    }
                    else
                    {
                        itemVO.Descricao = string.Empty;
                    }

                    lstItemVO.Add(itemVO);
                }
            }

            return lstItemVO;

        }

        public static ObservableCollection<ItemVO> CarregaGridAgenda()
        {
            lstItemVO = new ObservableCollection<ItemVO>();

            itemVO.Titulo = "Cinema";
            itemVO.SubTitulo = "Cinema";
            itemVO.CorLetraNoticiaRadio = "#343434";
            itemVO.HasGridImagemIBahia = true;
            itemVO.HasPodCast = false;
            itemVO.HasGridPromocao = false;
            itemVO.ImagemFundo = new RadioPlayer.Beans.ValueObject.ValueObject.Xml.ImageVO();

            itemVO.ImagemFundo.Url = "ms-appx:///Assets/2_icone_globo_cultura_cinema.png";
            itemVO.ImgCorMais = "ms-appx:///Assets/+.png";

            lstItemVO.Add(itemVO);

            itemVO = new ItemVO();
            itemVO.Titulo = "Exposições";
            itemVO.SubTitulo = "Exposições";
            itemVO.CorLetraNoticiaRadio = "#343434";
            itemVO.HasGridImagemIBahia = true;
            itemVO.HasPodCast = false;
            itemVO.HasGridPromocao = false;
            itemVO.ImagemFundo = new RadioPlayer.Beans.ValueObject.ValueObject.Xml.ImageVO();

            itemVO.ImagemFundo.Url = "ms-appx:///Assets/4_icone_globo_cultura_exposicao.png";

            lstItemVO.Add(itemVO);

            itemVO = new ItemVO();
            itemVO.Titulo = "Teatro";
            itemVO.SubTitulo = "Teatro";
            itemVO.CorLetraNoticiaRadio = "#343434";
            itemVO.HasGridImagemIBahia = true;
            itemVO.HasGridPromocao = false;
            itemVO.ImagemFundo = new RadioPlayer.Beans.ValueObject.ValueObject.Xml.ImageVO();


            itemVO.ImagemFundo.Url = "ms-appx:///Assets/1_icone_globo_cultura_teatro.png";


            lstItemVO.Add(itemVO);

            itemVO = new ItemVO();
            itemVO.Titulo = "Shows";
            itemVO.SubTitulo = "Shows";
            itemVO.CorLetraNoticiaRadio = "#343434";
            itemVO.HasGridImagemIBahia = true;
            itemVO.HasPodCast = false;
            itemVO.HasGridPromocao = false;
            itemVO.ImagemFundo = new RadioPlayer.Beans.ValueObject.ValueObject.Xml.ImageVO();
            itemVO.ImagemFundo.Url = "ms-appx:///Assets/3_icone_globo_cultura_musica.png";

            lstItemVO.Add(itemVO);

            return lstItemVO;
        }


        public static ObservableCollection<ItemVO> CarregaGridProgramacao()
        {
            lstItemVO = new ObservableCollection<ItemVO>();

      
          
            if (DataSourceRadio.ListaItemProgramacao.Count > 0)
            {
               // foreach (var itemProgramacaoVO in DataSourceRadio.ListaItemProgramacao.Take(4).ToList())
             
                foreach (var itemProgramacaoVO in DataSourceRadio.ListaItemProgramacao.ToList())
                {
                    itemVO = new ItemVO();
                    itemVO.CorLetraNoticiaRadio = Constantes.strCor;
                    itemVO.CorLetraNoticiaRadioBahiaFM = Constantes.strCor;
                    itemVO.CorLetraNoticiaRadio = Constantes.strCor;
                    itemVO.ImgCorMais = Constantes.strCor;

             

                    itemVO.IdChannel = 92;
                    itemVO.CorStackPanelItem = Constantes.strCor;


                    if (itemProgramacaoVO.Guid != null)
                    {
                           itemVO.Guid = Convert.ToInt32(itemProgramacaoVO.Guid);

                            if (itemProgramacaoVO.Title != null)
                            {
                                itemVO.Titulo = itemProgramacaoVO.Title;
                            }
                            else
                            {
                                itemVO.Titulo = string.Empty;
                            }

                            if (itemProgramacaoVO.Description != null)
                            {
                                if (itemProgramacaoVO.Description.Length > 150)
                                {
                                    itemVO.SubTitulo = itemProgramacaoVO.Description.Substring(0, 150) + "  ...";
                                }
                                else
                                {
                                    itemVO.SubTitulo = itemProgramacaoVO.Description;
                                }
                            }
                            else
                            {
                                itemVO.Titulo = string.Empty;
                            }

                            if (itemProgramacaoVO.Link != null)
                            {
                                itemVO.Link = itemProgramacaoVO.Link;
                            }
                            else
                            {
                                itemVO.Link = string.Empty;
                            }



                            itemVO.Descricao = itemProgramacaoVO.Content;
                            // ... Input string.
                            string input = itemProgramacaoVO.Description;
                            ObservableCollection<ImageChannelSalvadorVO> lstImageChannelSalvadorVO = new ObservableCollection<ImageChannelSalvadorVO>();
                            ImageChannelSalvadorVO objImageSalvadorVO = new ImageChannelSalvadorVO();

                            itemVO.ImagemFundo = new RadioPlayer.Beans.ValueObject.ValueObject.Xml.ImageVO();

                            if (itemProgramacaoVO.ListaImage.Count > 0)
                            {
                                foreach (var itemImage in itemProgramacaoVO.ListaImage)
                                {

                                    if (itemImage.Url != null)
                                    {
                                        itemVO.ImagemFundo.Url = itemImage.Url;
                                    }


                                    itemVO.ImgCorMais = "ms-appx:///Assets/+.png";
                                }
                            }
                            else
                            {
                                itemVO.HasPodCast = true;
                            }
                        
                    }
                    else
                    {
                        itemVO.Descricao = string.Empty;
                    }

                    foreach (var itemCategoria in itemProgramacaoVO.ListaCategoria)
                    {
                        itemVO.GuidCategory = itemCategoria.Guid;
                    }

                    lstItemVO.Add(itemVO);
                }
            }

            return lstItemVO;

        }


        public static string CarregaHtml()
        {

            string body = " <html> "
            + " <head> "
             + "    <meta charset='utf-8' /> "
             + "    <title></title> "

            + " <SCRIPT LANGUAGE=JavaScript> "

            + " OAS_url ='http://ads.globo.com/RealMedia/ads/'; "
            + " OAS_listpos = 'Top,x07,Right,Right3,Middle,Middle2,Middle3'; "
            + " OAS_query = ''; "
            + " OAS_sitepage = 'afbahiafm/app'; "
            + " OAS_target = '_blank'; "

            + " OAS_version = 10; "
            + " OAS_rn = '001234567890'; OAS_rns = '1234567890'; "
            + " OAS_rn = new String (Math.random()); OAS_rns = OAS_rn.substring (2, 11); "

            + "  function OAS_NORMAL(pos) {  "
            + "  document.write('<A HREF='' + OAS_url + 'click_nx.ads/' + OAS_sitepage + '/1' + OAS_rns + '@' + OAS_listpos + '!' + pos + OAS_query + '' TARGET=_top>'); "
            + "  document.write('<IMG SRC='' + OAS_url + 'adstream_nx.ads/' + OAS_sitepage + '/1' + OAS_rns + '@' + OAS_listpos + '!' + pos + OAS_query + '' BORDER=0></A>'); "
            + " } "

            + " </SCRIPT> "
            + " <SCRIPT LANGUAGE=JavaScript1.1> "

            + " OAS_version = 11; "
            + " if (navigator.userAgent.indexOf('Mozilla/3') != -1) "
            + " OAS_version = 10; "
            + " if (OAS_version >= 11) "
            + " document.write('<SCR'+ 'IPT LANGUAGE=JavaScript1.1 SRC='' + OAS_url + 'adstream_mjx.ads/' + OAS_sitepage + '/1' + OAS_rns + '@' + OAS_listpos + OAS_query + ''><//SCRIPT>');  "
            + " </SCRIPT><SCRIPT LANGUAGE='JavaScript'> "
             + "  document.write(''); "
            + " function OAS_AD(pos) { "
            + " if (OAS_version >= 11) "
            + " OAS_RICH(pos); "
            + " else "
             + "  OAS_NORMAL(pos); "
            + " } "
            + " </SCRIPT> "
            + " </head> "
            + " <body> "
            + "   <script> "
            + "    OAS_AD('x07'); "
            + "    </script> "
            + " </body> "
            + "  </html> ";

            return body;
        }

    }
}
