using RadioPlayer.Beans.ValueObject.ValueObject.View.AgendaCultural;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace RadioPlayer.Common
{
    /// <summary>
    /// Classe template seletor
    /// </summary>
    public class DetalheEventoTemplateSelector : DataTemplateSelector
    {
        #region Atributo
        /// <summary>
        /// Atríbuto templateTwoTexts
        /// </summary>
        private DataTemplate itemListVieAgendaDetalhesEvento;

        /// <summary>
        /// Atríbuto templateThreeTexts
        /// </summary>
        private DataTemplate itemListAgendaDetalhesEventoLocal;
        #endregion

        #region Propriedades
        /// <summary>
        /// Gets or sets - propriedade TemplateTwoTexts
        /// </summary>
        public DataTemplate TemplateTwoTexts
        {
            get { return this.itemListVieAgendaDetalhesEvento; }
            set { this.itemListVieAgendaDetalhesEvento = value; }
        }

        /// <summary>
        /// Gets or sets - propriedade TemplateThreeTexts
        /// </summary>
        public DataTemplate TemplateThreeTexts
        {
            get { return this.itemListAgendaDetalhesEventoLocal; }
            set { this.itemListAgendaDetalhesEventoLocal = value; }
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

            if (((ItemDetalheAgendaCulturalVO)item).Endereco != null)
            {
                return this.TemplateThreeTexts;
            }

            return this.TemplateTwoTexts;
        }
        #endregion
    }
}
