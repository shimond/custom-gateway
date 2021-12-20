using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gw.Model
{
    public class TodoItem
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Completed { get; set; }
    }

}
