using Meny_To_Meny_Relationship_in_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace Meny_To_Meny_Relationship_in_MVC.Data
{
    public class MenyToMenyContext : DbContext
    {
        public MenyToMenyContext(DbContextOptions<MenyToMenyContext> options)
        : base (options) { }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<PostTag> PostTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostTag>().HasKey( pt => new {pt.PostId , pt.TagId});

            modelBuilder.Entity<PostTag>()
                .HasOne(pt => pt.Post).WithMany(pt => pt.PostTags).HasForeignKey(pt => pt.PostId);

            modelBuilder.Entity<PostTag>()
                .HasOne(pt => pt.Tag).WithMany(pt => pt.PostTags).HasForeignKey(pt => pt.TagId);
        }
    }
}
