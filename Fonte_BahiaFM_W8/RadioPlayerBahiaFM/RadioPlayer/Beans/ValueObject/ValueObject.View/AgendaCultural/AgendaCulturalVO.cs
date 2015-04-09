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
    /// Classe AgendaCulturalVO.
    /// Essa classe serve de base para todos os dados dos eventos de Agenda Cultural.
    /// </summary>
    [KnownType(typeof(AgendaCulturalVO))]
    [DataContractAttribute]
    public class AgendaCulturalVO : BindableBaseVO
    {
        /// <summary>
        /// Atríbuto TituloPagina. 
        /// Título da página de detalhamento de Evento (Cinema, Teatro, Artes e Exposições, Música).
        /// </summary>
        private string tituloPagina;

        /// <summary>
        /// Atríbuto TituloEvento. 
        /// Título específico do Evento (nome do filme, nome da peça, etc...).
        /// </summary>
        private string tituloEvento;

        /// <summary>
        /// Atríbuto TituloDetalhesEvento. 
        /// Título da área de detalhes do Evento.
        /// </summary>
        private string tituloDetalhesEvento;

        /// <summary>
        /// Atríbuto TituloCartazEvento. 
        /// Título da área de Evento onde mostra onde e quando aquele Evento estará em cartaz.
        /// </summary>
        private string tituloCartazEvento;

        /// <summary>
        /// Atríbuto ItemDetalheAgendaCultural. 
        /// Mais informações específicas sobre o Evento (No caso de filme, é sinopse).
        /// </summary>
        private ItemDetalheAgendaCulturalVO maisInfoEvento;

        /// <summary>
        /// Atríbuto ListaDetalhesEvento. 
        /// Lista de detalhes dos eventos.
        /// </summary>
        private ObservableCollection<ItemDetalheAgendaCulturalVO> listaDetalhesEvento;


        /// <summary>
        /// Atríbuto ItemAgendaCulturalVO. 
        /// Mais informações específicas sobre o Evento (No caso de filme, é sinopse).
        /// </summary>
        private ObservableCollection<ItemAgendaCulturalVO> listaAgendaCultural;


        /// <summary>
        /// Gets or sets - propriedade ListaAgendaCultural
        /// </summary>
        public ObservableCollection<ItemAgendaCulturalVO> ListaAgendaCultural
        {
            get { return listaAgendaCultural; }
            set { listaAgendaCultural = value; }
        }

        /// <summary>
        /// Gets or sets - propriedade TituloPagina
        /// </summary>
        public string TituloPagina
        {
            get { return this.tituloPagina; }
            set { this.SetProperty(ref this.tituloPagina, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade TituloEvento
        /// </summary>
        public string TituloEvento
        {
            get { return this.tituloEvento; }
            set { this.SetProperty(ref this.tituloEvento, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade TituloDetalhesEvento
        /// </summary>
        public string TituloDetalhesEvento
        {
            get { return this.tituloDetalhesEvento; }
            set { this.SetProperty(ref this.tituloDetalhesEvento, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade TituloCartazEvento
        /// </summary>
        public string TituloCartazEvento
        {
            get { return this.tituloCartazEvento; }
            set { this.SetProperty(ref this.tituloCartazEvento, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade MaisInfoEvento
        /// </summary>
        public ItemDetalheAgendaCulturalVO MaisInfoEvento
        {
            get { return this.maisInfoEvento; }
            set { this.SetProperty(ref this.maisInfoEvento, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade ListaDetalhesEvento
        /// </summary>
        public ObservableCollection<ItemDetalheAgendaCulturalVO> ListaDetalhesEvento
        {
            get { return this.listaDetalhesEvento; }
            set { this.SetProperty(ref this.listaDetalhesEvento, value); }
        }
    }
}
