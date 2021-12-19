using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.OracleClient;
namespace Tyuta
{
    public partial class AddTrainingType : System.Web.UI.Page
    {
        public string errMsg = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["ConnectionString"] = ConfigurationManager.ConnectionStrings["hr"].ToString();
                InitGrid();
            }
        }
        private void InitGrid()
        {
            DataSet ds = new DataSet();
            OracleConnection con = new OracleConnection(Session["ConnectionString"].ToString());
            
            string sql = "Select * from CODES WHERE TABLECODE='TrainingType' order by DESCRIPTION";
            OracleCommand cmd = new OracleCommand(sql, con);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.Fill(ds);
            Session["CODES"] = ds.Tables[0];
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
            GridView1.HeaderRow.ForeColor = System.Drawing.Color.RoyalBlue;
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Text = "קוד הכשרה";
                e.Row.Cells[2].Text = "תיאור הכשרה";
                e.Row.Cells[7].Text = "מזהה הכשרה";
            }

            for (int iCell = 0; iCell <= e.Row.Cells.Count - 1; iCell++)
            {                
                e.Row.Cells[iCell].Visible = false;
            }

            for (int iCell = 1; iCell <= 2; iCell++)
            {
                e.Row.Cells[iCell].Width = 200;
                e.Row.Cells[iCell].Visible = true;
            }
            e.Row.Cells[7].Width = 200;
            e.Row.Cells[7].Visible = true;
        }

        protected void cmdAdd_Click(object sender, EventArgs e)
        {
            int line = 0;
            try
            {

           
            string script = "<script language='javascript'>SetWaitCursor()</script>";
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script, false);

            if (txtDesc.Text == "" || txtCode.Text == "")
            {
                RegisterClientScriptBlock("alert", "<script>alert('יש להזין שם וקוד הדרכה')</script>");
            }
            else
            {
                if (Session["CODES"] == null)
                {
                    line = 1;
                        errMsg = "Session Codes is null";
                    return;
                }
                DataRow[] drFind = ((DataTable)Session["CODES"]).Select("TABLECODE='TrainingType' AND CODE='" + txtCode.Text + "'");
                if (drFind.Length > 0)
                {
                    RegisterClientScriptBlock("alert", "<script>alert('קוד כבר קיים')</script>");
                }
                else
                {
                    
                    DataSet ds = new DataSet();
                    OracleConnection con = new OracleConnection(Session["ConnectionString"].ToString());
                    OracleCommandBuilder build;
                    string sql = "Select * from CODES WHERE 1=-1";
                    OracleCommand cmd = new OracleCommand(sql, con);
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    da.Fill(ds);
                    line = 1;
                    DataRow drNew = ds.Tables[0].NewRow();
                    line = 2;
                    drNew["CODE"] = txtCode.Text;
                    drNew["DESCRIPTION"] = txtDesc.Text;
                    drNew["TABLECODE"] = "TrainingType";

                    ds.Tables[0].Rows.Add(drNew);
                    line = 3;
                    build = new OracleCommandBuilder(da);
                    line = 4;
                    da.Update(ds.Tables[0]);
                    line = 5;

                    InitGrid();
                    drFind = ((DataTable)Session["CODES"]).Select("TABLECODE='TrainingType' AND CODE='" + txtCode.Text + "'");
                    string sMsg = " קוד התווסף בהצלחה-" + drFind[0]["MZAA_CODE"];
                    RegisterClientScriptBlock("alert", "<script>alert('" + sMsg + "')</script>");

                    txtCode.Text = "";
                    txtDesc.Text = "";
                }
            }

            }
            
            catch(Exception ex)
            {
                errMsg = ex.Message+ " line:"+line;
            }
           
        }
    }
}