using System;

namespace WebApplication1
{
    public partial class Login : System.Web.UI.Page
    {
        protected void AdminLogin(object sender, EventArgs e)
        {
            string adminID = "admin";
            string adminPassword = "admin123";

            if (txtAdminID.Text == adminID && txtAdminPassword.Text == adminPassword)
            {
                Response.Redirect("AdminComp2.aspx");
            }
            else
            {
                lblMessage.Text = "Invalid Admin ID or Password!";
            }
        }
    }
}
