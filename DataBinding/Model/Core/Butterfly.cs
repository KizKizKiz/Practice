using System;
using System.Data;
using System.Data.SqlClient;


namespace Task_1.Core
{
    public class Butterfly:Insect
    {
        private string _color;
        /// <summary>
        /// Получает/задает цвет крыльев бабочки
        /// </summary>
        public string Color
        {
            get { return _color; }
            set
            {
                if (String.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentException("Color should not be null or has white space");
                }
                _color = value;
            }
        }

        private double _wingsArea;
        /// <summary>
        /// Получает/задает площадь крыльев бабочки
        /// </summary>      
        public double WingsArea
        {
            get { return _wingsArea; }
            set
            {
                if (value<0) {
                    throw new ArgumentOutOfRangeException("WingsArea cannot be less than zero");
                }
                _wingsArea = value;
            }
        }
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Butterfly"/> с полями, выставленными в значение по умолчанию
        /// </summary>
        public Butterfly():
            this("White", default(float))
        {            
        }
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Butterfly"/> с указанными значениями полей
        /// </summary>
        /// <param name="color">Цвет крыльев бабочки</param>
        /// <param name="wingsArea">Площадь крыльев бабочки</param>
        public Butterfly(string color, float wingsArea) 
            :base()
        {
            Color = color;
            WingsArea = wingsArea;
        }
        /// <summary>
        /// Представляет информацию об объекте <see cref="Butterfly"/>
        /// </summary>        
        public override string ToString()
        {
            return base.ToString()+
                $"\n\tColor:{Color}" +
                $"\n\tWings area:{WingsArea}";
        }        
    }
}
