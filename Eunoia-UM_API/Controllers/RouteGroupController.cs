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
    public class RouteGroupController : Controller
    {
        Api_CommonResponse response = new Api_CommonResponse();

        [HttpPost]
        [Route("AddRouteGroup")]
        public Api_CommonResponse SaveRouteGroup(RouteGroupModel request)
        {
            try
            {
                string routeIds = (request.SelectedRouteIDs != null && request.SelectedRouteIDs.Count > 0)
                    ? string.Join(",", request.SelectedRouteIDs)
                    : string.Empty;

                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@iGroupId", request.iGroupId);
                param[1] = new SqlParameter("@sEntryNo", request.sEntryNo ?? string.Empty);
                param[2] = new SqlParameter("@dtEntryDate", request.dtEntryDate);
                param[3] = new SqlParameter("@sGroupName", request.sGroupName ?? string.Empty);
                param[4] = new SqlParameter("@RouteIDs", routeIds);

                DataSet ds = DBOperation.FillDataSet("USP_SaveRouteGroupInfo", param);

                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    response.responseCode = 0;
                    response.message = ds.Tables[0].Rows[0]["Message"].ToString();
                    response.statusCode = Convert.ToInt32(ds.Tables[0].Rows[0]["StatusCode"]);
                }
                else
                {
                    response.responseCode = 1;
                    response.message = $"We are facing server issue at the moment please try again...";
                    response.statusCode = -1;
                    response.data = null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }

    }
}
