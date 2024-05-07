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
		[HttpGet("Paged")]
		public async Task<ActionResult<IEnumerable<Publisher>>> GetPagedPublisher(
			[FromQuery] int page = 1,
			[FromQuery] int pageSize = 10)
		{
			if (page <= 0 || pageSize <= 0) return BadRequest("Số trang và kích thước trang phải từ 1 trở lên");
			var Publishers = await _publisherRepository.PaginationPublishersAsync(page, pageSize);

			return Ok(Publishers);
		}
		[HttpGet("All")]
		public async Task<ActionResult<IEnumerable<Publisher>>> GetAllPublishers([FromQuery] string? name = null) // Tìm all nxb
		{
			var publishers = await _publisherRepository.FilterPublishersAsync(name);
			if (publishers == null || !publishers.Any()) return NotFound($"Không tìm thấy các tác giả có tên {name}.");
			return Ok(publishers);
		}
		[HttpGet("Sort")]
		public async Task<ActionResult<IEnumerable<Publisher>>> SortingPublishers(
			[FromQuery] string? SortField = "Title",
			[FromQuery] bool SortDescending = false) // Sắp xếp theo tiêu đề hoặc Id trong repo service
		{
			var publisher = await _publisherRepository.SortingPublishersAsync(SortField, SortDescending);
			if (publisher == null || !publisher.Any()) return NotFound("Không tìm thấy các nxb");
			return Ok(publisher);
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
