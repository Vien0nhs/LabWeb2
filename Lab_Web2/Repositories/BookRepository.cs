using Humanizer.Localisation;
using Lab_Web2.Data;
using Lab_Web2.Enities;
using Lab_Web2.EntitiesDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

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

		public async Task<ActionResult<Book?>> GetBookById(int id)
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
	}
}
