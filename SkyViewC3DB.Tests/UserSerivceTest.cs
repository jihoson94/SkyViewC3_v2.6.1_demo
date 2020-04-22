
using Xunit;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SkyViewC3DB.Services;
using SkyViewC3DB.Contexts;
using System.Linq;

namespace SkyViewC3DB.Tests
{
    public class UserServiceTest
    {
        /// <summary>
        /// Test AddUser method of UserService
        /// </summary>
        [Fact]
        public void Add_user_to_database()
        {
            var connection = new SqliteConnection("Datasource=:memory:");
            connection.Open();
            try
            {
                // Connection 
                var options = new DbContextOptionsBuilder<IMSContext>().UseSqlite(connection).Options;
                using (var context = new IMSContext(options))
                {
                    context.Database.EnsureCreated();
                }
                // insert data
                using (var context = new IMSContext(options))
                {
                    var service = new UserService(context);
                    service.AddUser("jihoson", "123", "jihoson94@rnd.re.kr");
                    context.SaveChanges();
                }
                // validation
                using (var context = new IMSContext(options))
                {
                    Assert.Equal(1, context.Users.Count());
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
