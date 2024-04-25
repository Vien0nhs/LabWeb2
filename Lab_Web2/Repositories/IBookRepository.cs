using Lab_Web2.Enities;
using Lab_Web2.EntitiesDTO;
using Microsoft.AspNetCore.Mvc;

namespace Lab_Web2.Repositories
{
	public interface IBookRepository
	{
		Task<ActionResult<IEnumerable<Book>>> GetAllBooks();
		Task<ActionResult<Book?>> GetBookById(int id);
		Task UpdateBook(int id, BookUpdateDTO bookUpdateDTO);
		Task CreateBook(BookDTO_CUD bookDTOCUD);
		Task DeleteBook(int id);
	}
}
