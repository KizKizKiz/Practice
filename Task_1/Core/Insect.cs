using System;
using System.Data;
using System.Data.SqlClient;

namespace Task_1.Core
{
    public class Insect:Animal
    {
        private int _feet;        
        /// <summary>
        /// Получает/задает количество лап насекомого
        /// </summary>       
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
        /// <summary>
        /// Получает/задает значение, указывающее, является ли насекомое опасным
        /// </summary>
        public bool IsDangerous { get; set; }
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Insect"/> с полями, выставленными в значение по умолчанию 
        /// </summary>
        public Insect():
            this(default(int), false)
        {            
        }
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Insect"/> с полями, выставленными в значение по умолчанию 
        /// </summary>
        /// <param name="countOfFeet">Количество лап</param>
        /// <param name="dangerous">Является опасным</param>
        public Insect(int countOfFeet, bool dangerous = false)
            :base()
        {
            Feet = countOfFeet;
            IsDangerous = dangerous;
        }
        /// <summary>
        /// Представляет информацию об объекте <see cref="Insect"/>
        /// </summary>   
        public override string ToString()
        {
            return base.ToString() +
                   $"\n\tFeet:{Feet}" +
                   $"\n\tIs dangerous:{IsDangerous}";
        }
        /// <summary>
        /// Инициализирует поля объекта <see cref="Insect"/>
        /// </summary>
        /// <param name="reader">Объект-инициализатор</param>
        public override void Serialize(SqlDataReader reader)
        {
            base.Serialize(reader);
            Feet = reader.GetInt32(4);
            IsDangerous = reader.GetBoolean(5);
        }
    }
}
