using Lab_Web2.Enities;
using Lab_Web2.EntitiesDTO;

namespace Lab_Web2.Repositories
{
	public interface IAuthorRepository
	{
		Task<IEnumerable<Author>> GetAllAuthor();
		Task<Author?> GetAuthorByIdAsync(int Id);
		Task AddAuthorAsync(CreateAuthorDTO createAuthorDTO);
		Task UpdateAuthorAsync(int Id, UpdateAuthorDTO updateAuthorDTO);
		Task DeleteAuthorAsync(int Id);
	}
}
