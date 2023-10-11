﻿
namespace alwaysinformed.Models
{
    public class ArticleForCreatingDto
    {
        public string Content { get; set; } = null!;

        public int? CategoryId { get; set; }

        public string Image { get; set; } = null!;

        public string ShortDescription { get; set; } = null!;

        public string Title { get; set; } = null!;
        public string Url { get; set; } = null!;

    }
}