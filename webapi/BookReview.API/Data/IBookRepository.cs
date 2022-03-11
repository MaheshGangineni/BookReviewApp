using BookReview.API.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookReview.API.Data
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetReviews();
        Task<Book> GetReview(int id);
        Task<bool> AddReview(Book book);
        Task<bool> UpdateReview(Book book);
        Task<int> DeleteReview(Book book);
    }
}
