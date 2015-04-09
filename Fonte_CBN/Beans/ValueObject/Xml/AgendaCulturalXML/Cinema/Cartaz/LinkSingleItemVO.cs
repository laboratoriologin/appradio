using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RadioPlayer.Beans.ValueObject.ValueObject.Xml.AgendaCulturalXML.Cinema.Cartaz
{
    /// <summary>
    /// Classe Value Object Channel
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
        /// Atríbuto link.
        /// </summary>
        private string link;

        /// <summary>
        /// Atríbuto guid.
        /// </summary>
        private int guid;

        /// <summary>
        /// Atríbuto sessao.
        /// </summary>
        private ObservableCollection<SessaoVO> sessao;

        /// <summary>
        /// Atríbuto thumb.
        /// </summary>
        private string thumb;

        /// <summary>
        /// Atríbuto image.
        /// </summary>
        private string image;

        /// <summary>
        /// Atríbuto genero.
        /// </summary>
        private string genero;

        /// <summary>
        /// Atríbuto faixaEtaria.
        /// </summary>
        private string faixaEtaria;

        /// <summary>
        /// Atríbuto direcao.
        /// </summary>
        private string direcao;

        /// <summary>
        /// Atríbuto anoLancamento.
        /// </summary>
        private string anoLancamento;

        /// <summary>
        /// Atríbuto elenco.
        /// </summary>
        private string elenco;

        /// <summary>
        /// Atríbuto sinopse.
        /// </summary>
        private string sinopse;

        /// <summary>
        /// Atríbuto trailer.
        /// </summary>
        private string trailer;

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkSingleItemVO"/> class.
        /// </summary>
        public LinkSingleItemVO()
        {
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
        /// Gets or sets - propriedade Sessao.
        /// </summary>
        public ObservableCollection<SessaoVO> Sessao
        {
            get { return this.sessao; }
            set { this.SetProperty(ref this.sessao, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Thumb.
        /// </summary>
        public string Thumb
        {
            get { return this.thumb; }
            set { this.SetProperty(ref this.thumb, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Image.
        /// </summary>
        public string Image
        {
            get { return this.image; }
            set { this.SetProperty(ref this.image, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Genero.
        /// </summary>
        public string Genero
        {
            get { return this.genero; }
            set { this.SetProperty(ref this.genero, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade FaixaEtaria.
        /// </summary>
        public string FaixaEtaria
        {
            get { return this.faixaEtaria; }
            set { this.SetProperty(ref this.faixaEtaria, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Direcao.
        /// </summary>
        public string Direcao
        {
            get { return this.direcao; }
            set { this.SetProperty(ref this.direcao, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade AnoLancamento.
        /// </summary>
        public string AnoLancamento
        {
            get { return this.anoLancamento; }
            set { this.SetProperty(ref this.anoLancamento, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Elenco.
        /// </summary>
        public string Elenco
        {
            get { return this.elenco; }
            set { this.SetProperty(ref this.elenco, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Sinopse.
        /// </summary>
        public string Sinopse
        {
            get { return this.sinopse; }
            set { this.SetProperty(ref this.sinopse, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Trailer.
        /// </summary>
        public string Trailer
        {
            get { return this.trailer; }
            set { this.SetProperty(ref this.trailer, value); }
        }
    }
}
