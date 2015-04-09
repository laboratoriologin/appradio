using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RadioPlayer.Beans.ValueObject.ValueObject.Xml.AgendaCulturalXML.Cinema.Cartaz
{
    /// <summary>
    /// Classe Value Object 
    /// </summary>
    [KnownType(typeof(HorarioVO))]
    [DataContractAttribute]
    public class HorarioVO : BindableBaseVO
    {
        /// <summary>
        /// Atríbuto tipo.
        /// </summary>
        private ObservableCollection<string> tipo;

        /// <summary>
        /// Atríbuto sessao3D.
        /// </summary>
        private ObservableCollection<string> sessao3D;

        /// <summary>
        /// Atríbuto hora.
        /// </summary>
        private ObservableCollection<string> hora;

        /// <summary>
        /// Atríbuto observacoes.
        /// </summary>
        private ObservableCollection<string> observacoes;

        /// <summary>
        /// Initializes a new instance of the <see cref="HorarioVO"/> class.
        /// </summary>
        public HorarioVO()
        {
            this.hora = new ObservableCollection<string>();
            this.observacoes = new ObservableCollection<string>();
            this.sessao3D = new ObservableCollection<string>();
            this.tipo = new ObservableCollection<string>();
        }

        /// <summary>
        /// Gets or sets - propriedade tipo.
        /// </summary>
        public ObservableCollection<string> Tipo
        {
            get { return this.tipo; }
            set { this.SetProperty(ref this.tipo, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Sessao3D.
        /// </summary>
        public ObservableCollection<string> Sessao3D
        {
            get { return this.sessao3D; }
            set { this.SetProperty(ref this.sessao3D, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Hora.
        /// </summary>
        public ObservableCollection<string> Hora
        {
            get { return this.hora; }
            set { this.SetProperty(ref this.hora, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Observacoes.
        /// </summary>
        public ObservableCollection<string> Observacoes
        {
            get { return this.observacoes; }
            set { this.SetProperty(ref this.observacoes, value); }
        }
    }
}
