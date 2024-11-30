using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class BookService : IBookService
    {
        private ApplicationDbContext _dbContext;
        public BookService(ApplicationDbContext context)
        {
            _dbContext = context;
        }
        public Task<Book> GetBook()
        {
            List<Book> books = _dbContext.Books.ToList();
            Book book = new();
            book = books.OrderByDescending(a => a.Price*a.QuantityPublished).FirstOrDefault();
            return Task.FromResult(book);
        }

        public Task<List<Book>> GetBooks()
        {
            List<Book> books = _dbContext.Books.ToList();
            DateTime fix = new(2012, 5, 23); //"23 мая 2012" выход альбома "Carolus Rex" группы Sabaton
            books = _dbContext.Books.Where(a => a.Title.Contains("Red") == true && a.PublishDate > fix).ToList();
            return Task.FromResult(books);
        }
    }
}
