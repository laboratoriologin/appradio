using RadioPlayer.Common;
using RadioPlayer.Common.Architecture;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.WebUI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace RadioPlayer.Common
{
    /// <summary>
    /// Typical implementation of Page that provides several important conveniences:
    /// <list type="bullet">
    /// <item>
    /// <description>Application view state to visual state mapping</description>
    /// </item>
    /// <item>
    /// <description>GoBack, GoForward, and GoHome event handlers</description>
    /// </item>
    /// <item>
    /// <description>Mouse and keyboard shortcuts for navigation</description>
    /// </item>
    /// <item>
    /// <description>State management for navigation and process lifetime management</description>
    /// </item>
    /// <item>
    /// <description>A default view model</description>
    /// </item>
    /// </list>
    /// </summary>
    [Windows.Foundation.Metadata.WebHostHidden]
    public abstract class LayoutAwarePage : Page
    {
        /// <summary>
        /// Identifies the <see cref="DefaultViewModel"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DefaultViewModelProperty =
            DependencyProperty.Register("DefaultViewModel", typeof(IObservableMap<string, object>), typeof(LayoutAwarePage), null);

        /// <summary>
        /// Atríbuto _layoutAwareControls.
        /// </summary>
        private List<Control> layoutAwareControls;

        /// <summary>
        /// Atríbuto _pageKey
        /// </summary>
        private string pageKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="LayoutAwarePage"/> class.
        /// </summary>
        public LayoutAwarePage()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                return;
            }

            // Create an empty default view model
            this.DefaultViewModel = new ObservableDictionary<string, object>();

            // When this page is part of the visual tree make two changes:
            // 1) Map application view state to visual state for the page
            // 2) Handle keyboard and mouse navigation requests
            this.Loaded += (sender, e) =>
            {
                this.StartLayoutUpdates(sender, e);

                // Keyboard and mouse navigation only apply when occupying the entire window
                if (this.ActualHeight == Window.Current.Bounds.Height &&
                    this.ActualWidth == Window.Current.Bounds.Width)
                {
                    // Listen to the window directly so focus isn't required
                    Window.Current.CoreWindow.Dispatcher.AcceleratorKeyActivated +=
                        CoreDispatcher_AcceleratorKeyActivated;
                    Window.Current.CoreWindow.PointerPressed +=
                        this.CoreWindow_PointerPressed;
                }
            };

            // Undo the same changes when the page is no longer visible
            this.Unloaded += (sender, e) =>
            {
                this.StopLayoutUpdates(sender, e);
                Window.Current.CoreWindow.Dispatcher.AcceleratorKeyActivated -=
                    CoreDispatcher_AcceleratorKeyActivated;
                Window.Current.CoreWindow.PointerPressed -=
                    this.CoreWindow_PointerPressed;
            };
        }

        /// <summary>
        /// Gets or sets - propriedade DefaultViewModel
        /// An implementation of <see cref="IObservableMap&lt;String, Object&gt;"/> designed to be
        /// used as a trivial view model.
        /// </summary>
        protected IObservableMap<string, object> DefaultViewModel
        {
            get { return this.GetValue(DefaultViewModelProperty) as IObservableMap<string, object>; }

            set { this.SetValue(DefaultViewModelProperty, value); }
        }

        /// <summary>
        /// Método assincrono PopUpMsgError
        /// </summary>
        /// <param name="msgError">string msgError</param>
        public async void PopUpMsgError(string msgError)
        {
            // Create the message dialog and set its content and title
            var messageDialog = new MessageDialog(msgError, "Erro no sistema!");

            // Add commands and set their callbacks
            messageDialog.Commands.Add(new UICommand("Fechar", (command) => { Application.Current.Exit(); }));

            // Set the command that will be invoked by default
            messageDialog.DefaultCommandIndex = 1;

            // Show the message dialog
            await messageDialog.ShowAsync();
        }

        /// <summary>
        /// Método que atualiza os componentes de acordo com o status de atualizando.
        /// </summary>
        /// <param name="progressBarr">Componente Progress Bar da tela.</param>
        /// <param name="btn">Componente Botão Atualizar da tela.</param>
        public abstract void PropertyComponentChange(ProgressBar progressBarr, Button btn);


        /// <summary>
        /// Invoked as an event handler, typically on the <see cref="FrameworkElement.Loaded"/>
        /// event of a <see cref="Control"/> within the page, to indicate that the sender should
        /// start receiving visual state management changes that correspond to application view
        /// state changes.
        /// </summary>
        /// <param name="sender">Instance of <see cref="Control"/> that supports visual state
        /// management corresponding to view states.</param>
        /// <param name="e">Event data that describes how the request was made.</param>
        /// <remarks>The current view state will immediately be used to set the corresponding
        /// visual state when layout updates are requested.  A corresponding
        /// <see cref="FrameworkElement.Unloaded"/> event handler connected to
        /// <see cref="StopLayoutUpdates"/> is strongly encouraged.  Instances of
        /// <see cref="LayoutAwarePage"/> automatically invoke these handlers in their Loaded and
        /// Unloaded events.</remarks>
        /// <seealso cref="DetermineVisualState"/>
        /// <seealso cref="InvalidateVisualState"/>
        public void StartLayoutUpdates(object sender, RoutedEventArgs e)
        {
            var control = sender as Control;

            if (control == null)
            {
                return;
            }

            if (this.layoutAwareControls == null)
            {
                // Start listening to view state changes when there are controls interested in updates
                Window.Current.SizeChanged += this.WindowSizeChanged;
                this.layoutAwareControls = new List<Control>();
            }

            this.layoutAwareControls.Add(control);

            // Set the initial visual state of the control
            VisualStateManager.GoToState(control, this.DetermineVisualState(ApplicationView.Value), false);
        }

        /// <summary>
        /// Invoked as an event handler, typically on the <see cref="FrameworkElement.Unloaded"/>
        /// event of a <see cref="Control"/>, to indicate that the sender should start receiving
        /// visual state management changes that correspond to application view state changes.
        /// </summary>
        /// <param name="sender">Instance of <see cref="Control"/> that supports visual state
        /// management corresponding to view states.</param>
        /// <param name="e">Event data that describes how the request was made.</param>
        /// <remarks>The current view state will immediately be used to set the corresponding
        /// visual state when layout updates are requested.</remarks>
        /// <seealso cref="StartLayoutUpdates"/>
        public void StopLayoutUpdates(object sender, RoutedEventArgs e)
        {
            var control = sender as Control;

            if (control == null || this.layoutAwareControls == null)
            {
                return;
            }

            this.layoutAwareControls.Remove(control);

            if (this.layoutAwareControls.Count == 0)
            {
                // Stop listening to view state changes when no controls are interested in updates
                this.layoutAwareControls = null;
                Window.Current.SizeChanged -= this.WindowSizeChanged;
            }
        }

        /// <summary>
        /// Updates all controls that are listening for visual state changes with the correct
        /// visual state.
        /// </summary>
        /// <remarks>
        /// Typically used in conjunction with overriding <see cref="DetermineVisualState"/> to
        /// signal that a different value may be returned even though the view state has not
        /// changed.
        /// </remarks>
        public void InvalidateVisualState()
        {
            if (this.layoutAwareControls != null)
            {
                string visualState = this.DetermineVisualState(ApplicationView.Value);
                foreach (var layoutAwareControl in this.layoutAwareControls)
                {
                    VisualStateManager.GoToState(layoutAwareControl, visualState, false);
                }
            }
        }

        /// <summary>
        /// Translates <see cref="ApplicationViewState"/> values into strings for visual state
        /// management within the page.  The default implementation uses the names of enum values.
        /// Subclasses may override this method to control the mapping scheme used.
        /// </summary>
        /// <param name="viewState">View state for which a visual state is desired.</param>
        /// <returns>Visual state name used to drive the
        /// <see cref="VisualStateManager"/></returns>
        /// <seealso cref="InvalidateVisualState"/>
        protected virtual string DetermineVisualState(ApplicationViewState viewState)
        {
            return viewState.ToString();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property provides the group to be displayed.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Returning to a cached page through navigation shouldn't trigger state loading
            if (this.pageKey != null)
            {
                return;
            }

            var frameState = SuspensionManager.SessionStateForFrame(this.Frame);

            this.pageKey = "Page-" + this.Frame.BackStackDepth;

            if (e.NavigationMode == NavigationMode.New)
            {
                // Clear existing state for forward navigation when adding a new page to the
                // navigation stack
                var nextPageKey = this.pageKey;
                int nextPageIndex = this.Frame.BackStackDepth;
                while (frameState.Remove(nextPageKey))
                {
                    nextPageIndex++;
                    nextPageKey = "Page-" + nextPageIndex;
                }

                // Pass the navigation parameter to the new page
                this.LoadState(e.Parameter, null);
            }
            else
            {
                // Pass the navigation parameter and preserved page state to the page, using
                // the same strategy for loading suspended state and recreating pages discarded
                // from cache
                this.LoadState(e.Parameter, (Dictionary<string, object>)frameState[this.pageKey]);
            }
        }

        /// <summary>
        /// Invoked when this page will no longer be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property provides the group to be displayed.</param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            var frameState = SuspensionManager.SessionStateForFrame(this.Frame);
            var pageState = new Dictionary<string, object>();
            this.SaveState(pageState);
            frameState[this.pageKey] = pageState;
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected virtual void LoadState(object navigationParameter, Dictionary<string, object> pageState)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected virtual void SaveState(Dictionary<string, object> pageState)
        {
        }

        /// <summary>
        /// Invoked as an event handler to navigate backward in the page's associated
        /// <see cref="Frame"/> until it reaches the top of the navigation stack.
        /// </summary>
        /// <param name="sender">Instance that triggered the event.</param>
        /// <param name="e">Event data describing the conditions that led to the event.</param>
        protected virtual void GoHome(object sender, RoutedEventArgs e)
        {
            // Use the navigation frame to return to the topmost page
            if (this.Frame != null)
            {
                while (this.Frame.CanGoBack)
                {
                    this.Frame.GoBack();
                }
            }
        }

        /// <summary>
        /// Invoked as an event handler to navigate backward in the navigation stack
        /// associated with this page's <see cref="Frame"/>.
        /// </summary>
        /// <param name="sender">Instance that triggered the event.</param>
        /// <param name="e">Event data describing the conditions that led to the
        /// event.</param>
        protected virtual void GoBack(object sender, RoutedEventArgs e)
        {
            // Use the navigation frame to return to the previous page
            if (this.Frame != null && this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }

        /// <summary>
        /// Evento click na logo.
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">TappedRoutedEventArgs e</param>
        protected virtual void GoToSite(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Uri targetUri = new Uri(Constantes.URLSite);
            Launcher.LaunchUriAsync(targetUri);
        }

        /// <summary>
        /// Invoked as an event handler to navigate forward in the navigation stack
        /// associated with this page's <see cref="Frame"/>.
        /// </summary>
        /// <param name="sender">Instance that triggered the event.</param>
        /// <param name="e">Event data describing the conditions that led to the
        /// event.</param>
        protected virtual void GoForward(object sender, RoutedEventArgs e)
        {
            // Use the navigation frame to move to the next page
            if (this.Frame != null && this.Frame.CanGoForward)
            {
                this.Frame.GoForward();
            }
        }

        /// <summary>
        /// Chamado quando a execução do aplicativo está sendo suspenso. O estado do aplicativo é salvo
        /// Sem saber se a aplicação será encerrada ou reiniciada com o conteúdo
        /// Da memória continua intacta.
        /// </summary>
        /// <param name="sender">A origem do pedido de suspensão.</param>
        /// <param name="e">Detalhes sobre o pedido de suspensão.</param>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            await SuspensionManager.SaveAsync();
            deferral.Complete();
        }

        /// <summary>
        /// Evento click na logo.
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">TappedRoutedEventArgs e</param>
        private void Image_Tapped_1(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Uri targetUri = new Uri("http://www.ibahia.com.br");
            Launcher.LaunchUriAsync(targetUri);
        }

        /// <summary>
        /// Invoked on every keystroke, including system keys such as Alt key combinations, when
        /// this page is active and occupies the entire window.  Used to detect keyboard navigation
        /// between pages even when the page itself doesn't have focus.
        /// </summary>
        /// <param name="sender">Instance that triggered the event.</param>
        /// <param name="args">Event data describing the conditions that led to the event.</param>
        private void CoreDispatcher_AcceleratorKeyActivated(CoreDispatcher sender, AcceleratorKeyEventArgs args)
        {
            var virtualKey = args.VirtualKey;

            // Only investigate further when Left, Right, or the dedicated Previous or Next keys
            // are pressed
            if ((args.EventType == CoreAcceleratorKeyEventType.SystemKeyDown ||
                args.EventType == CoreAcceleratorKeyEventType.KeyDown) &&
                (virtualKey == VirtualKey.Left || virtualKey == VirtualKey.Right ||
                (int)virtualKey == 166 || (int)virtualKey == 167))
            {
                var coreWindow = Window.Current.CoreWindow;
                var downState = CoreVirtualKeyStates.Down;
                bool menuKey = (coreWindow.GetKeyState(VirtualKey.Menu) & downState) == downState;
                bool controlKey = (coreWindow.GetKeyState(VirtualKey.Control) & downState) == downState;
                bool shiftKey = (coreWindow.GetKeyState(VirtualKey.Shift) & downState) == downState;
                bool noModifiers = !menuKey && !controlKey && !shiftKey;
                bool onlyAlt = menuKey && !controlKey && !shiftKey;

                if (((int)virtualKey == 166 && noModifiers) ||
                    (virtualKey == VirtualKey.Left && onlyAlt))
                {
                    // When the previous key or Alt+Left are pressed navigate back
                    args.Handled = true;
                    this.GoBack(this, new RoutedEventArgs());
                }
                else if (((int)virtualKey == 167 && noModifiers) ||
                    (virtualKey == VirtualKey.Right && onlyAlt))
                {
                    // When the next key or Alt+Right are pressed navigate forward
                    args.Handled = true;
                    this.GoForward(this, new RoutedEventArgs());
                }
            }
        }

        /// <summary>
        /// Invoked on every mouse click, touch screen tap, or equivalent interaction when this
        /// page is active and occupies the entire window.  Used to detect browser-style next and
        /// previous mouse button clicks to navigate between pages.
        /// </summary>
        /// <param name="sender">Instance that triggered the event.</param>
        /// <param name="args">Event data describing the conditions that led to the event.</param>
        private void CoreWindow_PointerPressed(CoreWindow sender, PointerEventArgs args)
        {
            var properties = args.CurrentPoint.Properties;

            // Ignore button chords with the left, right, and middle buttons
            if (properties.IsLeftButtonPressed || properties.IsRightButtonPressed ||
                properties.IsMiddleButtonPressed)
            {
                return;
            }

            // If back or foward are pressed (but not both) navigate appropriately
            bool backPressed = properties.IsXButton1Pressed;

            bool forwardPressed = properties.IsXButton2Pressed;

            if (backPressed ^ forwardPressed)
            {
                args.Handled = true;

                if (backPressed)
                {
                    this.GoBack(this, new RoutedEventArgs());
                }

                if (forwardPressed)
                {
                    this.GoForward(this, new RoutedEventArgs());
                }
            }
        }

        /// <summary>
        /// Método WindowSizeChanged
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e"> WindowSizeChangedEventArgs e</param>
        private void WindowSizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            this.InvalidateVisualState();
        }

        /// <summary>
        /// Implementation of IObservableMap that supports reentrancy for use as a default view
        /// model.
        /// </summary>
        /// <typeparam name="K"> K key</typeparam>
        /// <typeparam name="V">V value</typeparam>
        public class ObservableDictionary<K, V> : IObservableMap<K, V>
        {
            /// <summary>
            /// Atríbuto _dictionary
            /// </summary>
            private Dictionary<K, V> dictionary = new Dictionary<K, V>();

            /// <summary>
            /// Atríbuto do evento MapChanged
            /// </summary>
            public event MapChangedEventHandler<K, V> MapChanged;

            /// <summary>
            /// Gets a value indicating whether the item is value.
            /// </summary>
            public bool IsReadOnly
            {
                get { return false; }
            }

            /// <summary>
            /// Gets - Método Values
            /// </summary>
            public ICollection<V> Values
            {
                get { return this.dictionary.Values; }
            }

            /// <summary>
            /// Gets - Método Keys
            /// </summary>
            public ICollection<K> Keys
            {
                get { return this.dictionary.Keys; }
            }

            /// <summary>
            /// Gets - Método Count
            /// </summary>
            public int Count
            {
                get { return this.dictionary.Count; }
            }

            /// <summary>
            /// Gets or Sets - Método V
            /// </summary>
            /// <param name="key">K key</param>
            /// <returns>V Valuee</returns>
            public V this[K key]
            {
                get
                {
                    return this.dictionary[key];
                }

                set
                {
                    this.dictionary[key] = value;
                    this.InvokeMapChanged(CollectionChange.ItemChanged, key);
                }
            }

            /// <summary>
            /// Método Add
            /// </summary>
            /// <param name="key">K key</param>
            /// <param name="value">V value</param>
            public void Add(K key, V value)
            {
                this.dictionary.Add(key, value);
                this.InvokeMapChanged(CollectionChange.ItemInserted, key);
            }

            /// <summary>
            /// Método Add
            /// </summary>
            /// <param name="item">KeyValuePair item</param>
            public void Add(KeyValuePair<K, V> item)
            {
                this.Add(item.Key, item.Value);
            }

            /// <summary>
            /// Método Remove
            /// </summary>
            /// <param name="key">K ke</param>
            /// <returns>Retorna booleano</returns>
            public bool Remove(K key)
            {
                if (this.dictionary.Remove(key))
                {
                    this.InvokeMapChanged(CollectionChange.ItemRemoved, key);
                    return true;
                }

                return false;
            }

            /// <summary>
            /// Método Remove
            /// </summary>
            /// <param name="item">KeyValuePair item</param>
            /// <returns>Retorna booleano</returns>
            public bool Remove(KeyValuePair<K, V> item)
            {
                V currentValue;

                if (this.dictionary.TryGetValue(item.Key, out currentValue) &&
                    object.Equals(item.Value, currentValue) && this.dictionary.Remove(item.Key))
                {
                    this.InvokeMapChanged(CollectionChange.ItemRemoved, item.Key);
                    return true;
                }

                return false;
            }

            /// <summary>
            /// Método Clear
            /// </summary>
            public void Clear()
            {
                var priorKeys = this.dictionary.Keys.ToArray();
                this.dictionary.Clear();
                foreach (var key in priorKeys)
                {
                    this.InvokeMapChanged(CollectionChange.ItemRemoved, key);
                }
            }

            /// <summary>
            /// Método ContainsKey
            /// </summary>
            /// <param name="key">K key</param>
            /// <returns>Retorna Booleano</returns>
            public bool ContainsKey(K key)
            {
                return this.dictionary.ContainsKey(key);
            }

            /// <summary>
            /// Método TryGetValue
            /// </summary>
            /// <param name="key">K key</param>
            /// <param name="value">out V value</param>
            /// <returns>Retorna Booleano</returns>
            public bool TryGetValue(K key, out V value)
            {
                return this.dictionary.TryGetValue(key, out value);
            }

            /// <summary>
            /// Método Contains
            /// </summary>
            /// <param name="item">KeyValuePair item</param>
            /// <returns>Retorna Booleano</returns>
            public bool Contains(KeyValuePair<K, V> item)
            {
                return this.dictionary.Contains(item);
            }

            /// <summary>
            /// Método IEnumerator
            /// </summary>
            /// <returns>KeyValuePair GetEnumerator</returns>
            public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
            {
                return this.dictionary.GetEnumerator();
            }

            /// <summary>
            /// Método CopyTo
            /// </summary>
            /// <param name="array">KeyValuePair array</param>
            /// <param name="arrayIndex">int arrayIndex</param>
            public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
            {
                int arraySize = array.Length;
                foreach (var pair in this.dictionary)
                {
                    if (arrayIndex >= arraySize)
                    {
                        break;
                    }

                    array[arrayIndex++] = pair;
                }
            }

            /// <summary>
            /// Método IEnumerable
            /// </summary>
            /// <returns>_dictionary GetEnumerator</returns>
            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.dictionary.GetEnumerator();
            }

            /// <summary>
            /// Delegação de evento InvokeMapChanged
            /// </summary>
            /// <param name="change">CollectionChange change</param>
            /// <param name="key">K key</param>
            private void InvokeMapChanged(CollectionChange change, K key)
            {
                var eventHandler = this.MapChanged;

                if (eventHandler != null)
                {
                    eventHandler(this, new ObservableDictionaryChangedEventArgs(change, key));
                }
            }

            /// <summary>
            /// Classe ObservableDictionaryChangedEventArgs
            /// </summary>
            private class ObservableDictionaryChangedEventArgs : IMapChangedEventArgs<K>
            {
                /// <summary>
                /// Initializes a new instance of the <see cref="ObservableDictionaryChangedEventArgs"/> class.
                /// Atríbuto do evento ObservableDictionaryChangedEventArgs
                /// </summary>
                /// <param name="change">CollectionChange change</param>
                /// <param name="key"> K key</param>
                public ObservableDictionaryChangedEventArgs(CollectionChange change, K key)
                {
                    this.CollectionChange = change;
                    this.Key = key;
                }

                /// <summary>
                /// Gets - Atríbuto do evento CollectionChange
                /// </summary>
                public CollectionChange CollectionChange { get; private set; }

                /// <summary>
                /// Gets - Atríbuto Key
                /// </summary>
                public K Key { get; private set; }
            }
        }
    }
}
