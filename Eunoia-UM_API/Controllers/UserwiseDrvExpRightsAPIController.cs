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
    public class UserwiseDrvExpRightsAPIController: Controller
    {
        Api_CommonResponse response = new Api_CommonResponse();

        [HttpPost]
        [Route("SaveAllDrvExp")]
        public Api_CommonResponse SaveUserwiseDrvExpRights([FromBody] UserwiseDrvExpRightsModel request)
        {
            try
            {

                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@iId", request.iId);
                param[1] = new SqlParameter("@iEntryNo", request.iEntryNo);
                param[2] = new SqlParameter("@dtEntryDate", request.dtEntryDate);
                param[3] = new SqlParameter("@iExpenseHeadId", request.iExpenseHeadId);
                param[4] = new SqlParameter("@iUserId", request.iUserId);
                param[5] = new SqlParameter("@sRangeFrom", request.sRangeFrom);
                param[6] = new SqlParameter("@sRangeTo", request.sRangeTo);
                param[7] = new SqlParameter("@sApprovalLevel", request.sApprovalLevel);


                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Save_UserwiseDrvExpRights]", param);
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
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601) // Unique constraint violation
                {
                    response.responseCode = 1;
                    response.message = "Entry No already exists. Please enter a unique Entry No.";
                    response.statusCode = 409;
                }
                else
                {
                    response.responseCode = 1;
                    response.message = "Database error: " + ex.Message;
                    response.statusCode = 500;
                }
            }
            catch (Exception ex)
            {
                response.responseCode = 1;
                response.message = "Unexpected error: " + ex.Message;
                response.statusCode = 500;
            }

            return response;
        }

        [HttpPost]
        [Route("DeleteDrvExpRights")]
        public Api_CommonResponse DeleteDrvExpRights([FromBody] DeleteRequest request)
        {
            Api_CommonResponse response = new Api_CommonResponse();

            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@iId", request.iId);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Delete_UserwiseDrvExpRights]", param);

                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    response.responseCode = 0;
                    response.message = ds.Tables[0].Rows[0]["Message"].ToString();
                    response.statusCode = Convert.ToInt32(ds.Tables[0].Rows[0]["StatusCode"]);
                }
                else
                {
                    response.responseCode = 1;
                    response.message = "Server error: Unable to delete the record at this moment.";
                    response.statusCode = -1;
                }
            }
            catch (SqlException ex)
            {
                response.responseCode = 1;
                response.message = "Database error: " + ex.Message;
                response.statusCode = 500;
            }
            catch (Exception ex)
            {
                response.responseCode = 1;
                response.message = "Unexpected error: " + ex.Message;
                response.statusCode = 500;
            }

            return response;
        }

       

    }
}
