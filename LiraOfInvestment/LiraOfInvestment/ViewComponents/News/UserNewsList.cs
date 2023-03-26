using BusinessLayer.Abstract;
using EntityLayer.Dto;
using Microsoft.AspNetCore.Mvc;

namespace LiraOfInvestment.ViewComponents.News
{
    public class UserNewsList:ViewComponent
    {
        INewsService _newsService;

        public UserNewsList(INewsService newsService)
        {
            _newsService = newsService;
        }
        public IViewComponentResult Invoke()
        {
            var values = _newsService.TGetList().Where(x=>x.Which_Symbols.Contains("nan")).Take(5).ToList();
            var model = new UserProfile();
            model.news = values;
            return View(model);
        }
    }
}
