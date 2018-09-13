﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_1.Core;
using Task_1;
using DataBinding.View;
using System.Windows;

namespace DataBinding.ViewModel
{
    class AnimalDetailViewModel : ViewModelBase
    {
        private Essential _essential;
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
        public AnimalDetailViewModel(Animal animal, Essential essential)
        {
            _detailView = new DetailView()
            {
                DataContext = this
            };
            _animal = animal;
            _essential = essential;
            Squads = _essential.Load("SELECT DISTINCT Squad FROM ANIMALS").
                      Select((an) => an.Squad).ToList();
            SelectedSquad = _animal.Squad;
            _detailView.ShowDialog();
        }
        private SQUAD _squad;
        /// <summary>
        /// Получает/задает выбранный тип животного
        /// </summary>
        public SQUAD SelectedSquad
        {
            get
            {
                return _animal.Squad;
            }
            set
            {                
                SetProperty(ref _squad, value);
                HideElement();
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
        /// <summary>
        /// Управляет видимостью полей бабочки
        /// </summary>
        public Visibility HideButterfly { get; set; }
        /// <summary>
        /// Управляет видимостью полей паука
        /// </summary>
        public Visibility HideSpider { get; set; }
        /// <summary>
        /// Определяет тип животного и устанавливает видимость полей
        /// </summary>
        private void HideElement()
        {
            switch (_animal.Squad) {
                case SQUAD.spiders: {
                    HideButterfly = Visibility.Collapsed;
                    HideSpider = Visibility.Visible;
                    break;
                }
                case SQUAD.lepidoptera: {
                    HideButterfly = Visibility.Visible;
                    HideSpider = Visibility.Collapsed;
                    break;
                }
                default:
                break;
            }
        }
    }
}
