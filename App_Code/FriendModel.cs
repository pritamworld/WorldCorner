using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class FriendModel
{
    private string name, email, status, proImg;
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

    public string Status
    {
        get { return status; }
        set { status = value; }
    }
}
