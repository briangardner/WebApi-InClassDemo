using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_InClassDemo.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }

    }
}