namespace culinaryApp.Dto
{
    public class GetPlannerDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public ICollection<GetPlannerRecipeDto>? PlannerRecipes { get; set; }
        public ICollection<GetProductFromPlannerDto>? Products { get; set; }
    }
}
