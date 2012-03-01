using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Diary.Models;

namespace TravelDiary.Controllers
{
    public class ReplyController : Controller
    {
        //
        // GET: /Reply/

        public ActionResult Index()
        {
            return View();
        }

        public object Save(string comment, string from, string day)
        {
            var r = new Reply();
            r.Create(comment, from, day);

            return Redirect("/");
        }
    }
}
