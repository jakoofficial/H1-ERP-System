using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1_ERP_System.UI
{
    public class Todo
    {
        public enum TodoState { Todo, Started, Done}
        public string Title { get; set; } = "";
        public int Priority { get; set; } 
        public TodoState State { get; set; }
        public Todo(string title, int priority, TodoState state)
        {
            Title = title;
            Priority = priority;

        }
    }
}
