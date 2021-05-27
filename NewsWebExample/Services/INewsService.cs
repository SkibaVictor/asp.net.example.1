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
    public interface INewsService
    {
        NewsIndexViewModel GetIndexViewModel(int? tagId);
        NewsViewModel GetViewModel(int id);
        NewsEditViewModel GetEditViewModel(int id);
        void Create(NewsCreateViewModel model);
        void Edit(NewsEditViewModel model);
        void Delete(NewsViewModel model);
        NewsCreateViewModel GetCreateViewModel();
    }

    public class NewsService : INewsService
    {
        private ApplicationContext Context { get; }
        private IMapper Mapper { get; }
        private ITagsService TagsService { get; }

        public NewsService(ApplicationContext context,
            IMapper mapper,
            ITagsService tagsService)
        {
            Context = context;
            Mapper = mapper;
            TagsService = tagsService;
        }

        public NewsIndexViewModel GetIndexViewModel(int? tagId)
        {
            var newsQuery = Context.News
                .Include(x => x.NewsTags)
                .ThenInclude(x => x.Tag)
                .AsQueryable();

            if (tagId != null)
            {
                if (!Context.Tags.Any(x => x.Id == tagId))
                {
                    throw new KeyNotFoundException();
                }
                newsQuery = newsQuery.Where(x => x.NewsTags.Any(t => t.Tag.Id == tagId));
            }

            var news = newsQuery.OrderByDescending(x => x.CreateDateTime).ToList();
            var indexViewModel = new NewsIndexViewModel
            {
                News = Mapper.Map<List<NewsShortViewModel>>(news),
                Tags = TagsService.GetAll(),
                CurrentTag = tagId
            };

            return indexViewModel;
        }

        public NewsViewModel GetViewModel(int id)
        {
            return Mapper.Map<NewsViewModel>(GetById(id));
        }

        public NewsEditViewModel GetEditViewModel(int id)
        {
            var model = Mapper.Map<NewsEditViewModel>(GetById(id));
            model.Tags = TagsService.GetAll();
            return model;
        }

        public void Create(NewsCreateViewModel model)
        {
            var selectedTags = Context.Tags
                .Where(x => model.SelectedTags.Contains(x.Id))
                .ToList();
            var news = Mapper.Map<News>(model);
            foreach (var tag in selectedTags)
            {
                var newsToTag = new NewsToTag { News = news, Tag = tag };
                Context.NewsToTags.Add(newsToTag);
                news.NewsTags.Add(newsToTag);
                tag.TagNews.Add(newsToTag);
            }
            Context.News.Add(news);
            Context.SaveChanges();
        }

        public void Edit(NewsEditViewModel model)
        {
            var news = GetById(model.Id);

            news.Name = model.Name;
            news.Content = model.Content;
            var newsTagsToRemove = news.NewsTags.Where(x => !model.SelectedTags.Contains(x.Id));
            Context.NewsToTags.RemoveRange(newsTagsToRemove);
            foreach (var tag in model.SelectedTags)
            {
                if (news.NewsTags.All(x => x.Id != tag))
                {
                    var newsTag = new NewsToTag
                    {
                        News = news,
                        Tag = Context.Tags.First(x => x.Id == tag)
                    };
                    news.NewsTags.Add(newsTag);
                    Context.NewsToTags.Add(newsTag);
                }
            }

            Context.SaveChanges();
        }

        public void Delete(NewsViewModel model)
        {
            var news = GetById(model.Id);

            Context.News.Remove(news);
            Context.NewsToTags.RemoveRange(news.NewsTags);
            Context.SaveChanges();

        }

        public NewsCreateViewModel GetCreateViewModel()
        {
            return new NewsCreateViewModel
            {
                Tags = TagsService.GetAll()
            };
        }

        private News GetById(int id)
        {
            var news = Context.News
                .Include(x => x.NewsTags)
                .ThenInclude(x => x.Tag)
                .FirstOrDefault(x => x.Id == id);

            if (news == null)
            {
                throw new KeyNotFoundException();
            }

            return news;
        }
    }
}
