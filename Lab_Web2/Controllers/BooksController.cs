using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab_Web2.Data;
using Lab_Web2.Enities;
using Lab_Web2.EntitiesDTO;
using Lab_Web2.Repositories;

namespace Lab_Web2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibaryDbContext _context;
        private readonly IBookRepository _ibookRepository;

        public BooksController(LibaryDbContext context, IBookRepository ibookRepository)
        {
            _context = context;
            _ibookRepository = ibookRepository;
        }

        [HttpGet]
		public async Task<ActionResult<IEnumerable<BookDTO>>> GetAllBooks()
		{
			var book = await _ibookRepository.GetAllBooks();
			return Ok(book);
		}

		[HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetBookById(int id)
        {
            var book = await _ibookRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPut("{id}")]
		public async Task<IActionResult> UpdateBook(int id, BookUpdateDTO bookUpdateDTO)
		{
            await _ibookRepository.UpdateBook(id, bookUpdateDTO);
			return Ok();
		}

		[HttpPost]
        public async Task<ActionResult<Book>> CreateBook(BookDTO_CUD bookDTOCUD)
        {
            await _ibookRepository.CreateBook(bookDTOCUD);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _ibookRepository.DeleteBook(id);
            return Ok();
        }
    }
}
