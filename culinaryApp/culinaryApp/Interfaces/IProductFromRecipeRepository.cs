﻿using culinaryApp.Models;

namespace culinaryApp.Interfaces
{
    public interface IProductFromRecipeRepository
    {
        ICollection<ProductFromRecipe> GetProducts();
        ProductFromRecipe GetProduct(int id);
        bool ProductExists(int productId);
        bool CreateProductFromRecipe(ProductFromRecipe productFromRecipe);
        bool Save();
    }
}
