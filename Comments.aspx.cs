﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Comments : System.Web.UI.Page
{
    private DataHandler dh = new DataHandler();
    private string postId;
    protected FeedModel pst;
    protected void Page_Load(object sender, EventArgs e)
    {
        postId = Request.QueryString["pid"];
        if (Session == null || Session["email"] == null || Session["name"] == null)
        {
            if (postId == null || "".Equals(postId))
                Response.Redirect("Default.aspx");
            else
                Response.Redirect("Default.aspx?nav=Comments.aspx?pid=" + postId);
        }
        else
        {
            if (postId == null || "".Equals(postId))
            {
                Response.Redirect("Home.aspx");
            }
        }
        if (!IsPostBack)
        {
            updateData();
            updateNot();
        }
        ListView1.PagePropertiesChanged += new EventHandler(updateData);
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
    protected void ListView1_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            Image pro = (Image)e.Item.FindControl("CProImage");

            string email = (string)DataBinder.Eval(e.Item.DataItem,"ProImg");
            pro.ImageUrl = "~/proimg/" + email ;
        }
    }
    protected void PostButton_Click(object sender, EventArgs e)
    {
        string post = NewPost.Text.Trim();
        if (post == "")
        {

        }
        else
        {
            post = post.Replace("\n", "<br>");
            try
            {
                dh.comment(Session["name"].ToString(), Session["email"].ToString(), postId, post);
                NewPost.Text = "";
                updateData();
            }
            catch
            {
                System.Console.WriteLine("Exception in Sending new Post");
            }
        }
        UpdatePanel1.Update();
    }
    protected void LikeButton_Command(object sender, CommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Like":
                try
                {
                    dh.likePost(Session["name"].ToString(), Session["email"].ToString(), e.CommandArgument.ToString());
                    updateData();
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
                    updateData();
                }
                catch
                {
                    System.Console.WriteLine("Exception in Unliking Post");
                }
                break;
        }
        UpdatePanel1.Update();
    }
    private void updateData()
    {
        try
        {
            pst = dh.getPostDetails(Session["email"].ToString(), postId);
            ProImage.ImageUrl = "~/proimg/" + pst.Proimg ;
            ProfileLink.Text = pst.Name;
            ProfileLink.NavigateUrl = "Profiles.aspx?email=" + pst.Email;
            DataLabel.Text = pst.Data;
            TimeLabel.Text = pst.Date + " " + pst.Time;
            if(pst.Comments>0){
                CommentLabel.Text = pst.Comments+" Comments";
            }
            else
            {
                CommentLabel.Text = "No Comment";
            }
            if (pst.ILike)
            {
                LikeButton.Text = "Unlike";
                LikeButton.CommandName = "Unlike";
                LikeButton.CommandArgument = postId;
                pst.Likes--;
                if (pst.Likes == 0)
                    LikeLabel.Text = "You Like This";
                else
                    LikeLabel.Text = "<a href='Likes.aspx?pid=" + postId + "' class='link'>You and " + pst.Likes + " others like this</a>";
            }
            else
            {
                LikeButton.Text = "Like";
                LikeButton.CommandName = "Like";
                LikeButton.CommandArgument = postId;
                if (pst.Likes == 0)
                    LikeLabel.Text = "Be the First to Like This";
                else
                    LikeLabel.Text = "<a href='Likes.aspx?pid=" + postId + "' class='link'>" + pst.Likes + " Likes</a>";
            }
            List<CommentModel> feed = dh.getComments(postId);
            ListView1.DataSource = feed;
            ListView1.DataBind();
        }
        catch
        {
            System.Console.WriteLine("Exception in UpdateData of Comments");
        }
        UpdatePanel1.Update();
    }
    protected void updateData(object obj, EventArgs args)
    {
        try
        {
            ListView1.DataSource = dh.getComments(postId);
            ListView1.DataBind();
        }
        catch
        {

        }
        UpdatePanel1.Update();
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