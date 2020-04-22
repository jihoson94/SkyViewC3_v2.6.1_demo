
using Xunit;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SkyViewC3DB.Services;
using SkyViewC3DB.Contexts;
using System.Linq;
using System;

namespace SkyViewC3DB.Tests
{
    [CollectionDefinition("Database collection")]
    public class DatabaseCollection : ICollectionFixture<SqliteDatabaseFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }

    [Collection("Database collection")]
    public class UserServiceTest : IDisposable
    {
        public IMSContext _contextFixture;

        //Set Up
        public UserServiceTest(SqliteDatabaseFixture sqliteDatabaseFixture)
        {
            var connection = sqliteDatabaseFixture.connection;
            var options = new DbContextOptionsBuilder<IMSContext>()
                    .UseSqlite(connection)
                    .Options;

            _contextFixture = new IMSContext(options);
            _contextFixture.Database.EnsureCreated();
        }

        //Tear Down
        public void Dispose()
        {
            _contextFixture.Dispose();
        }


        [Fact]
        public void Add_user_to_database()
        {
            var service = new UserService(_contextFixture);
            service.AddUser("jihoson", "123", "jihoson94@rnd.re.kr");
            _contextFixture.SaveChanges();
            Assert.Equal(1, _contextFixture.Users.Count());
        }
    }
}
