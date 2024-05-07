using Humanizer.Localisation;
using Lab_Web2.Data;
using Lab_Web2.Enities;
using Lab_Web2.EntitiesDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;
#nullable disable
namespace Lab_Web2.Repositories
{
	public class BookRepository : IBookRepository
	{
		private readonly LibaryDbContext _context;
		public BookRepository(LibaryDbContext context)
		{
			_context = context;
		}
		public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
		{
			return await _context.Books.ToListAsync();
		}

		public async Task<ActionResult<Book>> GetBookById(int id)
		{
			return await _context.Books.FindAsync(id);		
		}
		public async Task CreateBook(BookDTO_CUD bookDTOCUD)
		{
			var book = new Book
			{
				Title = bookDTOCUD.Title,
				Description = bookDTOCUD.Description,
				Genre = bookDTOCUD.Genre,
				CoverURI = bookDTOCUD.CoverURI,
				DateAdded = bookDTOCUD.DateAdded,
				PublisherId = bookDTOCUD.PublisherId
			};
			await _context.Books.AddAsync(book);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateBook(int id, BookUpdateDTO bookUpdateDTO)
		{
			var book = await _context.Books.FindAsync(id);
			if(book != null)
			{
				book.Title = bookUpdateDTO.Title;
				book.Description = bookUpdateDTO.Description;
				book.Genre = bookUpdateDTO.Genre;
				book.CoverURI = bookUpdateDTO.CoverURI;
				book.DateAdded = bookUpdateDTO.DateAdded;
				_context.Entry(book).State = EntityState.Modified;
				await _context.SaveChangesAsync();
			}
		}
		public async Task DeleteBook(int id)
		{
			var book = await _context.Books.FindAsync(id);
			if (book != null)
			{
				_context.Books.Remove(book);
				await _context.SaveChangesAsync();
			}
		}
		public async Task<IEnumerable<Book>> FilterBooksAsync(string title = null)
		{
			var query = _context.Books.AsQueryable();

			if (!string.IsNullOrEmpty(title))
			{
				query = query.Where(a => a.Title.Contains(title));
			}
			return await query.ToListAsync();
		}
		public async Task<IEnumerable<Book>> SortingBooksAsync(string Sort = "Title", bool SortDescending = false)
		{
			var book = _context.Books.AsQueryable();
			switch (Sort)
			{
				case "Title":
					{
						book = SortDescending ? book.OrderByDescending(a => a.Title) : book.OrderBy(a => a.Title);
						break;
					}
				case "BookId":
					{
						book = SortDescending ? book.OrderByDescending(a => a.Title) : book.OrderBy(a => a.Title);
						break;
					}
				default:
					{
						book = book.OrderBy(a => a.Title);
						break;
					}
			}
			return await book.ToListAsync();
		}
		public async Task<IEnumerable<Book>> PaginationBooksAsync(int page = 1, int pageSize = 10)
		{
			var query = _context.Books.AsQueryable();
			return await query
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();
		}
	}
}
