using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RadioPlayer.Beans.ValueObject.ValueObject.Xml.AgendaCulturalXML
{
    /// <summary>
    /// Classe LinkSingleItemVO
    /// </summary>
    [KnownType(typeof(LinkSingleItemVO))]
    [DataContractAttribute]
    public class LinkSingleItemVO : BindableBaseVO
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
        /// Atríbuto guid.
        /// </summary>
        private int guid;

        /// <summary>
        /// Atríbuto pubDate.
        /// </summary>
        private string pubDate;

        /// <summary>
        /// Atríbuto preco.
        /// </summary>
        private ObservableCollection<string> preco;

        /// <summary>
        /// Atríbuto horario.
        /// </summary>
        private string horario;

        /// <summary>
        /// Atríbuto labelSessoes.
        /// </summary>
        private string labelSessoes;

        /// <summary>
        /// Atríbuto sessao.
        /// </summary>
        private ObservableCollection<SessaoVO> sessao;

        /// <summary>
        /// Atríbuto endereco
        /// </summary>
        private EnderecoVO endereco;

        /// <summary>
        /// Atríbuto thumb.
        /// </summary>
        private string thumb;

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkSingleItemVO"/> class.
        /// </summary>
        public LinkSingleItemVO()
        {
            this.preco = new ObservableCollection<string>();
            this.sessao = new ObservableCollection<SessaoVO>();
        }

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
        /// Gets or sets - propriedade Preco.
        /// </summary>
        public ObservableCollection<string> Preco
        {
            get { return this.preco; }
            set { this.SetProperty(ref this.preco, value); }
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
        /// Gets or sets - propriedade Sessao.
        /// </summary>
        public ObservableCollection<SessaoVO> Sessao
        {
            get { return this.sessao; }
            set { this.SetProperty(ref this.sessao, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Endereco.
        /// </summary>
        public EnderecoVO Endereco
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
