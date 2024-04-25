using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Lab_Web2.Data;
using Lab_Web2.Enities;
using Lab_Web2.EntitiesDTO;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Lab_Web2.Repositories
{
	public class AuthorRepository : IAuthorRepository
	{
		private readonly LibaryDbContext _context;

		public AuthorRepository(LibaryDbContext context)
		{
			_context = context;
		}
		public async Task<IEnumerable<Author>> GetAllAuthor()
		{
			return await _context.Authors.ToListAsync();
		}

		public async Task<Author?> GetAuthorByIdAsync(int Id)
		{
			return await _context.Authors.FindAsync(Id);
		}
		public async Task AddAuthorAsync(CreateAuthorDTO createAuthorDTO)
		{
			var author = new Author
			{
				Name = createAuthorDTO.Name,
			};
			await _context.Authors.AddAsync(author);
			await _context.SaveChangesAsync();		
		}

		public async Task DeleteAuthorAsync(int Id)
		{
			var author = await _context.Authors.FindAsync(Id);
			if (author != null)
			{
				_context.Authors.Remove(author);
				await _context.SaveChangesAsync();
			}
		}
		public async Task UpdateAuthorAsync(int Id, UpdateAuthorDTO updateAuthorDTO)
		{
			var author = await _context.Authors.FindAsync(Id);
			if(author != null)
			{
				author.Name = updateAuthorDTO.Name;
				_context.Entry(author).State = EntityState.Modified;
				await _context.SaveChangesAsync();
			}
		}
	}
}
