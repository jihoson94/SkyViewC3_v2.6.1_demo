

using SkyViewC3Service.Repositories;
using RobotStoreContextLib;
using RobotStoreEntitiesLib;

using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

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

            var newUser = new User()
            {
                UserID = "ADMIN",
                Name = "Tester",
                Email = "test@rnd.re.kr",
                Password = "123",
                IsDelete = false
            };
            var addedUser = await repository.CreateAsync(newUser);
            Assert.NotNull(addedUser);

            var retrievedUser = await repository.RetrieveAsync(addedUser.UserID);
            Assert.NotNull(retrievedUser);
            Assert.True(newUser.Name == retrievedUser.Name);

            var notFoundUser = await repository.RetrieveAsync("djkdjfkdjfdjfkjdjk");
            Assert.Null(notFoundUser);
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
            //TODO: How to Check All Retrieve?? 
            var users = await repository.RetrieveAllAsync();
        }

        [Fact]
        public async Task TestDeleteAsync()
        {
            var newUser = new User()
            {
                UserID = "TestforTestDeleteAsync",
                Name = "Tester",
                Email = "test@rnd.re.kr",
                Password = "123",
                IsDelete = false
            };
            var addedUser = await repository.CreateAsync(newUser);
            Assert.NotNull(addedUser);
            bool? result = await repository.DeleteAsync(newUser.UserID);
            Assert.True(result);
            var notFoundUser = await repository.RetrieveAsync(newUser.UserID);
            Assert.Null(notFoundUser);

        }

        [Fact]
        public async Task TestUpdateAsync()
        {
            var originUser = new User()
            {
                UserID = "TestforTestUpdateAsync",
                Name = "Tester",
                Email = "test@rnd.re.kr",
                Password = "123",
                IsDelete = false
            };
            var originEmail = originUser.Email;
            var addedUser = await repository.CreateAsync(originUser);
            Assert.NotNull(addedUser);

            var modifiedUser = await repository.RetrieveAsync(originUser.UserID);
            modifiedUser.Email = "modifid_email@test.kr";

            var resultUser = await repository.UpdateAsync(modifiedUser.UserID, modifiedUser);

            Assert.False(resultUser.Email == originEmail);

        }

        [Fact]
        public async Task TestRetrieveUserPermissionsAsync()
        {
            var newUser = new User()
            {
                UserID = "TestforRetrieveUserPermissionAsync",
                Name = "Tester",
                Email = "test@rnd.re.kr",
                Password = "123",
                IsDelete = false
            };
            var user = await repository.CreateAsync(newUser);
            Assert.NotNull(user);

            var permissionList = new Permission[]{
                new Permission(){PermissionID="TestPermission1"},
                new Permission(){PermissionID="TestPermission2"},
                new Permission(){PermissionID="TestPermission3"},
            };
            var expectedPermissionList = new Permission[]{
                new Permission(){PermissionID="TestPermission1"},
                new Permission(){PermissionID="TestPermission2"},
                new Permission(){PermissionID="TestPermission3"},
            };

            context.Permissions.AddRange(permissionList);
            context.SaveChanges();
            context.UserPermissions.AddRange(new UserPermission[]{
                new UserPermission(){UserID = newUser.UserID, PermissionID= "TestPermission1"},
                new UserPermission(){UserID = newUser.UserID, PermissionID= "TestPermission2"},
                new UserPermission(){UserID = newUser.UserID, PermissionID= "TestPermission3"},
            });
            context.SaveChanges();
            var permissionsOfUser = await repository.RetrieveUserPermissionsAsync(newUser.UserID);

            Assert.Equal(permissionsOfUser, expectedPermissionList);
        }

        [Fact]
        public async Task TestCheckPermissionAsync()
        {
            var newUser = new User()
            {
                UserID = "TestforCheckPermissionAsync",
                Name = "Tester",
                Email = "test@rnd.re.kr",
                Password = "123",
                IsDelete = false
            };
            var user = await repository.CreateAsync(newUser);
            Assert.NotNull(user);

            var permissionList = new Permission[]{
                new Permission(){PermissionID="TestPermission1"},
                new Permission(){PermissionID="TestPermission2"},
                new Permission(){PermissionID="TestPermission3"},
            };

            context.Permissions.AddRange(permissionList);
            context.SaveChanges();
            context.UserPermissions.AddRange(new UserPermission[]{
                new UserPermission(){UserID = newUser.UserID, PermissionID= "TestPermission1"},
            });
            context.SaveChanges();
            var result = await repository.CheckPermissionAsync(newUser.UserID, new Permission() { PermissionID = "TestPermission1" });
            Assert.True(result);
            result = await repository.CheckPermissionAsync(newUser.UserID, new Permission() { PermissionID = "TestPermission2" });
            Assert.False(result);
        }

        [Fact]
        public async Task TestAddUserPermissionAsync()
        {
            var newUser = new User()
            {
                UserID = "TestforAddUserPermissionAsync",
                Name = "Tester",
                Email = "test@rnd.re.kr",
                Password = "123",
                IsDelete = false
            };
            var user = await repository.CreateAsync(newUser);
            Assert.NotNull(user);

            var newPermission = new Permission() { PermissionID = "TestPermission" };

            context.Permissions.Add(newPermission);
            context.SaveChanges();

            var hasPermission = await repository.CheckPermissionAsync(newUser.UserID, newPermission);
            Assert.False(hasPermission);

            var userPermission = await repository.AddUserPermissionAsync(newUser.UserID, newPermission);
            Assert.True(userPermission.UserID == newUser.UserID);
            Assert.True(userPermission.PermissionID == newPermission.PermissionID);
            hasPermission = await repository.CheckPermissionAsync(newUser.UserID, newPermission);
            Assert.True(hasPermission);

        }

        [Fact]
        public async Task TestRemoveUserPermissionAsync()
        {
            var newUser = new User()
            {
                UserID = "TestforRemoveUserPermissionAsync",
                Name = "Tester",
                Email = "test@rnd.re.kr",
                Password = "123",
                IsDelete = false
            };
            var user = await repository.CreateAsync(newUser);
            Assert.NotNull(user);

            var permissionList = new Permission[]{
                new Permission(){PermissionID="TestPermission1"},
                new Permission(){PermissionID="TestPermission2"},
                new Permission(){PermissionID="TestPermission3"},
            };

            var expectedPemissionList = new Permission[]{
                new Permission(){PermissionID="TestPermission2"},
                new Permission(){PermissionID="TestPermission3"},
            };


            context.Permissions.AddRange(permissionList);
            context.SaveChanges();
            context.UserPermissions.AddRange(new UserPermission[]{
                new UserPermission(){UserID = newUser.UserID, PermissionID = "TestPermission1"},
                new UserPermission(){UserID = newUser.UserID, PermissionID = "TestPermission2"},
                new UserPermission(){UserID = newUser.UserID, PermissionID = "TestPermission3"},
            });
            context.SaveChanges();

            var removePermission = new Permission() { PermissionID = "TestPermission1" };
            var result = await repository.RemoveUserPermissionAsync(newUser.UserID, removePermission);
            Assert.True(result);
            var permissionsOfUser = await repository.RetrieveUserPermissionsAsync(newUser.UserID);
            Assert.True(permissionsOfUser.Count() == 2);
            Assert.Equal(expectedPemissionList, permissionsOfUser);
        }

        [Fact]
        public async Task TestUpdateGradeAsync()
        {
            var newUser = new User()
            {
                UserID = "TestforUpdateGradeAsync",
                Name = "Tester",
                Email = "test@rnd.re.kr",
                Password = "123",
                IsDelete = false
            };
            var user = await repository.CreateAsync(newUser);
            Assert.NotNull(user);

            var newGrade = new Grade()
            {
                GradeID = "TestAdmin"
            };
            context.Grades.Add(newGrade);
            context.SaveChanges();

            Assert.Null(newUser.Grade);
            user = await repository.UpdateGradeAsync(newUser.UserID, newGrade);
            Assert.NotNull(user);
            Assert.Equal<Grade>(user.Grade, newGrade);
        }

        [Fact]
        public async Task TestAddUserHistory()
        {
            var byUser = new User()
            {
                UserID = "TestAdminforAddUserHistory",
                Name = "Tester",
                Email = "test@rnd.re.kr",
                Password = "123",
                IsDelete = false
            };
            var newUser = new User()
            {
                UserID = "TestforAddUserHistory",
                Name = "Tester",
                Email = "test@rnd.re.kr",
                Password = "123",
                IsDelete = false
            };


            byUser = await repository.CreateAsync(byUser); // Login User
            newUser = await repository.CreateAsync(newUser); // Created User
            var addedHistory = await repository.AddUserHistoryAsync(newUser, byUser);
            Assert.True(addedHistory.AddBy == byUser);
            Assert.True(addedHistory.UserID == newUser.UserID);
            Assert.True(addedHistory.Name == newUser.Name);
            Assert.True(addedHistory.Email == newUser.Email);
            Assert.False(addedHistory.IsDelete);

        }

        [Fact]
        public async Task AddUserPemissionHistory()
        {
            var byUser = new User()
            {
                UserID = "TestAdminforAddUserPemissionHistory",
                Name = "Tester",
                Email = "test@rnd.re.kr",
                Password = "123",
                IsDelete = false
            };

            byUser = await repository.CreateAsync(byUser); // Login User

            var newPermission = new Permission() { PermissionID = "TestPermission!!!" };
            context.Permissions.Add(newPermission);
            context.SaveChanges();

            var userPermission = await repository.AddUserPermissionAsync(byUser.UserID, newPermission);

            var addedHistory = await repository.AddUserPemissionHistoryAsync(userPermission, byUser, isDelete: false);
            Assert.True(addedHistory.Action == "add");
            Assert.True(addedHistory.PermissionID == userPermission.PermissionID);
            Assert.True(addedHistory.UserID == byUser.UserID);
            Assert.True(addedHistory.AddBy == byUser);
        }

    }
}