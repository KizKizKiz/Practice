using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Binding
{
    enum WinVersion
    {
        Windows98,
        WindowsXP,
        Windows7,
        Windows8
    }
    class MainViewModel : ViewModelBase
    {
        private List<WinVersion> _windows;
        public List<WinVersion> Windows
        {
            get
            { return _windows; }
            set
            { SetProperty(ref _windows, value); }
        }

        public MainViewModel()
        {
            Windows = new List<WinVersion>() { WinVersion.Windows7, WinVersion.Windows8, WinVersion.Windows98, WinVersion.WindowsXP };
            SelectedWindows = Windows[2];
        }        
        private Visibility _visField3;
        public Visibility VisField3
        {
            get
            {                
                return _visField3;
            }
            set
            {
                SetProperty(ref _visField3, value);
            }
        }

        private Visibility _visField4;
        public Visibility VisField4
        {
            get
            {                
                return _visField4;
            }
            set
            {                               
                SetProperty(ref _visField4, value, "VisField4");
            }
        }       

        private WinVersion _selectedWindow;
        public WinVersion SelectedWindows
        {
            get
            {                
                return _selectedWindow;
            }
            set
            {
                if (_selectedWindow==WinVersion.Windows7) {
                    VisField4 = Visibility.Hidden;
                }
                SetProperty(ref _selectedWindow, value);                
            }
        }
    }
}
