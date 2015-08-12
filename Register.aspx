<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Welcome to My Website</title>
    <link rel="stylesheet" type="text/css" href="css/style.css" media="only screen and (min-width:701px)" />
</head>
<body>
    <div class="webheader">
        Website Name
    </div>
    <div class="main">
        <div class="regform">
            <form id="form1" runat="server">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <span class="title">Register Here</span><hr />
                        <span class="labletext">Enter Email</span>
                        <asp:TextBox CssClass="inputfield" ID="Email" placeholder="Email" runat="server" TextMode="Email"></asp:TextBox>
                        <span class="labletext">Enter Password</span>
                        <asp:TextBox CssClass="inputfield" ID="Password" placeholder="Password" runat="server" TextMode="Password"></asp:TextBox>
                        <span class="labletext">Re-Enter Password</span>
                        <asp:TextBox CssClass="inputfield" ID="RePassword" placeholder="Re-Password" runat="server" TextMode="Password"></asp:TextBox>
                        <span class="labletext">Enter Name</span>
                        <asp:TextBox CssClass="inputfield" ID="Name" placeholder="Name" runat="server" TextMode="SingleLine"></asp:TextBox>
                        <span class="labletext">Gender</span>
                        <asp:RadioButtonList CssClass="inputfield" ID="Gender" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True">Male</asp:ListItem>
                            <asp:ListItem>Female</asp:ListItem>
                        </asp:RadioButtonList>
                        <span class="labletext">Choose Country</span>
                        <asp:DropDownList ID="CountryName" CssClass="inputfield" runat="server">
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
                        <span class="labletext">Select Security Question</span>
                        <span class="labletext"></span>
                        <asp:DropDownList ID="SequrityQuestion" CssClass="inputfield" runat="server">
                            <asp:ListItem>Your First Pet Name</asp:ListItem>
                            <asp:ListItem>Your Mother&#39;s Maiden Name</asp:ListItem>
                            <asp:ListItem>Your Favorite Actor</asp:ListItem>
                            <asp:ListItem>Your Favorite Actress</asp:ListItem>
                            <asp:ListItem>Where You were Born</asp:ListItem>
                            <asp:ListItem>Your First School</asp:ListItem>
                        </asp:DropDownList>
                        <span class="labletext">Your Answer</span>
                        <asp:TextBox CssClass="inputfield" ID="SequrityAnswer" placeholder="Answer" runat="server" TextMode="SingleLine"></asp:TextBox>
                        <span class="labletext"></span>
                        <asp:Label CssClass="inputfield" ID="ErrorLabel" runat="server" Font-Bold="True"></asp:Label>
                        <span class="labletext">&nbsp;</span>
                        <asp:Button CssClass="inputfield button" ID="SubmitButton" runat="server" Text="Register" OnClick="SubmitButton_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="1" runat="server">
                    <ProgressTemplate>
                        <div class="loader">
                            <img width="40" height="40" src="img/wait_please.gif" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </form>
            <span class="labletext">&nbsp;</span>
            <a href="Default.aspx" class="inputfield link right">Login Here</a>
        </div>
    </div>
    <div class="footer">
        All Rights Reserved &copy; 2015
    </div>
</body>
</html>
