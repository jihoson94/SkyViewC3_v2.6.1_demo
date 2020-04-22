
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
        [Fact]
        public void Add_user_to_database()
        {
            var connection = new SqliteConnection("Datasource=:memory:");
            connection.Open();
            try
            {
                var options = new DbContextOptionsBuilder<IMSContext>().UseSqlite(connection).Options;
                using (var context = new IMSContext(options))
                {
                    context.Database.EnsureCreated();
                }

                using (var context = new IMSContext(options))
                {
                    var service = new UserService(context);
                    service.AddUser("JIHOSON");
                    context.SaveChanges();
                }
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
