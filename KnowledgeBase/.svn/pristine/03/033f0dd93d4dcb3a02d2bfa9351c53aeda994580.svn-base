using System;
using System.Collections.Generic;
using System.Text;

namespace SSOManagerLib.Dapper
{
   public  interface IDapperRepository
    {
        List<T> Query<T>(string sql, object param);
        T QuerySingle<T>(string sql, object param);
    }
}
