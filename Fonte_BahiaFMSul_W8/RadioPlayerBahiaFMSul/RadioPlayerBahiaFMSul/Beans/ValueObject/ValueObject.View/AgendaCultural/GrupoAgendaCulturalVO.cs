using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioPlayer.Beans.ValueObject.ValueObject.View.AgendaCultural
{
    /// <summary>
    /// Classe Value Object GrupoAgendaCulturalCinema
    /// </summary>
    public class GrupoAgendaCulturalVO : BindableBaseVO
    {
        /// <summary>
        /// Atríbuto titulo
        /// </summary>
        private string titulo = string.Empty;

        /// <summary>
        /// Atríbuto listaItemAgendaCultural
        /// </summary>
        private ObservableCollection<ItemAgendaCulturalVO> listaItemAgendaCultural;

        /// <summary>
        /// Gets or sets - propriedade Titulo
        /// </summary>
        public string Titulo
        {
            get { return this.titulo; }
            set { this.SetProperty(ref this.titulo, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade ListaItemAgendaCultural
        /// </summary>
        public ObservableCollection<ItemAgendaCulturalVO> ListaItemAgendaCultural
        {
            get { return this.listaItemAgendaCultural; }
            set { this.SetProperty(ref this.listaItemAgendaCultural, value); }
        }
    }
}
