using AutoMapper;

namespace alwaysinformed.MapperProfiles
{
    public class ArticleProfile:Profile
    {
        public ArticleProfile()
        {
            CreateMap<Entities.Article, Models.ArticleForCreatingDto>();
        }
    }
}
