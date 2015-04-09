using RadioPlayer.Beans;
using RadioPlayer.Common.Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace RadioPlayer.Common
{
    /// <summary>
    /// Classe HtmlToRtfConverter
    /// </summary>
    public class HtmlToRtfConverter
    {
        /// <summary>
        /// Using a DependencyProperty as the backing store for Html.  This enables animation, styling, binding, etc
        /// </summary>
        public static readonly DependencyProperty HtmlProperty =
            DependencyProperty.RegisterAttached("Html", typeof(string), typeof(HtmlToRtfConverter), new PropertyMetadata(string.Empty, OnHtmlChanged));

        /// <summary>
        /// Atríbuto hyperlinkButtonStyleIBahia
        /// </summary>
        private static Style hyperlinkButtonStyleIBahia = (Style)App.Current.Resources["HyperlinkButtonStyleIBahia"];

        /// <summary>
        /// Interface ITextContainer
        /// </summary>
        private interface ITextContainer
        {
            /// <summary>
            /// Assinatura Inline
            /// </summary>
            /// <param name="text">Inline text</param>
            void Add(Inline text);

            /// <summary>
            /// Assinatura Block
            /// </summary>
            /// <param name="paragraph">Block paragraph</param>
            void Add(Block paragraph);

            /// <summary>
            /// Assinatura HyperlinkButton
            /// </summary>
            /// <param name="hyperlinkButton">HyperlinkButton hyperlinkButton</param>
            void Add(HyperlinkButton hyperlinkButton);

            /// <summary>
            /// Assinatura Image
            /// </summary>
            /// <param name="image">Image image</param>
            void Add(Image image);

            /// <summary>
            /// Assinatura WebView
            /// </summary>
            /// <param name="webView">WebView webView</param>
            void Add(WebView webView);

            /// <summary>
            /// Assinatura Grid
            /// </summary>
            /// <param name="grid">Grid grid</param>
            void Add(Grid grid);
        }

        /// <summary>
        /// Gets or sets - propriedade ListaTarefaLoadDados.
        /// </summary>
        public static Style HyperlinkButtonStyleIBahia
        {
            get { return hyperlinkButtonStyleIBahia; }
            set { hyperlinkButtonStyleIBahia = value; }
        }

        /// <summary>
        /// Gets - DependencyObject obj.
        /// </summary>
        /// <param name="obj">DependencyObject obj</param>
        /// <returns>string DependencyObject</returns>
        public static string GetHtml(DependencyObject obj)
        {
            return (string)obj.GetValue(HtmlProperty);
        }

        /// <summary>
        /// Sets - DependencyObject obj.
        /// </summary>
        /// <param name="obj">DependencyObject obj</param>
        /// <param name="value">string value</param>
        public static void SetHtml(DependencyObject obj, string value)
        {
            obj.SetValue(HtmlProperty, value);
        }

        /// <summary>
        /// Evento OnHtmlChanged
        /// </summary>
        /// <param name="sender">DependencyObject sender</param>
        /// <param name="eventArgs">DependencyPropertyChangedEventArgs eventArgs</param>
        private static void OnHtmlChanged(DependencyObject sender, DependencyPropertyChangedEventArgs eventArgs)
        {
            RichTextBlock parent = (RichTextBlock)sender;
            parent.Blocks.Clear();
            RichTextBlockTextContainer richTextBlockTextContainer = new RichTextBlockTextContainer(parent);

            try
            {
                string formatado = Util.ReplaceSpecialCharacters((string)eventArgs.NewValue);

                formatado = "<body>" + formatado + "</body>";

                   XmlDocument document = new XmlDocument();
                document.LoadXml(formatado);

                var paragraph = new Paragraph();
                ParagraphTextContainer paragraphTextContainer = new ParagraphTextContainer(paragraph);
                richTextBlockTextContainer.Add(paragraph);
                ////richTextBlockTextContainer.Add(new LineBreak());

                paragraphTextContainer.Add(new Run { Text = DataSourceRadio.DetalhamentoNoticia.Titulo, FontSize = 56 });
                paragraphTextContainer.Add(new LineBreak());
                paragraphTextContainer.Add(new LineBreak());

                paragraphTextContainer.Add(new Run { Text = DataSourceRadio.DetalhamentoNoticia.SubTitulo, FontSize = 44 });
                paragraphTextContainer.Add(new LineBreak());
                ////paragraph = new Paragraph();
                ////paragraphTextContainer = new ParagraphTextContainer(paragraph);
                ////richTextBlockTextContainer.Add(paragraph);

                ParseElement((XmlElement)document.GetElementsByTagName("body")[0], richTextBlockTextContainer);
            }
            catch (Exception)
            {
                parent.Blocks.Clear();

                if (DataSourceRadio.DetalhamentoNoticia.ImagemNoticia.Url != null)
                {
                    var image = new Image();
                    BitmapImage bi = new BitmapImage(new Uri(DataSourceRadio.DetalhamentoNoticia.ImagemNoticia.Url));

                    image.Source = bi;
                    image.Stretch = Stretch.Fill;
                    image.Width = 450;
                    image.Height = 300;

                    ToolTip toolTip = new ToolTip();
                    toolTip.Content = DataSourceRadio.DetalhamentoNoticia.ImagemNoticia.Title;
                    ToolTipService.SetToolTip(image, toolTip);

                    image.HorizontalAlignment = HorizontalAlignment.Center;
                    image.VerticalAlignment = VerticalAlignment.Center;

                    image.Margin = new Thickness(5, 5, 5, 5);

                    ImagenContainer imagenContainer = new ImagenContainer(image);

                    richTextBlockTextContainer.Add(image);
                    richTextBlockTextContainer.Add(new LineBreak());
                }

                var paragraph = new Paragraph();
                ParagraphTextContainer paragraphTextContainer = new ParagraphTextContainer(paragraph);
                richTextBlockTextContainer.Add(paragraph);
                richTextBlockTextContainer.Add(new LineBreak());

                paragraphTextContainer.Add(new Run { Text = DataSourceRadio.DetalhamentoNoticia.Titulo, FontSize = 56 });

                paragraph = new Paragraph();
                paragraphTextContainer = new ParagraphTextContainer(paragraph);
                richTextBlockTextContainer.Add(paragraph);
                richTextBlockTextContainer.Add(new LineBreak());
                richTextBlockTextContainer.Add(new LineBreak());

                paragraphTextContainer.Add(new Run { Text = DataSourceRadio.DetalhamentoNoticia.SubTitulo, FontSize = 46 });

                paragraph = new Paragraph();
                paragraphTextContainer = new ParagraphTextContainer(paragraph);
                richTextBlockTextContainer.Add(paragraph);
                richTextBlockTextContainer.Add(new LineBreak());
                richTextBlockTextContainer.Add(new LineBreak());

                paragraphTextContainer.Add(new Run { Text = DataSourceRadio.DetalhamentoNoticia.ConteudoSemHtml, FontSize = 38 });
            }
        }

        /// <summary>
        /// Método ParseElement
        /// </summary>
        /// <param name="element">XmlElement element,</param>
        /// <param name="parent">ITextContainer parent</param>
        private static void ParseElement(XmlElement element, ITextContainer parent)
        {
            foreach (var child in element.ChildNodes)
            {
                if (child is Windows.Data.Xml.Dom.XmlText)
                {
                    if (child.InnerText.ToLower().Contains("[youtube"))
                    {
                        string[] youtube = child.InnerText.Split(new string[] { "[youtube" }, StringSplitOptions.RemoveEmptyEntries);

                        WebView webView = new WebView();
                        foreach (string strYoutube in youtube)
                        {
                            string codYoutube = strYoutube.TrimStart().Substring(0, strYoutube.TrimStart().Length - 1);

                            webView.VerticalAlignment = VerticalAlignment.Top;
                            webView.HorizontalAlignment = HorizontalAlignment.Center;
                            webView.Height = 630;
                            webView.Margin = new Thickness(150, 10, 5, 5);
                            webView.Width = 745;
                            webView.Loaded += delegate
                            {
                                MainPage d = new MainPage();

                                string beginEnderecoYoutube = "<html><head><style>html, body { height:100%; width:100%; overflow:hidden; }body { margin:0; }</style></head><body><iframe width='920' height='640' src='";
                                string endEnderecoYoutube = "'></iframe></body></html>";
                                string embeddYoutube = "http://www.youtube.com/embed/";

                                string iframeYoutube = beginEnderecoYoutube + embeddYoutube + codYoutube + endEnderecoYoutube;
                                webView.NavigateToString(iframeYoutube);
                            };

                            //// parent.Add(new LineBreak());
                            parent.Add(webView);
                        }
                    }
                    else if (string.IsNullOrEmpty(child.InnerText) || child.InnerText == "\n")
                    {
                        continue;
                    }
                    else
                    {
                        Run run = new Run { Text = child.InnerText, FontSize = 35 };
                        run.Foreground = new SolidColorBrush(Colors.Black);
                        parent.Add(run);
                    }
                }
                else if (child is XmlElement)
                {
                    XmlElement e = (XmlElement)child;
                    switch (e.TagName.ToUpper())
                    {
                        case "P":
                            var paragraph = new Paragraph();
                            parent.Add(paragraph);
                            ParseElement(e, new ParagraphTextContainer(paragraph));
                            break;
                        case "DIV":
                            ParseElement(e, parent);
                            break;
                        case "I":
                            var italic = new Italic();
                            parent.Add(italic);
                            ParseElement(e, new SpanTextContainer(italic));
                            break;
                        case "B":
                        case "STRONG":
                            var bold = new Bold();
                            parent.Add(bold);
                            ParseElement(e, new SpanTextContainer(bold));
                            break;
                        case "U":
                            var underline = new Underline();
                            parent.Add(underline);
                            ParseElement(e, new SpanTextContainer(underline));
                            break;
                        case "A":
                            var hyperlinkButton = new HyperlinkButton();
                            var href = e.Attributes.Where(item => item.NodeName.Equals("href")).First();
                            Style titleTextStyle = HyperlinkButtonStyleIBahia;

                            hyperlinkButton.NavigateUri = new Uri(href.NodeValue.ToString());
                            hyperlinkButton.Style = titleTextStyle;
                            parent.Add(hyperlinkButton);
                            ParseElement(e, new HyperlinkButtonContainer(hyperlinkButton));
                            break;
                        case "BR":
                            parent.Add(new LineBreak());
                            break;
                        case "IMG":
                            var image = new Image();
                            var src = e.Attributes.Where(item => item.NodeName.Equals("src")).First();
                            var title = e.Attributes.Where(item => item.NodeName.Equals("title")).First();

                            BitmapImage bi = new BitmapImage(new Uri(src.NodeValue.ToString()));

                            image.Source = bi;
                            image.Stretch = Stretch.Fill;
                            image.Width = 650;
                            image.Height = 450;

                            image.HorizontalAlignment = HorizontalAlignment.Center;
                            image.VerticalAlignment = VerticalAlignment.Center;

                            ToolTip toolTip = new ToolTip();
                            toolTip.Content = title.InnerText;
                            ToolTipService.SetToolTip(image, toolTip);

                            image.Margin = new Thickness(130, 5, 5, 5);

                            parent.Add(image);
                            ParseElement(e, new ImagenContainer(image));
                            break;
                        case "TABLE":

                            var grid = new Grid();
                            //// grid.Margin = new Thickness(15, 5, 5, 5);
                            grid.VerticalAlignment = VerticalAlignment.Center;
                            grid.HorizontalAlignment = HorizontalAlignment.Center;

                            IXmlNode thead;
                            IXmlNode tbody;
                            IXmlNode tfoot;

                            parent.Add(grid);
                            parent.Add(new LineBreak());
                            parent.Add(new LineBreak());

                            GridContainer gridContainer = new GridContainer(grid);

                            if (e.GetElementsByTagName("thead").Any())
                            {
                                thead = e.SelectSingleNode("thead");

                                if (thead.ChildNodes.Any())
                                {
                                    if (thead.FirstChild.NodeName.ToUpper().Equals("TR"))
                                    {
                                        gridContainer.ByRow = true;
                                    }
                                    else
                                    {
                                        gridContainer.ByRow = false;
                                    }
                                }

                                ParseElement((XmlElement)thead, gridContainer);
                            }

                            if (e.GetElementsByTagName("tbody").Any())
                            {
                                tbody = e.SelectSingleNode("tbody");

                                if (tbody.ChildNodes.Any())
                                {
                                    if (tbody.FirstChild.NodeName.ToUpper().Equals("TR"))
                                    {
                                        gridContainer.ByRow = true;
                                    }
                                    else
                                    {
                                        gridContainer.ByRow = false;
                                    }
                                }

                                ParseElement((XmlElement)tbody, gridContainer);
                            }

                            if (e.GetElementsByTagName("tfoot").Any())
                            {
                                tfoot = e.SelectSingleNode("tfoot");

                                if (tfoot.ChildNodes.Any())
                                {
                                    if (tfoot.FirstChild.NodeName.ToUpper().Equals("TR"))
                                    {
                                        gridContainer.ByRow = true;
                                    }
                                    else
                                    {
                                        gridContainer.ByRow = false;
                                    }
                                }

                                ParseElement((XmlElement)tfoot, gridContainer);
                            }

                            break;

                        case "TH":
                        case "TD":

                            ((GridContainer)parent).Column++;

                            if (!((GridContainer)parent).ByRow)
                            {
                                ColumnDefinition cd = new ColumnDefinition();
                                ((GridContainer)parent).Add(cd);
                            }
                            else
                            {
                                if (((GridContainer)parent).Column > ((GridContainer)parent).Pgrid.ColumnDefinitions.Count)
                                {
                                    ColumnDefinition cd = new ColumnDefinition();
                                    ((GridContainer)parent).Add(cd);
                                }
                            }

                            if (e.Attributes.Where(item => item.NodeName.Equals("rowspan")).Any())
                            {
                                ((GridContainer)parent).HasSpanRow = true;
                                var rowspan = e.Attributes.Where(item => item.NodeName.Equals("rowspan")).First();
                                ((GridContainer)parent).SpanRow = int.Parse(rowspan.NodeValue.ToString());
                            }
                            else
                            {
                                ((GridContainer)parent).HasSpanRow = false;
                                ((GridContainer)parent).SpanRow = 0;
                            }

                            if (e.Attributes.Where(item => item.NodeName.Equals("colspan")).Any())
                            {
                                ((GridContainer)parent).HasSpanColumn = true;
                                var spanColumn = e.Attributes.Where(item => item.NodeName.Equals("colspan")).First();
                                ((GridContainer)parent).SpanColumn = int.Parse(spanColumn.NodeValue.ToString());
                            }
                            else
                            {
                                ((GridContainer)parent).HasSpanColumn = false;
                                ((GridContainer)parent).SpanColumn = 0;
                            }

                            ParseElement(e, parent);

                            if (!((GridContainer)parent).ByRow)
                            {
                                ((GridContainer)parent).Row = 0;
                            }

                            break;
                        case "TR":

                            ((GridContainer)parent).Row++;

                            if (((GridContainer)parent).ByRow)
                            {
                                RowDefinition rd = new RowDefinition();
                                ((GridContainer)parent).Add(rd);
                            }
                            else
                            {
                                if (((GridContainer)parent).Row > ((GridContainer)parent).Pgrid.RowDefinitions.Count)
                                {
                                    RowDefinition rd = new RowDefinition();
                                    ((GridContainer)parent).Add(rd);
                                }
                            }

                            if (e.Attributes.Where(item => item.NodeName.Equals("rowspan")).Any())
                            {
                                ((GridContainer)parent).HasSpanRow = true;
                                var rowspan = e.Attributes.Where(item => item.NodeName.Equals("rowspan")).First();
                                ((GridContainer)parent).SpanRow = int.Parse(rowspan.NodeValue.ToString());
                            }
                            else
                            {
                                ((GridContainer)parent).HasSpanRow = false;
                                ((GridContainer)parent).SpanRow = 0;
                            }

                            if (e.Attributes.Where(item => item.NodeName.Equals("colspan")).Any())
                            {
                                ((GridContainer)parent).HasSpanColumn = true;
                                var spanColumn = e.Attributes.Where(item => item.NodeName.Equals("colspan")).First();
                                ((GridContainer)parent).SpanColumn = int.Parse(spanColumn.NodeValue.ToString());
                            }
                            else
                            {
                                ((GridContainer)parent).HasSpanColumn = false;
                                ((GridContainer)parent).SpanColumn = 0;
                            }

                            ParseElement(e, parent);

                            if (((GridContainer)parent).ByRow)
                            {
                                ((GridContainer)parent).Column = 0;
                            }

                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Classe selada SpanTextContainer
        /// </summary>
        private sealed class SpanTextContainer : ITextContainer
        {
            /// <summary>
            /// Atríbuto pspan
            /// </summary>
            private readonly Span pspan;

            /// <summary>
            /// Initializes a new instance of the <see cref="SpanTextContainer"/> class.
            /// </summary>
            /// <param name="span">Span span</param>
            public SpanTextContainer(Span span)
            {
                this.pspan = span;
            }

            /// <summary>
            /// Propriedade Add
            /// </summary>
            /// <param name="text">Inline text</param>
            public void Add(Inline text)
            {
                this.pspan.Inlines.Add(text);
            }

            /// <summary>
            /// Propriedade Add
            /// </summary>
            /// <param name="paragraph">Block paragraph</param>
            public void Add(Block paragraph)
            {
                throw new NotSupportedException();
            }

            /// <summary>
            /// Propriedade Add
            /// </summary>
            /// <param name="hyperlinkButton">HyperlinkButton hyperlinkButton</param>
            public void Add(HyperlinkButton hyperlinkButton)
            {
                InlineUIContainer i = new InlineUIContainer();
                i.Child = hyperlinkButton;
                this.pspan.Inlines.Add(i);
            }

            /// <summary>
            /// Propriedade Add
            /// </summary>
            /// <param name="image">Image image</param>
            public void Add(Image image)
            {
                InlineUIContainer i = new InlineUIContainer();
                i.Child = image;
                this.pspan.Inlines.Add(i);
            }

            /// <summary>
            /// Propriedade Add
            /// </summary>
            /// <param name="webView">WebView webView</param>
            public void Add(WebView webView)
            {
                InlineUIContainer i = new InlineUIContainer();
                i.Child = webView;
                this.pspan.Inlines.Add(i);
            }

            /// <summary>
            /// Propriedade Add
            /// </summary>
            /// <param name="grid">Grid grid</param>
            public void Add(Grid grid)
            {
                grid.Width = 900;
                grid.VerticalAlignment = VerticalAlignment.Center;
                grid.HorizontalAlignment = HorizontalAlignment.Center;
                InlineUIContainer i = new InlineUIContainer();
                i.Child = grid;
                this.pspan.Inlines.Add(i);
            }
        }

        /// <summary>
        /// Classe selada ImagenContainer
        /// </summary>
        private sealed class ImagenContainer : ITextContainer
        {
            /// <summary>
            /// Atríbuto _image
            /// </summary>
            private readonly Image pimage;

            /// <summary>
            /// Initializes a new instance of the <see cref="ImagenContainer"/> class.
            /// </summary>
            /// <param name="image">Image image</param>
            public ImagenContainer(Image image)
            {
                this.pimage = image;
            }

            /// <summary>
            /// Propriedade Add
            /// </summary>
            /// <param name="text">Inline text</param>
            public void Add(Inline text)
            {
                throw new NotSupportedException();
            }

            /// <summary>
            /// Propriedade Add
            /// </summary>
            /// <param name="paragraph">Block paragraph</param>
            public void Add(Block paragraph)
            {
                throw new NotSupportedException();
            }

            /// <summary>
            /// Propriedade Add
            /// </summary>
            /// <param name="hyperlinkButton">HyperlinkButton hyperlinkButton</param>
            public void Add(HyperlinkButton hyperlinkButton)
            {
                throw new NotSupportedException();
            }

            /// <summary>
            /// Propriedade Add
            /// </summary>
            /// <param name="image">Image image</param>
            public void Add(Image image)
            {
                throw new NotSupportedException();
            }

            /// <summary>
            /// Propriedade Add
            /// </summary>
            /// <param name="webView">WebView webView</param>
            public void Add(WebView webView)
            {
                throw new NotSupportedException();
            }

            /// <summary>
            /// Propriedade Add
            /// </summary>
            /// <param name="grid">Grid grid</param>
            public void Add(Grid grid)
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Classe selada HyperlinkButtonContainer
        /// </summary>
        private sealed class HyperlinkButtonContainer : ITextContainer
        {
            /// <summary>
            /// Atríbuto pblock
            /// </summary>
            private readonly HyperlinkButton pblock;

            /// <summary>
            /// Initializes a new instance of the <see cref="HyperlinkButtonContainer"/> class.
            /// </summary>
            /// <param name="block">HyperlinkButton block</param>
            public HyperlinkButtonContainer(HyperlinkButton block)
            {
                this.pblock = block;
            }

            /// <summary>
            /// Propriedade Add
            /// </summary>
            /// <param name="text">Inline text</param>
            public void Add(Inline text)
            {
                this.pblock.Content = ((Windows.UI.Xaml.Documents.Run)text).Text;
            }

            /// <summary>
            /// Propriedade Add
            /// </summary>
            /// <param name="paragraph">Block paragraph</param>
            public void Add(Block paragraph)
            {
                throw new NotSupportedException();
            }

            /// <summary>
            /// Propriedade Add
            /// </summary>
            /// <param name="hyperlinkButton">HyperlinkButton hyperlinkButton</param>
            public void Add(HyperlinkButton hyperlinkButton)
            {
                throw new NotSupportedException();
            }

            /// <summary>
            /// Propriedade Add
            /// </summary>
            /// <param name="image">Image image</param>
            public void Add(Image image)
            {
                throw new NotSupportedException();
            }

            /// <summary>
            /// Propriedade Add
            /// </summary>
            /// <param name="webView">WebView webView</param>
            public void Add(WebView webView)
            {
                throw new NotSupportedException();
            }

            /// <summary>
            /// Propriedade Add
            /// </summary>
            /// <param name="grid">Grid grid</param>
            public void Add(Grid grid)
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Classe selada ParagraphTextContainer
        /// </summary>
        private sealed class ParagraphTextContainer : ITextContainer
        {
            /// <summary>
            /// Atríbuto pblock
            /// </summary>
            private readonly Paragraph pblock;

            /// <summary>
            /// Initializes a new instance of the <see cref="ParagraphTextContainer"/> class.
            /// </summary>
            /// <param name="block">Paragraph block</param>
            public ParagraphTextContainer(Paragraph block)
            {
                this.pblock = block;
                this.pblock.LineStackingStrategy = LineStackingStrategy.MaxHeight;
            }

            /// <summary>
            /// Propriedade Add
            /// </summary>
            /// <param name="text">Inline text</param>
            public void Add(Inline text)
            {
                this.pblock.Inlines.Add(text);
            }

            /// <summary>
            /// Propriedade Add
            /// </summary>
            /// <param name="hyperlinkButton">HyperlinkButton hyperlinkButton</param>
            public void Add(HyperlinkButton hyperlinkButton)
            {
                InlineUIContainer i = new InlineUIContainer();
                i.Child = hyperlinkButton;
                this.pblock.Inlines.Add(i);
            }

            /// <summary>
            /// Propriedade Add
            /// </summary>
            /// <param name="paragraph">Block paragraph</param>
            public void Add(Block paragraph)
            {
                throw new NotSupportedException();
            }

            /// <summary>
            /// Propriedade Add
            /// </summary>
            /// <param name="image">Image image</param>
            public void Add(Image image)
            {
                InlineUIContainer i = new InlineUIContainer();
                i.Child = image;
                this.pblock.Inlines.Add(i);
            }

            /// <summary>
            /// Propriedade Add
            /// </summary>
            /// <param name="webView">WebView webView</param>
            public void Add(WebView webView)
            {
                InlineUIContainer i = new InlineUIContainer();
                i.Child = webView;
                this.pblock.Inlines.Add(i);
            }

            /// <summary>
            /// Propriedade Add
            /// </summary>
            /// <param name="grid">Grid grid</param>
            public void Add(Grid grid)
            {
                grid.Width = 900;
                grid.VerticalAlignment = VerticalAlignment.Center;
                grid.HorizontalAlignment = HorizontalAlignment.Center;
                this.pblock.LineStackingStrategy = LineStackingStrategy.MaxHeight;
                this.pblock.LineHeight = 550;
                this.pblock.TextAlignment = TextAlignment.Center;
                InlineUIContainer i = new InlineUIContainer();
                i.Child = grid;
                this.pblock.Inlines.Add(i);
            }
        }

        /// <summary>
        /// Classe selada RichTextBlockTextContainer
        /// </summary>
        private sealed class RichTextBlockTextContainer : ITextContainer
        {
            /// <summary>
            /// Atríbuto prichTextBlock
            /// </summary>
            private readonly RichTextBlock prichTextBlock;

            /// <summary>
            /// Initializes a new instance of the <see cref="RichTextBlockTextContainer"/> class.
            /// </summary>
            /// <param name="richTextBlock">RichTextBlock richTextBlock</param>
            public RichTextBlockTextContainer(RichTextBlock richTextBlock)
            {
                this.prichTextBlock = richTextBlock;
            }

            /// <summary>
            /// Propriedade Add
            /// </summary>
            /// <param name="hyperlinkButton">HyperlinkButton hyperlinkButton</param>
            public void Add(HyperlinkButton hyperlinkButton)
            {
                var paragraph = new Paragraph();
                InlineUIContainer i = new InlineUIContainer();
                i.Child = hyperlinkButton;
                paragraph.Inlines.Add(i);

                this.prichTextBlock.Blocks.Add(paragraph);
            }

            /// <summary>
            /// Propriedade Add
            /// </summary>
            /// <param name="text">Inline text</param>
            public void Add(Inline text)
            {
                var paragraph = new Paragraph();
                paragraph.Inlines.Add(text);

                this.prichTextBlock.Blocks.Add(paragraph);
            }

            /// <summary>
            /// Propriedade Add
            /// </summary>
            /// <param name="paragraph">Block paragraph</param>
            public void Add(Block paragraph)
            {
                paragraph.TextAlignment = TextAlignment.Justify;
                this.prichTextBlock.Blocks.Add(paragraph);
            }

            /// <summary>
            /// Propriedade Add
            /// </summary>
            /// <param name="image">Image image</param>
            public void Add(Image image)
            {
                var paragraph = new Paragraph();
                InlineUIContainer i = new InlineUIContainer();
                i.Child = image;
                paragraph.Inlines.Add(i);

                this.prichTextBlock.Blocks.Add(paragraph);
            }

            /// <summary>
            /// Propriedade Add
            /// </summary>
            /// <param name="webView">WebView webView</param>
            public void Add(WebView webView)
            {
                var paragraph = new Paragraph();
                InlineUIContainer i = new InlineUIContainer();
                i.Child = webView;
                paragraph.Inlines.Add(i);

                this.prichTextBlock.Blocks.Add(paragraph);
            }

            /// <summary>
            /// Propriedade Add
            /// </summary>
            /// <param name="grid">Grid grid</param>
            public void Add(Grid grid)
            {
                grid.Width = 900;
                grid.VerticalAlignment = VerticalAlignment.Center;
                grid.HorizontalAlignment = HorizontalAlignment.Center;
                var paragraph = new Paragraph();
                paragraph.LineStackingStrategy = LineStackingStrategy.MaxHeight;
                paragraph.LineHeight = 550;
                paragraph.TextAlignment = TextAlignment.Center;
                InlineUIContainer i = new InlineUIContainer();
                i.Child = grid;
                paragraph.Inlines.Add(i);

                this.prichTextBlock.Blocks.Add(paragraph);
            }
        }

        /// <summary>
        /// Classe selada GridContainer
        /// </summary>
        private sealed class GridContainer : ITextContainer
        {
            /// <summary>
            /// Atríbuto Pgrid
            /// </summary>
            private readonly Grid pgrid;

            /// <summary>
            /// Propriedade Add
            /// </summary>
            private bool hasSpanColumn;

            /// <summary>
            /// Propriedade Add
            /// </summary>
            private bool hasSpanRow;

            /// <summary>
            /// Propriedade Add
            /// </summary>
            private int spanColumn;

            /// <summary>
            /// Propriedade SpanRow
            /// </summary>
            private int spanRow;

            /// <summary>
            /// Propriedade Column
            /// </summary>
            private int column;

            /// <summary>
            /// Propriedade Row
            /// </summary>
            private int row;

            /// <summary>
            /// Propriedade ByRow
            /// </summary>
            private bool pbyRow;

            /// <summary>
            /// Initializes a new instance of the <see cref="GridContainer"/> class.
            /// </summary>
            /// <param name="grid">Grid grid</param>
            public GridContainer(Grid grid)
            {
                this.pgrid = grid;
            }

            /// <summary>
            /// Gets or sets a value indicating whether HasSpanRow.
            /// </summary>
            public bool HasSpanRow
            {
                get { return this.hasSpanRow; }
                set { this.hasSpanRow = value; }
            }

            /// <summary>
            /// Gets or sets - propriedade SpanColumn.
            /// </summary>
            public int SpanColumn
            {
                get { return this.spanColumn; }
                set { this.spanColumn = value; }
            }

            /// <summary>
            /// Gets or sets - propriedade SpanRow.
            /// </summary>
            public int SpanRow
            {
                get { return this.spanRow; }
                set { this.spanRow = value; }
            }

            /// <summary>
            /// Gets or sets - propriedade Column.
            /// </summary>
            public int Column
            {
                get { return this.column; }
                set { this.column = value; }
            }

            /// <summary>
            /// Gets or sets - propriedade Row.
            /// </summary>
            public int Row
            {
                get { return this.row; }
                set { this.row = value; }
            }

            /// <summary>
            /// Gets or sets a value indicating whether ByRow.
            /// </summary>
            public bool ByRow
            {
                get { return this.pbyRow; }
                set { this.pbyRow = value; }
            }

            /// <summary>
            /// Gets or sets a value indicating whether HasSpanColumn.
            /// </summary>
            public bool HasSpanColumn
            {
                get { return this.hasSpanColumn; }
                set { this.hasSpanColumn = value; }
            }

            /// <summary>
            /// Gets - propriedade Pgrid.
            /// </summary>
            public Grid Pgrid
            {
                get { return this.pgrid; }
            }

            /// <summary>
            /// Propriedade Add
            /// </summary>
            /// <param name="columnDefinition">ColumnDefinition columnDefinition</param>
            public void Add(ColumnDefinition columnDefinition)
            {
                columnDefinition.Width = new GridLength(900);
                this.Pgrid.ColumnDefinitions.Add(columnDefinition);
            }

            /// <summary>
            /// Propriedade Add
            /// </summary>
            /// <param name="rowDefinition">RowDefinition rowDefinition</param>
            public void Add(RowDefinition rowDefinition)
            {
                rowDefinition.Height = GridLength.Auto;
                this.Pgrid.RowDefinitions.Add(rowDefinition);
            }

            /// <summary>
            /// Propriedade Add
            /// </summary>
            /// <param name="hyperlinkButton">HyperlinkButton hyperlinkButton</param>
            public void Add(HyperlinkButton hyperlinkButton)
            {
                RichTextBlock rtb = new RichTextBlock();
                Style rtbStyle = (Style)App.Current.Resources["ItemRichTextStyle"];

                rtb.Style = rtbStyle;
                var paragraph = new Paragraph();
                paragraph.TextAlignment = TextAlignment.Justify;

                InlineUIContainer i = new InlineUIContainer();

                i.Child = hyperlinkButton;
                paragraph.Inlines.Add(i);

                rtb.Blocks.Add(paragraph);

                int indexColumn = this.Pgrid.ColumnDefinitions.Count;
                int indexRow = this.Pgrid.RowDefinitions.Count;

                if (indexColumn.Equals(0))
                {
                    Grid.SetColumn(rtb, 0);
                }
                else
                {
                    Grid.SetColumn(rtb, this.Column - 1);
                }

                if (indexRow.Equals(0))
                {
                    Grid.SetRow(rtb, 0);
                }
                else
                {
                    Grid.SetRow(rtb, this.Row - 1);
                }

                if (this.HasSpanColumn)
                {
                    Grid.SetColumnSpan(rtb, this.SpanColumn);
                }

                if (this.HasSpanRow)
                {
                    Grid.SetRowSpan(rtb, this.SpanRow);
                }

                this.Pgrid.Children.Add(rtb);
            }

            /// <summary>
            /// Propriedade Add
            /// </summary>
            /// <param name="text">Inline text</param>
            public void Add(Inline text)
            {
                RichTextBlock rtb = new RichTextBlock();
                Style rtbStyle = (Style)App.Current.Resources["ItemRichTextStyle"];
                rtb.Style = rtbStyle;
                var paragraph = new Paragraph();
                paragraph.LineStackingStrategy = LineStackingStrategy.MaxHeight;
                paragraph.TextAlignment = TextAlignment.Justify;

                paragraph.Inlines.Add(text);
                rtb.Blocks.Add(paragraph);

                int indexColumn = this.Pgrid.ColumnDefinitions.Count;
                int indexRow = this.Pgrid.RowDefinitions.Count;

                if (indexColumn.Equals(0))
                {
                    Grid.SetColumn(rtb, 0);
                }
                else
                {
                    Grid.SetColumn(rtb, this.Column - 1);
                }

                if (indexRow.Equals(0))
                {
                    Grid.SetRow(rtb, 0);
                }
                else
                {
                    Grid.SetRow(rtb, this.Row - 1);
                }

                if (this.HasSpanColumn)
                {
                    Grid.SetColumnSpan(rtb, this.SpanColumn);
                }

                if (this.HasSpanRow)
                {
                    Grid.SetRowSpan(rtb, this.SpanRow);
                }

                this.Pgrid.Children.Add(rtb);
            }

            /// <summary>
            /// Propriedade Add
            /// </summary>
            /// <param name="paragraph">Block paragraph</param>
            public void Add(Block paragraph)
            {
                RichTextBlock rtb = new RichTextBlock();
                rtb.TextWrapping = TextWrapping.Wrap;

                Style rtbStyle = (Style)App.Current.Resources["ItemRichTextStyle"];
                rtb.Style = rtbStyle;
                rtb.Blocks.Add(paragraph);

                int indexColumn = this.Pgrid.ColumnDefinitions.Count;
                int indexRow = this.Pgrid.RowDefinitions.Count;

                if (indexColumn.Equals(0))
                {
                    Grid.SetColumn(rtb, 0);
                }
                else
                {
                    Grid.SetColumn(rtb, this.Column - 1);
                }

                if (indexRow.Equals(0))
                {
                    Grid.SetRow(rtb, 0);
                }
                else
                {
                    Grid.SetRow(rtb, this.Row - 1);
                }

                if (this.HasSpanColumn)
                {
                    Grid.SetColumnSpan(rtb, this.SpanColumn);
                }

                if (this.HasSpanRow)
                {
                    Grid.SetRowSpan(rtb, this.SpanRow);
                }

                this.Pgrid.Children.Add(rtb);
            }

            /// <summary>
            /// Propriedade Add
            /// </summary>
            /// <param name="image">Image image</param>
            public void Add(Image image)
            {
                RichTextBlock rtb = new RichTextBlock();
                Style rtbStyle = (Style)App.Current.Resources["ItemRichTextStyle"];
                rtb.Style = rtbStyle;
                var paragraph = new Paragraph();

                InlineUIContainer i = new InlineUIContainer();

                i.Child = image;
                paragraph.Inlines.Add(i);

                rtb.Blocks.Add(paragraph);

                int indexColumn = this.Pgrid.ColumnDefinitions.Count;
                int indexRow = this.Pgrid.RowDefinitions.Count;

                if (indexColumn.Equals(0))
                {
                    Grid.SetColumn(rtb, 0);
                }
                else
                {
                    Grid.SetColumn(rtb, this.Column - 1);
                }

                if (indexRow.Equals(0))
                {
                    Grid.SetRow(rtb, 0);
                }
                else
                {
                    Grid.SetRow(rtb, this.Row - 1);
                }

                if (this.HasSpanColumn)
                {
                    Grid.SetColumnSpan(rtb, this.SpanColumn);
                }

                if (this.HasSpanRow)
                {
                    Grid.SetRowSpan(rtb, this.SpanRow);
                }

                this.Pgrid.Children.Add(rtb);
            }

            /// <summary>
            /// Propriedade Add
            /// </summary>
            /// <param name="webView">WebView webView</param>
            public void Add(WebView webView)
            {
                RichTextBlock rtb = new RichTextBlock();
                Style rtbStyle = (Style)App.Current.Resources["ItemRichTextStyle"];
                rtb.Style = rtbStyle;
                var paragraph = new Paragraph();
                InlineUIContainer i = new InlineUIContainer();

                i.Child = webView;
                paragraph.Inlines.Add(i);

                rtb.Blocks.Add(paragraph);

                int indexColumn = this.Pgrid.ColumnDefinitions.Count;
                int indexRow = this.Pgrid.RowDefinitions.Count;

                if (indexColumn.Equals(0))
                {
                    Grid.SetColumn(rtb, 0);
                }
                else
                {
                    Grid.SetColumn(rtb, this.Column - 1);
                }

                if (indexRow.Equals(0))
                {
                    Grid.SetRow(rtb, 0);
                }
                else
                {
                    Grid.SetRow(rtb, this.Row - 1);
                }

                if (this.HasSpanColumn)
                {
                    Grid.SetColumnSpan(rtb, this.SpanColumn);
                }

                if (this.HasSpanRow)
                {
                    Grid.SetRowSpan(rtb, this.SpanRow);
                }

                this.Pgrid.Children.Add(rtb);
            }

            /// <summary>
            /// Propriedade Add
            /// </summary>
            /// <param name="grid">Grid grid</param>
            public void Add(Grid grid)
            {
                grid.Width = 850;
                grid.VerticalAlignment = VerticalAlignment.Center;
                grid.HorizontalAlignment = HorizontalAlignment.Center;
                RichTextBlock rtb = new RichTextBlock();
                Style rtbStyle = (Style)App.Current.Resources["ItemRichTextStyle"];
                rtb.Style = rtbStyle;
                var paragraph = new Paragraph();
                InlineUIContainer i = new InlineUIContainer();

                i.Child = grid;
                paragraph.Inlines.Add(i);

                rtb.Blocks.Add(paragraph);

                int indexColumn = this.Pgrid.ColumnDefinitions.Count;
                int indexRow = this.Pgrid.RowDefinitions.Count;

                if (indexColumn.Equals(0))
                {
                    Grid.SetColumn(rtb, 0);
                }
                else
                {
                    Grid.SetColumn(rtb, this.Column - 1);
                }

                if (indexRow.Equals(0))
                {
                    Grid.SetRow(rtb, 0);
                }
                else
                {
                    Grid.SetRow(rtb, this.Row - 1);
                }

                if (this.HasSpanColumn)
                {
                    Grid.SetColumnSpan(rtb, this.SpanColumn);
                }

                if (this.HasSpanRow)
                {
                    Grid.SetRowSpan(rtb, this.SpanRow);
                }

                this.Pgrid.Children.Add(rtb);
            }
        }
    }



    internal interface ITextContainer
    {
        /// <summary>
        /// Assinatura Inline
        /// </summary>
        /// <param name="text">Inline text</param>
        void Add(Inline text);

        /// <summary>
        /// Assinatura Block
        /// </summary>
        /// <param name="paragraph">Block paragraph</param>
        void Add(Block paragraph);

        /// <summary>
        /// Assinatura HyperlinkButton
        /// </summary>
        /// <param name="hyperlinkButton">HyperlinkButton hyperlinkButton</param>
        void Add(HyperlinkButton hyperlinkButton);

        /// <summary>
        /// Assinatura Image
        /// </summary>
        /// <param name="image">Image image</param>
        void Add(Image image);

        /// <summary>
        /// Assinatura WebView
        /// </summary>
        /// <param name="webView">WebView webView</param>
        void Add(WebView webView);

        /// <summary>
        /// Assinatura Grid
        /// </summary>
        /// <param name="grid">Grid grid</param>
        void Add(Grid grid);
    }
}

