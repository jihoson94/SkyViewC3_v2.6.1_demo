

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
        public async Task TestCreateAsync()
        {
            var newUser = new User()
            {
                UserID = "Test",
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

        [Fact]
        public async Task TestRetrieveAsync()
        {
            var tempUser = await repository.CreateAsync(new User() { UserID = "test123", Name = "Tester", Password = "123" });
            var addedUser = await repository.RetrieveAsync(tempUser.UserID);
            Assert.Equal(tempUser.UserID, addedUser.UserID);
            Assert.Equal(tempUser.Name, addedUser.Name);
            Assert.Equal(tempUser.Email, addedUser.Email);
            Assert.Equal(tempUser.Password, addedUser.Password);
            Assert.Equal(tempUser.Permissions, addedUser.Permissions);
        }

        // [Fact]
        // public async Task TestDeleteAsync()
        // {
        //     //TODO: implement
        // }

    }
}