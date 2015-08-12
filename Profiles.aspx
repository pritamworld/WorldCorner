<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Profiles.aspx.cs" Inherits="Profiles" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <title>Profile</title>
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
                                        <table>
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
                                    <asp:Panel ID="FrndDataPanel"  runat="server">
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
            <asp:LinkButton CssClass="linkbutton" ID="LinkButton7" Font-Bold="true" runat="server" OnClick="MyProfileLinkButton_Click"></asp:LinkButton><hr />
            <asp:LinkButton CssClass="linkbutton" ID="LinkButton8" PostBackUrl="~/Messages.aspx" Text="Messages" runat="server"></asp:LinkButton><hr />
            <asp:LinkButton CssClass="linkbutton" ID="LinkButton9" PostBackUrl="~/Friends.aspx" Text="Friends" runat="server"></asp:LinkButton><hr />
            <asp:LinkButton CssClass="linkbutton" ID="LinkButton10" PostBackUrl="~/Home.aspx" Text="News Feed" runat="server"></asp:LinkButton><hr />
            <asp:LinkButton CssClass="linkbutton" ID="LinkButton15" PostBackUrl="~/Search.aspx" Text="Search" runat="server"></asp:LinkButton><hr />
            <asp:LinkButton CssClass="linkbutton" ID="LinkButton11" PostBackUrl="~/Notifications.aspx" Text="Notifications" runat="server"></asp:LinkButton><hr />
            <asp:LinkButton CssClass="linkbutton" ID="LinkButton12" PostBackUrl="~/FriendRequests.aspx" Text="Friend Requests" runat="server"></asp:LinkButton><hr />
            <asp:LinkButton CssClass="linkbutton" ID="LinkButton13" Text="Logout" runat="server" OnClick="LinkButton6_Click"></asp:LinkButton><hr />
        </div>
        <div class="navbar">
            <asp:LinkButton CssClass="linkbutton" ID="MyProfileLinkButton" Font-Bold="true" Text='<%#Session["email"].ToString() %>' runat="server" OnClick="MyProfileLinkButton_Click"></asp:LinkButton><hr />
            <asp:LinkButton CssClass="linkbutton" ID="LinkButton1" PostBackUrl="~/Messages.aspx" Text="Messages" runat="server"></asp:LinkButton><hr />
            <asp:LinkButton CssClass="linkbutton" ID="LinkButton2" PostBackUrl="~/Friends.aspx" Text="Friends" runat="server"></asp:LinkButton><hr />
            <asp:LinkButton CssClass="linkbutton" ID="LinkButton3" PostBackUrl="~/Home.aspx" Text="News Feed" runat="server"></asp:LinkButton><hr />
            <asp:LinkButton CssClass="linkbutton" ID="LinkButton14" PostBackUrl="~/Search.aspx" Text="Search" runat="server"></asp:LinkButton><hr />
            <asp:LinkButton CssClass="linkbutton" ID="LinkButton4" PostBackUrl="~/Notifications.aspx" Text="Notifications" runat="server"></asp:LinkButton><hr />
            <asp:LinkButton CssClass="linkbutton" ID="LinkButton5" PostBackUrl="~/FriendRequests.aspx" Text="Friend Requests" runat="server"></asp:LinkButton><hr />
            <asp:LinkButton CssClass="linkbutton" ID="LinkButton6" Text="Logout" runat="server" OnClick="LinkButton6_Click"></asp:LinkButton><hr />
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div class="newmain">
                    <div class="profileheader">
                        <div class="profilename">
                            <asp:Label ID="NameLabel" runat="server"></asp:Label>
                        </div>
                        <div class="profilepic">
                            <div class="proimg">
                                <asp:Image ID="ProfileImage" runat="server" />
                            </div>
                            <div class="changepicture">
                                <asp:ImageButton ID="ChangeButton" runat="server" ImageUrl="~/img/prochange.png" Height="40px" Width="40px" OnClick="ChangeButton_Click" />
                            </div>
                            <div class="profilemenu">
                                <div class="profilebutton">
                                    <asp:Button ID="AboutButton" runat="server" CssClass="button autowidth" Text="About" OnClick="AboutButton_Click" />
                                    <asp:Button ID="ViewPostButton" runat="server" CssClass="button autowidth" Text="Posts" Visible="false" OnClick="ViewPostButton_Click" />
                                    <asp:Button ID="AddFriend" runat="server" CssClass="button autowidth" Visible="false" OnCommand="AddFriend_Click" />
                                    <asp:Button ID="MessageButton" runat="server" CssClass="button autowidth" Text="Message" Visible="false" OnClick="MessageButton_Click" />
                                </div>

                                <div class="profilechangebutton">
                                    <asp:Panel ID="ChangePanel" runat="server">
                                        <asp:Button ID="EditButton" runat="server" CssClass="button autowidth" Text="Edit Profile" OnClick="EditButton_Click" />
                                        <asp:Button ID="ChagePassword" runat="server" CssClass="button autowidth" Text="Change Password" OnClick="ChagePassword_Click" />
                                        <asp:Button ID="ChangeSecq" runat="server" CssClass="button autowidth" Text="Change Security Question" OnClick="ChangeSecq_Click" />
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:Panel ID="FeedPanel" runat="server">
                        <div class="newpost">
                            <asp:Panel ID="PostPanel" CssClass="about" runat="server">
                                <asp:TextBox ID="NewPost" runat="server" TextMode="MultiLine" CssClass="inputfield" placeholder="New Post"></asp:TextBox>
                                <asp:Button ID="PostButton" runat="server" Text="Post" CssClass="button" OnClick="PostButton_Click" />
                            </asp:Panel>
                        </div>
                        <div class="feed">
                            <asp:ListView ID="ListView1" runat="server" OnItemDataBound="ListView1_ItemDataBound">
                                <ItemTemplate>
                                    <table class="post">
                                        <tr runat="server" class="userdetails tabheader">
                                            <td colspan="2">
                                                <asp:Image ID="ProImage" runat="server" Width="30px" Height="30px" />
                                                <a href="Profiles.aspx?email=<%#user.email %>" class="link"><%#user.name %></a>
                                            </td>
                                            <td>
                                                <%#Eval("Date") %> &nbsp; &nbsp; <%#Eval("Time") %>
                                            </td>
                                        </tr>
                                        <tr runat="server" class="userdetails">
                                            <td colspan="3">
                                                <asp:Label ID="PostData" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr runat="server">
                                            <td style="width: 33%">
                                                <asp:Button runat="server" ID="LikeButton" OnCommand="LikeButton_Command"></asp:Button>
                                            </td>
                                            <td style="width: 33%">
                                                <asp:Label ID="LikeLabel" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 33%">
                                                <a href="Comments.aspx?pid=<%#Eval("PostId") %>" class="link"><%#Eval("Comments") %> &nbsp; Comments</a>
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
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="AboutPanel" runat="server" Visible="false">
                        <div class="newnewpost">
                            <table class="post about">
                                <tr class="userdetails" style="width: 100%;">
                                    <td style="width: 50%">Email :
                                    </td>
                                    <td style="width: 50%">
                                        <asp:Label ID="EmailLabel" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="userdetails">
                                    <td>Gender :
                                    </td>
                                    <td>
                                        <asp:Label ID="GenderLabel" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="userdetails">
                                    <td>About :
                                    </td>
                                    <td>
                                        <asp:Label ID="About" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="userdetails">
                                    <td>Address :
                                    </td>
                                    <td>
                                        <asp:Label ID="AddressLabel" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="userdetails">
                                    <td>Country :
                                    </td>
                                    <td>
                                        <asp:Label ID="CountryLabel" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="EditPanel" runat="server" Visible="false">
                        <div class="newnewpost">
                            <table class="post about">
                                <tr class="userdetails">
                                    <td colspan="2" style="width: 100%;">
                                        <asp:Label ID="EditError" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="userdetails">
                                    <td style="width: 50%;" class="labletext">Name :
                                    </td>
                                    <td style="width: 50%;">
                                        <asp:TextBox CssClass="inputfield" ID="EditName" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="userdetails">
                                    <td class="labletext">Gender :
                                    </td>
                                    <td>
                                        <asp:RadioButtonList CssClass="inputfield" ID="EditGender" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem>Male</asp:ListItem>
                                            <asp:ListItem>Female</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr class="userdetails">
                                    <td class="labletext">About :
                                    </td>
                                    <td>
                                        <asp:TextBox CssClass="inputfield" ID="EditAbout" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="userdetails">
                                    <td class="labletext">Address :
                                    </td>
                                    <td>
                                        <asp:TextBox CssClass="inputfield" ID="EditAddress" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="userdetails">
                                    <td class="labletext">Country :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="EditCountry" CssClass="inputfield" runat="server">
                                            <asp:ListItem>Afganistan</asp:ListItem>
                                            <asp:ListItem>Bangladesh</asp:ListItem>
                                            <asp:ListItem>Canada</asp:ListItem>
                                            <asp:ListItem>China</asp:ListItem>
                                            <asp:ListItem>Germany</asp:ListItem>
                                            <asp:ListItem>India</asp:ListItem>
                                            <asp:ListItem>Indonesia</asp:ListItem>
                                            <asp:ListItem>Myanmar</asp:ListItem>
                                            <asp:ListItem>Nepal</asp:ListItem>
                                            <asp:ListItem>Pakistan</asp:ListItem>
                                            <asp:ListItem>Russia</asp:ListItem>
                                            <asp:ListItem>Saudi Arab</asp:ListItem>
                                            <asp:ListItem>South Africa</asp:ListItem>
                                            <asp:ListItem>United Kingdom</asp:ListItem>
                                            <asp:ListItem>United States</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr class="userdetails">
                                    <td></td>
                                    <td>
                                        <asp:Button ID="EditSubmit" CssClass="inputfield button autowidth" runat="server" Text="Submit Changes" OnClick="EditSubmit_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    <div class="footer">
        All Rights Reserved &copy; 2015
    </div>
</body>
</html>
