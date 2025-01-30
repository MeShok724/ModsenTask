namespace ModsenTask.Application.DTOs
{
    public class BookResponse
    {
        public Guid Id { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid AuthorId { get; set; }
        public bool IsTaken { get; set; } = false;
        public byte[] Image { get; set; } = [];
    }
}
