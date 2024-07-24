using TestAbsensi.Models;
using TestAbsensi.Repositories;

namespace TestAbsensi.Seeds
{
    public static class UserSeeder
    {
        public static void Initialize(AppDbContext context)
        {

            var totalUser = context.User.ToList();

            if (totalUser.Count == 0)
            {
                var user = new UserModel()
                {
                  Username="admin",
                  Password= BCrypt.Net.BCrypt.HashPassword("admin123")
                };

                context.User.Add(user);
                context.SaveChanges();
            }
        }
    }
}
