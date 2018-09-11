using System;
using System.Data.SqlClient;

using Task_1.Core;
namespace Task_1
{
    class Essential : CachedData<Animal>
    {
        /// <summary>
        /// Имя таблицы, из которой происходит выборка данных
        /// </summary>
        public override string Table
        {
            get
            {
                return "Animals";                
            }
        }
        /// <summary>
        /// Возвращает объект типа <see cref="Animal"/>
        /// </summary>
        /// <param name="reader">Объект чтения</param>
        /// <returns></returns>
        protected override Animal Serialize(SqlDataReader reader, Type type)
        {
            Animal animal = null;
            switch ((SQUAD) reader["Squad"]) {
                case SQUAD.spiders: {
                    animal = base.Serialize(reader, typeof(Spider));
                    break;
                }
                case SQUAD.lepidoptera: {
                    animal = base.Serialize(reader, typeof(Butterfly));
                    break;
                }
                default:
                break;
            }
            return animal;
        }
    }
}
