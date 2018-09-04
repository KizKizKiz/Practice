using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1.Core
{
    public class Butterfly:Insect
    {
        private String _color;

        private float _wingsArea;

        public String Color
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
            
        public float WingsArea
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

        public Butterfly():
            this("White", default(float))
        {            
        }

        public Butterfly(string color, float wingsArea) 
            :base()
        {
            Color = color;
            WingsArea = wingsArea;
        }

        public override string ToString()
        {
            return string.Format($"Type:{GetType().Name}" +
                $"\n\tName:{this.Name}" +
                $"\n\tAge:{this.Age}" +
                $"\n\tCount of feet:{this.Feet}" +
                $"\n\tColor:{this.Color}" +
                $"\n\tWings area:{this.WingsArea}");
        }
    }
}
