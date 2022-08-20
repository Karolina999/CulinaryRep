namespace culinaryApp.Models
{
    public class Planner
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public User? User { get; set; }
        public ProductFromPlanner? Products { get; set; }
    }

    public enum MealTypes
    {
        Breakfast, IIBreakfast, Dinner, Snack, Supper
    }
}
