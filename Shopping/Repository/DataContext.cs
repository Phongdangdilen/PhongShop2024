
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shopping.Models;
using Shopping.Models.ViewModel;
using System.Configuration;


namespace Shopping.Repository
{
    public class DataContext : IdentityDbContext<AppUserModel>
    {
        public DataContext() 
        {

        }

        public DataContext(DbContextOptions options) : base(options)
        {
        }
		public DbSet<BrandModel> Brands { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<OderModel> Orders { get; set; }
        public DbSet<OderDetail> oderDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=ANHPHONGDZ;Initial Catalog=Shop10;Trusted_Connection=True;Trustservercertificate=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

	}
}
