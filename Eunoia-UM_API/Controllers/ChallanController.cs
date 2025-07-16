using Eunoia_UM_API.Helper;
using Eunoia_UM_API.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.IO;

namespace Eunoia_UM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChallanController : ControllerBase
    {
        Api_CommonResponse api_Response = new Api_CommonResponse();

        [HttpGet]
        [Route("GetChallanDropdownData")]
        public Api_CommonResponse ChallanData(string type = "")
        {
            try
            {
                string procName = "";

                if (type == "BankCashName")
                    procName = "[dbo].[USP_Legal_BankCashName_Dropdown_Get]";
                if (type == "PaymentMode")
                    procName = "[dbo].[USP_Legal_PaymentMode_Dropdown_Get]";
                if (type == "DebitAccount")
                    procName = "[dbo].[USP_Legal_DebitAccount_Dropdown_Get]";
                if (type == "VehicleNumber")
                    procName = "[dbo].[USP_Legal_VehicleNumbers_Dropdown_Get]";
                if (type == "ChallanLocation")
                    procName = "[dbo].[USP_Legal_ChallanLocation_Dropdown_Get]";
                if (type == "ChallanType")
                    procName = "[dbo].[USP_Legal_ChallanType_Dropdown_Get]";
                if (type == "Authority")
                    procName = "[dbo].[USP_Legal_Authority_Dropdown_Get]";

                DataSet ds = DBOperation.FillDataSet(procName);
                if (ds != null && ds.Tables != null)
                {
                    api_Response.responseCode = 1;
                    api_Response.data = JsonConvert.SerializeObject(ds.Tables[0]);
                    api_Response.message = type;
                    api_Response.statusCode = 1;
                }
                else
                {
                    api_Response.responseCode = 0;
                    api_Response.message = "No Data Available...";
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
        [Route("GetVehicle_DriverDetails")]
        public Api_CommonResponse GetVehicle_DriverDetails(int vehicleID)
        {
            try
            {

                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@VehicleId", vehicleID);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Legal_Vehicle_DriverDetails_Get]", param);
                if (ds != null && ds.Tables != null)
                {
                    api_Response.responseCode = 1;
                    api_Response.data = JsonConvert.SerializeObject(ds.Tables[0]);
                    api_Response.message = "Vehicle Driver Data";
                    api_Response.statusCode = 1;
                }
                else
                {
                    api_Response.responseCode = 0;
                    api_Response.message = "No Data Available...";
                    api_Response.statusCode = -1;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return api_Response;
        }

        [HttpPost]
        [Route("AddChallan")]
        public Api_CommonResponse AddChallan(Challan challan)
        {
            var ChallanPath = new Dictionary<string, string>();
            try
            {
                ChallanPath = CommonController.UploadChallanAttachment(challan.challanAttachment, challan.entryNo, "Challan_Attachment_" + DateTime.Now.ToString("ddMMMyyyyhhmmss") + "." + challan.challanAttachmentEXT);


                SqlParameter[] param = new SqlParameter[18];
                param[0] = new SqlParameter("@sEntryNo", challan.entryNo);
                param[1] = new SqlParameter("@dtEntryDate", Convert.ToDateTime(challan.entryDate).ToString("yyyy-dd-MM hh:mm tt"));
                param[2] = new SqlParameter("@iFk_VehclId", challan.vehicleID);
                param[3] = new SqlParameter("@sChallanNo", challan.challanNo);
                param[4] = new SqlParameter("@dtChallanDate", Convert.ToDateTime(challan.challanDate).ToString("yyyy-dd-MM hh:mm tt"));
                param[5] = new SqlParameter("@iChallanType", challan.challanTypeID);
                param[6] = new SqlParameter("@iFk_ChallanLocId", challan.challanLocationID);
                param[7] = new SqlParameter("@dChallanAmt", challan.challanAmount);
                param[8] = new SqlParameter("@iFk_AuthorityId", challan.authorityID);
                param[9] = new SqlParameter("@sChallanAttchmnt", ChallanPath["DBPath"].ToString());
                //param[10] = new SqlParameter("@dtPaymtDate", Convert.ToDateTime(challan.paymentDate).ToString("yyyy-dd-MM"));
                //param[11] = new SqlParameter("@iPaymtMode", challan.paymentModeID);
                //param[12] = new SqlParameter("@iFk_BankCash", challan.bankID);
                //param[13] = new SqlParameter("@dAmt", challan.payAmount);
                //param[14] = new SqlParameter("@sReceiptNo", challan.receiptNumber);
                //param[17] = new SqlParameter("@sPaymtAtchmnt", challan.paymentAttachmentPath);
                param[10] = new SqlParameter("@iMaxNo", challan.iMaxNo);
                param[11] = new SqlParameter("@iVoucherStyle", challan.voucherStyleID);
                param[12] = new SqlParameter("@iFk_YearId", challan.iYearID);
                param[13] = new SqlParameter("@iFk_BranchId", challan.iBranchID);
                param[14] = new SqlParameter("@iCrtdBy", challan.createdByID);
                param[15] = new SqlParameter("@iFk_DebitAccnt", challan.debitAccount);
                param[16] = new SqlParameter("@dDebitAmt", challan.debitAmount);
                param[17] = new SqlParameter("@sRemarks", challan.sRemarks);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Legal_CHALLANMST_Save]", param);
                if (ds != null && ds.Tables != null)
                {
                    api_Response.responseCode = 1;
                    api_Response.data = JsonConvert.SerializeObject(ds.Tables[0]);
                    api_Response.message = ds.Tables[0].Rows[0]["Message"].ToString();
                    api_Response.statusCode = Convert.ToInt32(ds.Tables[0].Rows[0]["StatusCode"].ToString());
                }
                else
                {
                    api_Response.responseCode = 0;
                    api_Response.message = "No Data Available...";
                    api_Response.statusCode = -1;
                }
            }
            catch (Exception e)
            {
                if (System.IO.File.Exists(ChallanPath["ABSPath"]))
                    System.IO.File.Delete(ChallanPath["ABSPath"]);

                throw e;
            }
            return api_Response;
        }

        [HttpPost]
        [Route("EditChallan")]
        public Api_CommonResponse EditChallan(ChallanEditDetails challan)
        {
            var ChallanPath = new Dictionary<string, string>();
            try
            {
                // Ensure the path starts correctly with "images\Challan"
                if (challan.challanAttachment.StartsWith("/images/Challan/"))
                {
                    // Trim the leading slash and construct the full file path correctly
                    string relativePath = challan.challanAttachment.TrimStart('/');
                    string baseDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");  // Ensure you're using the wwwroot directory if needed
                    string filePath = Path.Combine(baseDirectory, relativePath);  // Construct the full path

                    // Extract and assign the file extension to paymentAttachmentEXT
                    string fileExtension = Path.GetExtension(filePath);
                    challan.challanAttachmentEXT = fileExtension.TrimStart('.'); // Remove the dot and assign

                    // Check if the file exists
                    if (System.IO.File.Exists(filePath))
                    {
                        byte[] fileData = System.IO.File.ReadAllBytes(filePath);  // Read the file content in binary
                        string base64Data = Convert.ToBase64String(fileData);  // Convert to Base64 string

                        // Pass the Base64 string to the UploadChallanAttachment function
                        ChallanPath = CommonController.UploadChallanAttachment(base64Data, challan.sEntryNo, "Reciept_Attachment_" + DateTime.Now.ToString("ddMMMyyyyhhmmss") + "." + challan.challanAttachmentEXT);
                    }
                    else
                    {
                        throw new FileNotFoundException("The specified file does not exist at the path: " + filePath);
                    }
                }
                else
                {
                    ChallanPath = CommonController.UploadChallanAttachment(challan.challanAttachment, challan.sEntryNo, "Reciept_Attachment_" + DateTime.Now.ToString("ddMMMyyyyhhmmss") + "." + challan.challanAttachmentEXT);
                }

                SqlParameter[] param = new SqlParameter[17];
                param[0] = new SqlParameter("@sEntryNo", challan.sEntryNo);
                param[1] = new SqlParameter("@dtEntryDate", Convert.ToDateTime(challan.EntryDate).ToString("yyyy-dd-MM hh:mm tt"));
                param[2] = new SqlParameter("@iFk_VehclId", challan.iFk_VehclId);
                param[3] = new SqlParameter("@sChallanNo", challan.sChallanNo);
                param[4] = new SqlParameter("@dtChallanDate", Convert.ToDateTime(challan.ChallanDate).ToString("yyyy-dd-MM hh:mm tt"));
                param[5] = new SqlParameter("@iChallanType", challan.iChallanType);
                param[6] = new SqlParameter("@iFk_ChallanLocId", challan.iFk_ChallanLocId);
                param[7] = new SqlParameter("@dChallanAmt", challan.dChallanAmt);
                param[8] = new SqlParameter("@iFk_AuthorityId", challan.iFk_AuthorityId);
                param[9] = new SqlParameter("@sChallanAttchmnt", ChallanPath["DBPath"].ToString());
                //param[10] = new SqlParameter("@dtPaymtDate", Convert.ToDateTime(challan.PaymentDate).ToString("yyyy-dd-MM"));
                //param[11] = new SqlParameter("@iPaymtMode", challan.iPaymtMode);
                //param[12] = new SqlParameter("@iFk_BankCash", challan.iFk_BankCash);
                //param[13] = new SqlParameter("@dAmt", challan.dAmt);
                //param[14] = new SqlParameter("@sReceiptNo", challan.sReceiptNo);
                //param[15] = new SqlParameter("@iFk_DebitAccnt", challan.iFk_DebitAccnt);
                //param[16] = new SqlParameter("@dDebitAmt", challan.dDebitAmt);
                //param[17] = new SqlParameter("@sPaymtAtchmnt", "");
                param[10] = new SqlParameter("@iMaxNo", challan.iMaxNo);
                param[11] = new SqlParameter("@iVoucherStyle", challan.voucherStyleID);
                param[12] = new SqlParameter("@iFk_YearId", challan.iYearID);
                param[13] = new SqlParameter("@iFk_BranchId", challan.iBranchID);
                param[14] = new SqlParameter("@iCrtdBy", challan.createdByID);
                param[15] = new SqlParameter("iPk_ChallanMStId", challan.iPk_ChallanMStId);
                param[16] = new SqlParameter("sRemarks", challan.sRemarks);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Legal_CHALLANMST_Update]", param);
                if (ds != null && ds.Tables != null)
                {
                    api_Response.responseCode = 1;
                    api_Response.data = JsonConvert.SerializeObject(ds.Tables[0]);
                    api_Response.message = ds.Tables[0].Rows[0]["Message"].ToString();
                    api_Response.statusCode = Convert.ToInt32(ds.Tables[0].Rows[0]["StatusCode"].ToString());
                }
                else
                {
                    api_Response.responseCode = 0;
                    api_Response.message = "No Data Available...";
                    api_Response.statusCode = -1;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return api_Response;
        }

        [HttpPost]
        [Route("PaymentDetailsChallan")]
        public Api_CommonResponse UpdatePaymentDetailsChallan(ChallanEditDetails challan)
        {
            var receiptPath = new Dictionary<string, string>();
            try
            {
                // Ensure the path starts correctly with "images\Challan"
                if (challan.sPaymtAtchmnt.StartsWith("/images/Challan/"))
                {
                    // Trim the leading slash and construct the full file path correctly
                    string relativePath = challan.sPaymtAtchmnt.TrimStart('/');
                    string baseDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");  // Ensure you're using the wwwroot directory if needed
                    string filePath = Path.Combine(baseDirectory, relativePath);  // Construct the full path

                    // Extract and assign the file extension to paymentAttachmentEXT
                    string fileExtension = Path.GetExtension(filePath);
                    challan.paymentAttachmentEXT = fileExtension.TrimStart('.'); // Remove the dot and assign

                    // Check if the file exists
                    if (System.IO.File.Exists(filePath))
                    {
                        byte[] fileData = System.IO.File.ReadAllBytes(filePath);  // Read the file content in binary
                        string base64Data = Convert.ToBase64String(fileData);  // Convert to Base64 string

                        // Pass the Base64 string to the UploadChallanAttachment function
                        receiptPath = CommonController.UploadChallanAttachment(base64Data, challan.sEntryNo, "Reciept_Attachment_" + DateTime.Now.ToString("ddMMMyyyyhhmmss") + "." + challan.paymentAttachmentEXT);
                    }
                    else
                    {
                        throw new FileNotFoundException("The specified file does not exist at the path: " + filePath);
                    }
                }
                else
                {
                    receiptPath = CommonController.UploadChallanAttachment(challan.sPaymtAtchmnt, challan.sEntryNo, "Reciept_Attachment_" + DateTime.Now.ToString("ddMMMyyyyhhmmss") + "." + challan.paymentAttachmentEXT);
                }


                SqlParameter[] param = new SqlParameter[14];
                param[0] = new SqlParameter("@dtPaymtDate", Convert.ToDateTime(challan.PaymentDate).ToString("yyyy-dd-MM hh:mm tt"));
                param[1] = new SqlParameter("@iPaymtMode", challan.iPaymtMode);
                param[2] = new SqlParameter("@iFk_BankCash", challan.iFk_BankCash);
                param[3] = new SqlParameter("@dAmt", challan.dAmt);
                param[4] = new SqlParameter("@sReceiptNo", challan.sReceiptNo);
                param[5] = new SqlParameter("@iFk_DebitAccnt", challan.iFk_DebitAccnt);
                param[6] = new SqlParameter("@dDebitAmt", challan.dDebitAmt);
                param[7] = new SqlParameter("@sPaymtAtchmnt", receiptPath["DBPath"].ToString());
                param[8] = new SqlParameter("@iMaxNo", challan.iMaxNo);
                param[9] = new SqlParameter("@iVoucherStyle", challan.voucherStyleID);
                param[10] = new SqlParameter("@iFk_YearId", challan.iYearID);
                param[11] = new SqlParameter("@iFk_BranchId", challan.iBranchID);
                param[12] = new SqlParameter("@iCrtdBy", challan.createdByID);
                param[13] = new SqlParameter("iPk_ChallanMStId", challan.iPk_ChallanMStId);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Legal_CHALLANMST_Update_Payment]", param);
                if (ds != null && ds.Tables != null)
                {
                    api_Response.responseCode = 1;
                    api_Response.data = JsonConvert.SerializeObject(ds.Tables[0]);
                    api_Response.message = ds.Tables[0].Rows[0]["Message"].ToString();
                    api_Response.statusCode = Convert.ToInt32(ds.Tables[0].Rows[0]["StatusCode"].ToString());
                }
                else
                {
                    api_Response.responseCode = 0;
                    api_Response.message = "No Data Available...";
                    api_Response.statusCode = -1;
                }
            }
            catch (Exception e)
            {
                if (System.IO.File.Exists(receiptPath["ABSPath"]))  // Ensure we use System.IO.File here as well
                    System.IO.File.Delete(receiptPath["ABSPath"]);
                throw e;
            }
            return api_Response;
        }



        [HttpPost]
        [Route("GetChallan")]
        public Api_CommonResponse GetChallan(int branch, int yearID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@BranchId", branch);
                param[1] = new SqlParameter("@YearId", yearID);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Legal_ListOfChallan_Index_Get]", param);
                if (ds != null && ds.Tables != null)
                {
                    api_Response.responseCode = 1;
                    api_Response.data = JsonConvert.SerializeObject(ds.Tables[0]);
                    api_Response.message = "Challan List";
                    api_Response.statusCode = 1;
                }
                else
                {
                    api_Response.responseCode = 0;
                    api_Response.message = "No Data Available...";
                    api_Response.statusCode = -1;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return api_Response;
        }

        [HttpPost]
        [Route("GetChallanDetailsForEdit")]
        public Api_CommonResponse GetChallanDetailsForEdit(int rowID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Id", rowID);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Legal_ChallanMasterDetailsForEdit_Edit_Get]", param);
                if (ds != null && ds.Tables != null)
                {
                    api_Response.responseCode = 1;
                    api_Response.data = JsonConvert.SerializeObject(ds.Tables[0]);
                    api_Response.message = "Challan Details For Edit";
                    api_Response.statusCode = 1;
                }
                else
                {
                    api_Response.responseCode = 0;
                    api_Response.message = "No Data Available...";
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
        [Route("DownloadChallanDocx")]
        public IActionResult DownloadChallanDocx(string filePath)
        {
            try
            {
                // Ensure the filePath is valid and secure
                string baseDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"); // Base directory
                string fullPath = Path.Combine(baseDirectory, filePath.TrimStart('/')); // Combine base with relative path

                if (System.IO.File.Exists(fullPath))
                {
                    var memory = new MemoryStream();
                    using (var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                    {
                        stream.CopyTo(memory);
                    }
                    memory.Position = 0;

                    // Extract file name
                    string fileName = Path.GetFileName(fullPath);

                    // Return file as a downloadable response
                    return File(memory, "application/octet-stream", fileName);
                }
                else
                {
                    return NotFound("The specified file was not found.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an error
                return StatusCode(500, "An error occurred while processing your request: " + ex.Message);
            }
        }


    }
}


