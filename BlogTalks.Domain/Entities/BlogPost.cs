namespace BlogTalks.Domain.Entities
{
    public class BlogPost 
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<String> Tags { get; set; } = new List<String>();
        //Navigation
        public List<Comment> Comments { get; set; } = new List<Comment>();

        
    }
}
