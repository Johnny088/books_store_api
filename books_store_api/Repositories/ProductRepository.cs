//using Microsoft.EntityFrameworkCore;
//using books_store_DAL;

//namespace books_store_api.Repositories
//{
//    public class ProductRepository
//    {
//        private readonly AppDbContext _context;
//        public ProductRepository(AppDbContext context)
//        {
//            _context = context;
//        }
//        public async Task<IQueryable> GetBooksAsync()
//        {
//            return _context.Books.AsNoTracking();
//        }
//    }
//}
