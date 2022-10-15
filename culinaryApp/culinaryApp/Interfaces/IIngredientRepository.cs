using culinaryApp.Models;

namespace culinaryApp.Interfaces
{
    public interface IIngredientRepository
    {
        ICollection<Ingredient> GetIngredients();
        Ingredient GetIngredient(int id);
        Ingredient GetIngredient(string name);
        bool IngredientExists(int recipeId);
        bool CreateIngredient(Ingredient ingredient);
        bool Save();
    }
}
