using System.Threading.Tasks;
using IPSSharp.Models;
using ReactiveUI;
namespace IPSSharp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Greeting => "IPSSharp v0.1.0";

        private string statusLineText;
        
        private string romFileText;
        private string ipsFileText;
        private string outputFileText;
        private readonly Patcher patcher = new();
        
        private bool overwriteCheckboxValue;
        public string StatusLineText
        {
            get => statusLineText;
            set => this.RaiseAndSetIfChanged(ref statusLineText, value);
        }

        public string RomFileText
        {
            get => romFileText;
            set => this.RaiseAndSetIfChanged(ref romFileText, value);
        }
        
        public string IPSFileText
        {
            get => ipsFileText;
            set => this.RaiseAndSetIfChanged(ref ipsFileText, value);
        }
        
        public string OutputFileText
        {
            get => outputFileText;
            set => this.RaiseAndSetIfChanged(ref outputFileText, value);
        }

        public bool OverwriteCheckboxValue
        {
            get => overwriteCheckboxValue;
            set => this.RaiseAndSetIfChanged(ref overwriteCheckboxValue, value);
        }

        public MainWindowViewModel()
        {
            statusLineText = "Waiting...";
            
            romFileText = string.Empty;
            ipsFileText = string.Empty;
            outputFileText = string.Empty;
        }

        public async Task PatchClicked()
        {
            StatusLineText = "Patching...";

            patcher.romFile = romFileText;
            patcher.ipsFile = ipsFileText;
            patcher.outputFile = outputFileText;
            patcher.overwrite = overwriteCheckboxValue;

            await Task.Run(() =>
            {
                patcher.Patch();
            });
            
            StatusLineText = "Patched!";
        }
    }
}
