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
    public class AuthorsController : ControllerBase
    {
        private readonly LibaryDbContext _context;
        private readonly IAuthorRepository _iauthorRepository;

        public AuthorsController(LibaryDbContext context, IAuthorRepository iauthorrepository)
        {
            _context = context;
            _iauthorRepository = iauthorrepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAllAuthors()
        {
            var author = await _iauthorRepository.GetAllAuthor();
            return Ok(author);
        }

		[HttpGet("{id}")]
		public async Task<ActionResult<Author>> GetAuthorById(int id)
		{
			var author = await _iauthorRepository.GetAuthorByIdAsync(id);

			return Ok(author);
		}

		[HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, UpdateAuthorDTO updateAuthorDTO)
        {
            await _iauthorRepository.UpdateAuthorAsync(id, updateAuthorDTO);

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<Author>> CreateAuthor(CreateAuthorDTO CAuthorDIO)
        {
            await _iauthorRepository.AddAuthorAsync(CAuthorDIO);
			return Ok(CAuthorDIO);
		}

		[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            await _iauthorRepository.DeleteAuthorAsync(id);
            return Ok();
        }
    }
}
