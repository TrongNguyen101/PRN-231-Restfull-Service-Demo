using Microsoft.EntityFrameworkCore;
using RestfullServiceDemo.Model;

namespace RestfullServiceDemo.DataContext
{
    public class booksContext : DbContext
    {
        public booksContext() { }

        public booksContext(DbContextOptions<booksContext> options) : base(options) { }

        public virtual DbSet<Book> books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("BangConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("books", "dbo");
                entity.HasKey(book => book.Id);

                entity.Property(book => book.Id)
                .IsRequired(true)
                .HasColumnName("Book ID")
                .ValueGeneratedOnAdd();             

                entity.Property(book => book.Title)
                .IsRequired(true)
                .HasColumnName("Book Title");

                entity.Property(book => book.Description)
                .IsRequired(true)
                .HasColumnName("Book Description");

                entity.Property(book => book.Price)
                .IsRequired(true)
                .HasColumnName("Price");

                entity.Property(book => book.Quantity)
                .IsRequired(true)
                .HasColumnName("Quanlity");

                entity.Property(book => book.isDelete)
                .IsRequired(true)
                .HasColumnName("Delete");
            });
        }
    }
}
