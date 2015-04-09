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
    /// Classe CinemaVO.
    /// Essa classe serve de base para todos os dados dos eventos de Agenda Cultural.
    /// </summary>
    [KnownType(typeof(CinemaVO))]
    [DataContractAttribute]
    public class CinemaVO : AgendaCulturalVO
    {
        /// <summary>
        /// Atríbuto video
        /// </summary>
        private string video;

        /// <summary>
        /// Atríbuto listaOndeEstaPassando.
        /// </summary>
        private ObservableCollection<ItemOndeEstaPassandoFilmeAgendaCulturalVO> listaOndeEstaPassando;

        /// <summary>
        /// Gets or sets - propriedade Video.
        /// </summary>
        public string Video
        {
            get { return this.video; }
            set { this.SetProperty(ref this.video, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade ListaOndeEstaPassando
        /// </summary>
        public ObservableCollection<ItemOndeEstaPassandoFilmeAgendaCulturalVO> ListaOndeEstaPassando
        {
            get { return this.listaOndeEstaPassando; }
            set { this.SetProperty(ref this.listaOndeEstaPassando, value); }
        }
    }
}
