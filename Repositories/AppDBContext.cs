using TestAbsensi.Models;
//using System.Data.Entity;
using Microsoft.EntityFrameworkCore;



namespace TestAbsensi.Repositories
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<AbsensiModel> Absensi { get; set; }
        public DbSet<KaryawanModel> Karyawan { get; set; }
        public DbSet<UserModel> User { get; set; }
    }
}
