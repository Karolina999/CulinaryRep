namespace culinaryApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public Ingredient? Ingredient { get; set; }
        
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
