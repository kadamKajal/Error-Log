using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Error_Log
{
    public partial class errlog : System.Web.UI.Page        
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                throw new ApplicationException("Throw error");
                var i = 5;
                var j = 0;
                var result = i / j;
            }
            catch(Exception ee)
            {
                //write user friendly error message
                lblMessage.Text = "Sorry,an error Occured.Please try again later.";
                //log the error now
                LogError(ee.ToString());
            }

        }
        private void LogError(String meassage)
        {
            try
            {
                string path = "~/" + DateTime.Today.ToString("dd-mm-yy") + ".txt";
                if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
                {
                    File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close();
                }
                using (StreamWriter w = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path)))
                {
                    w.WriteLine("\r\nlog Entry : ");
                    w.WriteLine("{0}",DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    string err = "Error in:" + System.Web.HttpContext.Current.Request.Url.ToString() +

                    "\n\nError Message:" + meassage;
                    w.WriteLine(err);
                    w.WriteLine("=============================================");
                    w.Flush();
                    w.Close();




                }

            }
            catch
            {
                
                throw;
                

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}