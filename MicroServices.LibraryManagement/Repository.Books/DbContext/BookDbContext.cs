using Microsoft.EntityFrameworkCore;
using Repository.Books.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Books.DbContext
{
    public class BookDbContext : Microsoft.EntityFrameworkCore.DbContext, IDisposable
    {
        public BookDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Name = "Da Vinci Code", Author = "Dan Brown", AvailableCopies = 4, TotalCopies = 5 },
                new Book { Id = 2, Name = "The Deception Point", Author = "Dan Brown", AvailableCopies = 2, TotalCopies = 3 },
                new Book { Id = 3, Name = "And then there were none", Author = "Agatha Christi", AvailableCopies = 3, TotalCopies = 3 },
                new Book { Id = 4, Name = "Wings of Fire", Author = "Abdul Kalam", AvailableCopies = 5, TotalCopies = 5 }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
