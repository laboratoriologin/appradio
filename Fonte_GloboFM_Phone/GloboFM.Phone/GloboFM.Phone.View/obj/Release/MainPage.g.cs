﻿#pragma checksum "E:\Projetos\ProjetoRadio\Radio - Versao2.5_Final\Radio - Versao2.6\Fonte_GloboFM_Phone\GloboFM.Phone\GloboFM.Phone.View\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6BFD53CDB05C69BDDB85B514BABC4AE8"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace GloboFM.Phone.View {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.Button btnPlay;
        
        internal System.Windows.Controls.Button btnPause;
        
        internal System.Windows.Controls.ProgressBar pbCarregando;
        
        internal System.Windows.Controls.TextBlock txtStatus;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/GloboFM.Phone.View;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.btnPlay = ((System.Windows.Controls.Button)(this.FindName("btnPlay")));
            this.btnPause = ((System.Windows.Controls.Button)(this.FindName("btnPause")));
            this.pbCarregando = ((System.Windows.Controls.ProgressBar)(this.FindName("pbCarregando")));
            this.txtStatus = ((System.Windows.Controls.TextBlock)(this.FindName("txtStatus")));
        }
    }
}

