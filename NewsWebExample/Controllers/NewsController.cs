using Microsoft.AspNetCore.Mvc;
using NewsWebExample.Services;
using NewsWebExample.ViewModels;

namespace NewsWebExample.Controllers
{
    public class NewsController : Controller
    {
        private INewsService NewsService { get; }
        private ITagsService TagsService { get; }
        public NewsController(INewsService newsService,
            ITagsService tagsService)
        {
            NewsService = newsService;
            TagsService = tagsService;
        }

        public IActionResult Index(int? tagId = null)
        {
            return View(NewsService.GetIndexViewModel(tagId));
        }

        public IActionResult Details(int id)
        {
            return View(NewsService.GetViewModel(id));
        }

        public IActionResult Create()
        {
            return View(NewsService.GetCreateViewModel());
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(NewsCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Tags = TagsService.GetAll();
                return View(model);
            }

            try
            {
                NewsService.Create(model);
            }
            catch
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            return View(NewsService.GetEditViewModel(id));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(NewsEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Tags = TagsService.GetAll();
                return View(model);
            }

            try
            {
                NewsService.Edit(model);
            }
            catch
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            return View(NewsService.GetViewModel(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(NewsViewModel model)
        {
            try
            {
                NewsService.Delete(model);
            }
            catch
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
