using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RadioPlayer.Beans.ValueObject.ValueObject.Xml.PodCastXML
{
    /// <summary>
    /// Classe Value Object ItemPodCastVO.
    /// </summary>
    [KnownType(typeof(ItemPodCastVO))]
    [DataContractAttribute]
    public class ItemPodCastVO : BindableBaseVO
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
        private string guid;

        /// <summary>
        /// Atríbuto pubDate.
        /// </summary>
        private string pubDate;

        /// <summary>
        /// Atríbuto listaCategoria.
        /// </summary>
        private ObservableCollection<CategoriaPodCastVO> listaCategoria;

        /// <summary>
        /// Atríbuto listaImage.
        /// </summary>
        private ObservableCollection<ImagePodCastVO> listaImage;

        /// <summary>
        /// Atríbuto itunes_duration
        /// </summary>
        private string itunes_duration;


        /// <summary>
        /// Atríbuto video.
        /// </summary>
        private string video;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemPodCastVO"/> class.
        /// </summary>
        public ItemPodCastVO()
        {
            this.listaCategoria = new ObservableCollection<CategoriaPodCastVO>();
            this.listaImage = new ObservableCollection<ImagePodCastVO>();
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
        public string Guid
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
        public ObservableCollection<CategoriaPodCastVO> ListaCategoria
        {
            get { return this.listaCategoria; }
            set { this.SetProperty(ref this.listaCategoria, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade ListaImage.
        /// </summary>
        public ObservableCollection<ImagePodCastVO> ListaImage
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


        /// <summary>
        /// Gets or sets - propriedade Itunes_duration.
        /// </summary>
        public string Itunes_duration
        {
            get { return itunes_duration; }
            set { this.SetProperty(ref this.itunes_duration, value); }
        }
    }
}
