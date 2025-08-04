namespace BlogTalks.API.Dtos
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int BlogPostID { get; set; }
    }
}
