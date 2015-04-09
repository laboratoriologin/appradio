using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RadioPlayer.Beans.ValueObject.Xml.ProgramacaoRadioXML
{

    /// <summary>
    /// Classe Value Object HorarioProgramacaoRadioXMLVO.
    /// </summary>
    [KnownType(typeof(HorarioProgramacaoRadioXMLVO))]
    [DataContractAttribute]
    public class HorarioProgramacaoRadioXMLVO : BindableBaseVO
    {
        /// <summary>
        /// Atríbuto diaDaSemana.
        /// </summary>
        private string diaDaSemana;


        /// <summary>
        /// Atríbuto horarioInicio.
        /// </summary>
        private string horarioInicio;

        /// <summary>
        /// Atríbuto horarioFinal.
        /// </summary>
        private string horarioFinal;
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemPodCastVO"/> class.
        /// </summary>
        public HorarioProgramacaoRadioXMLVO()
        {
        }



        /// <summary>
        /// Gets or sets - propriedade DiaDaSemana.
        /// </summary>
        public string DiaDaSemana
        {
            get { return diaDaSemana; }
            set { this.SetProperty(ref this.diaDaSemana, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade horarioInicio.
        /// </summary>
        public string HorarioInicio
        {
            get { return this.horarioInicio; }
            set { this.SetProperty(ref this.horarioInicio, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade horarioFinal.
        /// </summary>
        public string HorarioFinal
        {
            get { return this.horarioFinal; }
            set { this.SetProperty(ref this.horarioFinal, value); }
        }

    }
}
