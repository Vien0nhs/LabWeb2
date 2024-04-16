using System.ComponentModel.DataAnnotations;

namespace Lab_Web2.Enities
{
	public class Book_Author
	{
		public int BookId {  get; set; }
		public Book? Books { get; set;}
		public int AuthorId {  get; set; }
		public Author? Authors { get; set; }

	}
}
