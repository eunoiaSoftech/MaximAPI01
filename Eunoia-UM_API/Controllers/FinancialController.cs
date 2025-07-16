using Eunoia_UM_API.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Eunoia_UM_API.Model;
using Newtonsoft.Json;

namespace Eunoia_UM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialController : ControllerBase
    {
        Api_CommonResponse res = new Api_CommonResponse();
        [HttpPost]
        [Route("FinacialYear")]
        public Api_CommonResponse FinacialYear(FinancialYearConfigureModel FinancialYearConfig)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@dStartDate", FinancialYearConfig.StartDate);
                param[1] = new SqlParameter("@dEndDate", FinancialYearConfig.EndDate);
                param[2] = new SqlParameter("@id", FinancialYearConfig.Id);
                param[3] = new SqlParameter("@Flag", FinancialYearConfig.Flag);
                DataSet ds = DBOperation.FillDataSet("[dbo].[spFinancialMst]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    res.responseCode = 0;
                    res.message = ds.Tables[0].Rows[0]["Message"].ToString();
                    res.statusCode = Convert.ToInt32(ds.Tables[0].Rows[0]["StatusCode"]);
                }
                else
                {
                    res.responseCode = 1;
                    res.message = $"We are facing server issue at the moment please try again...";
                    res.statusCode = -1;
                    res.data = null;
                }
            }
            catch (Exception ex)
            {
                res.statusCode = 402;
            }
            return res;
        }
        [HttpGet]
        [Route("GetFinancialYear")]
        public Api_CommonResponse GetFinancialYear()
        {
            Api_CommonResponse response = new Api_CommonResponse();
            try
            {
                SqlParameter[] param = new SqlParameter[0];
                DataSet ds = DBOperation.FillDataSet("[dbo].[spFinancialMst]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    var data = ds.Tables[0];
                    var data1 = data.Rows.OfType<DataRow>()
                               .Select(row => data.Columns.OfType<DataColumn>()
                                .ToDictionary(col => col.ColumnName, c => row[c]));
                    response.data = JsonConvert.SerializeObject(data1);
                    response.statusCode = 200;
                }
            }
            catch (Exception ex)
            {
                response.statusCode = 402;
            }
            return response;
        }
        [HttpGet]
        [Route("EditFinancialYear")]
        public Api_CommonResponse EditFinancialYear(int Id)
        {
            Api_CommonResponse response = new Api_CommonResponse();
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@id", Id);
                param[1] = new SqlParameter("@Flag", "E");
                DataSet ds = DBOperation.FillDataSet("[dbo].[spFinancialMst]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    var data = ds.Tables[0];
                    var data1 = data.Rows.OfType<DataRow>()
                               .Select(row => data.Columns.OfType<DataColumn>()
                                .ToDictionary(col => col.ColumnName, c => row[c]));
                    response.data = JsonConvert.SerializeObject(data1);
                    response.statusCode = 200;
                }
            }
            catch (Exception ex)
            {
                response.statusCode = 402;
            }
            return response;
        }
        [HttpGet]
        [Route("DeleteFinancialYear")]
        public Api_CommonResponse DeleteFinancialYear(int Id)
        {
            Api_CommonResponse response = new Api_CommonResponse();
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@id", Id);
                param[1] = new SqlParameter("@Flag", "D");
                DataSet ds = DBOperation.FillDataSet("[dbo].[spFinancialMst]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    res.responseCode = 0;
                    res.message = ds.Tables[0].Rows[0]["Message"].ToString();
                    res.statusCode = Convert.ToInt32(ds.Tables[0].Rows[0]["StatusCode"]);
                }
                else
                {
                    res.responseCode = 1;
                    res.message = $"We are facing server issue at the moment please try again...";
                    res.statusCode = -1;
                    res.data = null;
                }
            }
            catch (Exception ex)
            {
                response.statusCode = 402;
            }
            return response;
        }
    }
}
