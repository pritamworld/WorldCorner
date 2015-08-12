using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;

public partial class Register : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session != null && Session["email"] != null && Session["name"] != null && Session["email"].ToString() != "" && Session["name"].ToString() != "")
        {
            Response.Redirect("Home.aspx");
        }
        if(!IsPostBack){
            ErrorLabel.Visible = false;
        }
    }
    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        string name = Name.Text.Trim();
        string email = Email.Text.Trim();
        string pass = Password.Text.Trim();
        string repass = RePassword.Text.Trim();
        string gen = Gender.Text;
        string country = CountryName.Text;
        string secq = SequrityQuestion.Text;
        string seca = SequrityAnswer.Text.Trim();
        if(name==""||email==""||pass==""||repass==""||gen==""||country==""||secq==""||seca=="")
        {
            ErrorLabel.Text = "All Fields are Mandatory";
            ErrorLabel.ForeColor = System.Drawing.Color.Red;
            ErrorLabel.Visible = true;
        }
        else
        {
            if (pass != null && pass.Equals(repass))
            {
                DataHandler dh = new DataHandler();
                var q = dh.validateUser(email);
                if(q.Any()){
                    ErrorLabel.Text = "Email is already in Use...";
                    ErrorLabel.ForeColor = System.Drawing.Color.Red;
                    ErrorLabel.Visible = true;
                }
                else
                {
                    string encPass = MainClass.getEncriptedString(pass);
                    dh.register(email, name, encPass, gen, secq, seca, country);
                    Session["email"] = email;
                    Session["name"] = name;
                    Response.Redirect("Home.aspx");
                }
            }
            else
            {
                ErrorLabel.Text = "Password Do Not Match";
                ErrorLabel.ForeColor = System.Drawing.Color.Red;
                ErrorLabel.Visible = true;
            }
        }
    }
}