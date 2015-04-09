using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RadioPlayer.Beans.ValueObject.ValueObject.Xml.AgendaCulturalXML
{
    /// <summary>
    /// Classe de Endereço das Agendas Culturais Value Object 
    /// </summary>
    [KnownType(typeof(EnderecoVO))]
    [DataContractAttribute]
    public class EnderecoVO : BindableBaseVO
    {
        /// <summary>
        /// Atríbuto espaco.
        /// </summary>
        private string espaco;

        /// <summary>
        /// Atríbuto logradouro.
        /// </summary>
        private string logradouro;

        /// <summary>
        /// Atríbuto complemento.
        /// </summary>
        private string complemento;

        /// <summary>
        /// Atríbuto estabelecimento.
        /// </summary>
        private string estabelecimento;

        /// <summary>
        /// Atríbuto localidade.
        /// </summary>
        private string localidade;

        /// <summary>
        /// Gets or sets - propriedade Espaco.
        /// </summary>
        public string Espaco
        {
            get { return this.espaco; }
            set { this.SetProperty(ref this.espaco, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Logradouro.
        /// </summary>
        public string Logradouro
        {
            get { return this.logradouro; }
            set { this.SetProperty(ref this.logradouro, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Complemento.
        /// </summary>
        public string Complemento
        {
            get { return this.complemento; }
            set { this.SetProperty(ref this.complemento, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Estabelecimento.
        /// </summary>
        public string Estabelecimento
        {
            get { return this.estabelecimento; }
            set { this.SetProperty(ref this.estabelecimento, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Localidade.
        /// </summary>
        public string Localidade
        {
            get { return this.localidade; }
            set { this.SetProperty(ref this.localidade, value); }
        }
    }
}