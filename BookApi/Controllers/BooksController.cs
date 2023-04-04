using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace BookApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/book")]
    public class BooksController : ControllerBase
    {
        private readonly DataContext _context;

        public BooksController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound("Book not found");
            }
            return Ok(book);
        }

        //post api.book

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Book>>> PostBook(Book book)
        {

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return Ok(await _context.Books.ToListAsync());
        }

        //put api/book/id
        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<Book>>> PutBook(int id, Book updateBook)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound("Book doesn't exist");
            }

            book.Title = updateBook.Title;
            book.Author = updateBook.Author;
            await _context.SaveChangesAsync();

            return Ok(await _context.Books.ToListAsync());
        }

        //delete api.book/id

        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<Book>>> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if(book == null)
            {
                return NotFound("Book not found");
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return Ok(await _context.Books.ToListAsync());
        }
    }
}
