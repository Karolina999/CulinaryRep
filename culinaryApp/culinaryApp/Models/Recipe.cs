namespace culinaryApp.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public Level Level { get; set; }
        /*public TimeSpan? Time { get; set; }*/
        public string? Time { get; set; }
        public int People { get; set; }
        public string? Photo  { get; set; }
        public RecipeType RecipeType { get; set; }
        public User? Owner { get; set; }
        public ICollection<User>? Watchers { get; set; }
        public ICollection<WatchedRecipe>? WatchedRecipes { get; set; }
        public ICollection<Planner>? Planners { get; set; }
        public ICollection<PlannerRecipe>? PlannerRecipe { get; set; }

        /*public ICollection<ProductFromRecipe>? Products { get; set; }*/
        /*public ICollection<Step>? Steps { get; set; }*/
        /*public ICollection<UserComment>? UserComments { get; set; }*/
    }

    public enum Level
    {
        Easy, Medium, Hard
    }
    public enum RecipeType
    {
        Dessert, Vegan, Drinks
    }
}
