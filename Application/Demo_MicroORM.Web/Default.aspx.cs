using System;
using System.Web.UI;

namespace Demo_MicroORM.Web
{
    public partial class _Default : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnDapper_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dapper.aspx");
        }

        protected void btnMassive_Click(object sender, EventArgs e)
        {
            Response.Redirect("Massive.aspx");
        }
    }
}