namespace alwaysinformed_bll.Models.POST
{
    public class CommentPostDto
    {
        public string Text { get; set; }
        public string UserName { get; set; }
        public int? ArticleId { get; set; }

    }
}