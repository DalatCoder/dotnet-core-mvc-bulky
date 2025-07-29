using Bulky.DataAccess.Data;
using Bulky.Models;

namespace Bulky.DataAccess.Repository.IRepository;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly ApplicationDbContext _db;

    public ProductRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(Product product)
    {
        var productFromDb = _db.Products.FirstOrDefault(u => u.Id == product.Id);

        if (productFromDb != null)
        {
            productFromDb.Title = product.Title;
            productFromDb.ISBN = product.ISBN;
            productFromDb.Price = product.Price;
            productFromDb.Price50 = product.Price50;
            productFromDb.Price100 = product.Price100;
            productFromDb.ListPrice = product.ListPrice;
            productFromDb.Description = product.Description;
            productFromDb.CategoryId = product.CategoryId;
            productFromDb.Author = product.Author;

            if (product.ImageUrl != null)
            {
                productFromDb.ImageUrl = product.ImageUrl;
            }
        }
    }
}