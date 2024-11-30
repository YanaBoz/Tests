using Microsoft.EntityFrameworkCore;
using TestTask.Models;
using TestTask.Services.Interfaces;
using TestTask.Data;
using System.Linq;
using static System.Reflection.Metadata.BlobBuilder;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace TestTask.Services.Implementations
{
    public class AuthorService : IAuthorService
    {
        private ApplicationDbContext _dbContext;
        public AuthorService(ApplicationDbContext context)
        {
            _dbContext = context;
        }
        public Task<Author> GetAuthor()
        {
            int book = _dbContext.Books.OrderBy(b => b.AuthorId).OrderByDescending(a => a.Title.Length).First().AuthorId;
            Author author = _dbContext.Authors.Where(d => d.Id == book).First();
            return Task.FromResult(author);
        }

        public Task<List<Author>> GetAuthors()
        {
            DateTime fix = new(2015, 12, 31);
            List<Author> Auth = _dbContext.Authors.Where(s => s.Books.Where(a=> a.PublishDate > fix).Count() % 2 == 0 && s.Books.Where(a => a.PublishDate > fix).Count() != 0).ToList();
            return Task.FromResult(Auth);
        }
    }
}
