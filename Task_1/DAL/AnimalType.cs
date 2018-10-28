using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_1;
using Task_1.Core;

namespace ConsoleApp.Model
{
    public class AnimalType:IKey
    {
        public int ID { get; set; }
        public SQUAD Type { get; set; }

        public virtual List<Animal> Animals { get; set; }
    }
}
