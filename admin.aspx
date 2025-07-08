<%@ Page Language="C#" AutoEventWireup="true" CodeFile="admin.aspx.cs" Inherits="Admin" %>

<%@ Register Assembly="FastReport.Web" Namespace="FastReport.Web" TagPrefix="cc1" %>


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

<body class="body"  >

<div class="header"></div>        
<form id="frmAdmin" runat="server">

<div class="Contentbody">

    <div class="Contenttop">
        <table class="ContentTable">
            <tr>
                <td>
                        <asp:Label ID="Label1" runat="server" Text="سال" CssClass="admin_lbly"></asp:Label>
                        <asp:DropDownList ID="List_Year" runat="server" AutoPostBack="true"  CssClass="admin_listy"
                           onselectedindexchanged="List_Year_SelectedIndexChanged"  >
                        </asp:DropDownList>
                        <asp:Label ID="Label2" runat="server" Text="ماه" CssClass="admin_lblm"></asp:Label>  
                        <asp:DropDownList ID="List_Month" runat="server" AutoPostBack="true" CssClass="admin_listm" 
                           onselectedindexchanged="List_Month_SelectedIndexChanged"   >
                        </asp:DropDownList> 
                </td>
                <td >
                    <asp:Button ID="Btnexit" runat="server" Text="خروج" CssClass="exit_btn" OnClick="Btnexit_Click" /> 
                     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                     <asp:UpdatePanel ID="UpdatePanelLogin" runat="server">
                    <ContentTemplate>   
                    <!--<asp:Button ID="btnPrint" runat="server" Text="چاپ" CssClass="print_btn" OnClick="BtnPrint_Click" /> -->
                    <asp:Button ID="BtnSave" runat="server" Text=" ذخیره " CssClass="print_btn" OnClick="BtnSave_Click" /> 
                    <asp:Button ID="BtnChangePass" runat="server" Text=" تغییر رمز " CssClass="print_btn" OnClick="BtnChangePass_Click"  />
                    </ContentTemplate>
              </asp:UpdatePanel> 

                </td>
            </tr>
        </table>
  
                        
    </div>   

    <div class="Contentrep">
     
        <div style="margin-right: 1px;">
            
            <cc1:WebReport ID="WebReport1" runat="server"  PdfAuthor="Taheri" PdfCreator="Taheri" SinglePage="True" Padding="0, 0, 0, 0" Zoom="1.17" Height="850px" Width="958px" ShowToolbar="False" CssClass="rep"/>
        </div>
    </div>
    
</div>
     
</form>

<div class="footermain"></div>
  
</body>
</html>
