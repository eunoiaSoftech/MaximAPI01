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
    public class TripExpController : Controller
    {
        Api_CommonResponse api_Response = new Api_CommonResponse();

        [HttpGet]
        [Route("GetRouteMaster")]
        public Api_CommonResponse GetRouteMaster()
        {
            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Get_Route]");
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Route List";
                    api_Response.statusCode = 1;
                    api_Response.data = JsonConvert.SerializeObject(ds.Tables[0]);
                    api_Response.responseCode = 1;
                }
                else
                {
                    api_Response.message = "No Data Available";
                    api_Response.statusCode = 1;
                    api_Response.responseCode = 1;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return api_Response;
        }

        [HttpGet]
        [Route("GetRoutevalues")]
        public Api_CommonResponse GetRoutevalues()
        {
            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_MasterGetRouteinfo_Get]");
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Route List";
                    api_Response.statusCode = 1;
                    api_Response.data = JsonConvert.SerializeObject(ds.Tables[0]);
                    api_Response.responseCode = 1;
                }
                else
                {
                    api_Response.message = "No Data Available";
                    api_Response.statusCode = 1;
                    api_Response.responseCode = 1;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return api_Response;
        }

        [HttpPost]
        [Route("DeleteListItem")]
        public Api_CommonResponse DeleteListItem(MFinancialYear trip)
        {
            Api_CommonResponse res = new Api_CommonResponse();
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@iPk_TrpexpmstId", trip.iPk_TrpexpmstId);
                DataSet ds = DBOperation.FillDataSet("USP_Master_TripExpense_Delete", param);
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


    }
}
