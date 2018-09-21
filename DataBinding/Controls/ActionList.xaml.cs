using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataBinding.Controls
{
    /// <summary>
    /// Логика взаимодействия для ActionList.xaml
    /// </summary>
    public partial class ActionList : UserControl
    {
        public ActionList()
        {
            InitializeComponent();
        }                
        /// <summary>
        /// Список команд
        /// </summary>
        public List<ICommand> Actions
        {
            get { return (List<ICommand>) GetValue(ActionProperty); }
            set { SetValue(ActionProperty, value); }
        }                        
        public static readonly DependencyProperty ActionProperty =
            DependencyProperty.Register(nameof(Actions), typeof(List<ICommand>), typeof(ActionList));
    }
}
