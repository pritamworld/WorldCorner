using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ForgotPassword : System.Web.UI.Page
{
    DataHandler dh = new DataHandler();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void SubmitEmail_Click(object sender, EventArgs e)
    {
        string email = EmailText.Text.Trim();
        if(email == null || "".Equals(email)){
            ErrorLabel1.Text = "Please Enter Email Id";
            EmailPanel.Visible = true;
            PassPanel.Visible = false;
        }
        else
        {
            try
            {
                var q = dh.validateUser(email);
                if (q.Any())
                {
                    foreach(user_detail u in q){
                        SecqLabel.Text = u.secq;
                    }
                    Session["remail"] = email;
                    EmailPanel.Visible = false;
                    PassPanel.Visible = true;
                }
                else
                {
                    ErrorLabel1.Text = "Email Id does Not Exist";
                    EmailPanel.Visible = true;
                    PassPanel.Visible = false;
                }
            }
            catch
            {
                EmailPanel.Visible = true;
                PassPanel.Visible = false;
            }
        }
    }
    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        if(Session["remail"]==null || "".Equals(Session["remail"])){
            EmailPanel.Visible = true;
            PassPanel.Visible = false;
        }
        else
        {
            string newpass = NewPassword.Text.Trim();
            string renew = ReNewPassword.Text.Trim();

            if (newpass == null || renew == null || "".Equals(newpass) || "".Equals(renew))
            {
                ErrorLabel.Text = "Password do not Match";
            }
            else
            {
                var q = dh.validateUser(Session["remail"].ToString());
                if (q.Any())
                {
                    foreach (user_detail u in q)
                    {
                        if (u.seca.Equals(SecqAnswer.Text.Trim()))
                        {
                            u.pass = MainClass.getEncriptedString(newpass);
                        }
                        else
                        {
                            ErrorLabel.Text = "Wrong Answer";
                            return;
                        }
                    }
                    dh.dc.SubmitChanges();
                    ErrorLabel.Text = "Password Changed Successfully";
                }
            }
        }
    }
}