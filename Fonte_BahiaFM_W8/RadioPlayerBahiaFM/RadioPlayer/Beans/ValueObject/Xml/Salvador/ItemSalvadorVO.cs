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
    /// Classe Value Object ItemSalvadorVO.
    /// </summary>
    [KnownType(typeof(ItemSalvadorVO))]
    [DataContractAttribute]
    public class ItemSalvadorVO : BindableBaseVO
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
        private string question;

        /// <summary>
        /// Atríbuto link.
        /// </summary>
        private string link;

        /// <summary>
        /// Atríbuto guid.
        /// </summary>
        private string guid;

        /// <summary>
        /// Atríbuto dateRaffle.
        /// </summary>
        private string dateRaffle;

        /// <summary>
        /// Atríbuto listaCategoria.
        /// </summary>
        private ObservableCollection<CategoriaSalvadorVO> listaCategoria;

        /// <summary>
        /// Atríbuto listaImage.
        /// </summary>
        private ObservableCollection<ImageChannelSalvadorVO> listaImage;

        /// <summary>
        /// Atríbuto dateWithdrawal
        /// </summary>
        private string dateWithdrawal;

        /// <summary>
        /// Atríbuto terms
        /// </summary>
        private string terms;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemPodCastVO"/> class.
        /// </summary>
        public ItemSalvadorVO()
        {
            this.listaCategoria = new ObservableCollection<CategoriaSalvadorVO>();
            this.listaImage = new ObservableCollection<ImageChannelSalvadorVO>();
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
        /// Gets or sets - propriedade question.
        /// </summary>
        public string Question
        {
            get { return this.question; }
            set { this.SetProperty(ref this.question, value); }
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
        public string DateRaffle
        {
            get { return this.dateRaffle; }
            set { this.SetProperty(ref this.dateRaffle, value); }
        }



        /// <summary>
        /// Gets or sets - propriedade ListaCategoria.
        /// </summary>
        public ObservableCollection<CategoriaSalvadorVO> ListaCategoria
        {
            get { return this.listaCategoria; }
            set { this.SetProperty(ref this.listaCategoria, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade ListaImage.
        /// </summary>
        public ObservableCollection<ImageChannelSalvadorVO> ListaImage
        {
            get { return this.listaImage; }
            set { this.SetProperty(ref this.listaImage, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade dateWithdrawal.
        /// </summary>
        public string DateWithdrawal
        {
            get { return dateWithdrawal; }
            set { this.SetProperty(ref this.dateWithdrawal, value); }
        }


        /// <summary>
        /// Gets or sets - propriedade Terms.
        /// </summary>
        public string Terms
        {
            get { return terms; }
            set { this.SetProperty(ref this.terms, value); }
        }
    }
}
