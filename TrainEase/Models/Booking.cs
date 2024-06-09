namespace TrainEase.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; } = new Trainer(); // Valor predeterminado
        public int ClientId { get; set; }
        public Client Client { get; set; } = new Client(); // Valor predeterminado
        public DateTime BookingDate { get; set; } = DateTime.Now; // Valor predeterminado
        public bool IsOnline { get; set; }
    }
}