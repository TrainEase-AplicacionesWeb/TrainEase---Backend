namespace TrainEase.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int TrainerId { get; set; }
        public int ClientId { get; set; }
        public string Content { get; set; } = string.Empty; // Valor predeterminado
        public int Rating { get; set; }
        public DateTime Date { get; set; } = DateTime.Now; // Valor predeterminado

        public Trainer Trainer { get; set; } = new Trainer(); // Valor predeterminado
        public Client Client { get; set; } = new Client(); // Valor predeterminado
    }
}