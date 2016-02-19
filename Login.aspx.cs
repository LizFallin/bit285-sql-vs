﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {

        SqlConnection conn = new SqlConnection(
             ConfigurationManager
             .ConnectionStrings["ConnectionString"].ConnectionString);

        conn.Open();

        string checkuser =
            "SELECT count(*) FROM UserData WHERE UserName = @username AND Password = @password";

        SqlCommand com = new SqlCommand(checkuser, conn);

        com.Parameters.AddWithValue("@username", txtLoginUser.Text);
        com.Parameters.AddWithValue("@password", txtLoginPassword.Text);

        int temp = Convert.ToInt32(com.ExecuteScalar());

        conn.Close();

        if (temp == 1)
        {
            Session["user"] = txtLoginUser.Text;
            lblPasswordStatus.Visible = true;
            lblPasswordStatus.Text = "User credentials are validated!";
            lblPasswordStatus.Visible = false;

            Response.Redirect("Grid.aspx");
        }

        else
        {
            lblPasswordStatus.Visible = true;
            lblPasswordStatus.Text = "Invalid user credentials! Try again.";
        }
    }
}