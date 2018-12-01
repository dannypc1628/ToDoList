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
        private ToDoListDBEntities db = new ToDoListDBEntities();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string GetToDoList(string Token)
        {
            JwtToken Jwt = new JwtToken();
            string IP = Request.UserHostAddress;
            TokenCheckObj TokenResult = Jwt.CheckToken(Token,IP);
                
            object Result = null;

            if (TokenResult.Status)
            {
                List<Models.ToDoList> DoListData = new List<Models.ToDoList>();
                int Users_Id = Convert.ToInt32(TokenResult.Users_Id);
                DoListData = db.ToDoList.Where(a => a.Owner_ID == Users_Id).ToList();
                Result = new
                {
                    Status = true,
                    List = DoListData
                };
            }
            else
            {
                Result = new { Status = true, ErrMsg = TokenResult.ErrMsg };
            }
            string Output = JsonConvert.SerializeObject(Result);
            
            return Output;
        }

        [HttpPost]
        public string AddToDo(string Token ,string Title)
        {
            JwtToken Jwt = new JwtToken();
            string IP = Request.UserHostAddress;
            TokenCheckObj TokenResult = Jwt.CheckToken(Token, IP);
            Models.ToDoList DoListData = new Models.ToDoList
            {
                Owner_ID = Convert.ToInt32(TokenResult.Users_Id),
                Title = Title,
                Create_Date = DateTime.Now,
                Completed = false
            };

            object Result = null;
            if (TokenResult.Status)
            {                
                db.ToDoList.Add(DoListData);
                db.SaveChanges();
                Result = new
                {
                    Status = true,
                    ID = DoListData.ID,
                    Title = Title,
                    Completed = DoListData.Completed
                };
            }
            else
            {
                Result = new { Status = false, ErrMsg = TokenResult.ErrMsg };
            }
 
            string Output = JsonConvert.SerializeObject(Result);
            return Output;
        }

        [HttpPost]
        public string UpdateToDo(string Token, int ID)
        {
            JwtToken Jwt = new JwtToken();
            string IP = Request.UserHostAddress;
            TokenCheckObj TokenResult = Jwt.CheckToken(Token, IP);
            Models.ToDoList DoListData = new Models.ToDoList();

            object Result = null;

            if (TokenResult.Status)
            {
                DoListData = db.ToDoList.Find(ID);
                if (DoListData.Completed == false)
                {
                    DoListData.Completed_Date = DateTime.Now;
                }                
                DoListData.Completed = !DoListData.Completed;
                db.SaveChanges();
                Result = new { Status = true };
            }
            else
            {
                Result = new
                {
                    Status = false,
                    ErrMsg = TokenResult.ErrMsg
                };
            }
             
            string Output = JsonConvert.SerializeObject(Result);
            return Output;
        }
    }
}