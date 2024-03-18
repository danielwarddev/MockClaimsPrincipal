namespace MockClaimsPrincipal;

public record Product(int Id, string Name);

public interface IProductService
{
    Task<Product[]> GetAllProducts();
    Task<Product[]> GetPublicProducts();
}

public class ProductService : IProductService
{
    public Task<Product[]> GetAllProducts()
    {
        throw new NotImplementedException();
    }

    public Task<Product[]> GetPublicProducts()
    {
        throw new NotImplementedException();
    }
}
