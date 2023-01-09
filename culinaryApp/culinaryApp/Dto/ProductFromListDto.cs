using culinaryApp.Models;

namespace culinaryApp.Dto
{
    public class ProductFromListDto
    {
        public int Id { get; set; }
        public Unit Unit { get; set; }
        public double Amount { get; set; }
        public int IngredientId { get; set; }

    }
}
