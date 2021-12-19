using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using System.Configuration;
using System.Data.OracleClient;
using System.Text;
using System.IO;
using ExcelDataReader;

namespace Tyuta
{
    public partial class Content1 : System.Web.UI.Page
    {
        DataSet ds = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["ConnectionString"]= ConfigurationManager.ConnectionStrings["hr"].ToString();
                InitLUT();
            }

        }
        private void InitLUT()
        {
            
            OracleConnection con = new OracleConnection(Session["ConnectionString"].ToString());
            OracleCommandBuilder build;
            string sql = "select * from CODES WHERE TABLECODE='TrainingType' Or TABLECODE='CourseCode'";
            OracleCommand cmd = new OracleCommand(sql, con);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.Fill(ds);
            Session["CODES"] = ds.Tables[0];
        }
        private void tryme()
        {

            return;
            
            string conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source = " + FileUpload1.PostedFile.FileName + ";Extended Properties = Excel 12.0;";


            OleDbConnection connExcel =
    new OleDbConnection(
        @"Provider=Microsoft.Jet.OLEDB.8.0;Data Source=" + FileUpload1.PostedFile.FileName + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=0;';");      

            OleDbCommand cmdExcel = new OleDbCommand();

            OleDbDataAdapter oda = new OleDbDataAdapter();

            DataTable dt = new DataTable();

            cmdExcel.Connection = connExcel;


            //Get the name of First Sheet

            connExcel.Open();

            DataTable dtExcelSchema;

            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();

            connExcel.Close();



            //Read Data from First Sheet

            connExcel.Open();

            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";

            oda.SelectCommand = cmdExcel;

            oda.Fill(dt);

            connExcel.Close();




        }

        public  void ReadBase(string PathToBase)
        {
            //unused!!!
            //  if (ImpersonateValidUser(User, Password, Domain))
            // {
            try
            {
              //  PathToBase = PathToBase.Replace(@"\\", @"\");
                PathToBase = @"C:\test\Processed\TrainingsTest4.xlsx";
                string FileDirPass = "c:\\uploads\\miki.xlsx";

                FileUpload1.PostedFile.SaveAs(FileDirPass);
                
                string filePath = FileDirPass;
                FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
                IExcelDataReader excelReader;

                DataSet dataSet;
                if (Path.GetExtension(filePath).ToUpper() == ".XLS")
                {
                    excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                    dataSet = excelReader.AsDataSet(new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = false
                        }
                    });
                }
                else
                {
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    dataSet = excelReader.AsDataSet();
                }

                if (dataSet.Tables.Count > 0)
                {
                    //     RevertToSelf();
                    //     ClassData.InsertBase(CreateDataTableBase(dataSet.Tables[0]));
                }
            }
            catch (Exception ex)
            {
                //  RevertToSelf();
                //   ClassLog.WriteToLog(ex.ToString());
            }
            finally
            {
                //  RevertToSelf();
            }
            //   } 
        }

        private void ReadExcelFile()

        {
            DataSet ds = new DataSet();
            int counter = 1;
            try
            {

                var headers = new List<string>();
                //delete existing file
                
                //if (File.Exists("c:\\uploads\\miki.xlsx")) File.Delete("c:\\uploads\\miki.xlsx");
                //end appendix
                
                string FileDirPass = "c:\\uploads\\input"+ DateTime.Now.ToString("ddmmyy hhmmss") + ".xlsx";

                FileUpload1.PostedFile.SaveAs(FileDirPass);

                string filePath = FileDirPass;
                FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
                IExcelDataReader excelReader;

                excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);                                                            

                ds = excelReader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow=true,
                        FilterColumn = (rowReader, columnIndex) => {
                            //var s = rowReader.GetOrdinal("ColA");  //not supported
                            

                            return true;
                        }

                        //ReadHeaderRow = rowReader => {
                        //    for (var i = 0; i < rowReader.FieldCount; i++)
                        //        headers.Add(Convert.ToString(rowReader.GetValue(i)));
                        //},

                        //FilterColumn = (columnReader, columnIndex) =>
                        //    headers.IndexOf("string") != columnIndex


                    }
                });

                
                ds.Tables[0].Columns.Add("ErrDesc");
                ds.Tables[0].Columns.Add("CourseCode");
                ds.Tables[0].Columns.Add("TrainingCode");
                ds.Tables[0].Columns.Add("dan_id");
                ds.Tables[0].Columns.Add("StartCourse");
                ds.Tables[0].Columns.Add("EndCourse");
                
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    counter += 1;
                    string stam;
                    stam = DateTime.Parse(dr["תחילת קורס"].ToString().Trim()).ToString("dd/MM/yyyy 0:00:00");
                    //dr["תחילת קורס"] = DateTime.Parse(stam);

                    stam = DateTime.Parse(dr["סיום קורס"].ToString().Trim()).ToString("dd/MM/yyyy 0:00:00");
                    //dr["סיום קורס"] = DateTime.Parse(stam);


                    
                    if (dr["תחילת קורס"].ToString().Trim() != "") dr["StartCourse"] = DateTime.Parse(dr["תחילת קורס"].ToString().Trim()).ToString("dd/MM/yyyy");
                    //dr["תחילת קורס"] = dr["StartCourse"];
                    if (dr["סיום קורס"].ToString().Trim() != "") dr["EndCourse"] = DateTime.Parse(dr["סיום קורס"].ToString().Trim()).ToString("dd/MM/yyyy");
                    //dr["סיום קורס"] = dr["EndCourse"];

                    if (counter == 2048)
                    {
                        string k = "po";
                    }
                    //Console.WriteLine(counter);
                }
                counter = -1;
                ds.Tables[0].Columns.Remove("תחילת קורס");
                ds.Tables[0].Columns.Remove("סיום קורס");

                ds.Tables[0].AcceptChanges();
                Session["InputFile"] = ds.Tables[0];
                GridView1.DataSource = Session["InputFile"];
                GridView1.DataBind();
                SetHeaders();
                cmdInput.Enabled = true;
                
                
            }
            catch (Exception ex)
            {
                string msg= " שורה " +counter + " שגויה " ;
                if (counter>-1)
                    msg = " שורה " + counter + " שגויה " ;
                else
                    msg = "שורות תקינות אנא וודא שהעמודות מעוצבות לפי סוג התוכן";
                RegisterClientScriptBlock("alert", "<script>alert('"+msg+"')</script>");
                //RegisterClientScriptBlock("alert", "<script>alert('mikikumi'" + ex.Message + ")</script>");
                //throw ex;
            }
        }

        private void orgReadExcelFile()

        {
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source = " + FileUpload1.PostedFile.FileName + ";Extended Properties = Excel 8.0;";
            string stam = "C:\\inetpub\\wwwroot\\Training\\bin\\file7.xlsx";
            //strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source = " + stam + ";Extended Properties = Excel 8.0;";
            try
            {

                //dima:ReadBase(FileUpload1.PostedFile.FileName);
                



                //end dima




                //FileUpload1.PostedFile.SaveAs("c:\\uploads\\miki.xlsx");
                //stam = "C:\\uploads\\miki.xlsx";
                //strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source = " + stam + ";Extended Properties = Excel 8.0;";
                // strConn = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Mode=Share Deny None;Data Source={0};user id=Admin;password=;", stam);
                OleDbConnection conn = new OleDbConnection(strConn);              


                DataSet ds = new DataSet();
                string strExcel = "";
                OleDbDataAdapter myCommand = null;                
               

                //System.Diagnostics.Process newProcess;

                
                //newProcess = System.Diagnostics.Process.Start("notepad.exe");
                
                //newProcess = System.Diagnostics.Process.Start("C:\\inetpub\\wwwroot\\Training\\bin\\file7.xlsx");//newProcess = System.Diagnostics.Process.Start(FileUpload1.PostedFile.FileName);                
                //not showing:Console.WriteLine(FileUpload1.PostedFile.FileName + " SHALOM!!!");
                //string sfullpath = System.IO.Path.Combine(Server.MapPath("~/upload/"), FileUpload1.FileName);
                
                //not returning RegisterClientScriptBlock("alert", "<script>alert('" + Server.MapPath("~/upload/") + "')</script>");

                
                //if (System.IO.File.Exists("c:\\uploads\\miki.xlsx"))


                //    RegisterClientScriptBlock("alert", "<script>alert('EXISTS!!')</script>"); 
                //else
                //    RegisterClientScriptBlock("alert", "<script>alert(' NOT EXISTS!!')</script>");

                

                

                strExcel = "Select * from [sheet1$]";
                myCommand = new OleDbDataAdapter(strExcel, strConn);
                myCommand.Fill(ds, "table1");
//return;
                //how to delete empty rows:
                //we could specify on workernumber only!!!DataTable n = ds.Tables[0].Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is DBNull || string.IsNullOrWhiteSpace(field as string))).CopyToDataTable();
                //Session["InputFile"] = ds.Tables[0].Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is DBNull || string.IsNullOrWhiteSpace(field as string))).CopyToDataTable();
                

                ds.Tables[0].Columns.Add("ErrDesc");
                ds.Tables[0].Columns.Add("CourseCode");
                ds.Tables[0].Columns.Add("TrainingCode");
                ds.Tables[0].Columns.Add("dan_id");
                ds.Tables[0].Columns.Add("StartCourse");
                ds.Tables[0].Columns.Add("EndCourse");

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["תחילת קורס"].ToString() != "") dr["StartCourse"] = ((DateTime)dr["תחילת קורס"]).ToString("dd/MM/yyyy");
                    if (dr["סיום קורס"].ToString() != "") dr["EndCourse"] = ((DateTime)dr["סיום קורס"]).ToString("dd/MM/yyyy");
                }

                ds.Tables[0].AcceptChanges();
                Session["InputFile"] = ds.Tables[0];
                GridView1.DataSource = Session["InputFile"];
                GridView1.DataBind();
                SetHeaders();
                cmdInput.Enabled = true;
                conn.Close();
                //if (newProcess != null) newProcess.Kill();
            }
            catch (Exception ex)
            {
                RegisterClientScriptBlock("alert", "<script>alert('mikikumi'"+ ex.Message  +")</script>");
                throw ex;
            }
        }

        protected void cmdReadExcel_Click(object sender, EventArgs e)
        {
            if (FileUpload1.PostedFile.FileName=="")
            
                RegisterClientScriptBlock("alert", "<script>alert('יש לבחור קובץ לקליטה')</script>");

            else
            {
                ReadExcelFile();
                StringBuilder sb = new StringBuilder();
                sb.Append(@"PrintRecsCount("+GridView1.Rows.Count +");");              


                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "KlitaFileMissing", sb.ToString(), true);
            }
        }
        private bool IsEmptyValue(string ValToCheck)
        {
            if (ValToCheck == "")
                return true;
            return false;
        }
        private bool IsNumberValue(string ValToCheck)
        {
            decimal number1 = 0;
            if (!(decimal.TryParse(ValToCheck, out number1)))
                return false;
            return true;
        }
        private bool IsDateValue(string ValToCheck)
        {
            DateTime temp;
            if (!(DateTime.TryParse(ValToCheck, out temp))) return false;
            return true;
        }
        private bool IsValidFile()
        {
            DataTable dt = (DataTable)Session["InputFile"];
            bool blnValid = true;
            for(int iRow=0;iRow<=dt.Rows.Count-1;iRow++)
            {
                string sErrDesc="";
                string fldVal="";
                 
                 
                
                for (int iCol=0;iCol<= dt.Rows[iRow].Table.Columns.Count - 1;iCol++)
                {
                    string sFldName = dt.Rows[iRow].Table.Columns[iCol].ColumnName;
                    
                    fldVal = dt.Rows[iRow][sFldName].ToString();
                    
                    switch (sFldName.Trim())
                    {
                        case "מא":
                        case "סוג ההדרכה":
                        case "ספק הקורס":
                        case "שעות בפועל":
                        case "תחילת קורס":
                        case "סיום קורס":
                        case "StartCourse":
                        case "EndCourse":
                            if (IsEmptyValue(fldVal)) sErrDesc += sFldName + "  איננו מאותחל" + Environment.NewLine;                             
                            break;
                    }
                    switch (sFldName.Trim())
                    {
                        case "מא":
                        case "שעות בפועל":
                            if (!IsNumberValue(fldVal)) sErrDesc += sFldName + "  צריך להיות מספר" + Environment.NewLine;
                            break;
                    }
                    switch (sFldName.Trim())
                    {
                        case "תחילת קורס":
                        case "סיום קורס":
                        case "StartCourse":
                        case "EndCourse":

                            if (!IsDateValue(fldVal)) sErrDesc += sFldName + "  צריך להיות תאריך" + Environment.NewLine;
                            break;
                    }
                    if (sFldName.Trim()=="סוג ההדרכה")
                    {
                        DataRow[] drFind = ((DataTable)Session["CODES"]).Select("TABLECODE='CourseCode' and DESCRIPTION='" + fldVal + "'");
                        if (drFind.Length == 0)
                            sErrDesc += "לא קיימת הדרכה מסוג זה" + Environment.NewLine;
                        else
                            dt.Rows[iRow]["CourseCode"] = drFind[0]["CODE"];
                    }
                    if (sFldName.Trim() == "קוד קורס")
                    {
                        if (fldVal == "") fldVal = "0";

                        DataRow[] drFind = ((DataTable)Session["CODES"]).Select("TABLECODE='TrainingType' and MZAA_CODE=" + fldVal );
                        if (drFind.Length == 0)
                            sErrDesc += "לא קיימת השתלמות מסוג זה" + Environment.NewLine;//TO DO: CREATE A PAGE TO INSERT A ROW
                        else
                            dt.Rows[iRow]["TrainingCode"] = drFind[0]["CODE"];
                    }
                    if(sFldName.Trim() == "מא"){
                        string dan_id = GetDanId(fldVal);
                        if (dan_id == "")
                            sErrDesc += "לא נמצא מ.א" + Environment.NewLine; 
                        else
                            dt.Rows[iRow]["dan_id"] = dan_id;
                    }

                }
                

                if (sErrDesc!="")//bad value found
                {
                    dt.Rows[iRow]["ErrDesc"] = sErrDesc;
                    blnValid = false;//enough 1 mistake and the klita is aborted
                }
                else
                {
                    dt.Rows[iRow]["ErrDesc"] = "";
                }
            }
            //dialog box 2 confirm action-in html code!!!           
            
            Session["InputFile"] = dt;
            return blnValid;
        }
        private void SetHeaders()
        {
            //GridView1.HeaderRow.Cells[0].Text = "ת.ז";
            //GridView1.HeaderRow.Cells[1].Text = "משפחה";
            //GridView1.HeaderRow.Cells[2].Text = "פרטי";
            //GridView1.HeaderRow.Cells[3].Text = "תיאור שגיאה";
            GridView1.HeaderRow.ForeColor = System.Drawing.Color.RoyalBlue;
        }
        protected void cmdInput_Click(object sender, EventArgs e)
        {

            if (GridView1.Rows.Count == 0)
            {                                                                      
                RegisterClientScriptBlock("alert", "<script>alert('יש לבחור קובץ לקליטה')</script>");
            }

            else
            {
                bool isValidFile = IsValidFile();

                if (isValidFile)
                    InsertExcelFile();
                else
                {
                    GridView1.DataSource = Session["InputFile"];
                    GridView1.DataBind();
                    SetHeaders();
                    
                    for (int irow = 0; irow <= ((DataTable)GridView1.DataSource).Rows.Count - 1; irow++)
                    {
                        if (((DataTable)GridView1.DataSource).Rows[irow]["ErrDesc"] != "") GridView1.Rows[irow].ForeColor = System.Drawing.Color.Red;
                        
                    }
                    RegisterClientScriptBlock("alert", "<script>alert('ראה רשומות שגויות בקובץ')</script>");

                }                                

            }
        }
        
        private void  InsertExcelFile()
        {
            int counter = 1;
            try
            {
            

            string ReadConnectionString = ConfigurationManager.ConnectionStrings["hr"].ToString();
            DataSet ds = new DataSet();
             
            OracleConnection con = new OracleConnection(ReadConnectionString);
            OracleCommandBuilder build;
            string sql = "Select * from TRAINING where 1=-1";
            OracleCommand cmd = new OracleCommand(sql, con);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.Fill(ds);                                  

            DataTable dt = (DataTable)Session["InputFile"];

            foreach (DataRow drTmp in dt.Rows)
            {
                
                DataRow drNew = ds.Tables[0].NewRow();                
                
                string TrainingCode = drTmp["TrainingCode"].ToString();//CHAR CODE OF col e                                               
                
                drNew["INTERNALSYSTEMID"] = "DANHR"+ drTmp["dan_id"];
                drNew["DAN_ID"] = drTmp["dan_id"];
                drNew["TRAININGTYPE"] = TrainingCode;
                drNew["COURSECODE"] =drTmp["CourseCode"];
                drNew["COURSEVENDOR"] = drTmp["ספק הקורס"];
                drNew["PAYMENTTYPE"] =0;
                    drNew["COURSESTARTDATE"] = drTmp["startCourse"];//drTmp["תחילת קורס"];
                drNew["COURSEENDDATE"] = drTmp["endCourse"];//drTmp["סיום קורס"];                
                    drNew["ACTUALCOST"] = 0;
                drNew["REMARK"] = "";// drTmp["הערה"];
                drNew["ACTUALHOURS"] = drTmp["שעות בפועל"];
                drNew["MODIFICATIONUSER"] = Environment.UserName;
                drNew["MODIFICATIONDATE"] = DateTime.Now;
                drNew["CREATIONUSER"] = Environment.UserName;
                drNew["CREATIONDATE"] = DateTime.Now;

                ds.Tables[0].Rows.Add(drNew);
                counter += 1;


            }
           
            build = new OracleCommandBuilder(da);
            da.Update(ds.Tables[0]);

            RegisterClientScriptBlock("finished", "<script>alert('קובץ ניקלט בהצלחה')</script>");

            

            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "<script>PrintRecsCount(0)</script>", false);


            GridView1.DataSource = null;
            GridView1.DataBind();
            SetHeaders();
            cmdInput.Enabled = false;
            }
            catch (Exception ex)
            {
                string f= (ex.Message);
            }

        }
        private string GetDanId(string WORKER_NUMBER)
        {
            char pad = '0';
            string sDanId = "";
            string sWorkerNumber;
            int iWorkerNumber= Int32.Parse(WORKER_NUMBER);  
            
            if (iWorkerNumber < 4000)
                sWorkerNumber = WORKER_NUMBER.PadLeft(5, pad);
            else
                sWorkerNumber = WORKER_NUMBER;

            DataSet dsWorkers = new DataSet();

            OracleConnection con = new OracleConnection(Session["ConnectionString"].ToString());
            OracleCommandBuilder build;
            string sql = "Select * from WORKERPERSONALDETAILS where WORKERNUMBER='"+ sWorkerNumber + "'";
            OracleCommand cmd = new OracleCommand(sql, con);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.Fill(dsWorkers);
            if (dsWorkers.Tables[0].Rows.Count>0) sDanId = dsWorkers.Tables[0].Rows[0]["DAN_ID"].ToString();
            return sDanId;
        }
        

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //return;
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "מס' אישי";
                e.Row.Cells[12].Text = "תאריך התחלה";
                e.Row.Cells[13].Text = "תאריך סיום";
                e.Row.Cells[8].Text = "תיאור שגיאה";
            }


            for (int iCell = 0; iCell <= e.Row.Cells.Count - 1; iCell++)
            {
                e.Row.Cells[iCell].Width = 0;
                e.Row.Cells[iCell].Visible = false;
            }

            for (int iCell = 0; iCell <= 7; iCell++)
            {
                e.Row.Cells[iCell].Width = 100;
                e.Row.Cells[iCell].Visible = true;
            }

            for (int iCell = 12; iCell <= 13; iCell++)
            {
                e.Row.Cells[iCell].Width = 100;
                e.Row.Cells[iCell].Visible = true;
            }

            e.Row.Cells[8].Width = 300;
            e.Row.Cells[8].Visible = true;
        }

        protected void Unnamed1_Click(object sender, EventArgs e)
        {
            while(true)
            {

            } ;
        }
    }
}