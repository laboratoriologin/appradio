using XML = RadioPlayer.Beans.ValueObject.ValueObject.Xml;
using RadioPlayer.Beans.ValueObject.ValueObject.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RadioPlayer.Beans.ValueObject.ValueObject.View.Principal;
using RadioPlayer.Common.Architecture;
using RadioPlayer.Beans.ValueObject.ValueObject.Xml.NoticiaCorreioXML;
using RadioPlayer.Beans.ValueObject.ValueObject.Xml.PodCastXML;
using RadioPlayer.Beans.ValueObject.Xml.Promocao;

namespace RadioPlayer.Beans.BO
{
    /// <summary>
    /// Classe de regra de negócios da tela de grupo de noticias.
    /// </summary>
    public class GrupoNoticiasItemBLL
    {
        private string strNomeCategoria;
        //public GrupoNoticiasItemBLL()
        //{
        //}

        public GrupoNoticiasItemBLL(int opcao)
        {
            switch (opcao)
            {
                case 1:
                    DataSourceRadio.GrupoNoticias.ListaCategoria = new ObservableCollection<CategoriaVO>();
                    DataSourceRadio.GrupoNoticias.Titulo = "NOTÍCIAS";
                    break;
                case 2:
                    DataSourceRadio.GrupoNoticias.ListaCategoria = new ObservableCollection<CategoriaVO>();
                    DataSourceRadio.GrupoNoticias.Titulo = "ESPORTES";
                    break;
                case 3:
                    DataSourceRadio.GrupoNoticias.ListaCategoria = new ObservableCollection<CategoriaVO>();
                    DataSourceRadio.GrupoNoticias.Titulo = "ENTRETENIMENTO";
                    break;
            }
        }

        /// <summary>
        /// Metodo responsavel por carregar todos os dados do VO XML no VO VIEW de acordo com
        /// opção da tela.
        /// </summary>
        /// <param name="opcao">
        /// Opcao que diz qual sera o tipo da tela.
        /// 1 - Noticia
        /// 2 - Esporte
        /// 3 - Entretenimento
        /// </param>
        public void LoadDataGrupoNoticias(int opcao, int idCategoria)
        {
            switch (opcao)
            {
                case 1:
                    this.LoadDataNoticia(idCategoria);
                    break;
                case 2:
                    this.LoadDataEsporte(idCategoria);
                    break;
                case 3:
                    this.LoadDataEntretenimento(idCategoria);
                    break;

                case 4:
                         break;


                case 5:
                    this.LoadDataPodCast();
                    break;

                case 6:
                    this.LoadDataPromocao();
                    break;
            }
        }

        /// <summary>
        /// Metodo responsavel por carregar os dados de noticia.
        /// </summary>
        private void LoadDataNoticia(int idCategoria)
        {
            // Carrega as categorias de noticia em ordem a ser exibida e carrega seus itens.
            XML.ChannelVO channel = DataSourceRadio.ListaChannelVO.Where(item => item.Id == idCategoria).First<XML.ChannelVO>();
            List<XML.ItemVO> listaItem = channel.ListaItem.OrderByDescending(item => item.PubDate).Take(4).ToList();

            CategoriaVO categoria = new CategoriaVO() { NomeCategoria = channel.NomeChannel, ListaItem = null, CorLetraTitulo = ItemVO.CorLetraNoticia };
            categoria.ListaItem = new ObservableCollection<ItemVO>();

            foreach (XML.ItemVO item in listaItem)
            {
                ItemVO itemView = new ItemVO();
                itemView.Titulo = item.Title.ToUpper();
                itemView.SubTitulo = item.Title;
                itemView.IsList = false;
                itemView.ListaSubItem = null;
                itemView.IdChannel = channel.Id;
                itemView.Guid = item.Guid;
                itemView.Descricao = item.Description;
                itemView.StrConteudo = item.Content;
                itemView.ImagemFundo = new XML.ImageVO();

                itemView.ImagemFundo = new XML.ImageVO();

                // Define se tem imagem na noticia ou não
                if (item.ListaImage.Count.Equals(0))
                {
                    itemView.HasImagemIBahia = true;
                }
                else
                {
                    itemView.HasImagemIBahia = false;
                    itemView.ImagemFundo.Url = item.ListaImage.First().Url;

                    if (!string.IsNullOrEmpty(item.ListaImage.First().Title))
                    {
                        itemView.ImagemFundo.Title = item.ListaImage.First().Title;
                    }
                }

                itemView.ListaTituloSubItem = null;
                itemView.IsEventoCultural = false;
                itemView.CorStackPanelItem = ItemVO.CorLetraNoticia;

                categoria.ListaItem.Add(itemView);
            }


            if (string.IsNullOrEmpty(strNomeCategoria) && (listaItem.Count() > 0))
            {
                DataSourceRadio.GrupoNoticias.ListaCategoria.Add(categoria);
                strNomeCategoria = categoria.NomeCategoria;
            }







        }


        /// <summary>
        /// Metodo responsavel por carregar os dados de noticia.
        /// </summary>
        public void LoadDataNoticiaSalvador()
        {
            // Carrega as categorias de noticia em ordem a ser exibida e carrega seus itens.
            ChannelCorreioVO channel = DataSourceRadio.ListaChannelCorreioVO.Where(item => item.Id == 100).First<XML.NoticiaCorreioXML.ChannelCorreioVO>();
            List<XML.NoticiaCorreioXML.ItemCorreioVO> listaItem = channel.ListaItem.OrderByDescending(item => item.PubDate).Take(4).ToList();

            CategoriaVO categoria = new CategoriaVO() { NomeCategoria = channel.NomeChannel, ListaItem = null, CorLetraTitulo = ItemVO.CorLetraNoticia };
            categoria.ListaItem = new ObservableCollection<ItemVO>();

            foreach (ItemCorreioVO item in listaItem)
            {
                ItemVO itemView = new ItemVO();
                itemView.Titulo = "Notícias CBN";
                itemView.SubTitulo = item.Title;
                itemView.IsList = false;
                itemView.ListaSubItem = null;
                itemView.IdChannel = channel.Id;
                itemView.Guid = item.Guid;
                itemView.Descricao = item.Description;
                itemView.StrConteudo = item.Content;
                itemView.ImagemFundo = new XML.ImageVO();

                // Define se tem imagem na noticia ou não
                if (item.ListaImage.Count.Equals(0))
                {
                    itemView.HasImagemIBahia = true;
                }
                else
                {
                    itemView.HasImagemIBahia = false;
                    itemView.ImagemFundo.Url = item.ListaImage.First().Url;

                    if (!string.IsNullOrEmpty(item.ListaImage.First().Title))
                    {
                        itemView.ImagemFundo.Title = item.ListaImage.First().Title;
                    }
                }

                itemView.ListaTituloSubItem = null;
                itemView.IsEventoCultural = false;
                itemView.CorStackPanelItem = ItemVO.CorLetraNoticia;


                categoria.ListaItem.Add(itemView);
            }

            categoria.NomeCategoria = "Notícias CBN";
            categoria.CorLetraTitulo = "FFFFFF";
            //if (string.IsNullOrEmpty(strNomeCategoria) && (listaItem.Count() > 0))
            //{
            DataSourceRadio.GrupoNoticias.ListaCategoria.Add(categoria);
            Constantes.objNoticiaCorreio = DataSourceRadio.GrupoNoticias.ListaCategoria.ToList();
            strNomeCategoria = categoria.NomeCategoria;
            //} 


        }

        /// <summary>
        /// Metodo responsavel por carregar os dados de noticia.
        /// </summary>
        public void LoadDataPromocao()
        {
            // Carrega as categorias de noticia em ordem a ser exibida e carrega seus itens.
            ChannelPromocaoVO channel = DataSourceRadio.ListaChannelPromocaoVO.Where(item => item.Id == 105).First<ChannelPromocaoVO>();
            List<ItemPromocaoVO> listaItem = channel.ListaItem.Take(4).ToList();

            CategoriaVO categoria = new CategoriaVO() { NomeCategoria = channel.NomeChannel, ListaItem = null, CorLetraTitulo = ItemVO.CorLetraNoticia };
            categoria.ListaItem = new ObservableCollection<ItemVO>();

            foreach (ItemPromocaoVO item in listaItem)
            {
                ItemVO itemView = new ItemVO();
                itemView.Titulo = "Promoção";
                itemView.SubTitulo = item.Title;
                itemView.IsList = false;
                itemView.ListaSubItem = null;
                itemView.IdChannel = channel.Id;
                //itemView.Guid = item.Guid;
                itemView.Descricao = item.Description;
                itemView.StrConteudo = item.Content;
                itemView.ImagemFundo = new XML.ImageVO();

                // Define se tem imagem na noticia ou não
                if (item.ListaImage.Count.Equals(0))
                {
                    itemView.HasImagemIBahia = true;
                }
                else
                {
                    itemView.HasImagemIBahia = false;
                    itemView.ImagemFundo.Url = item.ListaImage.First().Url;

                    if (!string.IsNullOrEmpty(item.ListaImage.First().Title))
                    {
                        itemView.ImagemFundo.Title = item.ListaImage.First().Title;
                    }
                }

                itemView.ListaTituloSubItem = null;
                itemView.IsEventoCultural = false;
                itemView.CorStackPanelItem = ItemVO.CorLetraNoticia;


                categoria.ListaItem.Add(itemView);
            }

            categoria.NomeCategoria = "Promoção";
            categoria.CorLetraTitulo = "#00417F";
            //if (string.IsNullOrEmpty(strNomeCategoria) && (listaItem.Count() > 0))
            //{
            DataSourceRadio.GrupoNoticias.ListaCategoria.Add(categoria);
            Constantes.objNoticiaPromocao = DataSourceRadio.GrupoNoticias.ListaCategoria.ToList();
            strNomeCategoria = categoria.NomeCategoria;
            //} 
        }

        /// <summary>
        /// Metodo responsavel por carregar os dados de noticia PodCast.
        /// </summary>
        public void LoadDataNoticiaPodCast()
        {
            // Carrega as categorias de noticia em ordem a ser exibida e carrega seus itens.
            ChannelPodCastVO channel = DataSourceRadio.ListaChannelPodCastVO.Where(item => item.Id == 100).First<XML.PodCastXML.ChannelPodCastVO>();
            List<ItemPodCastVO> listaItem = channel.ListaItem.OrderByDescending(item => item.PubDate).Take(4).ToList();

            CategoriaVO categoria = new CategoriaVO() { NomeCategoria = channel.NomeChannel, ListaItem = null, CorLetraTitulo = ItemVO.CorLetraNoticia };
            //categoria.ListaCorreioItem = new ObservableCollection<ItemPodCastVO>();
            categoria.ListaItem = new ObservableCollection<ItemVO>();

            foreach (ItemPodCastVO item in listaItem)
            {
                ItemVO itemView = new ItemVO();
                itemView.Titulo = "Podcast CBN";
                itemView.SubTitulo = item.Title;
                itemView.IsList = false;
                itemView.ListaSubItem = null;
                itemView.IdChannel = channel.Id;
                //itemView.Guid = item.Guid;
                itemView.Descricao = item.Description;
                itemView.StrConteudo = item.Content;
                itemView.ImagemFundo = new XML.ImageVO();

                // Define se tem imagem na noticia ou não
                if (item.ListaImage.Count.Equals(0))
                {
                    itemView.HasImagemIBahia = true;
                }
                else
                {
                    itemView.HasImagemIBahia = false;
                    itemView.ImagemFundo.Url = item.ListaImage.First().Url;

                    if (!string.IsNullOrEmpty(item.ListaImage.First().Title))
                    {
                        itemView.ImagemFundo.Title = item.ListaImage.First().Title;
                    }
                }

                itemView.ListaTituloSubItem = null;
                itemView.IsEventoCultural = false;
                itemView.CorStackPanelItem = ItemVO.CorLetraNoticia;


                categoria.ListaItem.Add(itemView);
            }

            categoria.NomeCategoria = "Podcast CBN";
            categoria.CorLetraTitulo = "FFFFFF";
            //if (string.IsNullOrEmpty(strNomeCategoria) && (listaItem.Count() > 0))
            //{
            DataSourceRadio.GrupoNoticias.ListaCategoria.Add(categoria);
            Constantes.objNoticiaPodcast = DataSourceRadio.GrupoNoticias.ListaCategoria.ToList();
            strNomeCategoria = categoria.NomeCategoria;
            //} 
        }


        /// <summary>
        /// Metodo responsavel por carregar os dados de noticia.
        /// </summary>
        public void LoadDataPodCast()
        {
            // Carrega as categorias de noticia em ordem a ser exibida e carrega seus itens.
            ChannelPodCastVO channel = DataSourceRadio.ListaChannelPodCastVO.Where(item => item.Id == 101).First<ChannelPodCastVO>();
            List<ItemPodCastVO> listaItem = channel.ListaItem.OrderByDescending(item => item.PubDate).Take(4).ToList();

            CategoriaVO categoria = new CategoriaVO() { NomeCategoria = channel.NomeChannel, ListaItem = null, CorLetraTitulo = ItemVO.CorLetraNoticia };
            categoria.ListaItem = new ObservableCollection<ItemVO>();

            foreach (ItemPodCastVO item in listaItem)
            {
                ItemVO itemView = new ItemVO();
                itemView.Titulo = item.Title.ToUpper();
                itemView.SubTitulo = item.Title;
                itemView.IsList = false;
                itemView.ListaSubItem = null;
                itemView.IdChannel = channel.Id;
                //itemView.Guid = item.Guid;
                itemView.Descricao = item.Description;
                itemView.StrConteudo = item.Content;
                itemView.ImagemFundo = new XML.ImageVO();

                // Define se tem imagem na noticia ou não
                if (item.ListaImage.Count.Equals(0))
                {
                    itemView.HasImagemIBahia = true;
                }
                else
                {
                    itemView.HasImagemIBahia = false;
                    itemView.ImagemFundo.Url = item.ListaImage.First().Url;

                    if (!string.IsNullOrEmpty(item.ListaImage.First().Title))
                    {
                        itemView.ImagemFundo.Title = item.ListaImage.First().Title;
                    }
                }

                itemView.ListaTituloSubItem = null;
                itemView.IsEventoCultural = false;
                itemView.CorStackPanelItem = ItemVO.CorLetraNoticia;

                categoria.ListaItem.Add(itemView);
            }


            if (string.IsNullOrEmpty(strNomeCategoria) && (listaItem.Count() > 0))
            {
                DataSourceRadio.GrupoNoticias.ListaCategoria.Add(categoria);
                strNomeCategoria = categoria.NomeCategoria;
            }
        }



        /// <summary>
        /// Metodo responsavel por carregar os dados de esporte.
        /// </summary>
        private void LoadDataEsporte(int idCategoria)
        {
            // Carrega as categorias de noticia em ordem a ser exibida e carrega seus itens.
            XML.ChannelVO channel = DataSourceRadio.ListaChannelVO.Where(item => item.Id == idCategoria).First<XML.ChannelVO>();
            List<XML.ItemVO> listaItem = channel.ListaItem.OrderByDescending(item => item.PubDate).Take(4).ToList();
            CategoriaVO categoria = new CategoriaVO() { NomeCategoria = channel.NomeChannel, ListaItem = null, CorLetraTitulo = ItemVO.CorLetraEsportes };
            categoria.ListaItem = new ObservableCollection<ItemVO>();

            foreach (XML.ItemVO item in listaItem)
            {
                ItemVO itemView = new ItemVO();
                itemView.Titulo = item.Title.ToUpper();
                itemView.SubTitulo = item.Title;
                itemView.IsList = false;
                itemView.ListaSubItem = null;
                itemView.IdChannel = channel.Id;
                itemView.Guid = item.Guid;

                itemView.ImagemFundo = new XML.ImageVO();

                // Define se tem imagem na noticia ou não
                if (item.ListaImage.Count.Equals(0))
                {
                    itemView.HasImagemIBahia = true;
                }
                else
                {
                    itemView.HasImagemIBahia = false;
                    itemView.ImagemFundo.Url = item.ListaImage.First().Url;

                    if (!string.IsNullOrEmpty(item.ListaImage.First().Title))
                    {
                        itemView.ImagemFundo.Title = item.ListaImage.First().Title;
                    }
                }

                itemView.ListaTituloSubItem = null;
                itemView.IsEventoCultural = false;
                itemView.CorStackPanelItem = ItemVO.CorLetraEsportes;

                categoria.ListaItem.Add(itemView);
            }

            DataSourceRadio.GrupoNoticias.ListaCategoria.Add(categoria);
        }

        /// <summary>
        /// Metodo responsavel por carregar os dados de entretenimento.
        /// </summary>
        private void LoadDataEntretenimento(int idCategoria)
        {
            // Carrega as categorias de noticia em ordem a ser exibida e carrega seus itens.
            XML.ChannelVO channel = DataSourceRadio.ListaChannelVO.Where(item => item.Id == idCategoria).First<XML.ChannelVO>();
            List<XML.ItemVO> listaItem = channel.ListaItem.OrderByDescending(item => item.PubDate).Take(4).ToList();
            CategoriaVO categoria = new CategoriaVO() { NomeCategoria = channel.NomeChannel, ListaItem = null, CorLetraTitulo = ItemVO.CorLetraEntretenimento };
            categoria.ListaItem = new ObservableCollection<ItemVO>();

            foreach (XML.ItemVO item in listaItem)
            {
                ItemVO itemView = new ItemVO();
                itemView.Titulo = item.Title.ToUpper();
                itemView.SubTitulo = item.Title;
                itemView.IsList = false;
                itemView.ListaSubItem = null;
                itemView.IdChannel = channel.Id;
                itemView.Guid = item.Guid;
                itemView.Descricao = item.Description;
                itemView.StrConteudo = item.Content;
                itemView.ImagemFundo = new XML.ImageVO();

                // Define se tem imagem na noticia ou não
                if (item.ListaImage.Count.Equals(0))
                {
                    itemView.HasImagemIBahia = true;
                }
                else
                {
                    itemView.HasImagemIBahia = false;
                    itemView.ImagemFundo.Url = item.ListaImage.First().Url;

                    if (!string.IsNullOrEmpty(item.ListaImage.First().Title))
                    {
                        itemView.ImagemFundo.Title = item.ListaImage.First().Title;
                    }
                }

                itemView.ListaTituloSubItem = null;
                itemView.IsEventoCultural = false;
                itemView.CorStackPanelItem = ItemVO.CorLetraEntretenimento;

                categoria.ListaItem.Add(itemView);


            }

            DataSourceRadio.GrupoNoticias.ListaCategoria.Add(categoria);

        }



        public List<CategoriaVO> LoadDataTipoNoticia(int[] idEnumNoticias, string cor)
        {
            //IEnumerable<ItemVO> objItem = new ObservableCollection<ItemVO>();
            List<CategoriaVO> lstCategoria = new List<CategoriaVO>();
            ObservableCollection<ItemVO> lstItemVO = new ObservableCollection<ItemVO>();
            ItemVO objItemVO = new ItemVO();
      
         

            foreach (CategoriaVO item in DataSourceRadio.GrupoNoticias.ListaCategoria)
            {
                if (item.ListaItem.Where(i => idEnumNoticias.Contains(i.IdChannel)).ToList().Count() > 0)
                {
                    item.CorLetraTitulo = cor;

                    Constantes.strCor = cor;
                    foreach (var itemVO in item.ListaItem.Where(i => idEnumNoticias.Contains(i.IdChannel)).ToList())
                    {
                        itemVO.ImgCorMais = "ms-appx:///Assets/+.png";

                        lstItemVO.Add(itemVO);
                    }

                    item.ListaItem = new ObservableCollection<ItemVO>();

                    item.ListaItem = lstItemVO;

                    lstCategoria.Add(item);
                    lstItemVO = new ObservableCollection<ItemVO>();
                }
            }

            return lstCategoria.ToList();
        }
    }
}