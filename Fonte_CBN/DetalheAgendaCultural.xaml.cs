using RadioPlayer.Beans;
using RadioPlayer.Beans.BO;
using View = RadioPlayer.Beans.ValueObject.ValueObject.View.AgendaCultural;
using XML = RadioPlayer.Beans.ValueObject.ValueObject.Xml.AgendaCulturalXML;
using RadioPlayer.Common.Architecture;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using RadioPlayer.Flyout;


// The Item Detail Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234232

namespace RadioPlayer
{
    /// <summary>
    /// A page that displays details for a single item within a group while allowing gestures to
    /// flip through other items belonging to the same group.
    /// </summary>
    public sealed partial class DetalheAgendaCultural : RadioPlayer.Common.LayoutAwarePage
    {
        public DetalheAgendaCultural()
        {
            this.InitializeComponent();

        }



        /// <summary>
        /// Atributo Identificador do channel da agenda cultural selecionado.
        /// </summary>
        private int idChannelSelecionado = 0;

        ///// <summary>
        ///// Atríbuto evento
        ///// </summary>
        //private View.TipoAgendaCulturalVO evento = null;

        /// <summary>
        /// Atríbuto beginEnderecoYoutube
        /// </summary>
        private string beginEnderecoYoutube = "<html><head><style>html, body { height:100%; overflow:hidden; }body { margin:0; }</style></head><body><iframe width='920' height='640' src='";

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
        private int idItemSelecionado;



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
        public void LoadState(object navigationParameter, int IdChannel)
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

                DataSourceRadio.AgendaCultural = new View.TipoAgendaCulturalVO();

                this.idItemSelecionado = Convert.ToInt32(navigationParameter);

                this.LoadMain(this.idItemSelecionado, IdChannel);

                this.DefaultViewModel["itemsViewSource"] = DataSourceRadio.AgendaCultural;
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
                string strTitulo = DataSourceRadio.AgendaCultural.TituloPagina.ToLower();

                char primeira = char.ToUpper(strTitulo[0]);
                strTitulo = primeira + strTitulo.Substring(1);
                this.pageTitle.Text = strTitulo;
                this.pageTitle.Foreground = Util.GetColorFromHexa(Constantes.strCor);
                this.titulo.Text = "Galeria";
                this.titulo.Foreground = Util.GetColorFromHexa(Constantes.strCor);
                this.maisInfo.Text = DataSourceRadio.AgendaCultural.TituloEvento;
                this.maisInfo.Foreground = Util.GetColorFromHexa(Constantes.strCor);
                this.detalhes.Text = DataSourceRadio.AgendaCultural.TituloDetalhesEvento;
                this.detalhes.Foreground = Util.GetColorFromHexa(Constantes.strCor);
                //this.emCartaz.Text = DataSourceRadio.AgendaCultural.TituloCartazEvento;
                //this.emCartaz.Foreground = Util.GetColorFromHexa(Constantes.strCor);
                this.texto1.Text = DataSourceRadio.AgendaCultural.MaisInfoEvento.ConteudoItemDetalhe;
                this.texto1.Foreground = Util.GetColorFromHexa(Constantes.strCor);
                this.imgCartaz.Source = Util.ImageFromRelativePath(this, DataSourceRadio.AgendaCultural.ImagemEvento);


                this.ListaDetalhesEvento.ItemsSource = DataSourceRadio.AgendaCultural.ListaDetalhesEvento.ToList();
                this.ListaDetalhesEvento.Foreground = Util.GetColorFromHexa(Constantes.strCor);
                //this.ListaEmCartaz.ItemsSource = DataSourceRadio.AgendaCultural.ListaCartazEvento.ToList();
                //this.ListaEmCartaz.Foreground = Util.GetColorFromHexa(Constantes.strCor);
                Constantes.intFlagBack = 6;
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
        protected async void LoadMain(int guid, int idChannel)
        {
            if (Util.IsConnectedToInternet())
            {
                AgendaCulturalBLL agendaCulturalBLL = new AgendaCulturalBLL();

                XML.ChannelAgendaCulturalVO channelAgendaCultural = DataSourceRadio.ListaChannelAgendaCulturalVO.Where(item => item.Id.Equals(idChannel)).First<XML.ChannelAgendaCulturalVO>();
                XML.ItemAgendaCulturalVO itemAgendaCulturaVO = channelAgendaCultural.ListaItem.Where(item => item.Guid.Equals(guid)).First<XML.ItemAgendaCulturalVO>();

                if (itemAgendaCulturaVO.LinkSingle == null)
                    itemAgendaCulturaVO.LinkSingle = XMLParserVO.LoadXmLinkSingleUrl(itemAgendaCulturaVO.UrlLinkSingle);

                //ChannelAgendaCulturalVO channelAgendaCulturalVO = DataSourceRadio.ListaChannelAgendaCulturalVO.Where(item => item.Id == idChannel).First<ChannelAgendaCulturalVO>();
                // Identifica o canal da Noticia
                //if (DataSourceRadio.ListaChannelAgendaCulturalVO.Where(item => item.Id.Equals(idChannel)).First<ChannelAgendaCulturalVO>().ListaItem.Where(item2 => item2.Guid.Equals(guid)).Any<ItemAgendaCulturalVO>())

                agendaCulturalBLL.LoadDataAgendaCultural(channelAgendaCultural, guid);



                this.DefaultViewModel["TextoFacebook"] = Util.ShareFacebook(agendaCulturalBLL.ItemAgendaCultural.Link);
                this.DefaultViewModel["TextoTwitter"] = Util.ShareTwitter(agendaCulturalBLL.ItemAgendaCultural.Link, agendaCulturalBLL.ItemAgendaCultural.Title);
                this.DefaultViewModel["corTitulo"] = Constantes.strCor;

                if (channelAgendaCultural.NomeChannel == "Shows")
                {
                    //this.stackDetalheEvento.SetValue(Grid.ColumnProperty, 0);
                    //this.stackMaisInfo.SetValue(Grid.ColumnProperty, 1);
                    //this.maisInfo.Margin = new Thickness(0, 0, 760, 40);
                    //this.detalhes.Margin = new Thickness(600, 0, 30, 40);
                    //this.gridDetalhesSnapped.SetValue(Grid.ColumnProperty, 2);
                    //this.gridMaisInfoSnapped.SetValue(Grid.ColumnProperty, 4);
                    //this.maisInfoSnapped.SetValue(Grid.RowProperty, 1);
                    //this.detalhesSnapped.SetValue(Grid.RowProperty, 3);
                }

                DataSourceRadio.IsUpdatingAgendaCulturalDetalhe = false;
                PropertyComponentChange(indeterminateProgressBar1, AppBarUpdateDataBtn);
            }
            else
            {
                TelaMensagem.Instance.ShowMensagem(Constantes.ResourceErro.GetString("Conexao"));
            }
        }


        /// <summary>
        /// Evento  AppBarDoneButton
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">RoutedEventArgs e</param>
        private void AppBarUpdateDataBtn_Click(object sender, RoutedEventArgs e)
        {
            DataSourceRadio.IsUpdatingAgendaCulturalDetalhe = true;
            PropertyComponentChange(indeterminateProgressBar1, AppBarUpdateDataBtn);

            DataSourceRadio.AgendaCultural.TituloCartazEvento = string.Empty;
            DataSourceRadio.AgendaCultural.TituloDetalhesEvento = string.Empty;
            DataSourceRadio.AgendaCultural.TituloEvento = string.Empty;
            DataSourceRadio.AgendaCultural.TituloPagina = string.Empty;

            if (DataSourceRadio.AgendaCultural.ListaAgendaCultural != null)
                DataSourceRadio.AgendaCultural.ListaAgendaCultural.Clear();

            if (DataSourceRadio.AgendaCultural.ListaDetalhesEvento != null)
                DataSourceRadio.AgendaCultural.ListaDetalhesEvento.Clear();

            if (DataSourceRadio.AgendaCultural.ListaCartazEvento != null)
                DataSourceRadio.AgendaCultural.ListaCartazEvento.Clear();

            DataSourceRadio.AgendaCultural.MaisInfoEvento.ConteudoItemDetalhe = string.Empty;
            DataSourceRadio.AgendaCultural.MaisInfoEvento.Endereco = string.Empty;
            DataSourceRadio.AgendaCultural.MaisInfoEvento.TituloItemDetalhe = string.Empty;

            XML.ChannelAgendaCulturalVO channelAgendaCultural = DataSourceRadio.ListaChannelAgendaCulturalVO.Where(item => item.Id.Equals(this.idChannelSelecionado)).First<XML.ChannelAgendaCulturalVO>();
            XML.ItemAgendaCulturalVO itemAgendaCulturaVO = channelAgendaCultural.ListaItem.Where(item => item.Guid.Equals(this.idItemSelecionado)).First<XML.ItemAgendaCulturalVO>();

            itemAgendaCulturaVO.LinkSingle = null;

            this.LoadMain(this.idItemSelecionado, this.idChannelSelecionado);
        }

        /// <summary>
        /// Evento ViewState
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e"> VisualStateChangedEventArgs e</param>
        private void ApplicationViewStates_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            //if (e.NewState.Name.Equals(FullScreenLandscape.Name))
            //{
            //    this.BeginEnderecoYoutube = "<html><head><style>html, body { height:100%; overflow:hidden; }body { margin:0; }</style></head><body><iframe width='920' height='640' src='";
            //    string iframeYoutube = this.BeginEnderecoYoutube + this.EmbeddYoutube + DataSourceRadio.CinemaVO.Video + this.EndEnderecoYoutube;
            //    this.Web.NavigateToString(iframeYoutube);
            //}
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
    }
}
