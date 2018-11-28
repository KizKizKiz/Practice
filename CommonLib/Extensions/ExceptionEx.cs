using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Extensions
{
    public static class ExceptionEx
    {
        /// <summary>
        /// Возвращает всю информацию об исключении и внутренних исключениях
        /// </summary>
        /// <param name="exc"></param>
        /// <returns></returns>
        public static string FullMessage(this Exception exc)
        {
            StringBuilder stringBuilder = new StringBuilder();
            while (exc != null) {
                stringBuilder.Append($"Type:{exc.GetType().Name}\n" +
                    $"{exc.Message}\n");
                exc = exc.InnerException;
            }
            return stringBuilder.ToString();
        }
    }
}
