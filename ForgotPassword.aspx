<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Welcome to My Website</title>
    <link rel="stylesheet" type="text/css" href="css/style.css" />
</head>
<body>
    <div class="webheader">
        Website Name
    </div>
    <div class="main">
        <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="passchange">
                        <asp:Panel ID="EmailPanel" runat="server">
                            <span class="labletext">Enter Email</span>
                            <asp:TextBox CssClass="inputfield" ID="EmailText" placeholder="Enter Email" runat="server" TextMode="Email"></asp:TextBox>
                            <span class="labletext"></span>
                            <asp:Label CssClass="inputfield" ID="ErrorLabel1" Text="" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                            <span class="labletext"></span>
                            <asp:Button CssClass="inputfield button autowidth" ID="SubmitEmail" runat="server" Text="Submit" OnClick="SubmitEmail_Click" />
                        </asp:Panel>
                        <asp:Panel ID="PassPanel" runat="server" Visible="false">
                            <span class="labletext">Your Secuerity Question</span>
                            <asp:Label CssClass="inputfield" ID="SecqLabel" Text="" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                            <span class="labletext">Enter Your Answer</span>
                            <asp:TextBox CssClass="inputfield" ID="SecqAnswer" placeholder="Your Answer" runat="server" TextMode="Password"></asp:TextBox>
                            <span class="labletext">Enter New Password</span>
                            <asp:TextBox CssClass="inputfield" ID="NewPassword" placeholder="New Password" runat="server" TextMode="Password"></asp:TextBox>
                            <span class="labletext">Re-Enter New Password</span>
                            <asp:TextBox CssClass="inputfield" ID="ReNewPassword" placeholder="Repeate Password" runat="server" TextMode="Password"></asp:TextBox>
                            <span class="labletext"></span>
                            <asp:Label CssClass="inputfield" ID="ErrorLabel" Text="" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                            <span class="labletext"></span>
                            <asp:Button CssClass="inputfield button autowidth" ID="SubmitButton" runat="server" Text="Change Password" OnClick="SubmitButton_Click" />
                        </asp:Panel>
                        <span class="labletext">&nbsp;</span>
                        <a href="Default.aspx" class="inputfield link right">Login Here</a>
                        <span class="labletext">&nbsp;</span>
                        <a href="Register.aspx" class="inputfield link right">Register Here</a>
                    </div>
                   
                </ContentTemplate>
            </asp:UpdatePanel>
        </form>
    </div>
    <div class="footer">
        All Rights Reserved &copy; 2015
    </div>
</body>
</html>
