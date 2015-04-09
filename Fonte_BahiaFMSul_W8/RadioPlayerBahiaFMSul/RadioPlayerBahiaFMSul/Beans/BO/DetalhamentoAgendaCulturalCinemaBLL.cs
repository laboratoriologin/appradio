using RadioPlayer.Beans.ValueObject.ValueObject.View.AgendaCultural;
using RadioPlayer.Beans.ValueObject.ValueObject.Xml.AgendaCulturalXML.Cinema.Cartaz;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioPlayer.Beans.BO
{
    /// <summary>
    /// Classe Detalhamento Agenda Cultural Cinema
    /// </summary>
    public class DetalhamentoAgendaCulturalCinemaBLL
    {
        /// <summary>
        /// Atributo itemAgendaCultural.
        /// </summary>
        private ItemCinemaVO itemCinemaVO;

        /// <summary>
        /// Gets or sets - propriedade Evento
        /// </summary>
        public ItemCinemaVO ItemCinemaVO
        {
            get { return this.itemCinemaVO; }
            set { this.itemCinemaVO = value; }
        }

        /// <summary>
        /// Método de carregar a tela.
        /// </summary>
        /// <param name="channelCinemaVO">canal do cinema</param>
        /// <param name="id">id do elemento</param>
        public void LoadDataDetalhamentoAgendaCulturalCinema(ItemCinemaVO itemCinemaVOSelect, int id)
        {
            // Seleciona o evento com o id passado dentro do channel de agenda cultural.
            this.itemCinemaVO = itemCinemaVOSelect;

            if (this.itemCinemaVO.Linksingle.Title.Length <= 60)
            {
                DataSourceRadio.CinemaVO.TituloEvento = this.itemCinemaVO.Linksingle.Title;
            }
            else
            {
                DataSourceRadio.CinemaVO.TituloEvento = this.itemCinemaVO.Linksingle.Title.Substring(0, 57) + "...";
            }

            DataSourceRadio.CinemaVO.TituloPagina = "CINEMA";
            DataSourceRadio.CinemaVO.TituloDetalhesEvento = "Detalhes do Filme";
            DataSourceRadio.CinemaVO.TituloCartazEvento = "Programação";
            DataSourceRadio.CinemaVO.Video = this.itemCinemaVO.Linksingle.Trailer.Substring(this.itemCinemaVO.Linksingle.Trailer.IndexOf('=') + 1);

            DataSourceRadio.CinemaVO.ListaDetalhesEvento = new ObservableCollection<ItemDetalheAgendaCulturalVO>();

            if (!string.IsNullOrEmpty(this.itemCinemaVO.Linksingle.Genero))
            {
                DataSourceRadio.CinemaVO.ListaDetalhesEvento.Add(new ItemDetalheAgendaCulturalVO { TituloItemDetalhe = "Gênero", ConteudoItemDetalhe = this.itemCinemaVO.Linksingle.Genero });
            }

            if (!string.IsNullOrEmpty(this.itemCinemaVO.Linksingle.FaixaEtaria))
            {
                DataSourceRadio.CinemaVO.ListaDetalhesEvento.Add(new ItemDetalheAgendaCulturalVO { TituloItemDetalhe = "Faixa Etária", ConteudoItemDetalhe = this.itemCinemaVO.Linksingle.FaixaEtaria });
            }

            if (!string.IsNullOrEmpty(this.itemCinemaVO.Linksingle.Direcao))
            {
                DataSourceRadio.CinemaVO.ListaDetalhesEvento.Add(new ItemDetalheAgendaCulturalVO { TituloItemDetalhe = "Direção", ConteudoItemDetalhe = this.itemCinemaVO.Linksingle.Direcao });
            }

            if (!string.IsNullOrEmpty(this.itemCinemaVO.Linksingle.Elenco))
            {
                DataSourceRadio.CinemaVO.ListaDetalhesEvento.Add(new ItemDetalheAgendaCulturalVO { TituloItemDetalhe = "Elenco", ConteudoItemDetalhe = this.itemCinemaVO.Linksingle.Elenco });
            }

            if (!string.IsNullOrEmpty(this.itemCinemaVO.Linksingle.AnoLancamento))
            {
                DataSourceRadio.CinemaVO.ListaDetalhesEvento.Add(new ItemDetalheAgendaCulturalVO { TituloItemDetalhe = "Ano de Lançamento", ConteudoItemDetalhe = this.itemCinemaVO.Linksingle.AnoLancamento });
            }

            if (!string.IsNullOrEmpty(this.itemCinemaVO.Linksingle.Sinopse))
            {
                DataSourceRadio.CinemaVO.MaisInfoEvento = new ItemDetalheAgendaCulturalVO { TituloItemDetalhe = "Sinopse", ConteudoItemDetalhe = this.itemCinemaVO.Linksingle.Sinopse };
            }

            DataSourceRadio.CinemaVO.ListaOndeEstaPassando = new ObservableCollection<ItemOndeEstaPassandoFilmeAgendaCulturalVO>();
            ItemOndeEstaPassandoFilmeAgendaCulturalVO itemOndeEstaPassandoFilmeAgendaCulturalVO = new ItemOndeEstaPassandoFilmeAgendaCulturalVO();

            foreach (var itemCinemaSessao in this.itemCinemaVO.Linksingle.Sessao)
            {
                itemOndeEstaPassandoFilmeAgendaCulturalVO.NomeLocal = itemCinemaSessao.Cinema;

               RadioPlayer.Beans.ValueObject.ValueObject.View.AgendaCultural.HorarioVO horarioVO = new RadioPlayer.Beans.ValueObject.ValueObject.View.AgendaCultural.HorarioVO();

                DataSourceRadio.CinemaVO.ListaOndeEstaPassando = new ObservableCollection<ItemOndeEstaPassandoFilmeAgendaCulturalVO>();

                for (int i = 0; i < this.itemCinemaVO.Linksingle.Sessao.Count; i++)
                {
                    StringBuilder lstHorarioVO = new StringBuilder();
                    StringBuilder lstObservacoes = new StringBuilder();

                    for (int j = 0; j < this.itemCinemaVO.Linksingle.Sessao[i].Horario.Hora.Count; j++)
                    {
                        if (j != 0)
                        {
                            lstHorarioVO.Append(" - " + this.itemCinemaVO.Linksingle.Sessao[i].Horario.Hora[j]);
                        }
                        else
                        {
                            lstHorarioVO.Append(this.itemCinemaVO.Linksingle.Sessao[i].Horario.Hora[j]);
                        }
                    }

                    for (int j = 0; j < this.itemCinemaVO.Linksingle.Sessao[i].Horario.Observacoes.Count; j++)
                    {
                        lstObservacoes.Append(this.itemCinemaVO.Linksingle.Sessao[i].Horario.Observacoes[j] + "\n");
                    }

                    DataSourceRadio.CinemaVO.ListaOndeEstaPassando.Add(new ItemOndeEstaPassandoFilmeAgendaCulturalVO { NomeLocal = this.itemCinemaVO.Linksingle.Sessao[i].Cinema + " - " + this.itemCinemaVO.Linksingle.Sessao[i].Sala, Horarios = lstHorarioVO.ToString(), Info = lstObservacoes.ToString() });
                }
            }
        }
    }
}
