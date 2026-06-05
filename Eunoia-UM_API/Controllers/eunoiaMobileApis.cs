using Microsoft.AspNetCore.Mvc;
using Eunoia_UM_API.Helper;
using Eunoia_UM_API.Model;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace Eunoia_UM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class eunoiaMobileApis : Controller
    {

        [HttpGet]
        [Route("InsertUserAccessLogo")]
        public Api_CommonResponse InsertUserAccessLogo(
    decimal dLatitude,
    decimal dLongitude,
    int iFk_UserId
)
        {
            Api_CommonResponse api_Response = new Api_CommonResponse();

            try
            {
                SqlParameter[] param = new SqlParameter[3];

                param[0] = new SqlParameter("@dLatitude", dLatitude);
                param[1] = new SqlParameter("@dLongitude", dLongitude);
                param[2] = new SqlParameter("@iFk_UserId", iFk_UserId);

                DataSet ds = DBOperation.FillDataSet(
                    "USP_UserAccessLogo_InsertUserDetails_Save",
                    param
                );

                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    var resultList = new List<Dictionary<string, object>>();

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var dict = new Dictionary<string, object>();

                        foreach (DataColumn col in ds.Tables[0].Columns)
                        {
                            dict[col.ColumnName] = row[col];
                        }

                        resultList.Add(dict);
                    }

                    api_Response.responseCode = 0;
                    api_Response.statusCode = Convert.ToInt32(ds.Tables[0].Rows[0]["StatusCode"]);
                    api_Response.message = ds.Tables[0].Rows[0]["Message"].ToString();
                    api_Response.data = resultList;
                }
                else
                {
                    api_Response.responseCode = 1;
                    api_Response.statusCode = -1;
                    api_Response.message = "We are facing server issue at the moment please try again...";
                    api_Response.data = null;
                }
            }
            catch (Exception ex)
            {
                api_Response.responseCode = 1;
                api_Response.statusCode = 500;
                api_Response.message = ex.Message;
                api_Response.data = null;
            }

            return api_Response;
        }

        [HttpGet]
        [Route("GetMyTaskListData")]
        public Api_CommonResponse GetMyTaskListData(
    string FromDate = null,
    string ToDate = null,
    int BranchId = 0
)
        {
            Api_CommonResponse api_Response = new Api_CommonResponse();

            try
            {
                SqlParameter[] param = new SqlParameter[3];

                param[0] = new SqlParameter("@FromDate", (object)FromDate ?? DBNull.Value);
                param[1] = new SqlParameter("@ToDate", (object)ToDate ?? DBNull.Value);
                param[2] = new SqlParameter("@BranchId", BranchId);

                DataSet ds = DBOperation.FillDataSet(
                    "[dbo].[USP_Owner_GetMyTaskListData_Get]",
                    param
                );

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    var resultList = new List<Dictionary<string, object>>();

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var dict = new Dictionary<string, object>();

                        foreach (DataColumn col in ds.Tables[0].Columns)
                        {
                            dict[col.ColumnName] = row[col];
                        }

                        resultList.Add(dict);
                    }

                    api_Response.responseCode = 200;
                    api_Response.message = "Data retrieved successfully";
                    api_Response.statusCode = 0;
                    api_Response.data = resultList;
                }
                else
                {
                    api_Response.responseCode = 404;
                    api_Response.message = "No records found";
                    api_Response.statusCode = -1;
                }
            }
            catch (Exception ex)
            {
                api_Response.responseCode = 500;
                api_Response.message = "Internal server error: " + ex.Message;
                api_Response.statusCode = -1;
            }

            return api_Response;
        }


        [HttpGet]
        [Route("GetFastTagApprovalList")]
        public Api_CommonResponse GetFastTagApprovalList(
            int StatusType = 0,
            int iFk_BranchId = 0,
            int iFk_YearId = 0
        )
        {
            Api_CommonResponse api_Response = new Api_CommonResponse();

            try
            {
                SqlParameter[] param = new SqlParameter[3];

                param[0] = new SqlParameter("@StatusType", StatusType);
                param[1] = new SqlParameter("@iFk_BranchId", iFk_BranchId);
                param[2] = new SqlParameter("@iFk_YearId", iFk_YearId);

                DataSet ds = DBOperation.FillDataSet(
                    "[dbo].[USP_eunoiaMobileApis_Approval_FASTTAGIMPMST_Get_All]",
                    param
                );

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    var resultList = new List<Dictionary<string, object>>();

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var dict = new Dictionary<string, object>();

                        foreach (DataColumn col in ds.Tables[0].Columns)
                        {
                            dict[col.ColumnName] = row[col];
                        }

                        resultList.Add(dict);
                    }

                    api_Response.responseCode = 200;
                    api_Response.message = "Data retrieved successfully";
                    api_Response.statusCode = 0;
                    api_Response.data = resultList;
                }
                else
                {
                    api_Response.responseCode = 404;
                    api_Response.message = "No records found";
                    api_Response.statusCode = -1;
                }
            }
            catch (Exception ex)
            {
                api_Response.responseCode = 500;
                api_Response.message = "Internal server error: " + ex.Message;
                api_Response.statusCode = -1;
            }

            return api_Response;
        }

        [HttpGet]
        [Route("UpdateFastTagApprovalStatus")]
        public Api_CommonResponse UpdateFastTagApprovalStatus(
    int iPk_FastTagImportMstId,
    int iStatus,
    int iCrtdBy,
    string sRejectReason = null
)
        {
            Api_CommonResponse api_Response = new Api_CommonResponse();

            try
            {
                SqlParameter[] param = new SqlParameter[4];

                param[0] = new SqlParameter("@iPk_FastTagImportMstId", iPk_FastTagImportMstId);
                param[1] = new SqlParameter("@iStatus", iStatus);
                param[2] = new SqlParameter("@iCrtdBy", iCrtdBy);
                param[3] = new SqlParameter("@sRejectReason",
                            (object)sRejectReason ?? DBNull.Value);

                DataSet ds = DBOperation.FillDataSet(
                    "USP_MASTER_FasttagImportApproval_UpdateStatus",
                    param
                );

                if (ds != null && ds.Tables != null &&
                    ds.Tables.Count > 0 &&
                    ds.Tables[0].Rows.Count > 0)
                {
                    var resultList = new List<Dictionary<string, object>>();

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var dict = new Dictionary<string, object>();

                        foreach (DataColumn col in ds.Tables[0].Columns)
                        {
                            dict[col.ColumnName] = row[col];
                        }

                        resultList.Add(dict);
                    }

                    api_Response.responseCode = 0;
                    api_Response.statusCode = 200;
                    api_Response.message = ds.Tables[0].Rows[0]["Message"].ToString();
                    api_Response.data = resultList;
                }
                else
                {
                    api_Response.responseCode = 1;
                    api_Response.statusCode = -1;
                    api_Response.message = "No records updated";
                    api_Response.data = null;
                }
            }
            catch (Exception ex)
            {
                api_Response.responseCode = 1;
                api_Response.statusCode = 500;
                api_Response.message = ex.Message;
                api_Response.data = null;
            }

            return api_Response;
        }
    }
}
