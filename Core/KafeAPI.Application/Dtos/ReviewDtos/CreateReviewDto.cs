namespace KafeAPI.Application.Dtos.ReviewDtos
{
    public class CreateReviewDto
    {
        public string UserId { get; set; }
        public string Comment { get; set; }
        public int Raiting { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
