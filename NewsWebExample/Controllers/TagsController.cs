using Microsoft.AspNetCore.Mvc;
using NewsWebExample.Services;
using NewsWebExample.ViewModels;

namespace NewsWebExample.Controllers
{
    public class TagsController : Controller
    {
        private ITagsService TagsService { get; }
        public TagsController(ITagsService tagsService)
        {
            TagsService = tagsService;
        }

        public IActionResult Index()
        {
            return View(TagsService.GetAll());
        }

        public IActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(TagCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                TagsService.Create(model);
            }
            catch
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            return View(TagsService.GetEditViewModel(id));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(TagEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                TagsService.Edit(model);
            }
            catch
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            return View(TagsService.GetViewModel(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(TagViewModel model)
        {
            try
            {
                TagsService.Delete(model);
            }
            catch
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
