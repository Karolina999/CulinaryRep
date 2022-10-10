namespace culinaryApp.Models
{
    public class Planner
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        /*public MealTypes MealType { get; set; }*/
        public User? User { get; set; }
        public ICollection<Recipe>? Recipes { get; set; }
        public ICollection<PlannerRecipe>? PlannerRecipes { get; set; }      
    }

    /*public enum MealTypes
    {
        Breakfast, IIBreakfast, Dinner, Snack, Supper
    }*/
}
