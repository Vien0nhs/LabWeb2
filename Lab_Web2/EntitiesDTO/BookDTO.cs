namespace Lab_Web2.EntitiesDTO
{
	public class BookDTO
	{
		public int BookId { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public string? Genre { get; set; }
		public string? CoverURI { get; set; }
		public DateTime DateAdded { get; set; }
	}
}
