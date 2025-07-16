using Eunoia_UM_API.Helper;
using Eunoia_UM_API.Model;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;

namespace Eunoia_UM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OverloadExpController : Controller
    {
        Api_CommonResponse res = new Api_CommonResponse();

        [HttpPost]
        [Route("OverloadExpensesSav")]

        public Api_CommonResponse OverloadExpensesSave(OverloadE OverloadExp)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@iFk_RouteId", OverloadExp.iFk_RouteId);
                param[1] = new SqlParameter("@iFk_VehTypId", OverloadExp.iFk_VehTypId);
                param[2] = new SqlParameter("@dFromWt", OverloadExp.dFromWt);
                param[3] = new SqlParameter("@dToWt", OverloadExp.dToWt);
                param[4] = new SqlParameter("@iFk_UomId", OverloadExp.iFk_UomId);
                param[5] = new SqlParameter("@dQty", OverloadExp.dQty);
                param[6] = new SqlParameter("@dAmount", OverloadExp.dAmount);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_OVRLOADMST_Save]", param);
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
        [Route("GetOverloadExpenseList")]
        public Api_CommonResponse GetOverloadExpenseList()
        {
            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_GetOverloadExp_Get]");
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


    

