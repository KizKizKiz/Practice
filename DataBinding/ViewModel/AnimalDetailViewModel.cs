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
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {                               
                SetProperty(ref _name, value);
                Animal.Name = _name;
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
                Name = Animal.Name;
            }
        }        
        /// <summary>
        /// Окно детального отображения данных
        /// </summary>
        public AnimalDetailViewModel(Animal animal, DBAnimal context)
        {            
            _dbSquads = new DBSquad(context.Context);
            Squads = _dbSquads.
                LazyLoadTable().
                Select(c => c.Type).
                ToList();

            Animal = animal;
            _cachedInsect = _animal;
            _dbAnimals = context;
            SelectedSquad = Animal.Squad;
            Name = Animal.Name;
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
                _cachedInsect.ID = Animal.ID;
                TryChangeTypeOfAnimal(type);
            }
        }
        private bool TryChangeTypeOfAnimal(Type type)
        {
            if (_squad == _cachedInsect.Squad) {
                Animal = _cachedInsect;
                return false;
            }
            var _cachedInsectProperties = _cachedInsect.GetType().GetProperties();
            var insect = (Animal) Activator.CreateInstance(type);
            var properties = type.GetProperties();
            foreach (var propertyInfo in properties) {
                var prop = _cachedInsectProperties.FirstOrDefault((property) => property.Name == propertyInfo.Name);
                if (prop != null) {
                    var value = prop.GetValue(_cachedInsect);
                    prop.SetValue(insect, value);
                }
            }
            insect.Squad = SelectedSquad;
            Animal = insect;
            return true;
        }

        private RelayCommand _saveToDb;
        public RelayCommand Save
        {
            get
            {
                return _saveToDb ??
                    (_saveToDb = new RelayCommand("Сохранить",
                    (obj) => {
                        _dbAnimals.Save(Animal, Animal.ID);
                        _cachedInsect = Animal;
                    },
                    (obj) => _dbAnimals.HasModifiedOrDetached(Animal)));
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
                        Animal = _dbAnimals.Reload(_cachedInsect);
                        //Animal.Name = "TRAR";
                        DetailView.Close();
                    },
                    (obj) => _dbAnimals.HasModifiedOrDetached(Animal)));
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
