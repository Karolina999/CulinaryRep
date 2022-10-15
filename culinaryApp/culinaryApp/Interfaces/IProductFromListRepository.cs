using culinaryApp.Models;

namespace culinaryApp.Interfaces
{
    public interface IProductFromListRepository
    {
        ICollection<ProductFromList> GetProducts();
        ProductFromList GetProduct(int id);
        bool ProductExists(int productId);
        bool CreateProductFromList(ProductFromList productFromList);
        bool Save();
    }
}
