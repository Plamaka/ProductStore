using Meny_To_Meny_Relationship_in_MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Meny_To_Meny_Relationship_in_MVC.Data
{
    public class MenyToMenyContext : IdentityDbContext<AppUser>
    {
        public MenyToMenyContext(DbContextOptions<MenyToMenyContext> options)
        : base (options) { }

        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<PostTag> PostTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PostTag>().HasKey( pt => new {pt.PostId , pt.TagId});

            modelBuilder.Entity<PostTag>()
                .HasOne(pt => pt.Post).WithMany(pt => pt.PostTags).HasForeignKey(pt => pt.PostId);

            modelBuilder.Entity<PostTag>()
                .HasOne(pt => pt.Tag).WithMany(pt => pt.PostTags).HasForeignKey(pt => pt.TagId);

            modelBuilder.Entity<Post>()
                .HasOne(au => au.User).WithMany(au => au.Posts).HasForeignKey(au => au.UserId);
        }
    }
}
