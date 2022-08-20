namespace culinaryApp.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public IngredientCategory IngredientCategory { get; set; }
    }

    public enum IngredientCategory
    {
        Alcohols,
        LooseArticles,
        MeatAndColdCuts,
        Preserves,
        FrozenFoodAndIceCream,
        Dairy,
        Spices,
        CerealAndMuesli,
        Fishes,
        SweetsAndSnacks,
        Fats,
        VegetablesAndFruits,
        Bread,
        Other
    }
}
