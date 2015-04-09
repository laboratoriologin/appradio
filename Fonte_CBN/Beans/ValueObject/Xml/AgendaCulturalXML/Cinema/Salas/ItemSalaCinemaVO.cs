using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RadioPlayer.Beans.ValueObject.ValueObject.Xml.AgendaCulturalXML.Salas
{
    [KnownType(typeof(ItemSalaCinemaVO))]
    [DataContractAttribute]
    public class ItemSalaCinemaVO : BindableBaseVO
    {
        /// <summary>
        /// Atríbuto title.
        /// </summary>
        private string title;

        /// <summary>
        /// Atríbuto tipo.
        /// </summary>
        private string tipo;

        /// <summary>
        /// Atríbuto description.
        /// </summary>
        private string description;

        /// <summary>
        /// Atríbuto link.
        /// </summary>
        private string link;

        /// <summary>
        /// Atríbuto linksingle.
        /// </summary>
        private LinkSingleItemVO linksingle;

        /// <summary>
        /// Atríbuto guid. 
        /// </summary>
        private int guid;

        /// <summary>
        /// Atríbuto pubDate.
        /// </summary>
        private string pubDate;

        /// <summary>
        /// Atríbuto horario.
        /// </summary>
        private string horario;

        /// <summary>
        /// Atríbuto labelSessoes.
        /// </summary>
        private string labelSessoes;

        /// <summary>
        /// Atríbuto endereco.
        /// </summary>
        private EnderecoSalaCinemaVO endereco;

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
        /// Gets or sets - propriedade Tipo.
        /// </summary>
        public string Tipo
        {
            get { return this.tipo; }
            set { this.SetProperty(ref this.tipo, value); }
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
        /// Gets or sets - propriedade PubDate.
        /// </summary>
        public string PubDate
        {
            get { return this.pubDate; }
            set { this.SetProperty(ref this.pubDate, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Horario.
        /// </summary>
        public string Horario
        {
            get { return this.horario; }
            set { this.SetProperty(ref this.horario, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade LabelSessoes.
        /// </summary>
        public string LabelSessoes
        {
            get { return this.labelSessoes; }
            set { this.SetProperty(ref this.labelSessoes, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Endereco.
        /// </summary>
        public EnderecoSalaCinemaVO Endereco
        {
            get { return this.endereco; }
            set { this.SetProperty(ref this.endereco, value); }
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
