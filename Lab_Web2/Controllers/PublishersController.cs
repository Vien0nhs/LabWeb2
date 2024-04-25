using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab_Web2.Data;
using Lab_Web2.Enities;
using Lab_Web2.EntitiesDTO;
using Lab_Web2.Repositories;

namespace Lab_Web2.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PublisherController : ControllerBase
	{
		private readonly IPublisherRepository _publisherRepository;

		public PublisherController(IPublisherRepository publisherRepository)
		{
			_publisherRepository = publisherRepository;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Publisher>>> GetAllPublishers()
		{
			var publishers = await _publisherRepository.GetAllPublishers();
			return Ok(publishers);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Publisher>> GetPublisherById(int id)
		{
			var publisher = await _publisherRepository.GetPublisherById(id);
			if (publisher == null)
			{
				return NotFound();
			}

			return Ok(publisher);
		}

		[HttpPost]
		public async Task<IActionResult> AddPublisher(PublisherDTO_CUD publisherDTOCUD)
		{
			await _publisherRepository.AddPublisherAsync(publisherDTOCUD);
			return StatusCode(201);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdatePublisher(int id, PublisherDTO_CUD publisherDTOCUD)
		{
			await _publisherRepository.UpdatePublisherAsync(id, publisherDTOCUD);
			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeletePublisher(int id)
		{
			await _publisherRepository.DeletePublisherAsync(id);
			return Ok();
		}
	}
}
