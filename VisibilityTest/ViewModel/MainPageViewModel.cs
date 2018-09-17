using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Content_test
{
    enum WindowsVersion
    {
        Windows8,
        Windows7,
        WindowsXP,
        Windows10
    }

    class MainPageViewModel:INotifyPropertyChanged
    {
        public MainPageViewModel()
        {
            Windows = new List<WindowsVersion>() { WindowsVersion.Windows10, WindowsVersion.Windows7, WindowsVersion.WindowsXP };
            SelectedWindows = WindowsVersion.Windows8;
            
        }

        private List<WindowsVersion> _windows;
        public List<WindowsVersion> Windows
        {
            get
            {
                return _windows;
            }
            set
            {
                _windows = value;
                OnPropertyChanged("Windows");
            }
        }

        private WindowsVersion _selectedWindows;
        public WindowsVersion SelectedWindows
        {
            get
            {
                if (_selectedWindows==WindowsVersion.WindowsXP) {
                    VisField1 = Visibility.Hidden;
                }
                return _selectedWindows;
            }
            set
            {
                _selectedWindows = value;
                OnPropertyChanged(nameof(SelectedWindows));                                             
            }
        }
        
        private Visibility _visField1;        
        public Visibility VisField1
        {
            get
            {                
                return _visField1;
            }
            set
            {
                _visField1 = value;
                OnPropertyChanged(nameof(VisField1));
            }

        }

        private Visibility _visField2;
        public Visibility VisField2
        {
            get
            {
                return _visField2;
            }
            set
            {
                _visField2 = value;
                OnPropertyChanged("VisField2");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(prop));
        }
    }
}
