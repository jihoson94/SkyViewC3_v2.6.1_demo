using System;
using Microsoft.EntityFrameworkCore;
using SkyViewC3DB.Contexts;
using Xunit;

namespace SkyViewC3DB.Tests
{

    [Collection("Database collection")]
    public class IMSServiceTest : IDisposable
    {
        public IMSContext _contextFixture;

        //Set Up
        public IMSServiceTest(SqliteDatabaseFixture sqliteDatabaseFixture)
        {
            var connection = sqliteDatabaseFixture.connection;
            var options = new DbContextOptionsBuilder<IMSContext>()
                    .UseSqlite(connection)
                    .Options;

            _contextFixture = new IMSContext(options);
            _contextFixture.Database.EnsureCreated();
        }

        //TearDown
        public void Dispose()
        {
            _contextFixture.Dispose();
        }
    }
}