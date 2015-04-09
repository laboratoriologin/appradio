using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace RadioPlayer.Beans.ValueObject.ValueObject.View.AgendaCultural
{
    /// <summary>
    /// Classe CinemaVO.
    /// Essa classe serve de base para todos os dados dos eventos de Agenda Cultural.
    /// </summary>
    [KnownType(typeof(ItemOndeEstaPassandoFilmeAgendaCulturalVO))]
    [DataContractAttribute]
    public class ItemOndeEstaPassandoFilmeAgendaCulturalVO : BindableBaseVO
    {
        /// <summary>
        /// Atríbuto nomeLocal
        /// </summary>
        private string nomeLocal;

        /// <summary>
        /// Atríbuto horarios
        /// </summary>
        private string horarios;

        /// <summary>
        /// Atríbuto info
        /// </summary>
        private string info;

        /// <summary>
        /// Gets or sets - propriedade NomeLocal.
        /// </summary>
        public string NomeLocal
        {
            get { return this.nomeLocal; }
            set { this.SetProperty(ref this.nomeLocal, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Horarios.
        /// </summary>
        public string Horarios
        {
            get { return this.horarios; }
            set { this.SetProperty(ref this.horarios, value); }
        }

        /// <summary>
        /// Gets - propriedade GetInfo.
        /// </summary>
        public Visibility GetInfo
        {
            get
            {
                if (string.IsNullOrEmpty(this.info))
                {
                    return Windows.UI.Xaml.Visibility.Collapsed;
                }
                else
                {
                    return Windows.UI.Xaml.Visibility.Visible;
                }
            }
        }

        /// <summary>
        /// Gets or sets - propriedade Info.
        /// </summary>
        public string Info
        {
            get { return this.info; }
            set { this.SetProperty(ref this.info, value); }
        }
    }
}
