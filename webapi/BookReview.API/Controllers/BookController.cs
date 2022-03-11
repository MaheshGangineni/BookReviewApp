using BookReview.API.Data;
using BookReview.API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BookReview.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> PostReview([FromBody] Book book)
        {
            bool postind= await _bookRepository.AddReview(book);
            if (postind)
                return StatusCode(StatusCodes.Status201Created);
            else
                return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Book>),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Book>>> GetReviews()
        {
            IEnumerable<Book> books= await _bookRepository.GetReviews();

            return Ok(books);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Book), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Book>> GetReview(int id)
        {
            Book book = await _bookRepository.GetReview(id);
            if (book != null)
                return Ok(book);
            else
                return NotFound();
        }

        [HttpPut]
        [ProducesResponseType(typeof(Book), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateReview([FromBody] Book book)
        {
            bool postind = await _bookRepository.UpdateReview(book);
            if (postind)
            { 
                var updatedbook = await _bookRepository.GetReview(book.BookId);
                return StatusCode(StatusCodes.Status200OK,updatedbook);
            }
            else
                return StatusCode(StatusCodes.Status400BadRequest);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var book = await _bookRepository.GetReview(id);
            if(book!=null)
            {
                int postind = await _bookRepository.DeleteReview(book);
                if (postind > 0)
                {
                    return StatusCode(StatusCodes.Status200OK);
                }
                else
                    return StatusCode(StatusCodes.Status400BadRequest);
            }
            else
                return StatusCode(StatusCodes.Status400BadRequest);
        }



    }
}
