<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Messages.aspx.cs" Inherits="Messages" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <title>Messages</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
            <div class="webname">
                Website Name
            </div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="note">
                        <ul>
                            <li>
                                <asp:ImageButton ID="Not" runat="server" OnClick="Not_Click" Height="40" Width="40" ImageUrl="~/img/notify.png" />
                                <asp:Label CssClass="ticker" runat="server" ID="noteId" BackColor="Red" ForeColor="Black" />
                            </li>
                            <li>
                                <asp:ImageButton ID="NotMsg" ImageUrl="~/img/msg.png" runat="server" OnClick="NotMsg_Click" Height="40" Width="40" />
                                <asp:Label CssClass="ticker" runat="server" ID="Label1" BackColor="Red" ForeColor="Black" />
                            </li>
                            <li>
                                <asp:ImageButton ID="NotFrnd" runat="server" ImageUrl="~/img/frnd.png" OnClick="NotFrnd_Click" Height="40" Width="40" />
                                <asp:Label CssClass="ticker" runat="server" ID="Label2" BackColor="Red" ForeColor="Black" />
                            </li>
                            <li>
                                <img class="flip" src="img/menu-slider.jpg" /></li>
                        </ul>
                    </div>
                    <asp:Panel ID="NotPanel" CssClass="notpanel" Visible="false" runat="server">
                        <div class="marker"></div>
                        <div class="notdata">
                            <asp:ListView ID="NotList" OnDataBound="NotList_DataBound" OnItemDataBound="NotList_ItemDataBound" runat="server">
                                <ItemTemplate>
                                    <asp:Panel ID="NotDataPanel" CssClass="post" runat="server">
                                        <table >
                                            <tr>
                                                <td>
                                                    <a href="<%#Eval("link") %>" class="link"><%#Eval("nottext") %></a>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </ItemTemplate>
                                <EmptyItemTemplate>
                                    <div class="empty">
                                        No Notifications
                                    </div>
                                </EmptyItemTemplate>
                            </asp:ListView>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="MsgPanel" CssClass="notmsgpanel" Visible="false" runat="server">
                        <div class="marker"></div>
                        <div class="notdata">
                            <asp:ListView ID="MsgList" OnDataBound="MsgList_DataBound" OnItemDataBound="MsgList_ItemDataBound" runat="server">
                                <ItemTemplate>
                                    <asp:Panel ID="MsgDataPanel" CssClass="post" runat="server">
                                        <table style="width: 98%;">
                                            <tr runat="server" style="float: left; width: 100%;">
                                                <td colspan="2" style="float: left;">
                                                    <asp:Image ID="UserImage" runat="server" Width="30px" Height="30px" />
                                                    <a href="Chats.aspx?email=<%#Eval("Email") %>" class="link"><%#Eval("Name") %></a>
                                                </td>
                                                <td style="float: right;">
                                                    <%#Eval("Date") %> &nbsp; &nbsp; <%#Eval("Time") %>
                                                </td>
                                            </tr>
                                            <tr runat="server">
                                                <td colspan="3" style="float: left;">
                                                    <asp:Label ID="DataLabel" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </ItemTemplate>
                                <EmptyItemTemplate>
                                    <div class="empty">
                                        No Messages
                                    </div>
                                </EmptyItemTemplate>
                            </asp:ListView>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="FrndPanel" CssClass="notfrndpanel" Visible="false" runat="server">
                        <div class="marker"></div>
                        <div class="notdata">
                            <asp:ListView ID="FrndList" OnDataBound="FrndList_DataBound" OnItemDataBound="FrndList_ItemDataBound" runat="server">
                                <ItemTemplate>
                                    <asp:Panel ID="FrndDataPanel" runat="server">
                                        <table class="post" runat="server">
                                            <tr>
                                                <td rowspan="2" class="imagetd" width="65px">
                                                    <asp:Image ID="UserImage" Width="40" Height="40" runat="server" />
                                                </td>
                                                <td class="nametd tabheader">
                                                    <a class="link" href="Profiles.aspx?email=<%#Eval("Email") %>"><%#Eval("Name") %> </a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="statustd">
                                                    <asp:Button ID="ConfirmFriend" Text="Accept" CssClass="button autowidth" runat="server" OnCommand="ConfirmFriend_Click" />
                                                    <asp:Button ID="DeleteFriend" Text="Delete Request" CssClass="button autowidth" runat="server" OnCommand="DeleteFriend_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </ItemTemplate>
                                <EmptyItemTemplate>
                                    <div class="empty">
                                        No New Request
                                    </div>
                                </EmptyItemTemplate>
                            </asp:ListView>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:Timer ID="Timer1" Interval="2000" OnTick="Timer1_Tick" runat="server"></asp:Timer>
            <div class="nav">
                <ul>
                    <li><a href="Home.aspx">Home</a></li>
                    <li><a href="Profiles.aspx">Profile</a></li>
                    <li><a href="Logout.aspx">Logout</a></li>
                </ul>
            </div>
        </div>
        <div class="container">
            <asp:LinkButton CssClass="linkbutton" ID="LinkButton7" Font-Bold="true" PostBackUrl="~/Profiles.aspx" runat="server"></asp:LinkButton><hr />
            <asp:LinkButton CssClass="linkbutton" ID="LinkButton8" PostBackUrl="~/Messages.aspx" Text="Messages" runat="server"></asp:LinkButton><hr />
            <asp:LinkButton CssClass="linkbutton" ID="LinkButton9" PostBackUrl="~/Friends.aspx" Text="Friends" runat="server"></asp:LinkButton><hr />
            <asp:LinkButton CssClass="linkbutton" ID="LinkButton10" PostBackUrl="~/Home.aspx" Text="News Feed" runat="server"></asp:LinkButton><hr />
            <asp:LinkButton CssClass="linkbutton" ID="LinkButton15" PostBackUrl="~/Search.aspx" Text="Search" runat="server"></asp:LinkButton><hr />
            <asp:LinkButton CssClass="linkbutton" ID="LinkButton11" PostBackUrl="~/Notifications.aspx" Text="Notifications" runat="server"></asp:LinkButton><hr />
            <asp:LinkButton CssClass="linkbutton" ID="LinkButton12" PostBackUrl="~/FriendRequests.aspx" Text="Friend Requests" runat="server"></asp:LinkButton><hr />
            <asp:LinkButton CssClass="linkbutton" ID="LinkButton13" Text="Logout" runat="server" OnClick="LinkButton6_Click"></asp:LinkButton><hr />
        </div>

        <div class="navbar">
            <asp:LinkButton CssClass="linkbutton" ID="MyProfileLinkButton" Font-Bold="true" PostBackUrl="~/Profiles.aspx" runat="server"></asp:LinkButton><hr />
            <asp:LinkButton CssClass="linkbutton" ID="LinkButton1" PostBackUrl="~/Messages.aspx" Text="Messages" runat="server"></asp:LinkButton><hr />
            <asp:LinkButton CssClass="linkbutton" ID="LinkButton2" PostBackUrl="~/Friends.aspx" Text="Friends" runat="server"></asp:LinkButton><hr />
            <asp:LinkButton CssClass="linkbutton" ID="LinkButton3" PostBackUrl="~/Home.aspx" Text="News Feed" runat="server"></asp:LinkButton><hr />
            <asp:LinkButton CssClass="linkbutton" ID="LinkButton14" PostBackUrl="~/Search.aspx" Text="Search" runat="server"></asp:LinkButton><hr />
            <asp:LinkButton CssClass="linkbutton" ID="LinkButton4" PostBackUrl="~/Notifications.aspx" Text="Notifications" runat="server"></asp:LinkButton><hr />
            <asp:LinkButton CssClass="linkbutton" ID="LinkButton5" PostBackUrl="~/FriendRequests.aspx" Text="Friend Requests" runat="server"></asp:LinkButton><hr />
            <asp:LinkButton CssClass="linkbutton" ID="LinkButton6" Text="Logout" runat="server" OnClick="LinkButton6_Click"></asp:LinkButton><hr />
        </div>
        <div class="newmain">
            <div class="newnewpost">
                <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:ListView ID="ListView1" runat="server" OnItemDataBound="ListView1_ItemDataBound">
                            <ItemTemplate>
                                <table class="post">
                                    <tr runat="server" class="userdetails tabheader">
                                        <td colspan="2" style="width: 70%;">
                                            <asp:Image ID="UserImage" runat="server" Width="30px" Height="30px" />
                                            <a href="Chats.aspx?email=<%#Eval("Email") %>" class="link"><%#Eval("Name") %></a>
                                        </td>
                                        <td>
                                            <%#Eval("Date") %> &nbsp; &nbsp; <%#Eval("Time") %>
                                        </td>
                                    </tr>
                                    <tr runat="server" class="userdetails">
                                        <td colspan="3">
                                            <%#Eval("Data") %>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:ListView>
                        <div class="pager">
                            <asp:DataPager ID="DataPager1" runat="server" PagedControlID="ListView1" PageSize="10">
                                <Fields>
                                    <asp:NextPreviousPagerField ButtonType="Image" ShowFirstPageButton="true" ShowPreviousPageButton="true" ShowNextPageButton="false" />
                                    <asp:NumericPagerField ButtonType="Image" />
                                    <asp:NextPreviousPagerField ButtonType="Image" ShowFirstPageButton="false" ShowLastPageButton="true" ShowPreviousPageButton="false" />
                                </Fields>
                            </asp:DataPager>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </form>
    <div class="footer">
        All Rights Reserved &copy; 2015
    </div>
</body>
</html>
