using culinaryApp.Models;

namespace culinaryApp.Dto
{
    public class GetProductFromPlannerDto
    {
        public int Id { get; set; }
        public Unit Unit { get; set; }
        public double Amount { get; set; }
        public int IngredientId { get; set; }
        public Ingredient? Ingredient { get; set; }
        public MealTypes MealType { get; set; }
    }
}
