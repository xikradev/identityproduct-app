using identityproduct_app.Domain.Models;

namespace identityproduct_app.Domain.Services.Interfaces
{
    public interface IProductService
    {
        public Task CreateProduct(Product product);
        public Task<IEnumerable<Product>> GetAllProducts();
        public Task<Product> GetProductById(int id);
        public Task DeleteProduct(Product product);
        public Task UpdateProduct(Product product);
    }
}
