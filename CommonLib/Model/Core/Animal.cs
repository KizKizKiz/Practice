using System;
using System.Runtime.Serialization;
using System.ServiceModel;
namespace Practice.Core
{
    /// <summary>
    /// Отряды животных
    /// </summary>
    [DataContract]
    public enum SQUAD
    {
        [EnumMember]
        spiders = 1,
        [EnumMember]
        lepidoptera = 2
    }
    [DataContract]
    [KnownType(typeof(Butterfly))]
    [KnownType(typeof(Spider))]
    [KnownType(typeof(Insect))]
    abstract public class Animal: IKey
    {
        /// <summary>
        /// Отряд животного
        /// </summary>       
        [DataMember]
        public virtual SQUAD Squad { get; set; }        
        private int _age;
        /// <summary>
        /// Получает/задает имя животного
        /// </summary>
        [DataMember]
        public int Age
        {
            get { return _age; }
            set
            {
                if (value >= 100 || value < 0) {
                    throw new ArgumentOutOfRangeException("Age must be in range from 0 to 100");
                }
                _age = value;
            }
        }        
        private string _name;
        /// <summary>
        /// Получает/задает возраст животного        
        /// </summary>
        [DataMember]
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
        /// <summary>
        /// Идентификатор животного
        /// </summary>
        [DataMember]
        public int ID { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Animal"/> с полями, выставленными в значение по умолчанию 
        /// </summary>
        public Animal():
            this(default(int), "NoName")
        { }
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Animal"/> с указанными значениями полей
        /// </summary>
        /// <param name="age">Возраст животного</param>
        /// <param name="name">Имя животного</param>
        public Animal(int age, string name)
        {
            Age = age;
            Name = name; 
        }
        /// <summary>
        /// Представляет информацию об объекте <see cref="Animal"/>
        /// </summary> 
        public override string ToString()
        {
            return $"ID:{ID}" +
                   $"\n\tName:{Name}" +
                   $"\n\tAge:{Age}"+
                   $"\n\tSquad:{Squad}";
        }
       
    }
}
