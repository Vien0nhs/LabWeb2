using Lab_Web2.Enities;
using Microsoft.EntityFrameworkCore;

namespace Lab_Web2.Data
{
	public class LibaryDbContext: DbContext
	{
		public LibaryDbContext(DbContextOptions<LibaryDbContext> options) : base(options) { }
		public DbSet<Author> Authors {  get; set; }
		public DbSet<Book> Books { get; set; }
		public DbSet<Book_Author> Book_Authors { get; set; }
		public DbSet<Publisher> Publisher { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Book_Author>()
				.HasKey(a => new {a.AuthorId, a.BookId});

			modelBuilder.Entity<Book_Author>()
				.HasOne(a => a.Books)
				.WithMany(a => a.Book_Authors)
				.HasForeignKey(a => a.BookId);

			modelBuilder.Entity<Book_Author>()
				.HasOne(a => a.Authors)
				.WithMany(a => a.Book_Authors)
				.HasForeignKey(a => a.AuthorId);

			modelBuilder.Entity<Book>()
				.HasOne(a => a.Publisher)
				.WithMany(a => a.Books)
				.HasForeignKey(a => a.PublisherId);

			modelBuilder.Entity<Author>(s =>
			{
				s.HasData(new Author
				{
					Id = 1,
					Name = "Vien"
				});
			});
			modelBuilder.Entity<Publisher>(s =>
			{
				s.HasData(new Publisher
				{
					PublisherId = 1,
					Name = "Kim Dong"
				});
			});
			modelBuilder.Entity<Book>(s =>
			{

				s.HasData(new Book
				{

					BookId = 1,
					Title = "SamuraiX",
					Description = "Samurai Manga",
					Genre = "Action",
					CoverURI = "/SomeUri",
					DateAdded = DateTime.Now,
					PublisherId = 1
				});
			});
			modelBuilder.Entity<Book_Author>(s =>
			{
				s.HasData(new Book_Author
				{
					BookId = 1,
					AuthorId = 1,
				});
			});
		}
	}
}
