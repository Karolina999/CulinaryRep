using culinaryApp.Models;

namespace culinaryApp.Dto
{
    public class ProductFromRecipeCreateDto
    {
        public int Id { get; set; }
        public Unit Unit { get; set; }
        public int Amount { get; set; }
        public int IngredientId { get; set; }
    }
}
