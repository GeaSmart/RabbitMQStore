using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models;
using ProductAPI.RabbitMQ;
using ProductAPI.Services;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService service;
        private readonly IRabbitMQProducer rabbit;

        public ProductController(IProductService service, IRabbitMQProducer rabbit)
        {
            this.service = service;
            this.rabbit = rabbit;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await service.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<Product> GetProductById(int id)
        {
            return await service.GetById(id);
        }
        [HttpPost]
        public async Task<Product> AddProduct(Product product)
        {
            var productData = await service.Add(product);
            rabbit.SendProductMessage(productData);
            return productData;
        }
        [HttpPut]
        public async Task<Product> UpdateProduct(Product product)
        {
            return await service.Update(product);
        }
        [HttpDelete("{id}")]
        public async Task<bool> DeleteProduct(int id)
        {
            return await service.Delete(id);
        }
    }
}
