namespace culinaryApp.Models
{
    public class ProductFromList
    {
        public int Id { get; set; }
        public Unit Unit { get; set; }
        public double Amount { get; set; }
        public int IngredientId { get; set; }
        public Ingredient? Ingredient { get; set; }
        public ShoppingList? ShoppingList { get; set; }
    }

    public enum Unit
    {
        gram,
        kilogram,
        piece,
        pinch,
        spoon,
        glass,
        decagram,
        liter,
        bunch,
        pack,
        milliliter,
        teaspoon,
        grain,
        slice,
        jar,
        tin,
        leaf,
        handful,
        cm
    }
}
