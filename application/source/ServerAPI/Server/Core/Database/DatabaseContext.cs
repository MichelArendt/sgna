#nullable disable

using Microsoft.EntityFrameworkCore;
using ServerAPI.Server.Models;
using ServerAPI.Server.Models.Category;
using ServerAPI.Server.Models.Product;
using ServerAPI.Server.Models.Users;
using ServerAPI.Server.Services;

namespace ServerAPI.Server.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
            // This allows for instantiation at design time.
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options, AppSettingsService appSettingsService)
            : base(options)
        {
            //Configuration = configuration;
            appSettings = appSettingsService;
        }

        //private IConfiguration Configuration;
        private AppSettingsService appSettings;
        public DbSet<UserModel> Users { get; set; }
        public DbSet<UserTokenLoginModel> Users_Tokens_Login{ get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<ProductRelationshipsModel> Products_Relationships { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<SubcategoryOfModel> SubcategoryOf { get; set; }
        public DbSet<PropertyModel> Properties { get; set; }
        public DbSet<ImageModel> Images { get; set; }

        public void SetAppSettings(AppSettingsService appSettingsService)
        {
            appSettings = appSettingsService;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => appSettings.GetDatabaseOptions(ref optionsBuilder);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductRelationshipsModel>()
                .HasKey(x => new { x.ParentId, x.ProductId, x.ChildId });

            // Note that in both cases you have to turn the delete cascade off 
            // for at least one of the relationships and manually delete the 
            // related join entities before deleting the main entity, because 
            // self referencing relationships always introduce possible cycles 
            // or multiple cascade path issue, preventing the usage of cascade
            // delete.
            modelBuilder.Entity<ProductRelationshipsModel>()
                .HasOne(pt => pt.Parent)
                .WithMany(p => p.Parent)
                .HasForeignKey(pt => pt.ParentId);

            modelBuilder.Entity<ProductRelationshipsModel>()
                .HasOne(pt => pt.Product)
                .WithMany(p => p.Product)
                .HasForeignKey(pt => pt.ProductId);
                //.OnDelete(DeleteBehavior.Restrict); // see the note above

            modelBuilder.Entity<ProductRelationshipsModel>()
                .HasOne(pt => pt.Child)
                .WithMany(p => p.Child)
                .HasForeignKey(pt => pt.ChildId);


            modelBuilder.Entity<SubcategoryOfModel>()
                .HasKey(x => new { x.CategoryId, x.SubcategoryId });

            modelBuilder.Entity<SubcategoryOfModel>()
                .HasOne(pt => pt.Category)
                .WithMany(p => p.From)
                .HasForeignKey(pt => pt.CategoryId);
            //.OnDelete(DeleteBehavior.Restrict); // see the note above

            modelBuilder.Entity<SubcategoryOfModel>()
                .HasOne(pt => pt.Subcategory)
                .WithMany(p => p.To)
                .HasForeignKey(pt => pt.SubcategoryId);
        }
    }
}
