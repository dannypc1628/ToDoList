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
    public class LoginController : Controller
    {
        private  ToDoListEntities db =new ToDoListEntities();
        // GET: Login
        public ActionResult Index()
        {
            
            return View(new UsersObj());
        }
        [HttpPost]
        public Object Index(String UserText,String Password)
        {
            var Ans = db.Users.Where(a=>a.UserText==UserText).Where( a=>a.Password==Password).FirstOrDefault();
            if (Ans != null)
            {
                ViewBag.Text = "登入成功";
                JwtToken Jwt = new JwtToken();
                string Token = Jwt.GetToken(UserText, Request.UserHostAddress);
                return new
                {
                    status = true,
                    token = Token
                };
            }
            else{
                ViewBag.Text = "帳號或密碼錯誤";
                return View();
            }
            
        }
        
        public string CheckToken()
        {
            
            return "沒有資料";
        }
        [HttpPost]
        public string CheckToken(String Token)
        {
            JwtToken Jwt = new JwtToken();
            string Ans = Jwt.CheckToken(Token);
            return Ans;
        }


        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult SignUp()
        {
            
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult SignUp(string UserText,string Name,string Password)
        {
            Users obj = new Users();
            obj.UserText = UserText;
            obj.Name = Name;
            obj.Password = Password;
            try
            {
                // TODO: Add insert logic here
                db.Users.Add(obj);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Text = UserText ;
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
