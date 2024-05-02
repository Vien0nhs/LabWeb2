using Lab_Web2.Enities;
using Lab_Web2.EntitiesDTO;
#nullable disable
namespace Lab_Web2.Repositories
{
	public interface IAuthorRepository
	{
		Task<IEnumerable<Author>> GetAllAuthor();
		Task<Author> GetAuthorByIdAsync(int Id);
		Task AddAuthorAsync(CreateAuthorDTO createAuthorDTO);
		Task UpdateAuthorAsync(int Id, UpdateAuthorDTO updateAuthorDTO);
		Task DeleteAuthorAsync(int Id);
		Task<IEnumerable<Author>> FilterAuthorsAsync(string name = null);
		Task<IEnumerable<Author>> SortingAuthorsAsync(string Sort = "Name", bool SortDescending = false);
		Task<IEnumerable<Author>> PaginationAuthorsAsync(int page = 1, int pageSize = 10);
	}
}
