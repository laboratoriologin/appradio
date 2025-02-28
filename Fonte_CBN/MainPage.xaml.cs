﻿using GoogleAnalyticsTracker.Core;
using GoogleAnalyticsTracker.RT;
using HtmlAgilityPack;
using RadioPlayer.Beans;
using RadioPlayer.Beans.BO;
using RadioPlayer.Beans.ValueObject.ValueObject.View.AgendaCultural;
using RadioPlayer.Beans.ValueObject.ValueObject.View.Noticia;
using RadioPlayer.Beans.ValueObject.ValueObject.View.Principal;
using RadioPlayer.Beans.ValueObject.ValueObject.Xml.AgendaCulturalXML;
using RadioPlayer.Beans.ValueObject.ValueObject.Xml.AgendaCulturalXML.Cinema.Cartaz;
using RadioPlayer.Beans.ValueObject.ValueObject.Xml.NoticiaCorreioXML;
using RadioPlayer.Beans.ValueObject.ValueObject.Xml.PodCastXML;
using RadioPlayer.Beans.ValueObject.Xml.Programacao;
using RadioPlayer.Beans.ValueObject.Xml.ProgramacaoRadioXML;
using RadioPlayer.Beans.ValueObject.Xml.Salvador;
using RadioPlayer.Common;
using RadioPlayer.Common.Architecture;
using RadioPlayer.Common.Flyout;
using RadioPlayer.Model;
using RadioPlayer.Model.Beans;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Data.Xml.Dom;
using Windows.Media;
using Windows.Storage.Streams;
using Windows.System;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using View = RadioPlayer.Beans.ValueObject.ValueObject.View.AgendaCultural;
using Xml = RadioPlayer.Beans.ValueObject.ValueObject.Xml;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace RadioPlayer
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : RadioPlayer.Common.LayoutAwarePage
    {
        private static TelaMensagem telaMensagem = TelaMensagem.Instance;
        /// <summary>
        /// Atríbuto web
        /// </summary>
        private WebView web = new WebView();
        private WebView wvPodCast = new WebView();

        /// <summary>
        /// Atríbuto hasProgramacaoMusical.
        /// </summary>
        private bool hasProgramacaoMusical;

        public WebView WvPodCast
        {
            get { return wvPodCast; }
            set { wvPodCast = value; }
        }

        //private List<RadioItem> ListRadio = new List<RadioItem>();
        /// <summary>
        /// Atributo com a formação do html do youtube.
        /// </summary>
        private string beginEnderecoYoutube = "<html><head><style>html, body { height:100%; overflow:hidden; }body { margin:0; }</style></head><body><iframe width='410' height='100' src='";
        /// <summary>
        /// Lista de parametros recebidos.
        /// </summary>
        private List<object> parametro = new List<object>();
        private RadioBO radioBO;
        private GrupoNoticiasItemBLL grupoNoticiasItemBLL;
        private Preference preference;
        private bool hasError = false;
        private int radioIndex = 0;
        private bool isBlockOnSelected = false;
        private bool isBlockOnThemeSelected = false;
        private bool isBlockSelectOnThemeSelected = false;
        private bool isBlockGridInicial = false;
        private bool isBlockGridInicialSelect = false;
        private int flagBack = 0;
        private Button btnLinkPodcast = new Button();
        //private Button btnLinkPodcastExterno = new Button();
        private TextBlock StatusText = new TextBlock();
        private string itemLinkPromocao = "";
        /// Tarefa de download dos dados do sistema.
        /// </summary>
        private Task task;
        private HtmlDocument htmlDocument = new HtmlDocument();
        private string formatado;
        private CancellationTokenSource cts = new CancellationTokenSource();
        private Style objStyle = new Style();
        private Style objResourceStyle = Application.Current.Resources["GroupHeaderTextStyle"] as Style;
        private ItemVO itemVO = new ItemVO();
        private CategoriaVO categoriaVO = new CategoriaVO();
        //private XDocument document = XDocument.Load(Windows.ApplicationModel.Package.Current.InstalledLocation.Path + " \\ " + @"\Common\Architecture\xmlStyle.xml");
        private List<CategoriaVO> lstCategoria = new List<CategoriaVO>();
        private List<CategoriaVO> lstCategoriaFilter = new List<CategoriaVO>();
        private List<Menu> lstItemCategoria = new List<Menu>();
        private AgendaCulturalBLL objAgendaCulturalBLL = new AgendaCulturalBLL();
        private Menu objMenu = new Menu();
        private GrupoAgendaCulturalBLL objGrupoAgendaCulturalBLL = new GrupoAgendaCulturalBLL();
        private ObservableCollection<ItemVO> lstItemVO = new ObservableCollection<ItemVO>();
        private DetalheAgendaCinema objDetalheAgendaCinema = new DetalheAgendaCinema();
        private DetalheAgendaCultural objDetalheAgendaCultural = new DetalheAgendaCultural();
        private List<RadioCategory> _lstRadioCategory = new List<RadioCategory>();
        private GrupoAgendaCulturalVO objGrupoAgendaCulturalVO;
        private BitmapImage bitmapImage = null;
        private string strDia = string.Empty;
        private HttpClient httpClient;
        private ItemProgramacaoRadioXMLVO itemProgramacaoRadioXMLVO = new ItemProgramacaoRadioXMLVO();
        private StringBuilder strDescricaoFormatada = new StringBuilder();


        /// <summary>
        /// Array de categorias gerenciadas pela página.
        /// </summary>
        private int[] arrIdCategoria = { 22, 2, 12, 13, 14, 26, 3 };

        /// <summary>
        /// Atríbuto RootFrame
        /// </summary>
        private Frame rootFrame;

        /// <summary>
        /// Atríbuto Dismissed
        /// </summary>
        private bool dismissed = false; // Variable to track Splash screen dismissal status.

        private Dictionary<CancellationTokenSource, Task> listTaskUI = new Dictionary<CancellationTokenSource, Task>();

        /// <summary>
        /// Gets or sets - propriedade Web
        /// </summary>
        public WebView Web
        {
            get { return this.web; }
            set { this.web = value; }
        }

        /// <summary>
        /// Gets or sets - atributo BeginEnderecoYoutube.
        /// </summary>
        public string BeginEnderecoYoutube
        {
            get { return this.beginEnderecoYoutube; }
            set { this.beginEnderecoYoutube = value; }
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

        public MainPage()
        {

            this.InitializeComponent();

            //Util.VerificarBootXml();


            this.radioBO = new RadioBO(this.BaseUri);
            this.grupoNoticiasItemBLL = new GrupoNoticiasItemBLL(3);

            preference = new Preference();

            MediaControl.PlayPressed += MediaControlPlayPressed;
            MediaControl.PausePressed += MediaControlPausePressed;
            MediaControl.PlayPauseTogglePressed += MediaControlPlayPauseTogglePressed;
            MediaControl.StopPressed += MediaControlStopPressed;
            MediaControl.NextTrackPressed += MediaControl_NextTrackPressed;
            MediaControl.PreviousTrackPressed += MediaControl_PreviousTrackPressed;

            // Register for the window resize event
            Window.Current.SizeChanged += WindowSizeChanged;
            PropertyComponentChange(progressNoticia, AppBarUpdateDataBtn);

            #region Rastrear tela Google analytics

            using (Tracker tracker = new Tracker("UA-1974624-63", ""))
            {
                tracker.TrackPageViewAsync("APP Rádio CBN", "api/create");
            }
            #endregion
            //banner.LoadCompleted += new Windows.UI.Xaml.Navigation.LoadCompletedEventHandler(banner_LoadCompleted);
            //banner_Copy.LoadCompleted += new Windows.UI.Xaml.Navigation.LoadCompletedEventHandler(banner_Copy_LoadCompleted);


            //httpClient = new HttpClient();
            //// Increase the max buffer size for the response so we don't get an exception with so many web sites
            ////httpClient.MaxResponseContentBufferSize = 256000;
            //httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");
            //banner.ScriptNotify += banner_ScriptNotify;

            //// Here we have to set the AllowedScriptNotifyUri property because we are 
            //// navigating to some site where we don't own the content and we want to 
            //// allow window.external.notify() to pass data back to the app.
            //List<Uri> allowedUris = new List<Uri>();
            //allowedUris.Add(new Uri("http://www.centraldedownload.com.br/Radio/CBN/SquareBannerPage.html"));
            //banner.AllowedScriptNotifyUris = allowedUris;

            Uri strBanner = new Uri(@"http://www.centraldedownload.com.br/radio/cbn/whatsapp.html");
            Uri strBannerSquare = new Uri(@"http://www.centraldedownload.com.br/radio/cbn/square.html");
            Uri strBannerSnapped = new Uri(@"http://www.centraldedownload.com.br/radio/cbn/snapped.html");

            wvBanner.Source = strBanner;
            banner.Source = strBannerSquare;
            banner_Copy.Source = strBannerSnapped;
        }

        #region Métodos
        /// <summary>
        /// Método Carregamento Load
        /// </summary>
        /// <param name="guid">int guid</param>
        /// <param name="idChannel">int idChannel</param>
        /// <param name="corTexto">Cor do texto</param>
        private void Load(int guid, int idChannel, string corTexto)
        {
            Xml.ChannelVO channelNoticia;

            //// Identifica o canal da Noticia
            if (DataSourceRadio.ListaChannelVO.Where(item => item.Id == idChannel).First<Xml.ChannelVO>().ListaItem.Where(item2 => item2.Guid.Equals(guid)).Any<Xml.ItemVO>())
            {
                channelNoticia = DataSourceRadio.ListaChannelVO.Where(item => item.Id == idChannel).First<Xml.ChannelVO>();
            }
            else
            {
                channelNoticia = DataSourceRadio.ListaChannelVOInactive.Where(item => item.Id == idChannel).First<Xml.ChannelVO>();
            }

            DetalhamentoNoticiaBLL detalhamentoNoticiaBLL = new DetalhamentoNoticiaBLL();
            detalhamentoNoticiaBLL.LoadDataDetalhamentoNoticia(channelNoticia, guid);

            this.DefaultViewModel["TextoFacebook"] = Util.ShareFacebook(detalhamentoNoticiaBLL.ItemNoticia.Link);
            this.DefaultViewModel["TextoTwitter"] = Util.ShareTwitter(detalhamentoNoticiaBLL.ItemNoticia.Link, detalhamentoNoticiaBLL.ItemNoticia.Title);
        }
        #endregion

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary <see cref="Frame.Navigate(Type, Object)"/> of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(object navigationParameter, Dictionary<string, object> pageState)
        {
            DataSourceRadio.CurrentPage = this;

            // Allow saved page state to override the initial item to display
            if (pageState != null && pageState.ContainsKey("SelectedItem"))
            {
                navigationParameter = pageState["SelectedItem"];
            }

            List<object> lstParametro = (List<object>)navigationParameter;



            this.Load(Convert.ToInt32(lstParametro[0]), Convert.ToInt32(lstParametro[1]), lstParametro[2].ToString());
            //// Preencher Lista DEFAULT
            DataSourceRadio.DetalhamentoNoticia.Channel = DataSourceRadio.DetalhamentoNoticia.Channel.ToUpper();

            this.DefaultViewModel["groupedItemsViewSource"] = DataSourceRadio.ListaCategoria;


        }

        private void OnThemeSelected(object sender, SelectionChangedEventArgs e)
        {
            if (!isBlockOnThemeSelected)
            {
                Theme selectedTheme = (Theme)themesGrid.SelectedItem;
                SetTheme(selectedTheme);

            }


        }

        // ---------------------------------------------------------------
        // Date      240311
        // Purpose   Get a Brush from a hex color value.
        // Entry     sHexColor - The color, format "#AARRGGBB".
        // Return    A SolidColorBrush.
        // Comments  
        // ---------------------------------------------------------------
        private SolidColorBrush HexColorToBrush(string sHexColor)
        {
            if (sHexColor.Length != 9)
            {
                return new SolidColorBrush(Colors.White);
            }
            byte A = Convert.ToByte(sHexColor.Substring(1, 2), 16);
            byte R = Convert.ToByte(sHexColor.Substring(3, 2), 16);
            byte G = Convert.ToByte(sHexColor.Substring(5, 2), 16);
            byte B = Convert.ToByte(sHexColor.Substring(7, 2), 16);
            SolidColorBrush sb =
                new SolidColorBrush(Color.FromArgb(A, R, G, B));
            return sb;
        }


        private void SetTheme(Theme theme)
        {
            SolidColorBrush colorItem = HexColorToBrush("#97210514");
            SolidColorBrush colorFont = HexColorToBrush("#E5343434");


            var bitmapImage = new BitmapImage(theme.BackgroundImg);

            colorItem = HexColorToBrush("#E5343434");
            colorFont = HexColorToBrush("#E5343434");


            ImageTopo.Source = new BitmapImage(theme.LogoImg);
            ImageTopoSnapped.Source = new BitmapImage(theme.LogoImg);

            preference.SetTheme(theme.Id);

            RootImage.Source = bitmapImage;
            RootImage.Stretch = Stretch.UniformToFill;
            RootImage.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
            RootImage.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;

            PlayerTopGrid.Background = colorItem;
            PlayerTopGridSnapped.Background = colorItem;

            TitleNamePlayerSnapped.Foreground = new SolidColorBrush(Colors.White);
            TitleNamePlayer.Foreground = new SolidColorBrush(Colors.White);
            txtAgendaCultural.Foreground = new SolidColorBrush(Colors.White);

            Util.VerificarCor(colorFont.Color.ToString());


            PlayerBottomGrid.Background = colorItem;
            PlayerBottomGridBanner.Background = colorItem;
            PlayerBottomGrid1.Background = colorItem;
            PlayerBottomGridSnapped.Background = colorItem;
            PlayerBottomGridRedeSocialSnapped.Background = colorItem;

            List<RadioCategory> listCategory = new List<RadioCategory>();
            listCategory.AddRange((List<RadioCategory>)cvsRadioCategory.Source);

            foreach (RadioCategory cat in listCategory)
            {
                cat.Color = colorFont.Color.ToString();
                foreach (RadioItem radio in cat.Items)
                {
                    radio.Color = colorItem.Color.ToString();

                    radio.Image = new Uri("ms-appx:/Assets/CBN/140/Logo-210x210px.png");

                }
            }

            int index = 1;

            if (isBlockGridInicial)
            {
                index = Convert.ToInt32(DataSourceRadio.ObjBootThemeVO.strBoot) - 1;
            }
            else
            {
                index = 1;
            }


            cvsRadioCategory.Source = listCategory;
            loadRadioItem(radioBO.getCBNFM(), false);



            isBlockGridInicial = false;

        }


        private void WindowSizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {


            this.VerificarResolucao();
            RootImage.Source = bitmapImage;
            RootImage.Stretch = Stretch.UniformToFill;
            RootImage.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
            RootImage.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;


            if (e.Size.Height > 800.0)
            {

            }
            else if (e.Size.Height > 1080.0)
            {

            }
            else if (e.Size.Height > 1440.0)
            {

            }
        }

        private void VerificarResolucao()
        {
            List<Theme> list = radioBO.getThemesList();
            Theme theme = list.First();
            foreach (Theme t in list)
            {
                if (t.Id == preference.GetTheme())
                {
                    theme = t;
                    break;
                }
            }

            // We have different widths to use depending on the view state
            if (ApplicationView.Value != ApplicationViewState.Snapped)
            {
                GridSnapped.Visibility = Visibility.Collapsed;
                AppBarNormal.Visibility = Visibility.Collapsed;
                GridNormal.Visibility = Visibility.Visible;
                bitmapImage = new BitmapImage(theme.BackgroundImg);
                ThemeBar.Visibility = Visibility.Visible;
                gridPrincipal.Visibility = Visibility.Visible;
                txtAgendaCultural.Visibility = Visibility.Collapsed;

                backButton.Visibility = Visibility.Visible;
                gridNoticias.Visibility = Visibility.Visible;
                viewNoticias.Visibility = Visibility.Collapsed;
                grdCategoria.Visibility = Visibility.Visible;
                viewAgenda.Visibility = Visibility.Collapsed;
                GridTituloAgenda.Visibility = Visibility.Visible;
                titleTxtSnapped.Visibility = Visibility.Collapsed;
                titleTxt.Visibility = Visibility.Visible;
                backButton.Visibility = Visibility.Collapsed;
                flagBack = 0;

            }
            else
            {
                btnLinkPodcast.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                GridSnapped.Visibility = Visibility.Visible;
                AppBarNormal.Visibility = Visibility.Collapsed;
                GridNormal.Visibility = Visibility.Collapsed;
                bitmapImage = new BitmapImage(theme.BackgroundImgSnap);
                ThemeBar.Visibility = Visibility.Collapsed;
                gridPrincipal.Visibility = Visibility.Collapsed;
                titleTxt.Visibility = Visibility.Collapsed;
                titleTxtSnapped.Visibility = Visibility.Visible;
                backButton.Visibility = Visibility.Collapsed;
                gridNoticias.Visibility = Visibility.Collapsed;
                viewNoticias.Visibility = Visibility.Collapsed;
                grdCategoria.Visibility = Visibility.Collapsed;
                viewAgenda.Visibility = Visibility.Collapsed;
                GridTituloAgenda.Visibility = Visibility.Collapsed;
                objDetalheAgendaCinema.Visibility = Visibility.Collapsed;

                objDetalheAgendaCinema.Web.NavigateToString("http://www.google.com");

            }


        }

        private async void MediaControl_PreviousTrackPressed(object sender, object e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {

            });
        }

        private async void MediaControl_NextTrackPressed(object sender, object e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {

            });
        }

        private async void MediaControlStopPressed(object sender, object e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                Stop();
            });
        }

        private async void MediaControlPlayPauseTogglePressed(object sender, object e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                OnClickPlayPause();
            });
        }

        private void MediaControlPausePressed(object sender, object e)
        {
        }

        private void MediaControlPlayPressed(object sender, object e)
        {
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.loadRadioList();
            this.loadThemeList();

        }

        private void loadThemeList()
        {

            List<Theme> list = radioBO.getThemesList();
            isBlockOnThemeSelected = true;
            cvsThemes.Source = list;

            if (string.IsNullOrEmpty(DataSourceRadio.ObjBootThemeVO.strBoot))
            {
                this.CreateTheme(radioBO.getCBNFM().Id);
                isBlockGridInicial = false;
            }
            else
            {
                this.CreateTheme(Convert.ToInt32(DataSourceRadio.ObjBootThemeVO.strBoot));

                isBlockGridInicial = true;

            }

            isBlockSelectOnThemeSelected = false;
            isBlockOnThemeSelected = false;

            foreach (Theme t in list)
            {
                if (t.Id == preference.GetTheme())
                {
                    themesGrid.SelectedItem = t;
                    SetTheme(t);

                    break;
                }
            }


        }

        /// <summary>
        /// Carrega as informações da rádio.
        /// </summary>
        /// <param name="item"></param>
        private void loadRadioItem(RadioItem item, Boolean play)
        {
            BitmapImage image = new BitmapImage();

            this.titleTxt.Text = item.Title;
            this.titleTxtSnapped.Text = item.Title;

            if (item.Description != null)
            {
                this.descriptionTxt.Text = item.Locality + " " + item.Description;
                this.descriptionTxtSnapped.Text = item.Locality + " " + item.Description;
            }

            MediaControl.ArtistName = item.Locality;
            MediaControl.TrackName = item.Title;

            if (play)
            {
                hasError = false;

                Stop();
                Play();
            }





        }

        /// <summary>
        /// Carregar a base de rádios
        /// </summary>
        private void loadRadioList()
        {
            cvsRadioCategory.Source = radioBO.getRadiosCategory();

            this.loadRadioItem(radioBO.getCBNFM(), true);
        }

        /// <summary>
        /// Método para selecionar a grid no primeiro carregamento.
        /// </summary>
        private void SelecionaGridView()
        {
            if (lstCategoria.Count() == 4)
            {
                grdCategoria.SelectedIndex = 0;
            }
        }

        private void radiosGrid_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {

            string fontColor = string.Empty;

            this.radioIndex = 3;
            loadRadioItem(radioBO.getCBNFM(), true);


            this.CreateTheme(radioBO.getCBNFM().Id);
            isBlockSelectOnThemeSelected = false;
            backButton.Visibility = Visibility.Collapsed;
            if (ApplicationView.Value != ApplicationViewState.Snapped)
            {
                gridNoticias.Visibility = Visibility.Visible;
                grdCategoria.Visibility = Visibility.Visible;

            }

            //// bahia FM

            lstCategoria = new List<CategoriaVO>();
            lstItemCategoria = new List<Menu>();

            foreach (var item in DataSourceRadio.LstCategoriaBahiaFMSul)
            {
                objMenu = new Menu();
                objMenu.Categoria = item.NomeCategoria;
                objMenu.FontColor = item.CorLetraTitulo;
                fontColor = item.CorLetraTitulo;
                lstItemCategoria.Add(objMenu);
            }

            objMenu = new Menu();
            objMenu.Categoria = "Agenda";
            objMenu.FontColor = "#343434";

            lstCategoria = DataSourceRadio.LstCategoriaCBN.ToList();

            categoriaVO = new CategoriaVO();
            categoriaVO.ListaItem = new ObservableCollection<ItemVO>();
            categoriaVO.Id = 1;
            categoriaVO.NomeCategoria = "Agenda";
            categoriaVO.ListaItem = CarregaGridAgenda();

            lstCategoria.Add(categoriaVO);

            lstItemCategoria.Add(objMenu);

            categoriaVO = new CategoriaVO();
            objMenu = new Menu();

            objMenu.Categoria = "Podcast";
            objMenu.FontColor = "#343434";

            categoriaVO.ListaItem = new ObservableCollection<ItemVO>();
            categoriaVO.Id = 2;
            categoriaVO.NomeCategoria = "Podcast";
            categoriaVO.ListaItem = CarregaGridPodCast();

            lstCategoria.Add(categoriaVO);

            objMenu.Lista = lstItemVO;

            lstItemCategoria.Add(objMenu);

            this.DefaultViewModel["CATEGORIA"] = lstItemCategoria.ToList();
            itemVO.CorLetraNoticiaRadio = Constantes.strCor;
            categoriaVO.CorLetraTitulo = Constantes.strCor;
            itemVO.CorStackPanelItem = Constantes.strCor;
            categoriaVO.CorLetraTitulo = itemVO.CorStackPanelItem;

            lstCategoriaFilter = new List<CategoriaVO>();
            lstCategoriaFilter.Add(lstCategoria.FirstOrDefault());
            this.DefaultViewModel["groupedItemsViewSource"] = lstCategoriaFilter;

            groupedItemsViewSource.Source = this.DefaultViewModel["groupedItemsViewSource"];


            //this.grupoNoticiasItemBLL.LoadDataGrupoNoticias(1, 22);
            Constantes.intEstacao = 4;

            SelecionaGridView();



            //flagBack = 0;
            //categoriaVO = new CategoriaVO();
            //objMenu = new Menu();

            //objMenu.Categoria = "Fala Bahia";
            //objMenu.FontColor = "#343434";

            //categoriaVO.ListaItem = new ObservableCollection<ItemVO>();
            //categoriaVO.Id = 2;
            //categoriaVO.NomeCategoria = "Fala Bahia";
            //categoriaVO.ListaItem = CarregaGridSalvador();

            //lstCategoria.Add(categoriaVO);

            //objMenu.Lista = lstItemVO;

            //lstItemCategoria.Add(objMenu);

            //this.DefaultViewModel["CATEGORIA"] = lstItemCategoria.ToList();
            //itemVO.CorLetraNoticiaRadio = Constantes.strCor;
            //categoriaVO.CorLetraTitulo = Constantes.strCor;
            //itemVO.CorStackPanelItem = Constantes.strCor;
            //categoriaVO.CorLetraTitulo = itemVO.CorStackPanelItem;

            //lstCategoriaFilter = new List<CategoriaVO>();
            //lstCategoriaFilter.Add(lstCategoria.FirstOrDefault());
            //this.DefaultViewModel["groupedItemsViewSource"] = lstCategoriaFilter;

            //groupedItemsViewSource.Source = this.DefaultViewModel["groupedItemsViewSource"];

            grdCategoria.SelectedIndex = 0;
            flagBack = 0;
            categoriaVO = new CategoriaVO();
            objMenu = new Menu();

            objMenu.Categoria = "Programação";
            objMenu.FontColor = "#343434";

            categoriaVO.ListaItem = new ObservableCollection<ItemVO>();
            categoriaVO.Id = 2;
            categoriaVO.NomeCategoria = "Programação";
            categoriaVO.ListaItem = CarregaGridProgramacaoMusical();

            lstCategoria.Add(categoriaVO);

            objMenu.Lista = lstItemVO;

            lstItemCategoria.Add(objMenu);

            this.DefaultViewModel["CATEGORIA"] = lstItemCategoria.ToList();
            itemVO.CorLetraNoticiaRadio = Constantes.strCor;
            categoriaVO.CorLetraTitulo = Constantes.strCor;
            itemVO.CorStackPanelItem = Constantes.strCor;
            categoriaVO.CorLetraTitulo = itemVO.CorStackPanelItem;

            lstCategoriaFilter = new List<CategoriaVO>();
            lstCategoriaFilter.Add(lstCategoria.FirstOrDefault());
            this.DefaultViewModel["groupedItemsViewSource"] = lstCategoriaFilter;

            groupedItemsViewSource.Source = this.DefaultViewModel["groupedItemsViewSource"];
            //this.grupoNoticiasItemBLL.LoadDataGrupoNoticias(1, 22);
            Constantes.intEstacao = 3;
            grdCategoria.SelectedIndex = 0;
        }

        private ObservableCollection<ItemVO> CarregaGridSalvador()
        {
            lstItemVO = new ObservableCollection<ItemVO>();



            if (DataSourceRadio.ListaItemSalvador.Count > 0)
            {
                foreach (var itemSalvadorVO in DataSourceRadio.ListaItemSalvador.ToList())
                {
                    itemVO = new ItemVO();
                    itemVO.CorLetraNoticiaRadio = Constantes.strCor;
                    itemVO.CorLetraNoticiaRadioBahiaFM = Constantes.strCor;
                    itemVO.CorLetraNoticiaRadio = Constantes.strCor;
                    itemVO.ImgCorMais = Constantes.strCor;

                    itemVO.IdChannel = 12;
                    itemVO.CorStackPanelItem = Constantes.strCor;
                    if (itemSalvadorVO.Guid != null)
                    {
                        itemVO.Guid = Convert.ToInt32(itemSalvadorVO.Guid);

                        if (itemSalvadorVO.Title != null)
                        {
                            itemVO.Titulo = itemSalvadorVO.Title;
                        }
                        else
                        {
                            itemVO.Titulo = string.Empty;
                        }

                        if (itemSalvadorVO.Description != null)
                        {
                            if (itemSalvadorVO.Description.Length > 150)
                            {
                                itemVO.SubTitulo = itemSalvadorVO.Description.Substring(0, 150) + "  ...";
                            }
                            else
                            {
                                itemVO.SubTitulo = itemSalvadorVO.Description;
                            }
                        }
                        else
                        {
                            itemVO.Titulo = string.Empty;
                        }

                        if (itemSalvadorVO.Link != null)
                        {
                            itemVO.Link = itemSalvadorVO.Link;
                        }
                        else
                        {
                            itemVO.Link = string.Empty;
                        }



                        itemVO.Descricao = itemSalvadorVO.Content;
                        // ... Input string.
                        string input = itemSalvadorVO.Description;
                        ObservableCollection<ImageChannelSalvadorVO> lstImageChannelSalvadorVO = new ObservableCollection<ImageChannelSalvadorVO>();
                        ImageChannelSalvadorVO objImageSalvadorVO = new ImageChannelSalvadorVO();
                        // ... One or more digits.
                        //Match m = Regex.Match(input, "&quot;uploads\\/[a-zA-Z0-9 ._]*&quot;");

                        //if (m.Value != null)
                        //{
                        //    objImageSalvadorVO.Url = itemSalvadorVO;
                        //    lstImageChannelSalvadorVO.Add(objImageSalvadorVO);
                        //}

                        itemVO.ImagemFundo = new Xml.ImageVO();

                        if (itemSalvadorVO.ListaImage.Count > 0)
                        {
                            foreach (var itemImage in itemSalvadorVO.ListaImage)
                            {
                                itemVO.ImagemFundo.Url = itemImage.Url;
                            }
                        }
                        else
                        {
                            itemVO.HasGridImagemIBahia = false;
                            itemVO.HasPodCast = true;
                        }
                    }
                    else
                    {
                        itemVO.Descricao = string.Empty;
                    }

                    lstItemVO.Add(itemVO);
                }
            }

            return lstItemVO;

        }

        private ObservableCollection<ItemVO> CarregaGridProgramacaoMusical()
        {
            lstItemVO = new ObservableCollection<ItemVO>();
            int countItem = 0;

            if (DataSourceRadio.ListaChannelProgramacaoRadioVO.Count > 0)
            {
                foreach (var itemSalvadorVO in DataSourceRadio.ListaChannelProgramacaoRadioVO.ToList())
                {
                    foreach (var item in itemSalvadorVO.ListaItem)
                    {


                        itemVO = new ItemVO();
                        itemVO.CorLetraNoticiaRadio = Constantes.strCor;
                        itemVO.CorLetraNoticiaRadioBahiaFM = Constantes.strCor;
                        itemVO.CorLetraNoticiaRadio = Constantes.strCor;
                        itemVO.ImgCorMais = Constantes.strCor;

                        itemVO.IdChannel = 72;
                        itemVO.CorStackPanelItem = Constantes.strCor;
                        if (item.Guid != null)
                        {
                            itemVO.Guid = Convert.ToInt32(item.Guid);

                            if (item.Title != null)
                            {
                                itemVO.Titulo = item.Title;
                            }
                            else
                            {
                                itemVO.Titulo = string.Empty;
                            }

                            if (item.Description != null)
                            {
                                if (item.Description.Length > 150)
                                {
                                    itemVO.SubTitulo = item.Description;

                                    strDescricaoFormatada = new StringBuilder();

                                    strDescricaoFormatada.AppendLine();
                                    strDescricaoFormatada.AppendLine();
                                    strDescricaoFormatada.AppendLine();
                                    strDescricaoFormatada.AppendLine();
                                }
                                else
                                {
                                    itemVO.SubTitulo = item.Description;


                                    strDescricaoFormatada = new StringBuilder();

                                    strDescricaoFormatada.AppendLine();
                                    strDescricaoFormatada.AppendLine();
                                    strDescricaoFormatada.AppendLine();
                                    strDescricaoFormatada.AppendLine();
                                    strDescricaoFormatada.AppendLine();
                                    strDescricaoFormatada.AppendLine();
                                    strDescricaoFormatada.AppendLine();
                                    strDescricaoFormatada.AppendLine();
                                    strDescricaoFormatada.AppendLine();
                                    strDescricaoFormatada.AppendLine();
                                    strDescricaoFormatada.AppendLine();
                                    strDescricaoFormatada.AppendLine();
                                }
                            }
                            else
                            {
                                itemVO.Titulo = string.Empty;
                            }

      
                         
                            


                            foreach (var itemHorario in item.ListaHorarioProgramacaoRadioXMLVO.OrderBy(i => i.DiaDaSemana).ToList())
                            {
                                if (!itemHorario.DiaDaSemana.Equals(strDia))
                                {

                                    strDescricaoFormatada.Append(Util.formatDateXML(Convert.ToInt32(itemHorario.DiaDaSemana)) + "\n");
                                    strDia = itemHorario.DiaDaSemana;
                                }

                                strDescricaoFormatada.Append("Das " + itemHorario.HorarioInicio.ToString() + " às " + itemHorario.HorarioFinal.ToString() + "\n");


                                //                 lstConteudoHorario.Add()
                            }
                            itemVO.Descricao = strDescricaoFormatada.ToString();
                            itemVO.HasImagemIBahia = false;
                            itemVO.HasGridPromocao = false;
                            itemVO.HasPodCast = false;
                            itemVO.HasProgramacaoMusical = true;

                            itemVO.ImagemFundo = new Xml.ImageVO();
                            foreach (var itemImage in item.ListaImage)
                            {

                                if (itemImage.Url != null)
                                {
                                    itemVO.ImagemFundo.Url =  itemImage.Url;
                                }

                                itemVO.ImgCorMais = "ms-appx:///Assets/+.png";
                            }

                        

                            
                            //countItem = countItem + 1;
                        }
                        else
                        {
                            itemVO.Descricao = string.Empty;
                        }

                        lstItemVO.Add(itemVO);
                    }
                }
            }

            return lstItemVO;

        }

        public ObservableCollection<ItemVO> CarregaGridProgramacao()
        {
            lstItemVO = new ObservableCollection<ItemVO>();



            if (DataSourceRadio.ListaItemProgramacao.Count > 0)
            {
                foreach (var itemProgramacaoVO in DataSourceRadio.ListaItemProgramacao.ToList())
                {
                    itemVO = new ItemVO();
                    itemVO.CorLetraNoticiaRadio = Constantes.strCor;
                    itemVO.CorLetraNoticiaRadioBahiaFM = Constantes.strCor;
                    itemVO.CorLetraNoticiaRadio = Constantes.strCor;
                    itemVO.ImgCorMais = Constantes.strCor;

                    itemVO.IdChannel = 222;
                    itemVO.CorStackPanelItem = Constantes.strCor;
                    if (itemProgramacaoVO.Guid != null)
                    {
                        itemVO.Guid = Convert.ToInt32(itemProgramacaoVO.Guid);

                        if (itemProgramacaoVO.Title != null)
                        {
                            itemVO.Titulo = itemProgramacaoVO.Title;
                        }
                        else
                        {
                            itemVO.Titulo = string.Empty;
                        }

                        if (itemProgramacaoVO.Description != null)
                        {
                            if (itemProgramacaoVO.Description.Length > 150)
                            {
                                itemVO.SubTitulo = itemProgramacaoVO.Description.Substring(0, 150) + "  ...";
                            }
                            else
                            {
                                itemVO.SubTitulo = itemProgramacaoVO.Description;
                            }
                        }
                        else
                        {
                            itemVO.Titulo = string.Empty;
                        }

                        if (itemProgramacaoVO.Link != null)
                        {
                            itemVO.Link = itemProgramacaoVO.Link;
                        }
                        else
                        {
                            itemVO.Link = string.Empty;
                        }



                        itemVO.Descricao = itemProgramacaoVO.Content;
                        // ... Input string.
                        string input = itemProgramacaoVO.Description;
                        ObservableCollection<ImageChannelProgramacaoVO> lstImageChannelProgramacaoVO = new ObservableCollection<ImageChannelProgramacaoVO>();
                        ImageChannelProgramacaoVO objImageProgramacaoVO = new ImageChannelProgramacaoVO();
                        itemVO.ImagemFundo = new RadioPlayer.Beans.ValueObject.ValueObject.Xml.ImageVO();


                    }
                    else
                    {
                        itemVO.Descricao = string.Empty;
                    }

                    lstItemVO.Add(itemVO);
                }
            }

            return lstItemVO;

        }
        private ObservableCollection<ItemVO> CarregaGridPodCast()
        {
            lstItemVO = new ObservableCollection<ItemVO>();




            foreach (var itemPodcast in DataSourceRadio.ListaItemPodCastVO.ToList())
            {
                itemVO = new ItemVO();
                itemVO.Titulo = itemPodcast.Title;
                itemVO.SubTitulo = itemPodcast.Description;
                itemVO.StrConteudo = itemPodcast.Guid;
                itemVO.Link = itemPodcast.Link;
                itemVO.Guid = 399;
                itemVO.Descricao = itemPodcast.Description;
                itemVO.CorLetraNoticiaRadio = Constantes.strCor;
                itemVO.HasPodCast = true;
                itemVO.ImagemFundo = new Xml.ImageVO();

                foreach (var itemImage in itemPodcast.ListaImage)
                {
                    itemVO.ImagemFundo.Url = itemImage.Url;
                }

                lstItemVO.Add(itemVO);
            }

            return lstItemVO;
        }

        protected ObservableCollection<ItemVO> CarregaGridAgenda()
        {
            lstItemVO = new ObservableCollection<ItemVO>();

            itemVO.Titulo = "Cinema";
            itemVO.SubTitulo = "Cinema";
            itemVO.CorLetraNoticiaRadio = "#343434";
            itemVO.HasGridImagemIBahia = true;
            itemVO.HasPodCast = false;
            itemVO.HasGridPromocao = false;
            itemVO.ImagemFundo = new Xml.ImageVO();

            itemVO.ImagemFundo.Url = "ms-appx:///Assets/2_icone_globo_cultura_cinema.png";
            itemVO.ImgCorMais = "ms-appx:///Assets/+.png";

            lstItemVO.Add(itemVO);

            itemVO = new ItemVO();
            itemVO.Titulo = "Exposições";
            itemVO.SubTitulo = "Exposições";
            itemVO.CorLetraNoticiaRadio = "#343434";
            itemVO.HasGridImagemIBahia = true;
            itemVO.HasPodCast = false;
            itemVO.HasGridPromocao = false;
            itemVO.ImagemFundo = new Xml.ImageVO();

            itemVO.ImagemFundo.Url = "ms-appx:///Assets/4_icone_globo_cultura_exposicao.png";

            lstItemVO.Add(itemVO);

            itemVO = new ItemVO();
            itemVO.Titulo = "Teatro";
            itemVO.SubTitulo = "Teatro";
            itemVO.CorLetraNoticiaRadio = "#343434";
            itemVO.HasGridImagemIBahia = true;
            itemVO.HasGridPromocao = false;
            itemVO.ImagemFundo = new Xml.ImageVO();


            itemVO.ImagemFundo.Url = "ms-appx:///Assets/1_icone_globo_cultura_teatro.png";


            lstItemVO.Add(itemVO);

            itemVO = new ItemVO();
            itemVO.Titulo = "Shows";
            itemVO.SubTitulo = "Shows";
            itemVO.CorLetraNoticiaRadio = "#343434";
            itemVO.HasGridImagemIBahia = true;
            itemVO.HasPodCast = false;
            itemVO.HasGridPromocao = false;
            itemVO.ImagemFundo = new Xml.ImageVO();
            itemVO.ImagemFundo.Url = "ms-appx:///Assets/3_icone_globo_cultura_musica.png";

            lstItemVO.Add(itemVO);

            return lstItemVO;
        }

        public async void LoadMain()
        {
            HomeBLL homeBLL = new HomeBLL();

            List<Task> taskChannel = DataSourceRadio.ListaTarefaLoadDados.Where<Task>(item => item.AsyncState.GetType().Equals(typeof(Xml.ChannelVO))).ToList();
            List<Task> listTaskNoticia = taskChannel.Where<Task>(item => arrIdCategoria.Contains(((Xml.ChannelVO)item.AsyncState).Id)).ToList();

            int taskIBahia = listTaskNoticia.Where(item => !((Task)item).IsCompleted).Count<object>();

            if (!taskIBahia.Equals(0))
            {
                while (taskIBahia != 0)
                {
                    await Task.Delay(500);
                    homeBLL.LoadDataHome();
                    taskIBahia = listTaskNoticia.Where(item => !((Task)item).IsCompleted).Count<object>();

                    if (DataSourceRadio.ListaTarefaLoadDados.Where(item => ((Task)item).IsFaulted).Any<object>())
                    {
                        object tarefaError = DataSourceRadio.ListaTarefaLoadDados.Where(item => ((Task)item).IsFaulted).First<object>();
                        this.PopUpMsgError(((Task)tarefaError).Exception.InnerException.InnerException.Message);
                    }
                }
            }
            else
            {
                homeBLL.LoadDataHome();
            }

            DataSourceRadio.IsUpdating = false;
        }

        /// <summary>
        /// Método para carrega os dados do sistema.
        /// </summary>
        /// <param name="id">Id do tipo de categoria para carregar.</param>
        private void LoadMain(int id)
        {
            //GrupoNoticiasItemBLL grupoNoticiasItemBLL = new GrupoNoticiasItemBLL(id);

            List<Task> listTask = DataSourceRadio.ListaTarefaLoadDados.Where<Task>(item => item.AsyncState.GetType().Equals(typeof(Xml.ChannelVO))).ToList<Task>();
            int[] idCategorias;

            switch (id)
            {
                case 1:
                    idCategorias = new int[] { 2, 12, 13, 14, 152, 1 };
                    listTask = listTask.Where<Task>(item => idCategorias.Contains(((Xml.ChannelVO)item.AsyncState).Id)).ToList();
                    break;
                case 2:
                    idCategorias = new int[] { 4, 48, 49, 25 };
                    listTask = listTask.Where<Task>(item => idCategorias.Contains(((Xml.ChannelVO)item.AsyncState).Id)).ToList();
                    break;
                case 3:
                    idCategorias = new int[] { 27, 28, 29, 30, 51, 96, 88, 97, 32, 36 };
                    listTask = listTask.Where<Task>(item => idCategorias.Contains(((Xml.ChannelVO)item.AsyncState).Id)).ToList();
                    break;

                case 4:
                    idCategorias = new int[] { 27, 28, 29, 30, 51, 96, 88, 97, 32, 36 };
                    listTask = listTask.Where<Task>(item => idCategorias.Contains(((Xml.ChannelVO)item.AsyncState).Id)).ToList();
                    break;
            }


            if (listTask.Any<Task>(item => !item.IsCompleted))
            {
                DataSourceRadio.IsUpdatingGrupoNoticiasItem = true;
            }
            else
            {
                DataSourceRadio.IsUpdatingGrupoNoticiasItem = false;
            }

            CancellationTokenSource cts;
            Task loadUI;
            //// Erro  ao atualizar 
            foreach (Task item in listTask)
            {
                if (item.IsCompleted)
                {
                    grupoNoticiasItemBLL.LoadDataGrupoNoticias(id, ((Xml.ChannelVO)item.AsyncState).Id);
                }
                else
                {
                    if (!item.Status.Equals(TaskStatus.Running))
                    {
                        cts = new CancellationTokenSource();
                        var token = cts.Token;

                        loadUI = item.ContinueWith((t) => UpdateUIPageSecond(id, ((Xml.ChannelVO)item.AsyncState).Id, grupoNoticiasItemBLL, token), token);

                        item.ContinueWith((t) =>
                        {
                            Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, () =>
                            {
                                DataSourceRadio.IsUpdatingGrupoNoticiasItem = false;
                                this.PropertyComponentChange(this.progressNoticia, this.AppBarUpdateDataBtn);
                                Util.LoadUIDataBiding();
                                this.UpdateLayout();
                                gridNoticias.UpdateLayout();
                                gridNoticias_ItemClick(this, new ItemClickEventArgs());
                            });
                        });

                        this.listTaskUI.Add(cts, loadUI);

                        item.Start();
                    }

                }

            }

        }


        private async void UpdateUIPage(string id, Task task, GrupoAgendaCulturalBLL grupoAgendaCulturalBLL, CancellationToken ct)
        {
            //if (!ct.IsCancellationRequested)
            //{
            //    while (!taskRunnig.IsCompleted)
            //    {
            await Task.Delay(1000);
            if (!ct.IsCancellationRequested)
            {
                grupoAgendaCulturalBLL.LoadDataGrupoAgendaCultural(id);
            }
            //else
            //{
            //    break;
            //}
            // }

            DataSourceRadio.IsUpdatingAgendaCultural = false;
            this.PropertyComponentChange(this.progressNoticia, this.AppBarUpdateDataBtn);
            //}
        }

        protected void UpdateUIPageSecond(int id, int idCategoria, GrupoNoticiasItemBLL grupoNoticiasItemBLL, CancellationToken ct)
        {
            if (!ct.IsCancellationRequested)
                Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, () =>
                {
                    grupoNoticiasItemBLL.LoadDataGrupoNoticias(id, idCategoria);
                });




        }

        /// <summary>
        /// Método Load Main
        /// </summary>
        /// <param name="id">Parametro agenda cultural</param>
        public void LoadMainAgenda(string id)
        {
            if ("Cinema".Equals(id))
            {
                this.task = DataSourceRadio.ListaTarefaLoadDados.Single<Task>(item => item.AsyncState.GetType().Equals(typeof(ChannelCinemaVO)));
            }
            else
            {
                List<Task> listTaskAgendaCultural = DataSourceRadio.ListaTarefaLoadDados.Where<Task>(item => item.AsyncState.GetType().Equals(typeof(ChannelAgendaCulturalVO))).ToList<Task>();

                if ("Shows".Equals(id))
                {
                    this.task = listTaskAgendaCultural.Single<Task>(item => ((ChannelAgendaCulturalVO)item.AsyncState).Id.Equals(2));

                }
                else if ("Teatro".Equals(id))
                {
                    this.task = listTaskAgendaCultural.Single<Task>(item => ((ChannelAgendaCulturalVO)item.AsyncState).Id.Equals(5));
                }
                else if ("Exposições".Equals(id))
                {
                    this.task = listTaskAgendaCultural.Single<Task>(item => ((ChannelAgendaCulturalVO)item.AsyncState).Id.Equals(4));
                }

            }

            DataSourceRadio.IsUpdatingAgendaCultural = true;
            this.PropertyComponentChange(this.progressNoticia, this.AppBarUpdateDataBtn);
            GrupoAgendaCulturalBLL grupoAgendaCulturalBLL = new GrupoAgendaCulturalBLL();

            if (this.task.Status.Equals(TaskStatus.Created))
            {
                var token = cts.Token;
                this.task.ContinueWith((t) =>
                {
                    Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, () =>
                    {
                        UpdateUIPage(id, task, grupoAgendaCulturalBLL, token);
                    }).AsTask().Wait();
                }, token);

                this.task.Start();

                //  DataSourceIBahia.TaskUIAgendaCultural.Start();
            }
            else if (this.task.Status.Equals(TaskStatus.Running))
            {
                var token = cts.Token;

                DataSourceRadio.TaskUIAgendaCultural = new Task(() =>
                {
                    Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, () =>
                    {
                        UpdateUIPage(id, task, grupoAgendaCulturalBLL, token);
                    }).AsTask().Wait();

                }, token);

                // DataSourceIBahia.TaskUIAgendaCultural.Start();
            }
            else
            {
                grupoAgendaCulturalBLL.LoadDataGrupoAgendaCultural(id);
                DataSourceRadio.IsUpdatingAgendaCultural = false;
                this.PropertyComponentChange(this.progressNoticia, this.AppBarUpdateDataBtn);
            }
        }

        /// <summary>
        /// Método que atualiza os componentes de acordo com o status de atualizando.
        /// </summary>
        /// <param name="progressBarr">Componente Progress Bar da tela.</param>
        /// <param name="btn">Componente Botão Atualizar da tela.</param>
        public override void PropertyComponentChange(ProgressBar progressBarr, Button btn)
        {
            if (DataSourceRadio.IsUpdatingGrupoNoticiasItem)
            {
                progressBarr.IsIndeterminate = true;
                btn.IsEnabled = false;
            }
            else
            {

                progressBarr.IsIndeterminate = false;
                btn.IsEnabled = true;
                isBlockOnSelected = false;

            }
        }

        /// <summary>
        /// Evento  AppBarDoneButton
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">RoutedEventArgs e</param>
        private void AppBarUpdateDataBtn_Click(object sender, RoutedEventArgs e)
        {
            DataSourceRadio.IsUpdating = true;
            PropertyComponentChange(progressNoticia, AppBarUpdateDataBtn);

            DataSourceRadio.GrupoAgendaCulturalVO.Titulo = string.Empty;
            DataSourceRadio.GrupoAgendaCulturalVO.ListaItemAgendaCultural.Clear();

            string id = this.parametro[0].ToString();

            if ("Cinema".Equals(id))
            {
                Main.LoadMainRefresh(typeof(ChannelCinemaVO), new int[1] { 0 });
            }
            else
            {
                if ("Shows".Equals(id))
                {
                    Main.LoadMainRefresh(typeof(ChannelAgendaCulturalVO), new int[1] { 2 });
                }
                else if ("Teatro".Equals(id))
                {
                    Main.LoadMainRefresh(typeof(ChannelAgendaCulturalVO), new int[1] { 5 });
                }
                else if ("Exposições".Equals(id))
                {
                    Main.LoadMainRefresh(typeof(ChannelAgendaCulturalVO), new int[1] { 4 });
                }

            }

            this.LoadMainAgenda(id);
        }

        private void CreateTheme(int p)
        {
            List<Theme> list = radioBO.getThemesList();
            Theme tema;

            switch (p)
            {
                case 1:

                    tema = new Theme();
                    isBlockSelectOnThemeSelected = true;
                    isBlockOnThemeSelected = true;
                    cvsThemes.Source = list;
                    isBlockOnThemeSelected = false;
                    tema = list[5];
                    SetTheme(tema);
                    break;
                case 2:

                    tema = new Theme();
                    isBlockSelectOnThemeSelected = true;
                    isBlockOnThemeSelected = true;
                    cvsThemes.Source = list;
                    isBlockOnThemeSelected = false;
                    tema = list[6];
                    SetTheme(tema);
                    break;
                case 3:

                    tema = new Theme();
                    isBlockSelectOnThemeSelected = true;
                    isBlockOnThemeSelected = true;
                    cvsThemes.Source = list;
                    isBlockOnThemeSelected = false;
                    tema = list[7];
                    SetTheme(tema);
                    break;
                case 4:
                    tema = new Theme();

                    isBlockSelectOnThemeSelected = true;
                    isBlockOnThemeSelected = true;
                    cvsThemes.Source = list;
                    isBlockOnThemeSelected = false;
                    tema = list[8];
                    SetTheme(tema);

                    break;
                default:
                    break;
            }
        }

        public void PlaySound(string pathSound)
        {
            mediaPlayer.Source = new Uri(pathSound);
            mediaPlayer.AutoPlay = true;
        }

        public void StopSound()
        {
            mediaPlayer.Source = null;
        }

        public void Play()
        {
            PlaySound(radioBO.getCBNFM().Url);

            titleTxt.Foreground = Util.GetColorFromHexa("#FFFFFF");
            descriptionTxt.Foreground = Util.GetColorFromHexa("#FFFFFF");
            statusTxt.Foreground = Util.GetColorFromHexa("#FFFFFF");
            progressPlayer.Foreground = Util.GetColorFromHexa("#FFFFFF");
            Constantes.strCor = "#FFFFFF";
            backButton.Style = (Windows.UI.Xaml.Style)Application.Current.Resources["BotaoVoltar"];
            progressNoticia.Foreground = Util.GetColorFromHexa("#FFFFFF");


            imgPlayBtn.Source = new BitmapImage(new Uri(this.BaseUri, Constantes.imgPauseBranco));
            imgPlayBtnSnapped1.Source = new BitmapImage(new Uri(this.BaseUri, Constantes.imgPauseBranco));
        }

        public void Stop()
        {
            StopSound();


            imgPlayBtn.Source = new BitmapImage(new Uri(this.BaseUri, Constantes.imgPlayBranco));
            imgPlayBtnSnapped1.Source = new BitmapImage(new Uri(this.BaseUri, Constantes.imgPlayBranco));
        }


        private void OnClickPlayPause()
        {
            if (mediaPlayer.CurrentState == MediaElementState.Closed)
            {
                Play();
            }
            else
            {
                Stop();
            }
        }

        #region Player Controls

        private void playBtn_Click(object sender, RoutedEventArgs e)
        {
            OnClickPlayPause();
        }

        private void nextBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        private async void updateRadioStatus()
        {

            bool showProgress = false;

            string status = "";

            switch (mediaPlayer.CurrentState)
            {
                case MediaElementState.Closed:
                    if (hasError)
                    {
                        if (NetworkInterface.GetIsNetworkAvailable())
                        {
                            status = "A Rádio esta fora do ar";
                        }
                        else
                        {
                            status = "Não conectado a Internet";
                            telaMensagem.ShowMensagem(status);
                        }
                    }
                    else
                    {
                        status = "Parado";
                    }
                    break;
                case MediaElementState.Opening:
                    showProgress = true;
                    status = "Carregando";
                    break;
                case MediaElementState.Paused:
                    status = "Em espera";
                    break;
                case MediaElementState.Playing:
                    status = "Tocando";
                    break;
                case MediaElementState.Stopped:
                    status = "Parado";
                    break;
                case MediaElementState.Buffering:
                    showProgress = true;
                    status = "Carregando " + Convert.ToInt16(mediaPlayer.BufferingProgress * 100) + "%";
                    break;
            }

            progressPlayer.IsActive = showProgress;
            progressPlayerSnapped.IsActive = showProgress;
            progressPlayer.Visibility = showProgress ? Windows.UI.Xaml.Visibility.Visible : Windows.UI.Xaml.Visibility.Collapsed;
            progressPlayerSnapped.Visibility = showProgress ? Windows.UI.Xaml.Visibility.Visible : Windows.UI.Xaml.Visibility.Collapsed;

            statusTxt.Text = status;
            statusTxtSnapped.Text = status;
        }

        private void mediaPlayer_MediaOpened(object sender, RoutedEventArgs e)
        {
            RadioItem item = radioBO.getCBNFM();
            MediaControl.ArtistName = item.Locality;
            MediaControl.TrackName = item.Title;
            //MediaControl.AlbumArt = await createImageMediaControl(item);            
        }

        private void mediaPlayer_CurrentStateChanged(object sender, RoutedEventArgs e)
        {
            updateRadioStatus();
        }

        private void mediaPlayer_BufferingProgressChanged(object sender, RoutedEventArgs e)
        {
            updateRadioStatus();
        }

        private void mediaPlayer_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            hasError = true;
            updateRadioStatus();
        }

        protected override void GoBack(object sender, RoutedEventArgs e)
        {
            if (Constantes.intFlagBack != null)
            {
                flagBack = Constantes.intFlagBack;
            }

            switch (flagBack)
            {
                case 0:
                    backButton.Visibility = Visibility.Collapsed;
                    gridNoticias.Visibility = Visibility.Collapsed;
                    viewNoticias.Visibility = Visibility.Collapsed;
                    grdCategoria.Visibility = Visibility.Collapsed;
                    viewAgenda.Visibility = Visibility.Collapsed;
                    GridTituloAgenda.Visibility = Visibility.Collapsed;
                    this.gridPrincipal.Children.Remove(gridPrincipal.Children[3]);
                    objDetalheAgendaCinema.Visibility = Visibility.Collapsed;
                    objDetalheAgendaCultural.Visibility = Visibility.Collapsed;

                    break;
                case 1:
                    this.gridPrincipal.Children.Remove(gridPrincipal.Children[3]);
                    backButton.Visibility = Visibility.Visible;
                    gridNoticias.Visibility = Visibility.Visible;
                    grdCategoria.Visibility = Visibility.Visible;
                    viewNoticias.Visibility = Visibility.Collapsed;
                    viewAgenda.Visibility = Visibility.Collapsed;
                    GridTituloAgenda.Visibility = Visibility.Collapsed;
                    flagBack = 0;
                    break;

                case 2:

                    viewAgenda.Visibility = Visibility.Visible;
                    GridTituloAgenda.Visibility = Visibility.Visible;

                    backButton.Visibility = Visibility.Visible;
                    gridNoticias.Visibility = Visibility.Visible;
                    grdCategoria.Visibility = Visibility.Visible;
                    viewNoticias.Visibility = Visibility.Collapsed;
                    viewAgenda.Visibility = Visibility.Collapsed;
                    GridTituloAgenda.Visibility = Visibility.Collapsed;

                    this.gridPrincipal.Children.Remove(gridPrincipal.Children[4]);

                    objDetalheAgendaCinema.Visibility = Visibility.Collapsed;

                    objDetalheAgendaCinema.Web.NavigateToString("http://www.google.com");

                    flagBack = 0;
                    break;

                case 3:

                    viewAgenda.Visibility = Visibility.Visible;
                    GridTituloAgenda.Visibility = Visibility.Visible;

                    backButton.Visibility = Visibility.Visible;
                    gridNoticias.Visibility = Visibility.Visible;
                    grdCategoria.Visibility = Visibility.Visible;
                    viewNoticias.Visibility = Visibility.Collapsed;
                    viewAgenda.Visibility = Visibility.Collapsed;
                    GridTituloAgenda.Visibility = Visibility.Collapsed;

                    this.gridPrincipal.Children.Remove(gridPrincipal.Children[3]);

                    flagBack = 0;
                    break;

                case 4:

                    backButton.Visibility = Visibility.Collapsed;
                    gridNoticias.Visibility = Visibility.Visible;
                    grdCategoria.Visibility = Visibility.Visible;
                    viewNoticias.Visibility = Visibility.Collapsed;
                    flagBack = 0;
                    Constantes.intFlagBack = 0;
                    viewAgenda.Visibility = Visibility.Collapsed;
                    GridTituloAgenda.Visibility = Visibility.Collapsed;
                    this.gridPrincipal.Children.Remove(gridPrincipal.Children[3]);
                    break;


                case 6:
                    viewAgenda.Visibility = Visibility.Visible;
                    GridTituloAgenda.Visibility = Visibility.Visible;
                    backButton.Visibility = Visibility.Visible;
                    gridNoticias.Visibility = Visibility.Collapsed;
                    //gridDetalheNoticia.Visibility = Visibility.Visible;
                    grdCategoria.Visibility = Visibility.Collapsed;
                    this.gridPrincipal.Children.Remove(gridPrincipal.Children[3]);
                    flagBack = 4;
                    Constantes.intFlagBack = 4;
                    break;

                case 7:
                    viewAgenda.Visibility = Visibility.Visible;
                    GridTituloAgenda.Visibility = Visibility.Visible;
                    backButton.Visibility = Visibility.Visible;
                    gridNoticias.Visibility = Visibility.Collapsed;
                    //gridDetalheNoticia.Visibility = Visibility.Visible;
                    grdCategoria.Visibility = Visibility.Collapsed;
                    this.gridPrincipal.Children.Remove(gridPrincipal.Children[3]);
                    flagBack = 0;
                    Constantes.intFlagBack = 4;
                    break;

                default:

                    break;
            }

        }

        public bool ConstruaGridAgenda()
        {

            if (categoriaVO.Id == 2)
            {
                viewAgenda.Visibility = Visibility.Visible;
                GridTituloAgenda.Visibility = Visibility.Visible;
                backButton.Visibility = Visibility.Visible;
                gridNoticias.Visibility = Visibility.Collapsed;
                grdCategoria.Visibility = Visibility.Collapsed;

                return true;
            }

            if (categoriaVO.Id == 1)
            {
                viewAgenda.Visibility = Visibility.Visible;
                GridTituloAgenda.Visibility = Visibility.Visible;
                backButton.Visibility = Visibility.Visible;
                gridNoticias.Visibility = Visibility.Collapsed;
                grdCategoria.Visibility = Visibility.Collapsed;

                return false;

            }

            return false;
        }

        private async void gridNoticias_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                #region Google Analytic Evento

                var item = (ItemVO)e.ClickedItem;

                Dictionary<string, string> dictionary = new Dictionary<string, string>();

                dictionary.Add(item.Guid.ToString(), item.Titulo);

                using (Tracker tracker = new Tracker("UA-1974624-63", ""))
                {
                    await tracker.TrackEventAsync("MainPage", "Click", dictionary, default(string));
                }
                #endregion


                if (((ItemVO)e.ClickedItem).Titulo.Equals("Cinema"))
                {


                    if (DataSourceRadio.GrupoAgendaCulturalVO.ListaItemAgendaCultural.Count > 0)
                    {
                        objGrupoAgendaCulturalVO = new GrupoAgendaCulturalVO();
                        objGrupoAgendaCulturalVO.ListaItemAgendaCultural = Util.ToObservableCollection(DataSourceRadio.GrupoAgendaCulturalVO.ListaItemAgendaCultural.Where(i => i.IsCinema.Equals(true)));

                        objGrupoAgendaCulturalVO.Titulo = ((ItemVO)e.ClickedItem).Titulo;
                        this.DefaultViewModel["AgendaCulturalVO"] = objGrupoAgendaCulturalVO;
                        this.txtAgendaCultural.Text = ((ItemVO)e.ClickedItem).Titulo;
                        ConstruaGridAgenda();
                        Constantes.intFlagBack = 4;
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
                else if (((ItemVO)e.ClickedItem).Titulo.Equals("Teatro"))
                {


                    if (DataSourceRadio.GrupoAgendaCulturalVO.ListaItemAgendaCultural.Count > 0)
                    {
                        objGrupoAgendaCulturalVO = new GrupoAgendaCulturalVO();
                        objGrupoAgendaCulturalVO.ListaItemAgendaCultural = Util.ToObservableCollection(DataSourceRadio.GrupoAgendaCulturalVO.ListaItemAgendaCultural.Where(i => i.IdTipoAgendaCultural.Equals(5)));

                        objGrupoAgendaCulturalVO.Titulo = ((ItemVO)e.ClickedItem).Titulo;
                        this.DefaultViewModel["AgendaCulturalVO"] = objGrupoAgendaCulturalVO;
                        this.txtAgendaCultural.Text = ((ItemVO)e.ClickedItem).Titulo;
                        ConstruaGridAgenda();
                        Constantes.intFlagBack = 4;
                        return;
                    }
                    else
                    {
                        return;
                    }
                }

                else if (((ItemVO)e.ClickedItem).Titulo.Equals("Exposições"))
                {

                    if (DataSourceRadio.GrupoAgendaCulturalVO.ListaItemAgendaCultural.Count > 0)
                    {
                        objGrupoAgendaCulturalVO = new GrupoAgendaCulturalVO();
                        objGrupoAgendaCulturalVO.ListaItemAgendaCultural = Util.ToObservableCollection(DataSourceRadio.GrupoAgendaCulturalVO.ListaItemAgendaCultural.Where(i => i.IdTipoAgendaCultural.Equals(4)));
                        objGrupoAgendaCulturalVO.Titulo = ((ItemVO)e.ClickedItem).Titulo;
                        this.DefaultViewModel["AgendaCulturalVO"] = objGrupoAgendaCulturalVO;
                        this.txtAgendaCultural.Text = ((ItemVO)e.ClickedItem).Titulo;
                        ConstruaGridAgenda();
                        Constantes.intFlagBack = 4;
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
                else if (((ItemVO)e.ClickedItem).Titulo.Equals("Shows"))
                {


                    if (DataSourceRadio.GrupoAgendaCulturalVO.ListaItemAgendaCultural.Count > 0)
                    {
                        objGrupoAgendaCulturalVO.ListaItemAgendaCultural = Util.ToObservableCollection(DataSourceRadio.GrupoAgendaCulturalVO.ListaItemAgendaCultural.Where(i => i.IdTipoAgendaCultural.Equals(2)));

                        objGrupoAgendaCulturalVO.Titulo = ((ItemVO)e.ClickedItem).Titulo;
                        this.DefaultViewModel["AgendaCulturalVO"] = objGrupoAgendaCulturalVO;
                        this.txtAgendaCultural.Text = ((ItemVO)e.ClickedItem).Titulo;
                        ConstruaGridAgenda();
                        Constantes.intFlagBack = 4;
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
                if (((ItemVO)e.ClickedItem).IdChannel.Equals(72))
                {


                    if (DataSourceRadio.ListaChannelProgramacaoRadioVO.Count > 0)
                    {
                        backButton.Visibility = Visibility.Visible;
                        gridNoticias.Visibility = Visibility.Collapsed;
                        grdCategoria.Visibility = Visibility.Collapsed;
                        viewNoticias.Visibility = Visibility.Visible;

                        Constantes.intFlagBack = 4;

                        DefaultViewModel["Cor"] = Constantes.strCor;
                        DefaultViewModel["Title"] = ((ItemVO)(e.ClickedItem)).Titulo;
                        DefaultViewModel["Subtitle"] = ((ItemVO)(e.ClickedItem)).SubTitulo;
                        DefaultViewModel["Conteudo"] = ((ItemVO)(e.ClickedItem)).Descricao;
                        DefaultViewModel["Cor"] = Constantes.strCor;
                        DefaultViewModel["Image"] = ((ItemVO)(e.ClickedItem)).ImagemFundo;
                        return;

                    }
                    else
                    {
                        return;
                    }
                }
                if (((ItemVO)e.ClickedItem).Guid.Equals(399))
                {


                    if (DataSourceRadio.ListaChannelPodCastVO.Count > 0)
                    {
                        backButton.Visibility = Visibility.Visible;
                        gridNoticias.Visibility = Visibility.Collapsed;
                        grdCategoria.Visibility = Visibility.Collapsed;
                        viewNoticias.Visibility = Visibility.Visible;

                        DataSourceRadio.DetalhamentoNoticia.Titulo = ((ItemVO)(e.ClickedItem)).Titulo;
                        DataSourceRadio.DetalhamentoNoticia.SubTitulo = ((ItemVO)(e.ClickedItem)).SubTitulo;
                        DataSourceRadio.DetalhamentoNoticia.LegendaImagemNoticia = ((ItemVO)(e.ClickedItem)).Descricao;
                        Constantes.intFlagBack = 4;


                        if (((ItemVO)e.ClickedItem).Guid.Equals(399))
                        {
                            formatado = Util.ReplaceSpecialCharacters(((ItemVO)(e.ClickedItem)).SubTitulo);
                        }
                        else
                        {
                            formatado = Util.ReplaceSpecialCharacters(((ItemVO)(e.ClickedItem)).SubTitulo);

                        }

                        if (((ItemVO)e.ClickedItem).Guid.Equals(399))
                        {
                            btnLinkPodcast.Visibility = Windows.UI.Xaml.Visibility.Visible;
                        }





                        if (((ItemVO)e.ClickedItem).Guid.Equals(399))
                        {
                            //this.StopSound();
                            //this.Stop();

                            Uri uriRadio = new Uri(((ItemVO)(e.ClickedItem)).Link);

                            //var uriStreamReference = RandomAccessStreamReference.CreateFromUri(uriRadio);
                            //var uriStream = await uriStreamReference.OpenReadAsync();
                            //mediaPlayer.SetSource(uriStream, "audio/mp3");

                            Uri strLink = new Uri(((ItemVO)(e.ClickedItem)).Link);
                            WebView wvPodCast = new WebView();
                            wvPodCast.Source = strLink;


                            HyperlinkButton hlbPodcast = new HyperlinkButton();

                            hlbPodcast.NavigateUri = uriRadio;


                            btnLinkPodcast.Name = "btnLinkPodcast";
                            btnLinkPodcast.Height = 50;
                            btnLinkPodcast.Width = 380;
                            btnLinkPodcast.Margin = new Thickness(0, -330, 0, 0);
                            btnLinkPodcast.Content = "Clique aqui para acessar o podcast";
                            this.itemLinkPromocao = ((ItemVO)(e.ClickedItem)).Link;
                            btnLinkPodcast.Click += new RoutedEventHandler(btnPodcast_Click);
                            btnLinkPodcast.Foreground = Util.GetColorFromHexa("#FFFFFF");
                            btnLinkPodcast.Background = Util.GetColorFromHexa("#343434");
                            btnLinkPodcast.BorderBrush = Util.GetColorFromHexa("#343434");

                            gridPrincipal.Children.Add(btnLinkPodcast);

                            //wvPodCast.LoadCompleted += new LoadCompletedEventHandler(wvPodCast_LoadCompleted);
                            //wvPodCast.Loaded += new RoutedEventHandler(wvPodCast_Loaded);
                            //wvPodCast.Height = 500;
                            //wvPodCast.Width = 500;
                            //gridPrincipal.Children.Add(wvPodCast);
                            formatado = null;
                        }


                        XmlDocument document = new XmlDocument();

                        if (formatado != null)
                        {
                            document.LoadXml(formatado);
                        }

                        DataSourceRadio.DetalhamentoNoticia.Conteudo = document.InnerText;
                        DefaultViewModel["Cor"] = Constantes.strCor;
                        DefaultViewModel["Title"] = DataSourceRadio.DetalhamentoNoticia.Titulo;
                        DefaultViewModel["Subtitle"] = DataSourceRadio.DetalhamentoNoticia.SubTitulo;
                        DefaultViewModel["Description"] = DataSourceRadio.DetalhamentoNoticia.LegendaImagemNoticia;
                        DefaultViewModel["Conteudo"] = DataSourceRadio.DetalhamentoNoticia.Conteudo;
                        DefaultViewModel["Image"] = String.Empty;
                        return;

                    }
                    else
                    {
                        return;
                    }
                }



                //////////////////


                if (((ItemVO)e.ClickedItem).IdChannel.Equals(222))
                {


                    if (DataSourceRadio.ListaChannelSalvadorVO.Count > 0)
                    {
                        backButton.Visibility = Visibility.Visible;
                        gridNoticias.Visibility = Visibility.Collapsed;
                        grdCategoria.Visibility = Visibility.Collapsed;
                        viewNoticias.Visibility = Visibility.Visible;

                        DataSourceRadio.DetalhamentoNoticia.Titulo = ((ItemVO)(e.ClickedItem)).Titulo;
                        DataSourceRadio.DetalhamentoNoticia.SubTitulo = ((ItemVO)(e.ClickedItem)).SubTitulo;
                        DataSourceRadio.DetalhamentoNoticia.LegendaImagemNoticia = ((ItemVO)(e.ClickedItem)).Descricao;
                        DataSourceRadio.DetalhamentoNoticia.ImagemNoticia = ((ItemVO)(e.ClickedItem)).ImagemFundo;
                        Constantes.intFlagBack = 4;


                        if (((ItemVO)e.ClickedItem).IdChannel.Equals(222))
                        {
                            formatado = Util.ReplaceSpecialCharacters(((ItemVO)(e.ClickedItem)).SubTitulo);
                        }
                        else
                        {
                            formatado = Util.ReplaceSpecialCharacters(((ItemVO)(e.ClickedItem)).StrConteudo);

                        }

                        if (((ItemVO)e.ClickedItem).IdChannel.Equals("222"))
                        {

                            btnLinkPodcast.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                            //btnLinkPodcastExterno.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                        }



                        formatado = "<body>" + formatado + "</body>";



                        XmlDocument document = new XmlDocument();

                        if (formatado != null)
                        {
                            document.LoadXml(formatado);
                        }

                        DataSourceRadio.DetalhamentoNoticia.Conteudo = document.InnerText;
                        DefaultViewModel["Cor"] = Constantes.strCor;
                        DefaultViewModel["Title"] = DataSourceRadio.DetalhamentoNoticia.Titulo;
                        DefaultViewModel["Subtitle"] = DataSourceRadio.DetalhamentoNoticia.SubTitulo;
                        DefaultViewModel["Description"] = DataSourceRadio.DetalhamentoNoticia.LegendaImagemNoticia;
                        DefaultViewModel["Conteudo"] = DataSourceRadio.DetalhamentoNoticia.Conteudo;
                        DefaultViewModel["Image"] = DataSourceRadio.DetalhamentoNoticia.ImagemNoticia.Url;
                        DefaultViewModel["Cor"] = Constantes.strCor;
                        return;

                    }
                    else
                    {
                        return;
                    }
                }
                else
                {

                    backButton.Visibility = Visibility.Visible;
                    gridNoticias.Visibility = Visibility.Collapsed;
                    grdCategoria.Visibility = Visibility.Collapsed;
                    viewNoticias.Visibility = Visibility.Visible;

                    DataSourceRadio.DetalhamentoNoticia.Titulo = ((ItemVO)(e.ClickedItem)).Titulo;
                    DataSourceRadio.DetalhamentoNoticia.SubTitulo = ((ItemVO)(e.ClickedItem)).SubTitulo;
                    DataSourceRadio.DetalhamentoNoticia.LegendaImagemNoticia = ((ItemVO)(e.ClickedItem)).Descricao;
                    DataSourceRadio.DetalhamentoNoticia.ImagemNoticia = ((ItemVO)(e.ClickedItem)).ImagemFundo;
                    Constantes.intFlagBack = 4;


                    if (((ItemVO)e.ClickedItem).Titulo.Equals("Notícias CBN"))
                    {
                        formatado = Util.ReplaceSpecialCharacters(((ItemVO)(e.ClickedItem)).Descricao);
                    }
                    else
                    {
                        formatado = Util.ReplaceSpecialCharacters(((ItemVO)(e.ClickedItem)).StrConteudo);

                    }

                    if (((ItemVO)e.ClickedItem).IdChannel.Equals("299"))
                    {
                        btnLinkPodcast.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                        //btnLinkPodcastExterno.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    }



                    formatado = "<body>" + formatado + "</body>";



                    XmlDocument document = new XmlDocument();

                    if (formatado != null)
                    {
                        document.LoadXml(formatado);
                    }

                    DataSourceRadio.DetalhamentoNoticia.Conteudo = document.InnerText;
                    DefaultViewModel["Cor"] = Constantes.strCor;
                    DefaultViewModel["Title"] = DataSourceRadio.DetalhamentoNoticia.Titulo;
                    DefaultViewModel["Subtitle"] = DataSourceRadio.DetalhamentoNoticia.SubTitulo;
                    DefaultViewModel["Description"] = DataSourceRadio.DetalhamentoNoticia.LegendaImagemNoticia;
                    DefaultViewModel["Conteudo"] = DataSourceRadio.DetalhamentoNoticia.Conteudo;

                    DefaultViewModel["Cor"] = Constantes.strCor;
                    return;

                }
                /////////////


                if (((ItemVO)e.ClickedItem).IdChannel.Equals(12))
                {


                    if (DataSourceRadio.ListaChannelSalvadorVO.Count > 0)
                    {
                        backButton.Visibility = Visibility.Visible;
                        gridNoticias.Visibility = Visibility.Collapsed;
                        grdCategoria.Visibility = Visibility.Collapsed;
                        viewNoticias.Visibility = Visibility.Visible;

                        DataSourceRadio.DetalhamentoNoticia.Titulo = ((ItemVO)(e.ClickedItem)).Titulo;
                        DataSourceRadio.DetalhamentoNoticia.SubTitulo = ((ItemVO)(e.ClickedItem)).SubTitulo;
                        DataSourceRadio.DetalhamentoNoticia.LegendaImagemNoticia = ((ItemVO)(e.ClickedItem)).Descricao;
                        DataSourceRadio.DetalhamentoNoticia.ImagemNoticia = ((ItemVO)(e.ClickedItem)).ImagemFundo;
                        Constantes.intFlagBack = 4;


                        if (((ItemVO)e.ClickedItem).IdChannel.Equals(12))
                        {
                            formatado = Util.ReplaceSpecialCharacters(((ItemVO)(e.ClickedItem)).Descricao);
                        }
                        else
                        {
                            formatado = Util.ReplaceSpecialCharacters(((ItemVO)(e.ClickedItem)).StrConteudo);

                        }

                        if (((ItemVO)e.ClickedItem).IdChannel.Equals("12"))
                        {

                            btnLinkPodcast.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                            //btnLinkPodcastExterno.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                        }



                        formatado = "<body>" + formatado + "</body>";

                        if (((ItemVO)e.ClickedItem).Guid.Equals(299))
                        {
                            //this.StopSound();
                            //this.Stop();

                            Uri uriRadio = new Uri(((ItemVO)(e.ClickedItem)).StrConteudo);

                            //var uriStreamReference = RandomAccessStreamReference.CreateFromUri(uriRadio);
                            //var uriStream = await uriStreamReference.OpenReadAsync();
                            //mediaPlayer.SetSource(uriStream, "audio/mp3");



                            //HyperlinkButton hlbPodcast = new HyperlinkButton();

                            //hlbPodcast.NavigateUri = uriRadio;


                            //btnLinkPromocao.Name = "btnPromocao";
                            //btnLinkPromocao.Height = 50;
                            //btnLinkPromocao.Width = 350;
                            //btnLinkPromocao.Margin = new Thickness(500, -700, 0, 0);
                            //btnLinkPromocao.Content = "Clique aqui para acessar o podcast";
                            //this.itemLinkPromocao = ((ItemVO)(e.ClickedItem)).StrConteudo;
                            //btnLinkPromocao.Click += new RoutedEventHandler(btnPromocao_Click);

                            //gridPrincipal.Children.Add(btnLinkPromocao);

                            formatado = null;
                        }

                        XmlDocument document = new XmlDocument();

                        if (formatado != null)
                        {
                            document.LoadXml(formatado);
                        }

                        DataSourceRadio.DetalhamentoNoticia.Conteudo = document.InnerText;
                        DefaultViewModel["Cor"] = Constantes.strCor;
                        DefaultViewModel["Title"] = DataSourceRadio.DetalhamentoNoticia.Titulo;
                        DefaultViewModel["Subtitle"] = DataSourceRadio.DetalhamentoNoticia.SubTitulo;
                        DefaultViewModel["Description"] = DataSourceRadio.DetalhamentoNoticia.LegendaImagemNoticia;
                        DefaultViewModel["Conteudo"] = DataSourceRadio.DetalhamentoNoticia.Conteudo;
                        DefaultViewModel["Image"] = DataSourceRadio.DetalhamentoNoticia.ImagemNoticia.Url;
                        DefaultViewModel["Cor"] = Constantes.strCor;
                        return;

                    }
                    else
                    {
                        return;
                    }
                }
                else
                {

                    backButton.Visibility = Visibility.Visible;
                    gridNoticias.Visibility = Visibility.Collapsed;
                    grdCategoria.Visibility = Visibility.Collapsed;
                    viewNoticias.Visibility = Visibility.Visible;

                    DataSourceRadio.DetalhamentoNoticia.Titulo = ((ItemVO)(e.ClickedItem)).Titulo;
                    DataSourceRadio.DetalhamentoNoticia.SubTitulo = ((ItemVO)(e.ClickedItem)).SubTitulo;
                    DataSourceRadio.DetalhamentoNoticia.LegendaImagemNoticia = ((ItemVO)(e.ClickedItem)).Descricao;
                    DataSourceRadio.DetalhamentoNoticia.ImagemNoticia = ((ItemVO)(e.ClickedItem)).ImagemFundo;
                    Constantes.intFlagBack = 4;


                    if (((ItemVO)e.ClickedItem).Titulo.Equals("Notícias CBN"))
                    {
                        formatado = Util.ReplaceSpecialCharacters(((ItemVO)(e.ClickedItem)).Descricao);
                    }
                    else
                    {
                        formatado = Util.ReplaceSpecialCharacters(((ItemVO)(e.ClickedItem)).StrConteudo);

                    }

                    if (((ItemVO)e.ClickedItem).IdChannel.Equals("299"))
                    {
                        btnLinkPodcast.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                        //btnLinkPodcastExterno.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    }



                    formatado = "<body>" + formatado + "</body>";



                    XmlDocument document = new XmlDocument();

                    if (formatado != null)
                    {
                        document.LoadXml(formatado);
                    }

                    DataSourceRadio.DetalhamentoNoticia.Conteudo = document.InnerText;
                    DefaultViewModel["Cor"] = Constantes.strCor;
                    DefaultViewModel["Title"] = DataSourceRadio.DetalhamentoNoticia.Titulo;
                    DefaultViewModel["Subtitle"] = DataSourceRadio.DetalhamentoNoticia.SubTitulo;
                    DefaultViewModel["Description"] = DataSourceRadio.DetalhamentoNoticia.LegendaImagemNoticia;
                    DefaultViewModel["Conteudo"] = DataSourceRadio.DetalhamentoNoticia.Conteudo;

                    DefaultViewModel["Cor"] = Constantes.strCor;
                    return;

                }

            }

            catch (Exception)
            {


                if (!string.IsNullOrEmpty(DataSourceRadio.DetalhamentoNoticia.Titulo))
                {
                    DefaultViewModel["Title"] = DataSourceRadio.DetalhamentoNoticia.Titulo;
                    DefaultViewModel["Subtitle"] = DataSourceRadio.DetalhamentoNoticia.SubTitulo;

                    if (DataSourceRadio.DetalhamentoNoticia.ImagemNoticia.Url != null)
                    {
                        DefaultViewModel["Image"] = DataSourceRadio.DetalhamentoNoticia.ImagemNoticia.Url;
                    }
                    DefaultViewModel["Cor"] = Constantes.strCor;
                    HtmlToText html = new HtmlToText();

                    //if (e.ClickedItem != null)
                    //{
                    //    btnLinkPromocao.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    //    btnLinkPromocao.Name = "btnPromocao";
                    //    btnLinkPromocao.Height = 50;
                    //    btnLinkPromocao.Width = 350;
                    //    btnLinkPromocao.Margin = new Thickness(470, -558, 0, 0);
                    //    btnLinkPromocao.Content = "Clique aqui para acessar a promoção";


                    //    this.itemLinkPromocao = ((ItemVO)(e.ClickedItem)).Link;
                    //}

                    //btnLinkPromocao.Click += new RoutedEventHandler(btnPromocao_Click);

                    //gridPrincipal.Children.Add(btnLinkPromocao);

                    DefaultViewModel["Conteudo"] = html.ConvertHtml(formatado);
                }
            }
            finally
            {
                flagBack = 1;
                DataSourceRadio.DetalhamentoNoticia = new DetalhamentoNoticiasVO();

            }





        }

        protected async void btnPodcast_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.itemLinkPromocao))
            {
                await Windows.System.Launcher.LaunchUriAsync(new Uri(this.itemLinkPromocao));
            }
            else
            {
                Flyout.TelaMensagem.Instance.ShowMensagem("Página fora do ar, tente mais tarde!");
            }
        }

        /// <summary>
        /// Método  clique do menu.
        /// </summary>
        /// <param name="sender">Objeto sender</param>
        /// <param name="e">Argumento seleção</param>
        private void grdCategoria_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {

            try
            {

                if (!string.IsNullOrEmpty(((Menu)(((FrameworkElement)(((RoutedEventArgs)(e)).OriginalSource)).DataContext)).Categoria))
                {
                    string strCategoriaMenu = ((Menu)(((FrameworkElement)(((RoutedEventArgs)(e)).OriginalSource)).DataContext)).Categoria;
                    this.DefaultViewModel["groupedItemsViewSource"] = lstCategoria.Where(i => i.NomeCategoria.Equals(strCategoriaMenu)).ToList();

                    groupedItemsViewSource.Source = this.DefaultViewModel["groupedItemsViewSource"];
                }
            }
            catch (Exception)
            {

                return;
            }
        }

        private void pageRoot_Loaded(object sender, RoutedEventArgs e)
        {
            this.LoadMain(3);
            this.LoadMain(1);
            this.LoadMainAgenda("Cinema");
            this.LoadMainAgenda("Teatro");
            this.LoadMainAgenda("Exposições");
            this.LoadMainAgenda("Shows");

            grupoNoticiasItemBLL.LoadDataPromocao();
            grupoNoticiasItemBLL.LoadDataNoticiaCorreio();
            flagBack = 0;
            isBlockOnSelected = true;
            this.GoBack(this, new RoutedEventArgs());
            radiosGrid_Tapped(this, new Windows.UI.Xaml.Input.TappedRoutedEventArgs());
        }

        /// <summary>
        /// Evento click do listview Agenda Cultural 
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">ItemClickEventArgs e</param>
        private void ItemGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            #region Google Analytic Evento
            var item = (View.ItemAgendaCulturalVO)e.ClickedItem;

            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            dictionary.Add(item.Guid.ToString(), "Agenda Cultural");

            using (Tracker tracker = new Tracker("UA-1974624-63", ""))
            {
                tracker.TrackEventAsync("MainPage", "Click", dictionary, default(string));
            }

            #endregion

            View.ItemAgendaCulturalVO subItemSelecionado = (View.ItemAgendaCulturalVO)e.ClickedItem;

            ////Verifica se agenda cultural for cinema
            if (subItemSelecionado.IsCinema)
            {
                flagBack = 2;
                if (this.gridPrincipal.Children.Count < 4)
                {
                    this.gridPrincipal.Children.Add(objDetalheAgendaCinema);
                }
                else
                {
                    this.gridPrincipal.Children.Remove(gridPrincipal.Children[3]);
                    this.gridPrincipal.Children.Add(objDetalheAgendaCinema);
                }
                objDetalheAgendaCinema.Visibility = Visibility.Visible;
                viewAgenda.Visibility = Visibility.Collapsed;
                GridTituloAgenda.Visibility = Visibility.Collapsed;
                objDetalheAgendaCinema.LoadState(subItemSelecionado.Guid);
                objDetalheAgendaCinema.UpdateLayout();
            }
            else
            {
                flagBack = 3;
                int intTipoAgendaCultural = ((View.ItemAgendaCulturalVO)(e.ClickedItem)).IdTipoAgendaCultural;
                this.gridPrincipal.Children.Add(objDetalheAgendaCultural);
                objDetalheAgendaCultural.Visibility = Visibility.Visible;
                viewAgenda.Visibility = Visibility.Collapsed;
                GridTituloAgenda.Visibility = Visibility.Collapsed;
                objDetalheAgendaCultural.LoadState(subItemSelecionado.Guid, intTipoAgendaCultural);
                objDetalheAgendaCultural.UpdateLayout();

            }
        }



        private void btnFacebookGeral_Click(object sender, RoutedEventArgs e)
        {
            Launcher.LaunchUriAsync(new Uri(Util.ShareFacebook(@"https://www.facebook.com/CBNSalvador?fref=ts")));
        }


        private void btnTwiterGeral_Click(object sender, RoutedEventArgs e)
        {
            Launcher.LaunchUriAsync(new Uri(Util.ShareTwitter(@"https://twitter.com/cbn_salvador", "CBN FM")));

        }

        private void btnInstagramGeral_Click(object sender, RoutedEventArgs e)
        {
            Launcher.LaunchUriAsync(new Uri("http://instagram.com/cbnoficial"));
        }



        private void banner_Loaded(object sender, RoutedEventArgs e)
        {

            try
            {
                Uri targetUri = new Uri(@"http://www.centraldedownload.com.br/Radio/CBN/SquareBannerPage.html");
                banner.Navigate(targetUri);

                banner.UpdateLayout();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void wvPodCast_Loaded(object sender, RoutedEventArgs e)
        {

            try
            {
                Uri targetUri = new Uri(this.itemLinkPromocao);
                wvPodCast.Navigate(targetUri);

                wvPodCast.UpdateLayout();
            }
            catch (Exception)
            {

                throw;
            }
        }

        //private void banner_Copy_Loaded(object sender, RoutedEventArgs e)
        //{

        //    try
        //    {
        //        Uri targetUriSnapped = new Uri(@"http://www.centraldedownload.com.br/Radio/CBN/SquareBannerPageSnapped.html");
        //        banner_Copy.Navigate(targetUriSnapped);
        //        banner_Copy.UpdateLayout();
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //private void wvPodCast_LoadCompleted(object sender, NavigationEventArgs e)
        //{
        //    try
        //    {
        //        Uri targetUri = new Uri(this.itemLinkPromocao);
        //        wvPodCast.Navigate(targetUri);

        //        wvPodCast.UpdateLayout();
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //private async void banner_LoadCompleted(object sender, NavigationEventArgs e)
        //{
        //    try
        //    {
        //        await Task.Delay(TimeSpan.FromSeconds(20));
        //        Uri targetUri = new Uri(@"http://www.centraldedownload.com.br/Radio/CBN/SquareBannerPage.html");
        //        banner.Navigate(targetUri);

        //        banner.UpdateLayout();
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //private async void banner_Copy_LoadCompleted(object sender, NavigationEventArgs e)
        //{
        //    try
        //    {
        //        await Task.Delay(TimeSpan.FromSeconds(20));
        //        Uri targetUriSnapped = new Uri(@"http://www.centraldedownload.com.br/Radio/CBN/SquareBannerPageSnapped.html");
        //        banner_Copy.Navigate(targetUriSnapped);

        //        banner_Copy.UpdateLayout();
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //void banner_ScriptNotify(object sender, NotifyEventArgs e)
        //{
        //    // Respond to the script notification.
        //}

    }
}
