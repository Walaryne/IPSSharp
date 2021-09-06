using System;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;

namespace IPSSharp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Greeting => "IPSSharp v0.1.0";

        public string PatchButtonText => "Patch";
        
    }
}
