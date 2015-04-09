using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RadioPlayer.Beans.ValueObject.Xml.Salvador
{
    /// <summary>
    /// Classe Value Object ChannelSalvadorVO.
    /// </summary>
    [KnownType(typeof(ChannelSalvadorVO))]
    [DataContractAttribute]
    public class ChannelSalvadorVO : BindableBaseVO
    {
          #region Atríbutos


        /// <summary>
        /// Atríbuto está guid
        /// </summary>
        private string guid;

     /// <summary>
        /// Atríbuto está carregado
        /// </summary>
        private bool isLoaded;

        /// <summary>
        /// Atríbuto url
        /// </summary>
        private string url;

        /// <summary>
        /// Atríbuto id
        /// </summary>
        private int id;

        /// <summary>
        /// Atríbuto nomeChannel
        /// </summary>
        private string nomeChannel;

        /// <summary>
        /// Atríbuto Title
        /// </summary>
        private string title;

        /// <summary>
        /// Atríbuto description
        /// </summary>
        private string description;

        /// <summary>
        /// Atríbuto link
        /// </summary>
        private string link;

        /// <summary>
        /// Atríbuto content
        /// </summary>
        private ImageChannelSalvadorVO image;

        /// <summary>
        /// Atríbuto LastBuildDate
        /// </summary>
        private string lastBuildDate;


        /// <summary>
        /// Atríbuto LastBuildDate
        /// </summary>
        private string copyright;

      

        /// <summary>
        /// Atríbuto PubDate
        /// </summary>
        private string pubDate;

        /// <summary>
        /// Atríbuto Item
        /// </summary>
        private ObservableCollection<ItemSalvadorVO> listaItem;

        #endregion

        #region Construtor
        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelPodCastVO"/> class.
        /// </summary>
        public ChannelSalvadorVO()
        {
            this.isLoaded = false;
            this.listaItem = new ObservableCollection<ItemSalvadorVO>();
        }
        #endregion

        #region Propriedades



        /// <summary>
        /// Gets or sets a value indicating whether Guid
        /// </summary>
        public string Guid
        {
            get { return guid; }
            set { this.SetProperty(ref this.guid, value); }
        }


   

        /// <summary>
        /// Gets or sets a value indicating whether IsLoaded
        /// </summary>
        public bool IsLoaded
        {
            get { return this.isLoaded; }
            set { this.SetProperty(ref this.isLoaded, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Copyright
        /// </summary>
        public string Copyright
        {
            get { return copyright; }
            set { this.SetProperty(ref this.copyright, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade url
        /// </summary>
        public string Url
        {
            get { return this.url; }
            set { this.SetProperty(ref this.url, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade id
        /// </summary>
        public int Id
        {
            get { return this.id; }
            set { this.SetProperty(ref this.id, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade nomeChannel
        /// </summary>
        public string NomeChannel
        {
            get { return this.nomeChannel; }
            set { this.SetProperty(ref this.nomeChannel, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Title
        /// </summary>
        public string Title
        {
            get { return this.title; }
            set { this.SetProperty(ref this.title, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Description
        /// </summary>
        public string Description
        {
            get { return this.description; }
            set { this.SetProperty(ref this.description, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Image
        /// </summary>
        public ImageChannelSalvadorVO Image
        {
            get { return this.image; }
            set { this.SetProperty(ref this.image, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Link
        /// </summary>
        public string Link
        {
            get { return this.link; }
            set { this.SetProperty(ref this.link, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade LastBuildDate
        /// </summary>
        public string LastBuildDate
        {
            get { return this.lastBuildDate; }
            set { this.SetProperty(ref this.lastBuildDate, value); }
        }

        /// <summary>
        /// Gets - propriedade LastBuildDate no tipo DateTime.
        /// </summary>
        public DateTime LastBuildDateToDateTime
        {
            get { return Convert.ToDateTime(this.lastBuildDate); }
        }

        /// <summary>
        /// Gets or sets - propriedade PubDate
        /// </summary>
        public string PubDate
        {
            get { return this.pubDate; }
            set { this.SetProperty(ref this.pubDate, value); }
        }

        /// <summary>
        /// Gets - propriedade PubDate no tipo DateTime.
        /// </summary>
        public DateTime PubDateToDateTime
        {
            get { return Convert.ToDateTime(this.pubDate); }
        }

        /// <summary>
        /// Gets or sets - propriedade Item
        /// </summary>
        public ObservableCollection<ItemSalvadorVO> ListaItem
        {
            get { return this.listaItem; }
            set { this.SetProperty(ref this.listaItem, value); }
        }
        #endregion
    }
}
