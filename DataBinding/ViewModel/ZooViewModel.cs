using System.Collections.Generic;
using System.Linq;
using DataBinding.View;
using System.Windows.Media;
using System.Windows;
using System.Reflection;
using System.Diagnostics;
using DataBinding.AnimalService;
namespace DataBinding.ViewModel
{
    class ZooViewModel : ViewModelBase
    {
        private AnimalServiceClient _animalService;        
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
                        SelectedAnimal.DetailView = new DetailView();
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
            _animalService = new AnimalServiceClient();
            _propertiesOfColors = typeof(Colors).GetProperties();
            Colors = _propertiesOfColors.Select((color) => color.Name).ToList();                        

            Animals = _animalService.Animals().
                Select(s => new AnimalDetailViewModel(s, _animalService)).ToList<ViewModelBase>();
        }
    }
}
