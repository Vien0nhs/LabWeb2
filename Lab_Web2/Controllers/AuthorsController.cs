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

namespace Lab_Web2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly LibaryDbContext _context;

        public AuthorsController(LibaryDbContext context)
        {
            _context = context;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAuthors()
        {
            var author = await _context.Authors.Select(a => new AuthorDTO
            {
                Id = a.Id,
                Name = a.Name
            }).ToListAsync();
            return Ok(author);
        }

		// GET: api/Authors/5
		[HttpGet("{id}")]
		public async Task<ActionResult<AuthorDTO>> GetAuthor(int id)
		{
			var author = await _context.Authors
				.Where(a => a.Id == id)
				.Select(a => new AuthorDTO
				{
					Id = a.Id,
					Name = a.Name
				})
				.FirstOrDefaultAsync();

			if (author == null)
			{
				return NotFound();
			}

			return Ok(author);
		}


		// PUT: api/Authors/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, UpdateAuthorDTO updateAuthorDTO)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            if (id != author.Id)
            {
                return BadRequest();
            }
			author.Name = updateAuthorDTO.Name;
			_context.Entry(author).State = EntityState.Modified;

			try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(CreateAuthorDTO CAuthorDIO)
        {
            var author = new Author
            {
                Name = CAuthorDIO.Name
            };
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
			return Ok();

		}

		// DELETE: api/Authors/5
		[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
}
