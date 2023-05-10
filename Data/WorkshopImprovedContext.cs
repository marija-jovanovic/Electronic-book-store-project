using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkshopImproved.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WorkshopImproved.Areas.Identity.Data;

namespace WorkshopImproved.Data
{
    public class WorkshopImprovedContext : IdentityDbContext<WorkshopImprovedUser>
    {
        public WorkshopImprovedContext (DbContextOptions<WorkshopImprovedContext> options)
            : base(options)
        {
        }

        public DbSet<WorkshopImproved.Models.Book> Book { get; set; } = default!;

        public DbSet<WorkshopImproved.Models.Genre>? Genre { get; set; }

        public DbSet<WorkshopImproved.Models.Author>? Author { get; set; }

        public DbSet<WorkshopImproved.Models.Review>? Review { get; set; }

        public DbSet<WorkshopImproved.Models.UserBooks>? UserBooks { get; set; }

        public DbSet<WorkshopImproved.Models.BookGenre> BookGenre { get; set; }
      /* protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<BookGenre>()
             .HasOne<Book>(p => p.Book)
             .WithMany(p => p.Genres)
             .HasForeignKey(p => p.BookId);
            //.HasPrincipalKey(p => p.Id);
            builder.Entity<BookGenre>()
            .HasOne<Genre>(p => p.Genre)
            .WithMany(p => p.Books)
            .HasForeignKey(p => p.GenreId);
            //.HasPrincipalKey(p => p.Id);
            builder.Entity<Book>()
            .HasOne<Author>(p => p.Author)
            .WithMany(p => p.Books)
            .HasForeignKey(p => p.AuthorId);
            //.HasPrincipalKey(p => p.Id);
            builder.Entity<Review>().
                HasOne<Book>(p => p.Book).WithMany(p => p.Reviews).HasForeignKey(p => p.BookId);
            builder.Entity<UserBooks>()
                .HasOne<Book>(p => p.Book).WithMany(p => p.Buyers).HasForeignKey(p => p.BookId);
        }  */
         
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        } 
    }
    }

