using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Diary.Models;

namespace TravelDiary.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            var d = new DayStory().GetAllDays();
            return View(d);
        }


        public ActionResult Details(string id)
        {
            var d = DayStory.GetSingleDay(id);
            return View(d);
        }

    }
}
