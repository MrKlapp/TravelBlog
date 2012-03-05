using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Diary.Models;
using RiaLibrary.Web;

namespace Diary.Controllers
{
    public class DayController : Controller
    {
        
        //
        // POST: /Day/Save
        //[HttpPost, ValidateInput(false)]
        //public ActionResult Save(FormCollection collection)
        //{
        //    var d = new DayStory {Day = collection[0], Header = collection[1]};
        //    d.Text = "<!--" + d.Header + "--> <br>" + collection[2];
            
        //    try {
        //        DayStory.Save(d);
        //        //return Redirect("Home/Upload/", new { id = d.Day });
        //        return Redirect("/Home/Upload/" + d.Day);
        //    }
        //    catch(Exception ex) {
        //        throw ex;
        //    }

        //}
        
        
        public ActionResult Delete(string id)
        {
            return View();
        }

    }
}
