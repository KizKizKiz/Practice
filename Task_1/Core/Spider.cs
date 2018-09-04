using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1.Core
{
    public class Spider:Insect
    {
        private bool _hasPoison;

        public bool HasPoison
        {
            get { return _hasPoison; }
            set
            {
                IsDangerous = value ? true : false;
                _hasPoison = value;
            }
        }

        public bool IsRare { get; set; }  

        public Spider()
            :this(false, false)
        {
            
        }

        public Spider(bool hasPoison, bool isRare)
            :base()
        {
            HasPoison = hasPoison;
            IsRare = isRare;
        }

        public override string ToString()
        {
            return string.Format($"Type:{GetType().Name}" +
                $"\n\tName:{this.Name}" +
                $"\n\tAge:{this.Age}" +
                $"\n\tCount of feet:{this.Feet}" +
                $"\n\tHas poison:{this.HasPoison}" +
                $"\n\tIs rare:{this.IsRare} ");
        }
    }
}
