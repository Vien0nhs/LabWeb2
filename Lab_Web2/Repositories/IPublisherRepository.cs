using System.Collections.Generic;
using System.Threading.Tasks;
using Lab_Web2.Enities;
using Lab_Web2.EntitiesDTO;
using Microsoft.AspNetCore.Mvc;

namespace Lab_Web2.Repositories
{
	public interface IPublisherRepository
	{
		Task<ActionResult<IEnumerable<Publisher>>> GetAllPublishers();
		Task<ActionResult<Publisher?>> GetPublisherById(int id);
		Task AddPublisherAsync(PublisherDTO_CUD publisherDTOCUD);
		Task UpdatePublisherAsync(int id, PublisherDTO_CUD publisherDTOCUD);
		Task DeletePublisherAsync(int id);
	}
}
