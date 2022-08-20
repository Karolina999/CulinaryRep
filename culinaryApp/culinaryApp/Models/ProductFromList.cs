namespace culinaryApp.Models
{
    public class ProductFromList
    {
        public int Id { get; set; }
        public Product? Product { get; set; }
        public ShoppingList? ShoppingList { get; set; }
    }
}
