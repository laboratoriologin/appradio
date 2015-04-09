using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioPlayer.Beans.BO
{
    using System.Linq;
    using System.Text;
    
    using RadioPlayer.Beans.ValueObject.ValueObject.Xml;

    /// <summary>
    /// Classe de regra de negócios da tela de detalhe de notícias.
    /// </summary>
    public class DetalhamentoNoticiaBLL
    {
        /// <summary>
        /// Atributo itemNoticia.
        /// </summary>
        private ItemVO itemNoticia;

        /// <summary>
        /// Gets or sets - propriedade Evento
        /// </summary>
        public ItemVO ItemNoticia
        {
            get { return this.itemNoticia; }
            set { this.itemNoticia = value; }
        }

        /// <summary>
        /// Método para carregar os dados de uma notícia.
        /// </summary>
        /// <param name="channel">channel da notícia</param>
        /// <param name="idNoticia">id da notícia</param>
        /// <param name="corTexto">Cor do texto</param>
        public void LoadDataDetalhamentoNoticia(ChannelVO channel, int idNoticia)
        {
            // Identifica o item Referente à notícia
            this.itemNoticia = channel.ListaItem.Where(item => item.Guid == idNoticia).First<ItemVO>();

            // Identifica o nome do Canal de Notícias
            DataSourceRadio.DetalhamentoNoticia.Channel = channel.NomeChannel;

            // Identifica o Título da Notícia
            DataSourceRadio.DetalhamentoNoticia.Titulo = this.itemNoticia.Title;

            // Identifica a descrição da Notícia
            DataSourceRadio.DetalhamentoNoticia.SubTitulo = this.itemNoticia.Description;

            DataSourceRadio.DetalhamentoNoticia.ImagemNoticia = new ImageVO();

            DataSourceRadio.DetalhamentoNoticia.Conteudo = this.itemNoticia.Content;

            DataSourceRadio.DetalhamentoNoticia.ConteudoSemHtml = this.itemNoticia.ContentOld;

           

            ////// Verifica se Notícia possui imagem e legenda de imagem
            ////if (this.itemNoticia.ListaImage.Count.Equals(0))
            ////{
            ////    DataSourceRadio.DetalhamentoNoticia.ImagemNoticia.Url = "Assets/noticia2.png";
            ////}
            ////else
            ////{
            ////    DataSourceRadio.DetalhamentoNoticia.ImagemNoticia.Url = this.itemNoticia.ListaImage[0].Url;

            ////    if (!string.IsNullOrEmpty(this.itemNoticia.ListaImage[0].Title))
            ////    {
            ////        DataSourceRadio.DetalhamentoNoticia.ImagemNoticia.Title = this.itemNoticia.ListaImage[0].Title;
            ////    }

            ////    if (!this.itemNoticia.Content.IndexOf('\n').Equals(-1))
            ////    {
            ////        if (this.itemNoticia.Content.IndexOf('\n') < 150)
            ////        {
            ////            DataSourceRadio.DetalhamentoNoticia.LegendaImagemNoticia = this.itemNoticia.Content.Substring(0, this.itemNoticia.Content.IndexOf('\n'));
            ////        }
            ////        else
            ////        {
            ////            DataSourceRadio.DetalhamentoNoticia.LegendaImagemNoticia = "";
            ////        }
            ////    }
            ////    else
            ////    {
            ////        DataSourceRadio.DetalhamentoNoticia.LegendaImagemNoticia = "";
            ////    }
            ////}

            ////// Identifica o Conteúdo da Notícia
            ////if (!this.itemNoticia.Content.IndexOf('\n').Equals(-1))
            ////{
            ////    if (this.itemNoticia.Content.IndexOf('\n') < 150)
            ////    {
            ////        DataSourceRadio.DetalhamentoNoticia.Conteudo = this.itemNoticia.Content.Substring(this.itemNoticia.Content.IndexOf('\n'));
            ////    }
            ////    else
            ////    {
            ////        DataSourceRadio.DetalhamentoNoticia.Conteudo = this.itemNoticia.Content;
            ////    }
            ////}
            ////else
            ////{
            ////    DataSourceRadio.DetalhamentoNoticia.Conteudo = this.itemNoticia.Content;
            ////}
        }
    }
}
