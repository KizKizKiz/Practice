using System;
using System.Windows;

using DataBinding.ViewModel;
namespace DataBinding
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ZooViewModel(); 
        }
    }
}
