using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RadioPlayer.Beans.ValueObject.ValueObject.View.AgendaCultural
{
    /// <summary>
    /// Classe ItemDetalheAgendaCulturalVO.
    /// Essa classe serve de base para todos os dados dos eventos de Agenda Cultural.
    /// </summary>
    [KnownType(typeof(ItemEmCartazEventoAgendaCulturalVO))]
    [DataContractAttribute]
    public class ItemEmCartazEventoAgendaCulturalVO : BindableBaseVO
    {
        /// <summary>
        /// Atríbuto dataHorarioEvento
        /// </summary>
        private string dataHorarioEvento;

        /// <summary>
        /// Atríbuto nomeEvento
        /// </summary>
        private string nomeEvento;

        /// <summary>
        /// Gets or sets - propriedade DataHorarioEvento.
        /// </summary>
        public string DataHorarioEvento
        {
            get { return this.dataHorarioEvento; }
            set { this.SetProperty(ref this.dataHorarioEvento, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade NomeEvento.
        /// </summary>
        public string NomeEvento
        {
            get { return this.nomeEvento; }
            set { this.SetProperty(ref this.nomeEvento, value); }
        }
    }
}
