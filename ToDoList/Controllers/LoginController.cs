using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoList.Models;
using ToDoList.Models.obj;
using ToDoList.Security;
using Newtonsoft.Json;

namespace ToDoList.Controllers
{
    public class LoginController : Controller
    {
        private  ToDoListDBEntities db =new ToDoListDBEntities();
        // GET: Login
        public ActionResult Index()
        {
            
            return View(new UsersObj());
        }
        [HttpPost]
        public Object Index(String Account,String Password)
        {
            Users UserItem = db.Users.Where(a=>a.Account==Account).Where( a=>a.Password==Password).FirstOrDefault();
            if (UserItem != null)
            {
                //ViewBag.Text = "登入成功";
                JwtToken Jwt = new JwtToken();
                string Users_Id = Convert.ToString(UserItem.ID);
                string IP = Request.UserHostAddress;
                string Token = Jwt.GetToken(Users_Id, IP);
                Object Result = new { status=true,token=Token};
                string Output = JsonConvert.SerializeObject(Result);
                return Output;
                
            }
            else{
                Object Result = new { status = false, errMsg ="帳號密碼錯誤" };
                string Output = JsonConvert.SerializeObject(Result);
                return Output;
            }
            
        }
        
        //[HttpPost]
        //public TokenCheckObj CheckToken(String Token)
        //{
        //    JwtToken Jwt = new JwtToken();
        //    TokenCheckObj Ans = Jwt.CheckToken(Token, Request.UserHostAddress);
        //    return Ans;
        //}

       

        // GET: Login/Create
        public ActionResult SignUp()
        {
            
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult SignUp(string Account, string Name,string Password,string Phone,string Email)
        {
            Users obj = new Users
            {
                Account = Account,
                Name = Name,
                Password = Password,
                Email = Email
            };
            try
            {
                // TODO: Add insert logic here
                db.Users.Add(obj);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Text = Account;
                return View(obj);
            }
        }

        //public ActionResult Show()
        //{
        //    List<Users> obj = db.Users.ToList();
        //    return View(obj);
        //}

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
