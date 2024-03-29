﻿using Microsoft.EntityFrameworkCore;
using Repository.Books.DbContext;
using Repository.Books.Domain;

namespace Repository.Books
{
    public class BookRepository : IBookRepository
    {
        private readonly BookDbContext _dbContext;

        public BookRepository(BookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _dbContext.Books.ToListAsync();
        }

        public async Task<Book?> GetBookAsync(int id)
        {
            return await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SubscribeBook(Book book)
        {
            book.Subscribe();
            await _dbContext.SaveChangesAsync();
        }

        public async Task UnsubscribeBook(Book book)
        {
            book.Unsubscribe();
            await _dbContext.SaveChangesAsync();
        }
    }
}