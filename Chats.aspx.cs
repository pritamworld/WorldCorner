using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Chats : System.Web.UI.Page
{
    DataHandler dh = new DataHandler();
    private string email,myemil;

    protected void Page_Load(object sender, EventArgs e)
    {
        email = Request.QueryString["email"];
        if (Session == null || Session["email"] == null || Session["name"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        myemil = Session["email"].ToString();
        if (!IsPostBack)
        {
            if (email == null || "".Equals(email) || email.Equals(myemil))
                Response.Redirect("Home.aspx");
            else
            {
                var a = dh.validateUser(email);
                if (a.Any())
                {
                    Label3.Text = a.First().name;
                }
                else
                {
                    Response.Redirect("Home.aspx");
                }
            }
            updateData();
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
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        updateData();
        updateNot();
    }
    private void updateData()
    {
        ListView1.DataSource = dh.getMessages(myemil, email);
        ListView1.DataBind();
    }
    protected void ListView1_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            string senderemail = (string)DataBinder.Eval(e.Item.DataItem,"from_id");
            Panel listitem = (Panel)e.Item.FindControl("Panel1");
            ((Label)e.Item.FindControl("MsgLabel")).Text = (string)DataBinder.Eval(e.Item.DataItem, "msg");
            if(senderemail.Equals(myemil))
            {
                listitem.CssClass = "sender";
            }
            else
            {
                listitem.CssClass = "receiver";
            }
        }
    }
    protected void PostButton_Click(object sender, EventArgs e)
    {
        string data = NewPost.Text.Trim();
        if(data == null || "".Equals(data)){

        }
        else
        {
            try
            {
                data = data.Replace("\n", "<br>");
                dh.sendMessage(myemil, email, data);
                NewPost.Text = "";
                updateData();
                scroll();
            }
            catch
            {
               
            }
        }
        
    }
    private void scroll()
    {
        ScriptManager.RegisterStartupScript(
            UpdatePanel1,
            typeof(Page),
            new Guid().ToString(),
            "scrollToBottom();",
            true
            );
    }
    protected void ListView1_ItemInserted(object sender, ListViewInsertedEventArgs e)
    {
        scroll();
    }
    protected void LikeButton_Command(object sender, CommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Like":
                try
                {
                    dh.likePost(Session["name"].ToString(), Session["email"].ToString(), e.CommandArgument.ToString());
                    Response.Redirect("Profiles.aspx?email="+e.CommandArgument.ToString());
                }
                catch
                {
                    System.Console.WriteLine("Exception in Liking Post");
                }
                break;
            case "Unlike":
                try
                {
                    dh.unLikePost(Session["email"].ToString(), e.CommandArgument.ToString());
                    updateNot();
                }
                catch
                {
                    System.Console.WriteLine("Exception in Unliking Post");
                }
                break;
        }
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
            Response.Redirect("Profiles.aspx?email=" + e.CommandArgument.ToString());
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