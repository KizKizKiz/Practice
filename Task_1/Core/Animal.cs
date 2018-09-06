using System;
using System.Data;
using System.Data.SqlClient;

namespace Task_1.Core
{
    /// <summary>
    /// Отряды животных
    /// </summary>
    public enum SQUAD
    {
        spiders = 1,
        lepidoptera = 2
    }
    public class Animal
    {        
        /// <summary>
        /// Отряд животного
        /// </summary>
        public SQUAD Squad { get; set; }

        private int _age;
        /// <summary>
        /// Получает/задает имя животного
        /// </summary>
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
            return $"Name:{Name}" +
                   $"\n\tAge:{Age}"+
                   $"\n\tSquad:{Squad}";
        }
        /// <summary>
        /// Инициализирует поля объекта <see cref="Animal"/>
        /// </summary>
        /// <param name="reader">Объект-инициализатор</param>
        public virtual void Serialize(SqlDataReader reader)
        {
            Squad = (SQUAD) Enum.Parse(typeof(SQUAD), reader["Squad"].ToString());
            Name = reader["Name"].ToString();
            Age = Convert.ToInt32(reader["Age"]);
        }
    }
}
