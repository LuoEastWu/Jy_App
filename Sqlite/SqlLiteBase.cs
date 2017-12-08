using SqlSugar;
using System;
using System.Linq;

namespace Sqlite
{
    public class SqlLiteBase
    {
        public static SqlSugarClient GetInstance()
        {
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig() { ConnectionString = Config.ConnectionString, DbType = DbType.Sqlite, IsAutoCloseConnection = true });
            db.Ado.IsEnableLogEvent = true;
            db.Ado.LogEventStarting = (sql, pars) =>
            {
                Console.WriteLine(sql + "\r\n" + db.Utilities.SerializeObject(pars.ToDictionary(s => s.ParameterName, s => s.Value)));
                Console.WriteLine();
            };
            return db;
        }
    }
}
