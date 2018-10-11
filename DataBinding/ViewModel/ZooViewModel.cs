using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Configuration;
using DataBinding.View;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using System.Reflection;
using DataBinding.Core;
using DataBinding.Model;
using DataBinding.Model.DAL;
using System.Diagnostics;
using System.ComponentModel;

namespace DataBinding.ViewModel
{
    class ZooViewModel : ViewModelBase
    {
        private DBAnimal _essential;
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
        /// <summary>
        /// Получает/задает выбранный цвет
        /// </summary>
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
                    (obj) => {
                        SelectedAnimal.DetailView.ShowDialog();
                        },
                    (obj) => SelectedAnimal != null));
            }
        }
        private AnimalDetailViewModel _selectedAnimal;
        /// <summary>
        /// Получает/задает выбранное животное
        /// </summary>
        public AnimalDetailViewModel SelectedAnimal
        {
            get
            {
                return _selectedAnimal;
            }
            set
            {
                SetProperty(ref _selectedAnimal, value);                
            }
        }

        private List<ViewModelBase> _animals;
        /// <summary>
        /// Получает/задает коллекцию животных 
        /// </summary>
        public List<ViewModelBase> Animals
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
            Colors = _propertiesOfColors.Select((color) => color.Name).ToList();            
            _essential = new DBAnimal();

            var animals = _essential.Load().ToList();
            Animals = new List<ViewModelBase>();                        
            for (int i = 0; i < animals.Count; i++) {
                Animals.Add(new AnimalDetailViewModel(animals[i], _essential));
            }           
        }
    }
}
