using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_InClassDemo.Models
{
    public class TaskList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Todo> Todos { get; set; }
    }
}