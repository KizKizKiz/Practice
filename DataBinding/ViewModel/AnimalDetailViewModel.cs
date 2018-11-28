using DataBinding.AnimalService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace DataBinding.ViewModel
{
    class AnimalDetailViewModel : ViewModelBase, IDataErrorInfo
    {
        private Window _detailView;
        private AnimalServiceClient _animalService;
        /// <summary>
        /// Получает/задает окно детального отображения информации
        /// </summary
        public Window DetailView
        {
            get { return _detailView; }
            set
            {
                _detailView = value;
                _detailView.DataContext = this;
            }
        }        
        /// <summary>
        /// Получает/задает имя животного
        /// </summary>
        public string Name
        {
            get
            {
                return Animal.Name;
            }
            set
            {
                var name = Animal.Name;
                Animal.Name = value;
                SetProperty(ref name, value);
            }
        }
        private Animal _animal;
        public Animal Animal
        {
            get
            {
                return _animal;
            }
            set
            {
                SetProperty(ref _animal, value);                
            }
        }
        /// <summary>
        /// Окно детального отображения данных
        /// </summary>
        public AnimalDetailViewModel(Animal animal, AnimalServiceClient animalService)
        {
            _animalService = animalService;
            Squads = animalService.Squads();
            Animal = animal;
            _cachedAnimal = Serialize(animal.GetType(), Animal);
            SelectedSquad = Animal.Squad;
        }
        private Animal _cachedAnimal;

        private SQUAD _squad;
        /// <summary>
        /// Получает/задает выбранный тип животного
        /// </summary>
        public SQUAD SelectedSquad
        {
            get
            {
                return _squad;
            }
            set
            {
                SetProperty(ref _squad, value);
                Type type = null;
                switch (_squad) {
                    case SQUAD.spiders: {
                        HideButterfly = Visibility.Collapsed;
                        HideSpider = Visibility.Visible;
                        type = typeof(Spider);
                        break;
                    }
                    case SQUAD.lepidoptera: {
                        HideButterfly = Visibility.Visible;
                        HideSpider = Visibility.Collapsed;
                        type = typeof(Butterfly);
                        break;
                    }
                }

                if (SelectedSquad != Animal.Squad) {                    
                    Animal = Serialize(type, Animal);                    
                    Animal.Squad = SelectedSquad;
                }
            }
        }
        private Animal Serialize(Type type, Animal source)
        {
            var _cachedAnimalProperties = source.GetType().GetProperties();
            var animal = (Animal) Activator.CreateInstance(type);
            var properties = type.GetProperties();
            foreach (var propertyInfo in properties) {
                var prop = _cachedAnimalProperties.FirstOrDefault((property) => property.Name == propertyInfo.Name);
                if (prop != null) {
                    var value = prop.GetValue(source);
                    prop.SetValue(animal, value);
                }
            }
            return animal;
        }        
        private RelayCommand _saveToDb;
        /// <summary>
        /// Сохраняет изменения и кэширует сохраняемый объект
        /// </summary>
        public RelayCommand Save
        {
            get
            {
                return _saveToDb ??
                    (_saveToDb = new RelayCommand("Сохранить",
                    (obj) => {
                        try {
                            _animalService.Save(Animal, Animal.ID);
                            _cachedAnimal = Serialize(Animal.GetType(), Animal);
                        }
                        catch (Exception e) {
                            MessageBox.Show(e.Message);
                        }                        
                    },
                    (obj) => true ));
            }
        }        
        private RelayCommand _cancel;
        /// <summary>
        /// Отменяет произведенный действия и приводит объект к изначальному состоянию
        /// </summary>
        public RelayCommand Cancel
        {
            get
            {
                return _cancel ??
                    (_cancel = new RelayCommand("Отмена",
                    (obj) => {                        
                        Name = _cachedAnimal.Name;                        
                        SelectedSquad = _cachedAnimal.Squad;                        
                        Animal = _cachedAnimal;
                        _cachedAnimal = Serialize(Animal.GetType(), Animal);
                        DetailView.Close();
                    },
                    (obj) =>true));
            }
        }
        private List<SQUAD> _squads;
        /// <summary>
        /// Получает/задает типы животных
        /// </summary>
        public List<SQUAD> Squads
        {
            get
            {
                return _squads;
            }
            set
            {
                SetProperty(ref _squads, value);
            }
        }
        private Visibility _hideButterfly;
        /// <summary>
        /// Управляет видимостью полей бабочки
        /// </summary>        
        public Visibility HideButterfly
        {
            get
            {
                return _hideButterfly;
            }
            set
            {
                SetProperty(ref _hideButterfly, value);
            }
        }
        private Visibility _hideSpider;
        /// <summary>
        /// Управляет видимостью полей паука
        /// </summary>
        public Visibility HideSpider
        {
            get
            {
                return _hideSpider;
            }
            set
            {
                SetProperty(ref _hideSpider, value);
            }
        }

        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName) {
                    case "WingsArea": {
                        if (string.IsNullOrWhiteSpace(Name)) {
                            error = "Поле не должно быть пустым";
                        }
                        break;
                    }
                }
                return error;
            }
        }
    }       
}
