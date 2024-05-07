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
		[HttpGet("Paged")]
		public async Task<ActionResult<IEnumerable<Book>>> GetPagedBook(
			[FromQuery] int page = 1,
			[FromQuery] int pageSize = 10) // Pagination for Book
		{
			if (page <= 0 || pageSize <= 0) return BadRequest("Số trang và kích thước trang phải từ 1 trở lên");
			var Books = await _ibookRepository.PaginationBooksAsync(page, pageSize);

			return Ok(Books);
		}
		[HttpGet("All")]
		public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks([FromQuery] string? title = null) // Tìm all sach
		{
			var Book = await _ibookRepository.FilterBooksAsync(title);
			if (Book == null || !Book.Any()) return NotFound($"Không tìm thấy các cuon sach có tiêu đề {title}.");
			return Ok(Book);
		}
		[HttpGet("Sort")]
		public async Task<ActionResult<IEnumerable<Book>>> SortingBooks(
			[FromQuery] string? SortField = "Title",
			[FromQuery] bool SortDescending = false) // Sắp xếp theo tiêu đề hoặc Id trong repo service
		{
			var book = await _ibookRepository.SortingBooksAsync(SortField, SortDescending);
			if (book == null || !book.Any()) return NotFound("Không tìm thấy các cuon sach");
			return Ok(book);
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
