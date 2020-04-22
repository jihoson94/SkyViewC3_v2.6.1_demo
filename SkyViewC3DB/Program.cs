using System;
using System.Linq;
using SkyViewC3DB.Contexts;
using SkyViewC3DB.Models;
namespace SkyViewC3DB
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new IMSContext())
            {
                var gradeType = new GradeType();
                gradeType.GradeTypeName = "Dev";

                var grade = new Grade();
                grade.GradeType = gradeType;

                var user = new User();
                user.Name = "jiho";
                user.Grade = grade;

                db.Users.Add(user);
                db.SaveChanges();

                user = db.Users.First<User>();
                Console.WriteLine(user.Name);
            }
        }

    }
}
