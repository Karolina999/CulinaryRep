namespace culinaryApp.Models
{
    public class ProductFromPlanner
    {
        public int Id { get; set; }
        public Product? Product { get; set; }
        public Planner? Planner { get; set; }
    }
}

