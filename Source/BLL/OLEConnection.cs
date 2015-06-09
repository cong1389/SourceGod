using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//using System.Data.SqlClient;
//using System.Data.Odbc;
using System.Data.Odbc;
using Cb.DBUtility;

namespace Cb.BLL
{
    public class Connection
    {
        public static DateTime dateNgayXem;
        public static string strRef;

        public static DataSet GetKQBenhNhanMaster(string strRef)
        {
            Write2Log.WriteLogs("Data1", "pageSize", "1");
            string query = string.Format(@"SELECT DISTINCT REQUESTS.LOCCODE, REQUESTS.ACCESSNUMBER,CONVERT(VARCHAR(10),REQUESTS.COLLECTIONDATE,103) AS COLECTIONDATE, PATIENTS.SEX, CONVERT(VARCHAR(10),PATIENTS.BIRTHDATE,103) AS BIRTH_DATE, PATIENTS.NAME + ' ' + PATIENTS.FIRSTNAME AS TEN
                            FROM PATIENTS INNER JOIN REQUESTS ON PATIENTS.PATNUMBER = REQUESTS.PATNUMBER
                            WHERE (((REQUESTS.ACCESSNUMBER)='{0}'))", strRef.ToUpper());

            return GetData(query);
        }

        public static DataSet GetKQBenhNhanDetail(string strRef)
        {
            Write2Log.WriteLogs("Data2", "pageSize", "2");
            string query =
                string.Format(@"SELECT '' AS STT, '' AS THANG_DOI_CHIEU, TESTS.DEPTH, DICT_TESTS.TESTTEXT AS TEST_TEXT, TESTS.RESULT AS RESULTS, DICT_TESTS.UNITS, TESTS.MINIMUM, TESTS.MAXIMUM
                        FROM (REQUESTS INNER JOIN TESTS ON REQUESTS.ACCESSNUMBER = TESTS.ACCESSNUMBER) INNER JOIN DICT_TESTS ON TESTS.TESTCODE = DICT_TESTS.TESTCODE
                        WHERE (((REQUESTS.ACCESSNUMBER)='{0}') AND ((TESTS.NOTPRINTABLE) Is Null))
                        ORDER BY TESTS.TESTORDER", strRef.ToUpper());
            return GetData(query);
        }
        public static DataSet GetKQDonVi(string strLocCode, string ngayXem)
        {
            Write2Log.WriteLogs("Data3", "pageSize", "3");
            String query = string.Format(@"SELECT '' AS STT, REQUESTS.ACCESSNUMBER, PATIENTS.NAME + ' ' + PATIENTS.FIRSTNAME AS TEN_BENH_NHAN, CONVERT(VARCHAR(10),PATIENTS.BIRTHDATE,103) AS BIRTH_DATE
                            FROM PATIENTS INNER JOIN REQUESTS ON PATIENTS.PATNUMBER = REQUESTS.PATNUMBER
                            WHERE   REQUESTS.COLLECTIONDATE > convert(datetime,'{0}',103) AND 
                                    REQUESTS.COLLECTIONDATE < dateadd(DAY, 1, convert(datetime,'{0}',103)) AND REQUESTS.LOCCODE='{2}'
                            ORDER BY (PATIENTS.FIRSTNAME + ' ' + PATIENTS.NAME)", ngayXem, ngayXem, strLocCode);
            return GetData(query);
        }

        //#region OLEDB
        public static OdbcConnection con = new OdbcConnection(ConfigurationManager.ConnectionStrings["dc"].ConnectionString);
        private static DataSet GetData(string queryString)
        {
            Write2Log.WriteLogs("Data", "pageSize", "4");
            try
            {
                OdbcCommand oCommand = new OdbcCommand(queryString, con);
                OdbcDataAdapter oAdapter = new OdbcDataAdapter();
                oAdapter.SelectCommand = oCommand;
                DataSet oDataSet = new DataSet();
                if (con.State != ConnectionState.Open) con.Open();
                Write2Log.WriteLogs(con.State.ToString(), "pageSize", "5");
                oAdapter.Fill(oDataSet);
                return oDataSet;
            }
            catch (Exception Ex)
            {
                Write2Log.WriteLogs("fail", "pageSize", "6");
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            return null;
        }
        //#endregion

        #region ODBC
        //public static OdbcConnection con = new OdbcConnection(ConfigurationManager.ConnectionStrings["dc"].ConnectionString);
        //public static OdbcConnection con = new OdbcConnection("DSN = tdquery_DB");
        //private static DataSet GetData(string queryString)
        //{
        //    try
        //    {
        //        OdbcCommand oCommand = new OdbcCommand(queryString, con);
        //        OdbcDataAdapter oAdapter = new OdbcDataAdapter();
        //        oAdapter.SelectCommand = oCommand;
        //        DataSet oDataSet = new DataSet();
        //        if (con.State != ConnectionState.Open) con.Open();
        //        oAdapter.Fill(oDataSet);

        //        return oDataSet;
        //    }
        //    catch (Exception Ex)
        //    {
        //        throw Ex;
        //    }
        //    finally
        //    {
        //        if (con.State != ConnectionState.Closed)
        //            con.Close();
        //    }
        //    return null;
        //}
        #endregion
        /// <summary>
        /// gets Test_Text
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetTest_Text(string s)
        {
            return s;
        }
        /// <summary>
        ///gets Results
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetResults(string s)
        {
            switch (s)
            {
                case "ABCES":
                    return "Váp xe";
                case "CUTAN":
                    return "Tổn thương ngoài da";
                case "GORGE":
                    return "Họng";
                case "HEMOC":
                    return "Cấy máu";
                case "LCR":
                    return "Dịch não tủy";
                case "NEO":
                    return "Mũi";
                case "EOI0":
                    return "Mắt";
                case "ORE0":
                    return "Tai";
                case "PERIC":
                    return "Dịch màn tim";
                case "PERIT":
                    return "Dịch màn bụng";
                case "PLEU":
                    return "Dịch màn phổi";
                case "PUS":
                    return "Mủ";
                case "SKIN":
                    return "Da";
                case "SPER":
                    return "Tinh dịch";
                case "UERTH":
                    return "Niệu đạo";
                case "URINE":
                    return "Nước tiểu";
                case "VAGIN":
                    return "Âm đạo";
                case "PO":
                    return "Dương";
                case "NE":
                    return "Âm";
                case "IP":
                case "TPP":
                case "BP":
                case "DBP":
                case "P":
                case "POS":
                case "PV":
                case "POSSP":
                case "HP":
                case "PR":
                    return "Dương tính";
                case "IN":
                case "TBN":
                case "BN":
                case "DBN":
                case "N":
                case "NEGSP":
                case "NEG":
                case "NV":
                case "NEV":
                case "HN":
                    return "¢m tÝnh";
                case "IG":
                case "TPD":
                case "ID":
                    return "Nghi ngờ";
                case "C":
                case "C90":
                    return "Trong";
                case "0":
                    return "Không thấy";
                case "1":
                    return "Có rất ít";
                case "2":
                    return "";
                case "ST":
                    return "Không mọc";
                case "FCN":
                    return "Vi trùng cộng sinh bình thường";
                case "FCA":
                    return "Không có vi khuẩn cộng sinh";
                case "A20":
                    return "Phân mềm";
                case "C20":
                    return "Vàng nâu";
                case "292":
                    return "Nuôi cấy âm tính";
                default:
                    return "Nuôi cấy âm tính trên môi trường Sabouraud";
            }
        }
    }
}
