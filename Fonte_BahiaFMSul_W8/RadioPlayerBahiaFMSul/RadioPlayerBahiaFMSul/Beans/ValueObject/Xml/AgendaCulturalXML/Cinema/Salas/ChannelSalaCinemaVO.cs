using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RadioPlayer.Beans.ValueObject.ValueObject.Xml.AgendaCulturalXML.Salas
{
    /// <summary>
    /// Classe Value Object Channel
    /// </summary>
    [KnownType(typeof(ChannelSalaCinemaVO))]
    [DataContractAttribute]
    public class ChannelSalaCinemaVO : BindableBaseVO
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
        /// Atríbuto link. 
        /// </summary>
        private string link;

        /// <summary>
        /// Atríbuto lastBuildDate.
        /// </summary>
        private string lastBuildDate;

        /// <summary>
        /// Atríbuto pubDate.
        /// </summary>
        private string pubDate;

        /// <summary>
        /// Atríbuto listaItem. 
        /// </summary>
        private ObservableCollection<CinemaVO> listaItem;

        /// <summary>
        /// Gets or sets - propriedade Title.
        /// </summary>
        public string Title
        {
            get { return this.title; }
            set { this.SetProperty(ref this.title, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Description.
        /// </summary>
        public string Description
        {
            get { return this.description; }
            set { this.SetProperty(ref this.description, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Link.
        /// </summary>
        public string Link
        {
            get { return this.link; }
            set { this.SetProperty(ref this.link, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade LastBuildDate.
        /// </summary>
        public string LastBuildDate
        {
            get { return this.lastBuildDate; }
            set { this.SetProperty(ref this.lastBuildDate, value); }
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
        /// Gets or sets - propriedade ListaItem.
        /// </summary>
        public ObservableCollection<CinemaVO> ListaItem
        {
            get { return this.listaItem; }
            set { this.SetProperty(ref this.listaItem, value); }
        }
    }
}
