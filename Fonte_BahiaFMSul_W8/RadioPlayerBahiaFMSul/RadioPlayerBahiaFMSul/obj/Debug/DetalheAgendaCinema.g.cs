﻿

#pragma checksum "D:\Projetos\ProjetoRadio\Radio - Versao2.6\Fonte_BahiaFMSul_W8\RadioPlayerBahiaFMSul\RadioPlayerBahiaFMSul\DetalheAgendaCinema.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "9A135B89E2D2EEF5F459E97B0C56D45F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RadioPlayer
{
    partial class DetalheAgendaCinema : global::RadioPlayer.Common.LayoutAwarePage, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 251 "..\..\DetalheAgendaCinema.xaml"
                ((global::Windows.UI.Xaml.VisualStateGroup)(target)).CurrentStateChanged += this.ApplicationViewStates_CurrentStateChanged;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 289 "..\..\DetalheAgendaCinema.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.AppBarUpdateDataBtn_Click;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


