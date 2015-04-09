using RadioPlayer.Beans.ValueObject.ValueObject.Xml.NoticiaCorreioXML;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RadioPlayer.Beans.ValueObject.ValueObject.View.Principal
{
    /// <summary>
    /// Classe Value Object SessaoVO
    /// </summary>
    [KnownType(typeof(CategoriaVO))]
    [DataContractAttribute]
    public class CategoriaVO : BindableBaseVO
    {
        /// <summary>
        /// Atríbuto id da categoria.
        /// </summary>
        private int id;

        /// <summary>
        /// Atríbuto nomeCategoria.
        /// </summary>
        private string nomeCategoria;

        /// <summary>
        /// Atríbuto corLetraTitulo.
        /// </summary>
        private string corLetraTitulo;

        /// <summary>
        /// Atríbuto listaItem.
        /// </summary>
        private ObservableCollection<ItemCorreioVO> listaCorreioItem;


        /// <summary>
        /// Atríbuto listaItem.
        /// </summary>
        private ObservableCollection<ItemVO> listaItem;

        /// <summary>
        /// Gets or sets - propriedade Id.
        /// </summary>
        public int Id
        {
            get { return this.id; }
            set { this.SetProperty(ref this.id, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade NomeCategoria.
        /// </summary>
        public string NomeCategoria
        {
            get { return this.nomeCategoria; }
            set { this.SetProperty(ref this.nomeCategoria, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade CorLetraTitulo.
        /// </summary>
        public string CorLetraTitulo
        {
            get { return this.corLetraTitulo; }
            set { this.SetProperty(ref this.corLetraTitulo, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade ListaItem.
        /// </summary>
        public ObservableCollection<ItemVO> ListaItem
        {
            get { return this.listaItem; }
            set { this.SetProperty(ref this.listaItem, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade ListaItem.
        /// </summary>
        public ObservableCollection<ItemCorreioVO> ListaCorreioItem
        {
            get { return this.listaCorreioItem; }
            set { this.SetProperty(ref this.listaCorreioItem, value); }
        }
    }
}
