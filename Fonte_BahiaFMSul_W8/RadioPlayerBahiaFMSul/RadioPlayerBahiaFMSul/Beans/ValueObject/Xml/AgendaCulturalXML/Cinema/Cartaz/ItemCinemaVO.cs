using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RadioPlayer.Beans.ValueObject.ValueObject.Xml.AgendaCulturalXML.Cinema.Cartaz
{
    /// <summary>
    /// Classe Value Object 
    /// </summary>
    [KnownType(typeof(ItemCinemaVO))]
    [DataContractAttribute]
    public class ItemCinemaVO : BindableBaseVO
    {
        /// <summary>
        /// Atríbuto title.
        /// </summary>
        private string title;

        /// <summary>
        /// Atríbuto link.
        /// </summary>
        private string link;

        /// <summary>
        /// Atríbuto link linksingle.
        /// </summary>
        private string linkLinksingle;

        /// <summary>
        /// Atríbuto linksingle.
        /// </summary>
        private LinkSingleItemVO linksingle;

        /// <summary>
        /// Atríbuto guid.
        /// </summary>
        private int guid;

        /// <summary>
        /// Atríbuto thumb.
        /// </summary>
        private string thumb;

        /// <summary>
        /// Gets or sets - propriedade Title.
        /// </summary>
        public string Title
        {
            get { return this.title; }
            set { this.SetProperty(ref this.title, value); }
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
        /// Gets or sets - propriedade link Linksingle.
        /// </summary>
        public string LinkLinksingle
        {
            get { return this.linkLinksingle; }
            set { this.SetProperty(ref this.linkLinksingle, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Linksingle.
        /// </summary>
        public LinkSingleItemVO Linksingle
        {
            get { return this.linksingle; }
            set { this.SetProperty(ref this.linksingle, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Guid.
        /// </summary>
        public int Guid
        {
            get { return this.guid; }
            set { this.SetProperty(ref this.guid, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Thumb.
        /// </summary>
        public string Thumb
        {
            get { return this.thumb; }
            set { this.SetProperty(ref this.thumb, value); }
        }
    }
}
