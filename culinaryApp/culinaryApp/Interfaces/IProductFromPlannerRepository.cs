using culinaryApp.Models;

namespace culinaryApp.Interfaces
{
    public interface IProductFromPlannerRepository
    {
        ICollection<ProductFromPlanner> GetProducts();
        ProductFromPlanner GetProduct(int id);
        bool ProductExists(int productId);
        bool CreateProductFromPlanner(ProductFromPlanner productFromPlanner);
        bool Save();
    }
}
