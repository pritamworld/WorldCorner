using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class css_ChagePicture : System.Web.UI.Page
{
    DataHandler dh = new DataHandler();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session == null || Session["email"] == null || Session["name"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        if(!IsPostBack){
            updateNot();
        }
        MyProfileLinkButton.Text = Session["name"].ToString();
        LinkButton7.Text = Session["name"].ToString();
    }
    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        Session["email"] = null;
        Session["name"] = null;
        Session.Abandon();
        Session.Clear();
        Response.Redirect("Default.aspx");
    }
    protected void UploadButton_Click(object sender, EventArgs e)
    {
        if(ImageUploader.HasFile){
            string name = Session["email"].ToString()+".jpg";
            ImageUploader.PostedFile.SaveAs(Server.MapPath("~/proimg/")+name);
            ImageUploader.Visible = false;
            UploadButton.Visible = false;
            UploadedImage.ImageUrl = "~/proimg/"+ name;
            UploadedImage.Visible = true;
            ConfirmButton.Visible = true;
            ResetButton.Visible = true;
        }
    }
    protected void ConfirmButton_Click(object sender, EventArgs e)
    {
        try
        {
            dh.uploadImage(Session["email"].ToString());
        }
        catch
        {
            //Handle Exception
        }
        Response.Redirect("Profiles.aspx");
    }
    protected void ResetButton_Click(object sender, EventArgs e)
    {
        ImageUploader.Visible = true;
        UploadButton.Visible = true;
        UploadedImage.Visible = false;
        ConfirmButton.Visible = false;
        ResetButton.Visible = false;
    }
    protected void NotList_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            int read = (int)DataBinder.Eval(e.Item.DataItem, "read");
            Panel panel = (Panel)e.Item.FindControl("NotDataPanel");
            if (read == 0)
            {
                panel.CssClass = "selected";
                MainClass.notcount++;
            }
        }
    }
    protected void MsgList_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            Image img = (Image)e.Item.FindControl("UserImage");
            img.ImageUrl = "~/proimg/" + (string)DataBinder.Eval(e.Item.DataItem, "ProImg");
            int read = (int)DataBinder.Eval(e.Item.DataItem, "Read");
            Panel panel = (Panel)e.Item.FindControl("MsgDataPanel");
            Label data = (Label)e.Item.FindControl("DataLabel");
            string dt = (string)DataBinder.Eval(e.Item.DataItem, "Data");
            if (dt.Contains("<br>"))
            {
                dt = dt.Substring(0, dt.IndexOf("<br>"));
            }
            data.Text = dt.Length > 20 ? dt.Substring(0, 19) + "..." : dt;
            if (read == 0 && !(Session["email"].ToString().Equals(DataBinder.Eval(e.Item.DataItem, "Sender"))))
            {
                panel.CssClass = "selected";
                MainClass.msgcount++;
            }
        }
    }
    protected void FrndList_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            Image img = (Image)e.Item.FindControl("UserImage");
            Button del = (Button)e.Item.FindControl("DeleteFriend");
            Button accept = (Button)e.Item.FindControl("ConfirmFriend");
            del.CommandArgument = accept.CommandArgument = (string)DataBinder.Eval(e.Item.DataItem, "Email");
            img.ImageUrl = "~/proimg/" + (string)DataBinder.Eval(e.Item.DataItem, "ProImg");
        }
    }
    protected void NotList_DataBound(object sender, EventArgs e)
    {
        if (MainClass.notcount > 0)
        {
            noteId.Text = MainClass.notcount.ToString();
            MainClass.notcount = 0;
        }

    }
    protected void MsgList_DataBound(object sender, EventArgs e)
    {
        if (MainClass.msgcount > 0)
        {
            Label1.Text = MainClass.msgcount.ToString();
            MainClass.msgcount = 0;
        }
    }
    protected void FrndList_DataBound(object sender, EventArgs e)
    {
        if (FrndList.Items.Count > 0)
        {
            Label2.Text = FrndList.Items.Count.ToString();
        }
    }
    protected void ConfirmFriend_Click(object sender, CommandEventArgs e)
    {
        try
        {
            dh.approveFriend(Session["name"].ToString(), Session["email"].ToString(), e.CommandArgument.ToString());
            Response.Redirect("Profiles.aspx?email="+e.CommandArgument.ToString());
        }
        catch
        {

        }
    }
    protected void DeleteFriend_Click(object sender, CommandEventArgs e)
    {
        try
        {
            dh.removeFriend(Session["email"].ToString(), e.CommandArgument.ToString());
            updateNot();
        }
        catch
        {

        }
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        updateNot();
    }
    private void updateNot()
    {
        NotList.DataSource = dh.getNotifications(Session["email"].ToString());
        FrndList.DataSource = dh.getFriendRequests(Session["email"].ToString());
        MsgList.DataSource = dh.getRecentMessages(Session["email"].ToString());
        NotList.DataBind();
        FrndList.DataBind();
        MsgList.DataBind();
    }
    protected void Not_Click(object sender, ImageClickEventArgs e)
    {
        if (NotPanel.Visible)
        {
            MsgPanel.Visible = false;
            FrndPanel.Visible = false;
            NotPanel.Visible = false;
        }
        else
        {
            MsgPanel.Visible = false;
            FrndPanel.Visible = false;
            NotPanel.Visible = true;
            noteId.Text = "";
            dh.notificationRead(Session["email"].ToString());
        }

    }
    protected void NotMsg_Click(object sender, ImageClickEventArgs e)
    {
        if (MsgPanel.Visible)
        {
            MsgPanel.Visible = false;
            FrndPanel.Visible = false;
            NotPanel.Visible = false;
        }
        else
        {
            MsgPanel.Visible = true;
            FrndPanel.Visible = false;
            NotPanel.Visible = false;
        }
    }
    protected void NotFrnd_Click(object sender, ImageClickEventArgs e)
    {
        if (FrndPanel.Visible)
        {
            MsgPanel.Visible = false;
            FrndPanel.Visible = false;
            NotPanel.Visible = false;
        }
        else
        {
            MsgPanel.Visible = false;
            FrndPanel.Visible = true;
            NotPanel.Visible = false;
        }
    }
}