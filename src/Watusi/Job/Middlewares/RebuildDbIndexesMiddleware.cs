/*
using Sohato.ElectronicPayment.TransactionsEtl.JobFramework.Attributes;
using System.Data.Entity;
using System.Data.SqlClient;

namespace Watusi.Middlewares
{
    [Retry]
    public class RebuildDbIndexesMiddleware : IMiddleware
    {
        private string _connectionString;
        private string[] _tablesName;
        private string[] _tablesSchema;

        public RebuildDbIndexesMiddleware(string connString, string[] tablesName, string[] tablesSchema)
        {
            _connectionString = connString;
            _tablesName = tablesName;
            _tablesSchema = tablesSchema;
        }

        public void Run(IJobContext jobContext)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                for (int i = 0; i < _tablesName.Length; i++)
                {
                    var rebuildNonClusteredIndexes = $@"
                        DECLARE @Schema sysname
                        SET @Schema = '{_tablesSchema[i]}'
                        DECLARE @Table sysname
                        SET @Table = '{_tablesName[i]}'
 
                        -- Get the non-clustered indexes
                        DECLARE @Indexes TABLE(Name sysname)
                        INSERT INTO @Indexes(Name)
                        SELECT ind.name
                        FROM sys.indexes ind
                        WHERE ind.object_id = OBJECT_ID(@SCHEMA + '.' + @TABLE)
                        AND ind.Type != 1 -- 1 is clustered
                        AND ind.is_disabled = 1
     
                        -- Rebuild the indexes
                        DECLARE @sql1 NVARCHAR(MAX)
                        SELECT @sql1 = isnull(@sql1, '') + 'ALTER INDEX ' + name + ' ON ' + @SCHEMA + '.' + @TABLE + ' REBUILD; '
                        FROM @Indexes
                        EXEC SP_EXECUTESQL @sql1
                        ";
                    command.CommandText = rebuildNonClusteredIndexes;
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }

        }
    }
}
*/