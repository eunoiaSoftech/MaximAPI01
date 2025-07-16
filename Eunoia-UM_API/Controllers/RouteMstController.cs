using Eunoia_UM_API.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using Eunoia_UM_API.Model;

namespace Eunoia_UM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteMstController : Controller
    {
        Api_CommonResponse res = new Api_CommonResponse();

        [HttpPost]
        [Route("RouteSave")]
        public Api_CommonResponse RouteSave(RouteMt route)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[12];
                //param[0] = new SqlParameter("@iPk_RouteID", route.iPk_RouteID);
                param[0] = new SqlParameter("@sRouteName", route.sRouteName);
                param[1] = new SqlParameter("@iFk_FrmStnId", route.iFk_FrmStnId);
                param[2] = new SqlParameter("@iFk_ToStnId", route.iFk_ToStnId);
                param[3] = new SqlParameter("@dTotalKMs", route.dTotalKMs);
                param[4] = new SqlParameter("@iFk_PreparedBy", route.iFk_PreparedBy);   
                param[5] = new SqlParameter("@iFk_CheckedBy", route.iFk_CheckedBy);
                param[6] = new SqlParameter("@iVoucherStyle", route.iVoucherStyle);
                param[7] = new SqlParameter("@iFk_BranchID", route.iFk_BranchID);
                param[8] = new SqlParameter("@iFk_YearID", route.iFk_BranchID);
                param[9] = new SqlParameter("@iFk_Approvedby", route.iFk_Approvedby);
                param[10] = new SqlParameter("@iTransitTime", route.iTransitTime);
                param[11] = new SqlParameter("@sShortRoute", route.sShortRoute);
                param[11] = new SqlParameter("@iPk_RouteID", route.iPk_RouteID);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_MasterROUTEMST_Save]", param);
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
            catch (Exception e)
            {
                throw e;
            }
            return res;
        }

        [HttpGet]
        [Route("GetCity")]
        public Api_CommonResponse GetCity()
        {
            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_MASTER_Route_GetCities]");
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    res.statusCode = 1;
                    res.responseCode = 0;
                    res.message = "Data Fetched Successfully...";
                    res.data = JsonConvert.SerializeObject(ds.Tables[0]);
                }
                else
                {
                    res.statusCode = 1;
                    res.responseCode = 0;
                    res.message = "No Data Available...";
                }
            }
            catch (Exception e)
            {
                res.statusCode = 0;
                res.responseCode = 0;
                res.message = e.Message;
            }
            return res;
        }
        [HttpGet]
        [Route("GetRouteList")]
        public Api_CommonResponse GetRouteList()
        {
            try
            {
                SqlParameter[] param = new SqlParameter[0];
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_MasterRoute_List]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    var data = ds.Tables[0];

                    res.data = JsonConvert.SerializeObject(data);
                    res.statusCode = 200;
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
    

