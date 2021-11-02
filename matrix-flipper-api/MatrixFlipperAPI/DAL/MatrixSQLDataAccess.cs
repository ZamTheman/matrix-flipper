using MatrixFlipperAPI.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Dapper;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MatrixFlipperAPI.DAL
{
    public class MatrixSQLDataAccess : IMatrixDataAccess
    {
        private string connectionString = "";
        private ILogger logger;

        public MatrixSQLDataAccess(IConfiguration configuration, ILogger<MatrixSQLDataAccess> logger)
        {
            connectionString = configuration.GetConnectionString("default");
            this.logger = logger;
        }

        public async Task DeleteMatrixAsync(string name)
        {
            var sql = @"
DELETE FROM Matrices WHERE Matrices.Name = @name";

            await ExecuteCommandAsync(sql, new { name });
        }

        public async Task<IEnumerable<Matrix>> GetAllMatricesAsync()
        {
            var sql = @"
SELECT Json FROM Matrices";
            using var sqlConnection = new SqlConnection(connectionString);
            try
            {
                var result = await sqlConnection.QueryAsync<string>(sql);
                var matrices = new List<Matrix>();
                foreach (var matrix in result)
                    matrices.Add(JsonConvert.DeserializeObject<Matrix>(matrix));

                return matrices;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<Matrix> GetMatrixAsync(string name)
        {
            var sql = @"
SELECT
Json
FROM Matrices
WHERE Matrices.Name = @name";
            using var sqlConnection = new SqlConnection(connectionString);
            try
            {
                var result = await sqlConnection.QuerySingleAsync<string>(sql, new { name });
                return JsonConvert.DeserializeObject<Matrix>(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task CreateMatrixAsync(Matrix matrix)
        {
            var sql = @"
INSERT INTO Matrices
VALUES (@name, @matrix)
";

            await ExecuteCommandAsync(sql, new { matrix.Name, matrix = JsonConvert.SerializeObject(matrix) });
        }

        public async Task UpdateMatrixAsync(Matrix matrix)
        {
            var sql = @"
UPDATE Matrices
SET Json = @json
WHERE Name = @name";

            await ExecuteCommandAsync(sql, new { matrix.Name, json = JsonConvert.SerializeObject(matrix) });
        }

        private async Task ExecuteCommandAsync(string sql, object parameters)
        {
            using var sqlConnection = new SqlConnection(connectionString);
            try
            {
                var result = await sqlConnection.ExecuteAsync(sql, parameters);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }
    }
}
