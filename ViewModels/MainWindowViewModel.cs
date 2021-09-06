using System.Threading;
using System.Threading.Tasks;
using ReactiveUI;

namespace IPSSharp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Greeting => "IPSSharp v0.1.0";

        private string statusLineText;
        private bool overwriteCheckboxValue;
        public string StatusLineText
        {
            get => statusLineText;
            set => this.RaiseAndSetIfChanged(ref statusLineText, value);
        }

        public bool OverwriteCheckboxValue
        {
            get => overwriteCheckboxValue;
            set => this.RaiseAndSetIfChanged(ref overwriteCheckboxValue, value);
        }

        public MainWindowViewModel()
        {
            statusLineText = "Waiting...";
        }

        public async Task PatchClicked()
        {
            StatusLineText = "Patching...";
            await Task.Delay(5000);
            StatusLineText = "Patched!";
        }
        

    }
}
