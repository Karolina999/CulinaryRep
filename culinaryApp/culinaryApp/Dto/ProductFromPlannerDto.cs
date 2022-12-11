using culinaryApp.Models;

namespace culinaryApp.Dto
{
    public class ProductFromPlannerDto
    {
        public int Id { get; set; }
        public Unit Unit { get; set; }
        public double Amount { get; set; }
        public MealTypes MealType { get; set; }
    }
}
