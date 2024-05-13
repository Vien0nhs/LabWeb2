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
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace Lab_Web2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class AuthorsController : ControllerBase
    {
        private readonly LibaryDbContext _context;
        private readonly IAuthorRepository _iauthorRepository;
        private readonly ILogger<BooksController> _logger;

        public AuthorsController(LibaryDbContext context, IAuthorRepository iauthorrepository, ILogger<BooksController> logger)
        {
            _context = context;
            _iauthorRepository = iauthorrepository;
            _logger = logger;
        }

        [HttpGet("Paged")]
        public async Task<ActionResult<IEnumerable<Author>>> GetPagedAuthor(
            [FromQuery] int page = 1, 
            [FromQuery] int pageSize = 10) // Pagination for Author
        {
            if (page <= 0 || pageSize <= 0) return BadRequest("Số trang và kích thước trang phải từ 1 trở lên");
			var Authors = await _iauthorRepository.PaginationAuthorsAsync(page, pageSize);

			return Ok(Authors);
		}
        [HttpGet("All")]
        [Authorize(Roles = "Read")]
        public async Task<ActionResult<IEnumerable<Author>>> GetAllAuthors([FromQuery] string? name = null) // Tìm all tác giả
        {
            _logger.LogInformation("GetAll Book Action method was invoked");
            _logger.LogWarning("This is a warning log");
            _logger.LogError("This is a error log");
            var author = await _iauthorRepository.FilterAuthorsAsync(name);
            if (author == null || !author.Any()) return NotFound($"Không tìm thấy các tác giả có tên {name}.");
            _logger.LogInformation($"Finished GetAllBook request with data {JsonSerializer.Serialize(author)}"); 

            return Ok(author);
        }
        [HttpGet("Sort")]
		public async Task<ActionResult<IEnumerable<Author>>> SortingAuthors(
			[FromQuery] string? SortField = "Name",
			[FromQuery] bool SortDescending = false) // Sắp xếp theo tên hoặc Id trong repo service
		{
			var author = await _iauthorRepository.SortingAuthorsAsync(SortField, SortDescending);
            if(author == null || !author.Any()) return NotFound("Không tìm thấy các tác giả");
			return Ok(author);
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<Author>> GetAuthorById(int id)
		{
			var author = await _iauthorRepository.GetAuthorByIdAsync(id);
            if(author == null) return NotFound($"Không tìm thấy tác giả có id {id}.");
			return Ok(author);
		}

		[HttpPut("{id}")]
        [Authorize(Roles = "Read,Write")]
        public async Task<IActionResult> UpdateAuthor(int id, UpdateAuthorDTO updateAuthorDTO)
        {
            var author = await _context.Authors.FindAsync(id);
            if(author == null) return NotFound($"Không tìm thấy tác giả có id {id}.");
			if (string.IsNullOrEmpty(updateAuthorDTO.Name)) return BadRequest("Tên là bắt buộc.");
			await _iauthorRepository.UpdateAuthorAsync(id, updateAuthorDTO);
            return Ok(author);
        }

        [HttpPost]
        [Authorize(Roles = "Read,Write")]
        public async Task<ActionResult<Author>> CreateAuthor(CreateAuthorDTO CAuthorDIO)
        {
            if (string.IsNullOrEmpty(CAuthorDIO.Name)) return BadRequest("Tên là bắt buộc.");
			await _iauthorRepository.AddAuthorAsync(CAuthorDIO);
			return Ok(CAuthorDIO);
		}

		[HttpDelete("{id}")]
        [Authorize(Roles = "Read,Write")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null) return NotFound($"Không tìm thấy tác giả có id {id}");
            await _iauthorRepository.DeleteAuthorAsync(id);
            return Ok();
        }
    }
}
