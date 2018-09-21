using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Configuration;
using Task_1.Core;
using Task_1;
using DataBinding.View;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using System.Reflection;

namespace DataBinding.ViewModel
{
    class ZooViewModel : ViewModelBase
    {
        private Essential _essential;
        private PropertyInfo[] _propertiesOfColors;
        private List<string> _colors;
        /// <summary>
        /// Получает или задает цвет
        /// </summary>
        public List<string> Colors
        {
            get
            {
                return _colors;
            }
            set
            {
                SetProperty(ref _colors, value);
            }
        }

        private string _selectedColor;
        public string SelectedColor
        {
            get
            {
                return _selectedColor;
            }
            set
            {
                SetProperty(ref _selectedColor, value);
                var property = _propertiesOfColors.FirstOrDefault((col) => col.Name == value);
                var methodInfo = property.GetMethod;
                var color = (Color) methodInfo.Invoke(null, null);
                Application.Current.Resources["DefaultColor"] = new SolidColorBrush(color);
            }
        }
        private RelayCommand _detail;
        /// <summary>
        /// Отображает окно с детальной информацией о животном
        /// </summary>
        public RelayCommand Detail
        {
            get
            {
                return _detail ??
                    (_detail = new RelayCommand("Детально",
                    (obj) => new AnimalDetailViewModel(SelectedAnimal, _essential),
                    (obj) => SelectedAnimal != null));
            }
        }
        private Animal _selectedAnimal;
        /// <summary>
        /// Получает/задает выбранное животное
        /// </summary>
        public Animal SelectedAnimal
        {
            get
            {
                return _selectedAnimal;
            }
            set
            {
                SetProperty<Animal>(ref _selectedAnimal, value);
            }
        }

        private List<Animal> _animals;
        /// <summary>
        /// Получает/задает коллекцию животных 
        /// </summary>
        public List<Animal> Animals
        {
            get
            {
                return _animals;
            }
            set
            {
                SetProperty(ref _animals, value);
            }
        }
        public ZooViewModel()
        {
            _propertiesOfColors = typeof(Colors).GetProperties();
            var colorsName = _propertiesOfColors.Select((color) => color.Name);
            Colors = new List<string>(colorsName);
            _essential = new Essential();
            _essential.ConnectionString = ConfigurationSettings.AppSettings["AnimalSqlProvider"];
            Animals = _essential.Load("SELECT * FROM ANIMALS");

            SomeActions = new List<ICommand>()
            {
                new RelayCommand("Show 1", (obj)=>MessageBox.Show("Hello 1"),(obj)=>true),
                new RelayCommand("Show 2", (obj)=>MessageBox.Show("Hello 2"),(obj)=>true)
            };        
        }
        private List<ICommand> _actions;
        public List<ICommand> SomeActions
        {
            get
            {
                return _actions;
            }
            set
            {
                SetProperty(ref _actions, value);
            }
        }
    }
}
