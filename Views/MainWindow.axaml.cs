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
        private void RomFilePickerButton_OnClick(object? sender, RoutedEventArgs e)
        {
            GetFileFromDialog("RomFileText");
        }
        private void IPSFilePickerButton_OnClick(object? sender, RoutedEventArgs e)
        {
            GetFileFromDialog("IPSFileText");
        }
        private void OutputFilePickerButton_OnClick(object? sender, RoutedEventArgs e)
        {
            GetFileFromDialog("OutputFileText");
        }

        //Awaits a file dialog's return value and sets the Text value of TextBox with name "name"
        private async void GetFileFromDialog(string name)
        {
            string[] filePaths = await new OpenFileDialog().ShowAsync(this);
            if (filePaths != null)
            {
                this.FindControl<TextBox>(name).Text = filePaths[0];
            }
        }
    }
}
