using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ToDoList.Models.obj
{
    public class UsersObj
    {
        public int ID { get; set; }

        
        [DisplayName("帳號")]
        public string UserText { get; set; }

        [DisplayName("密碼")]
        public string Password { get; set; }

        [DisplayName("名字")]
        public string Name { get; set; }

        [DisplayName("電話")]
        public string Phone { get; set; }

        [DisplayName("信箱")]
        public string Email { get; set; }

    }
}