using RadioPlayer.Beans.ValueObject.ValueObject.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RadioPlayer.Beans.ValueObject.ValueObject.View.Noticia
{
    /// <summary>
    /// Classe Value Object DetalhamentoNoticiasVO 
    /// </summary>
    [KnownType(typeof(DetalhamentoNoticiasVO))]
    [DataContractAttribute]
    public class DetalhamentoNoticiasVO : BindableBaseVO
    {
        /// <summary>
        /// Atríbuto Channel.
        /// </summary>
        private string channel;

        /// <summary>
        /// Atríbuto Titulo.
        /// </summary>
        private string titulo;

        /// <summary>
        /// Atríbuto subTitulo.
        /// </summary>
        private string subTitulo;

        /// <summary>
        /// Atríbuto imagemNoticia.
        /// </summary>
        private ImageVO imagemNoticia;

        /// <summary>
        /// Atríbuto legendaImagemNoticia.
        /// </summary>
        private string legendaImagemNoticia;

        /// <summary>
        /// Atríbuto conteudo.
        /// </summary>
        private string conteudo;

        /// <summary>
        /// Atríbuto conteudo.
        /// </summary>
        private string conteudoSemHtml;

        /// <summary>
        /// Gets or sets - propriedade Titulo.
        /// </summary>
        public string Titulo
        {
            get { return this.titulo; }
            set { this.SetProperty(ref this.titulo, value); }
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
        /// Gets or sets - propriedade ImagemNoticia.
        /// </summary>
        public ImageVO ImagemNoticia
        {
            get { return this.imagemNoticia; }
            set { this.SetProperty(ref this.imagemNoticia, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade LegendaImagemNoticia.
        /// </summary>
        public string LegendaImagemNoticia
        {
            get { return this.legendaImagemNoticia; }
            set { this.SetProperty(ref this.legendaImagemNoticia, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Conteudo.
        /// </summary>
        public string Conteudo
        {
            get { return this.conteudo; }
            set { this.SetProperty(ref this.conteudo, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade conteudoSemHtml.
        /// </summary>
        public string ConteudoSemHtml
        {
            get { return this.conteudoSemHtml; }
            set { this.SetProperty(ref this.conteudoSemHtml, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Channel.
        /// </summary>
        public string Channel
        {
            get { return this.channel; }
            set { this.SetProperty(ref this.channel, value); }
        }
    }
}
