
using RadioPlayer.Beans.ValueObject.ValueObject.Xml.AgendaCulturalXML.Cinema.Cartaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RadioPlayer.Beans.ValueObject.ValueObject.Xml.AgendaCulturalXML.Cinema.Cartaz
{
    /// <summary>
    /// Classe Value Object SessaoVO
    /// </summary>
    [KnownType(typeof(SessaoVO))]
    [DataContractAttribute]
    public class SessaoVO : BindableBaseVO
    {
        /// <summary>
        /// Atríbuto cinema.
        /// </summary>
        private string cinema;

        /// <summary>
        /// Atríbuto sala.
        /// </summary>
        private string sala;

        /// <summary>
        /// Atríbuto horario.
        /// </summary>
        private HorarioVO horario;

        /// <summary>
        /// Initializes a new instance of the <see cref="SessaoVO"/> class.
        /// </summary>
        public SessaoVO()
        {

        }

        /// <summary>
        /// Gets or sets - propriedade Horario.
        /// </summary>
        public HorarioVO Horario
        {
            get { return this.horario; }
            set { this.SetProperty(ref this.horario, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Cinema.
        /// </summary>
        public string Cinema
        {
            get { return this.cinema; }
            set { this.SetProperty(ref this.cinema, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Sala.
        /// </summary>
        public string Sala
        {
            get { return this.sala; }
            set { this.SetProperty(ref this.sala, value); }
        }
    }
}
