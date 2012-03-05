using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Diary.Models;

namespace Diary.Controllers
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
        
        public ActionResult Upload(string id)
        {
            var d = DayStory.GetSingleDay(id);
            return View(d);
        }
        
        public ActionResult Day()
        {
            return View();
        }
        
        [HttpPost, ValidateInput(false)]
        public ActionResult SaveDay(FormCollection collection)
        {
            var d = new DayStory {Day = collection[0], Header = collection[1]};
            d.Text = "<!--" + d.Header + "--> <br>" + collection[2];
            
            try {
                DayStory.Save(d);
                //return Redirect("Upload/" + d.Day);
                return View("Upload", d);
            }
            catch(Exception ex) {
                throw ex;
            }
            return View();
        }

        public ActionResult DeleteDay(string id)
        {
            Image.Delete(id);
            return Redirect("/Home/Upload/"+ id);
        }
    }
}
