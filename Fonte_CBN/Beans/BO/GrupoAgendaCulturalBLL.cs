using RadioPlayer.Beans.ValueObject.ValueObject.View.AgendaCultural;
using RadioPlayer.Beans.ValueObject.ValueObject.Xml.AgendaCulturalXML.Cinema.Cartaz;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgendaCulturalXML = RadioPlayer.Beans.ValueObject.ValueObject.Xml.AgendaCulturalXML;

namespace RadioPlayer.Beans.BO
{
     /// <summary>
    /// Classe Bll GrupoAgendaCultural
    /// </summary>
    public class GrupoAgendaCulturalBLL
    {
        public GrupoAgendaCulturalBLL()
        {
            if(DataSourceRadio.GrupoAgendaCulturalVO.ListaItemAgendaCultural == null)
                DataSourceRadio.GrupoAgendaCulturalVO.ListaItemAgendaCultural = new ObservableCollection<ItemAgendaCulturalVO>();
        }

        /// <summary>
        /// Metodo responsavel por carregar todos os dados do VO XML no VO VIEW de acordo com
        /// opção da tela.
        /// </summary>
        /// <param name="opcao">
        /// Opcao que diz qual sera o tipo da tela.
        /// 1 - Musica
        /// 2 - Teatro
        /// 3 - Exposicao
        /// </param>
        public void LoadDataGrupoAgendaCultural(string opcao)
        {
            switch (opcao)
            {
                case "Shows":
                    this.LoadDataMusica();
                    break;
                case "Teatro":
                    this.LoadDataTeatro();
                    break;
                case "Exposições":
                    this.LoadDataExposicao();
                    break;
                case "Cinema":
                    this.LoadDataGrupoCinema();
                    break;
            }
        }

        /// <summary>
        /// Método de carregar a tela.
        /// </summary>
        public void LoadDataGrupoCinema()
        {
            //// Preenche a Categoria GrupoAgendaCulturalCinemaVO            
            DataSourceRadio.GrupoAgendaCulturalVO.Titulo = "Cinema";
            ChannelCinemaVO channelCinemaVO = DataSourceRadio.ListaChannelCinemaVO.Where(itemCinema => itemCinema.Id == "filmes_em_cartaz").First<ChannelCinemaVO>();

            //// Preenche o ListaItem do Agenda Cultural Cinema
            foreach (var itemCinema in channelCinemaVO.ListaItem.ToList<ItemCinemaVO>())
            {
                if (!DataSourceRadio.GrupoAgendaCulturalVO.ListaItemAgendaCultural.Any<ItemAgendaCulturalVO>(item => item.Guid.Equals(itemCinema.Guid)))
                {
                    ItemAgendaCulturalVO itemGrupoAgenda = new ItemAgendaCulturalVO();

                    itemGrupoAgenda.Guid = itemCinema.Guid;
                    itemGrupoAgenda.Nome = itemCinema.Title;
                    itemGrupoAgenda.Image = itemCinema.Thumb;
                    //itemGrupoAgenda.Genero = itemCinema.Linksingle.Genero;
                    //itemGrupoAgenda.FaixaEtaria = itemCinema.Linksingle.FaixaEtaria;
                    itemGrupoAgenda.IsCinema = true;
                    itemGrupoAgenda.IdTipoAgendaCultural = 0;

                    DataSourceRadio.GrupoAgendaCulturalVO.ListaItemAgendaCultural.Add(itemGrupoAgenda);
                }
            }
        }

        /// <summary>
        /// Método LoadDataExposicao
        /// </summary>
        private void LoadDataExposicao()
        {
            //// Preenche a Categoria GrupoAgendaCulturalCinemaVO
            DataSourceRadio.GrupoAgendaCulturalVO.Titulo = "Exposição";
            AgendaCulturalXML.ChannelAgendaCulturalVO agendaCulturalVO = DataSourceRadio.ListaChannelAgendaCulturalVO.Where(itemAgenda => itemAgenda.Id == 4).First<AgendaCulturalXML.ChannelAgendaCulturalVO>();
            var lstItemAgendaCulturalVO = agendaCulturalVO.ListaItem.GroupBy<AgendaCulturalXML.ItemAgendaCulturalVO, string>(itemGroupAgenda => itemGroupAgenda.Title);

            //// Preenche o ListaItem do Agenda Cultural EXPOSIÇÃO
            foreach (var item in lstItemAgendaCulturalVO)
            {
                ItemAgendaCulturalVO itemGrupoAgenda = new ItemAgendaCulturalVO();
                //// Preenche o ListaItem do Agenda Cultural EXPOSIÇÃO
                foreach (var itemAgenda in item)
                {
                    itemGrupoAgenda.Guid = itemAgenda.Guid;
                    itemGrupoAgenda.Nome = itemAgenda.Title;
                    itemGrupoAgenda.Image = itemAgenda.Thumb;
                    itemGrupoAgenda.Genero = itemAgenda.Endereco.Espaco;
                    itemGrupoAgenda.FaixaEtaria = string.Empty;
                    itemGrupoAgenda.IdTipoAgendaCultural = 4;
                }

                if (!DataSourceRadio.GrupoAgendaCulturalVO.ListaItemAgendaCultural.Any<ItemAgendaCulturalVO>(iacvo => iacvo.Nome.Equals(itemGrupoAgenda.Nome)))
                    DataSourceRadio.GrupoAgendaCulturalVO.ListaItemAgendaCultural.Add(itemGrupoAgenda);
            }            
        }

        /// <summary>
        /// Método LoadDataTeatro
        /// </summary>
        private void LoadDataTeatro()
        {
            //// Preenche a Categoria GrupoAgendaCulturalTeatroVO            
            DataSourceRadio.GrupoAgendaCulturalVO.Titulo = "Teatro";
            AgendaCulturalXML.ChannelAgendaCulturalVO agendaCulturalVO = DataSourceRadio.ListaChannelAgendaCulturalVO.Where(itemAgenda => itemAgenda.Id == 5).First<AgendaCulturalXML.ChannelAgendaCulturalVO>();
            var lstItemAgendaCulturalVO = agendaCulturalVO.ListaItem.GroupBy<AgendaCulturalXML.ItemAgendaCulturalVO, string>(itemGroupAgenda => itemGroupAgenda.Title);

            foreach (var item in lstItemAgendaCulturalVO)
            {
                ItemAgendaCulturalVO itemGrupoAgenda = new ItemAgendaCulturalVO();
                //// Preenche o ListaItem do Agenda Cultural Teatro
                foreach (var itemAgenda in item)
                {
                    itemGrupoAgenda.Guid = itemAgenda.Guid;
                    itemGrupoAgenda.Nome = itemAgenda.Title;
                    itemGrupoAgenda.Image = itemAgenda.Thumb;
                    itemGrupoAgenda.Genero = itemAgenda.Endereco.Espaco;
                    itemGrupoAgenda.FaixaEtaria = string.Empty;
                    itemGrupoAgenda.IdTipoAgendaCultural = 5;
                }

                if (!DataSourceRadio.GrupoAgendaCulturalVO.ListaItemAgendaCultural.Any<ItemAgendaCulturalVO>(iacvo => iacvo.Nome.Equals(itemGrupoAgenda.Nome)))
                    DataSourceRadio.GrupoAgendaCulturalVO.ListaItemAgendaCultural.Add(itemGrupoAgenda);                
            }
        }

        /// <summary>
        /// Método LoadDataMusica
        /// </summary>
        private void LoadDataMusica()
        {
            //// Preenche a Categoria GrupoAgendaCulturalMusicaVO 
            DataSourceRadio.GrupoAgendaCulturalVO.Titulo = "Shows";
            AgendaCulturalXML.ChannelAgendaCulturalVO agendaCulturalVO = DataSourceRadio.ListaChannelAgendaCulturalVO.Where(itemAgenda => itemAgenda.Id == 2).First<AgendaCulturalXML.ChannelAgendaCulturalVO>();
            var lstItemAgendaCulturalVO = agendaCulturalVO.ListaItem.GroupBy<AgendaCulturalXML.ItemAgendaCulturalVO, string>(itemGroupAgenda => itemGroupAgenda.Title);

            foreach (var item in lstItemAgendaCulturalVO)
            {
                ItemAgendaCulturalVO itemGrupoAgenda = new ItemAgendaCulturalVO();
                //// Preenche o ListaItem do Agenda Cultural Musica
                foreach (var itemAgenda in item)
                {
                    itemGrupoAgenda.Guid = itemAgenda.Guid;
                    itemGrupoAgenda.Nome = itemAgenda.Title;
                    itemGrupoAgenda.Image = itemAgenda.Thumb;
                    itemGrupoAgenda.Genero = itemAgenda.Endereco.Espaco;
                    itemGrupoAgenda.FaixaEtaria = string.Empty;
                    itemGrupoAgenda.IdTipoAgendaCultural = 2;
                }

                if (!DataSourceRadio.GrupoAgendaCulturalVO.ListaItemAgendaCultural.Any<ItemAgendaCulturalVO>(iacvo => iacvo.Nome.Equals(itemGrupoAgenda.Nome)))
                    DataSourceRadio.GrupoAgendaCulturalVO.ListaItemAgendaCultural.Add(itemGrupoAgenda);
            }
        }
    }
}
