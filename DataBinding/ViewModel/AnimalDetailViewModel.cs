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

namespace DataBinding.ViewModel
{
    class AnimalDetailViewModel : ViewModelBase
    {
        private DBAnimal _animals;

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
        private DetailView _detailView;
        public AnimalDetailViewModel(Animal animal, DBAnimal context)
        {            
            _detailView = new DetailView()
            {
                DataContext = this
            };
            Animal = animal;
            _cachedInsect =  _animal;
            _animals = context;
            Squads = _animals.              
                Load().
                Select(c=>c.SquadId).
                Distinct().
                ToList();            
            SelectedSquad = Animal.SquadId;
            _detailView.ShowDialog();
        }
        private Animal _cachedInsect;

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
                switch (_squad) {
                    case SQUAD.spiders: {
                        HideButterfly = Visibility.Collapsed;
                        HideSpider = Visibility.Visible;
                        TryChangeTypeOfAnimal(typeof(Spider));
                        break;
                    }
                    case SQUAD.lepidoptera: {
                        HideButterfly = Visibility.Visible;
                        HideSpider = Visibility.Collapsed;
                        TryChangeTypeOfAnimal(typeof(Butterfly));
                        break;
                    }
                }                           
            }
        }
        private bool TryChangeTypeOfAnimal(Type type)
        {
            if (_squad == _cachedInsect.SquadId) {
                Animal = _cachedInsect;
                return false;
            }
            var _cachedInsectProperties = _cachedInsect.GetType().GetProperties();
            var insect = (Animal) Activator.CreateInstance(type);            
            var properties = type.GetProperties();
            foreach (var propertyInfo in properties) {
                var prop = _cachedInsectProperties.FirstOrDefault((property) => property.Name == propertyInfo.Name);                
                if (prop != null && prop.PropertyType != typeof(AnimalType)) {
                    var value = prop.GetValue(_cachedInsect);
                    prop.SetValue(insect, value);
                }                                
            }            

            Animal = insect;
            Animal.AnimalType = _cachedInsect.AnimalType;
            return true;
        }

        private RelayCommand _saveToDb;
        public RelayCommand Save
        {
            get
            {
                return _saveToDb ??
                    (_saveToDb = new RelayCommand("Сохранить",
                    (obj) => _animals.Save(Animal),
                    (obj) => _animals.HasChanged(Animal) == true));
            }
        }
        private RelayCommand _cancel;
        public RelayCommand Cancel
        {
            get
            {
                return _cancel ??
                    (_cancel = new RelayCommand("Отмена",
                    (obj)=> {                        
                        _detailView.Close();
                    },
                    null ));
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
