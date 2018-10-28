using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
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
