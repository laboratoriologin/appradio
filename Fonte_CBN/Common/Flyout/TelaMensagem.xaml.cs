// <copyright file="TelaMensagem.xaml.cs" company="Login Informática">
//     Copyright (c) Login Informática. All rights reserved.
// </copyright>
// <author>Ricardo Carneiro</author>
//-----------------------------------------------------------------------

namespace RadioPlayer.Common.Flyout
{
    using System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Classe TelaMensagem
    /// </summary>
    public sealed partial class TelaMensagem : UserControl
    {
        /// <summary>
        /// Atríbuto instance.
        /// </summary>
        private static TelaMensagem instance = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="TelaMensagem"/> class.
        /// </summary>
        private TelaMensagem()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public static TelaMensagem Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TelaMensagem();
                }
                return instance;
            }
        }

        /// <summary>
        /// Método Show Mensagem
        /// </summary>
        /// <param name="e">Exception e</param>
        public void ShowMensagem(Exception e)
        {
            this.txtMessagem.Text = e.Message.ToString();
            this.MensagemPopup.IsOpen = true;
        }

        /// <summary>
        /// Método Show Mensagem
        /// </summary>
        /// <param name="e">Exception e</param>
        public void ShowMensagem(string e)
        {
            this.txtMessagem.Text = e.ToString();
            this.MensagemPopup.IsOpen = true;
        }

        /// <summary>
        /// Evento click do botão
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">RoutedEventArgs e</param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MensagemPopup.IsOpen = false;
            Application.Current.Exit();
        }
    }
}
