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
        private ObservableCollection<HorarioVO> horario;

        /// <summary>
        /// Gets or sets - propriedade Horario.
        /// </summary>
        public ObservableCollection<HorarioVO> Horario
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
