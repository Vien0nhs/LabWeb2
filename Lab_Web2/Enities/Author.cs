using System.ComponentModel.DataAnnotations;

namespace Lab_Web2.Enities
{
	public class Author
	{
		[Key]
		public int Id { get; set; }
		public string? Name { get; set; }
		public ICollection< Book_Author>? Book_Authors { get; set; }
	}
}
