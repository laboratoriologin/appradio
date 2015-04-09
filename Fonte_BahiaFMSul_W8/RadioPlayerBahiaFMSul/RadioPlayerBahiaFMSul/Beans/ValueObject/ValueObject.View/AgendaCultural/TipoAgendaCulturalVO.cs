using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RadioPlayer.Beans.ValueObject.ValueObject.View.AgendaCultural
{
    /// <summary>
    /// Classe TipoAgendaCulturalVO.
    /// Essa classe serve de base para todos os dados dos eventos de Agenda Cultural.
    /// </summary>
    [KnownType(typeof(TipoAgendaCulturalVO))]
    [DataContractAttribute]
    public class TipoAgendaCulturalVO : AgendaCulturalVO
    {
        /// <summary>
        /// Atríbuto imagemEvento
        /// </summary>
        private string imagemEvento;

        /// <summary>
        /// Atríbuto listaCartazEvento
        /// </summary>
        private ObservableCollection<ItemEmCartazEventoAgendaCulturalVO> listaCartazEvento;

        /// <summary>
        /// Gets or sets - propriedade ImagemEvento.
        /// </summary>
        public string ImagemEvento
        {
            get { return this.imagemEvento; }
            set { this.SetProperty(ref this.imagemEvento, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade ListaCartazEvento.
        /// </summary>
        public ObservableCollection<ItemEmCartazEventoAgendaCulturalVO> ListaCartazEvento
        {
            get { return this.listaCartazEvento; }
            set { this.SetProperty(ref this.listaCartazEvento, value); }
        }
    }
}
