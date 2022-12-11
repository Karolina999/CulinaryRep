using culinaryApp.Models;

namespace culinaryApp.Dto
{
    public class GetPlannerRecipeDto
    {
        public int Id { get; set; }
        public int PlannerId { get; set; }
        public int RecipeId { get; set; }
        public MealTypes MealType { get; set; }
        public RecipeDto Recipe { get; set; }
    }
}
