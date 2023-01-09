namespace culinaryApp.Models
{
    public class Step
    {
        public int Id { get; set; }
        public int StepNumber { get; set; }
        public string? Description { get; set; }
        public string? Photo { get; set; }
        public Recipe? Recipe { get; set; }

    }
}
