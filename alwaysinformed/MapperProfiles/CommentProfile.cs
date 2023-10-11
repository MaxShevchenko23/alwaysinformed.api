using AutoMapper;

namespace alwaysinformed.MapperProfiles
{
    public class CommentProfile:Profile
    {
        public CommentProfile()
        {
            CreateMap<Entities.Comment, Models.CommentDto>();
            CreateMap<Models.CommentDto, Entities.Comment>();
        }

    }
}
