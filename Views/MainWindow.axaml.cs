using System.Collections.Generic;
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

        private List<FileDialogFilter> openROMFileFilters, openIPSFileFilters, outputFileFilters;

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            
            openROMFileFilters = new List<FileDialogFilter>
            {
                new()
                {
                    Name = "SNES ROM Files",
                    Extensions = new List<string>
                    {
                        "smc"
                    }
                }
            };

            openIPSFileFilters = new List<FileDialogFilter>
            {
                new()
                {
                    Name = "IPS Files",
                    Extensions = new List<string>
                    {
                        "ips",
                        "IPS"
                    }
                }
            };

            outputFileFilters = new List<FileDialogFilter>
            {
                new()
                {
                    Name = "SNES ROM Files",
                    Extensions = new List<string>
                    {
                        "smc"
                    }
                }
            };
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
        private async void RomFilePickerButton_OnClick(object? sender, RoutedEventArgs e)
        {
            string[] filePaths = await new OpenFileDialog
            {
                Filters = openROMFileFilters
            }.ShowAsync(this);
            if (filePaths is { Length: > 0 })
            {
                this.FindControl<TextBox>("RomFileText").Text = filePaths[0];
            }
        }
        private async void IPSFilePickerButton_OnClick(object? sender, RoutedEventArgs e)
        {
            string[] filePaths = await new OpenFileDialog
            {
                Filters = openIPSFileFilters
            }.ShowAsync(this);
            if (filePaths is { Length: > 0 })
            {
                this.FindControl<TextBox>("IPSFileText").Text = filePaths[0];
            }
        }
        private async void OutputFilePickerButton_OnClick(object? sender, RoutedEventArgs e)
        {
            string filePaths = await new SaveFileDialog
            {
                Filters = outputFileFilters,
                DefaultExtension = "smc",
                InitialFileName = "output.smc"
            }.ShowAsync(this);
            if (filePaths is { Length: > 0 })
            {
                this.FindControl<TextBox>("OutputFileText").Text = filePaths;
            }

        }

        //Awaits a file dialog's return value and sets the Text value of TextBox with name "name"
        private async void GetFileFromDialog(string name)
        {
            string[] filePaths = await new OpenFileDialog().ShowAsync(this);
            if (filePaths is { Length: > 0 })
            {
                this.FindControl<TextBox>(name).Text = filePaths[0];
            }
        }
    }
}
