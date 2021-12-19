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
    public partial class ExecCom : System.Web.UI.Page
    {
        OracleConnection oCon ;
        protected void Page_Load(object sender, EventArgs e)
        {
            string sCon=  ConfigurationManager.ConnectionStrings["ConnectionDanProd"].ToString();
            oCon = new OracleConnection(sCon);

        }
        void DisplayMessage()
        {
            DataSet ds = new DataSet();
            string sMsg = "";
            string strIn = "";
            switch (RadioButtonList1.SelectedValue)
            {
                case "tagmulim":
                    strIn = "('119','156','157','158','1015','1113','1114')";
                    
                    break;
                case "CalcPremMonthly":
                    strIn = "('0040','0698')";
                    break;
                default:
                    strIn="('"+ RadioButtonList1.SelectedValue + "')";
                    break;
            }            

            int mm= DateTime.Today.AddMonths(-1).Month;
            int yy= DateTime.Today.AddMonths(-1).Year; 
            string sql = "Select count(*) kamut from presalary.payment where mm=" + 3 +" and yyyy= "+2+ " and BENEFICIARY in " + strIn+ " group by BENEFICIARY";

            OracleCommand cmd = new OracleCommand(sql, oCon);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.Fill(ds);
            foreach(DataRow drTmp in ds.Tables[0].Rows)
            {
                sMsg = " סמל  " + drTmp["BENEFICIARY"] + "-" + drTmp["kamut"] + " רשומות "+Environment.NewLine ;
            }
            RegisterClientScriptBlock("alert", "<script>alert('"+ sMsg +"')</script>");

        }
        protected void cmdExec_Click(object sender, EventArgs e)
        {
            //suppose we run it in 2nd of April
            string strCmdText="";
            
            string strFrom="", strTo = "";
            strFrom=DateTime.Today.AddMonths(-1).ToString("01/MM/yyyy");//01/03/
             
            strTo = DateTime.Parse(DateTime.Today.ToString("01/MM/yyyy")).AddDays(-1).ToString("dd/MM/yyyy");//31/03
            switch (RadioButtonList1.SelectedValue)
            {
                case "1109":
                    strCmdText = "execute usher.CalcPrem_Coeff.Calc_Prem_NightLines (to_date('" + strFrom + "','dd/mm/yyyy'),to_date('" +
                        strTo + "','dd/mm/yyyy'))";
                    break;
                case "112":
                    strCmdText = "execute usher.usher_prem.main ('" + DateTime.Today.AddMonths(-1).ToString("yyyyMM00") + "','" + strFrom + "','" + strTo + "')";
                    break;
                case "1021":
                    strCmdText = "execute usher.CalcPrem_Coeff.calc_model('" + DateTime.Today.AddMonths(-1).ToString("yyyyMM00") + "','" + strFrom + "','" + strTo + "')";
                    break;
                case "114":
                    strCmdText = "execute usher.Calc_NewPrem.MonthlyPrem_ForushersByMinutes('" + strFrom + "','" + strTo + "')";
                    break;
                case "693":
                    strCmdText = "execute usher.CalcPrem_Coeff.Driver_Bonus('" + strFrom + "')";
                    break;
                case "48":
                    strCmdText = "execute usher.CalcPrem_Coeff.calc_extra_prem('" + strFrom + "','" + strTo + "')";
                    break;
                case "CalcPremMonthly":
                    strCmdText = "execute usher.Calc_NewPrem.CalcPrem_Monthly('" + strFrom + "','" + strTo + "')";
                    break;
                case "tagmulim":
                    strCmdText = "execute usher.CalcPrem_coeff.tagmulim('" + DateTime.Today.AddMonths(-1).ToString("yyyyMM") + "')";
                    break;
                case "86":
                    strCmdText = "execute synel.personalpremya.main('" + DateTime.Today.AddMonths(-1).ToString("yyyyMM") + "',1)";
                    break;
                case "689":
                    
                    strFrom = DateTime.Today.AddMonths(-2).ToString("01/MM/yyyy");//01/02/

                    strTo = DateTime.Parse(strFrom).AddMonths(1).AddDays(-1).ToString("dd/MM/yyyy");//28/02

                    strCmdText = "execute usher.CalcPrem_coeff.member_manco('" + strFrom + "','" + strTo + "')";
                    break;
            }
            
            using (OracleCommand cmd = new OracleCommand( strCmdText, oCon))
            {
                cmd.CommandType = CommandType.Text;                                        
                oCon.Open();
                cmd.ExecuteNonQuery();
                DisplayMessage();
            }
            //using (OracleCommand cmd = new OracleCommand("sp_Add_contact", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;

            //    //cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = "jj";

            //    cmd.Parameters.AddWithValue("@LastName", "ll").DbType = DbType.String;
            //    cmd.Parameters["@LastName"].Direction = ParameterDirection.Input;
            //    con.Open();
            //    cmd.ExecuteNonQuery();
            //}
            
        }
    }
}