using System;
using System.Data.SqlClient;

using Task_1.Core;
namespace Task_1
{
    class Essential : CachedData<Animal>
    {
        /// <summary>
        /// Инициализирует сущность типа <see cref="Essential"/>
        /// </summary>
        /// <param name="table">Имя таблицы</param>
        public Essential(string table)
        {
            Table = table;
        }
        /// <summary>
        /// Имя таблицы, из которой происходит выборка данных
        /// </summary>
        public override string Table
        {
            get
            {
                return _table;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentException("Table name cannot be null or empty");
                }
                _table = value;
            }
        }

        /// <summary>
        /// Возвращает объект типа <see cref="Animal"/>
        /// </summary>
        /// <param name="reader">Объект чтения</param>
        /// <returns></returns>
        protected override Animal Serialize(SqlDataReader reader)
        {
            Animal animal = null;
            switch ((SQUAD) reader["Squad"]) {
                case SQUAD.spiders: {
                    animal = InitSpider(reader);
                    break;
                }
                case SQUAD.lepidoptera: {
                    animal = InitButterfly(reader);
                    break;
                }
                default:
                break;
            }
            animal.Id = Convert.ToInt32(reader["ID"]);
            return animal;
        }
        /// <summary>
        /// Создает и иницилизирует объект <see cref="Butterfly"/>
        /// </summary>
        /// <param name="reader">Объект чтения</param>
        /// <returns>Возвращает объект типа <see cref="Butterfly"/></returns>
        private Butterfly InitButterfly(SqlDataReader reader)
        {
            Butterfly butterfly = new Butterfly();

            butterfly.Name = reader["Name"].ToString();
            butterfly.Age = Convert.ToInt32(reader["Age"]);
            butterfly.IsDangerous = Convert.ToBoolean(reader["Is dangerous"]);
            butterfly.Squad = (SQUAD) reader["Squad"];
            butterfly.Feet = Convert.ToInt32(reader["Count of feet"]);
            butterfly.Color = reader["Color"].ToString();
            butterfly.WingsArea = Convert.ToSingle(reader["Wings area"]);

            return butterfly;
        }
        /// <summary>
        /// Создает и иницилизирует объект <see cref="Spider"/>
        /// </summary>
        /// <param name="reader">Объект чтения</param>
        /// <returns>Возвращает объект типа <see cref="Spider"/></returns>
        private Spider InitSpider(SqlDataReader reader)
        {
            Spider spider = new Spider();

            spider.Name = reader["Name"].ToString();
            spider.Age = Convert.ToInt32(reader["Age"]);
            spider.IsDangerous = Convert.ToBoolean(reader["Is dangerous"]);
            spider.Squad = (SQUAD) reader["Squad"];
            spider.Feet = Convert.ToInt32(reader["Count of feet"]);
            spider.HasPoison = Convert.ToBoolean(reader["Has poison"]);
            spider.IsRare = Convert.ToBoolean(reader["Is rare"]);

            return spider;
        }


    }
}
