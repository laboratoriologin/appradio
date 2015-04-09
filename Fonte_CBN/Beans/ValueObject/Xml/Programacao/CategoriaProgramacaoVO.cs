using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RadioPlayer.Beans.ValueObject.Xml.Programacao
{
    /// <summary>
    /// Classe Value Object CategoriaProgramacaoVO
    /// </summary> 
    [KnownType(typeof(CategoriaProgramacaoVO))]
    [DataContractAttribute]
    public class CategoriaProgramacaoVO : BindableBaseVO
    {
        /// <summary>
        /// Atríbuto guid 
        /// </summary>
        private int guid;

        /// <summary>
        /// Atríbuto title 
        /// </summary>
        private string title;

        /// <summary>
        /// Gets or sets - propriedade Guid 
        /// </summary>
        public int Guid
        {
            get { return this.guid; }
            set { this.SetProperty(ref this.guid, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Title 
        /// </summary>
        public string Title
        {
            get { return this.title; }
            set { this.SetProperty(ref this.title, value); }
        }
    }
}
