using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBinding.View;
using System.Windows;
using DataBinding.Model;
using DataBinding.Core;
using DataBinding.Model.DAL;
using System.Diagnostics;
using DataBinding.Model.DAL.Context;

namespace DataBinding.ViewModel
{
    class AnimalDetailViewModel : ViewModelBase
    {
        private Window _detailView;
        public Window DetailView
        {
            get { return _detailView; }
            set
            {
                _detailView = value;
                _detailView.DataContext = this;
            }
        }
        private DBSquad _dbSquads;
        private DBAnimal _dbAnimals;
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
        public AnimalDetailViewModel(Animal animal, DBAnimal context)
        {
            _dbSquads = new DBSquad(context.Context);
            _dbAnimals = context;

            Squads = _dbSquads.
                LazyLoadTable().
                Select(c => c.Type).
                ToList();
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
                    _dbAnimals.Dettach(Animal);
                    Animal = Serialize(type, Animal);
                    _dbAnimals.Attach(Animal);
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
        public RelayCommand Save
        {
            get
            {
                return _saveToDb ??
                    (_saveToDb = new RelayCommand("Сохранить",
                    (obj) => {
                        _dbAnimals.DiscriminatorUpdate(Animal);
                        _dbAnimals.Save(Animal);                        
                        _cachedAnimal = Serialize(Animal.GetType(), Animal);
                    },
                    (obj) => _dbAnimals.IsModified(Animal)));
            }
        }        
        private RelayCommand _cancel;
        public RelayCommand Cancel
        {
            get
            {
                return _cancel ??
                    (_cancel = new RelayCommand("Отмена",
                    (obj) => {
                        _dbAnimals.Dettach(Animal);
                        _dbAnimals.Attach(_cachedAnimal);
                        Name = _cachedAnimal.Name;
                        Animal = _dbAnimals.Reload(_cachedAnimal);
                        SelectedSquad = Animal.Squad;                                                
                        DetailView.Close();
                    },
                    (obj) => _dbAnimals.IsModified(Animal)));
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
    }       
}
