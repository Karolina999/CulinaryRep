namespace culinaryApp.Models
{
    public class ProductFromPlanner
    {
        public int Id { get; set; }
        public Unit Unit { get; set; }
        public Ingredient? Ingredient { get; set; }
        public Planner? Planner { get; set; }
    }
}

