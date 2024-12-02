using CRUDApi.Data;
using CRUDApi.Models;
using CRUDApi.Repositories.interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRUDApi.Repositories
{
    public class ProductsRepository : IProducts
    {
        private CrudDbContext _context;
        public ProductsRepository(CrudDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }

        public async Task<IEnumerable<Product>> GetMyProductsAsync(int userId)
        {
            var products = await _context.Products.Where(p => p.UserId == userId).ToListAsync();
            return products;
        }
        public async Task<IEnumerable<Product>> GetProductAsync(int id)
        {
            var product = await _context.Products.Where(p => p.Id == id).ToListAsync();
            return product;
        }
        public async Task<Product> CreateProductAsync(Product product)
        {
            try
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
            } catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return product;
        }
        public async Task<Product> UpdateProductAsync(Product product)
        {
            try
            {
                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            } catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return product;
        }
        public async Task DeleteProductAsync(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product != null)
                {
                    _context.Products.Remove(product);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
