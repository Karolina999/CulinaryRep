using culinaryApp.Models;

namespace culinaryApp.Interfaces
{
    public interface IProductFromRecipeRepository
    {
        ICollection<ProductFromRecipe> GetProducts();
        ProductFromRecipe GetProduct(int id);
        Ingredient GetIngredientFromProduct(int ingrednientId);
        bool ProductExists(int productId);
        bool CreateProductFromRecipe(ProductFromRecipe productFromRecipe);
        bool CreateProductsFromRecipe(ICollection<ProductFromRecipe> productsFromRecipe);
        bool UpdateProductFromRecipe(ProductFromRecipe productFromRecipe);
        bool DeleteProductFromRecipe(ProductFromRecipe productFromRecipe);
        bool DeleteProductsFromRecipe(ICollection<ProductFromRecipe> productsFromRecipe);
        bool Save();
    }
}
