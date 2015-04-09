using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioPlayer.Beans.ValueObject.ValueObject.View.AgendaCultural
{
    /// <summary>
    /// Classe de horários.
    /// </summary>
    public class HorarioVO : BindableBaseVO
    {
        /// <summary>
        /// Atríbuto tipo.
        /// </summary>
        private string tipo;

        /// <summary>
        /// Atríbuto sessao3D.
        /// </summary>
        private string sessao3D;

        /// <summary>
        /// Atríbuto hora.
        /// </summary>
        private string hora;

        /// <summary>
        /// Atríbuto observacoes.
        /// </summary>
        private string observacoes;

        /// <summary>
        /// Gets or sets - propriedade tipo.
        /// </summary>
        public string Tipo
        {
            get { return this.tipo; }
            set { this.SetProperty(ref this.tipo, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Sessao3D.
        /// </summary>
        public string Sessao3D
        {
            get { return this.sessao3D; }
            set { this.SetProperty(ref this.sessao3D, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Hora.
        /// </summary>
        public string Hora
        {
            get { return this.hora; }
            set { this.SetProperty(ref this.hora, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Observacoes.
        /// </summary>
        public string Observacoes
        {
            get { return this.observacoes; }
            set { this.SetProperty(ref this.observacoes, value); }
        }
    }
}
