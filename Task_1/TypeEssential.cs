using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Task_1.Core;

namespace Task_1
{
    class TypeEssential:DataAccess<SQUAD>
    {       
        /// <summary>
        /// Возвращает объект типа <see cref="SQUAD"/>
        /// </summary>
        /// <param name="reader">Объект чтения</param>
        /// <returns></returns>
        protected override SQUAD Serialize(SqlDataReader reader)
        {
            return (SQUAD)reader["Squad"];
        }        
    }
}
