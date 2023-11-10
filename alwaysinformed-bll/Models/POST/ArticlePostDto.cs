namespace alwaysinformed_bll.Models.POST
{
    public class ArticlePostDto
    {
        public int AuthorId { get; set; }
        public string Content { get; set; } = null!;

        public int? CategoryId { get; set; }

        public string Image { get; set; } = null!;

        public string ShortDescription { get; set; } = null!;

        public string Title { get; set; } = null!;

    }
}
