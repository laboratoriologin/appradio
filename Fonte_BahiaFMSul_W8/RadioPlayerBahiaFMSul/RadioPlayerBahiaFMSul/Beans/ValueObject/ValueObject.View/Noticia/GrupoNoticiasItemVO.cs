using RadioPlayer.Beans.ValueObject.ValueObject.View.Principal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RadioPlayer.Beans.ValueObject.ValueObject.View.Noticia
{
    /// <summary>
    /// Classe Value Object GrupoNoticiasItemVO 
    /// </summary>
    [KnownType(typeof(GrupoNoticiasItemVO))]
    [DataContractAttribute]
    public class GrupoNoticiasItemVO : BindableBaseVO
    {
        /// <summary>
        /// Atríbuto Titulo.
        /// </summary>
        private string titulo;

        /// <summary>
        /// Atríbuto ListaCategoria.
        /// </summary>
        private ObservableCollection<CategoriaVO> listaCategoria = new ObservableCollection<CategoriaVO>();

        internal ObservableCollection<CategoriaVO> ListaCategoria
        {
            get { return this.listaCategoria; }
            set { this.SetProperty(ref this.listaCategoria, value); }
        }
        
        /// <summary>
        /// Gets or sets - propriedade Titulo.
        /// </summary>
        public string Titulo
        {
            get { return this.titulo; }
            set { this.SetProperty(ref this.titulo, value); }
        }
    }
}
