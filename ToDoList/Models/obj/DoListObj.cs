using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoList.Models.obj
{
    public class DoListObj
    {
        public int ID { get; set; }

        public string Owner_Id { get; set; }

        public string Title { get; set; }

        public string Detail { get; set; }

        public bool Completed { get; set; }

        public DateTime Create_Date { get; set; }

        public DateTime Completed_Date { get; set; }

    }
}