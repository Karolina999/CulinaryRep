namespace culinaryApp.Models
{
    public class ProductFromPlanner
    {
        public int Id { get; set; }
        public Unit Unit { get; set; }
        public double Amount { get; set; }
        public MealTypes MealType { get; set; }
        public int IngredientId { get; set; }
        public Ingredient? Ingredient { get; set; }
        public Planner? Planner { get; set; }
    }
}

