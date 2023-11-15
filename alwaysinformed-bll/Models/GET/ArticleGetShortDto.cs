namespace alwaysinformed_bll.Models.GET
{
    public class ArticleGetShortDto
    {
        public int? AuthorId { get; set; }

        public int? CategoryId { get; set; }

        public string Image { get; set; } = null!;

        public DateTime PublicationDate { get; set; }

        public string ShortDescription { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Url { get; set; } = null!;
        public int? ArticleSandboxId { get; set; }


    }
}
