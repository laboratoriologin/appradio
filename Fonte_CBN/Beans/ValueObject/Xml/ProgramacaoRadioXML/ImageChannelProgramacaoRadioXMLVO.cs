using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RadioPlayer.Beans.ValueObject.Xml.ProgramacaoRadioXML
{
    /// <summary>
    /// Classe Value Object ImageChannelProgramacaoRadioXMLVO.
    /// </summary>
    [KnownType(typeof(ImageChannelProgramacaoRadioXMLVO))]
    [DataContractAttribute]
    public class ImageChannelProgramacaoRadioXMLVO : BindableBaseVO
    {
        /// <summary>
        /// Atríbuto url
        /// </summary>
        private string url;

      

     
        /// <summary>
        /// Atríbuto description
        /// </summary>
        private string guid;

        public string Guid
        {
            get { return guid; }
            set { this.SetProperty(ref this.guid, value); }
        }



        /// <summary>
        /// Gets or sets - propriedade Url.
        /// </summary>
        public string Url
        {
            get { return this.url; }
            set { this.SetProperty(ref this.url, value); }
        }

       }
}
