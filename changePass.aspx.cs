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
using System.Text;
using System.Security.Cryptography;

public partial class Loginx : System.Web.UI.Page
{


    protected void BtnComite_Click(object sender, EventArgs e)
    {

        Security sc = new Security();
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.AppSettings["Database"];
        con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        string Mcode;
        string oldpass;
        try
        {
            Mcode = Session["user_id"].ToString();
            cmd.CommandText = "SELECT * FROM Tb4  WHERE MELLI_CODE=@MC";
            cmd.Parameters.AddWithValue("@MC", Mcode);
            SqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            oldpass = rdr["Pass"].ToString();
            rdr.Close();
            
        }
        catch
        {
            Mcode = string.Empty;
            oldpass = string.Empty;
        }
        cmd.Parameters.Clear();
        string p0 = sc.MD5Hash(txtpassword.Text);
        string p1 = sc.MD5Hash(txtpasswordnew.Text);
        string p2 = sc.MD5Hash(txtpasswordconf.Text);
       
        
        if (p0 == oldpass)
        {
            if (p1 == p2)
            {
                try
                {              
                    cmd.CommandText = "UPDATE Tb4 SET [PASS] = (@NewP) , [FirstLogin]=(@FLog)  WHERE [MELLI_CODE]=@MCod";
                    cmd.Parameters.AddWithValue("@MCod", Mcode);
                    cmd.Parameters.AddWithValue("@NewP", p1);
                    cmd.Parameters.AddWithValue("@FLog", SqlDbType.Bit).Value = (1);
                    
                    cmd.ExecuteNonQuery();
                    lblerr.Text = "رمز جدید با موفقیت ثبت شد";
                    lblerr.Visible = true;
                }
                catch
                {
                    string x = cmd.CommandText;
                }

            }
            else
            {
                lblerr.Text = "هردو رمز باهم مطابقت ندارند";
                lblerr.Visible = true;
            }
        }
        else
        {
            lblerr.Text = "رمز وارد شده اشتباه است";
            lblerr.Visible = true;
        }

        con.Close();
    }

    protected void BtnGotoMain_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("loginx.aspx");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Timeout = 3;
        if (Session["user_id"] == null)
        {
            Session.Abandon();
            Response.Redirect("loginx.aspx");
        }

    }
}