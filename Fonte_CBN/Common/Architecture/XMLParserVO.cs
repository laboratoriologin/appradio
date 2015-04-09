using RadioPlayer.Beans.ValueObject.ValueObject.Xml;
using RadioPlayer.Beans.ValueObject.ValueObject.Xml.AgendaCulturalXML;
using RadioPlayer.Beans.ValueObject.ValueObject.Xml.AgendaCulturalXML.Cinema;
using Cinema = RadioPlayer.Beans.ValueObject.ValueObject.Xml.AgendaCulturalXML.Cinema.Cartaz;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Data.Xml.Dom;
using System.Net.Http;
using System.IO;
using RadioPlayer.Beans.ValueObject.ValueObject.Xml.NoticiaCorreioXML;
using RadioPlayer.Beans.ValueObject.ValueObject.Xml.PodCastXML;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using RadioPlayer.Beans;
using RadioPlayer.Beans.ValueObject.Xml.Promocao;
using RadioPlayer.Beans.ValueObject.Xml.Salvador;
using RadioPlayer.Beans.ValueObject.Xml.Programacao;
using RadioPlayer.Beans.ValueObject.Xml.ProgramacaoRadioXML;

namespace RadioPlayer.Common.Architecture
{
    /// <summary>
    /// Classe transformação do xml para VO
    /// </summary>
    public class XMLParserVO
    {
        #region Atríbuto

        #endregion;

        #region Métodos

        /// <summary>
        /// Método privado carrega o xml pela URL
        /// </summary>
        /// <param name="url">Url aonde está o xml</param>
        /// <returns> XmlDocument com os dados da url.</returns>
        public static async Task<XmlDocument> XDocToXmlDoc(string url)
        {
            try
            {
                Uri uri = new Uri(url);

                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(uri);
                //  response.EnsureSuccessStatusCode();

                // ReadAsStreamAsync() returns when the whole message is downloaded
                Stream stream = await response.Content.ReadAsStreamAsync();

                XDocument xdocXml = XDocument.Load(stream);

                //XDocument xdocXml = XDocument.Load(url);

                XmlDocument xmlDocument = new XmlDocument();
                //// TODO, verificar se o xml retorna vazio da url.
                // ... Input string.

             string objXDocument = xdocXml.Root.ToString().Replace("content:encoded", "content");
             //objXDocument = xdocXml.Root.ToString().Replace("content:encoded", "content");
             xmlDocument.LoadXml(objXDocument);
                return xmlDocument;

            }
            catch (TaskCanceledException ex)
            {
                throw new Exception("Erro ao ler os dados dos servidores do AppRadio", ex);
            }
        }

        /// <summary>
        /// Método para carregar o xml no VO
        /// </summary>
        /// <param name="channel"> Obj de channel do VO</param>
        public static async Task LoadXmlUrl(object channel)
        {
            try
            {
                if (channel.GetType().Equals(typeof(ChannelVO)))
                {
                    ChannelVO channelVO = (ChannelVO)channel;
                    await LoadXmChannelNoticialUrl(channelVO);
                }
                else if (channel.GetType().Equals(typeof(ChannelAgendaCulturalVO)))
                {
                    ChannelAgendaCulturalVO channelVO = (ChannelAgendaCulturalVO)channel;
                    await LoadXmChannelAgendaCulturalUrl(channelVO);
                }
                else if (channel.GetType().Equals(typeof(Cinema.ChannelCinemaVO)))
                {
                    Cinema.ChannelCinemaVO channelVO = (Cinema.ChannelCinemaVO)channel;
                    await LoadXmChannelCinemalUrl(channelVO);
                }
              
              
                else if (channel.GetType().Equals(typeof(ChannelPodCastVO)))
                {
                    ChannelPodCastVO channelVO = (ChannelPodCastVO)channel;
                    await LoadXmChannelPodCastUrl(channelVO);
                }

                else if (channel.GetType().Equals(typeof(ChannelProgramacaoVO)))
                {
                    ChannelProgramacaoVO channelVO = (ChannelProgramacaoVO)channel;
                     LoadXmChannelProgramacaoUrl(channelVO);
                }

                else if (channel.GetType().Equals(typeof(ChannelProgramacaoRadioXMLVO)))
                {
                    ChannelProgramacaoRadioXMLVO channelVO = (ChannelProgramacaoRadioXMLVO)channel;
                    await LoadXmChannelProgramacaoRadioXML(channelVO);
                }

                else if (channel.GetType().Equals(typeof(ChannelPromocaoVO)))
                {
                    ChannelPromocaoVO channelVO = (ChannelPromocaoVO)channel;
                    
                     LoadXmChannelPromocaoUrl(channelVO);

                 
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Método que carrega o xml local para o VO de nóticias ou de agenda cultural.
        /// </summary>
        /// <param name="channel">Obj channel VO ou channel agenda cultural vo.</param>
        /// <returns>Retorno Tarefa TASK.</returns>
        public static async Task LoadXmlLocalAsync(object channel)
        {
            try
            {
                if (channel.GetType().Equals(typeof(ChannelVO)))
                {
                    await XmlParserSerializableVO.RestoreAsync<ChannelVO>(Util.NameFile(((ChannelVO)channel).NomeChannel, ((ChannelVO)channel).Id), channel);
                }
                else if (channel.GetType().Equals(typeof(ChannelAgendaCulturalVO)))
                {
                    await XmlParserSerializableVO.RestoreAsync<ChannelAgendaCulturalVO>(Util.NameFile(((ChannelAgendaCulturalVO)channel).NomeChannel, ((ChannelAgendaCulturalVO)channel).Id), channel);
                }
                else if (channel.GetType().Equals(typeof(Cinema.ChannelCinemaVO)))
                {
                    await XmlParserSerializableVO.RestoreAsync<Cinema.ChannelCinemaVO>(Util.NameFile(((Cinema.ChannelCinemaVO)channel).NomeChannel + ((Cinema.ChannelCinemaVO)channel).Id, 0), channel);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método para salvar o xml em arquivo. Serialização.
        /// </summary>
        /// <param name="channelVO">Obj channel VO ou o channel agenda cultural VO.</param>
        /// <returns>Retorno Tarefa TASK.</returns>
        public static async Task SaveXmlLocalAsync(object channelVO)
        {
            try
            {
                if (channelVO.GetType().Equals(typeof(ChannelVO)))
                {
                    //// XmlParserSerializableVO.ObjChannelVO = (ChannelVO)channelVO;
                    await XmlParserSerializableVO.SaveAsync<ChannelVO>(Util.NameFile(((ChannelVO)channelVO).NomeChannel, ((ChannelVO)channelVO).Id), channelVO);
                }
                else if (channelVO.GetType().Equals(typeof(ChannelAgendaCulturalVO)))
                {
                    //// XmlParserSerializableVO.ObjChannelAgendaCulturalVO = (ChannelAgendaCulturalVO)channelVO;
                    await XmlParserSerializableVO.SaveAsync<ChannelAgendaCulturalVO>(Util.NameFile(((ChannelAgendaCulturalVO)channelVO).NomeChannel, ((ChannelAgendaCulturalVO)channelVO).Id), channelVO);
                }
                else if (channelVO.GetType().Equals(typeof(Cinema.ChannelCinemaVO)))
                {
                    //// XmlParserSerializableVO.ObjChannelCinemaVO = (Cinema.ChannelCinemaVO)channelVO;
                    await XmlParserSerializableVO.SaveAsync<Cinema.ChannelCinemaVO>(Util.NameFile(((Cinema.ChannelCinemaVO)channelVO).NomeChannel + ((Cinema.ChannelCinemaVO)channelVO).Id, 0), channelVO);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método para carregar o xml no VO
        /// </summary>
        /// <param name="channel"> Obj de channel do VO</param>
        private static async Task LoadXmChannelNoticialUrl(ChannelVO channel)
        {
            try
            {
                XmlDocument pvtXmlDocument = await XDocToXmlDoc(channel.Url);

                IXmlNode rssNode = pvtXmlDocument.SelectSingleNode("rss");
                IXmlNode channelNode = rssNode.SelectSingleNode("channel");

                channel.Title = channelNode.SelectSingleNode("title").InnerText;
                channel.Description = channelNode.SelectSingleNode("description").InnerText;
                channel.Link = channelNode.SelectSingleNode("link").InnerText;

                ImageVO imagemVO = new ImageVO();
                imagemVO.Url = channelNode.SelectSingleNode("image").SelectSingleNode("url").InnerText;
                imagemVO.Link = channelNode.SelectSingleNode("image").SelectSingleNode("link").InnerText;
                imagemVO.Title = channelNode.SelectSingleNode("image").SelectSingleNode("title").InnerText;
                imagemVO.Description = channelNode.SelectSingleNode("image").SelectSingleNode("description").InnerText;

                channel.Image = imagemVO;

                channel.LastBuildDate = channelNode.SelectSingleNode("lastBuildDate").InnerText;
                channel.PubDate = channelNode.SelectSingleNode("pubDate").InnerText;

                XmlNodeList itemNodeList = channelNode.SelectNodes("item");

                int qtd = 0;
                foreach (IXmlNode node in itemNodeList)
                {
                    qtd++;

                    //if (qtd.Equals(11))
                    //{
                    //    break;
                    //}

                    ItemVO itemVO = new ItemVO();

                    itemVO.Title = node.SelectSingleNode("title").InnerText;
                    itemVO.Description = node.SelectSingleNode("description").InnerText;
                    itemVO.Content = node.SelectSingleNode("content").InnerText;
                    itemVO.ContentOld = node.SelectSingleNode("content_old").InnerText;
                    itemVO.Link = node.SelectSingleNode("link").InnerText;
                    itemVO.Guid = Convert.ToInt32(node.SelectSingleNode("guid").InnerText);
                    itemVO.PubDate = node.SelectSingleNode("pubDate").InnerText;

                    if (node.SelectSingleNode("video") != null)
                    {
                        itemVO.Video = node.SelectSingleNode("video").InnerText;
                    }
                    else
                    {
                        itemVO.Video = string.Empty;
                    }

                    XmlNodeList categoriaNodeList = node.SelectNodes("category");

                    foreach (IXmlNode categoriaNode in categoriaNodeList)
                    {
                        CategoriaVO categoriaVO = new CategoriaVO();

                        categoriaVO.Guid = Convert.ToInt32(categoriaNode.SelectSingleNode("guid").InnerText);
                        categoriaVO.Title = categoriaNode.SelectSingleNode("title").InnerText;

                        itemVO.ListaCategoria.Add(categoriaVO);
                    }

                    XmlNodeList imageNodeList = node.SelectNodes("image");

                    foreach (IXmlNode imageNode in imageNodeList)
                    {
                        ImageVO imageVO = new ImageVO();

                        imageVO.Url = imageNode.SelectSingleNode("url").InnerText;
                        imageVO.Link = imageNode.SelectSingleNode("link").InnerText;
                        imageVO.Title = imageNode.SelectSingleNode("title").InnerText;
                        imageVO.Description = imageNode.SelectSingleNode("description").InnerText;

                        itemVO.ListaImage.Add(imageVO);
                    }

                    channel.ListaItem.Add(itemVO);
                }

                SaveXmlLocalAsync(channel);
            }
            catch (TaskCanceledException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método para carregar o xml da Programação no VO
        /// </summary>
        /// <param name="channel"> Obj de channel Programação do VO</param>        
        private static async Task LoadXmChannelProgramacaoUrl(ChannelProgramacaoVO channel)
        {
            try
            {


                XmlDocument pvtXmlDocument = await XDocToXmlDoc(channel.Url);

                IXmlNode rssNode = pvtXmlDocument.SelectSingleNode("rss");
                IXmlNode channelNode = rssNode.SelectSingleNode("channel");

                channel.Title = channelNode.SelectSingleNode("title").InnerText;
                channel.Description = channelNode.SelectSingleNode("description").InnerText;
                channel.Link = channelNode.SelectSingleNode("link").InnerText;

                ImageChannelProgramacaoVO imagemVO = new ImageChannelProgramacaoVO();
                //imagemVO.Url = channelNode.SelectSingleNode("image").SelectSingleNode("url").InnerText;
                imagemVO.Link = channelNode.SelectSingleNode("image").SelectSingleNode("link").InnerText;
                imagemVO.Title = channelNode.SelectSingleNode("image").SelectSingleNode("title").InnerText;
                //imagemVO.Description = channelNode.SelectSingleNode("image").SelectSingleNode("description").InnerText;

                channel.Image = imagemVO;

                channel.LastBuildDate = channelNode.SelectSingleNode("lastBuildDate").InnerText;
                channel.PubDate = channelNode.SelectSingleNode("pubDate").InnerText;

                XmlNodeList itemNodeList = channelNode.SelectNodes("item");

                int qtd = 0;
                foreach (IXmlNode node in itemNodeList)
                {
                    qtd++;

                    //if (qtd.Equals(11))
                    //{
                    //    break;
                    //}

                    ItemProgramacaoVO itemVO = new ItemProgramacaoVO();

                    itemVO.Title = node.SelectSingleNode("title").InnerText;
                    itemVO.Description = node.SelectSingleNode("description").InnerText;
                    //itemVO.Content = node.SelectSingleNode("content").InnerText;
                    //itemVO.Link = node.SelectSingleNode("link").InnerText;
                    itemVO.Guid = node.SelectSingleNode("guid").InnerText;
                    //itemVO.DateRaffle = node.SelectSingleNode("pubDate").InnerText;

                    //if (node.SelectSingleNode("video") != null)
                    //{
                    //    itemVO.Video = node.SelectSingleNode("video").InnerText;
                    //}
                    //else
                    //{
                    //    itemVO.Video = string.Empty;
                    //}

                    XmlNodeList categoriaNodeList = node.SelectNodes("category");

                    foreach (IXmlNode categoriaNode in categoriaNodeList)
                    {
                        CategoriaProgramacaoVO categoriaVO = new CategoriaProgramacaoVO();

                        categoriaVO.Guid = Convert.ToInt32(categoriaNode.SelectSingleNode("guid").InnerText);
                        categoriaVO.Title = categoriaNode.SelectSingleNode("title").InnerText;

                        itemVO.ListaCategoria.Add(categoriaVO);
                    }

                    XmlNodeList imageNodeList = node.SelectNodes("image");

                    foreach (IXmlNode imageNode in imageNodeList)
                    {
                        ImageChannelProgramacaoVO imageVO = new ImageChannelProgramacaoVO();

                        //imageVO.Url = imageNode.SelectSingleNode("url").InnerText;
                        //imageVO.Link = imageNode.SelectSingleNode("link").InnerText;
                        //imageVO.Title = imageNode.SelectSingleNode("title").InnerText;
                        //imageVO.Description = imageNode.SelectSingleNode("description").InnerText;

                        itemVO.ListaImage.Add(imageVO);
                    }


                    XmlNodeList agendaNodeList = node.SelectNodes("horario");

                    foreach (IXmlNode agendaNode in agendaNodeList)
                    {
                        AgendaChannelProgramacaoVO agendaChannelProgramacaoVO = new AgendaChannelProgramacaoVO();

                        agendaChannelProgramacaoVO.Guid = agendaNode.SelectSingleNode("guid").InnerText;
                        agendaChannelProgramacaoVO.Dia = agendaNode.SelectSingleNode("diaDaSemana").InnerText;
                        agendaChannelProgramacaoVO.HorarioInicio = agendaNode.SelectSingleNode("horarioInicio").InnerText;
                        agendaChannelProgramacaoVO.HorarioFinal = agendaNode.SelectSingleNode("horarioFinal").InnerText;


                        itemVO.ListaAgenda.Add(agendaChannelProgramacaoVO);
                    }

                    channel.ListaItem.Add(itemVO);
                    DataSourceRadio.ListaItemProgramacao.Add(itemVO);
                }

                SaveXmlLocalAsync(channel);
            }
            catch (TaskCanceledException ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Método para carregar o xml no VO
        /// </summary>
        /// <param name="channel"> Obj de channel do VO</param>
        private static async Task LoadXmChannelPodCastUrl(ChannelPodCastVO channel)
        {
            try
            {
                XmlDocument pvtXmlDocument = await XDocToXmlDoc(channel.Url);

                IXmlNode rssNode = pvtXmlDocument.SelectSingleNode("rss");
                IXmlNode channelNode = rssNode.SelectSingleNode("channel");

                channel.Title = channelNode.SelectSingleNode("title").InnerText;
                channel.Description = channelNode.SelectSingleNode("description").InnerText;
                channel.Link = channelNode.SelectSingleNode("link").InnerText;
                // ImagePodCastVO imagemVO = new ImagePodCastVO();
                //imagemVO.Url = channelNode.SelectSingleNode("image").SelectSingleNode("url").InnerText;
                //imagemVO.Link = channelNode.SelectSingleNode("image").SelectSingleNode("link").InnerText;
                //imagemVO.Title = channelNode.SelectSingleNode("image").SelectSingleNode("title").InnerText;
                //imagemVO.Description = channelNode.SelectSingleNode("image").SelectSingleNode("description").InnerText;

                //channel.Image = imagemVO;

                channel.LastBuildDate = channelNode.SelectSingleNode("lastBuildDate").InnerText;
                channel.PubDate = channelNode.SelectSingleNode("pubDate").InnerText;

                XmlNodeList itemNodeList = channelNode.SelectNodes("item");

                int qtd = 0;
                foreach (IXmlNode node in itemNodeList)
                {
                    qtd++;

                    //if (qtd.Equals(11))
                    //{
                    //    break;
                    //}

                    ItemPodCastVO itemVO = new ItemPodCastVO();

                    itemVO.Title = node.SelectSingleNode("title").InnerText;
                    itemVO.Description = node.SelectSingleNode("description").InnerText;
                    itemVO.Content = node.SelectSingleNode("content").InnerText;
                    //itemVO.ContentOld = node.SelectSingleNode("content_old").InnerText;
                    itemVO.Link = node.SelectSingleNode("link").InnerText;
                    itemVO.Guid = node.SelectSingleNode("guid").InnerText;
                    itemVO.Podcast = node.SelectSingleNode("podcast/link").InnerText;
                    //Match m = Regex.Match(input, "&quot;uploads\\/[a-zA-Z0-9 ._]*&quot;");
                    itemVO.PubDate = node.SelectSingleNode("pubDate").InnerText;

                    if (node.SelectSingleNode("video") != null)
                    {
                        itemVO.Video = node.SelectSingleNode("video").InnerText;
                    }
                    else
                    {
                        itemVO.Video = string.Empty;
                    }

                    //XmlNodeList categoriaNodeList = node.SelectNodes("category");

                    //foreach (IXmlNode categoriaNode in categoriaNodeList)
                    //{
                    //    CategoriaPodCastVO categoriaVO = new CategoriaPodCastVO();

                    //    categoriaVO.Guid = Convert.ToInt32(categoriaNode.SelectSingleNode("guid").InnerText);
                    //    categoriaVO.Title = categoriaNode.SelectSingleNode("title").InnerText;

                    //    itemVO.ListaCategoria.Add(categoriaVO);
                    //}

                    XmlNodeList imageNodeList = node.SelectNodes("image");

                    if (node.SelectSingleNode("content") != null)
                    {
                        itemVO.Description = node.SelectSingleNode("content").InnerText;
                        // ... Input string.
                        string input = node.SelectSingleNode("content").InnerText;
                        ObservableCollection<ImagePodCastVO> lstImageCorreio = new ObservableCollection<ImagePodCastVO>();
                        ImagePodCastVO objImagePodCastVO = new ImagePodCastVO();
                        // ... One or more digits.
                   

                          lstImageCorreio.Add(objImagePodCastVO);


                        itemVO.ListaImage = lstImageCorreio;

                    }
               

                    foreach (IXmlNode imageNode in imageNodeList)
                    {
                        ImagePodCastVO imageVO = new ImagePodCastVO();

                        imageVO.Url = imageNode.SelectSingleNode("url").InnerText;
                        imageVO.Link = imageNode.SelectSingleNode("link").InnerText;
                        imageVO.Title = imageNode.SelectSingleNode("title").InnerText;
                        imageVO.Description = imageNode.SelectSingleNode("description").InnerText;

                        itemVO.ListaImage.Add(imageVO);
                    }

                    channel.ListaItem.Add(itemVO);
                    DataSourceRadio.ListaItemPodCastVO.Add(itemVO);
                }

               

                SaveXmlLocalAsync(channel);
            }
            catch (TaskCanceledException ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Método para carregar o xml no VO
        /// </summary>
        /// <param name="channel"> Obj de channel do VO</param>
        private static async Task LoadXmChannelPromocaoBahiaSulUrl(ChannelPromocaoVO channel)
        {
            try
            {
                XmlDocument pvtXmlDocument = await XDocToXmlDoc(Constantes.strURLBahiaFMSul);

                IXmlNode rssNode = pvtXmlDocument.SelectSingleNode("rss");
                IXmlNode channelNode = rssNode.SelectSingleNode("channel");

                channel.Title = channelNode.SelectSingleNode("title").InnerText;
                channel.Description = channelNode.SelectSingleNode("description").InnerText;
                channel.Link = channelNode.SelectSingleNode("link").InnerText;
                ImageChannelPromocaoVO imagemVO = new ImageChannelPromocaoVO();
                imagemVO.Url = channelNode.SelectSingleNode("image").SelectSingleNode("url").InnerText;
                imagemVO.Link = channelNode.SelectSingleNode("image").SelectSingleNode("link").InnerText;
                imagemVO.Title = channelNode.SelectSingleNode("image").SelectSingleNode("title").InnerText;
                imagemVO.Description = channelNode.SelectSingleNode("image").SelectSingleNode("description").InnerText;

                channel.Image = imagemVO;

              
                XmlNodeList itemNodeList = channelNode.SelectNodes("item");

                int qtd = 0;
                foreach (IXmlNode node in itemNodeList)
                {
                    qtd++;

                    //if (qtd.Equals(11))
                    //{
                    //    break;
                    //}

                    ItemPromocaoVO itemVO = new ItemPromocaoVO();

                    itemVO.Title = node.SelectSingleNode("title").InnerText;
                    //itemVO.Description = node.SelectSingleNode("description").InnerText;
                    itemVO.Content = node.SelectSingleNode("content").InnerText;
                    //itemVO.ContentOld = node.SelectSingleNode("content_old").InnerText;
                    itemVO.Link = node.SelectSingleNode("link").InnerText;
                    //itemVO.Guid = node.SelectSingleNode("guid").InnerText;
                    itemVO.Guid = node.SelectSingleNode("guid").InnerText;
                    //Match m = Regex.Match(input, "&quot;uploads\\/[a-zA-Z0-9 ._]*&quot;");
                    itemVO.DateRaffle = node.SelectSingleNode("dateRaffle").InnerText;
                    itemVO.DateWithdrawal = node.SelectSingleNode("dateWithdrawal").InnerText;
                    itemVO.Question = node.SelectSingleNode("question").InnerText;
                    itemVO.Terms = node.SelectSingleNode("terms").InnerText;

                    
                    XmlNodeList categoriaNodeList = node.SelectNodes("category");

                    foreach (IXmlNode categoriaNode in categoriaNodeList)
                    {
                        CategoriaPromocaoVO categoriaVO = new CategoriaPromocaoVO();

                        categoriaVO.Guid = Convert.ToInt32(categoriaNode.SelectSingleNode("guid").InnerText);
                        categoriaVO.Title = categoriaNode.SelectSingleNode("title").InnerText;

                        itemVO.ListaCategoria.Add(categoriaVO);
                    }

                    XmlNodeList imageNodeList = node.SelectNodes("image");

                    if (node.SelectSingleNode("content") != null)
                    {
                        itemVO.Description = node.SelectSingleNode("content").InnerText;
                        // ... Input string.
                        string input = node.SelectSingleNode("content").InnerText;
                        ObservableCollection<ImageChannelPromocaoVO> lstImageChannelPromocaoVO = new ObservableCollection<ImageChannelPromocaoVO>();
                        ImageChannelPromocaoVO objImageChannelPromocaoVO = new ImageChannelPromocaoVO();
                        // ... One or more digits.


                        lstImageChannelPromocaoVO.Add(objImageChannelPromocaoVO);


                        itemVO.ListaImage = lstImageChannelPromocaoVO;

                    }


                    foreach (IXmlNode imageNode in imageNodeList)
                    {
                        ImageChannelPromocaoVO imageVO = new ImageChannelPromocaoVO();

                         imageVO.Url = imageNode.SelectSingleNode("url").InnerText;
                        //imageVO.Link = imageNode.SelectSingleNode("link").InnerText;
                        //imageVO.Title = imageNode.SelectSingleNode("title").InnerText;
                        //imageVO.Description = imageNode.SelectSingleNode("description").InnerText;

                        itemVO.ListaImage.Add(imageVO);
                    }

                    channel.ListaItem.Add(itemVO);
                    DataSourceRadio.ListaItemPromocaoBahiaSulVO.Add(itemVO);
                }



                SaveXmlLocalAsync(channel);
            }
            catch (TaskCanceledException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método para carregar o xml no VO
        /// </summary>
        /// <param name="channel"> Obj de channel do VO</param>
        private static async Task LoadXmChannelCBNFMUrl(ChannelPromocaoVO channel)
        {
            try
            {
                XmlDocument pvtXmlDocument = await XDocToXmlDoc(Constantes.strURLGloboFM);

                IXmlNode rssNode = pvtXmlDocument.SelectSingleNode("rss");
                IXmlNode channelNode = rssNode.SelectSingleNode("channel");

                channel.Title = channelNode.SelectSingleNode("title").InnerText;
                channel.Description = channelNode.SelectSingleNode("description").InnerText;
                channel.Link = channelNode.SelectSingleNode("link").InnerText;
                ImageChannelPromocaoVO imagemVO = new ImageChannelPromocaoVO();
                imagemVO.Url = channelNode.SelectSingleNode("image").SelectSingleNode("url").InnerText;
                imagemVO.Link = channelNode.SelectSingleNode("image").SelectSingleNode("link").InnerText;
                imagemVO.Title = channelNode.SelectSingleNode("image").SelectSingleNode("title").InnerText;
                imagemVO.Description = channelNode.SelectSingleNode("image").SelectSingleNode("description").InnerText;

                channel.Image = imagemVO;


                XmlNodeList itemNodeList = channelNode.SelectNodes("item");

                int qtd = 0;
                foreach (IXmlNode node in itemNodeList)
                {
                    qtd++;

                    if (qtd.Equals(11))
                    {
                        break;
                    }

                    ItemPromocaoVO itemVO = new ItemPromocaoVO();

                    itemVO.Title = node.SelectSingleNode("title").InnerText;
                    //itemVO.Description = node.SelectSingleNode("description").InnerText;
                    itemVO.Content = node.SelectSingleNode("content").InnerText;
                    //itemVO.ContentOld = node.SelectSingleNode("content_old").InnerText;
                    itemVO.Link = node.SelectSingleNode("link").InnerText;
                    //itemVO.Guid = node.SelectSingleNode("guid").InnerText;
                    itemVO.Guid = node.SelectSingleNode("guid").InnerText;
                    //Match m = Regex.Match(input, "&quot;uploads\\/[a-zA-Z0-9 ._]*&quot;");
                    itemVO.DateRaffle = node.SelectSingleNode("dateRaffle").InnerText;
                    itemVO.DateWithdrawal = node.SelectSingleNode("dateWithdrawal").InnerText;
                    itemVO.Question = node.SelectSingleNode("question").InnerText;
                    itemVO.Terms = node.SelectSingleNode("terms").InnerText;


                    XmlNodeList categoriaNodeList = node.SelectNodes("category");

                    foreach (IXmlNode categoriaNode in categoriaNodeList)
                    {
                        CategoriaPromocaoVO categoriaVO = new CategoriaPromocaoVO();

                        categoriaVO.Guid = Convert.ToInt32(categoriaNode.SelectSingleNode("guid").InnerText);
                        categoriaVO.Title = categoriaNode.SelectSingleNode("title").InnerText;

                        itemVO.ListaCategoria.Add(categoriaVO);
                    }

                    XmlNodeList imageNodeList = node.SelectNodes("image");

                    if (node.SelectSingleNode("content") != null)
                    {
                        itemVO.Description = node.SelectSingleNode("content").InnerText;
                        // ... Input string.
                        string input = node.SelectSingleNode("content").InnerText;
                        ObservableCollection<ImageChannelPromocaoVO> lstImageChannelPromocaoVO = new ObservableCollection<ImageChannelPromocaoVO>();
                        ImageChannelPromocaoVO objImageChannelPromocaoVO = new ImageChannelPromocaoVO();
                        // ... One or more digits.


                        lstImageChannelPromocaoVO.Add(objImageChannelPromocaoVO);


                        itemVO.ListaImage = lstImageChannelPromocaoVO;

                    }


                    foreach (IXmlNode imageNode in imageNodeList)
                    {
                        ImageChannelPromocaoVO imageVO = new ImageChannelPromocaoVO();

                        imageVO.Url = imageNode.SelectSingleNode("url").InnerText;
                        //imageVO.Link = imageNode.SelectSingleNode("link").InnerText;
                        //imageVO.Title = imageNode.SelectSingleNode("title").InnerText;
                        //imageVO.Description = imageNode.SelectSingleNode("description").InnerText;

                        itemVO.ListaImage.Add(imageVO);
                    }

                    channel.ListaItem.Add(itemVO);
                    DataSourceRadio.ListaItemPromocaoGloboFMVO.Add(itemVO);
                }
                SaveXmlLocalAsync(channel);
            }
            catch (TaskCanceledException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método para carregar o xml no VO
        /// </summary>
        /// <param name="channel"> Obj de channel do VO</param>
        private static async Task LoadXmChannelPromocaoGloboFMUrl(ChannelPromocaoVO channel)
        {
            try
            {
                XmlDocument pvtXmlDocument = await XDocToXmlDoc(Constantes.strURLGloboFM);

                IXmlNode rssNode = pvtXmlDocument.SelectSingleNode("rss");
                IXmlNode channelNode = rssNode.SelectSingleNode("channel");

                channel.Title = channelNode.SelectSingleNode("title").InnerText;
                channel.Description = channelNode.SelectSingleNode("description").InnerText;
                channel.Link = channelNode.SelectSingleNode("link").InnerText;
                ImageChannelPromocaoVO imagemVO = new ImageChannelPromocaoVO();
                imagemVO.Url = channelNode.SelectSingleNode("image").SelectSingleNode("url").InnerText;
                imagemVO.Link = channelNode.SelectSingleNode("image").SelectSingleNode("link").InnerText;
                imagemVO.Title = channelNode.SelectSingleNode("image").SelectSingleNode("title").InnerText;
                imagemVO.Description = channelNode.SelectSingleNode("image").SelectSingleNode("description").InnerText;

                channel.Image = imagemVO;


                XmlNodeList itemNodeList = channelNode.SelectNodes("item");

                int qtd = 0;
                foreach (IXmlNode node in itemNodeList)
                {
                    qtd++;

                    if (qtd.Equals(11))
                    {
                        break;
                    }

                    ItemPromocaoVO itemVO = new ItemPromocaoVO();

                    itemVO.Title = node.SelectSingleNode("title").InnerText;
                    //itemVO.Description = node.SelectSingleNode("description").InnerText;
                    itemVO.Content = node.SelectSingleNode("content").InnerText;
                    //itemVO.ContentOld = node.SelectSingleNode("content_old").InnerText;
                    itemVO.Link = node.SelectSingleNode("link").InnerText;
                    //itemVO.Guid = node.SelectSingleNode("guid").InnerText;
                    itemVO.Guid = node.SelectSingleNode("guid").InnerText;
                    //Match m = Regex.Match(input, "&quot;uploads\\/[a-zA-Z0-9 ._]*&quot;");
                    itemVO.DateRaffle = node.SelectSingleNode("dateRaffle").InnerText;
                    itemVO.DateWithdrawal = node.SelectSingleNode("dateWithdrawal").InnerText;
                    itemVO.Question = node.SelectSingleNode("question").InnerText;
                    itemVO.Terms = node.SelectSingleNode("terms").InnerText;


                    XmlNodeList categoriaNodeList = node.SelectNodes("category");

                    foreach (IXmlNode categoriaNode in categoriaNodeList)
                    {
                        CategoriaPromocaoVO categoriaVO = new CategoriaPromocaoVO();

                        categoriaVO.Guid = Convert.ToInt32(categoriaNode.SelectSingleNode("guid").InnerText);
                        categoriaVO.Title = categoriaNode.SelectSingleNode("title").InnerText;

                        itemVO.ListaCategoria.Add(categoriaVO);
                    }

                    XmlNodeList imageNodeList = node.SelectNodes("image");

                    if (node.SelectSingleNode("content") != null)
                    {
                        itemVO.Description = node.SelectSingleNode("content").InnerText;
                        // ... Input string.
                        string input = node.SelectSingleNode("content").InnerText;
                        ObservableCollection<ImageChannelPromocaoVO> lstImageChannelPromocaoVO = new ObservableCollection<ImageChannelPromocaoVO>();
                        ImageChannelPromocaoVO objImageChannelPromocaoVO = new ImageChannelPromocaoVO();
                        // ... One or more digits.


                        lstImageChannelPromocaoVO.Add(objImageChannelPromocaoVO);


                        itemVO.ListaImage = lstImageChannelPromocaoVO;

                    }


                    foreach (IXmlNode imageNode in imageNodeList)
                    {
                        ImageChannelPromocaoVO imageVO = new ImageChannelPromocaoVO();

                        imageVO.Url = imageNode.SelectSingleNode("url").InnerText;
                        //imageVO.Link = imageNode.SelectSingleNode("link").InnerText;
                        //imageVO.Title = imageNode.SelectSingleNode("title").InnerText;
                        //imageVO.Description = imageNode.SelectSingleNode("description").InnerText;

                        itemVO.ListaImage.Add(imageVO);
                    }

                    channel.ListaItem.Add(itemVO);
                    DataSourceRadio.ListaItemPromocaoGloboFMVO.Add(itemVO);
                }
                SaveXmlLocalAsync(channel);
            }
            catch (TaskCanceledException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método para carregar o xml no VO
        /// </summary>
        /// <param name="channel"> Obj de channel do VO</param>
        private static async Task LoadXmChannelPromocaoUrl(ChannelPromocaoVO channel)
        {
            try
            {
                XmlDocument pvtXmlDocument = await XDocToXmlDoc(channel.Url);

                IXmlNode rssNode = pvtXmlDocument.SelectSingleNode("rss");
                IXmlNode channelNode = rssNode.SelectSingleNode("channel");

                channel.Title = channelNode.SelectSingleNode("title").InnerText;
                channel.Description = channelNode.SelectSingleNode("description").InnerText;
                channel.Link = channelNode.SelectSingleNode("link").InnerText;
                ImageChannelPromocaoVO imagemVO = new ImageChannelPromocaoVO();
                imagemVO.Url = channelNode.SelectSingleNode("image").SelectSingleNode("url").InnerText;
                imagemVO.Link = channelNode.SelectSingleNode("image").SelectSingleNode("link").InnerText;
                imagemVO.Title = channelNode.SelectSingleNode("image").SelectSingleNode("title").InnerText;
                imagemVO.Description = channelNode.SelectSingleNode("image").SelectSingleNode("description").InnerText;

                channel.Image = imagemVO;


                XmlNodeList itemNodeList = channelNode.SelectNodes("item");

                int qtd = 0;
                foreach (IXmlNode node in itemNodeList)
                {
                    qtd++;

                    if (qtd.Equals(11))
                    {
                        break;
                    }

                    ItemPromocaoVO itemVO = new ItemPromocaoVO();

                    itemVO.Title = node.SelectSingleNode("title").InnerText;
                    //itemVO.Description = node.SelectSingleNode("description").InnerText;
                    itemVO.Content = node.SelectSingleNode("content").InnerText;
                    //itemVO.ContentOld = node.SelectSingleNode("content_old").InnerText;
                    itemVO.Link = node.SelectSingleNode("link").InnerText;
                    //itemVO.Guid = node.SelectSingleNode("guid").InnerText;
                    itemVO.Guid = node.SelectSingleNode("guid").InnerText;
                    //Match m = Regex.Match(input, "&quot;uploads\\/[a-zA-Z0-9 ._]*&quot;");
                    itemVO.DateRaffle = node.SelectSingleNode("dateRaffle").InnerText;
                    itemVO.DateWithdrawal = node.SelectSingleNode("dateWithdrawal").InnerText;
                    itemVO.Question = node.SelectSingleNode("question").InnerText;
                    itemVO.Terms = node.SelectSingleNode("terms").InnerText;


                    XmlNodeList categoriaNodeList = node.SelectNodes("category");

                    foreach (IXmlNode categoriaNode in categoriaNodeList)
                    {
                        CategoriaPromocaoVO categoriaVO = new CategoriaPromocaoVO();

                        categoriaVO.Guid = Convert.ToInt32(categoriaNode.SelectSingleNode("guid").InnerText);
                        categoriaVO.Title = categoriaNode.SelectSingleNode("title").InnerText;

                        itemVO.ListaCategoria.Add(categoriaVO);
                    }

                    XmlNodeList imageNodeList = node.SelectNodes("image");

                    if (node.SelectSingleNode("content") != null)
                    {
                        itemVO.Description = node.SelectSingleNode("content").InnerText;
                        // ... Input string.
                        string input = node.SelectSingleNode("content").InnerText;
                        ObservableCollection<ImageChannelPromocaoVO> lstImageChannelPromocaoVO = new ObservableCollection<ImageChannelPromocaoVO>();
                        ImageChannelPromocaoVO objImageChannelPromocaoVO = new ImageChannelPromocaoVO();
                        // ... One or more digits.


                        lstImageChannelPromocaoVO.Add(objImageChannelPromocaoVO);


                        itemVO.ListaImage = lstImageChannelPromocaoVO;

                    }


                    foreach (IXmlNode imageNode in imageNodeList)
                    {
                        ImageChannelPromocaoVO imageVO = new ImageChannelPromocaoVO();

                        imageVO.Url = imageNode.SelectSingleNode("url").InnerText;
                        //imageVO.Link = imageNode.SelectSingleNode("link").InnerText;
                        //imageVO.Title = imageNode.SelectSingleNode("title").InnerText;
                        //imageVO.Description = imageNode.SelectSingleNode("description").InnerText;

                        itemVO.ListaImage.Add(imageVO);
                    }

                    channel.ListaItem.Add(itemVO);
                    DataSourceRadio.ListaItemPromocaoVO.Add(itemVO);
                }



                SaveXmlLocalAsync(channel);
            }
            catch (TaskCanceledException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método para carregar o xml da agenda cultural no VO
        /// </summary>
        /// <param name="channel"> Obj de channel agenda cultural do VO</param>        
        private static async Task LoadXmChannelCinemalUrl(Cinema.ChannelCinemaVO channel)
        {
            try
            {
                XmlDocument pvtXmlDocument = await XDocToXmlDoc(channel.Url);

                IXmlNode rssNode = pvtXmlDocument.SelectSingleNode("rss");
                IXmlNode channelNode = rssNode.SelectSingleNode("channel");

                channel.Title = channelNode.SelectSingleNode("title").InnerText;
                channel.Description = channelNode.SelectSingleNode("description").InnerText;
                channel.Link = channelNode.SelectSingleNode("link").InnerText;
                channel.LastBuildDate = channelNode.SelectSingleNode("lastBuildDate").InnerText;
                channel.PubDate = channelNode.SelectSingleNode("pubDate").InnerText;

                XmlNodeList itemNodeList = channelNode.SelectNodes("item");
                Cinema.ItemCinemaVO itemVO;

                 foreach (IXmlNode node in itemNodeList)
                {
                    itemVO = new Cinema.ItemCinemaVO();

                    if (node.SelectSingleNode("guid") != null)
                    {
                        itemVO.Guid = Convert.ToInt32(node.SelectSingleNode("guid").InnerText);

                        if (node.SelectSingleNode("title") != null)
                        {
                            itemVO.Title = node.SelectSingleNode("title").InnerText;
                        }
                        else
                        {
                            itemVO.Title = string.Empty;
                        }

                        if (node.SelectSingleNode("link") != null)
                        {
                            itemVO.Link = node.SelectSingleNode("link").InnerText;
                        }
                        else
                        {
                            itemVO.Link = string.Empty;
                        }

                        if (node.SelectSingleNode("linksingle") != null)
                        {
                            itemVO.LinkLinksingle = node.SelectSingleNode("linksingle").InnerText;
                        }
                        else
                        {
                            itemVO.LinkLinksingle = string.Empty;
                        }

                        //itemVO.Linksingle = await LoadXmLinkSingleCinemaUrl(node.SelectSingleNode("linksingle").InnerText);
                        //itemVO.Linksingle = new Cinema.LinkSingleItemVO();

                        if (node.SelectSingleNode("thumb") != null)
                        {
                            itemVO.Thumb = node.SelectSingleNode("thumb").InnerText;
                        }
                        else
                        {
                            itemVO.Thumb = string.Empty;
                        }

                        channel.ListaItem.Add(itemVO);
                    }
                }

                SaveXmlLocalAsync(channel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Método para carregar o xml da Correio no VO
        /// </summary>
        /// <param name="channel"> Obj de channel Correio do VO</param>        
        private static async Task LoadXmChannelCorreioUrl(ChannelCorreioVO channel)
        {
            try
            {
                XmlDocument pvtXmlDocument = await XDocToXmlDoc(channel.Url);

                IXmlNode rssNode = pvtXmlDocument.SelectSingleNode("rss");
                IXmlNode channelNode = rssNode.SelectSingleNode("channel");

                channel.Title = channelNode.SelectSingleNode("title").InnerText;
                channel.Description = channelNode.SelectSingleNode("description").InnerText;
                channel.Link = channelNode.SelectSingleNode("link").InnerText;
                channel.LastBuildDate = channelNode.SelectSingleNode("lastBuildDate").InnerText;
                channel.PubDate = channelNode.SelectSingleNode("pubDate").InnerText;

                XmlNodeList itemNodeList = channelNode.SelectNodes("item");
                ItemCorreioVO itemVO;

                int qtd = 0;
                foreach (IXmlNode node in itemNodeList)
                {
                    itemVO = new ItemCorreioVO();

                    if (node.SelectSingleNode("guid") != null)
                    {
                        itemVO.Guid = Convert.ToInt32(node.SelectSingleNode("guid").InnerText);

                        if (node.SelectSingleNode("title") != null)
                        {
                            itemVO.Title = node.SelectSingleNode("title").InnerText;
                        }
                        else
                        {
                            itemVO.Title = string.Empty;
                        }

                        if (node.SelectSingleNode("link") != null)
                        {
                            itemVO.Link = node.SelectSingleNode("link").InnerText;
                        }
                        else
                        {
                            itemVO.Link = string.Empty;
                        }



                        if (node.SelectSingleNode("content") != null)
                        {
                            itemVO.Description = node.SelectSingleNode("content").InnerText;
                            // ... Input string.
                            string input = node.SelectSingleNode("content").InnerText;
                            ObservableCollection<ImageCorreioVO> lstImageCorreio = new ObservableCollection<ImageCorreioVO>();
                            ImageCorreioVO objImageCorreioVO = new ImageCorreioVO();
                            // ... One or more digits.
                            Match m = Regex.Match(input, "&quot;uploads\\/[a-zA-Z0-9 ._]*&quot;");

                            objImageCorreioVO.Url = "http://www.correio24horas.com.br/" + m.Value.Replace("&quot;", "");
                            lstImageCorreio.Add(objImageCorreioVO);


                            itemVO.ListaImage = lstImageCorreio;
                         
                        }
                        else
                        {
                            itemVO.Description = string.Empty;
                        }

                        //itemVO.Linksingle = await LoadXmLinkSingleCinemaUrl(node.SelectSingleNode("linksingle").InnerText);
                        //itemVO.Linksingle = new Cinema.LinkSingleItemVO();

                        //if (node.SelectSingleNode("thumb") != null)
                        //{
                        //    itemVO.Thumb = node.SelectSingleNode("thumb").InnerText;
                        //}
                        //else
                        //{
                        //    itemVO.Thumb = string.Empty;
                        //}

                        channel.ListaItem.Add(itemVO);
                        DataSourceRadio.ListaItemNoticiasCBNVO.Add(itemVO);
                    }
                }

                SaveXmlLocalAsync(channel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método para carregar o xml da ProgramacaoRadio no VO
        /// </summary>
        /// <param name="channel"> Obj de channel ProgramacaoRadio do VO</param>        
        private static async Task LoadXmChannelProgramacaoRadioXML(ChannelProgramacaoRadioXMLVO channel)
        {
            try
            {


                XmlDocument pvtXmlDocument = await XDocToXmlDoc(channel.Url);

                IXmlNode rssNode = pvtXmlDocument.SelectSingleNode("rss");
                IXmlNode channelNode = rssNode.SelectSingleNode("channel");

                channel.Title = channelNode.SelectSingleNode("title").InnerText;
                channel.Description = channelNode.SelectSingleNode("description").InnerText;
                channel.Link = channelNode.SelectSingleNode("link").InnerText;

                ImageChannelProgramacaoRadioXMLVO imagemVO = new ImageChannelProgramacaoRadioXMLVO();
                imagemVO.Url = channelNode.SelectSingleNode("image").InnerText;

                channel.Image = imagemVO;

                channel.LastBuildDate = channelNode.SelectSingleNode("lastBuildDate").InnerText;
                channel.PubDate = channelNode.SelectSingleNode("pubDate").InnerText;

                XmlNodeList itemNodeList = channelNode.SelectNodes("item");

                int qtd = 0;
                foreach (IXmlNode node in itemNodeList)
                {
                    qtd++;

                    //if (qtd.Equals(11))
                    //{
                    //    break;
                    //}

                    ItemProgramacaoRadioXMLVO itemVO = new ItemProgramacaoRadioXMLVO();

                    itemVO.Title = node.SelectSingleNode("title").InnerText;
                    itemVO.Description = node.SelectSingleNode("description").InnerText;
                    itemVO.Guid = node.SelectSingleNode("guid").InnerText;
 


                  
                        ImageChannelProgramacaoRadioXMLVO imageVO = new ImageChannelProgramacaoRadioXMLVO();

                        imageVO.Url = node.SelectSingleNode("image").InnerText;

                        itemVO.ListaImage.Add(imageVO);
                  
                    XmlNodeList horarioNodeList = node.SelectNodes("horario");

                    foreach (IXmlNode horarioNode in horarioNodeList)
                    {
                        HorarioProgramacaoRadioXMLVO horarioProgramacaoRadioXMLVO = new HorarioProgramacaoRadioXMLVO();

                        horarioProgramacaoRadioXMLVO.DiaDaSemana = horarioNode.SelectSingleNode("diaDaSemana").InnerText;
                        horarioProgramacaoRadioXMLVO.HorarioFinal = horarioNode.SelectSingleNode("horarioFinal").InnerText;
                        horarioProgramacaoRadioXMLVO.HorarioInicio = horarioNode.SelectSingleNode("horarioInicio").InnerText;

                        itemVO.ListaHorarioProgramacaoRadioXMLVO.Add(horarioProgramacaoRadioXMLVO);
                    }


                    XmlNodeList categoriaNodeList = node.SelectNodes("category");

                    foreach (IXmlNode categoriaNode in categoriaNodeList)
                    {
                        CategoriaProgramacaoRadioXMLVO categoriaVO = new CategoriaProgramacaoRadioXMLVO();

                        categoriaVO.Guid = Convert.ToInt32(categoriaNode.SelectSingleNode("guid").InnerText);
                        categoriaVO.Title = categoriaNode.SelectSingleNode("title").InnerText;

                        itemVO.ListaCategoria.Add(categoriaVO);
                    }


                    itemVO.ListaHorarioProgramacaoRadioXMLVO = itemVO.ListaHorarioProgramacaoRadioXMLVO.OrderBy(i => i.DiaDaSemana).ToObservableCollection();

                    channel.ListaItem.Add(itemVO);
                    DataSourceRadio.ListaItemProgramacaoRadio.Add(itemVO);

                }

                SaveXmlLocalAsync(channel);
            }
            catch (TaskCanceledException ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Método para carregar o xml da Correio no VO
        /// </summary>
        /// <param name="channel"> Obj de channel Correio do VO</param>        
        private static async Task LoadXmChannelSalvadorUrl(ChannelSalvadorVO channel)
        {
            try
            {
             

                XmlDocument pvtXmlDocument = await XDocToXmlDoc(channel.Url);

                IXmlNode rssNode = pvtXmlDocument.SelectSingleNode("rss");
                IXmlNode channelNode = rssNode.SelectSingleNode("channel");

                channel.Title = channelNode.SelectSingleNode("title").InnerText;
                channel.Description = channelNode.SelectSingleNode("description").InnerText;
                channel.Link = channelNode.SelectSingleNode("link").InnerText;

             
                if (channelNode.SelectSingleNode("image") != null)
                {
                    ImageChannelSalvadorVO imagemVO = new ImageChannelSalvadorVO();
                    imagemVO.Url = channelNode.SelectSingleNode("image").SelectSingleNode("url").InnerText;
                    imagemVO.Link = channelNode.SelectSingleNode("image").SelectSingleNode("link").InnerText;
                    imagemVO.Title = channelNode.SelectSingleNode("image").SelectSingleNode("title").InnerText;
                    imagemVO.Description = channelNode.SelectSingleNode("image").SelectSingleNode("description").InnerText;

                    channel.Image = imagemVO; 
                }

                channel.LastBuildDate = channelNode.SelectSingleNode("lastBuildDate").InnerText;
                channel.PubDate = channelNode.SelectSingleNode("pubDate").InnerText;

                XmlNodeList itemNodeList = channelNode.SelectNodes("item");

                int qtd = 0;
                foreach (IXmlNode node in itemNodeList)
                {
                    qtd++;

                    if (qtd.Equals(11))
                    {
                        break;
                    }

                    ItemSalvadorVO itemVO = new ItemSalvadorVO();

                    itemVO.Title = node.SelectSingleNode("title").InnerText;
                    itemVO.Description = node.SelectSingleNode("description").InnerText;
                    itemVO.Content = node.SelectSingleNode("content").InnerText;
                    itemVO.Link = node.SelectSingleNode("link").InnerText;
                    itemVO.Guid = node.SelectSingleNode("guid").InnerText;
                    itemVO.DateRaffle = node.SelectSingleNode("pubDate").InnerText;

                    //if (node.SelectSingleNode("video") != null)
                    //{
                    //    itemVO.Video = node.SelectSingleNode("video").InnerText;
                    //}
                    //else
                    //{
                    //    itemVO.Video = string.Empty;
                    //}

                    XmlNodeList categoriaNodeList = node.SelectNodes("category");

                    foreach (IXmlNode categoriaNode in categoriaNodeList)
                    {
                        CategoriaSalvadorVO categoriaVO = new CategoriaSalvadorVO();

                        categoriaVO.Guid = Convert.ToInt32(categoriaNode.SelectSingleNode("guid").InnerText);
                        categoriaVO.Title = categoriaNode.SelectSingleNode("title").InnerText;

                        itemVO.ListaCategoria.Add(categoriaVO);
                    }

                    XmlNodeList imageNodeList = node.SelectNodes("image");

                    foreach (IXmlNode imageNode in imageNodeList)
                    {
                        ImageChannelSalvadorVO imageVO = new ImageChannelSalvadorVO();

                        imageVO.Url = imageNode.SelectSingleNode("url").InnerText;
                        imageVO.Link = imageNode.SelectSingleNode("link").InnerText;
                        imageVO.Title = imageNode.SelectSingleNode("title").InnerText;
                        imageVO.Description = imageNode.SelectSingleNode("description").InnerText;

                        itemVO.ListaImage.Add(imageVO);
                    }

                    channel.ListaItem.Add(itemVO);
                    DataSourceRadio.ListaItemSalvador.Add(itemVO);
                }

                SaveXmlLocalAsync(channel);
            }
            catch (TaskCanceledException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método para Obter o Objeto LinkSingleCinema
        /// </summary>
        /// <param name="url">Url do link single</param>
        /// <returns>Retorna Objeto LinkSingleCinema</returns>
        public static async Task<Cinema.LinkSingleItemVO> LoadXmLinkSingleCinemaUrl(string url)
        {
            try
            {

                string strXML = XDocument.Load(url).ToString();
                XmlDocument pvtXmlDocument = new XmlDocument();

                pvtXmlDocument.LoadXml(strXML);

                IXmlNode rssNode = pvtXmlDocument.SelectSingleNode("rss");
                IXmlNode channelNode = rssNode.SelectSingleNode("channel");
                IXmlNode channelItemNode = channelNode.SelectSingleNode("item");

                Cinema.LinkSingleItemVO linkSingleItemVO = new Cinema.LinkSingleItemVO();
                linkSingleItemVO.Guid = Convert.ToInt32(channelItemNode.SelectSingleNode("guid").InnerText);
                linkSingleItemVO.Title = channelItemNode.SelectSingleNode("title").InnerText;
                linkSingleItemVO.Link = channelItemNode.SelectSingleNode("link").InnerText;
                linkSingleItemVO.Thumb = channelItemNode.SelectSingleNode("thumb").InnerText;

                if (channelItemNode.SelectSingleNode("image") != null)
                {
                    linkSingleItemVO.Image = channelItemNode.SelectSingleNode("image").InnerText;
                }
                else
                {
                    linkSingleItemVO.Image = string.Empty;
                }

                if (channelItemNode.SelectSingleNode("genero") != null)
                {
                    linkSingleItemVO.Genero = channelItemNode.SelectSingleNode("genero").InnerText;
                }
                else
                {
                    linkSingleItemVO.Genero = string.Empty;
                }

                if (channelItemNode.SelectSingleNode("faixa_etaria") != null)
                {
                    linkSingleItemVO.FaixaEtaria = channelItemNode.SelectSingleNode("faixa_etaria").InnerText;
                }
                else
                {
                    linkSingleItemVO.FaixaEtaria = string.Empty;
                }

                if (channelItemNode.SelectSingleNode("direcao") != null)
                {
                    linkSingleItemVO.Direcao = channelItemNode.SelectSingleNode("direcao").InnerText;
                }
                else
                {
                    linkSingleItemVO.Direcao = string.Empty;
                }

                if (channelItemNode.SelectSingleNode("ano_lancamento") != null)
                {
                    linkSingleItemVO.AnoLancamento = channelItemNode.SelectSingleNode("ano_lancamento").InnerText;
                }
                else
                {
                    linkSingleItemVO.AnoLancamento = string.Empty;
                }

                if (channelItemNode.SelectSingleNode("elenco") != null)
                {
                    linkSingleItemVO.Elenco = channelItemNode.SelectSingleNode("elenco").InnerText;
                }
                else
                {
                    linkSingleItemVO.Elenco = string.Empty;
                }

                if (channelItemNode.SelectSingleNode("sinopse") != null)
                {
                    linkSingleItemVO.Sinopse = channelItemNode.SelectSingleNode("sinopse").InnerText;
                }
                else
                {
                    linkSingleItemVO.Sinopse = string.Empty;
                }

                if (channelItemNode.SelectSingleNode("trailer") != null)
                {
                    linkSingleItemVO.Trailer = channelItemNode.SelectSingleNode("trailer").InnerText;
                }
                else
                {
                    linkSingleItemVO.Trailer = string.Empty;
                }

                XmlNodeList itemNodeList = channelItemNode.SelectSingleNode("sessoes").SelectNodes("sessao");

                foreach (IXmlNode node in itemNodeList)
                {
                    Cinema.SessaoVO sessaoVO = new Cinema.SessaoVO();
                    sessaoVO.Cinema = node.SelectSingleNode("cinema").InnerText;
                    sessaoVO.Sala = node.SelectSingleNode("sala").InnerText;

                    Cinema.HorarioVO horarioVO = new Cinema.HorarioVO();
                    IXmlNode itemNodeSessaoHorarios = node.SelectSingleNode("horarios");

                    XmlNodeList itemNodeTipoList = itemNodeSessaoHorarios.SelectNodes("tipo");

                    foreach (IXmlNode item in itemNodeTipoList)
                    {
                        horarioVO.Tipo.Add(item.InnerText);
                    }

                    XmlNodeList itemNodeSessao3DList = itemNodeSessaoHorarios.SelectNodes("sessao3D");

                    foreach (IXmlNode item in itemNodeSessao3DList)
                    {
                        horarioVO.Sessao3D.Add(item.InnerText);
                    }

                    XmlNodeList itemNodeHoraList = itemNodeSessaoHorarios.SelectNodes("hora");

                    foreach (IXmlNode item in itemNodeHoraList)
                    {
                        horarioVO.Hora.Add(item.InnerText);
                    }

                    XmlNodeList itemNodeObservacoesList = itemNodeSessaoHorarios.SelectNodes("observacoes");

                    foreach (IXmlNode item in itemNodeObservacoesList)
                    {
                        horarioVO.Observacoes.Add(item.InnerText);
                    }

                    sessaoVO.Horario = horarioVO;

                    linkSingleItemVO.Sessao.Add(sessaoVO);
                }

                return linkSingleItemVO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método para carregar o xml da agenda cultural no VO
        /// </summary>
        /// <param name="channel"> Obj de channel agenda cultural do VO</param>        
        private static async Task LoadXmChannelAgendaCulturalUrl(ChannelAgendaCulturalVO channel)
        {
            try
            {
                XmlDocument pvtXmlDocument = await XDocToXmlDoc(channel.Url);

                IXmlNode rssNode = pvtXmlDocument.SelectSingleNode("rss");
                IXmlNode channelNode = rssNode.SelectSingleNode("channel");

                channel.Title = channelNode.SelectSingleNode("title").InnerText;
                channel.Description = channelNode.SelectSingleNode("description").InnerText;
                channel.Link = channelNode.SelectSingleNode("link").InnerText;
                channel.LastBuildDate = channelNode.SelectSingleNode("lastBuildDate").InnerText;
                channel.PubDate = channelNode.SelectSingleNode("pubDate").InnerText;

                XmlNodeList itemNodeList = channelNode.SelectNodes("item");

                foreach (IXmlNode node in itemNodeList)
                {
                    ItemAgendaCulturalVO itemVO = new ItemAgendaCulturalVO();

                    itemVO.Title = node.SelectSingleNode("title").InnerText;
                    itemVO.Tipo = node.SelectSingleNode("tipo").InnerText;
                    itemVO.Description = node.SelectSingleNode("description").InnerText;
                    itemVO.Link = node.SelectSingleNode("link").InnerText;
                    //itemVO.LinkSingle = await LoadXmLinkSingleUrl(node.SelectSingleNode("linksingle").InnerText);
                    itemVO.UrlLinkSingle = node.SelectSingleNode("linksingle").InnerText;
                    itemVO.LinkSingle = null;
                    itemVO.Guid = Convert.ToInt32(node.SelectSingleNode("guid").InnerText);
                    itemVO.PubDate = node.SelectSingleNode("pubDate").InnerText;

                    if (node.SelectSingleNode("gratis") != null)
                    {
                        itemVO.Gratis = node.SelectSingleNode("gratis").InnerText;
                    }
                    else
                    {
                        itemVO.Gratis = string.Empty;
                    }

                    itemVO.Horario = node.SelectSingleNode("horario").InnerText;
                    itemVO.LabelSessoes = node.SelectSingleNode("labelSessoes").InnerText;

                    EnderecoVO enderecoVO = new EnderecoVO();

                    if (node.SelectSingleNode("endereco").SelectSingleNode("complemento") != null)
                    {
                        enderecoVO.Complemento = node.SelectSingleNode("endereco").SelectSingleNode("complemento").InnerText;
                    }
                    else
                    {
                        enderecoVO.Complemento = string.Empty;
                    }

                    if (node.SelectSingleNode("endereco").SelectSingleNode("espaco") != null)
                    {
                        enderecoVO.Espaco = node.SelectSingleNode("endereco").SelectSingleNode("espaco").InnerText;
                    }
                    else
                    {
                        enderecoVO.Espaco = string.Empty;
                    }

                    if (node.SelectSingleNode("endereco").SelectSingleNode("estabelecimento") != null)
                    {
                        enderecoVO.Estabelecimento = node.SelectSingleNode("endereco").SelectSingleNode("estabelecimento").InnerText;
                    }
                    else
                    {
                        enderecoVO.Estabelecimento = string.Empty;
                    }

                    if (node.SelectSingleNode("endereco").SelectSingleNode("localidade") != null)
                    {
                        enderecoVO.Localidade = node.SelectSingleNode("endereco").SelectSingleNode("localidade").InnerText;
                    }
                    else
                    {
                        enderecoVO.Localidade = string.Empty;
                    }

                    if (node.SelectSingleNode("endereco").SelectSingleNode("logradouro") != null)
                    {
                        enderecoVO.Logradouro = node.SelectSingleNode("endereco").SelectSingleNode("logradouro").InnerText;
                    }
                    else
                    {
                        enderecoVO.Logradouro = string.Empty;
                    }

                    itemVO.Endereco = enderecoVO;

                    if (node.SelectSingleNode("thumb") != null)
                    {
                        itemVO.Thumb = node.SelectSingleNode("thumb").InnerText;
                    }
                    else
                    {
                        itemVO.Thumb = string.Empty;
                    }

                    channel.ListaItem.Add(itemVO);
                }

                SaveXmlLocalAsync(channel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método para carregar o xml da agenda cultural no VO
        /// </summary>        
        /// <param name="url"> string url do Link Single</param>
        /// <returns> Objs LinkSingleItemVO</returns>
        public static  LinkSingleItemVO LoadXmLinkSingleUrl(string url)
        {
            try
            {
                string strXML = XDocument.Load(url).ToString();
                XmlDocument pvtXmlDocument = new XmlDocument();

                pvtXmlDocument.LoadXml(strXML);


                IXmlNode rssNode = pvtXmlDocument.SelectSingleNode("rss");
                IXmlNode channelNode = rssNode.SelectSingleNode("channel");
                IXmlNode channelItemNode = channelNode.SelectSingleNode("item");

                LinkSingleItemVO linkSingleItemVO = new LinkSingleItemVO();
                linkSingleItemVO.Title = channelItemNode.SelectSingleNode("title").InnerText;
                linkSingleItemVO.Tipo = channelItemNode.SelectSingleNode("tipo").InnerText;
                linkSingleItemVO.Description = channelItemNode.SelectSingleNode("description").InnerText;
                linkSingleItemVO.Link = channelItemNode.SelectSingleNode("link").InnerText;
                linkSingleItemVO.Guid = Convert.ToInt32(channelItemNode.SelectSingleNode("guid").InnerText);
                linkSingleItemVO.PubDate = channelItemNode.SelectSingleNode("pubDate").InnerText;

                IXmlNode itemNodeListPreco = channelItemNode.SelectSingleNode("precos");

                if (itemNodeListPreco != null)
                {
                    XmlNodeList itemNodeListValor = itemNodeListPreco.SelectNodes("valor");

                    foreach (IXmlNode node in itemNodeListValor)
                    {
                        linkSingleItemVO.Preco.Add(node.InnerText);
                    }
                }

                linkSingleItemVO.Horario = channelItemNode.SelectSingleNode("horario").InnerText;
                linkSingleItemVO.LabelSessoes = channelItemNode.SelectSingleNode("labelSessoes").InnerText;

                XmlNodeList itemNodeList = channelItemNode.SelectSingleNode("sessoes").SelectNodes("sessao");

                foreach (IXmlNode node in itemNodeList)
                {
                    SessaoVO sessaoVO = new SessaoVO();
                    sessaoVO.Guid = Convert.ToInt32(node.SelectSingleNode("guid").InnerText);
                    sessaoVO.Horario = node.SelectSingleNode("horario").InnerText;

                    linkSingleItemVO.Sessao.Add(sessaoVO);
                }

                EnderecoVO enderecoVO = new EnderecoVO();

                if (channelItemNode.SelectSingleNode("endereco").SelectSingleNode("complemento") != null)
                {
                    enderecoVO.Complemento = channelItemNode.SelectSingleNode("endereco").SelectSingleNode("complemento").InnerText;
                }
                else
                {
                    enderecoVO.Complemento = string.Empty;
                }

                if (channelItemNode.SelectSingleNode("endereco").SelectSingleNode("espaco") != null)
                {
                    enderecoVO.Espaco = channelItemNode.SelectSingleNode("endereco").SelectSingleNode("espaco").InnerText;
                }
                else
                {
                    enderecoVO.Espaco = string.Empty;
                }

                if (channelItemNode.SelectSingleNode("endereco").SelectSingleNode("estabelecimento") != null)
                {
                    enderecoVO.Estabelecimento = channelItemNode.SelectSingleNode("endereco").SelectSingleNode("estabelecimento").InnerText;
                }
                else
                {
                    enderecoVO.Estabelecimento = string.Empty;
                }

                if (channelItemNode.SelectSingleNode("endereco").SelectSingleNode("localidade") != null)
                {
                    enderecoVO.Localidade = channelItemNode.SelectSingleNode("endereco").SelectSingleNode("localidade").InnerText;
                }
                else
                {
                    enderecoVO.Localidade = string.Empty;
                }

                if (channelItemNode.SelectSingleNode("endereco").SelectSingleNode("logradouro") != null)
                {
                    enderecoVO.Logradouro = channelItemNode.SelectSingleNode("endereco").SelectSingleNode("logradouro").InnerText;
                }
                else
                {
                    enderecoVO.Logradouro = string.Empty;
                }

                linkSingleItemVO.Endereco = enderecoVO;

                if (channelItemNode.SelectSingleNode("thumb") != null)
                {
                    linkSingleItemVO.LabelSessoes = channelItemNode.SelectSingleNode("thumb").InnerText;
                }
                else
                {
                    linkSingleItemVO.LabelSessoes = string.Empty;
                }

                return linkSingleItemVO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}