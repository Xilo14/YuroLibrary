using System;
using Microsoft.EntityFrameworkCore;
using YuroLibrary.Domain;

namespace YuroLibrary.Core
{
    public class YuroLibraryDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<FileEntry> FileEntries { get; set; }

        public YuroLibraryDbContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=YuroLibrary;Username=postgres;Password=28]qYS7z+4mx{=JS");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Book>()
                .HasIndex(b => new { b.Author, b.Description, b.Name })
                .IsTsVectorExpressionIndex("english");

            modelBuilder
                .Entity<FileEntry>()
                .Property(e => e.FileFormat)
                .HasConversion<int>();

            modelBuilder
                .Entity<FileEntry>()
                .HasKey(e => e.FileId);

            //modelBuilder.Entity<FileEntry>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
        }
    }
}

