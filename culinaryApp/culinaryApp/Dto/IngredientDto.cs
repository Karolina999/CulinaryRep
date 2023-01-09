using culinaryApp.Models;

namespace culinaryApp.Dto
{
    public class IngredientDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public IngredientCategory IngredientCategory { get; set; }
    }
}
