using RadioPlayer.Beans.ValueObject.ValueObject.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RadioPlayer.Beans.ValueObject.ValueObject.View.Principal
{
     /// <summary>
    /// Classe Value Object SubItemVO
    /// </summary>
    [KnownType(typeof(SubItemVO))]
    [DataContractAttribute]
    public class SubItemVO : BindableBaseVO
    {
        /// <summary>
        /// Atríbuto para o tipo da classe.
        /// </summary>
        private object typeClass;

        /// <summary>
        /// Atríbuto identifivador do item.
        /// </summary>
        private int guid;

        /// <summary>
        /// Atríbuto identifivador do channel.
        /// </summary>
        private int idChannel;

        /// <summary>
        /// Atríbuto index.
        /// </summary>
        private int index;

        /// <summary>
        /// Atríbuto titulo.
        /// </summary>
        private string titulo;

        /// <summary>
        /// Atríbuto subTitulo.
        /// </summary>
        private string subTitulo;

        /// <summary>
        /// Atríbuto imagemFundoSubItem.
        /// </summary>
        private ImageVO imagemFundoSubItem;

        /// <summary>
        /// Gets or sets - propriedade GUID.
        /// </summary>
        public object TypeClass
        {
            get { return this.typeClass; }
            set { this.SetProperty(ref this.typeClass, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade GUID.
        /// </summary>
        public int Guid
        {
            get { return this.guid; }
            set { this.SetProperty(ref this.guid, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade IdChannel.
        /// </summary>
        public int IdChannel
        {
            get { return this.idChannel; }
            set { this.SetProperty(ref this.idChannel, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Titulo.
        /// </summary>
        public string Titulo
        {
            get { return this.titulo; }
            set { this.SetProperty(ref this.titulo, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade SubTitulo.
        /// </summary>
        public string SubTitulo
        {
            get { return this.subTitulo; }
            set { this.SetProperty(ref this.subTitulo, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade ImagemFundoSubItem.
        /// </summary>
        public ImageVO ImagemFundoSubItem
        {
            get { return this.imagemFundoSubItem; }
            set { this.SetProperty(ref this.imagemFundoSubItem, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Index.
        /// </summary>
        public int Index
        {
            get { return this.index; }
            set { this.SetProperty(ref this.index, value); }
        }
    }
}
