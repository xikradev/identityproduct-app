using identityproduct_app.Data.Repositories.Interfaces;
using identityproduct_app.Domain.Models;
using identityproduct_app.Domain.Services.Interfaces;

namespace identityproduct_app.Domain.Services
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateProduct(Product product)
        {
            await _repository.CreateProduct(product);
        }

        public async Task DeleteProduct(Product product)
        {
            await _repository.DeleteProduct(product);
        }

        public Task<IEnumerable<Product>> GetAllProducts()
        {
            return _repository.GetAllProducts();
        }

        public Task<Product> GetProductById(int id)
        {
            return _repository.GetProductById(id);
        }

        public async Task UpdateProduct(Product product)
        {
            await _repository.UpdateProduct(product);
        }
    }
}
