using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Configuration;
using Task_1.Core;
using Task_1;
using DataBinding.View;

namespace DataBinding.ViewModel
{
    class ZooViewModel : ViewModelBase
    {
        private Essential _essential;
        private RelayCommand _detail;
        /// <summary>
        /// Отображает окно с детальной информацией о животном
        /// </summary>
        public RelayCommand Detail
        {
            get
            {
                return _detail ??
                    (_detail = new RelayCommand(
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
            _essential = new Essential();
            _essential.ConnectionString = ConfigurationSettings.AppSettings["AnimalSqlProvider"];
            Animals = _essential.Load("SELECT * FROM ANIMALS");
        }
    }
}
