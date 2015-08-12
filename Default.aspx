<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Welcome to My Website</title>
    <link rel="stylesheet" type="text/css" href="css/style.css"  />
</head>
<body>
    <form id="form1" runat="server">
        <div class="webheader">
            Website Name
        </div>
        <div class="main">
            <div class="loginform">

                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <span class="labletext">Enter Email</span>
                        <asp:TextBox CssClass="inputfield" ID="TextBox1" placeholder="Name" runat="server" TextMode="Email"></asp:TextBox>
                        <span class="labletext">Enter Password</span>
                        <asp:TextBox CssClass="inputfield" ID="TextBox2" placeholder="Password" runat="server" TextMode="Password"></asp:TextBox>
                        <span class="labletext"></span>
                        <asp:Label CssClass="inputfield" ID="ErrorLabel" Text="" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                        <span class="labletext"></span>
                        <asp:Button CssClass="inputfield button" ID="SubmitButton" runat="server" Text="Login" OnClick="SubmitButton_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="1" runat="server">
                    <ProgressTemplate>
                        <div class="loader">
                            <img width="40" height="40" src="img/wait_please.gif" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>

                <span class="labletext">&nbsp;</span>
                <a href="Register.aspx" class="inputfield link right">Register Here</a>
                <span class="labletext">&nbsp;</span>
                <a href="ForgotPassword.aspx" class="inputfield link right">Forgot Password</a>
            </div>
        </div>
    </form>
    <div class="footer">
        All Rights Reserved &copy; 2015
    </div>
</body>
</html>
