using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session != null && Session["email"] != null && Session["name"] != null && Session["email"].ToString() != "" && Session["name"].ToString() != ""){
            Response.Redirect("Home.aspx");
        }
        if(!IsPostBack){
            ErrorLabel.Visible = false;
        }
    }
    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        string email = TextBox1.Text.Trim();
        string pass = TextBox2.Text.Trim();

        if("".Equals(email) || "".Equals(pass)){
            ErrorLabel.Text = "All Fields Are Mandatory";
            ErrorLabel.Visible = true;
        }
        else
        {
            DataHandler dh = new DataHandler();
            string encPass = MainClass.getEncriptedString(pass);
            var q = dh.validateUser(email);
            if(q.Any())
            {
                foreach(user_detail ud in q){
                    Session["email"] = email;
                    Session["pass"] = ud.pass;
                    Session["name"] = ud.name;
                }
                if(encPass.Equals(Session["pass"].ToString())){
                    string nav = Request.QueryString.Get("nav");
                    if(nav != null && !"".Equals(nav)){
                        Response.Redirect(nav);
                    }
                    Response.Redirect("Home.aspx");
                }
                else
                {
                    ErrorLabel.Text = "Wrong Password";
                    ErrorLabel.Visible = true;
                }
            }
            else
            {
                ErrorLabel.Text = "Wrong Email";
                ErrorLabel.Visible = true;
            }
        }
    }
}