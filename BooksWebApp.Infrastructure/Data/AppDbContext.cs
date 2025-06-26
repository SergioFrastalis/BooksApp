using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksWebApp.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BooksWebApp.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books => Set<Book>();
        public DbSet<Review> Reviews => Set<Review>();
        public DbSet<ReviewVote> ReviewVotes => Set<ReviewVote>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ReviewVote>()
                .HasOne(rv => rv.Review)
                .WithMany(r => r.Votes)
                .HasForeignKey(rv => rv.ReviewId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ReviewVote>()
                .HasOne(rv => rv.User)
                .WithMany(u => u.Votes)
                .HasForeignKey(rv => rv.UserId)
                .OnDelete(DeleteBehavior.Restrict); 
        }

    }
}
