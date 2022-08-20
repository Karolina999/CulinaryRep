namespace culinaryApp.Models
{
    public class ProductFromRecipe
    {
        public int Id { get; set; }
        public Product? Product { get; set; }
        public Recipe? Recipe { get; set; }
    }
}
