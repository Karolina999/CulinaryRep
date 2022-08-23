namespace culinaryApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<Recipe>? Recipes { get; set; }
        public ICollection<WatchedRecipes>? WatchedRecipes { get; set; }

        /*public ICollection<Recipe>? OwnRecipes { get; set; }*/
        /*public ICollection<UserComment>? Comments { get; set; }*/
        /*public ICollection<Planner>? Planners { get; set; }*/
        /*public ICollection<ShoppingList>? ShoppingLists { get; set; }*/

    }
} 
