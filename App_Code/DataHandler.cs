using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;


public class DataHandler
{
    public LtoSqlDataContext dc = new LtoSqlDataContext();

    //TODO
    public void uploadImage(string email)
    {
        var q = from u in dc.user_details
                where u.email == email
                select u;
        foreach(user_detail u in q){
            u.proimg = email + ".jpg";
        }
        dc.SubmitChanges();
    }
    public IEnumerable<user_detail> validateUser(string email)
    {
        return from a in dc.user_details
               where a.email == email
               select a;
    }
    public IEnumerable<message> getMessages(string email , string fromId)
    {
        var q = from m in dc.messages
               where (m.to_id == email && m.from_id == fromId) || (m.from_id == email && m.to_id == fromId)
               select m;
        foreach(message m in q){
            if(email.Equals(m.to_id)){
                m.read = 1;
            }
        }
        dc.SubmitChanges();
        return q;
    }
    public List<FeedModel> getFeeds(string email)
    {
        var q = from p in dc.posts
                from u in dc.user_details
                from f in dc.friends
                where (p.user_id == email && u.email == email) || (f.sender_id == email && f.receiver_id == p.user_id && f.receiver_id == u.email && f.status == 1) || (f.receiver_id == email && f.sender_id == p.user_id && f.sender_id == u.email && f.status == 1)
                orderby p.date descending, p.time descending
                select new {Name=u.name, Email=u.email, PostId=p.post_Id, Time=p.time, Date=p.date, Data=p.data ,Img=u.proimg};
        List<FeedModel> list = new List<FeedModel>();
        q = q.Distinct();
        foreach(var feed in q){

            int likes = (from l in dc.likes
                    where l.post_id == feed.PostId
                    select l).Count();
            bool isILike = (from l in dc.likes
                            where l.post_id == feed.PostId
                            select l.user_id).Contains(email);
            int comments = (from l in dc.comments
                            where l.post_id == feed.PostId
                            select l).Count();
            if(isILike){
                likes--;
            }
            FeedModel item = new FeedModel();
            item.Name = feed.Name;
            item.Email = feed.Email;
            item.Data = feed.Data;
            item.Date = feed.Date;
            item.Time = feed.Time;
            item.Proimg = feed.Img;
            item.Comments = comments;
            item.Likes = likes;
            item.ILike = isILike;
            item.PostId = feed.PostId.ToString();
            list.Add(item);
        }
        list.Sort();
        list.Reverse();
        return list;
    }
    public List<FeedModel> getFeedsForProfile(string email, string useremail)
    {
        var q = from p in dc.posts
                where p.user_id == email
                orderby p.date descending, p.time descending
                select p;
        List<FeedModel> list = new List<FeedModel>();
        foreach (var feed in q)
        {
            int likes = (from l in dc.likes
                         where l.post_id == feed.post_Id
                         select l).Count();
            bool isILike = (from l in dc.likes
                            where l.post_id == feed.post_Id
                            select l.user_id).Contains(useremail);
            int comments = (from l in dc.comments
                            where l.post_id == feed.post_Id
                            select l).Count();
            if (isILike)
            {
                likes--;
            }
            FeedModel item = new FeedModel();
            item.Email = feed.user_id;
            item.Data = feed.data;
            item.Date = feed.date;
            item.Time = feed.time;
            item.Comments = comments;
            item.Likes = likes;
            item.ILike = isILike;
            item.PostId = feed.post_Id.ToString();
            list.Add(item);
        }
        return list;
    }
    public void newPost(string email, string post)
    {
        post p = new post();
        p.user_id = email;
        p.data = post;
        p.date = DateTime.Now.ToShortDateString();
        p.time = DateTime.Now.ToShortTimeString();

        dc.posts.InsertOnSubmit(p);
        dc.SubmitChanges();
    }

    public void comment(string name,string email, string postId, string data)
    {
        comment com = new comment();
        com.user_id = email;
        com.post_id = Convert.ToInt32(postId);
        com.data = data;
        com.date = DateTime.Now.ToShortDateString();
        com.time = DateTime.Now.ToShortTimeString();
        var r = from u in dc.user_details join p in dc.posts on u.email equals p.user_id where p.post_Id == Convert.ToInt32(postId) select u;
        user_detail u2 = r.First();
        if(!email.Equals(u2.email)){
            notification no = new notification();
            no.nottext = name + " Commented on Your Post";
            no.link = "Comments.aspx?pid=" + postId;
            no.user_id = u2.email;
            no.read = 0;
            dc.notifications.InsertOnSubmit(no);
        }
        dc.comments.InsertOnSubmit(com);
        dc.SubmitChanges();
    }

    public void likePost(string name, string email, string postId)
    {
        like l = new like();
        l.user_id = email;
        l.post_id = Convert.ToInt32(postId);
        var r = from u in dc.user_details join p in dc.posts on u.email equals p.user_id where p.post_Id == Convert.ToInt32(postId) select u;
        user_detail u2 = r.First();
        if (!email.Equals(u2.email))
        {
            notification no = new notification();
            no.nottext = name + " Likes Your Post";
            no.link = "Comments.aspx?pid=" + postId;
            no.user_id = u2.email;
            no.read = 0;
            dc.notifications.InsertOnSubmit(no);
        }
        dc.likes.InsertOnSubmit(l);
        dc.SubmitChanges();
    }
    public int checkFriend(string email, string to)
    {
        var q = from f in dc.friends
                where (f.sender_id == email && f.receiver_id == to) || (f.sender_id == to && f.receiver_id == email)
                select f;
        int success = 0;
        if(q.Any()){
            foreach(friend f in q){
                if(f.status == 1){
                    success = 2;
                }
                else
                {
                    if(email.Equals(f.sender_id)){
                        success = 3;
                    }
                    else
                    {
                        success = 4;
                    }
                }
            }
        }
        else
        {
            success = 1;
        }
        return success;
    }
    public IEnumerable<user_detail> search(string search)
    {
        return from u in dc.user_details
               where u.email.Contains(search) || u.name.Contains(search)
               select u;
    }
    public void sendMessage(string email, string to, string data)
    {
        message ms = new message();
        ms.from_id = email;
        ms.to_id = to;
        ms.msg = data;
        ms.read = 0;
        ms.time = DateTime.Now.ToShortTimeString();
        ms.date = DateTime.Now.ToShortDateString();
        dc.messages.InsertOnSubmit(ms);
        dc.SubmitChanges();
    }

    public void register(string email, string name, string pass, string gen, string secq, string seca, string country)
    {
        user_detail ud = new user_detail();
        ud.name = name;
        ud.email = email;
        ud.pass = pass;
        ud.gender = gen;
        ud.seca = seca;
        ud.secq = secq;
        ud.country = country;
        ud.addr = "";
        ud.about = "";
        ud.proimg = "propic.gif";
        dc.user_details.InsertOnSubmit(ud);
        dc.SubmitChanges();
    }

    public void addFriend(string email, string to)
    {
        friend fr = new friend();
        fr.sender_id = email;
        fr.receiver_id = to;
        fr.status = 0;
        dc.friends.InsertOnSubmit(fr);
        dc.SubmitChanges();
    }

    public void approveFriend(string name, string email, string fr)
    {
        var q = from a in dc.friends
                where a.sender_id == fr && a.receiver_id == email
                select a;
        foreach(friend fri in q){
            fri.status = 1;
        }
        notification no = new notification();
        no.nottext = name + " Accepted Your Friend Request";
        no.link = "Profiles.aspx?email=" + email;
        no.user_id = fr;
        no.read = 0;
        dc.notifications.InsertOnSubmit(no);
        dc.SubmitChanges();
    }

    public void unLikePost(string email, string postId)
    {
        int pid = Convert.ToInt32(postId);
        var q = from l in dc.likes
                where l.user_id == email && l.post_id == pid
                select l;
        if(q.Any()){
            foreach(like l in q){
                dc.likes.DeleteOnSubmit(l);
            }
            dc.SubmitChanges();
        }
    }

    public List<FriendModel> getFriends(string p)
    {
        var q = from u in dc.user_details
                from f in dc.friends
                where ((f.sender_id == p && f.receiver_id == u.email) || (f.receiver_id == p && f.sender_id == u.email)) && f.status==1
                select new {Name=u.name,Email=u.email,Status=u.about, ProImg = u.proimg };
        List<FriendModel> list = new List<FriendModel>();
        foreach(var fr in q){
            FriendModel item = new FriendModel();
            item.Email = fr.Email;
            item.Name = fr.Name;
            item.Status = fr.Status;
            item.ProImg = fr.ProImg;
            list.Add(item);
        }
        return list;
    }
    public List<FriendModel> getFriendRequests(string p)
    {
        var q = from u in dc.user_details
                from f in dc.friends
                where (f.receiver_id == p && f.sender_id == u.email) && f.status == 0
                select new { Name = u.name, Email = u.email, Status = u.about, ProImg = u.proimg };
        List<FriendModel> list = new List<FriendModel>();
        foreach (var fr in q)
        {
            FriendModel item = new FriendModel();
            item.Email = fr.Email;
            item.Name = fr.Name;
            item.Status = fr.Status;
            item.ProImg = fr.ProImg;
            list.Add(item);
        }
        return list;
    }
    public List<CommentModel> getComments(string postId)
    {
        var q = from c in dc.comments join u in dc.user_details
                on c.user_id equals u.email
                where c.post_id == Convert.ToInt32(postId)
                orderby c.date descending, c.time descending
                select new { Email=u.email, Name=u.name, Date=c.date, Time=c.time, Data=c.data, ProImg = u.proimg };
        List<CommentModel> list = new List<CommentModel>();
        foreach(var c in q){
            CommentModel item = new CommentModel();
            item.Email = c.Email;
            item.Name = c.Name;
            item.Time = c.Time;
            item.Date = c.Date;
            item.Data = c.Data;
            item.ProImg = c.ProImg;
            list.Add(item);
        }
        return list;
    }
    public FeedModel getPostDetails(string email, string postId)
    {
        var q = from p in dc.posts
                join u in dc.user_details
                    on p.user_id equals u.email
                where p.post_Id == Convert.ToInt32(postId)
                select new { Name=u.name, Email = u.email, Date = p.date, Time = p.time, Data = p.data, ProImg = u.proimg };
        int likes = (from l in dc.likes
                     where l.post_id == Convert.ToInt32(postId)
                     select l).Count();
        bool iLike = (from l in dc.likes
                      where l.post_id == Convert.ToInt32(postId)
                      select l.user_id).Contains(email);
        int comments = (from l in dc.comments
                        where l.post_id == Convert.ToInt32(postId)
                        select l).Count();
        var item = q.First();
        FeedModel model = new FeedModel();
        model.Name = item.Name;
        model.Email = item.Email;
        model.Comments = comments;
        model.Data = item.Data;
        model.Date = item.Date;
        model.ILike = iLike;
        model.Likes = likes;
        model.Time = item.Time;
        model.Proimg = item.ProImg;
        return model;
    }

    public IEnumerable<user_detail> getLikes(string postId)
    {
        return from u in dc.user_details
               join l in dc.likes
                   on u.email equals l.user_id
               where l.post_id == Convert.ToInt32(postId)
               select u;
    }

    public void removeFriend(string p1, string p2)
    {
        var q = from f in dc.friends
                where (f.sender_id == p1 && f.receiver_id == p2) || (f.sender_id == p2 && f.receiver_id == p1)
                select f;
        foreach(friend f in q){
            dc.friends.DeleteOnSubmit(f);
        }
        dc.SubmitChanges();
    }
    public void updateProfile(string email, string name, string gen, string addr, string about, string country)
    {
        var q = from u in dc.user_details
                where u.email == email
                select u;
        foreach(user_detail u in q){
            u.name = name;
            u.gender = gen;
            u.addr = addr;
            u.about = about;
            u.country = country;
        }
        dc.SubmitChanges();
    }
    public List<CommentModel> getRecentMessages(string email)
    {
        List<FriendModel> friends = getFriends(email);
        List<CommentModel> list = new List<CommentModel>();
        foreach(FriendModel f in friends){
            var q = from m in dc.messages
                    where (m.from_id == email && m.to_id == f.Email) || (m.from_id == f.Email && m.to_id == email)
                    orderby m.date descending, m.time descending
                    select m;
            if(q.Any()){
                message msg = q.First();
                CommentModel item = new CommentModel();
                item.Email = f.Email;
                item.Sender = msg.from_id;
                item.Name = f.Name;
                item.ProImg = f.ProImg;
                item.Data = msg.msg;
                item.Date = msg.date;
                item.Time = msg.time;
                item.Read = (int)msg.read;
                list.Add(item);
            }
        }
        list.Sort();
        return list;
    }
    public IEnumerable<notification> getNotifications(string email)
    {
           List<notification> list =( from n in dc.notifications
                                    where n.user_id == email
                                    select n).ToList();
           list.Reverse();
           return list;
    }
    public void notificationRead(string email)
    {
        var q = from n in dc.notifications
                where n.user_id == email
                select n;
        foreach(notification n in q){
            n.read = 1;
        }
        dc.SubmitChanges();
    }
}