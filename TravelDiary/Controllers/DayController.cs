using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Diary.Models;
using RiaLibrary.Web;

namespace Diary.Controllers
{
    public class DayController : Controller
    {
        
       [HttpPost, ValidateInput(false)]
        public ActionResult SaveDay(FormCollection collection)
        {
            var d = new DayStory {Day = collection[0], Header = collection[1]};
            d.Text = "<!--" + d.Header + "--> <br>" + collection[2];
            
            try {
                DayStory.Save(d);
                //return Redirect("Upload/" + d.Day);
                return Redirect("/Home/Upload/" + d.Day);
            }
            catch(Exception ex) {
                throw ex;
            }
        }

        
        public void Delete(string id)
        {
            try {
                var root = AppDomain.CurrentDomain.BaseDirectory + "Upload\\" + id;
                Directory.Delete(root, true);
                Directory.Delete(root);
            }
            catch(Exception ex) {
                
            }
        }


    }
}
