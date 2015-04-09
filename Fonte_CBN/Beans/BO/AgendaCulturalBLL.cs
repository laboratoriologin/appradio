//-----------------------------------------------------------------------
// <copyright file="AgendaCulturalBLL.cs" company="Login Informática">
//     Copyright (c) Login Informática. All rights reserved.
// </copyright>
// <author> Ricardo Carneiro</author>
//-----------------------------------------------------------------------

namespace RadioPlayer.Beans.BO
{

    using RadioPlayer.Beans.ValueObject.ValueObject.View.AgendaCultural;
    using RadioPlayer.Common.Architecture;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using View = RadioPlayer.Beans.ValueObject.ValueObject.View.Principal;
    using Xml = RadioPlayer.Beans.ValueObject.ValueObject.Xml.AgendaCulturalXML;

    /// <summary>
    /// Classe AgendaCulturalBLL.
    /// </summary>
    public class AgendaCulturalBLL
    {
        /// <summary>
        /// Atributo itemAgendaCultural.
        /// </summary>
        private Xml.ItemAgendaCulturalVO itemAgendaCultural;


        /// <summary>
        /// Atributo objCategoriaVO.
        /// </summary>
        private View.CategoriaVO objCategoriaVO = new View.CategoriaVO();

        /// <summary>
        /// Atributo lstCategoriaVO.
        /// </summary>
        private List<View.CategoriaVO> lstCategoriaVO = new List<View.CategoriaVO>();

        /// <summary>
        /// Gets or sets - propriedade Evento
        /// </summary>
        public Xml.ItemAgendaCulturalVO ItemAgendaCultural
        {
            get { return this.itemAgendaCultural; }
            set { this.itemAgendaCultural = value; }
        }

        /// <summary>
        /// Método montar grid da agenda cultural.
        /// </summary>
        /// <param name="CorLetra">Cor da letra</param>
        /// <returns>Lista entidade categoria</returns>
        public List<View.CategoriaVO> GridAgendaCultural(string CorLetra)
        {
            //// Cinema
            this.objCategoriaVO = new View.CategoriaVO();
            this.objCategoriaVO.NomeCategoria = "Cinema";
            this.objCategoriaVO.CorLetraTitulo = CorLetra;
            this.objCategoriaVO.ListaItem = new ObservableCollection<View.ItemVO>();

            this.lstCategoriaVO.Add(this.objCategoriaVO);

            //// Teatro

            this.objCategoriaVO = new View.CategoriaVO();
            this.objCategoriaVO.NomeCategoria = "Teatro";
            this.objCategoriaVO.CorLetraTitulo = CorLetra;
            this.objCategoriaVO.ListaItem = new ObservableCollection<View.ItemVO>();

            this.lstCategoriaVO.Add(this.objCategoriaVO);

            //// Música

            this.objCategoriaVO = new View.CategoriaVO();
            this.objCategoriaVO.NomeCategoria = "Música";
            this.objCategoriaVO.CorLetraTitulo = CorLetra;
            this.objCategoriaVO.ListaItem = new ObservableCollection<View.ItemVO>();

            this.lstCategoriaVO.Add(this.objCategoriaVO);

            //// Exposição

            this.objCategoriaVO = new View.CategoriaVO();
            this.objCategoriaVO.NomeCategoria = "Exposição";
            this.objCategoriaVO.CorLetraTitulo = CorLetra;
            this.objCategoriaVO.ListaItem = new ObservableCollection<View.ItemVO>();

            this.lstCategoriaVO.Add(this.objCategoriaVO);

            return this.lstCategoriaVO;
        }
        
        /// <summary>
        /// Metodo responsavel por carregar todos os dados do VO XML no VO VIEW.
        /// </summary>
        /// <param name="channelAgendaCultural">Objeto channel de agenda cultural</param>
        /// <param name="id">Id da noticia que será mostrada na tela</param>
        public void LoadDataAgendaCultural(Xml.ChannelAgendaCulturalVO channelAgendaCultural, int id)
        {
            // Seleciona o evento com o id passado dentro do channel de agenda cultural.
            this.itemAgendaCultural = channelAgendaCultural.ListaItem.Where(item => item.Guid == id).First<Xml.ItemAgendaCulturalVO>();

            // Preenche VO do view de acordo com o objeto encontrado na busca anterior.
            DataSourceRadio.AgendaCultural.TituloPagina = channelAgendaCultural.NomeChannel.ToUpper();

            if (this.itemAgendaCultural.Title.Length <= 65)
            {
                DataSourceRadio.AgendaCultural.TituloEvento = this.itemAgendaCultural.Title;
            }
            else
            {
                DataSourceRadio.AgendaCultural.TituloEvento = this.itemAgendaCultural.Title.Substring(0, 62) + "...";
            }

            DataSourceRadio.AgendaCultural.TituloDetalhesEvento = "Detalhes";

            // Define Nome das proximas apresentacoes de acordo com o tipo de evento
            switch (channelAgendaCultural.NomeChannel)
            {
                case "Shows":
                    DataSourceRadio.AgendaCultural.TituloCartazEvento = "Próximas apresentações";
                    break;
                case "Teatro":
                    DataSourceRadio.AgendaCultural.TituloCartazEvento = "Em Cartaz";
                    break;
                case "Exposições":
                    DataSourceRadio.AgendaCultural.TituloCartazEvento = "Em Cartaz";
                    break;
                default:
                    DataSourceRadio.AgendaCultural.TituloCartazEvento = "Em Cartaz";
                    break;
            }

            DataSourceRadio.AgendaCultural.ImagemEvento = this.itemAgendaCultural.Thumb;
            DataSourceRadio.AgendaCultural.ListaDetalhesEvento = new ObservableCollection<ItemDetalheAgendaCulturalVO>();

            if (!string.IsNullOrEmpty(this.itemAgendaCultural.LinkSingle.Horario))
            {
                DataSourceRadio.AgendaCultural.ListaDetalhesEvento.Add(new ItemDetalheAgendaCulturalVO { TituloItemDetalhe = "Data e Horário", ConteudoItemDetalhe = Util.FormatarData(this.itemAgendaCultural.LinkSingle.Horario, false) });
            }

            string virgula = string.Empty;
            string virgula1 = string.Empty;

            if (!string.IsNullOrEmpty(this.itemAgendaCultural.LinkSingle.Endereco.Logradouro))
            {
                virgula = ", ";
            }

            if (!string.IsNullOrEmpty(this.itemAgendaCultural.LinkSingle.Endereco.Estabelecimento))
            {
                virgula1 = ", ";
            }

            if (!string.IsNullOrEmpty(this.itemAgendaCultural.LinkSingle.Endereco.Espaco))
            {
                DataSourceRadio.AgendaCultural.ListaDetalhesEvento.Add(new ItemDetalheAgendaCulturalVO { TituloItemDetalhe = "Local", ConteudoItemDetalhe = this.itemAgendaCultural.LinkSingle.Endereco.Espaco, Endereco = this.itemAgendaCultural.LinkSingle.Endereco.Logradouro + virgula + this.itemAgendaCultural.LinkSingle.Endereco.Estabelecimento + virgula1 + this.itemAgendaCultural.LinkSingle.Endereco.Localidade });
            }

            string precos = string.Empty;

            for (int i = 0; i < this.itemAgendaCultural.LinkSingle.Preco.Count; i++)
            {
                precos += this.itemAgendaCultural.LinkSingle.Preco[i] + "\n";
            }

            if (this.itemAgendaCultural.LinkSingle.Preco.Count > 0)
            {
                DataSourceRadio.AgendaCultural.ListaDetalhesEvento.Add(new ItemDetalheAgendaCulturalVO { TituloItemDetalhe = "Preço", ConteudoItemDetalhe = precos });
            }

            if (!string.IsNullOrEmpty(this.itemAgendaCultural.LinkSingle.Description))
            {
                DataSourceRadio.AgendaCultural.MaisInfoEvento = new ItemDetalheAgendaCulturalVO { TituloItemDetalhe = "Mais Informações", ConteudoItemDetalhe = this.itemAgendaCultural.LinkSingle.Description };
            }

            DataSourceRadio.AgendaCultural.ListaCartazEvento = new ObservableCollection<ItemEmCartazEventoAgendaCulturalVO>();

            for (int i = 0; i < this.itemAgendaCultural.LinkSingle.Sessao.Count; i++)
            {
                string data = Convert.ToDateTime(this.itemAgendaCultural.LinkSingle.Sessao[i].Horario).ToString();
                DataSourceRadio.AgendaCultural.ListaCartazEvento.Add(new ItemEmCartazEventoAgendaCulturalVO { DataHorarioEvento = data.Substring(0, 5) + "\n" + data.Substring(11, 5), NomeEvento = this.itemAgendaCultural.Title + "\n" + Util.FormatarData(this.itemAgendaCultural.LinkSingle.Sessao[i].Horario, true) });
            }
        }
    }
}
