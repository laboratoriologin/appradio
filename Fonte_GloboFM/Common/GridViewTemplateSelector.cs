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
    public class GridViewTemplateSelector : DataTemplateSelector
    {
        #region Atributo
        /// <summary>
        /// Atríbuto standard250x250ItemTemplateA
        /// </summary>
        private DataTemplate standard250x250ItemTemplateA;

        /// <summary>
        /// Atríbuto standard250x250ItemTemplateB
        /// </summary>
        private DataTemplate standard250x250ItemTemplateB;
        #endregion

        #region Propriedades

        /// <summary>
        /// Gets or sets - propriedade Standard250x250ItemTemplateA
        /// </summary>
        public DataTemplate Standard250x250ItemTemplateA
        {
            get { return this.standard250x250ItemTemplateA; }
            set { this.standard250x250ItemTemplateA = value; }
        }

        /// <summary>
        /// Gets or sets - propriedade Standard250x250ItemTemplateA
        /// </summary>
        public DataTemplate Standard250x250ItemTemplateB
        {
            get { return this.standard250x250ItemTemplateB; }
            set { this.standard250x250ItemTemplateB = value; }
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
            return this.Standard250x250ItemTemplateA;
        }
        #endregion
    }
}
