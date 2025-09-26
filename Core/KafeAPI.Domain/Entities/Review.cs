namespace KafeAPI.Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Comment { get; set; }
        public int Raiting { get; set; } //1-5
        public DateTime CreatedAt { get; set; }
    }
}
