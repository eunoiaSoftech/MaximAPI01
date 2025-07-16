using Eunoia_UM_API.Helper;
using Eunoia_UM_API.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace Eunoia_UM_API.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class Api : Controller
    {

        Api_CommonResponse api_Response = new Api_CommonResponse();

        [HttpGet]
        [Route("TallyexportTripExp")]
        public Api_CommonResponse TallyexportTripExp(string sFormdate, string sTodate)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@fromdate", sFormdate);
                param[1] = new SqlParameter("@todate", sTodate);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Accounts_TalleyExport_TripExpense]", param);
                                                             
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<Dictionary<string, object>> dataList = new List<Dictionary<string, object>>();
                    if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                    {
                        foreach (DataTable table in ds.Tables)
                        {
                            foreach (DataRow row in table.Rows)
                            {
                                var dataRow = new Dictionary<string, object>();
                                foreach (DataColumn col in table.Columns)
                                {
                                    dataRow[col.ColumnName] = row[col];
                                }
                                dataList.Add(dataRow);
                            }
                        }
                    }

                    api_Response.responseCode = dataList.Count > 0 ? 0 : 1;
                    api_Response.message = api_Response.responseCode == 0 ? "Talley export tripexp" : "Details Not Found...";
                    api_Response.statusCode = api_Response.responseCode == 0 ? 1 : -1;
                    api_Response.data = api_Response.responseCode == 0 ? dataList : null;
                }
                else
                {
                    api_Response.responseCode = 1;
                    api_Response.message = "Details Not Found...";
                    api_Response.statusCode = -1;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return api_Response;
        }

        [HttpGet]
        [Route("TallyexportTripData")]
        public Api_CommonResponse TallyexportTripData(string sFormdate, string sTodate)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@fromdate", sFormdate);
                param[1] = new SqlParameter("@todate", sTodate);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Operation_FreightIncome_Export]", param);


                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<Dictionary<string, object>> dataList = new List<Dictionary<string, object>>();
                    if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                    {
                        foreach (DataTable table in ds.Tables)
                        {
                            foreach (DataRow row in table.Rows)
                            {
                                var dataRow = new Dictionary<string, object>();
                                foreach (DataColumn col in table.Columns)
                                {
                                    dataRow[col.ColumnName] = row[col];
                                }
                                dataList.Add(dataRow);
                            }
                        }
                    }

                    api_Response.responseCode = dataList.Count > 0 ? 0 : 1;
                    api_Response.message = api_Response.responseCode == 0 ? "Talley export FrtIncome" : "Details Not Found...";
                    api_Response.statusCode = api_Response.responseCode == 0 ? 1 : -1;
                    api_Response.data = api_Response.responseCode == 0 ? dataList : null;
                }
                else
                {
                    api_Response.responseCode = 1;
                    api_Response.message = "Details Not Found...";
                    api_Response.statusCode = -1;
                }


            }
            catch (Exception e)
            {
                throw e;
            }
            return api_Response;
        }

        [HttpGet]
        [Route("LegDataforGPS")]
        public Api_CommonResponse LegDataforGPS(string sFormdate, string sTodate)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@fromdate", sFormdate);
                param[1] = new SqlParameter("@todate", sTodate);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Operation_GPS_LegData]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.responseCode = 1;
                    api_Response.message = "Synch Successfully";
                    api_Response.data = JsonConvert.SerializeObject(ds.Tables[0]);
                    api_Response.statusCode = 200;
                }
                else
                {
                    api_Response.responseCode = 1;
                    api_Response.message = "Details Not Found...";
                    api_Response.statusCode = -1;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return api_Response;
        }
    }
}
