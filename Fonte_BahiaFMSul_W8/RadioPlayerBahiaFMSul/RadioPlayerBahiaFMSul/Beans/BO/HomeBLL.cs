using RadioPlayer.Beans.ValueObject.ValueObject.Xml;
using RadioPlayer.Flyout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View = RadioPlayer.Beans.ValueObject.ValueObject.View.Principal;
using Cinema = RadioPlayer.Beans.ValueObject.ValueObject.Xml.AgendaCulturalXML.Cinema.Cartaz;
using System.Collections.ObjectModel;
using RadioPlayer.Beans.ValueObject.ValueObject.Xml.AgendaCulturalXML;
using RadioPlayer.Common.Architecture;

namespace RadioPlayer.Beans.BO
{

    /// <summary>
    /// Classe de regra de negócios da tela principal do sistema.
    /// </summary>
    public class HomeBLL
    {
        /// <summary>
        /// Atributo ultimasNoticias.
        /// </summary>
        private static View.CategoriaVO ultimasNoticias;

        /// <summary>
        /// Atributo agendaCultural.
        /// </summary>
        private static View.CategoriaVO agendaCultural;

        /// <summary>
        /// Atributo entretenimento.
        /// </summary>
        private static View.CategoriaVO entretenimento;

        /// <summary>
        /// Atributo esportes.
        /// </summary>
        private static View.CategoriaVO esportes;

        /// <summary>
        /// Atributo telaMensagem.
        /// </summary>
        private static TelaMensagem telaMensagem = TelaMensagem.Instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeBLL"/> class.
        /// </summary>
        public HomeBLL()
        {
            DataSourceRadio.ListaCategoria.Clear();

            ////As 4 categorias da tela principal
            ultimasNoticias = new View.CategoriaVO() { Id = 1, NomeCategoria = "ÚLTIMAS NOTÍCIAS", ListaItem = new ObservableCollection<View.ItemVO>(), CorLetraTitulo = View.ItemVO.CorLetraNoticia };
            agendaCultural = new View.CategoriaVO() { Id = 0, NomeCategoria = "DIVIRTA-SE", ListaItem = new ObservableCollection<View.ItemVO>(), CorLetraTitulo = View.ItemVO.CorLetraAgendaCultural };
            entretenimento = new View.CategoriaVO() { Id = 3, NomeCategoria = "ENTRETENIMENTO", ListaItem = new ObservableCollection<View.ItemVO>(), CorLetraTitulo = View.ItemVO.CorLetraEntretenimento };
              
            esportes = new View.CategoriaVO() { Id = 2, NomeCategoria = "ESPORTES", ListaItem = new ObservableCollection<View.ItemVO>(), CorLetraTitulo = View.ItemVO.CorLetraEsportes };

            ultimasNoticias.ListaItem.Add(new View.ItemVO());
            ultimasNoticias.ListaItem.Add(new View.ItemVO());
            ultimasNoticias.ListaItem.Add(new View.ItemVO());
            ultimasNoticias.ListaItem.Add(new View.ItemVO());
            ultimasNoticias.ListaItem.Add(new View.ItemVO());

            agendaCultural.ListaItem.Add(new View.ItemVO());
            agendaCultural.ListaItem.Add(new View.ItemVO());
            agendaCultural.ListaItem.Add(new View.ItemVO());
            agendaCultural.ListaItem.Add(new View.ItemVO());

            DataSourceRadio.ListaCategoria.Add(ultimasNoticias);
            DataSourceRadio.ListaCategoria.Add(agendaCultural);
            DataSourceRadio.ListaCategoria.Add(entretenimento);
            DataSourceRadio.ListaCategoria.Add(esportes);
        }

        /// <summary>
        /// Método que inicializa os dados da tela principal
        /// </summary>
        public void LoadDataHome()
        {
            try
            {
                ultimasNoticias = DataSourceRadio.ListaCategoria[0];
                agendaCultural = DataSourceRadio.ListaCategoria[1];
                entretenimento = DataSourceRadio.ListaCategoria[2];
                esportes = DataSourceRadio.ListaCategoria[3];

                ////Verifica se  a lista de ultimas notícias já foi prennchida
                if (ultimasNoticias.ListaItem[0].ListaSubItem.Count.Equals(0))
                {
                    ////Verifica se o channel de notícias já foi preenchido.
                    if (DataSourceRadio.ListaChannelVO.Where(item => item.Id == 22).FirstOrDefault<ChannelVO>().IsLoaded)
                    {
                        ////Seleciona o channel de ultimas noticias, o first e para capturar o primeiro elemento da consulta.
                        ChannelVO channelUltimasNoticias = DataSourceRadio.ListaChannelVO.Where(item => item.Id == 22).First<ChannelVO>();
                        ////Preenche as vintes primeiras noticias por ordem de publicação
                        List<ItemVO> listaUltimaNoticia = channelUltimasNoticias.ListaItem.OrderByDescending(item => item.PubDate).Take<ItemVO>(10).ToList();

                        ////A lista das ultimas noticias sem categorias.
                        View.ItemVO itemListaUltimaNoticia = new View.ItemVO() { Titulo = string.Empty, SubTitulo = string.Empty, IsList = true, ListaSubItem = new ObservableCollection<View.SubItemVO>(), ImagemFundo = new ImageVO(), HasImagemIBahia = false, ListaTituloSubItem = string.Empty, IsEventoCultural = false };

                       string categoria = string.Empty;

                        foreach (ItemVO itmListaUltimaNoticia in listaUltimaNoticia)
                        {
                            ImageVO img = new ImageVO();

                            if (!itmListaUltimaNoticia.ListaImage.Count.Equals(0))
                            {
                                ////Caso tenha alguma img na lista de img da noticia, pega a primeira
                                img.Url = itmListaUltimaNoticia.ListaImage[0].Url;

                                if (!string.IsNullOrEmpty(itmListaUltimaNoticia.ListaImage[0].Title))
                                {
                                    img.Title = itmListaUltimaNoticia.ListaImage[0].Title;
                                }
                            }

                            if (itmListaUltimaNoticia.ListaCategoria.Any<CategoriaVO>())
                            {
                                categoria = itmListaUltimaNoticia.ListaCategoria.First<CategoriaVO>().Title.ToUpper();
                            }
                            else
                            {
                                categoria = "ÚLTIMA NOTÍCIA";
                            }

                            itemListaUltimaNoticia.ListaSubItem.Add(new View.SubItemVO() { Titulo = categoria, SubTitulo = itmListaUltimaNoticia.Title, ImagemFundoSubItem = img, Index = 2, IdChannel = channelUltimasNoticias.Id, TypeClass = typeof(ChannelVO), Guid = itmListaUltimaNoticia.Guid });
                        }

                        itemListaUltimaNoticia.ListaSubItem.First<View.SubItemVO>().Index = 1;

                        ultimasNoticias.ListaItem[0] = itemListaUltimaNoticia;
                    }
                }

                if (ultimasNoticias.ListaItem[1].Guid.Equals(0))
                {
                    if (DataSourceRadio.ListaChannelVO.Where(item => item.Id == 2).First<ChannelVO>().IsLoaded)
                    {
                        ////Add uma noticia aleatória de salvador.
                        ultimasNoticias.ListaItem[1] = this.GetOneItemNoticia(2, View.ItemVO.BgImagemNoticia);
                    }
                }

                if (ultimasNoticias.ListaItem[2].Guid.Equals(0))
                {
                    if (DataSourceRadio.ListaChannelVO.Where(item => item.Id == 12).First<ChannelVO>().IsLoaded)
                    {
                        ////Add uma noticia aleatória da Bahia
                        ultimasNoticias.ListaItem[2] = this.GetOneItemNoticia(12, View.ItemVO.BgImagemNoticia);
                    }
                }

                if (ultimasNoticias.ListaItem[3].Guid.Equals(0))
                {
                    if (DataSourceRadio.ListaChannelVO.Where(item => item.Id == 13).First<ChannelVO>().IsLoaded)
                    {
                        ////Add uma noticia aleatória da Brasil
                        ultimasNoticias.ListaItem[3] = this.GetOneItemNoticia(13, View.ItemVO.BgImagemNoticia);
                    }
                }

                if (ultimasNoticias.ListaItem[4].Guid.Equals(0))
                {
                    if (DataSourceRadio.ListaChannelVO.Where(item => item.Id == 14).First<ChannelVO>().IsLoaded)
                    {
                        ////Add uma noticia aleatória da Mundo
                        ultimasNoticias.ListaItem[4] = this.GetOneItemNoticia(14, View.ItemVO.BgImagemNoticia);
                    }
                }

                if (ultimasNoticias.ListaItem[4].Guid.Equals(0))
                {
                    if (DataSourceRadio.ListaChannelVO.Where(item => item.Id == 1).First<ChannelVO>().IsLoaded)
                    {
                        ////Add uma noticia aleatória da Mundo
                        ultimasNoticias.ListaItem[4] = this.GetOneItemNoticia(1, View.ItemVO.BgImagemNoticia);
                    }
                }

                //if (string.IsNullOrEmpty(agendaCultural.ListaItem[0].Titulo))
                //{
                //    if (!DataSourceRadio.ListaChannelCinemaVO[0].ListaItem.Count().Equals(0))
                //    {
                View.ItemVO itemCinema = new View.ItemVO() { Titulo = string.Empty, ImgblocoAgendaCultural = Constantes.ResourceImagem.GetString(Constantes.ImagemAgendaCultural.imageCinema.ToString()), SubTitulo = string.Empty, IsList = true, ListaSubItem = new ObservableCollection<View.SubItemVO>(), ImagemFundo = new ImageVO(), HasImagemIBahia = false, ListaTituloSubItem = "Cinema", IsEventoCultural = true };

                //Cinema.ChannelCinemaVO channelCartaz = DataSourceRadio.ListaChannelCinemaVO[0];

                //List<Cinema.ItemCinemaVO> listaItemChannelCartaz = channelCartaz.ListaItem.OrderBy(item => item.Guid).Take(4).ToList();

                //ObservableCollection<View.SubItemVO> subItemTemp = new ObservableCollection<View.SubItemVO>();

                //foreach (Cinema.ItemCinemaVO itemChannelCartaz in listaItemChannelCartaz)
                //{
                //    subItemTemp.Add(new View.SubItemVO() { Titulo = itemChannelCartaz.Title, SubTitulo = itemChannelCartaz.Linksingle.Genero, ImagemFundoSubItem = new ImageVO(), Index = 0, Guid = itemChannelCartaz.Guid, TypeClass = typeof(Cinema.ItemCinemaVO), IdChannel = 0 });
                //}

                //itemCinema.ListaSubItem = subItemTemp;
                agendaCultural.ListaItem[0] = itemCinema;
                //    }
                //}

                //if (agendaCultural.ListaItem[1].ListaSubItem.Count.Equals(0))
                //{
                //    if (!DataSourceRadio.ListaChannelAgendaCulturalVO.Where(item => item.Id == 2).First<ChannelAgendaCulturalVO>().ListaItem.Count().Equals(0))
                //    {
                agendaCultural.ListaItem[1] = this.GetOneItemList(2);
                agendaCultural.ListaItem[1].ImgblocoAgendaCultural = Constantes.ResourceImagem.GetString(Constantes.ImagemAgendaCultural.imageMusica.ToString());
                //    }
                //}

                //if (string.IsNullOrEmpty(agendaCultural.ListaItem[2].Titulo))
                //{
                //    if (!DataSourceRadio.ListaChannelAgendaCulturalVO.Where(item => item.Id == 5).First<ChannelAgendaCulturalVO>().ListaItem.Count().Equals(0))
                //    {
                agendaCultural.ListaItem[2] = this.GetOneItemList(5);
                agendaCultural.ListaItem[2].ImgblocoAgendaCultural = Constantes.ResourceImagem.GetString(Constantes.ImagemAgendaCultural.imageTeatro.ToString());
                //    }
                //}

                //if (string.IsNullOrEmpty(agendaCultural.ListaItem[3].Titulo))
                //{
                //    if (!DataSourceRadio.ListaChannelAgendaCulturalVO.Where(item => item.Id == 4).First<ChannelAgendaCulturalVO>().ListaItem.Count().Equals(0))
                //    {
                agendaCultural.ListaItem[3] = this.GetOneItemList(4);
                agendaCultural.ListaItem[3].ImgblocoAgendaCultural = Constantes.ResourceImagem.GetString(Constantes.ImagemAgendaCultural.imageExposicao.ToString());
                //    }
                //}

                if (entretenimento.ListaItem.Count.Equals(0))
                {
                    if (!DataSourceRadio.ListaChannelVO.Where(item => item.Id == 26).First<ChannelVO>().ListaItem.Count().Equals(0))
                    {
                        List<View.ItemVO> listaEntretenimento = this.GetSixItemNoticia(26, View.ItemVO.CorLetraEntretenimento, View.ItemVO.BgImagemEntretenimento);

                        foreach (View.ItemVO item in listaEntretenimento)
                        {
                            entretenimento.ListaItem.Add(item);
                        }
                    }
                }

                if (esportes.ListaItem.Count.Equals(0))
                {
                    if (!DataSourceRadio.ListaChannelVO.Where(item => item.Id == 3).First<ChannelVO>().ListaItem.Count().Equals(0))
                    {
                        List<View.ItemVO> listaEsportes = this.GetSixItemNoticia(3, View.ItemVO.CorLetraEsportes, View.ItemVO.BgImagemEsportes);

                        foreach (View.ItemVO item in listaEsportes)
                        {
                            esportes.ListaItem.Add(item);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                telaMensagem.ShowMensagem(e);
                telaMensagem.UpdateLayout();
            }
        }

        /// <summary>
        /// Método que captura seis noticias do channel
        /// </summary>
        /// <param name="channelID">Id do channel</param>
        /// <param name="Cor">Cor da letra ou fundo do stackPanel</param>
        /// <param name="imgItem">Img de fundo do item</param>
        /// <returns>Retorna uma lista de ItemVO do view</returns>
        public List<View.ItemVO> GetSixItemNoticia(int channelID, string cor, string imgItem)
        {
            try
            {
                ////Seleciona o channel de Entretenimento, o first e para capturar o primeiro elemento da consulta.
                ChannelVO channelEntretenimento = DataSourceRadio.ListaChannelVO.Where(item => item.Id == channelID).First<ChannelVO>();
                List<View.ItemVO> listItemVO = new List<View.ItemVO>();

                List<ItemVO> listChannelItemVO = channelEntretenimento.ListaItem.OrderByDescending(item => item.PubDate).Take(6).ToList();
                ImageVO img = new ImageVO();
                bool hasImg = false;
                Random random = new Random();
                int intRandom = 0;

                foreach (ItemVO item in listChannelItemVO)
                {
                    intRandom = random.Next(item.ListaImage.Count);
                    img = new ImageVO();
                    hasImg = false;

                    if (item.ListaImage.Count.Equals(0))
                    {
                        img.Url = imgItem;
                        hasImg = false;
                    }
                    else
                    {
                        if (item.ListaImage.Count > 1)
                        {
                            img.Url = item.ListaImage[random.Next(item.ListaImage.Count)].Url;

                            if (!string.IsNullOrEmpty(item.ListaImage[random.Next(item.ListaImage.Count)].Title))
                            {
                                img.Title = item.ListaImage[random.Next(item.ListaImage.Count)].Title;
                            }

                            hasImg = true;
                        }
                        else
                        {
                            img.Url = item.ListaImage[0].Url;

                            if (!string.IsNullOrEmpty(item.ListaImage[0].Title))
                            {
                                img.Title = item.ListaImage[0].Title;
                            }

                            hasImg = true;
                        }
                    }

                    string categoria = string.Empty;

                    if (item.ListaCategoria.Any<CategoriaVO>())
                    {
                        categoria = item.ListaCategoria.First<CategoriaVO>().Title.ToUpper();
                    }
                    else
                    {
                        categoria = "NOTÍCIA";
                    }

                    ////Todo : verificar se a lista de intem do channel não está vazia, para não ocorer nenhum erro.
                    listItemVO.Add(new View.ItemVO() { Titulo = categoria.ToUpper(), SubTitulo = item.Title, IsList = false, ListaSubItem = null, ImagemFundo = img, HasImagemIBahia = hasImg, ListaTituloSubItem = null, IsEventoCultural = false, CorStackPanelItem = cor, Guid = item.Guid, IdChannel = channelID, TypeClass = typeof(ChannelVO) });
                }

                return listItemVO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Método que preenche uma lista e devolve um item
        /// </summary>
        /// <param name="channelID">Id do channelVO</param>
        /// <returns>Retorna um item da VO View</returns>
        public View.ItemVO GetOneItemList(int channelID)
        {
            try
            {
                ////Seleciona o channel de shows, o first e para capturar o primeiro elemento da consulta.
                ChannelAgendaCulturalVO channel = DataSourceRadio.ListaChannelAgendaCulturalVO.Where(item => item.Id == channelID).First<ChannelAgendaCulturalVO>();

                ////Lista de Musica. TODO: ordenar por data de evento
                View.ItemVO itemVO = new View.ItemVO() { Titulo = string.Empty, SubTitulo = string.Empty, IsList = true, ListaSubItem = new ObservableCollection<View.SubItemVO>(), ImagemFundo = new ImageVO(), HasImagemIBahia = false, ListaTituloSubItem = channel.NomeChannel, IsEventoCultural = true };

                //List<ItemAgendaCulturalVO> ListaAgendaCultural = channel.ListaItem.OrderByDescending(item => item.Horario).Take(4).ToList();

                //foreach (ItemAgendaCulturalVO item in ListaAgendaCultural)
                //{
                //    itemVO.ListaSubItem.Add(new View.SubItemVO() { Titulo = item.Title, SubTitulo = Convert.ToDateTime(item.LinkSingle.Horario).ToString().Substring(0, Convert.ToDateTime(item.LinkSingle.Horario).ToString().Length - 3), ImagemFundoSubItem = new ImageVO(), Index = 0, Guid = item.Guid, TypeClass = typeof(ItemAgendaCulturalVO), IdChannel = channelID });
                //}

                return itemVO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Método que Captura um item da notícia aleatóriamente
        /// </summary>
        /// <param name="channelID">Id do channelVO</param>
        /// <param name="imgItem">Img do fundo do item.</param>
        /// <returns>Retorna um itemVO do View</returns>
        public View.ItemVO GetOneItemNoticia(int channelID, string imgItem)
        {
            try
            {
                ////Seleciona o channel de salvador, o first e para capturar o primeiro elemento da consulta, por enquento está sendo capturado a primeira noticia aleatória..
                ChannelVO channelUltimasNoticias = DataSourceRadio.ListaChannelVO.Where(item => item.Id == channelID).First<ChannelVO>();
                Random random = new Random();
                int intRandom = 0;
                if (!channelUltimasNoticias.ListaItem.Count.Equals(0))
                {
                    intRandom = random.Next(channelUltimasNoticias.ListaItem.Count);
                    ImageVO img = new ImageVO();
                    bool hasImg = false;

                    if (channelUltimasNoticias.ListaItem[intRandom].ListaImage.Count.Equals(0))
                    {
                        img.Url = imgItem;
                        hasImg = false;
                    }
                    else
                    {
                        if (channelUltimasNoticias.ListaItem[intRandom].ListaImage.Count > 1)
                        {
                            img.Url = channelUltimasNoticias.ListaItem[intRandom].ListaImage[random.Next(channelUltimasNoticias.ListaItem[intRandom].ListaImage.Count)].Url;

                            if (!string.IsNullOrEmpty(channelUltimasNoticias.ListaItem[intRandom].ListaImage[random.Next(channelUltimasNoticias.ListaItem[intRandom].ListaImage.Count)].Title))
                            {
                                img.Title = channelUltimasNoticias.ListaItem[intRandom].ListaImage[random.Next(channelUltimasNoticias.ListaItem[intRandom].ListaImage.Count)].Title;
                            }

                            hasImg = true;
                        }
                        else
                        {
                            img.Url = channelUltimasNoticias.ListaItem[intRandom].ListaImage[0].Url;

                            if (!string.IsNullOrEmpty(channelUltimasNoticias.ListaItem[intRandom].ListaImage[0].Title))
                            {
                                img.Title = channelUltimasNoticias.ListaItem[intRandom].ListaImage[0].Title;
                            }

                            hasImg = true;
                        }
                    }

                    string categoria = string.Empty;
                    if (channelUltimasNoticias.ListaItem[intRandom].ListaCategoria.Any<CategoriaVO>())
                    {
                        categoria = channelUltimasNoticias.ListaItem[intRandom].ListaCategoria.First<CategoriaVO>().Title.ToUpper();
                    }
                    else
                    {
                        categoria = "NOTÍCIAS";
                    }

                    View.ItemVO itemListaUltimaNoticia = new View.ItemVO()
                    {
                        Titulo = categoria.ToUpper(),
                        SubTitulo = channelUltimasNoticias.ListaItem[intRandom].Title,
                        IsList = false,
                        ListaSubItem = null,
                        ImagemFundo = img,
                        HasImagemIBahia = hasImg,
                        ListaTituloSubItem = null,
                        IsEventoCultural = false,
                        CorStackPanelItem = View.ItemVO.CorLetraNoticia,
                        Guid = channelUltimasNoticias.ListaItem[intRandom].Guid,
                        TypeClass = typeof(ChannelVO),
                        IdChannel = channelID
                    };

                    return itemListaUltimaNoticia;
                }
                else
                {
                    return new View.ItemVO();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
