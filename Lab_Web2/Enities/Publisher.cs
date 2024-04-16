using System.ComponentModel.DataAnnotations;

namespace Lab_Web2.Enities
{
	public class Publisher
	{
		[Key]
		public int PublisherId {  get; set; }
		public string? Name {  get; set; }
		public ICollection<Book>? Books { get; set;}
	}
}
