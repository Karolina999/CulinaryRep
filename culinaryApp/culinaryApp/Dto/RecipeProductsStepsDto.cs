using culinaryApp.Models;

namespace culinaryApp.Dto
{
    public class RecipeProductsStepsDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public Level Level { get; set; }
        public string? Time { get; set; }
        public int People { get; set; }
        public string? Photo { get; set; }
        public RecipeType RecipeType { get; set; }
        public int OwnerId { get; set; }
        public ICollection<ProductFromRecipeCreateDto>? Products { get; set; }
        public ICollection<StepDto>? Steps { get; set; }

    }
}
