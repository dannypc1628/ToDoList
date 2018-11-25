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
        public string AddToDo(string Token ,string Title)
        {
            JwtToken Jwt = new JwtToken();
            string IP = Request.UserHostAddress;
            TokenCheckObj Ans = Jwt.CheckToken(Token, IP);
            DoList DoListData = new DoList
            {
                Owner_ID = Ans.Account,
                Title = Title,
                Create_Date = DateTime.Now,
                Completed = false
            };

            if (Ans.Status)
            {
                
                db.DoList.Add(DoListData);
                db.SaveChanges();
            }
            object result = new { Status = true, ID = DoListData.ID, Title= Title, Completed = DoListData.Completed };
            string AAA = JsonConvert.SerializeObject(result);
            return AAA;
        }

        [HttpPost]
        public string UpdateToDo(string Token, int ID)
        {
            JwtToken Jwt = new JwtToken();
            string IP = Request.UserHostAddress;
            TokenCheckObj Ans = Jwt.CheckToken(Token, IP);
            DoList toDo = new DoList();

            if (Ans.Status)
            {

                toDo = db.DoList.Find(ID);
                toDo.Completed = !toDo.Completed;
                db.SaveChanges();

            }
            object result = new { Status = true };
            string AAA = JsonConvert.SerializeObject(result);
            return AAA;
        }
    }
}