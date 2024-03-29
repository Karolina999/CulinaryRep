﻿using culinaryApp.Models;

namespace culinaryApp.Interfaces
{
    public interface IProductFromPlannerRepository
    {
        ICollection<ProductFromPlanner> GetProducts();
        ICollection<ProductFromPlanner> GetPlannerProducts(int plannerId);
        Ingredient GetIngredientFromProduct(int ingrednientId);
        ProductFromPlanner GetProduct(int id);
        bool ProductExists(int productId);
        bool CreateProductFromPlanner(ProductFromPlanner productFromPlanner);
        bool UpdateProductFromPlanner(ProductFromPlanner productFromPlanner);
        bool DeleteProductFromPlanner(ProductFromPlanner productFromPlanner);
        bool DeleteProductsFromPlanner(ICollection<ProductFromPlanner> productsFromPlanner);
        bool Save();
    }
}
