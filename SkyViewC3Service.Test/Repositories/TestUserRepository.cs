

using SkyViewC3Service.Repositories;
using RobotStoreContextLib;
using RobotStoreEntitiesLib;

using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SkyViewC3Service.Test.Repositories
{
    public class TestUserRepository : IDisposable
    {
        private UserRepository repository;
        private SqliteConnection connection;
        private RobotStoreContext context;
        // Setup
        public TestUserRepository()
        {
            connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<RobotStoreContext>()
                                    .UseSqlite(connection).Options;
            context = new RobotStoreContext(options);
            context.Database.EnsureCreated();
            repository = new UserRepository(context);
        }

        // TearDown
        public void Dispose()
        {
            connection.Close();
        }

        [Fact]
        public async Task TestRetrieveAsync()
        {
            var user = await repository.RetrieveAsync("ADMIN");
            Assert.True(user.UserID == "ADMIN");
        }

        [Fact]
        public async Task TestCreateAsync()
        {
            var newUser = new User()
            {
                UserID = "TEST",
                Name = "Tester",
                Email = "test@rnd.re.kr",
                Password = "123",
                IsDelete = false
            };
            var addedUser = await repository.CreateAsync(newUser);

            Assert.True(addedUser.UserID == newUser.UserID);
            Assert.True(addedUser.Name == newUser.Name);
            Assert.True(addedUser.Email == newUser.Email);
            Assert.True(addedUser.IsDelete == false);
        }

        [Fact]
        public async Task TestRetrieveAllAsync()
        {
            var users = await repository.RetrieveAllAsync();
            var userCountinDB = await context.Users.CountAsync();
            Assert.True(users.Count() == userCountinDB);
        }



    }
}