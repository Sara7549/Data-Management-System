using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Customerlogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string mobileNo = MobileNumber.Text.Trim();
            string password = Password.Text.Trim();

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Milestone2DB_24"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Customer_Account WHERE mobileNo = @mobileNo AND pass = @password";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@mobileNo", mobileNo);
                command.Parameters.AddWithValue("@password", password);

                connection.Open();
                int count = (int)command.ExecuteScalar();

                if (count > 0)
                {
                    
                    Session["MobileNo"] = mobileNo;
                    Response.Redirect("Customer.aspx");  
                }
                else
                {
                    ErrorLabel.Text = "Invalid credentials.";
                }
            }
        }
    }
}