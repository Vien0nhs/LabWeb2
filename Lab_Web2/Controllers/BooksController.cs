﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab_Web2.Data;
using Lab_Web2.Enities;
using Lab_Web2.EntitiesDTO;

namespace Lab_Web2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibaryDbContext _context;

        public BooksController(LibaryDbContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
		public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks()
		{
			var book = await _context.Books.Select(a => new BookDTO
			{
				BookId = a.BookId,
				Title = a.Title,
                Description = a.Description,
                CoverURI = a.CoverURI,
                DateAdded = a.DateAdded,
			}).ToListAsync();
			return Ok(book);
		}

		// GET: api/Books/5
		[HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetBook(int id)
        {
            var book = await _context.Books
                .Where(a => a.BookId == id)
                .Select(a => new BookDTO
                {
                    BookId = a.BookId,
                    Title = a.Title,
                    Description = a.Description,
                    CoverURI = a.CoverURI,
                    DateAdded = a.DateAdded,
                })
                .FirstOrDefaultAsync();

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.BookId)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.BookId }, book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookId == id);
        }
    }
}