using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Imaging;
using System.Activities.Statements;

public partial class captcha : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();
        Random rnd = new Random();
        
        string cp = Session["Captcha"].ToString();
        
        Bitmap secimage = new Bitmap(Server.MapPath("~/images/bkcapcha.jpg"));
        Graphics Grp = Graphics.FromImage(secimage);
        Grp.DrawString(cp.Substring(0, 1), new Font("Tahoma", 23, FontStyle.Bold), new SolidBrush(Color.Red), new PointF(4, rnd.Next(-4, 10)));
        Grp.DrawString(cp.Substring(1, 1), new Font("Tahoma", 21, FontStyle.Bold), new SolidBrush(Color.Blue), new PointF(27, rnd.Next(-4, 10)));
        Grp.DrawString(cp.Substring(2, 1), new Font("Tahoma", 22, FontStyle.Bold), new SolidBrush(Color.Green), new PointF(54, rnd.Next(-4, 10)));
        Grp.DrawString(cp.Substring(3, 1), new Font("Tahoma", 21, FontStyle.Bold), new SolidBrush(Color.Brown), new PointF(74, rnd.Next(-4, 10)));
        Grp.Flush();
        Response.ContentType = "image/jpeg";
        secimage.Save(Response.OutputStream, ImageFormat.Jpeg);


        Grp.Dispose();
        secimage.Dispose();
        

    }
}