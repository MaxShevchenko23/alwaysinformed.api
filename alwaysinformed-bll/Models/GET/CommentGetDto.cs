namespace alwaysinformed_bll.Models.GET
{
    public class CommentGetDto
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public string UserName { get; set; }
        public int? ArticleId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
