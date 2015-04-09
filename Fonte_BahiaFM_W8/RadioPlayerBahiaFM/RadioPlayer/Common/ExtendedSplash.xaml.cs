using RadioPlayer.Beans;
using RadioPlayer.Beans.BO;
using RadioPlayer.Beans.ValueObject.ValueObject.Xml.AgendaCulturalXML;
using RadioPlayer.Beans.ValueObject.ValueObject.Xml.AgendaCulturalXML.Cinema.Cartaz;
using RadioPlayer.Beans.ValueObject.ValueObject.Xml.NoticiaCorreioXML;
using RadioPlayer.Beans.ValueObject.ValueObject.Xml.PodCastXML;
using RadioPlayer.Beans.ValueObject.Xml.Programacao;
using RadioPlayer.Beans.ValueObject.Xml.Promocao;
using RadioPlayer.Beans.ValueObject.Xml.Salvador;
using RadioPlayer.Common.Architecture;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.ApplicationSettings;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

// The Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234233

namespace RadioPlayer.Common
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class ExtendedSplash
    {
        /// <summary>
        /// Atríbuto SplashImageRect
        /// </summary>
        private Rect splashImageRect; // Rect to store Splash screen image coordinates.

        /// <summary>
        /// Atríbuto Splash
        /// </summary>
        private SplashScreen splash; // Variable to hold the Splash screen object.

        /// <summary>
        /// Atríbuto Dismissed
        /// </summary>
        private bool dismissed = false; // Variable to track Splash screen dismissal status.

        /// <summary>
        /// Atríbuto RootFrame
        /// </summary>
        private Frame rootFrame;

        private MainPage main = new MainPage();
        private GrupoNoticiasItemBLL grupoNoticiasItemBLL = new GrupoNoticiasItemBLL(1);
        /// <summary>
        /// Atríbuto Ponto
        /// </summary>
        private string ponto = string.Empty;
        public ExtendedSplash()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedSplash"/> class.
        /// </summary>
        /// <param name="splashscreen">SplashScreen splashscreen</param>
        /// <param name="loadState">bool loadState</param>
        public ExtendedSplash(SplashScreen splashscreen, bool loadState)
        {
            InitializeComponent();
            this.CarregaThemeBoot();
            SettingsPane.GetForCurrentView().CommandsRequested += SettingCharmManager_CommandsRequested;

            // Listen for window resize events to reposition the extended Splash screen image accordingly.
            // This is important to ensure that the extended Splash screen is formatted properly in response to snapping, unsnapping, rotation, etc...
            Window.Current.SizeChanged += new WindowSizeChangedEventHandler(this.ExtendedSplash_OnResize);

            this.splash = splashscreen;
            if (this.splash != null)
            {
                // Register an event handler to be executed when the Splash screen has been Dismissed.
                this.splash.Dismissed += new TypedEventHandler<SplashScreen, object>(this.DismissedEventHandler);

                // Retrieve the window coordinates of the Splash screen image.
                this.splashImageRect = this.splash.ImageLocation;
                this.PositionImage();




            }

            //// Create a Frame to act as the navigation context 
            //// Não repita inicialização app quando a janela já tem conteúdo,
            //// Apenas garantir que a janela está ativa
            if (this.rootFrame == null)
            {
                //// Create a Frame to act as the navigation context and navigate to the first page
                this.rootFrame = new Frame();

                //// Associate the frame with a SuspensionManager key                                
                ////SuspensionManager.RegisterFrame(RootFrame, "AppFrame");

                //// Coloque a moldura na janela atual
                Window.Current.Content = this.rootFrame;
            }

            // Restore the saved session state if necessary


            this.RestoreStateAsync(loadState);
        }

        public void CarregaThemeBoot()
        {

            ImageBrush brush = new ImageBrush();
            imgGridSplash.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/BFM/180/Splash-Screen-1116x540px.png", UriKind.RelativeOrAbsolute));
            this.statusMainFirst.Foreground = new SolidColorBrush(Colors.Blue);

        }

        private void SettingCharmManager_CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            args.Request.ApplicationCommands.Add(new SettingsCommand("privacypolicy", "Privacy policy", OpenPrivacyPolicy));
        }


        private async void OpenPrivacyPolicy(IUICommand command)
        {
            Uri uri = new Uri("http://www.login.com.br/appmix");
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }

        /// <summary>
        /// Gets or sets - propriedade SplashImageRect.
        /// </summary>
        public Rect SplashImageRect
        {
            get { return this.splashImageRect; }
            set { this.splashImageRect = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the item is value.
        /// </summary>
        public bool Dismissed
        {
            get { return this.dismissed; }
            set { this.dismissed = value; }
        }

        /// <summary>
        /// Gets or sets - propriedade RootFrame.
        /// </summary>
        public Frame RootFrame
        {
            get { return this.rootFrame; }
            set { this.rootFrame = value; }
        }

        /// <summary>
        /// Gets or sets - propriedade Ponto.
        /// </summary>
        public string Ponto
        {
            get { return this.ponto; }
            set { this.ponto = value; }
        }

        /// <summary>
        /// Gets or sets - propriedade Splash.
        /// </summary>
        public SplashScreen Splash
        {
            get { return this.splash; }
            set { this.splash = value; }
        }

        /// <summary>
        /// Método PositionImage
        /// </summary>
        private void PositionImage()
        {
            extendedSplashImage.SetValue(Canvas.LeftProperty, this.SplashImageRect.X);
            extendedSplashImage.SetValue(Canvas.TopProperty, this.SplashImageRect.Y);
            extendedSplashImage.Height = this.SplashImageRect.Height;
            extendedSplashImage.Width = this.SplashImageRect.Width;
        }

        /// <summary>
        /// Evento DismissedEventHandler 
        /// Include code to be executed when the system has transitioned from the Splash screen to the extended Splash screen (application's first view).
        /// </summary>
        /// <param name="sender">SplashScreen sender</param>
        /// <param name="e">object e</param>
        private async void DismissedEventHandler(SplashScreen sender, object e)
        {
            DataSourceRadio.CurrentPage = this;

            this.Dismissed = true;

            DataSourceRadio.IsUpdating = true;

            Main.LoadResource();

            Main.LoadMain();

            grupoNoticiasItemBLL.LoadDataGrupoNoticias(1, 12);


            foreach (Task item in DataSourceRadio.ListaTarefaLoadDados)
            {
                if (!item.AsyncState.GetType().Equals(typeof(ChannelCinemaVO)) && !item.AsyncState.GetType().Equals(typeof(ChannelAgendaCulturalVO)) && !item.AsyncState.GetType().Equals(typeof(ChannelProgramacaoVO)) && !item.AsyncState.GetType().Equals(typeof(ChannelCorreioVO)) && !item.AsyncState.GetType().Equals(typeof(ChannelSalvadorVO)) && !item.AsyncState.GetType().Equals(typeof(ChannelPodCastVO)) && !item.AsyncState.GetType().Equals(typeof(ChannelPromocaoVO)))
                {
                    ////28,29,30,51,96,88,97,32,36,25,4,48,49,152,1,27
                    //if (!((ChannelVO)item.AsyncState).Id.Equals(28)
                    //    && !((ChannelVO)item.AsyncState).Id.Equals(29)
                    //    && !((ChannelVO)item.AsyncState).Id.Equals(30)
                    //    && !((ChannelVO)item.AsyncState).Id.Equals(51)
                    //    && !((ChannelVO)item.AsyncState).Id.Equals(96)
                    //    && !((ChannelVO)item.AsyncState).Id.Equals(88)
                    //    && !((ChannelVO)item.AsyncState).Id.Equals(97)
                    //    && !((ChannelVO)item.AsyncState).Id.Equals(32)
                    //    && !((ChannelVO)item.AsyncState).Id.Equals(36)
                    //    && !((ChannelVO)item.AsyncState).Id.Equals(25)
                    //    && !((ChannelVO)item.AsyncState).Id.Equals(4)
                    //    && !((ChannelVO)item.AsyncState).Id.Equals(48)
                    //    && !((ChannelVO)item.AsyncState).Id.Equals(49)
                    //    && !((ChannelVO)item.AsyncState).Id.Equals(152)
                    //    && !((ChannelVO)item.AsyncState).Id.Equals(1)

                    //    && !((ChannelVO)item.AsyncState).Id.Equals(27))
                    //    item.Start();
                }
            }


            await Task.Delay(8000);
            await Dispatcher.RunAsync(CoreDispatcherPriority.High, () => { RootFrame.Navigate(typeof(MainPage)); Window.Current.Content = RootFrame; });
        }

        /// <summary>
        /// Evento ExtendedSplash_OnResize
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">WindowSizeChangedEventArgs e</param>
        private void ExtendedSplash_OnResize(object sender, WindowSizeChangedEventArgs e)
        {
            // Safely update the extended Splash screen image coordinates. This function will be fired in response to snapping, unsnapping, rotation, etc...
            if (this.Splash != null)
            {
                // Update the coordinates of the Splash screen image.
                this.SplashImageRect = this.Splash.ImageLocation;
                this.PositionImage();
            }
        }

        /// <summary>
        /// Método Assincrono RestoreStateAsync
        /// </summary>
        /// <param name="loadState">bool loadState</param>
        private async void RestoreStateAsync(bool loadState)
        {
            if (loadState)
            {
                await SuspensionManager.RestoreAsync();
            }

            //// Normally you should start the time consuming task asynchronously here and 
            //// dismiss the extended Splash screen in the completed handler of that task
            //// This sample dismisses extended Splash screen  in the handler for "Learn More" button for demonstration
        }
    }
}