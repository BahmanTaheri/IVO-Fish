using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using FastReport;
using FastReport.Web;


public partial class Admin : System.Web.UI.Page
{
    enum Ptype
    {
        Print, Save, Show
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Timeout = 3;
        string Mcode=string.Empty;
        
        if (Session["user_id"] != null)
        try
        { 
            Mcode = Session["user_id"].ToString();
        }
        catch 
        {
            Mcode = string.Empty;
        }
      
        if (!Page.IsPostBack)
        {      
            if (Mcode == string.Empty)
            {
                
                Session.Abandon();
                Response.Redirect("loginx.aspx");
            }
            SqlConnection con = new SqlConnection
            {
                ConnectionString = ConfigurationManager.AppSettings["Database"]
            };
            try
            {

                bool flogin = Convert.ToBoolean(Session["FirstLogin"]);
                if (!flogin)
                    Response.Redirect("changePass.aspx");
                //---------------------------------------------------------------           

                SqlDataAdapter ad = new SqlDataAdapter("SELECT DISTINCT YEAR FROM Tb5 WHERE (MELLI_CODE = @PM) ORDER BY YEAR DESC", con);
                  ad.SelectCommand.Parameters.AddWithValue("@PM", Mcode);
                  DataTable dt = new DataTable();
                  ad.Fill(dt);
                  List_Year.DataTextField = "YEAR";
                  List_Year.DataValueField = "YEAR";
                  List_Year.DataSource = dt;
                  List_Year.DataBind();

                  //---------------------------------------------------------------
                  int y = List_Year.Items.Count;
                  if (y > 0)
                  {
                      List_Year.SelectedIndex = y ;
                  }
                  List_Year_SelectedIndexChanged(sender, e);
            }
            catch  
            { 
            
            }

              con.Close();

        }
    }

   
    protected void List_Year_SelectedIndexChanged(object sender, EventArgs e)
    {
        string Mcode;

        try
        {
            Mcode = Session["user_id"].ToString();
        }
        catch
        {
            Mcode = string.Empty;
            Session.Abandon();
            Response.Redirect("loginx.aspx");
        }

        string Yr = List_Year.SelectedValue.ToString();
        SqlConnection con = new SqlConnection();
             con.ConnectionString = ConfigurationManager.AppSettings["Database"];
    
        try
        {
        
            SqlDataAdapter ad = new SqlDataAdapter("SELECT  TbMonth.Title, TbMonth.ID FROM  Tb5 INNER JOIN  TbMonth ON Tb5.MONTH = TbMonth.ID WHERE (Tb5.MELLI_CODE = @PM1) AND (Tb5.YEAR = @PM2) ORDER BY Tb5.YEAR DESC", con);
            ad.SelectCommand.Parameters.AddWithValue("@PM1", Mcode);
            ad.SelectCommand.Parameters.AddWithValue("@PM2", Yr);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            List_Month.DataTextField = "Title";
            List_Month.DataValueField = "ID";
            List_Month.DataSource = dt;
            List_Month.DataBind();
            int x = List_Month.Items.Count;
            if (x > 0)
            {
                List_Month.SelectedIndex = x -1;
            }
            List_Month_SelectedIndexChanged(sender, e);

        }
        catch { }
    }


    protected void List_Month_SelectedIndexChanged(object sender, EventArgs e)
    {
        PrintOrSaveOrShow(Ptype.Show);
    }


    protected void Btnexit_Click(object sender, EventArgs e)
    {
        
       
        Session.Abandon();
        Response.Redirect("loginx.aspx");
        
    }


    void PrintOrSaveOrShow(Ptype Act)
    {
        string Mcode;
        string Pcode;
        try
        {
            Pcode = Session["user_id"].ToString();
            Mcode = Session["user_id"].ToString();
        }
        catch
        {
            Mcode = string.Empty;
            Pcode = string.Empty;
            Session.Abandon();
            Response.Redirect("loginx.aspx");
        }

        string Yr = List_Year.SelectedValue.ToString();
        int M = int.Parse(List_Month.SelectedValue);
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.AppSettings["Database"];


        SqlDataAdapter ad = new SqlDataAdapter("SELECT * from Tb5 where MELLI_CODE=@C_M AND YEAR=@Y AND MONTH=@M", con);
        ad.SelectCommand.Parameters.AddWithValue("@C_M", Mcode);
        ad.SelectCommand.Parameters.AddWithValue("@Y", Yr);
        ad.SelectCommand.Parameters.AddWithValue("@M", M);
        DataTable dt5 = new DataTable();
        ad.Fill(dt5);


        DataTable dt4 = new DataTable();
        ad.SelectCommand.CommandText = "SELECT * from Tb4 where MELLI_CODE=@C_M";  
        ad.Fill(dt4);

        DataTable dt3 = new DataTable();
        ad.SelectCommand.CommandText = "SELECT * from view3 where MELLI_CODE=@C_M AND YEAR=@Y AND MONTH=@M  ORDER BY SortID"; //----tb3 ->view3
        ad.Fill(dt3);

        DataTable dt2 = new DataTable();
        ad.SelectCommand.CommandText = "SELECT * from Tb2 where MELLI_CODE=@C_M AND YEAR=@Y AND MONTH=@M";
        ad.Fill(dt2);

        DataTable dt1 = new DataTable();
        ad.SelectCommand.CommandText = "SELECT * from Tb1 where MELLI_CODE=@C_M AND YEAR=@Y AND MONTH=@M";
        ad.Fill(dt1);
        DataTable dtm = new DataTable();
        ad.SelectCommand.CommandText = "SELECT * from TbMonth where ID=@M";
        ad.Fill(dtm);


        Report rep = new Report();
        rep.RegisterData(dt5, "Tb5");
        rep.RegisterData(dt4, "Tb4");
        rep.RegisterData(dt3, "Tb3");
        rep.RegisterData(dt2, "Tb2");
        rep.RegisterData(dt1, "Tb1");
        rep.RegisterData(dtm, "TbMonth");

        /*
        string stype = Pcode.PadLeft(2);
        if ((stype == "54") || (stype == "55"))
        {
            rep.Load(Server.MapPath("~/App_Data/ReportSalBaz.frx"));
        }
        else
        {
            rep.Load(Server.MapPath("~/App_Data/ReportSal.frx"));
        }
        */

        rep.Load(Server.MapPath("~/App_Data/ReportSal.frx"));


        WebReport1.Report = rep;
        switch (Act)
        {
            case Ptype.Print:
                WebReport1.PrintHtml(HttpContext.Current);
                break;
            case Ptype.Save:
                WebReport1.PrintPdf(HttpContext.Current);
                break;
        }
     


        ad.Dispose(); con.Dispose();
        dt1.Dispose(); dt2.Dispose();
        dt3.Dispose(); dt4.Dispose();
        dt5.Dispose();  


    }
    protected void BtnPrint_Click(object sender, EventArgs e)
    {
        PrintOrSaveOrShow(Ptype.Print);

    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        PrintOrSaveOrShow(Ptype.Save);
    }

    protected void BtnChangePass_Click(object sender, EventArgs e)
    {       
        Response.Redirect("changePass.aspx");
    }
}