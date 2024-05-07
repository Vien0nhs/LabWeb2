using System.Collections.Generic;
using System.Threading.Tasks;
using Lab_Web2.Enities;
using Lab_Web2.EntitiesDTO;
using Microsoft.AspNetCore.Mvc;
#nullable disable
namespace Lab_Web2.Repositories
{
	public interface IPublisherRepository
	{
		Task<ActionResult<IEnumerable<Publisher>>> GetAllPublishers();
		Task<ActionResult<Publisher>> GetPublisherById(int id);
		Task AddPublisherAsync(PublisherDTO_CUD publisherDTOCUD);
		Task UpdatePublisherAsync(int id, PublisherDTO_CUD publisherDTOCUD);
		Task DeletePublisherAsync(int id);
		Task<IEnumerable<Publisher>> FilterPublishersAsync(string name = null);
		Task<IEnumerable<Publisher>> SortingPublishersAsync(string Sort = "Name", bool SortDescending = false);
		Task<IEnumerable<Publisher>> PaginationPublishersAsync(int page = 1, int pageSize = 10);
	}
}
