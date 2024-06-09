namespace TrainEase.Models
{
    public class Trainer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
        public bool IsOnlineAvailable { get; set; }
    }
}