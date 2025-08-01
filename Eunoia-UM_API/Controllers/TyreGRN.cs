using Eunoia_UM_API.Helper;
using Eunoia_UM_API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using static iTextSharp.text.pdf.AcroFields;

namespace Eunoia_UM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TyreGRN : ControllerBase
    {
        Api_CommonResponse api_Response = new Api_CommonResponse();

        [HttpGet]
        [Route("GetVendorList")]
        public Api_CommonResponse GetVendorList()
        {
            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_GetVendorList_TyreGRN]", null);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.responseCode = 200;
                    api_Response.message = "Data retrieved successfully";
                    api_Response.statusCode = 0;

                    api_Response.data = JsonConvert.DeserializeObject<List<TyreGRNModel>>(
                        JsonConvert.SerializeObject(ds.Tables[0]));
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

        [HttpPost]
        [Route("GetPONOsQuantityByPartyId")]
        public Api_CommonResponse GetPONOsQuantityByPartyId(int partyId)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@PartyId", partyId);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_GetPONOsWithBalanceQtyByPartyId_TyreGRN]", param);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    var Data = JsonConvert.DeserializeObject<List<PONOsDetailTyreGRN>>(JsonConvert.SerializeObject(ds.Tables[0]));

                    api_Response.responseCode = 200;
                    api_Response.message = "Record fetched successfully.";
                    api_Response.statusCode = 0;
                    api_Response.data = null;
                    api_Response.data1 = Data;
                }
                else
                {
                    api_Response.responseCode = 404;
                    api_Response.message = "No record found for the given party.";
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

        [HttpPost]
        [Route("GetDescriptionAndTyreNo")]
        public Api_CommonResponse GetDescriptionAndTyreNo()
        {
            try
            {

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_GetDescriptionTyreNo_TyreGRN]", null);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    var Data = JsonConvert.DeserializeObject<List<TyrenDescriptionTyreGRN>>(JsonConvert.SerializeObject(ds.Tables[0]));

                    api_Response.responseCode = 200;
                    api_Response.message = "Record fetched successfully.";
                    api_Response.statusCode = 0;
                    api_Response.data = null;
                    api_Response.data1 = Data;
                }
                else
                {
                    api_Response.responseCode = 404;
                    api_Response.message = "No record found for the given party.";
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

        [HttpPost]
        [Route("SaveTyreGRN")]
        public Api_CommonResponse SaveTyreGRN(SaveTyreGRNRequest request)
        {
            try
            {
                if (request == null || request.iPk_PartyId == 0)
                {
                    api_Response.responseCode = 400;
                    api_Response.message = "Invalid request data.";
                    api_Response.statusCode = -1;
                    return api_Response;
                }

                SqlParameter[] masterParams = new SqlParameter[]
                {
                    new SqlParameter("@sInvoiceNo", request.sInvoiceNumber ?? string.Empty),
                    new SqlParameter("@dtInvoiceDate", request.dtInvoiceDate),
                    new SqlParameter("@iPk_PartyId", request.iPk_PartyId)
                };

                var dsMaster = DBOperation.FillDataSet("[dbo].[USP_SaveTyreGRN_Master]", masterParams);

                if (dsMaster == null || dsMaster.Tables.Count == 0 || dsMaster.Tables[0].Rows.Count == 0)
                {
                    api_Response.responseCode = 500;
                    api_Response.message = "Failed to save Tyre GRN master record.";
                    api_Response.statusCode = -1;
                    return api_Response;
                }

                int iPk_GRNId = Convert.ToInt32(dsMaster.Tables[0].Rows[0]["iPk_GRNId"]);

                // Insert PO Details linked with this master GRN ID
                if (request.PONOsDetails != null)
                {
                    var poDetail = request.PONOsDetails;
                    var poNo = poDetail.sPONO?.Trim().ToLower() == "string" ? null : poDetail.sPONO;

                    SqlParameter[] poParams = new SqlParameter[]
                    {
                            new SqlParameter("@iFk_GRNId", iPk_GRNId),
                            new SqlParameter("@sPONO", (object)poNo ?? (object)DBNull.Value ),
                            new SqlParameter("@dQuantity", poDetail.Quantity)
                    };
                    DBOperation.FillDataSet("[dbo].[USP_SavePONOsDetails_TyreGRN]", poParams);
                }

                // Insert Tyre Description details linked with master GRN ID
                if (request.TyreDetails != null && request.TyreDetails.Count > 0)
                {
                    if (request.TyreDetails != null)
                    {
                        foreach (var tyre in request.TyreDetails)
                        {
                            string description = tyre.sDescription?.Trim().ToLower() == "string" ? null : tyre.sDescription;
                            string tyreNo = tyre.sTyreNo?.Trim().ToLower() == "string" ? null : tyre.sTyreNo;

                            SqlParameter[] tyreParams = new SqlParameter[]
                            {
                            new SqlParameter("@iFk_GRNId", iPk_GRNId),
                            new SqlParameter("@sDescription", (object)description ?? (object)DBNull.Value),
                            new SqlParameter("@sTyreNo", (object)tyreNo ?? (object)DBNull.Value)
                            };
                            DBOperation.FillDataSet("[dbo].[USP_SaveTyreDescription_TyreGRN]", tyreParams);
                        }
                    }
                }

                api_Response.responseCode = 200;
                api_Response.message = "Tyre GRN saved successfully";
                api_Response.statusCode = 0;
                api_Response.data = new { iPk_GRNId = iPk_GRNId };

                return api_Response;
            }
            catch (Exception ex)
            {
                api_Response.responseCode = 500;
                api_Response.message = "Internal server error: " + ex.Message;
                api_Response.statusCode = -1;
                return api_Response;
            }
        }
    }
}
