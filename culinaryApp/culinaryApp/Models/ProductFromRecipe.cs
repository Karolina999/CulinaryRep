namespace culinaryApp.Models
{
    public class ProductFromRecipe
    {
        public Unit Unit { get; set; }
        public Ingredient? Ingredient { get; set; }
        public Recipe? Recipe { get; set; }
    }
}
