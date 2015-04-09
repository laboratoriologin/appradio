using RadioPlayer.Beans;
using RadioPlayer.Beans.BO;
using RadioPlayer.Beans.ValueObject.ValueObject.View.AgendaCultural;
using RadioPlayer.Beans.ValueObject.ValueObject.Xml.AgendaCulturalXML.Cinema.Cartaz;
using RadioPlayer.Common.Architecture;
using RadioPlayer.Flyout;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace RadioPlayer
{
    public sealed partial class DetalheAgendaCinema : RadioPlayer.Common.LayoutAwarePage
    {
        /// <summary>
        /// Atríbuto web
        /// </summary>
        private WebView web = new WebView();

        ///// <summary>
        ///// Atríbuto evento
        ///// </summary>
        //private CinemaVO evento = null;

        /// <summary>
        /// Atríbuto beginEnderecoYoutube
        /// </summary>
        private string beginEnderecoYoutube = "<html><head><style>html, body { height:100%; overflow:hidden; }body { margin:0; }</style></head><body><iframe width='100%' height='100%' src='";

        /// <summary>
        /// Atríbuto endEnderecoYoutube
        /// </summary>
        private string endEnderecoYoutube = "'></iframe></body></html>";

        /// <summary>
        /// Atríbuto embeddYoutube
        /// </summary>
        private string embeddYoutube = "http://www.youtube.com/embed/";

        /// <summary>
        /// Atríbuto identificador do filme selecionado
        /// </summary>
        private int idFilmeSelecionado;
        public DetalheAgendaCinema()
        {
            this.InitializeComponent();
      

        }
        /// <summary>
        /// Gets or sets - propriedade Web
        /// </summary>
        public WebView Web
        {
            get { return this.web; }
            set { this.web = value; }
        }

        /// <summary>
        /// Gets or sets - propriedade EmbeddYoutube
        /// </summary>
        public string EmbeddYoutube
        {
            get { return this.embeddYoutube; }
            set { this.embeddYoutube = value; }
        }

        /// <summary>
        /// Gets or sets - propriedade BeginEnderecoYoutube
        /// </summary>
        public string BeginEnderecoYoutube
        {
            get { return this.beginEnderecoYoutube; }
            set { this.beginEnderecoYoutube = value; }
        }

        /// <summary>
        /// Gets or sets - propriedade EndEnderecoYoutube
        /// </summary>
        public string EndEnderecoYoutube
        {
            get { return this.endEnderecoYoutube; }
            set { this.endEnderecoYoutube = value; }
        }

        ///// <summary>
        ///// Populates the page with content passed during navigation.  Any saved state is also
        ///// provided when recreating a page from a prior session.
        ///// </summary>
        ///// <param name="navigationParameter">The parameter value passed to
        ///// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        ///// </param>
        ///// <param name="pageState">A dictionary of state preserved by this page during an earlier
        ///// session.  This will be null the first time a page is visited.</param>
        public void LoadState(object navigationParameter)
        {
            if (Convert.ToInt32(navigationParameter) > 0)
            {
                DataSourceRadio.CurrentPage = this;

                //// Allow saved page state to override the initial item to display
                //if (pageState != null && pageState.ContainsKey("SelectedItem"))
                //{
                //    navigationParameter = pageState["SelectedItem"];
                //}

                DataSourceRadio.IsUpdatingAgendaCulturalDetalhe = true;
                PropertyComponentChange(indeterminateProgressBar1, AppBarUpdateDataBtn);

                DataSourceRadio.CinemaVO = new CinemaVO();

                this.idFilmeSelecionado = Convert.ToInt32(navigationParameter);

                this.LoadMain(this.idFilmeSelecionado);

                this.DefaultViewModel["itemsViewSource"] = DataSourceRadio.CinemaVO;
                //this.itemsViewSource.Source = this.DefaultViewModel["itemsViewSource"];
                this.LoadData();
                

                //WebViewLoaded();

                //webView.Loaded += WebView_Loaded;
                ////webViewSnapped.Loaded += WebView_Loaded;
                //webView.UpdateLayout();

                this.UpdateLayout();
            }
        }


        /// <summary>
        /// Método responsavel em carregar os objetos da tela.
        /// </summary>
        protected void LoadData()
        {

            if (Util.IsConnectedToInternet())
            {
                this.pageTitle.Text = DataSourceRadio.CinemaVO.TituloPagina;
                this.pageTitle.Foreground = Util.GetColorFromHexa("#FFFFFFFF");

                this.titulo.Text = "Trailer";
                this.titulo.Foreground = Util.GetColorFromHexa(Constantes.strCor);

                this.maisInfo.Text = DataSourceRadio.CinemaVO.TituloEvento;
                this.maisInfo.Foreground = Util.GetColorFromHexa(Constantes.strCor);

                this.detalhes.Text = DataSourceRadio.CinemaVO.TituloDetalhesEvento;
                this.detalhes.Foreground = Util.GetColorFromHexa(Constantes.strCor);

                this.emCartaz.Text = DataSourceRadio.CinemaVO.TituloCartazEvento;
                this.emCartaz.Foreground = Util.GetColorFromHexa(Constantes.strCor);

                this.texto1.Text = DataSourceRadio.CinemaVO.MaisInfoEvento.ConteudoItemDetalhe;
                this.texto1.Foreground = Util.GetColorFromHexa(Constantes.strCor);

                this.ListaEmCartaz.ItemsSource = DataSourceRadio.CinemaVO.ListaOndeEstaPassando;
                this.ListaEmCartaz.Foreground = Util.GetColorFromHexa(Constantes.strCor);

                this.ListaDetalhesEvento.ItemsSource = DataSourceRadio.CinemaVO.ListaDetalhesEvento.ToList();
                this.ListaDetalhesEvento.Foreground = Util.GetColorFromHexa(Constantes.strCor);

                Constantes.intFlagBack = 7;
                this.UpdateLayout();
            }

            else
            {
                TelaMensagem.Instance.ShowMensagem(Constantes.ResourceErro.GetString("Conexao").ToString());
            }

        }

        /// <summary>
        /// Método LoadMain
        /// </summary>
        /// <param name="guid">int guid</param>
        protected async void LoadMain(int guid)
        {
            try
            {
                DetalhamentoAgendaCulturalCinemaBLL detalhamentoAgendaCulturalCinemaBLL = new DetalhamentoAgendaCulturalCinemaBLL();
                ChannelCinemaVO channelCinemaVO = DataSourceRadio.ListaChannelCinemaVO.Where(itemCinema => itemCinema.Id == "filmes_em_cartaz").First<ChannelCinemaVO>();
                ItemCinemaVO itemCinemaVOSelect = channelCinemaVO.ListaItem.Where(item => item.Guid == guid).First<ItemCinemaVO>();

                if (itemCinemaVOSelect.Linksingle == null)
                    itemCinemaVOSelect.Linksingle = await XMLParserVO.LoadXmLinkSingleCinemaUrl(itemCinemaVOSelect.LinkLinksingle);

                detalhamentoAgendaCulturalCinemaBLL.LoadDataDetalhamentoAgendaCulturalCinema(itemCinemaVOSelect, guid);

                this.DefaultViewModel["TextoFacebook"] = Util.ShareFacebook(detalhamentoAgendaCulturalCinemaBLL.ItemCinemaVO.Link);
                this.DefaultViewModel["TextoTwitter"] = Util.ShareTwitter(detalhamentoAgendaCulturalCinemaBLL.ItemCinemaVO.Link, detalhamentoAgendaCulturalCinemaBLL.ItemCinemaVO.Title);
                this.DefaultViewModel["corTitulo"] = Constantes.strCor;

                DataSourceRadio.IsUpdatingAgendaCulturalDetalhe = false;
                PropertyComponentChange(indeterminateProgressBar1, AppBarUpdateDataBtn);


                WebViewLoaded();

                webView.Loaded += WebView_Loaded;
                //webViewSnapped.Loaded += WebView_Loaded;
                webView.UpdateLayout();

            }
            catch (Exception ex)
            {
                TelaMensagem.Instance.ShowMensagem(ex.Message);
      
            }
            //var _Frame = Window.Current.Content as Frame;
            //_Frame.Navigate(_Frame.Content.GetType());
            //_Frame.GoBack(); // remove from BackStack;
        }

        /// <summary>
        /// Evento webView_Loaded
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">RoutedEventArgs e</param>
        private void WebView_Loaded(object sender, RoutedEventArgs e)
        {
            this.Web = sender as WebView;
            string iframeYoutube = this.BeginEnderecoYoutube + this.EmbeddYoutube + DataSourceRadio.CinemaVO.Video + this.EndEnderecoYoutube;
            this.Web.NavigateToString(iframeYoutube);
        }

        private void WebViewLoaded()
        {
            this.Web = webView;
            string iframeYoutube = this.BeginEnderecoYoutube + this.EmbeddYoutube + DataSourceRadio.CinemaVO.Video + this.EndEnderecoYoutube;
            this.Web.NavigateToString(iframeYoutube);
        }
        /// <summary>
        /// Evento  AppBarDoneButton
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">RoutedEventArgs e</param>
        private void AppBarUpdateDataBtn_Click(object sender, RoutedEventArgs e)
        {
            DataSourceRadio.IsUpdatingAgendaCulturalDetalhe = true;
            this.PropertyComponentChange(indeterminateProgressBar1, AppBarUpdateDataBtn);

            DataSourceRadio.CinemaVO.TituloCartazEvento = string.Empty;
            DataSourceRadio.CinemaVO.TituloDetalhesEvento = string.Empty;
            DataSourceRadio.CinemaVO.TituloEvento = string.Empty;
            DataSourceRadio.CinemaVO.TituloPagina = string.Empty;

            //DataSourceRadio.CinemaVO.ListaAgendaCultural.Clear();
            DataSourceRadio.CinemaVO.ListaDetalhesEvento.Clear();
            DataSourceRadio.CinemaVO.ListaOndeEstaPassando.Clear();
            DataSourceRadio.CinemaVO.Video = string.Empty;

            DataSourceRadio.CinemaVO.MaisInfoEvento.ConteudoItemDetalhe = string.Empty;
            DataSourceRadio.CinemaVO.MaisInfoEvento.Endereco = string.Empty;
            DataSourceRadio.CinemaVO.MaisInfoEvento.TituloItemDetalhe = string.Empty;

            ChannelCinemaVO channelCinemaVO = DataSourceRadio.ListaChannelCinemaVO.Where(itemCinema => itemCinema.Id == "filmes_em_cartaz").First<ChannelCinemaVO>();
            ItemCinemaVO itemCinemaVOSelect = channelCinemaVO.ListaItem.Where(item => item.Guid == this.idFilmeSelecionado).First<ItemCinemaVO>();

            itemCinemaVOSelect.Linksingle = null;

            this.LoadMain(this.idFilmeSelecionado);
        }

        /// <summary>
        /// Evento ViewState
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e"> VisualStateChangedEventArgs e</param>
        private void ApplicationViewStates_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            if (e.NewState.Name.Equals(FullScreenLandscape.Name))
            {
                this.BeginEnderecoYoutube = "<html><head><style>html, body { height:100%; overflow:hidden; }body { margin:0; }</style></head><body><iframe width='920' height='640' src='";
                string iframeYoutube = this.BeginEnderecoYoutube + this.EmbeddYoutube + DataSourceRadio.CinemaVO.Video + this.EndEnderecoYoutube;
                this.Web.NavigateToString(iframeYoutube);
            }
            //else if (e.NewState.Name.Equals(Snapped.Name))
            //{
            //    this.BeginEnderecoYoutube = "<html><head><style>html, body { height:100%; overflow:hidden; }body { margin:0; }</style></head><body><iframe width='300' height='260' src='";
            //    string iframeYoutube = this.BeginEnderecoYoutube + this.EmbeddYoutube + DataSourceRadio.CinemaVO.Video + this.EndEnderecoYoutube;
            //    this.Web.NavigateToString(iframeYoutube);
            //}
        }

        public override void PropertyComponentChange(ProgressBar progressBarr, Button btn)
        {
            if (DataSourceRadio.IsUpdatingAgendaCulturalDetalhe)
            {
                progressBarr.IsIndeterminate = true;
                btn.IsEnabled = false;
            }
            else
            {
                progressBarr.IsIndeterminate = false;
                btn.IsEnabled = true;
            }
        }

        //private void usrAgendaCinema_Loaded(object sender, RoutedEventArgs e)
        //{
        //    this.LoadState(idFilmeSelecionado);
        //}


    }
}
