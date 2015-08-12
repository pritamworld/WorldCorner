using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CommentModel
/// </summary>
public class CommentModel : IComparable
{
    private string name, email, time, date, data, proImg, sender;

    public string Sender
    {
        get { return sender; }
        set { sender = value; }
    }
    private int read;

    public int Read
    {
        get { return read; }
        set { read = value; }
    }

    public string ProImg
    {
        get { return proImg; }
        set { proImg = value; }
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

    public string Time
    {
        get { return time; }
        set { time = value; }
    }

    public string Date
    {
        get { return date; }
        set { date = value; }
    }

    public string Data
    {
        get { return data; }
        set { data = value; }
    }

    public override string ToString()
    {
        return Date+" "+Time;
    }

    public int CompareTo(object obj)
    {
        return this.ToString().CompareTo(obj.ToString());
    }
}