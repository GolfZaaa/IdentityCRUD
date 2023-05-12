using IdentityCRUD.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace IdentityCRUD.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS; Database=IdentityCRUDSummer2; " +
                "Trusted_Connection = True; TrustServerCertificate = True");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>()
            .HasData(
                new IdentityRole { Name = "Member", NormalizedName = "MEMBER" },
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" });

            var Products = new List<Product>();

            var Type = new List<string> { "Drip","Esresso","Capu","Red"};

            //var number = Type.Count; ไม่ได้ใช้ ใช้อันนี้ Type[rnd.Next(0, Type.Count)]

            Random rnd = new Random();

            for (int i = 1; i <= 10; i++)
            {
                Products.Add(new Product
                {
                    Id = i,
                    Name = $"Coffee {i}",
                    Description = "Test",
                    QuantityInStock = rnd.Next(1, 100),
                    Price = rnd.Next(10,100),
                    Type = Type[rnd.Next(0, Type.Count)]
                });
            }

            var ProductImage = new List<ProductImage>();

            for (int i = 1; i < 300;i++)
            {
                ProductImage.Add(new ProductImage
                {
                    Id = i,
                    Image = $"https://picsum.photos/id/{i}/200/300",
                    ProductId = rnd.Next(1, 11)
                });
            }

            builder.Entity<Product>().HasData(Products);
            builder.Entity<ProductImage>().HasData(ProductImage);
        }

        public DbSet<Product> Products { get; set; }
         public DbSet<ProductImage> ProductImages { get; set; }

    }
}
