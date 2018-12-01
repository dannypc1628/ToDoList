using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoList.Models.obj
{
    public class TokenCheckObj
    {
        public bool Status { get; set; }

        public string Account { get; set; }

        public string IP { get; set; }

        public string ErrMsg { get; set; }
    }
}