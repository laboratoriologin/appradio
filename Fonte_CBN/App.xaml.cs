using RadioPlayer.Common;
using RadioPlayer.Common.Architecture;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227

namespace RadioPlayer
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
      
        }
        /// <summary>
        /// Função que seta o tipo de página.
        /// </summary>
        /// <param name="rootFrame">Indica o frame principal.</param>
        /// <returns>Retorna o currentSourcePageType.</returns>
        public static Type Current_source_page_type(Frame rootFrame)
        {
            Type currentSourcePageType = null;
            if (rootFrame != null)
            {
                currentSourcePageType = rootFrame.CurrentSourcePageType;
            }

            return currentSourcePageType;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            if (args.PreviousExecutionState != ApplicationExecutionState.Running)
            {
                bool loadState = args.PreviousExecutionState == ApplicationExecutionState.Terminated;
                ExtendedSplash extendedSplash = new ExtendedSplash(args.SplashScreen, loadState);
                Window.Current.Content = extendedSplash;
            }

            Window.Current.Activate();
            Frame rootFrame = Window.Current.Content as Frame;

            //////// Não repita inicialização app quando a janela já tem conteúdo,
            //////// Apenas garantir que a janela está ativa
            ////if (RootFrame == null)
            ////{
            ////    // Create a Frame to act as the navigation context and navigate to the first page
            ////    RootFrame = new Frame();

            ////    // Associate the frame with a SuspensionManager key                                
            ////    SuspensionManager.RegisterFrame(RootFrame, "AppFrame");

            //if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
            //{
            //    // Restore the saved session state only when appropriate
            //    try
            //    {
            //        SuspensionManager.RestoreAsync();
            //    }
            //    catch (SuspensionManagerException)
            //    {
            //        // Something went wrong restoring state.
            //        // Assume there is no state and continue
            //        Application.Current.Exit();
            //    }
            //}

            ////    //// Coloque a moldura na janela atual
            ////    Window.Current.Content = RootFrame;
            ////}

            ////if (RootFrame.Content == null)
            ////{
            ////    //// Quando a pilha de navegação não é restaurada navegar para a primeira página,
            ////    //// Configurar a nova página, passando as informações necessárias como uma navegação
            ////    //// Parâmetro 
            ////    if (!RootFrame.Navigate(typeof(Home)))
            ////    {
            ////        throw new Exception("Failed to create initial page");
            ////    }
            ////}
            //////// Verifique se a janela atual está ativo
            ////Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
       
            var deferral = e.SuspendingOperation.GetDeferral();
            await SuspensionManager.SaveAsync();
            deferral.Complete();

        
        }

    }
}
