using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for FeedModel
/// </summary>
public class FeedModel : IComparable
{
    private string name, email, data, postId, date, time, proimg;
    private int likes, comments;
    private bool iLike;
    public string Proimg
    {
        get { return proimg; }
        set { proimg = value; }
    }
    

    public bool ILike
    {
        get { return iLike; }
        set { iLike = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string Email
    {
        get { return email; }
        set { email = value; }
    }

    public string Data
    {
        get { return data; }
        set { data = value; }
    }

    public string PostId
    {
        get { return postId; }
        set { postId = value; }
    }

    public string Date
    {
        get { return date; }
        set { date = value; }
    }

    public string Time
    {
        get { return time; }
        set { time = value; }
    }

    public int Likes
    {
        get { return likes; }
        set { likes = value; }
    }

    public int Comments
    {
        get { return comments; }
        set { comments = value; }
    }

    public override string ToString()
    {
        return Date + " " + Time;
    }

    public int CompareTo(object obj)
    {
        return this.ToString().CompareTo(obj.ToString());
    }
}