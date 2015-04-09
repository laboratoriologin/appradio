using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RadioPlayer.Beans.ValueObject.ValueObject.Xml.AgendaCulturalXML.Salas
{
    /// <summary>
    /// Classe Value Object 
    /// </summary>
    [KnownType(typeof(CinemaVO))]
    [DataContractAttribute]
    public class CinemaVO : BindableBaseVO
    {
        /// <summary>
        /// Atríbuto nome.
        /// </summary>
        private string nome;

        /// <summary>
        /// Atríbuto clubeCorreio.
        /// </summary>
        private string clubeCorreio;

        /// <summary>
        /// Atríbuto observacoes.
        /// </summary>
        private string observacoes;

        /// <summary>
        /// Atríbuto endereco.
        /// </summary>
        private string endereco;

        /// <summary>
        /// Atríbuto mapa.
        /// </summary>
        private string mapa;

        /// <summary>
        /// Atríbuto sessao.
        /// </summary>
        private ObservableCollection<SessaoVO> sessao;

        /// <summary>
        /// Gets or sets - propriedade Nome.
        /// </summary>
        public string Nome
        {
            get { return this.nome; }
            set { this.SetProperty(ref this.nome, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade ClubeCorreio.
        /// </summary>
        public string ClubeCorreio
        {
            get { return this.clubeCorreio; }
            set { this.SetProperty(ref this.clubeCorreio, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Observacoes.
        /// </summary>
        public string Observacoes
        {
            get { return this.observacoes; }
            set { this.SetProperty(ref this.observacoes, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Endereco.
        /// </summary>
        public string Endereco
        {
            get { return this.endereco; }
            set { this.SetProperty(ref this.endereco, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Mapa.
        /// </summary>
        public string Mapa
        {
            get { return this.mapa; }
            set { this.SetProperty(ref this.mapa, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Sessao.
        /// </summary>
        public ObservableCollection<SessaoVO> Sessao
        {
            get { return this.sessao; }
            set { this.SetProperty(ref this.sessao, value); }
        }
    }
}
