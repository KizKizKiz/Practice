using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1.Core
{
    public class Animal
    {
        private Int32 _age;

        private string _name;

        public Int32 Age 
        {
            get { return _age; }
            set
            {
                if (value>=100 || value <0) {
                    throw new ArgumentOutOfRangeException("Age must be in range from 0 to 100");
                }
                _age = value;
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentException("Name cannot be null or contains only white space");
                }
                _name = value;
            }
        }

        public Animal():
            this(default(int), "NoName")
        { }          

        public Animal(int age, string name)
        {
            Age = age;
            Name = name;
        }

    }
}
