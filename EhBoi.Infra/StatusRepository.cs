using EhBoi.Infra.Data;
using EhBoi.Infra.Interfaces;
using Npgsql;
using System.Text.Json;

namespace EhBoi.Infra
{
    public class StatusRepository : IStatusRepository
    {
        private readonly string _connectionString;
        public StatusRepository(string connectionString) 
        {
            _connectionString = connectionString;
        }
        public StatusDoBanco ObterStatusDoBanco()
        {
            var status = new StatusDoBanco();
            
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                SELECT
                    (SELECT version()) AS version,
                    (SELECT pg_size_pretty(pg_database_size(current_database()))) AS database_size,
                    (SELECT COUNT(*) FROM pg_stat_activity) AS active_connections,
                    (SELECT SUM(pg_total_relation_size(relid)) FROM pg_catalog.pg_statio_user_tables) AS total_table_size,
                    (SELECT setting FROM pg_settings WHERE name = 'shared_buffers') AS shared_buffers,
                    (SELECT setting FROM pg_settings WHERE name = 'work_mem') AS work_mem;";

                using (var command = new NpgsqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        status.Versao= reader.GetString(0);
                        status.TamanhoDoBanco = reader.GetString(1);
                        status.ConexoesAtivas = reader.GetInt32(2);
                        status.SharedBuffers = reader.GetString(4);
                        status.Memoria = reader.GetString(5);
                    }
                }

            }
            return status;
        } 
    }
}
