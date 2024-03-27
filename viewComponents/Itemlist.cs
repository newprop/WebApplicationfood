using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.viewComponents
{
    public class Itemlist:ViewComponent
    {

        public IViewComponentResult Invoke(List<foodItem> list)
        {
            ViewBag.count = list.Count;
            ViewBag.total = list.Sum(i => i.ItemTotal);
            return View(list);
        }
    }
}
