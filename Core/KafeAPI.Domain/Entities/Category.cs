namespace KafeAPI.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<MenuItem> MenuItems { get; set; }

    }
}
