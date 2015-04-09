using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RadioPlayer.Beans.ValueObject.ValueObject.View.AgendaCultural
{
    /// <summary>
    /// Classe Value Object ItemAgendaCulturalVO 
    /// </summary>
    [KnownType(typeof(ItemAgendaCulturalVO))]
    [DataContractAttribute]
    public class ItemAgendaCulturalVO : BindableBaseVO
    {
        /// <summary>
        /// Atríbuto id
        /// </summary>
        private int guid;

        /// <summary>
        /// Atríbuto Image.
        /// </summary>
        private string image;

        /// <summary>
        /// Atríbuto Nome. 
        /// </summary>
        private string nome;

        /// <summary>
        /// Atríbuto Genero.
        /// </summary>
        private string genero;

        /// <summary>
        /// Atríbuto FaixaEtaria.
        /// </summary>
        private string faixaEtaria;

        /// <summary>
        /// Atríbuto IsCinema.
        /// </summary>
        private bool isCinema;

        /// <summary>
        /// Atríbuto IdTipoAgendaCultural.
        /// </summary>
        private int idTipoAgendaCultural;

        /// <summary>
        /// Atríbuto endereço.
        /// </summary>
        private string endereco;

        /// <summary>
        /// Gets or sets - propriedade Nome.
        /// </summary>
        public string Nome
        {
            get { return this.nome; }
            set { this.nome = value; }
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
        /// Gets or sets - propriedade Image.
        /// </summary>
        public string Image
        {
            get { return this.image; }
            set { this.SetProperty(ref this.image, value); }
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
        /// Gets or sets a value indicating whether IsCinema.
        /// </summary>
        public bool IsCinema
        {
            get { return this.isCinema; }
            set { this.SetProperty(ref this.isCinema, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade IdTipoAgendaCultural.
        /// </summary>
        public int IdTipoAgendaCultural
        {
            get { return this.idTipoAgendaCultural; }
            set { this.SetProperty(ref this.idTipoAgendaCultural, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Endereco.
        /// </summary>
        public string Endereco
        {
            get { return this.endereco; }
            set { this.endereco = value; }
        }
    }
}
