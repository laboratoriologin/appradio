using RadioPlayer.Beans.ValueObject.ValueObject.Xml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RadioPlayer.Beans.ValueObject.ValueObject.View.Principal
{
    /// <summary>
    /// Classe Value Object ItemVO
    /// </summary>
    [KnownType(typeof(ItemVO))]
    [DataContractAttribute]
    public class ItemVO : BindableBaseVO
    {
        /// <summary>
        /// Atríbuto BgImagemNoticia.
        /// </summary>
        private static string bgdImagemNoticia = "Assets/noticia.png";

        /// <summary>
        /// Atríbuto BgImagemLista.
        /// </summary>
        private static string bgdImagemLista = "Assets/listaNoticia.png";

        /// <summary>
        /// Atríbuto BgImagemEntretenimento.
        /// </summary>
        private static string bgdImagemEntretenimento = "Assets/entretenimento.png";

        /// <summary>
        /// Atríbuto BgImagemEsportes.
        /// </summary>
        private static string bgdImagemEsportes = "Assets/esportes.png";

        /// <summary>
        /// Atríbuto CorLetraNoticia.
        /// </summary>
        private static string corLetraNoticia = "#FF142E";

        /// <summary>
        /// Atríbuto CorLetraNoticiaRadio.
        /// </summary>
        private static string corLetraNoticiaRadio = "#FFFFFF";

        /// <summary>
        /// Atríbuto CorLetraNoticiaRadio.
        /// </summary>
        private static string imgCorMais = "ms-appx:///Assets/+.png";


        /// <summary>
        /// Atríbuto corLetraNoticiaRadioBahiaFM.
        /// </summary>
        public static string corLetraNoticiaRadioBahiaFM = "#00417F";
      

        /// <summary>
        /// Atríbuto CorLetraAgendaCultural.
        /// </summary>
        private static string corLetraAgendaCultural = "#FF142E";

        /// <summary>
        /// Atríbuto CorLetraEntretenimento.
        /// </summary>
        private static string corLetraEntretenimento = "#FFFFFF";

        /// <summary>
        /// Atríbuto CorLetraEsportes.
        /// </summary>
        private static string corLetraEsportes = "#406700";

        
        /// <summary>
        /// Atríbuto StrConteudo.
        /// </summary>
        private string strConteudo;

        /// <summary>
        /// Atríbuto para o tipo da classe.
        /// </summary>
        private object typeClass;

        /// <summary>
        /// Atríbuto identifivador do item.
        /// </summary>
        private int guid;

        /// <summary>
        /// Atríbuto identifivador do channel.
        /// </summary>
        private int idChannel;

        /// <summary>
        /// Atríbuto titulo.
        /// </summary>
        private string titulo;

        /// <summary>
        /// Atríbuto link.
        /// </summary>
        private string link;

        /// <summary>
        /// Atríbuto subTitulo.
        /// </summary>
        private string subTitulo;

        /// <summary>
        /// Atríbuto ImagemFundo.
        /// </summary>
        private ImageVO imagemFundo;

        /// <summary>
        /// Atríbuto hasImagemIBahia.
        /// </summary>
        private bool hasImagemIBahia;

        /// <summary>
        /// Atríbuto hasPodCast.
        /// </summary>
        private bool hasPodCast = false;

        /// <summary>
        /// Atríbuto hasImagemIBahia.
        /// </summary>
        private bool hasGridImagemIBahia;

        /// <summary>
        /// Atríbuto isList.
        /// </summary>
        private bool isList;

        /// <summary>
        /// Atríbuto isEventoCultural.
        /// </summary>
        private bool isEventoCultural;


        /// <summary>
        /// Atríbuto hasProgramacaoMusical.
        /// </summary>
        private bool hasProgramacaoMusical;

            /// <summary>
        /// Atríbuto imgblocoAgendaCultural.
        /// </summary>
        private string imgblocoAgendaCultural;

        /// <summary>
        /// Atríbuto baseUri.
        /// </summary>
        private Uri baseUri = new Uri("ms-appx:///");

        /// <summary>
        /// Atríbuto listaSubItem.
        /// </summary>
        private ObservableCollection<SubItemVO> listaSubItem;

        /// <summary>
        /// Atríbuto listaTituloSubItem.
        /// </summary>
        private string listaTituloSubItem;

        /// <summary>
        /// Atríbuto corStackPanelItem.
        /// </summary>
        private string corStackPanelItem;

        /// <summary>
        /// Atríbuto Descricao.
        /// </summary>
        private string descricao;

      


        /// <summary>
        /// Atríbuto hasGridPromocao.
        /// </summary>
        private bool hasGridPromocao;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemVO"/> class.
        /// </summary>
        public ItemVO()
        {
            this.listaSubItem = new ObservableCollection<SubItemVO>();
        }

        public string Descricao
        {
            get { return descricao; }
            set { this.SetProperty(ref this.descricao, value); }
        }
        public string CorLetraNoticiaRadio
        {
            get { return corLetraNoticiaRadio; }
            set { this.SetProperty(ref corLetraNoticiaRadio, value); }
        }

        public string ImgCorMais
        {
            get { return imgCorMais; }
            set { this.SetProperty(ref imgCorMais, value); }
        }

        public string CorLetraNoticiaRadioBahiaFM
        {
            get { return corLetraNoticiaRadioBahiaFM; }
            set { this.SetProperty(ref corLetraNoticiaRadioBahiaFM, value); }
        }
        /// <summary>
        /// Gets or sets - propriedade StrConteudo.
        /// </summary>
        public string StrConteudo
        {
            get { return strConteudo; }
            set { this.SetProperty(ref this.strConteudo, value); }
        }


        /// <summary>
        /// Gets or sets - propriedade link.
        /// </summary>
        public string Link
        {
            get { return link; }
            set { this.SetProperty(ref this.link, value); }
        }

        /// <summary>
        /// Gets - propriedade BgImagemNoticia.
        /// </summary>
        public static string BgImagemNoticia
        {
            get { return bgdImagemNoticia; }
        }

        /// <summary>
        /// Gets - propriedade BgImagemLista.
        /// </summary>
        public static string BgImagemLista
        {
            get { return bgdImagemLista; }
        }

        /// <summary>
        /// Gets - propriedade BgImagemEntretenimento.
        /// </summary>
        public static string BgImagemEntretenimento
        {
            get { return bgdImagemEntretenimento; }
        }

        /// <summary>
        /// Gets - propriedade BgImagemEsportes.
        /// </summary>
        public static string BgImagemEsportes
        {
            get { return bgdImagemEsportes; }
        }

        /// <summary>
        /// Gets - propriedade CorLetraNoticia.
        /// </summary>
        public static string CorLetraNoticia
        {
            get { return corLetraNoticia; }
        }

        /// <summary>
        /// Gets - propriedade CorLetraAgendaCultural.
        /// </summary>
        public static string CorLetraAgendaCultural
        {
            get { return corLetraAgendaCultural; }
        }

        /// <summary>
        /// Gets - propriedade CorLetraEntretenimento.
        /// </summary>
        public static string CorLetraEntretenimento
        {
            get { return corLetraEntretenimento; }
        }

        /// <summary>
        /// Gets - propriedade CorLetraEsportes.
        /// </summary>
        public static string CorLetraEsportes
        {
            get { return corLetraEsportes; }
        }

        /// <summary>
        /// Gets or sets - propriedade GUID.
        /// </summary>
        public object TypeClass
        {
            get { return this.typeClass; }
            set { this.SetProperty(ref this.typeClass, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade GUID.
        /// </summary>
        public int Guid
        {
            get { return this.guid; }
            set { this.SetProperty(ref this.guid, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade IdChannel.
        /// </summary>
        public int IdChannel
        {
            get { return this.idChannel; }
            set { this.SetProperty(ref this.idChannel, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade SubTitulo.
        /// </summary>
        public string SubTitulo
        {
            get { return this.subTitulo; }
            set { this.SetProperty(ref this.subTitulo, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade SubTitulo.
        /// </summary>
        public string Titulo
        {
            get { return this.titulo; }
            set { this.SetProperty(ref this.titulo, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade ImagemFundo.
        /// </summary>
        public ImageVO ImagemFundo
        {
            get { return this.imagemFundo; }
            set { this.SetProperty(ref this.imagemFundo, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether HasImagemIBahia.
        /// </summary>
        public bool HasImagemIBahia
        {
            get { return this.hasImagemIBahia; }
            set { this.SetProperty(ref this.hasImagemIBahia, value); }
        }


        /// <summary>
        /// Gets or sets a value indicating whether HasPodCast.
        /// </summary>
        public bool HasPodCast
        {
            get { return this.hasPodCast; }
            set { this.SetProperty(ref this.hasPodCast, value); }
        }


        /// <summary>
        /// Gets or sets a value indicating whether HasImagemIBahia.
        /// </summary>
        public bool HasGridImagemIBahia
        {
            get { return this.hasGridImagemIBahia; }
            set { this.SetProperty(ref this.hasGridImagemIBahia, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether HasImagemIBahia.
        /// </summary>
        public bool HasGridPromocao
        {
            get { return this.hasGridPromocao; }
            set { this.SetProperty(ref this.hasGridPromocao, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether IsList.
        /// </summary>
        public bool IsList
        {
            get { return this.isList; }
            set { this.SetProperty(ref this.isList, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether IsEventoCultural.
        /// </summary>
        public bool IsEventoCultural
        {
            get { return this.isEventoCultural; }
            set { this.SetProperty(ref this.isEventoCultural, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade ListaSubItem.
        /// </summary>
        public ObservableCollection<SubItemVO> ListaSubItem
        {
            get { return this.listaSubItem; }
            set { this.SetProperty(ref this.listaSubItem, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade ListaTituloSubItem.
        /// </summary>
        public string ListaTituloSubItem
        {
            get { return this.listaTituloSubItem; }
            set { this.SetProperty(ref this.listaTituloSubItem, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade CorStackPanelItem.
        /// </summary>
        public string CorStackPanelItem
        {
            get { return this.corStackPanelItem; }
            set { this.SetProperty(ref this.corStackPanelItem, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade ImgblocoAgendaCultural.
        /// </summary>
        public string ImgblocoAgendaCultural
        {
            get { return this.imgblocoAgendaCultural; }
            set { this.SetProperty(ref this.imgblocoAgendaCultural, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade HasProgramacaoMusical.
        /// </summary>
        public bool HasProgramacaoMusical
        {
            get { return hasProgramacaoMusical; }
            set { this.SetProperty(ref this.hasProgramacaoMusical, value); }
        }
   
    }
}
