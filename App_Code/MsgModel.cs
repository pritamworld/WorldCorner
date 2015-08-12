using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MsgModel
/// </summary>
public class MsgModel
{
    private string name, msg, time, date;

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

    public string Msg
    {
        get { return msg; }
        set { msg = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }
}