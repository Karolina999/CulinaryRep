using culinaryApp.Models;

namespace culinaryApp.Interfaces
{
    public interface IProductFromListRepository
    {
        ICollection<ProductFromList> GetProducts();
        ProductFromList GetProduct(int id);
        bool ProductExists(int productId);
        bool CreateProductFromList(ProductFromList productFromList);
        bool UpdateProductFromList(ProductFromList productFromList);
        bool DeleteProductFromList(ProductFromList productFromList);
        bool DeleteProductsFromList(ICollection<ProductFromList> productsFromList);
        bool Save();
    }
}
