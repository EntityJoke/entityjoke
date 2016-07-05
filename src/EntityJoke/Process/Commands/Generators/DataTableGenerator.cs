using EntityJoke.Connection;
using EntityJoke.Process.Commands.Generators;
using System.Collections.Generic;
using System.Data.Common;

namespace EntityJoke.Process.Commands
{
    public class DataTableGenerator : IDataTableGenerator
    {
        private readonly string commandSql;

        public DataTableGenerator(string commandSql)
        {
            this.commandSql = commandSql;
        }

        public List<Dictionary<string, object>> Generate()
        {
            var conn = DbConnectionFactory.Get();
            conn.Open();

            var dataTable = GenerateDataTable(conn);

            conn.Close();

            return dataTable;
        }

        private List<Dictionary<string, object>> GenerateDataTable(DbConnection conn)
        {
            var dataTable = new List<Dictionary<string, object>>();
            using (DbCommand cmd = DbCommandGenerator.Generate(commandSql))
            {
                cmd.Connection = conn;

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var dic = new Dictionary<string, object>();
                        for(var i = 0; i < reader.FieldCount; i++)
                            dic.Add(reader.GetName(i), reader[i]);
                        dataTable.Add(dic);
                    }
                }
            }
            return dataTable;
        }
    }
}
