using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gw.Model
{
    public class MyTodoItem
    {
        public int MyUserId { get; set; }
        public int MyId { get; set; }
        public string MyTitle { get; set; }
        public bool Finito { get; set; }
    }
}
