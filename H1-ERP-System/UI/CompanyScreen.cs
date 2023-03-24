using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;
using static H1_ERP_System.UI.Todo;

namespace H1_ERP_System.UI
{
    public class CompanyScreen : Screen
    {
        public override string Title { get; set; } = "CompanyScreen";
        protected override void Draw()
        {
            Clear(this);
            ListPage<Todo> listPage = new ListPage<Todo>();

            listPage.Add(new Todo("nr1", 1, Todo.TodoState.Todo));
            listPage.Add(new Todo("nr2", 2, TodoState.Todo));
            listPage.Add(new Todo("nr3", 3, TodoState.Todo));
            listPage.AddColumn("Company name", "Title");
            listPage.AddColumn("Country", "Priority");
            listPage.AddColumn("Currency", "State");

            listPage.Draw();
        }
    }
}
