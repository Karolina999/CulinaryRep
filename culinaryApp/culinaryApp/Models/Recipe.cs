namespace culinaryApp.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int MyProperty { get; set; }
        /*public Level Level { get; set; }*/
        public string Time { get; set; }
        public int People { get; set; }
        public string? Photo  { get; set; }
        public RecipeType RecipeType { get; set; }

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
