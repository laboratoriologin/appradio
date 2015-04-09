using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RadioPlayer.Beans.ValueObject.ValueObject.Xml.AgendaCulturalXML
{
    /// <summary>
    /// Classe SessaoVO
    /// </summary>
    [KnownType(typeof(SessaoVO))]
    [DataContractAttribute]
    public class SessaoVO : BindableBaseVO
    {
        /// <summary>
        /// Atríbuto horario.
        /// </summary>
        private string horario;

        /// <summary>
        /// Atríbuto guid.
        /// </summary>
        private int guid;

        /// <summary>
        /// Gets or sets - propriedade Horario.
        /// </summary>
        public string Horario
        {
            get { return this.horario; }
            set { this.SetProperty(ref this.horario, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Guid.
        /// </summary>
        public int Guid
        {
            get { return this.guid; }
            set { this.SetProperty(ref this.guid, value); }
        }
    }
}
