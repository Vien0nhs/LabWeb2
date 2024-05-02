using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Lab_Web2.Data;
using Lab_Web2.Enities;
using Lab_Web2.EntitiesDTO;
using Microsoft.AspNetCore.Http.HttpResults;
#nullable disable
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

		public async Task<Author> GetAuthorByIdAsync(int Id)
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
		public async Task<IEnumerable<Author>> FilterAuthorsAsync(string name = null)
		{
			var query = _context.Authors.AsQueryable(); //

			if (!string.IsNullOrEmpty(name)) // không có ! Sẽ trả về true cho kq name rỗng hoặc null, Thêm ! Để bắt method cũng phải trả về true nếu không rỗng không null.
			{
				query = query.Where(a => a.Name.Contains(name));
			}
			return await query.ToListAsync();
		}
		public async Task<IEnumerable<Author>> SortingAuthorsAsync(string Sort = "Name", bool SortDescending = false)
		{
			var author = _context.Authors.AsQueryable();
			switch (Sort)
			{
				case "Name":
					{
						author = SortDescending ? author.OrderByDescending(a => a.Name) : author.OrderBy(a => a.Name);
						break;
					}
				case "Id":
					{
						author = SortDescending ? author.OrderByDescending(a => a.Id) : author.OrderBy(a => a.Id);
						break;
					}
				default:
					{
						author = author.OrderBy(a => a.Name);
						break;
					}
			}
			return await author.ToListAsync();
		}
		public async Task<IEnumerable<Author>> PaginationAuthorsAsync(int page = 1, int pageSize = 10)
		{
			var query = _context.Authors.AsQueryable();
			return await query
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();
		}
	}
}
