

using Xunit;
using SkyViewC3Service.Repositories;
using Microsoft.Data.Sqlite;
using RobotStoreContextLib;
using System;
using Microsoft.EntityFrameworkCore;
using RobotStoreEntitiesLib;
using System.Threading.Tasks;

namespace SkyViewC3Service.Test.Repositories
{
    public class TestUserRepository : IDisposable
    {
        private UserRepository repository;
        private SqliteConnection connection;
        public TestUserRepository()
        {
            connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<RobotStoreContext>()
                                    .UseSqlite(connection).Options;
            var context = new RobotStoreContext(options);
            context.Database.EnsureCreated();
            repository = new UserRepository(context);
        }


        public void Dispose()
        {
            connection.Close();
        }

        [Fact]
        public async Task TestCreateAsync()
        {
            var newUser = new User();
            newUser.UserID = "Test";
            newUser.Name = "Tester";
            newUser.Email = "test@rnd.re.kr";
            newUser.Password = "123";
            var a = await repository.CreateAsync(newUser);
            Assert.True(a.UserID == newUser.UserID);

        }
    }
}