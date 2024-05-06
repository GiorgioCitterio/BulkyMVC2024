using BulkyWeb.Helpers;
using Dapper;
using Microsoft.Extensions.Options;
using MySqlConnector;
using System.Data;

namespace BulkyWeb.Data
{
    public class AppDbContext(IOptions<DbSettings> dbSettings)
    {
        private readonly DbSettings _dbSettings = dbSettings.Value;

        public IDbConnection CreateConnection()
        {
            var connectionString = $"Server={_dbSettings.Server}; Database={_dbSettings.Database}; Uid={_dbSettings.UserId}; Pwd={_dbSettings.Password};";
            return new MySqlConnection(connectionString);
        }

        public async Task Init()
        {
            await InitDatabase();
            await InitTables();
        }
        private async Task InitDatabase()
        {
            // create database if it doesn't exist
            var connectionString = $"Server={_dbSettings.Server}; Uid={_dbSettings.UserId}; Pwd={_dbSettings.Password};";
            using var connection = new MySqlConnection(connectionString);
            var sql = $"CREATE SCHEMA IF NOT EXISTS `{_dbSettings.Database}` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;";
            await connection.ExecuteAsync(sql);
        }
        private async Task InitTables()
        {
            // create tables if they don't exist
            using var connection = CreateConnection();

            var sql = $""" 
            USE `{_dbSettings.Database}`;
            CREATE TABLE IF NOT EXISTS  `Categories` (
              `Id` int unsigned NOT NULL AUTO_INCREMENT,
              `Name` varchar(120) NOT NULL,
              `DisplayOrder` int NOT NULL,
              `CreatedDateTime` datetime NOT NULL,
              PRIMARY KEY (`Id`)
            ) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
            """;
            await connection.ExecuteAsync(sql);
        }
    }
}
