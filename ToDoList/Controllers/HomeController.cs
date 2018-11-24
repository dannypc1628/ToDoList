using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoList.Models;
using ToDoList.Models.obj;
using ToDoList.Security;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        private ToDoListEntities db = new ToDoListEntities();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string GetToDoList(string Token)
        {
            JwtToken Jwt = new JwtToken();
            string IP = Request.UserHostAddress;
            TokenCheckObj Ans = Jwt.CheckToken(Token,IP);
            List<DoList> DoListData = new List<DoList>();
            if (Ans.Status)
            {
                 DoListData = db.DoList.Where(a => a.Owner_ID == Ans.Account).ToList();
            }
            object result = new { Status = true, List = DoListData };
            string AAA=JsonConvert.SerializeObject(result);


            return AAA;
        }
        [HttpPost]
        public ActionResult AddToDo()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}