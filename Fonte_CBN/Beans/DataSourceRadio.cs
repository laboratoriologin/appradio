using RadioPlayer.Beans.ValueObject;
using RadioPlayer.Beans.ValueObject.ValueObject.View.AgendaCultural;
using RadioPlayer.Beans.ValueObject.ValueObject.View.Noticia;
using RadioPlayer.Beans.ValueObject.ValueObject.View.Principal;
using RadioPlayer.Beans.ValueObject.ValueObject.Xml.AgendaCulturalXML.Cinema.Cartaz;
using RadioPlayer.Beans.ValueObject.ValueObject.Xml.NoticiaCorreioXML;
using RadioPlayer.Beans.ValueObject.ValueObject.Xml.PodCastXML;
using RadioPlayer.Beans.ValueObject.Xml.Salvador;
using RadioPlayer.Beans.ValueObject.Xml.Promocao;
using RadioPlayer.Model.Beans;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlVO = RadioPlayer.Beans.ValueObject.ValueObject.Xml;
using RadioPlayer.Beans.ValueObject.Xml.Programacao;
using RadioPlayer.Beans.ValueObject.Xml.ProgramacaoRadioXML;

namespace RadioPlayer.Beans
{
    /// <summary>
    /// Classe DataSourceRadio
    /// </summary>
    public static class DataSourceRadio
    {
        private static ColorVO cor = new ColorVO();

        private static BootThemeVO objBootThemeVO = new BootThemeVO();

        private static RadioItem objRadioItem = new RadioItem();

        /// <summary>
        /// Atributo CurrentPage
        /// </summary>
        private static object currentPage;

        /// <summary>
        /// Atributo isUpdating
        /// </summary>
        private static string channelActive = "A";

        /// <summary>
        /// Lista de tarefas para o carregamento dos dados.
        /// </summary>
        private static List<Task> listaTarefaLoadDados = new List<Task>();

        /// <summary>
        /// Atributo isUpdating
        /// </summary>
        private static bool isUpdating = false;

        /// <summary>
        /// Atributo isUpdatingGrupoNoticiasItem
        /// </summary>
        private static bool isUpdatingGrupoNoticiasItem = false;

        /// <summary>
        /// Atributo isUpdatingAgendaCultural
        /// </summary>
        private static bool isUpdatingAgendaCultural = false;

        /// <summary>
        /// Atributo isUpdatingAgendaCulturalDetalhe
        /// </summary>
        private static bool isUpdatingAgendaCulturalDetalhe = false;


        /// <summary>
        /// Atríbuto listaChannelVO
        /// </summary>
        private static ObservableCollection<XmlVO.NoticiaCorreioXML.ChannelCorreioVO> listaChannelCorreioVO = new ObservableCollection<XmlVO.NoticiaCorreioXML.ChannelCorreioVO>();

        /// <summary>
        /// Atríbuto listaChannelVO
        /// </summary>
        private static ObservableCollection<ChannelPromocaoVO> listaChannelPromocaoVO = new ObservableCollection<ChannelPromocaoVO>();

        // <summary>
        /// Atríbuto ChannelProgramacaoRadioXMLVO
        /// </summary>
        private static ObservableCollection<ChannelProgramacaoRadioXMLVO> listaChannelProgramacaoRadioVO = new ObservableCollection<ChannelProgramacaoRadioXMLVO>();


        private static ObservableCollection<ItemProgramacaoRadioXMLVO> listaItemProgramacaoRadio = new ObservableCollection<ItemProgramacaoRadioXMLVO>();


        public static ObservableCollection<ChannelProgramacaoRadioXMLVO> ListaChannelProgramacaoRadioVO
        {
            get { return DataSourceRadio.listaChannelProgramacaoRadioVO; }
            set { DataSourceRadio.listaChannelProgramacaoRadioVO = value; }
        }
        
        // <summary>
        /// Atríbuto listaChannelVO
        /// </summary>
        private static ObservableCollection<ChannelProgramacaoVO> listaChannelProgramacaoVO = new ObservableCollection<ChannelProgramacaoVO>();

        public static ObservableCollection<ChannelProgramacaoVO> ListaChannelProgramacaoVO
        {
            get { return DataSourceRadio.listaChannelProgramacaoVO; }
            set { DataSourceRadio.listaChannelProgramacaoVO = value; }
        }

        /// <summary>
        /// Atríbuto listaChannelVO
        /// </summary>
        private static ObservableCollection<XmlVO.ChannelVO> listaChannelAVO = new ObservableCollection<XmlVO.ChannelVO>();

        /// <summary>
        /// Atríbuto listaChannelVO
        /// </summary>
        private static ObservableCollection<XmlVO.ChannelVO> listaChannelBVO = new ObservableCollection<XmlVO.ChannelVO>();


        /// <summary>
        /// Atríbuto listaChannelVO
        /// </summary>
        private static ObservableCollection<ChannelPodCastVO> listaChannelPodCastAVO = new ObservableCollection<ChannelPodCastVO>();

        /// <summary>
        /// Atríbuto listaChannelVO
        /// </summary>
        private static ObservableCollection<ChannelPodCastVO> listaChannelPodCastBVO = new ObservableCollection<ChannelPodCastVO>();

        /// <summary>
        /// Atríbuto listaChannelVO
        /// </summary>
        private static ObservableCollection<ChannelSalvadorVO> listaChannelSalvadorAVO = new ObservableCollection<ChannelSalvadorVO>();

        /// <summary>
        /// Atríbuto listaChannelVO
        /// </summary>
        private static ObservableCollection<ChannelSalvadorVO> listaChannelSalvadorBVO = new ObservableCollection<ChannelSalvadorVO>();

        

        /// <summary>
        /// Atríbuto listaChannelAgendaCulturalVO
        /// </summary>
        private static ObservableCollection<XmlVO.AgendaCulturalXML.ChannelAgendaCulturalVO> listaChannelAgendaCulturalAVO = new ObservableCollection<XmlVO.AgendaCulturalXML.ChannelAgendaCulturalVO>();

        /// <summary>
        /// Atríbuto listaChannelAgendaCulturalVO
        /// </summary>
        private static ObservableCollection<XmlVO.AgendaCulturalXML.ChannelAgendaCulturalVO> listaChannelAgendaCulturalBVO = new ObservableCollection<XmlVO.AgendaCulturalXML.ChannelAgendaCulturalVO>();

        /// <summary>
        /// Atríbuto listaChannelChannelCinemaVO
        /// </summary>
        private static ObservableCollection<XmlVO.AgendaCulturalXML.Cinema.Cartaz.ChannelCinemaVO> listaChannelCinemaAVO = new ObservableCollection<XmlVO.AgendaCulturalXML.Cinema.Cartaz.ChannelCinemaVO>();

        /// <summary>
        /// Atríbuto listaChannelChannelCinemaVO
        /// </summary>
        private static ObservableCollection<XmlVO.AgendaCulturalXML.Cinema.Cartaz.ChannelCinemaVO> listaChannelCinemaBVO = new ObservableCollection<XmlVO.AgendaCulturalXML.Cinema.Cartaz.ChannelCinemaVO>();

        /// <summary>
        /// Atríbuto listaCategoria
        /// </summary>
        private static ObservableCollection<CategoriaVO> listaCategoria = new ObservableCollection<CategoriaVO>();

        /// <summary>
        /// Atríbuto listaCategoriaGrupoNoticia
        /// </summary>
        private static ObservableCollection<CategoriaVO> listaCategoriaGrupoNoticia = new ObservableCollection<CategoriaVO>();

        /// <summary>
        /// Atríbuto listaCategoriaGrupoNoticia
        /// </summary>
        private static GrupoAgendaCulturalVO grupoAgendaCulturalVO = new GrupoAgendaCulturalVO();

        /// <summary>
        /// Atríbuto ListaAgendaCulturalVO
        /// </summary>
        private static ObservableCollection<ItemAgendaCulturalVO> listaAgendaCulturalVO = new ObservableCollection<ItemAgendaCulturalVO>();

        /// <summary>
        /// Atríbuto detalhamentoNoticia
        /// </summary>
        private static DetalhamentoNoticiasVO detalhamentoNoticia = new DetalhamentoNoticiasVO();

        /// <summary>
        /// Atríbuto agendaCultural
        /// </summary>
        private static TipoAgendaCulturalVO agendaCultural = new TipoAgendaCulturalVO();

        /// <summary>
        /// Atríbuto CinemaVO
        /// </summary>
        private static CinemaVO cinemaVO = new CinemaVO();

        /// <summary>
        /// Atríbuto grupoNoticias
        /// </summary>
        private static GrupoNoticiasItemVO grupoNoticias = new GrupoNoticiasItemVO();

        /// <summary>
        /// Atríbuto ItemAgendaCulturalVO
        /// </summary>
        private static ItemAgendaCulturalVO itemAgendaCulturalVO = new ItemAgendaCulturalVO();

        /// <summary>
        /// Tarefa que atualiza a interface grafica do sistema.
        /// </summary>
        private static Task taskUIAgendaCultural;

        /// <summary>
        /// Atríbuto listaChannelPromocaoBahiaSulVO
        /// </summary>
        private static ObservableCollection<ChannelPromocaoVO> listaChannelPromocaoBahiaSulVO = new ObservableCollection<ChannelPromocaoVO>();


        /// <summary>
        /// Tarefa que atualiza a interface grafica do sistema.
        /// </summary>
        private static ItemPodCastVO itemPodCastVO = new ItemPodCastVO();

        private static ObservableCollection<CategoriaVO> lstCategoriaBahiaFM = new ObservableCollection<CategoriaVO>();

        private static ObservableCollection<CategoriaVO> lstCategoriaBahiaFMSul = new ObservableCollection<CategoriaVO>();
        private static ObservableCollection<CategoriaVO> lstCategoriaGloboFM = new ObservableCollection<CategoriaVO>();
        private static ObservableCollection<CategoriaVO> lstCategoriaCBN = new ObservableCollection<CategoriaVO>();

        /// <summary>
        /// 
        /// </summary>
        private static ObservableCollection<ChannelPromocaoVO> listaChannelPromocaoGloboFMVO = new ObservableCollection<ChannelPromocaoVO>();



        /// <summary>
        /// Atríbuto ListaItemPodCastVO
        /// </summary>
        private static ObservableCollection<ItemPodCastVO> listaItemPodCastVO = new ObservableCollection<ItemPodCastVO>();

        public static ObservableCollection<ItemPodCastVO> ListaItemPodCastVO
        {
            get { return DataSourceRadio.listaItemPodCastVO; }
            set { DataSourceRadio.listaItemPodCastVO = value; }
        }


        /// <summary>
        /// Atríbuto ListaItemProgramacao
        /// </summary>
        private static ObservableCollection<ItemProgramacaoVO> listaItemProgramacao = new ObservableCollection<ItemProgramacaoVO>();

      


        /// Atríbuto ListaItemPodCastVO
        /// </summary>
        private static ObservableCollection<ItemPromocaoVO> listaItemPromocaoVO = new ObservableCollection<ItemPromocaoVO>();
        private static ObservableCollection<ItemPromocaoVO> listaItemPromocaoBahiaSulVO = new ObservableCollection<ItemPromocaoVO>();
        private static ObservableCollection<ItemPromocaoVO> listaItemPromocaoGloboFMVO = new ObservableCollection<ItemPromocaoVO>();
        private static ObservableCollection<ItemCorreioVO> listaItemNoticiasCBNVO = new ObservableCollection<ItemCorreioVO>();
        private static ObservableCollection<ItemSalvadorVO> listaItemSalvador = new ObservableCollection<ItemSalvadorVO>();
 

        public static ObservableCollection<ItemSalvadorVO> ListaItemSalvador
        {
            get { return DataSourceRadio.listaItemSalvador; }
            set { DataSourceRadio.listaItemSalvador = value; }
        }
        
        public static ObservableCollection<ItemPromocaoVO> ListaItemPromocaoVO
        {
            get { return DataSourceRadio.listaItemPromocaoVO; }
            set { DataSourceRadio.listaItemPromocaoVO = value; }
        }


        public static ObservableCollection<ItemCorreioVO> ListaItemNoticiasCBNVO
        {
            get { return DataSourceRadio.listaItemNoticiasCBNVO; }
            set { DataSourceRadio.listaItemNoticiasCBNVO = value; }
        }

        public static ObservableCollection<ItemPromocaoVO> ListaItemPromocaoBahiaSulVO
        {
            get { return DataSourceRadio.listaItemPromocaoBahiaSulVO; }
            set { DataSourceRadio.listaItemPromocaoBahiaSulVO = value; }
        }

        public static ObservableCollection<ItemPromocaoVO> ListaItemPromocaoGloboFMVO
        {
            get { return DataSourceRadio.listaItemPromocaoGloboFMVO; }
            set { DataSourceRadio.listaItemPromocaoGloboFMVO = value; }
        }

        public static ObservableCollection<ItemProgramacaoVO> ListaItemProgramacao
        {
            get { return DataSourceRadio.listaItemProgramacao; }
            set { DataSourceRadio.listaItemProgramacao = value; }
        }


        public static ItemPodCastVO ItemPodCastVO
        {
            get { return DataSourceRadio.itemPodCastVO; }
            set { DataSourceRadio.itemPodCastVO = value; }
        }

        /// <summary>
        /// Gets or sets - propriedade Cor.
        /// </summary>
        public static ColorVO Cor
        {
            get { return DataSourceRadio.cor; }
            set { DataSourceRadio.cor = value; }
        }

        /// <summary>
        /// Gets or sets - propriedade CurrentPage.
        /// </summary>
        public static object CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; }
        }

        /// <summary>
        /// Gets or sets - propriedade ChannelActive.
        /// </summary>
        public static string ChannelActive
        {
            get { return channelActive; }
            set { channelActive = value; }
        }

        /// <summary>
        /// Gets or sets - propriedade ListaTarefaLoadDados.
        /// </summary>
        public static List<Task> ListaTarefaLoadDados
        {
            get { return listaTarefaLoadDados; }
            set { listaTarefaLoadDados = value; }
        }

        public static ObservableCollection<XmlVO.NoticiaCorreioXML.ChannelCorreioVO> ListaChannelCorreioVO
        {
            get
            {
                return DataSourceRadio.listaChannelCorreioVO;
            }
            set
            {
                DataSourceRadio.listaChannelCorreioVO = value;
            }
        }

        public static ObservableCollection<ChannelPromocaoVO> ListaChannelPromocaoVO
        {
            get
            {
                return DataSourceRadio.listaChannelPromocaoVO;
            }
            set
            {
                DataSourceRadio.listaChannelPromocaoVO = value;
            }
        }

        public static ObservableCollection<ChannelPromocaoVO> ListaChannelPromocaoBahiaSulVO
        {
            get
            {
                return DataSourceRadio.listaChannelPromocaoBahiaSulVO;
            }
            set
            {
                DataSourceRadio.listaChannelPromocaoBahiaSulVO = value;
            }
        }

        public static ObservableCollection<ChannelPromocaoVO> ListaChannelPromocaoGloboFMVO
        {
            get
            {
                return DataSourceRadio.listaChannelPromocaoGloboFMVO;
            }
            set
            {
                DataSourceRadio.listaChannelPromocaoGloboFMVO = value;
            }
        }

        /// <summary>
        /// Gets or sets - propriedade ListaChannelVO que está ativo.
        /// </summary>
        public static ObservableCollection<XmlVO.ChannelVO> ListaChannelVO
        {
            get
            {
                if (ChannelActive.Equals("A"))
                {
                    return listaChannelAVO;
                }
                else
                {
                    return listaChannelBVO;
                }
            }

            set
            {
                if (ChannelActive.Equals("A"))
                {
                    listaChannelAVO = value;
                }
                else
                {
                    listaChannelBVO = value;
                }
            }
        }


        /// <summary>
        /// Gets or sets - propriedade ListaChannelVO que está ativo.
        /// </summary>
        public static ObservableCollection<ChannelPodCastVO> ListaChannelPodCastVO
        {
            get
            {
                if (ChannelActive.Equals("A"))
                {
                    return listaChannelPodCastAVO;
                }
                else
                {
                    return listaChannelPodCastBVO;
                }
            }

            set
            {
                if (ChannelActive.Equals("A"))
                {
                    listaChannelPodCastAVO = value;
                }
                else
                {
                    listaChannelPodCastBVO = value;
                }
            }
        }


        /// <summary>
        /// Gets or sets - propriedade ListaChannelSalvadorVO que está ativo.
        /// </summary>
        public static ObservableCollection<ChannelSalvadorVO> ListaChannelSalvadorVO
        {
            get
            {
                if (ChannelActive.Equals("A"))
                {
                    return listaChannelSalvadorAVO;
                }
                else
                {
                    return listaChannelSalvadorBVO;
                }
            }

            set
            {
                if (ChannelActive.Equals("A"))
                {
                    listaChannelSalvadorAVO = value;
                }
                else
                {
                    listaChannelSalvadorBVO = value;
                }
            }
        }


        /// <summary>
        /// Gets or sets - propriedade ListaChannelVO que está inativo.
        /// </summary>
        public static ObservableCollection<XmlVO.ChannelVO> ListaChannelVOInactive
        {
            get
            {
                if (ChannelActive.Equals("A"))
                {
                    return listaChannelBVO;
                }
                else
                {
                    return listaChannelAVO;
                }
            }

            set
            {
                if (ChannelActive.Equals("A"))
                {
                    listaChannelBVO = value;
                }
                else
                {
                    listaChannelAVO = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets - propriedade ListaChannelAgendaCulturalVO que está ativo.
        /// </summary>
        public static ObservableCollection<XmlVO.AgendaCulturalXML.ChannelAgendaCulturalVO> ListaChannelAgendaCulturalVO
        {
            get
            {
                if (ChannelActive.Equals("A"))
                {
                    return listaChannelAgendaCulturalAVO;
                }
                else
                {
                    return listaChannelAgendaCulturalBVO;
                }
            }

            set
            {
                if (ChannelActive.Equals("A"))
                {
                    listaChannelAgendaCulturalAVO = value;
                }
                else
                {
                    listaChannelAgendaCulturalBVO = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets - propriedade ListaChannelAgendaCulturalVO que está ativo.
        /// </summary>
        public static ObservableCollection<XmlVO.AgendaCulturalXML.ChannelAgendaCulturalVO> ListaChannelAgendaCulturalVOInactive
        {
            get
            {
                if (ChannelActive.Equals("A"))
                {
                    return listaChannelAgendaCulturalBVO;
                }
                else
                {
                    return listaChannelAgendaCulturalAVO;
                }
            }

            set
            {
                if (ChannelActive.Equals("A"))
                {
                    listaChannelAgendaCulturalBVO = value;
                }
                else
                {
                    listaChannelAgendaCulturalAVO = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets - propriedade ListaChannelCinemaCartazVO que está ativo.
        /// </summary>
        public static ObservableCollection<ChannelCinemaVO> ListaChannelCinemaVO
        {
            get
            {
                if (ChannelActive.Equals("A"))
                {
                    return listaChannelCinemaAVO;
                }
                else
                {
                    return listaChannelCinemaBVO;
                }
            }

            set
            {
                if (ChannelActive.Equals("A"))
                {
                    listaChannelCinemaAVO = value;
                }
                else
                {
                    listaChannelCinemaBVO = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets - propriedade ListaChannelCinemaCartazVO que está Inativo.
        /// </summary>
        public static ObservableCollection<ChannelCinemaVO> ListaChannelCinemaVOInactive
        {
            get
            {
                if (ChannelActive.Equals("A"))
                {
                    return listaChannelCinemaBVO;
                }
                else
                {
                    return listaChannelCinemaAVO;
                }
            }

            set
            {
                if (ChannelActive.Equals("A"))
                {
                    listaChannelCinemaBVO = value;
                }
                else
                {
                    listaChannelCinemaAVO = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets - propriedade ListaCategoria.
        /// </summary>
        public static ObservableCollection<CategoriaVO> ListaCategoria
        {
            get { return listaCategoria; }
            set { listaCategoria = value; }
        }

        /// <summary>
        /// Gets or sets - propriedade ListaCategoriaGrupoNoticia.
        /// </summary>
        public static ObservableCollection<CategoriaVO> ListaCategoriaGrupoNoticia
        {
            get { return listaCategoriaGrupoNoticia; }
            set { listaCategoriaGrupoNoticia = value; }
        }

        /// <summary>
        /// Gets or sets - propriedade ListaAgendaCulturalVO.
        /// </summary>
        public static ObservableCollection<ItemAgendaCulturalVO> ListaAgendaCulturalVO
        {
            get { return listaAgendaCulturalVO; }
            set { listaAgendaCulturalVO = value; }
        }

        /// <summary>
        /// Gets or sets - propriedade DetalhamentoNoticia.
        /// </summary>
        public static DetalhamentoNoticiasVO DetalhamentoNoticia
        {
            get { return detalhamentoNoticia; }
            set { detalhamentoNoticia = value; }
        }

        /// <summary>
        /// Gets or sets - propriedade AgendaCultural.
        /// </summary>
        public static TipoAgendaCulturalVO AgendaCultural
        {
            get { return agendaCultural; }
            set { agendaCultural = value; }
        }

        /// <summary>
        /// Gets or sets - propriedade CinemaVO.
        /// </summary>
        public static CinemaVO CinemaVO
        {
            get { return DataSourceRadio.cinemaVO; }
            set { DataSourceRadio.cinemaVO = value; }
        }

        /// <summary>
        /// Gets or sets - propriedade GrupoNoticias.
        /// </summary>
        public static GrupoNoticiasItemVO GrupoNoticias
        {
            get { return grupoNoticias; }
            set { grupoNoticias = value; }
        }

        /// <summary>
        /// Gets or sets - propriedade ItemAgendaCulturalVO.
        /// </summary>
        public static ItemAgendaCulturalVO ItemAgendaCultural
        {
            get { return itemAgendaCulturalVO; }
            set { itemAgendaCulturalVO = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether IsUpdating.
        /// </summary>
        public static bool IsUpdating
        {
            get { return DataSourceRadio.isUpdating; }
            set { DataSourceRadio.isUpdating = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether isUpdatingGrupoNoticiasItem.
        /// </summary>
        public static bool IsUpdatingGrupoNoticiasItem
        {
            get { return DataSourceRadio.isUpdatingGrupoNoticiasItem; }
            set { DataSourceRadio.isUpdatingGrupoNoticiasItem = value; }
        }



        /// <summary>
        /// Gets or sets a value indicating whether isUpdatingGrupoNoticiasItem.
        /// </summary>
        public static bool IsUpdatingAgendaCultural
        {
            get { return DataSourceRadio.isUpdatingAgendaCultural; }
            set { DataSourceRadio.isUpdatingAgendaCultural = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether isUpdatingGrupoNoticiasItemDetalhe.
        /// </summary>
        public static bool IsUpdatingAgendaCulturalDetalhe
        {
            get { return DataSourceRadio.isUpdatingAgendaCulturalDetalhe; }
            set { DataSourceRadio.isUpdatingAgendaCulturalDetalhe = value; }
        }

        /// <summary>
        /// Gets or sets - propriedade ListaCategoriaAgendaCultural.
        /// </summary>
        public static GrupoAgendaCulturalVO GrupoAgendaCulturalVO
        {
            get { return DataSourceRadio.grupoAgendaCulturalVO; }
            set { DataSourceRadio.grupoAgendaCulturalVO = value; }
        }

        /// <summary>
        /// Método para trocar o channel Active do sistema.
        /// </summary>
        public static void ChangeChannelActive()
        {
            if (ChannelActive.Equals("A"))
            {
                ChannelActive = "B";
            }
            else
            {
                ChannelActive = "A";
            }
        }

        /// <summary>
        /// Gets or sets - propriedade ListaCategoriaAgendaCultural.
        /// </summary>
        public static Task TaskUIAgendaCultural
        {
            get { return DataSourceRadio.taskUIAgendaCultural; }
            set { DataSourceRadio.taskUIAgendaCultural = value; }
        }


        /// <summary>
        /// Gets or sets - propriedade ObjBootThemeVO.
        /// </summary>
        public static BootThemeVO ObjBootThemeVO
        {
            get { return DataSourceRadio.objBootThemeVO; }
            set { DataSourceRadio.objBootThemeVO = value; }
        }

        /// <summary>
        /// Gets or sets - propriedade LstCategoriaBahiaFMSul.
        /// </summary>
        public static ObservableCollection<CategoriaVO> LstCategoriaBahiaFMSul
        {
            get { return DataSourceRadio.lstCategoriaBahiaFMSul; }
            set { DataSourceRadio.lstCategoriaBahiaFMSul = value; }
        }

        /// <summary>
        /// Gets or sets - propriedade LstCategoriaGloboFM.
        /// </summary>
        public static ObservableCollection<CategoriaVO> LstCategoriaGloboFM
        {
            get { return DataSourceRadio.lstCategoriaGloboFM; }
            set { DataSourceRadio.lstCategoriaGloboFM = value; }
        }

        /// <summary>
        /// Gets or sets - propriedade LstCategoriaCBN.
        /// </summary>
        public static ObservableCollection<CategoriaVO> LstCategoriaCBN
        {
            get { return DataSourceRadio.lstCategoriaCBN; }
            set { DataSourceRadio.lstCategoriaCBN = value; }
        }


        /// <summary>
        /// Gets or sets - propriedade LstCategoriaBahiaFM.
        /// </summary>
        public static ObservableCollection<CategoriaVO> LstCategoriaBahiaFM
        {
            get { return DataSourceRadio.lstCategoriaBahiaFM; }
            set { DataSourceRadio.lstCategoriaBahiaFM = value; }
        }

        public static ObservableCollection<ItemProgramacaoRadioXMLVO> ListaItemProgramacaoRadio
        {
            get { return DataSourceRadio.listaItemProgramacaoRadio; }
            set { DataSourceRadio.listaItemProgramacaoRadio = value; }
        }


    }
}

