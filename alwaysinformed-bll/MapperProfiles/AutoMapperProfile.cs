using alwaysinformed_bll.Models.GET;
using alwaysinformed_bll.Models.POST;
using alwaysinformed_bll.Models.UPDATE;
using alwaysinformed_dal.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alwaysinformed_bll.MapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Article, ArticlePostDto>().ReverseMap();
            CreateMap<Article, ArticleGetFullDto>().ReverseMap();
            CreateMap<Article, ArticleGetShortDto>().ReverseMap();
            CreateMap<Article, ArticleUpdateDto>().ReverseMap();
            CreateMap<Author, AuthorGetDto>().ReverseMap();
            CreateMap<Category,CategoryGetDto>().ReverseMap();
            CreateMap<Category,CategoryPost>().ReverseMap();
            CreateMap<Category,CategoryUpdateDto>().ReverseMap();
            CreateMap<Comment,CommentGetDto>().ReverseMap();
            CreateMap<Favorite, FavoriteGetDto>().ReverseMap();
            CreateMap<User, UserPostDto>().ReverseMap();
            CreateMap<User, UserGetDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();
            CreateMap<ArticleSandboxStatus, ArticleSandboxStatusGetDto>().ReverseMap();
            CreateMap<UserRole, UserRoleGetDto>().ReverseMap();
            CreateMap<ArticleSandbox, ArticleSandboxGetDto>().ReverseMap();
            CreateMap<ArticleSandbox, ArticleSandboxPostDto>().ReverseMap();
            CreateMap<ArticleSandbox, ArticleSandboxUpdateDto>().ReverseMap();
            CreateMap<ArticleSandbox, ArticlePostDto>().ReverseMap();
            CreateMap<ArticleSandbox, ArticleUpdateDto>().ReverseMap();
            CreateMap<ArticleSandboxUpdateDto, ArticleUpdateDto>().ReverseMap();



        }
    }
}
