using System;
using System.Data;
using System.Data.SqlClient;


namespace Task_1.Core
{
    public class Spider:Insect
    {
        private bool _hasPoison;
        /// <summary>
        /// Получает/задает значение - имеет ли яд паук
        /// </summary>
        public bool HasPoison
        {
            get { return _hasPoison; }
            set
            {
                IsDangerous = value ? true : false;
                _hasPoison = value;
            }
        }
        /// <summary>
        /// Получает/задает значение - является ли паук редким 
        /// </summary>
        public bool IsRare { get; set; }
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Spider"/> с полями, выставленными в значение по умолчанию 
        /// </summary>
        public Spider()
            :this(false, false)
        {
            
        }
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Spider"/> с полями, выставленными в значение по умолчанию 
        /// </summary>
        /// <param name="hasPoison">Имеет яд</param>
        /// <param name="isRare">Является редким</param>
        public Spider(bool hasPoison, bool isRare)
            :base()
        {
            HasPoison = hasPoison;
            IsRare = isRare;
        }
        /// <summary>
        /// Представляет информацию об объекте <see cref="Spider"/>
        /// </summary>   
        public override string ToString()
        {
            return base.ToString() +
                $"\n\tHas poison:{HasPoison}" +
                $"\n\tIs rare:{IsRare}";
        }
        /// <summary>
        /// Инициализирует поля объекта <see cref="Spider"/>
        /// </summary>
        /// <param name="reader">Объект-инициализатор</param>
        public override void Serialize(SqlDataReader reader)
        {
            base.Serialize(reader);
            HasPoison = Convert.ToBoolean(reader["Has poison"]);
            IsRare = Convert.ToBoolean(reader["Is rare"]);
        }
    }
}
