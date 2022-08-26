using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using ProductAPI.Models;

namespace ProductAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext context;

        public ProductService(ApplicationDbContext context)
        {
            this.context = context;
        }
        
        public async Task<IEnumerable<Product>> GetAll()
        {
            return await context.Products.ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await context.Products.Where(x => x.ProductId == id).FirstOrDefaultAsync();            
        }
        public async Task<Product> Add(Product product)
        {
            var result = context.Products.Add(product);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Product> Update(Product product)
        {
            var result = context.Products.Update(product);
            await context.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<bool> Delete(int id)
        {
            var filteredData = await context.Products.Where(x => x.ProductId == id).FirstOrDefaultAsync();
            var result = context.Remove(filteredData);
            await context.SaveChangesAsync();
            return result != null;
        }
    }
}
