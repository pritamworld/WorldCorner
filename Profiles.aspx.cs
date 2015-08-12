using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Profiles : System.Web.UI.Page
{
    private DataHandler dh = new DataHandler();
    private string email;
    protected user_detail user;
    protected void Page_Load(object sender, EventArgs e)
    {
        email = Request.QueryString["email"];
        if (Session == null || Session["email"] == null || Session["name"] == null)
        {
            if (email == null || "".Equals(email))
                Response.Redirect("Default.aspx");
            else
                Response.Redirect("Default.aspx?nav=Profiles.aspx?email=" + email);
        }
        else
        {
            if (email == null || "".Equals(email))
            {
                email = Session["email"].ToString();
            }
        }
        if(email.Equals(Session["email"].ToString())){
            AddFriend.Visible = false;
        }
        if (!IsPostBack)
        {
            updateData();
            updateNot();
        }
        ListView1.PagePropertiesChanged += new EventHandler(updateData);
        if (!Session["email"].ToString().Equals(email))
        {
            ChangeButton.Visible = false;
            ChangePanel.Visible = false;
            PostPanel.Visible = false;
            MessageButton.Visible = true;
        }
        else
        {
            ChangeButton.Visible = true;
            ChangePanel.Visible = true;
            PostPanel.Visible = true;
            MessageButton.Visible = false;
        }
        if (user != null)
        {
            EditName.Text = user.name;
            EmailLabel.Text = user.email;
            GenderLabel.Text = EditGender.SelectedValue = user.gender;
            AddressLabel.Text = EditAddress.Text = user.addr;
            CountryLabel.Text = EditCountry.SelectedValue = user.country;
            About.Text = EditAbout.Text = user.about;
        }
        MyProfileLinkButton.Text = Session["name"].ToString();
        LinkButton7.Text = Session["name"].ToString();
    }
    private void updateProfile()
    {
        var q = dh.validateUser(email);
        if (q.Any())
        {
            foreach (user_detail ud in q)
            {
                NameLabel.Text = ud.name;
                ProfileImage.ImageUrl = "~/proimg/" + ud.proimg;
                user = ud;
                EditName.Text = user.name;
                EmailLabel.Text = user.email;
                GenderLabel.Text = EditGender.SelectedValue = user.gender;
                AddressLabel.Text = EditAddress.Text = user.addr;
                CountryLabel.Text = EditCountry.SelectedValue = user.country;
                About.Text = EditAbout.Text = user.about;
            }
        }
        else
        {
            Response.Redirect("Home.aspx");
        }
    }
    private void updateFriendButton()
    {
        if(!email.Equals(Session["email"].ToString())){
            int res = dh.checkFriend(Session["email"].ToString(), email);
            AddFriend.Visible = true;
            if(res == 1){
                AddFriend.Text = "Add as Friend";
                AddFriend.CommandName = "Add";
            }
            else if(res == 2){
                AddFriend.Text = "Remove Friend";
                AddFriend.CommandName = "Remove";
            }
            else if(res == 3){
                AddFriend.Text = "Request Sent";
                AddFriend.CommandName = "Remove";
            }
            else if(res == 4){
                AddFriend.Text = "Accept Request";
                AddFriend.CommandName = "Accept";
            }
        }
        else
        {
            AddFriend.Visible = false;
        }
        UpdatePanel1.Update();
    }
    private void updateData()
    {
        updateProfile();
        updateFriendButton();
        ListView1.DataSource = dh.getFeedsForProfile(email,Session["email"].ToString());
        ListView1.DataBind();
        UpdatePanel1.Update();
    }
    protected void updateData(object obj, EventArgs args)
    {
        List<FeedModel> feeds = dh.getFeedsForProfile(email,Session["email"].ToString());
        ListView1.DataSource = feeds;
        ListView1.DataBind();
        UpdatePanel1.Update();
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
            bool ilike = (bool)DataBinder.Eval(e.Item.DataItem, "ILike");
            Button b = (Button)e.Item.FindControl("LikeButton");
            Label l = (Label)e.Item.FindControl("LikeLabel");
            Label data = (Label)e.Item.FindControl("PostData");
            Image img = (Image)e.Item.FindControl("ProImage");
            img.ImageUrl = "~/proimg/" + user.proimg;
            int likes = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Likes"));
            string postId = (string)DataBinder.Eval(e.Item.DataItem, "PostId");
            string dataString = (string)DataBinder.Eval(e.Item.DataItem, "Data");
            if (dataString.Length > 50)
            {
                data.Text = dataString.Substring(0, 49) + "..." + "<a href='Comments.aspx?pid=" + postId + "' class='link'>See More</a>";
            }
            else
            {
                data.Text = dataString;
            }
            if (ilike)
            {
                b.Text = "Unlike";
                b.CommandName = "Unlike";
                b.CommandArgument = postId;
                if (likes == 0)
                    l.Text = "You Like This";
                else
                    l.Text = "<a href='Likes.aspx?pid="+postId+"' class='link'>You and " + likes + " others like this</a>";
            }
            else
            {
                b.Text = "Like";
                b.CommandName = "Like";
                b.CommandArgument = postId;
                if (likes == 0)
                    l.Text = "Be the First to Like This";
                else
                    l.Text = "<a href='Likes.aspx?pid=" + postId + "' class='link'>" + likes + " Likes</a>";
            }
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
                dh.newPost(Session["email"].ToString(), post);
                NewPost.Text = "";
                updateData();
            }
            catch
            {
                System.Console.WriteLine("Exception in Sending new Post");
            }
        }
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
    }
    protected void ChangeButton_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ChagePicture.aspx");
    }
    protected void MyProfileLinkButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Profiles.aspx?email=" + Session["email"].ToString());
        updateData();
    }
    protected void AboutButton_Click(object sender, EventArgs e)
    {
        ViewPostButton.Visible = true;
        AboutButton.Visible = false;
        FeedPanel.Visible = false;
        AboutPanel.Visible = true;
        EditPanel.Visible = false;
        UpdatePanel1.Update();
    }
    protected void ViewPostButton_Click(object sender, EventArgs e)
    {
        ViewPostButton.Visible = false;
        AboutButton.Visible = true;
        FeedPanel.Visible = true;
        AboutPanel.Visible = false;
        EditPanel.Visible = false;
        UpdatePanel1.Update();
    }
    protected void ChagePassword_Click(object sender, EventArgs e)
    {
        Response.Redirect("ChangePassword.aspx");
    }
    protected void ChangeSecq_Click(object sender, EventArgs e)
    {
        Response.Redirect("ChangeSecq.aspx");
    }
    protected void EditButton_Click(object sender, EventArgs e)
    {
        FeedPanel.Visible = false;
        AboutPanel.Visible = false;
        EditPanel.Visible = true;
        UpdatePanel1.Update();
    }
    protected void MessageButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Chats.aspx?email="+email);
    }
    protected void EditSubmit_Click(object sender, EventArgs e)
    {
        string name = EditName.Text;
        string gen = EditGender.Text;
        string addr = EditAddress.Text;
        string about = EditAbout.Text;
        string country = EditCountry.Text;
        if(name==null || "".Equals(name)){
            EditError.Text = "Name Cannot be Empty";
            EditError.ForeColor = System.Drawing.Color.DarkRed;
            UpdatePanel1.Update();
            return;
        }
        if (gen == null || "".Equals(gen))
        {
            EditError.Text = "Gender Cannot be Empty";
            EditError.ForeColor = System.Drawing.Color.DarkRed;
            UpdatePanel1.Update();
            return;
        }
        if (country == null || "".Equals(country))
        {
            EditError.Text = "Name Cannot be Empty";
            EditError.ForeColor = System.Drawing.Color.DarkRed;
            UpdatePanel1.Update();
            return;
        }
        try
        {
            dh.updateProfile(Session["email"].ToString(),name, gen, addr,about, country);
            EditError.Text = "Profile Updated";
            EditError.ForeColor = System.Drawing.Color.Green;
            updateProfile();
        }
        catch
        {
            EditError.Text = "Cannot Update Profile";
            EditError.ForeColor = System.Drawing.Color.DarkRed;
            UpdatePanel1.Update();
            return;
        }
    }
    protected void AddFriend_Click(object sender, CommandEventArgs e)
    {
        switch(e.CommandName){
            case "Add":
                try
                {
                    dh.addFriend(Session["email"].ToString(), email);
                    updateFriendButton();

                }
                catch
                {

                }
                break;
            case "Accept":
                try
                {
                    dh.approveFriend(Session["name"].ToString(), Session["email"].ToString(), email);
                    updateFriendButton();
                }
                catch
                {

                }
                break;
            case "Remove":
                try
                {
                    dh.removeFriend(Session["email"].ToString(), email);
                    updateFriendButton();
                }
                catch
                {

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
            ChangeButton.Visible = true;
            MsgPanel.Visible = false;
            FrndPanel.Visible = false;
            NotPanel.Visible = false;
        }
        else
        {
            ChangeButton.Visible = false;
            MsgPanel.Visible = false;
            FrndPanel.Visible = false;
            NotPanel.Visible = true;
            noteId.Text = "";
            dh.notificationRead(Session["email"].ToString());
        }
        UpdatePanel1.Update();
    }
    protected void NotMsg_Click(object sender, ImageClickEventArgs e)
    {
        if (MsgPanel.Visible)
        {
            ChangeButton.Visible = true;
            MsgPanel.Visible = false;
            FrndPanel.Visible = false;
            NotPanel.Visible = false;
        }
        else
        {
            ChangeButton.Visible = false;
            MsgPanel.Visible = true;
            FrndPanel.Visible = false;
            NotPanel.Visible = false;
        }
        UpdatePanel1.Update();
    }
    protected void NotFrnd_Click(object sender, ImageClickEventArgs e)
    {
        if (FrndPanel.Visible)
        {
            ChangeButton.Visible = true;
            MsgPanel.Visible = false;
            FrndPanel.Visible = false;
            NotPanel.Visible = false;
        }
        else
        {
            ChangeButton.Visible = false;
            MsgPanel.Visible = false;
            FrndPanel.Visible = true;
            NotPanel.Visible = false;
        }
        UpdatePanel1.Update();
    }
}