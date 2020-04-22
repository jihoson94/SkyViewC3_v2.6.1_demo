using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace SkyViewC3DB.Tests
{
    public class SqliteDatabaseFixture : IDisposable
    {
        public SqliteConnection connection;
        public SqliteDatabaseFixture()
        {
            connection = new SqliteConnection("Datasource=:memory:");
            connection.Open();
        }
        public void Dispose()
        {
            connection.Close();
        }
    }
}