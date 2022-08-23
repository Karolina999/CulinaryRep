namespace culinaryApp.Models
{
    public class PlannerRecipe
    {
        public int PlannerId { get; set; }
        public Planner? Planner { get; set; }
        public int RecipeId { get; set; }
        public Recipe? Recipe { get; set; }

    }
}
