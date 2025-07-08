<%@ Page Language="C#" AutoEventWireup="true" CodeFile="changePass.aspx.cs" Inherits="Loginx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width" />              
    <link href="CSS/General.css" rel="stylesheet"  media="screen"/> 
     
    <!--[if lt IE 9]>
    <script src="//html5shiv.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->
    <!-- set for all resoultion -->
    <!--
    <link href="CSS/LargDesktop.css" rel="stylesheet"  media="only screen and (min-width: 1800px)" />
    <link href="CSS/General.css"  rel="stylesheet" media="only screen and (min-width:1024px) and (max-width: 1600px)" />
    <link href="CSS/Tablet.css"  rel="stylesheet" media="only screen and (min-width:500px) and (max-width: 900px)" />
    <link href="CSS/Mobile.css" rel="stylesheet" media="only screen and  (max-width: 490px)"  />

        -->
       
    <title> سامانه فیش حقوقی کارکنان </title>

</head>

<body class="body">
    <div class="header"></div>
    <form id="formlogin" runat="server" defaultbutton="btnGotoMain" defaultfocus="txtpassword">
        <div class="Contentbody">
            

          <div class="Login_Form">
          <h1>تغییر رمز عبور   </h1>
              <asp:TextBox ID="txtpassword" runat="server" placeholder="کلمه عبور " CssClass="Login_ps" TextMode="Password" TabIndex="1"></asp:TextBox>
             
              <asp:TextBox ID="txtpasswordnew" runat="server" placeholder="کلمه عبور جدید" CssClass="Login_ps" TextMode="Password" TabIndex="2"></asp:TextBox>
              <asp:TextBox ID="txtpasswordconf" runat="server" placeholder="تکرار کلمه عبور جدید" CssClass="Login_ps" TextMode="Password" TabIndex="3"></asp:TextBox>
              <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
              <asp:UpdatePanel ID="UpdatePanelLogin" runat="server">
              </asp:UpdatePanel>

              <br />
              <asp:Button ID="BtnGotoMain" runat="server" Text="بازگشت" CssClass="Login_btn" TabIndex="4" OnClick="BtnGotoMain_Click" />
              <asp:Button ID="BtnComite" runat="server" Text="تغییر رمز" CssClass="Login_btn" TabIndex="4" OnClick="BtnComite_Click" />
              <br /> 
              <asp:Label ID="lblerr" runat="server" Text=" کلمه عبور اشتباه است" CssClass="Login_lbl" Visible="false"> </asp:Label>           
          </div>  

                                
           
       </div>


    </form>

    
    <div class="footermain"></div>
    

</body>
</html>
