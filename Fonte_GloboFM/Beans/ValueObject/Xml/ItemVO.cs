using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RadioPlayer.Beans.ValueObject.ValueObject.Xml
{
    /// <summary>
    /// Classe Value Object ItemVO.
    /// </summary>
    [KnownType(typeof(ItemVO))]
    [DataContractAttribute]
    public class ItemVO : BindableBaseVO
    {
        /// <summary>
        /// Atríbuto title.
        /// </summary>
        private string title;

        /// <summary>
        /// Atríbuto description.
        /// </summary>
        private string description;

        /// <summary>
        /// Atríbuto content.
        /// </summary>
        private string content;

        /// <summary>
        /// Atríbuto contentOld.
        /// </summary>
        private string contentOld;

        /// <summary>
        /// Atríbuto link.
        /// </summary>
        private string link;

        /// <summary>
        /// Atríbuto guid.
        /// </summary>
        private int guid;

        /// <summary>
        /// Atríbuto pubDate.
        /// </summary>
        private string pubDate;

        /// <summary>
        /// Atríbuto listaCategoria.
        /// </summary>
        private ObservableCollection<CategoriaVO> listaCategoria;

        /// <summary>
        /// Atríbuto listaImage.
        /// </summary>
        private ObservableCollection<ImageVO> listaImage;

        /// <summary>
        /// Atríbuto video.
        /// </summary>
        private string video;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemVO"/> class.
        /// </summary>
        public ItemVO()
        {
            this.listaCategoria = new ObservableCollection<CategoriaVO>();
            this.listaImage = new ObservableCollection<ImageVO>();
        }

        /// <summary>
        /// Gets or sets - propriedade title.
        /// </summary>
        public string Title
        {
            get { return this.title; }
            set { this.SetProperty(ref this.title, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade description.
        /// </summary>
        public string Description
        {
            get { return this.description; }
            set { this.SetProperty(ref this.description, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade content.
        /// </summary>
        public string Content
        {
            get { return this.content; }
            set { this.SetProperty(ref this.content, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade contentOld.
        /// </summary>
        public string ContentOld
        {
            get { return this.contentOld; }
            set { this.SetProperty(ref this.contentOld, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade link.
        /// </summary>
        public string Link
        {
            get { return this.link; }
            set { this.SetProperty(ref this.link, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade guid.
        /// </summary>
        public int Guid
        {
            get { return this.guid; }
            set { this.SetProperty(ref this.guid, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade PubDate.
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
        /// Gets or sets - propriedade ListaCategoria.
        /// </summary>
        public ObservableCollection<CategoriaVO> ListaCategoria
        {
            get { return this.listaCategoria; }
            set { this.SetProperty(ref this.listaCategoria, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade ListaImage.
        /// </summary>
        public ObservableCollection<ImageVO> ListaImage
        {
            get { return this.listaImage; }
            set { this.SetProperty(ref this.listaImage, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Video.
        /// </summary>
        public string Video
        {
            get { return this.video; }
            set { this.SetProperty(ref this.video, value); }
        }
    }
}
