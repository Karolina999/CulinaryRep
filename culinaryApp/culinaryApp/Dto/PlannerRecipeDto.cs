using culinaryApp.Models;

namespace culinaryApp.Dto
{
    public class PlannerRecipeDto
    {
        public int Id { get; set; }
        public int PlannerId { get; set; }
        public int RecipeId { get; set; }
        public MealTypes MealType { get; set; }
    }
}
