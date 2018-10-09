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
        public SQUAD Type { get; set; }     
        public List<Animal> Animals { get; set; }
    }
}
