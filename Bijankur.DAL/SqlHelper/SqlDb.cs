using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BJK.DAL.SqlHelper
{
    public class SqlDb
    {
        public SqlDb() { }
        public static string CheckStringDBNull(object anyValue)
        {
            return (anyValue != DBNull.Value) ? Convert.ToString(anyValue) : string.Empty;
        }

        public static int CheckIntDBNull(object anyValue)
        {
            return (anyValue != DBNull.Value) ? Convert.ToInt32(anyValue) : 0;
        }

        public static DateTime? CheckDateTimeDBNull(object anyValue)
        {
            return (anyValue == DBNull.Value || anyValue == null) ? (DateTime?)null : Convert.ToDateTime(anyValue);
        }
    }
}
