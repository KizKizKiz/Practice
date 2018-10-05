using DataBinding.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBinding.Model
{
    public class AnimalType
    {
        public SQUAD ID { get; set; }        
        public virtual List<Animal> Animals { get; set; }
    }
}
