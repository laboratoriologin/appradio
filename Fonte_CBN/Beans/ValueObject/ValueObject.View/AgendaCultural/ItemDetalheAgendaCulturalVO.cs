using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace RadioPlayer.Beans.ValueObject.ValueObject.View.AgendaCultural
{
    /// <summary>
    /// Classe CinemaVO.
    /// Essa classe serve de base para todos os dados dos eventos de Agenda Cultural.
    /// </summary>
    [KnownType(typeof(ItemDetalheAgendaCulturalVO))]
    [DataContractAttribute]
    public class ItemDetalheAgendaCulturalVO : BindableBaseVO
    {
        /// <summary>
        /// Atríbuto tituloItemDetalhe
        /// </summary>
        private string tituloItemDetalhe;

        /// <summary>
        /// Atríbuto conteudoItemDetalhe
        /// </summary>
        private string conteudoItemDetalhe;

        /// <summary>
        /// Atríbuto endereco
        /// </summary>
        private string endereco;

        /// <summary>
        /// Gets or sets - propriedade Video.
        /// </summary>
        public string TituloItemDetalhe
        {
            get { return this.tituloItemDetalhe; }
            set { this.SetProperty(ref this.tituloItemDetalhe, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade ConteudoItemDetalhe.
        /// </summary>
        public string ConteudoItemDetalhe
        {
            get { return this.conteudoItemDetalhe; }
            set { this.SetProperty(ref this.conteudoItemDetalhe, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Endereco.
        /// </summary>
        public string Endereco
        {
            get { return this.endereco; }
            set { this.SetProperty(ref this.endereco, value); }
        }
    }
}
