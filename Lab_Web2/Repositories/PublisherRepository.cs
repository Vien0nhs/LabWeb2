using System.Collections.Generic;
using System.Threading.Tasks;
using Lab_Web2.Data;
using Lab_Web2.Enities;
using Lab_Web2.EntitiesDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab_Web2.Repositories
{
	public class PublisherRepository : IPublisherRepository
	{
		private readonly LibaryDbContext _context;

		public PublisherRepository(LibaryDbContext context)
		{
			_context = context;
		}

		public async Task<ActionResult<IEnumerable<Publisher>>> GetAllPublishers()
		{
			return await _context.Publisher.ToListAsync();
		}

		public async Task<ActionResult<Publisher?>> GetPublisherById(int id)
		{
			return await _context.Publisher.FindAsync(id);
		}

		public async Task AddPublisherAsync(PublisherDTO_CUD publisherDTOCUD)
		{
			var publisher = new Publisher
			{
				Name = publisherDTOCUD.Name,
			};

			_context.Publisher.Add(publisher);
			await _context.SaveChangesAsync();
		}

		public async Task UpdatePublisherAsync(int id, PublisherDTO_CUD publisherDTOCUD)
		{
			var publisher = await _context.Publisher.FindAsync(id);

			if (publisher == null)
			{
				return;
			}

			publisher.Name = publisherDTOCUD.Name;
			_context.Entry(publisher).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}

		public async Task DeletePublisherAsync(int id)
		{
			var publisher = await _context.Publisher.FindAsync(id);

			if (publisher == null)
			{
				return;
			}

			_context.Publisher.Remove(publisher);
			await _context.SaveChangesAsync();
		}
	}
}
