using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NewsWebExample.Data;
using NewsWebExample.ViewModels;

namespace NewsWebExample.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tag, TagViewModel>();
            CreateMap<Tag, TagEditViewModel>();
            CreateMap<TagCreateViewModel, Tag>();
            CreateMap<News, NewsViewModel>()
                .ForMember(x => x.Tags, opt => opt.MapFrom(t => t.NewsTags.Select(tt => tt.Tag)))
                .ForMember(x => x.NewsDate, opt => opt.MapFrom(t => t.CreateDateTime))
                .IncludeAllDerived();
            CreateMap<News, NewsEditViewModel>()
                .ForMember(x => x.SelectedTags, opt => opt.MapFrom(t => t.NewsTags.Select(tt => tt.Tag.Id)))
                .ForMember(x => x.Tags, opt => opt.Ignore());
            CreateMap<News, NewsShortViewModel>()
                .ForMember(x => x.Tags, opt => opt.MapFrom(t => t.NewsTags.Select(tt => tt.Tag.Name)));
            CreateMap<NewsCreateViewModel, News>()
                .ForMember(x => x.NewsTags, opt => opt.Ignore());
        }
    }
}
