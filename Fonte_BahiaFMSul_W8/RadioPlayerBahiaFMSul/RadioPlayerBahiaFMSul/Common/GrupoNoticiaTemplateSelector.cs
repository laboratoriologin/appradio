using RadioPlayer.Beans.ValueObject.ValueObject.View.Principal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace RadioPlayer.Common
{
    public class GrupoNoticiaTemplateSelector : DataTemplateSelector
    {
        #region Atributo
        /// <summary>
        /// Atríbuto template figura mais texto.
        /// </summary>
        private DataTemplate grupoNoticiasItemSemImagem;

        /// <summary>
        /// Atríbuto template fundo vermelho mais texto.
        /// </summary>
        private DataTemplate mainPage;

        /// <summary>
        /// Atríbuto templateGridAgenda.
        /// </summary>
        private DataTemplate gridAgendaCultural;

        /// <summary>
        /// Atríbuto gridPodCast.
        /// </summary>
        private DataTemplate gridPodCast;

        /// <summary>
        /// Atríbuto gridPromocao.
        /// </summary>
        private DataTemplate gridPromocao;
        
        #endregion

        #region Propriedades
        /// <summary>
        /// Gets or sets - propriedade TemplateFiguraTexto.
        /// </summary>
        public DataTemplate TemplateFiguraTexto
        {
            get { return this.grupoNoticiasItemSemImagem; }
            set { this.grupoNoticiasItemSemImagem = value; }
        }

        /// <summary>
        /// Gets or sets - propriedade TemplateGridAgenda.
        /// </summary>
        public DataTemplate TemplateGridAgenda
        {
            get { return this.gridAgendaCultural; }
            set { this.gridAgendaCultural = value; }
        }

        /// <summary>
        /// Gets or sets - propriedade TemplateGridPodCast.
        /// </summary>
        public DataTemplate TemplateGridPodCast
        {
            get { return this.gridPodCast; }
            set { this.gridPodCast = value; }
        }

        /// <summary>
        /// Gets or sets - propriedade TemplateGridPodCast.
        /// </summary>
        public DataTemplate TemplateGridPromocao
        {
            get { return this.gridPromocao; }
            set { this.gridPromocao = value; }
        }

        /// <summary>
        /// Gets or sets - propriedade TemplateFundoVermelhoTexto.
        /// </summary>
        public DataTemplate TemplateFundoVermelhoTexto
        {
            get { return this.mainPage; }
            set { this.mainPage = value; }
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Classe select template core
        /// </summary>
        /// <param name="item">Object item</param>
        /// <param name="container">DependencyObject container</param>
        /// <returns>Componente DataTemplate</returns>
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;

            if (((ItemVO)item).HasGridPromocao == true)
            {
                return this.TemplateGridPromocao;
            }

            if (((ItemVO)item).HasPodCast == true)
            {
                return this.TemplateGridPodCast;
            }


            if (((ItemVO)item).HasImagemIBahia == true )
            {
                return this.TemplateFiguraTexto;
            }

              if (((ItemVO)item).HasGridImagemIBahia == true )
            {
                return this.TemplateGridAgenda;
            }

            
              


            return this.TemplateFundoVermelhoTexto;
        }
        #endregion
    }
}
