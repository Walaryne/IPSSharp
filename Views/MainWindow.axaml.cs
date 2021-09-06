using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using IPSSharp.ViewModels;

namespace IPSSharp.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void QuitButton_OnClick(object? sender, RoutedEventArgs e)
        {
            Close();
        }
        private void InputElement_OnPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            this.Get<Panel>("RootPanel").Focus();
        }
    }
}
