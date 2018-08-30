using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Watusi.HealthChecks
{
    public abstract class DbHealthCheck<TReturn> : HealthCheck<DbHealthCheckParams<TReturn>, TReturn>
    {
        private DbHealthCheckParams<TReturn> _params;
        public override string Name => $"SQL({_params.ConnectionString} - {_params.Query})";

        public DbHealthCheck(DbHealthCheckParams<TReturn> healthCheckParams) : base(healthCheckParams)
        {
            _params = healthCheckParams;
        }
        protected override HealthCheckStatus Check(TReturn healthResult) => _params.DecideStatus(healthResult);
        protected override async Task<TReturn> Health()
        {
            using (var connection = CreateConnection(_params.ConnectionString))
            {
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = _params.Query;

                var result = (TReturn)(await command.ExecuteScalarAsync());

                return result;
            }
        }

        protected abstract DbConnection CreateConnection(string connectionString);

    }
}
