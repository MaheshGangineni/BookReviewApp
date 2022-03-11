using BookReview.API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookReview.API.Data
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _context;

        public BookRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> AddReview(Book book)
        {
            await _context.Books.AddAsync(book);
            return await _context.SaveChangesAsync()>0;

        }

        public async Task<int> DeleteReview(Book book)
        {
             _context.Books.Remove(book);
            return  await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> GetReviews()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book> GetReview(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<bool> UpdateReview(Book book)
        {
            Book ubook = await _context.Books.FindAsync(book.BookId);
            if (ubook != null)
            {
                ubook.BookName = book.BookName;
                ubook.Review = book.Review;
            }
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
