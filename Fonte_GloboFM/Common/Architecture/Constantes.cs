using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.Storage;

namespace RadioPlayer.Common.Architecture
{

    using RadioPlayer.Beans.ValueObject.ValueObject.View.Principal;
    using System.Collections.ObjectModel;
    using Windows.ApplicationModel.Resources;
    using Windows.Storage;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Classe de Constantes do Sistema
    /// </summary>
    public class Constantes
    {
        /// <summary>
        /// Atríbuto objNoticiaCorreio.
        /// </summary>
        public static int  intFlagBack ;


        /// <summary>
        /// Atríbuto objNoticiaCorreio.
        /// </summary>
        public static List<CategoriaVO> objNoticiaCorreio = new List<CategoriaVO>();

        /// <summary>
        /// Atríbuto objNoticiaPromocao.
        /// </summary>
        public static List<CategoriaVO> objNoticiaPromocao = new List<CategoriaVO>();

        /// <summary>
        /// Atríbuto objNoticiaPodcast.
        /// </summary>
        public static List<CategoriaVO> objNoticiaPodcast = new List<CategoriaVO>();
        
        /// <summary>
        /// Atríbuto  Pause Branco.
        /// </summary>
        public static Grid grdAgenda ;

        // <summary>
        /// Atríbuto   ObjRadioItem.
        /// </summary>
        public static RadioItem objRadioItem;

        // <summary>
        /// Atríbuto int Estacao.
        /// </summary>
        public static int intEstacao;

        // <summary>
        /// Atríbuto str Estacao.
        /// </summary>
        public static string strEstacao = "3";

        /// <summary>
        /// Atríbuto  Pause Branco.
        /// </summary>
        public static string imgPauseBranco = @"Assets/pause.png";

        /// <summary>
        /// Atríbuto  Pause Azul.
        /// </summary>
        public static string imgPauseAzul = @"Assets/pause_azul.png";

        /// <summary>
        /// Atríbuto  Pause Flag.
        /// </summary>
        public static string imgPauseFlag = @"Assets/pause.png";

        /// <summary>
        /// Atríbuto  Play Branco.
        /// </summary>
        public static string imgPlayBranco = @"Assets/play.png";

        /// <summary>
        /// Atríbuto  Play Azul.
        /// </summary>
        public static string imgPlayAzul = @"Assets/play_azul.png";


        /// <summary>
        /// Atríbuto  Play Flag.
        /// </summary>
        public static string imgPlayFlag = @"Assets/play.png";

        /// <summary>
        /// Atríbuto  Favorito Desabilitado Branco.
        /// </summary>
        public static string imgFavoritoDesabilitadoBranco = @"Assets/favorito_desabilitado.png";

        /// <summary>
        /// Atríbuto  Favorito Desabilitado Azul.
        /// </summary>
        public static string imgFavoritoDesabilitadoAzul = @"Assets/favorito_desabilitado_azul.png";


        /// <summary>
        /// Atríbuto  FavoritoDesabilitado Flag.
        /// </summary>
        public static string imgFavoritoDesabilitadoFlag = @"Assets/favorito_desabilitado.png";

        /// <summary>
        /// Atríbuto  Favorito  Branco.
        /// </summary>
        public static string imgFavoritoBranco = @"Assets/favorito.png";

        /// <summary>
        /// Atríbuto  Favorito  Azul.
        /// </summary>
        public static string imgFavoritoAzul = @"Assets/favorito_azul.png";

        /// Atríbuto  Favorito Flag.
        /// </summary>
        public static string imgFavoritoFlag = @"Assets/favorito.png";


        /// <summary>
        /// Atríbuto  Retroceder  Branco.
        /// </summary>
        public static string imgRetrocederBranco = @"Assets/retroceder.png";

        /// <summary>
        /// Atríbuto  Retroceder  Azul.
        /// </summary>
        public static string imgRetrocederAzul = @"Assets/retroceder_azul.png";

        /// Atríbuto  Retroceder Flag.
        /// </summary>
        public static string imgRetrocederFlag = @"Assets/retroceder.png";

        public static string strURLBahiaFMSul = @"http://www.bahiafmsul.com.br/exportacao-promocoes-bahiafmsul/";

        public static string strURLGloboFM = @"http://www.gfm.com.br/exportacao-promocoes-gfm/";


        /// <summary>

        /// <summary>
        /// Atríbuto  Retroceder Desabilitado Branco.
        /// </summary>
        public static string imgRetrocederDesabilitadoBranco = @"Assets/retroceder_desabilitado.png";

        /// <summary>
        /// Atríbuto  Retroceder Desabilitado Azul.
        /// </summary>
        public static string imgRetrocederDesabilitadoAzul = @"Assets/retroceder_desabilitado_azul.png";

        /// Atríbuto  Retroceder Desabilitado Flag.
        /// </summary>
        public static string imgRetrocederDesabilitadoFlag = @"Assets/retroceder_desabilitado.png";

        /// <summary>
        /// Atríbuto  Avancar  Branco.
        /// </summary>
        public static string imgAvancarBranco = @"Assets/avancar.png";

        /// <summary>
        /// Atríbuto  Avancar  Azul.
        /// </summary>
        public static string imgAvancarAzul = @"Assets/avancar_azul.png";

        /// Atríbuto  avancar Flag.
        /// </summary>
        public static string imgAvancarFlag = @"Assets/avancar.png";

        /// <summary>
        /// Atríbuto  Avancar Desabilitado Branco.
        /// </summary>
        public static string imgAvancarDesabilitadoBranco = @"Assets/avancar_desabilitado.png";

        /// <summary>
        /// Atríbuto  Avancar Desabilitado Azul.
        /// </summary>
        public static string imgAvancarDesabilitadoAzul = @"Assets/avancar_desabilitado_azul.png";

        /// Atríbuto  Avancar Desabilitado Flag.
        /// </summary>
        public static string imgAvancarDesabilitadoFlag = @"Assets/avancar_desabilitado.png";

        /// <summary>
        /// Atríbuto  Cor.
        /// </summary>
        public static string strCor = "#FFFFFF";
        
        /// <summary>
        /// Atríbuto  urlSite.
        /// </summary>
        public const string URLSite = "http://www.ibahia.com.br";

        /// <summary>
        /// Atríbuto  progress.
        /// </summary>
        public const bool flagProgress = false;

        /// <summary>
        /// Atríbuto  flagPodCast.
        /// </summary>
        private static bool flagPodCast = false;

     

     


        /// <summary>
        /// Atríbuto taskName.
        /// </summary>
        public const string TaskName = "TileUpdaterIBahia";

        /// <summary>
        /// Atríbuto taskEntry. 
        /// </summary>
        public const string TaskEntry = "BackgroundTasks.TileUpdaterIBahia";

        /// <summary>
        /// Atríbuto estático do caminho local.
        /// </summary>
        private static string caminhoLocal = ApplicationData.Current.LocalFolder.Path + "/bdIbahia.xml";

        /// <summary>
        /// Atríbuto estático do resource de erros.
        /// </summary>
        private static ResourceLoader resourceErro = new ResourceLoader("ResourcesErro");

        /// <summary>
        /// Atríbuto estático do resource de erros.
        /// </summary>
        private static ResourceLoader resourceImagem = new ResourceLoader("ResourcesImagem");


        /// <summary>
        /// Atríbuto estático do resource de erros.
        /// </summary>
        private static ResourceLoader resourceBoot = new ResourceLoader("ResourcesBoot");

        /// <summary>
        /// 
        /// </summary>
        public static bool FlagPodCast
        {
            get { return Constantes.flagPodCast; }
            set { Constantes.flagPodCast = value; }
        }

       /// <summary>
        /// Enumeração de ImagemAgendaCultural
        /// </summary>
        public enum ImagemAgendaCultural
        {
            /// <summary>
            /// Atríbuto imageCinema
            /// </summary>
            imageCinema,

            /// <summary>
            /// Atríbuto imageTeatro
            /// </summary>
            imageTeatro,

            /// <summary>
            /// Atríbuto imageMusica
            /// </summary>
            imageMusica,

            /// <summary>
            /// Atríbuto imageExposicao
            /// </summary>
            imageExposicao
        }


        /// <summary>
        /// Enumeração de ImagemAgendaCultural
        /// </summary>
        public enum EnumNoticias
        {
            /// <summary>
            /// Atríbuto noticias
            /// </summary>
            noticias = 22,

            /// <summary>
            /// Atríbuto teatro
            /// </summary>
            teatro = 28,

            /// <summary>
            /// Atríbuto musica
            /// </summary>
            musica = 29,

            /// <summary>
            /// Atríbuto literatura
            /// </summary>
            literatura = 51,

            /// <summary>
            /// Atríbuto mundoRock
            /// </summary>
            mundoRock = 96,


            /// <summary>
            /// Atríbuto modaEstilo
            /// </summary>
            modaEstilo = 88,


            /// <summary>
            /// Atríbuto gastronomia
            /// </summary>
            gastronomia = 97,

            /// <summary>
            /// Atríbuto Salvador
            /// </summary>
            Salvador = 32,


            /// <summary>
            /// Atríbuto nemTeConto
            /// </summary>
            nemTeConto = 36,


            /// <summary>
            /// Atríbuto esporte
            /// </summary>
            esporte = 3,

            /// <summary>
            /// Atríbuto salvador
            /// </summary>
            salvador = 2,

            /// <summary>
            /// Atríbuto maisEsporte
            /// </summary>
            maisEsporte = 25,

            /// <summary>
            /// Atríbuto futebol
            /// </summary>
            futebol = 4,

            /// <summary>
            /// Atríbuto ecBahia
            /// </summary>
            ecBahia = 48,

            /// <summary>
            /// Atríbuto ecVitoria
            /// </summary>
            ecVitoria = 49,

            /// <summary>
            /// Atríbuto noticiaBahia
            /// </summary>
            noticiaBahia = 12,

            /// <summary>
            /// Atríbuto noticiaBrasil
            /// </summary>
            noticiaBrasil = 13,

            /// <summary>
            /// Atríbuto noticiaMundo
            /// </summary>
            noticiaMundo = 14,


            /// <summary>
            /// Atríbuto sustentabilidade
            /// </summary>
            sustentabilidade = 152,


            /// <summary>
            /// Atríbuto transito
            /// </summary>
            transito = 1,


            /// <summary>
            /// Atríbuto entretenimento
            /// </summary>
            entretenimento = 26,


            /// <summary>
            /// Atríbuto cinema
            /// </summary>
            cinema = 27


        }

        /// <summary>
        /// Gets or sets - propriedade CaminhoLocal 
        /// </summary>
        public static string CaminhoLocal
        {
            get { return Constantes.caminhoLocal; }
            set { Constantes.caminhoLocal = value; }
        }

        /// <summary>
        /// Gets or sets - propriedade ResourceErro
        /// </summary>
        public static ResourceLoader ResourceErro
        {
            get { return Constantes.resourceErro; }
            set { Constantes.resourceErro = value; }
        }

        /// <summary>
        /// Gets or sets - propriedade ResourceErro
        /// </summary>
        public static ResourceLoader ResourceImagem
        {
            get { return Constantes.resourceImagem; }
            set { Constantes.resourceImagem = value; }
        }


        /// <summary>
        /// Gets or sets - propriedade ResourceBoot
        /// </summary>
        public static ResourceLoader ResourceBoot
        {
            get { return Constantes.resourceBoot; }
            set { Constantes.resourceBoot = value; }
        }

      
    }
}
