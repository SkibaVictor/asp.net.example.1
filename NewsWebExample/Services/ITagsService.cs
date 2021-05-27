using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NewsWebExample.Data;
using NewsWebExample.ViewModels;

namespace NewsWebExample.Services
{
    public interface ITagsService
    {
        List<TagViewModel> GetAll();
        TagViewModel GetViewModel(int id);
        TagEditViewModel GetEditViewModel(int id);
        void Edit(TagEditViewModel model);
        void Create(TagCreateViewModel model);
        void Delete(TagViewModel model);
    }

    public class TagsService : ITagsService
    {
        private ApplicationContext Context { get; }
        private IMapper Mapper { get; }
        public TagsService(ApplicationContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        public List<TagViewModel> GetAll()
        {
            var tags = Context.Tags.OrderBy(x => x.Name).ToList();
            return Mapper.Map<List<TagViewModel>>(tags);
        }

        public TagViewModel GetViewModel(int id)
        {
            var tag = GetById(id);
            return Mapper.Map<TagViewModel>(tag);
        }

        public TagEditViewModel GetEditViewModel(int id)
        {
            var tag = GetById(id);
            return Mapper.Map<TagEditViewModel>(tag);
        }

        public void Edit(TagEditViewModel model)
        {
            var tag = GetById(model.Id);
            tag.Name = model.Name;
            Context.SaveChanges();
        }

        public void Create(TagCreateViewModel model)
        {
            var tag = Mapper.Map<Tag>(model);
            Context.Tags.Add(tag);
            Context.SaveChanges();
        }

        public void Delete(TagViewModel model)
        {
            var tag = Context.Tags
                .Include(x => x.TagNews)
                .ThenInclude(x => x.News)
                .FirstOrDefault(x => x.Id == model.Id);

            if (tag == null)
            {
                throw new KeyNotFoundException();
            }

            foreach (var tagNews in tag.TagNews)
            {
                var news = Context.News
                    .Include(x => x.NewsTags)
                    .ThenInclude(x => x.Tag)
                    .First(x => x.Id == tagNews.News.Id);
                if (news.NewsTags.Count == 1)
                {
                    Context.News.Remove(news);
                }
            }

            Context.NewsToTags.RemoveRange(tag.TagNews);
            Context.Tags.Remove(tag);
            Context.SaveChanges();
        }

        private Tag GetById(int id)
        {
            var tag = Context.Tags
                .FirstOrDefault(x => x.Id == id);
            if (tag == null)
            {
                throw new KeyNotFoundException();
            }

            return tag;
        }
    }
}
