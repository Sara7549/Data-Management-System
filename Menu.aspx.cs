﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Menu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Adminredirect(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");

        }

        protected void Customerredirect(object sender, EventArgs e)
        {
            Response.Redirect("Customerlogin.aspx");
        }
    }
}