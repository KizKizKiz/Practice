using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1.Core
{
    public class Insect:Animal
    {
        private int _feet;        

        public int Feet
        {
            get { return _feet; }
            set
            {
                if (value<0) {
                    throw new ArgumentOutOfRangeException("Feet must be greater than 0");
                }
                _feet = value;
            }
        }
        
        public bool IsDangerous { get; set; }        

        public Insect():
            this(default(int), false)
        {            
        }
        
        public Insect(int countOfFeet, bool dangerous = false)
            :base()
        {
            Feet = countOfFeet;
            IsDangerous = dangerous;
        }

        public override string ToString()
        {
            return string.Format($"Type:{GetType().Name}" +
                $"\n\tName:{this.Name}" +
                $"\n\tAge:{this.Age}" +
                $"\n\tCount of feet:{this.Feet}" +
                $"\n\tHas poison:{this.IsDangerous}");
        }
    }
}
