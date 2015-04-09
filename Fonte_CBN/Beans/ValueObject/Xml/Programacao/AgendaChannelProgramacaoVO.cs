using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RadioPlayer.Beans.ValueObject.Xml.Programacao
{
    /// <summary>
    /// Classe Value Object AgendaChannelProgramacaoVO.
    /// </summary>
    [KnownType(typeof(AgendaChannelProgramacaoVO))]
    [DataContractAttribute]
    public class AgendaChannelProgramacaoVO : BindableBaseVO
    {
        /// <summary>
        /// Atríbuto dia
        /// </summary>
        private string dia;

        /// <summary>
        /// Atríbuto horarioInicio
        /// </summary>
        private string horarioInicio;

        /// <summary>
        /// Atríbuto horarioFinal
        /// </summary>
        private string horarioFinal;

    

        /// <summary>
        /// Atríbuto guid
        /// </summary>
        private string guid;

        public string Guid
        {
            get { return guid; }
            set { this.SetProperty(ref this.guid, value); }
        }



        /// <summary>
        /// Gets or sets - propriedade Dia.
        /// </summary>
        public string Dia
        {
            get { return this.dia; }
            set { this.SetProperty(ref this.dia, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade HorarioInicio.
        /// </summary>
        public string HorarioInicio
        {
            get { return this.horarioInicio; }
            set { this.SetProperty(ref this.horarioInicio, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade HorarioFinal.
        /// </summary>
        public string HorarioFinal
        {
            get { return this.horarioFinal; }
            set { this.SetProperty(ref this.horarioFinal, value); }
        }


    }
}
