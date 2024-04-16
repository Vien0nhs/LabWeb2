using System.ComponentModel.DataAnnotations;

namespace Lab_Web2.Enities
{
	public class Book
	{
		[Key]
		public int BookId { get; set; }
		public string? Title {  get; set; }
		public string? Description {  get; set; }
		public string? Genre { get; set; }
		public string? CoverURI {  get; set; }
		public DateTime DateAdded { get; set; }
		public int PublisherId { get; set; }
		public Publisher? Publisher { get; set; }
		public ICollection<Book_Author>? Book_Authors { get; set;}
	}
}
