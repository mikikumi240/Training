using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tyuta
{
    public partial class Login : System.Web.UI.Page
    {
        public string errorMsg = "";
        //static System.Diagnostics.TraceSwitch generalSwitch = new System.Diagnostics.TraceSwitch("General", "Entire Application");

        protected void Page_Load(object sender, EventArgs e)
        {

            //ArgumentException ae = new ArgumentException("oyoyoyoyoyo");
            //Trace.Warn("Exception Handling", "Warning: Page_Load.", ae);
            //Trace.Write("Exception Handling", "write: Page_Load.", ae);
            //return;
            if (!IsPostBack)
            {
                if (Request["alert"] != null && Request["alert"] == "sessionend")
                {
                    RegisterClientScriptBlock("alert", "<script>alert('לא בוצעה פעולה במערכת זמן רב - הועברת למסך הזדהות')</script>");
                }                                
            }            
        }     

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            
            string nickName = userNameTxt.Value.TrimEnd().ToUpper();            

            string ReadConnectionString = ConfigurationManager.ConnectionStrings["ConnectionStringGlobal"].ToString();
            string sql = "";
            DataSet dsUsrs = null;
            try
            {

                sql = "select * from usrs where nickname= '" + nickName + "'";
                dsUsrs = OracleDataLayer.ExecuteDataset(sql, ReadConnectionString);
            }
            catch(Exception ex)
            {
                writeLog(ex.Message + " inner exeption: " + ex.InnerException);
                RegisterClientScriptBlock("alert", "<script>alert('קרתה תקלה, אנא נסה שוב יותר מאוחר')</script>");
            }
            finally
            {
                if (OracleDataLayer.conn != null && OracleDataLayer.conn.State == ConnectionState.Open)
                    OracleDataLayer.conn.Close();
            }

            
            if (dsUsrs != null && dsUsrs.Tables[0].Rows.Count > 0)
            {
                
                Response.Redirect("Main.aspx");
            }
            else
            {
                RegisterClientScriptBlock("alert", "<script>alert('משתמש לא קיים')</script>");
                errorMsg = "משתמש לא קיים";
                writeLog("wrong user or password, user: '" + nickName +  "'");
            }
           
        }
        private static void writeLog(string msg)
        {
            string logFilePath = ConfigurationManager.AppSettings["logFile"];
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(logFilePath, true))
            {
                file.WriteLine(DateTime.Now + " " + msg);
            }
        }

        






    }
}