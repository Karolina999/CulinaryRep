using culinaryApp.Models;

namespace culinaryApp.Interfaces
{
    public interface IIngredientRepository
    {
        ICollection<Ingredient> GetIngredients();
        Ingredient GetIngredient(int id);
        Ingredient GetIngredient(string name);
        bool IngredientExists(int ingredientId);
        bool CreateIngredient(Ingredient ingredient);
        bool UpdateIngredient(Ingredient ingredient);
        bool DeleteIngredient(Ingredient ingredient);
        bool Save();
    }
}
