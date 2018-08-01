using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SSOManagerLib.Model;

namespace SSOManagerLib.Dapper
{
    public class DapperContext
    {
        public MySqlConnection _conn;
        public DapperContext(IOptions<MysqlOption> options)
        {  

           var mysqlconnectionString = options.Value.mysqlconnectionString ?? "Server=10.22.10.101;Port=3306;Database=ucenter;Uid=boe;Pwd=boe888888;";
            var connection = new MySqlConnection(mysqlconnectionString);
            connection.Open();
            _conn=connection;
        }


    }
}