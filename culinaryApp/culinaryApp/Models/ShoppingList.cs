namespace culinaryApp.Models
{
    public class ShoppingList
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public User? User { get; set; }

        /*public ICollection<ProductFromList>? Products { get; set; }*/
    }
}
