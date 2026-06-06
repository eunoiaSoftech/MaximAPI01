using Eunoia_UM.Models;
using Eunoia_UM_API.Helper;
using Eunoia_UM_API.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Text;

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

        private List<T> ConvertDataTableToModel<T>(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return new List<T>();

            return JsonConvert.DeserializeObject<List<T>>(JsonConvert.SerializeObject(dt));
        }

        [HttpGet]
        [Route("GetNEFTApprovalDashboard")]
        public Api_CommonResponse GetNEFTApprovalDashboard(int iFk_BranchId, int iFk_YearId)
        {
            Api_CommonResponse response = new Api_CommonResponse();

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@iFk_BranchId", iFk_BranchId),
                    new SqlParameter("@iFk_YearId", iFk_YearId)
                };

                NEFTModel result = new NEFTModel();

                // Master
                DataSet dsMaster = DBOperation.FillDataSet("[dbo].[USP_NEFT_NEFTAPPROVE_NEFT_Get]",
                    param);

                result.NEFTMasters =
                    dsMaster != null && dsMaster.Tables.Count > 0
                    ? ConvertDataTableToModel<NEFTMaster>(dsMaster.Tables[0])
                    : new List<NEFTMaster>();


                // Pending/Edit
                SqlParameter[] param2 =
                {
                    new SqlParameter("@iFk_BranchId", iFk_BranchId),
                    new SqlParameter("@iFk_YearId", iFk_YearId)
                };
                DataSet dsEdit = DBOperation.FillDataSet("[dbo].[USP_NeftApprove_NeftApprove_Get]",param2);

                result.editData =
                    dsEdit != null && dsEdit.Tables.Count > 0
                    ? ConvertDataTableToModel<NEFTEdit>(dsEdit.Tables[0])
                    : new List<NEFTEdit>();


                // UnApprove
                SqlParameter[] param3 =
                {
                    new SqlParameter("@iFk_BranchId", iFk_BranchId),
                    new SqlParameter("@iFk_YearId", iFk_YearId)
                };
                DataSet dsUnApprove = DBOperation.FillDataSet("[dbo].[USP_NeftApprove_NeftApproveMaster_Get]",param3);

                result.NEFTForUnApprove =
                    dsUnApprove != null && dsUnApprove.Tables.Count > 0
                    ? ConvertDataTableToModel<NEFTEdit>(dsUnApprove.Tables[0])
                    : new List<NEFTEdit>();


                response.responseCode = 200;
                response.statusCode = 0;
                response.message = "Success";
                response.data = result;
            }
            catch (Exception ex)
            {
                response.responseCode = 500;
                response.statusCode = -1;
                response.message = ex.Message;
                response.data = null;
            }

            return response;
        }

        [HttpPost]
        [Route("ApproveNEFT")]
        public Api_CommonResponse ApproveNEFT(int iPk_NeftExportMstId, string dtApprovedon)
        {
            Api_CommonResponse response = new Api_CommonResponse();

            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@iPk_NeftExportMstId", iPk_NeftExportMstId),
                    new SqlParameter("@dtApprovedon", dtApprovedon)
                };

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_NEFT_API_ApproveNEFT]", param);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    response.responseCode = 200;
                    response.statusCode = 0;
                    response.message = "NEFT record approved successfully.";
                    response.data = true;
                }
                else
                {
                    response.responseCode = 404;
                    response.statusCode = -1;
                    response.message = "NEFT record not found for approval.";
                    response.data = false;
                }
            }
            catch (Exception ex)
            {
                response.responseCode = 500;
                response.statusCode = -1;
                response.message = "Internal server error: " + ex.Message;
                response.data = null;
            }

            return response;
        }

        [HttpGet]
        [Route("GetFMApprovalLevelOne")]
        public Api_CommonResponse GetFMApprovalLevelOne(
    int BranchId,
    int YearId,
    int UserId,
    int LocationId,
    string TransactionType = null
)
        {
            Api_CommonResponse api_Response = new Api_CommonResponse();

            try
            {
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@BranchId", BranchId);
                param[1] = new SqlParameter("@YearId", YearId);
                param[2] = new SqlParameter("@User_id", UserId);
                param[3] = new SqlParameter("@LoacationId", LocationId);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Operation_EunoiaApi_ChangeRequestIndex_Get]", param);

                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    List<driverList> trupDetails = JsonConvert.DeserializeObject<List<driverList>>(
                        JsonConvert.SerializeObject(ds.Tables[0])
                    );

                    api_Response.responseCode = 1;
                    api_Response.statusCode = 200;
                    api_Response.message = "Service List";
                    api_Response.data = new
                    {
                        TransactionType = TransactionType,
                        TrupDetails = trupDetails
                    };
                }
                else
                {
                    api_Response.responseCode = 1;
                    api_Response.statusCode = 1;
                    api_Response.message = "No Data Available";
                    api_Response.data = new
                    {
                        TransactionType = TransactionType,
                        TrupDetails = new List<driverList>()
                    };
                }
            }
            catch (Exception ex)
            {
                api_Response.responseCode = 500;
                api_Response.statusCode = -1;
                api_Response.message = "Internal server error: " + ex.Message;
                api_Response.data = null;
            }

            return api_Response;
        }

        [HttpPost]
        [Route("FMRateDifferenceApprovalUpdateFlags")]
        public async Task<Api_CommonResponse> FMRateDifferenceApprovalUpdateFlags([FromBody] FMRateDifferenceModel data)
        {
            Api_CommonResponse api_Response = new Api_CommonResponse();

            try
            {
                if (data == null || data.prchAprvll == null || data.prchAprvll.Count == 0)
                {
                    api_Response.responseCode = 1;
                    api_Response.statusCode = -1;
                    api_Response.message = "No approval items provided.";
                    api_Response.data = null;
                    return api_Response;
                }

                foreach (var item in data.prchAprvll)
                {
                    var result = await FMRateDifferenceApprovalUpdateInternal(item);
                    api_Response.responseCode = result.responseCode;
                    api_Response.statusCode = result.statusCode;
                    api_Response.message = result.message;
                    api_Response.data = result.data;
                }
            }
            catch (Exception ex)
            {
                api_Response.responseCode = 500;
                api_Response.statusCode = -1;
                api_Response.message = "Internal server error: " + ex.Message;
                api_Response.data = null;
            }

            return api_Response;
        }

        private async Task<Api_CommonResponse> FMRateDifferenceApprovalUpdateInternal(prchAprvll item)
        {
            Api_CommonResponse api_Response = new Api_CommonResponse();

            try
            {
                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@FmRateDiffId", item.iPk_FMRATEDIFId);
                param[1] = new SqlParameter("@iType", item.iType);
                param[2] = new SqlParameter("@UserId", (object)item.UserId ?? DBNull.Value);
                param[3] = new SqlParameter("@TransactionId", (object)item.TransactionId ?? DBNull.Value);
                param[4] = new SqlParameter("@TransactionType", (object)item.iTransType ?? DBNull.Value);
                param[5] = new SqlParameter("@iFk_MenuId", item.iFk_MenuId);

                DataSet ds = DBOperation.FillDataSet("USP_Operation_API_FMRateDifferenceFlagUpdate_Update", param);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];

                    api_Response.statusCode = Convert.ToInt32(row["StatusCode"]);
                    api_Response.message = row["Message"].ToString();
                    api_Response.responseCode = 0;
                    api_Response.data = null;

                    if (api_Response.statusCode == 1)
                    {
                        string emailBody = BuildTripSettlementEmailBody(row);
                        await SendTripSettlementChangeEmailAsync(
                            emailBody,
                            item.TransactionId,
                            item.iFk_LocationId,
                            item.iFk_BranchId,
                            item.iFk_YearId,
                            item.UserId
                        );
                    }
                }
                else
                {
                    api_Response.responseCode = 1;
                    api_Response.statusCode = -1;
                    api_Response.message = "Server issue. Please try again.";
                    api_Response.data = null;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return api_Response;
        }

        [HttpPost]
        [Route("FMRateDifferenceRejectionUpdateFlags")]
        public Api_CommonResponse FMRateDifferenceRejectionUpdateFlags([FromBody] FMRateDifferenceModel data)
        {
            Api_CommonResponse api_Response = new Api_CommonResponse();

            try
            {
                if (data == null || data.prchAprvll == null || data.prchAprvll.Count == 0)
                {
                    api_Response.responseCode = 1;
                    api_Response.statusCode = -1;
                    api_Response.message = "No rejection items provided.";
                    api_Response.data = null;
                    return api_Response;
                }

                foreach (var item in data.prchAprvll)
                {
                    SqlParameter[] param = new SqlParameter[6];
                    param[0] = new SqlParameter("@FmRateDiffId", item.iPk_FMRATEDIFId);
                    param[1] = new SqlParameter("@iType", 3); // 1 for level 1, 2 for level 2, 3 for reject
                    param[2] = new SqlParameter("@UserId", (object)item.UserId ?? DBNull.Value);
                    param[3] = new SqlParameter("@TransactionId", (object)item.TransactionId ?? DBNull.Value);
                    param[4] = new SqlParameter("@TransactionType", (object)item.iTransType ?? DBNull.Value);
                    param[5] = new SqlParameter("@iFk_MenuId", item.iFk_MenuId);

                    DataSet ds = DBOperation.FillDataSet(
                        "USP_Operation_API_FMRateDifferenceFlagUpdate_Update", param);

                    if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        api_Response.responseCode = 0;
                        api_Response.statusCode = Convert.ToInt32(ds.Tables[0].Rows[0]["StatusCode"]);
                        api_Response.message = ds.Tables[0].Rows[0]["Message"].ToString();
                        api_Response.data = null;
                    }
                    else
                    {
                        api_Response.responseCode = 1;
                        api_Response.statusCode = -1;
                        api_Response.message = "We are facing server issue at the moment please try again...";
                        api_Response.data = null;
                    }
                }
            }
            catch (Exception ex)
            {
                api_Response.responseCode = 500;
                api_Response.statusCode = -1;
                api_Response.message = "Internal server error: " + ex.Message;
                api_Response.data = null;
            }

            return api_Response;
        }

        [HttpGet]
        [Route("GetPaymentPlanningApprovalList")]
        public Api_CommonResponse GetPaymentPlanningApprovalList()
        {
            Api_CommonResponse response = new Api_CommonResponse();

            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_OwnerDashBoardPLANNINGSHEETFORAPPROVE_API_Get]");

                var data = ds != null &&
                           ds.Tables.Count > 0
                    ? ConvertDataTableToModel<PaymentPlanningApprove>(ds.Tables[0])
                    : new List<PaymentPlanningApprove>();

                response.responseCode = 200;
                response.statusCode = 0;
                response.message = "Success";
                response.data = data;
            }
            catch (Exception ex)
            {
                response.responseCode = 500;
                response.statusCode = -1;
                response.message = ex.Message;
                response.data = null;
            }

            return response;
        }

        [HttpPost]
        [Route("ApprovePaymentPlanning")]
        public Api_CommonResponse ApprovePaymentPlanning([FromBody] PaymentPlanningMaster model)
        {
            Api_CommonResponse response = new Api_CommonResponse();

            try
            {
                if (model == null ||
                    model.PYMTPLNGDTL1 == null ||
                    !model.PYMTPLNGDTL1.Any())
                {
                    response.responseCode = 400;
                    response.statusCode = -1;
                    response.message = "No invoice details provided for approval.";
                    response.data = null;

                    return response;
                }

                foreach (var invoiceDetail in model.PYMTPLNGDTL1)
                {
                    SqlParameter[] param = new SqlParameter[4];
                    {
                        param[0] = new SqlParameter("@iPk_PrchsInvPstngId", invoiceDetail.iPk_PrchsInvPstngId);

                        param[1] = new SqlParameter("@iIsPlaningDone", invoiceDetail.iIsPlaningDone);

                        param[2] = new SqlParameter("@dtPaymentDate", invoiceDetail.dtPaymentDate);

                        param[3] = new SqlParameter("@iCrtdBy", invoiceDetail.iCrtdBy);

                    }

                    DataSet ds = DBOperation.FillDataSet("[dbo].[USP_PYMTPLNG_PYMTPLNGAprove_API_Update]", param);

                    if (ds == null ||
                        ds.Tables.Count == 0 ||
                        ds.Tables[0].Rows.Count == 0)
                    {
                        response.responseCode = 500;
                        response.statusCode = -1;
                        response.message = "No response from database.";
                        response.data = null;

                        return response;
                    }

                    int statusCode = Convert.ToInt32(ds.Tables[0].Rows[0]["StatusCode"]);

                    string message = Convert.ToString(ds.Tables[0].Rows[0]["Message"]);

                    if (statusCode != 200)
                    {
                        response.responseCode = 400;
                        response.statusCode = statusCode;
                        response.message = message;
                        response.data = null;

                        return response;
                    }
                }

                response.responseCode = 200;
                response.statusCode = 0;
                response.message = "Payment plan approved successfully.";
                response.data = true;
            }
            catch (Exception ex)
            {
                response.responseCode = 500;
                response.statusCode = -1;
                response.message = ex.Message;
                response.data = null;
            }

            return response;
        }

        private string BuildTripSettlementEmailBody(DataRow row)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<p>Hello Dear User,</p>");
            sb.Append("<p>");
            sb.Append("It has been noticed that the <strong>Trip Settlement Amount</strong> ");
            sb.Append("has been changed and the additional amount exceeds the pending amount. ");
            sb.Append("Kindly find the updated details below:");
            sb.Append("</p>");
            sb.Append("<table border='1' cellpadding='6' cellspacing='0' ");
            sb.Append("style='border-collapse:collapse;width:100%'>");

            void AddRow(string label, object value)
            {
                sb.Append("<tr>");
                sb.Append($"<td style='background:#f2f2f2;font-weight:bold;width:35%'>{label}</td>");
                sb.Append($"<td>{value}</td>");
                sb.Append("</tr>");
            }

            AddRow("Entry No", row["sEntryNo"]);
            AddRow("Entry Date",
                Convert.ToDateTime(row["dtEntryDate"]).ToString("dd-MM-yyyy HH:mm"));
            AddRow("Pending Amount", row["dPendingAmt"]);
            AddRow("Additional Amount", row["dTtlAddAmnt"]);
            AddRow("Confirmed By", row["ApprovedBy"]);
            AddRow("Created By", row["CreatedBy"]);
            AddRow("Changed On",
                Convert.ToDateTime(row["ChangedOn"]).ToString("dd-MM-yyyy HH:mm"));

            sb.Append("</table>");
            sb.Append("<br/>");
            sb.Append("<p>Thanks,</p>");
            sb.Append("<p><strong>Team Maxim @EunoiaSofttech</strong></p>");

            return sb.ToString();
        }

        private async Task SendTripSettlementChangeEmailAsync(
            string emailBody,
            int? transactionId,
            int locationId,
            int branchId,
            int yearId,
            int? userId)
        {
            try
            {
                string baseUrl = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"];
                string url = baseUrl + "Company/TripSettlementChangeMailSend";

                var payload = new
                {
                    HtmlContent = emailBody,
                    iFk_LocationId = locationId,
                    iFk_BranchId = branchId,
                    iFk_YearID = yearId,
                    iFk_userId = userId,
                    srvcID = transactionId
                };

                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("cache-control", "no-cache");
                    httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

                    string jsonPayload = JsonConvert.SerializeObject(payload);
                    StringContent content = new StringContent(
                        jsonPayload,
                        System.Text.Encoding.UTF8,
                        "application/json"
                    );

                    await httpClient.PostAsync(url, content);
                }
            }
            catch (Exception)
            {
                // Email failure must not break the approval update response
            }
        }
    }
}
