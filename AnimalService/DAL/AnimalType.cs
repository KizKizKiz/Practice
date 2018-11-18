using Practice.Core;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Service.DAL
{
    [DataContract(Name = "SquadServiceContract")]
    public class AnimalType:IKey
    {      
        [DataMember]
        public SQUAD Type { get; set; }
        [DataMember]
        public virtual List<Animal> Animals { get; set; }
        [DataMember]
        public int ID { get; set; }
    }
}
