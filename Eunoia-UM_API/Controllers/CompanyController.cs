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
    public class CompanyController : ControllerBase
    {
        Api_CommonResponse api_Response = new Api_CommonResponse();

        [HttpPost]
        [Route("AddCompany")]
        public Api_CommonResponse AddCompany(CompanyMaster com)
        {
            try
            {
                var companyID = 0;

                SqlParameter[] param = new SqlParameter[12];
                param[0] = new SqlParameter("@sCmpnyNme", com.CompanyName);
                param[1] = new SqlParameter("@sCmpnyLogo", com.Logo);
                param[2] = new SqlParameter("@dRegstrdOn", com.RegisteredOn);
                param[3] = new SqlParameter("@SRegstrdDocName", com.RegisteredDocumentName);
                param[4] = new SqlParameter("@sRegstrdDoc", com.RegisteredDoc);
                param[5] = new SqlParameter("@iCmpnyCINNum", com.CINNumber);
                param[6] = new SqlParameter("@sCmpnyGSTNum", com.GSTNumber);
                param[7] = new SqlParameter("@sPanCdNum", com.PanCardNumber);
                param[8] = new SqlParameter("@sWebsiteURL", com.WebsiteURL);
                param[9] = new SqlParameter("@sEmail", com.EmailURL);
                param[10] = new SqlParameter("@sName", com.sName);
                param[11] = new SqlParameter("@CmpnyDesc", com.CmpnyDesc);
                DataSet ds = DBOperation.FillDataSet("[dbo].[Usp_Admin_Company_Save]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    companyID = Convert.ToInt32(ds.Tables[0].Rows[0]["CompanyID"]);

                    if (com.ContactDtls != null && com.ContactDtls.Count > 0)
                    {
                        foreach (var item in com.ContactDtls)
                        {
                            AddCompanyMembers(item, companyID, "Insert");
                        }
                        AddCompanyAddressDetails(com.AddressDtls, companyID, "I");
                    }
                    if (com.Brnchdtls2 != null && com.Brnchdtls2.Count > 0)
                    {
                        foreach (var item in com.Brnchdtls2)
                        {
                            AddBranchDetails(item, companyID);
                        }
                        //AddSupplierAddressDetails(supp.AddressDtls, supplierID, "I");
                    }
                    api_Response.message = ds.Tables[0].Rows[0]["Message"].ToString();
                    api_Response.statusCode = Convert.ToInt32(ds.Tables[0].Rows[0]["StatusCode"]);
                    api_Response.responseCode = 1;
                }
                else
                {
                    api_Response.message = "No Data Available";
                    api_Response.statusCode = 400;
                    api_Response.responseCode = 0;
                }
            }
            catch (Exception e)
            {
                api_Response.message = e.Message.ToString();
                api_Response.statusCode = 400;
                api_Response.responseCode = 0;

            }
            return api_Response;
        }

        [HttpPost]
        [Route("AddCompanyMembers")]
        public string AddCompanyMembers(ContactDtls member, int companyId, string type)
        {
            var status = "";
            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@iCmpnyMstId", companyId);
                param[1] = new SqlParameter("@sCntctDetlspersonNme", member.CntctPerson);
                param[2] = new SqlParameter("@sCntctNum", member.CntctNumber);
                param[3] = new SqlParameter("@sCntcEmail", member.EmailId);
                param[4] = new SqlParameter("@bPrimary", member.Primary);
                param[5] = new SqlParameter("@Flag", type);

                DataSet ds = DBOperation.FillDataSet("[dbo].[Usp_Admin_CompanyContactDtls_Save]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    status = ds.Tables[0].Rows[0]["Message"].ToString();
                }
                else
                {
                    status = "We Are facing some internal issue, Please try again later";
                }
            }
            catch (Exception e)
            {
                status = e.Message.ToString();

            }
            return status;
        }
        [HttpPost]
        [Route("AddCompanyAddressDetails")]
        public string AddCompanyAddressDetails(AddressDtls Address, int companyId, string type)
        {
            var status = "";
            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@iCmpnyMstId", companyId);
                param[1] = new SqlParameter("@sAddressCntry", Address.Country);
                param[2] = new SqlParameter("@sAddressState", Address.State);
                param[3] = new SqlParameter("@sAddressCity", Address.City);
                param[4] = new SqlParameter("@sPincd", Address.Pincode);
                param[5] = new SqlParameter("@sAddress", Address.Address);
                param[6] = new SqlParameter("@Flag", type);

                DataSet ds = DBOperation.FillDataSet("[dbo].[Usp_Admin_CompanyAddressdtls_Save]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    status = ds.Tables[0].Rows[0]["Message"].ToString();
                }
                else
                {
                    status = "We Are facing some internal issue, Please try again later";
                }
            }
            catch (Exception e)
            {
                status = e.Message.ToString();

            }
            return status;
        }
        [HttpPost]
        [Route("AddBranchDetails")]
        public string AddBranchDetails(Brnchdtls2 member, int partyId)
        {
            var status = "";
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@iFK_PartyId", partyId);
                param[1] = new SqlParameter("@iFk_BranchId", member.iFk_BranchId);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Operation_PRTYBRNCHMAPG_Save]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    status = ds.Tables[0].Rows[0]["Message"].ToString();
                }
                else
                {
                    status = "We Are facing some internal issue, Please try again later";
                }
            }
            catch (Exception e)
            {
                status = e.Message.ToString();

            }
            return status;
        }

        [HttpGet]
        [Route("GetCompanyMaster")]
        public Api_CommonResponse GetCompanyMaster()
        {

            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_GetCompanyList_Select]");
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Company List";
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
            }
            return api_Response;
        }

        [HttpPost]
        [Route("SendCompanyLink")]
        public Api_CommonResponse SendCompanyLink(string EmailAddress, string pass)
        {

            try
            {
                var displayName = EmailAddress.Split('@')[0].ToString();

                var emailStatus = DBCS.SendMail(new SendEmail { Message = "CompanySendLink", RecieverDisplayName = displayName, RecieverEmailID = EmailAddress, Password = pass });

                if (emailStatus == "Email Sent")
                {
                    api_Response.responseCode = 0;
                    api_Response.message = emailStatus;
                    api_Response.statusCode = 1;
                }
                else
                {
                    api_Response.responseCode = 0;
                    api_Response.message = "Facing Some Internal Issue, Please Try after some time";
                    api_Response.statusCode = 1;
                }
            }
            catch (Exception e)
            {
            }
            return api_Response;
        }

        [HttpPost]
        [Route("SendFmRateChangeAlert")]
        public Api_CommonResponse SendFmRateChangeAlert(string EntryNo, string EntryDt, string FrghtInNmOf, string OwnBrkNm, decimal FrtRt, decimal ChngdFrtRt, string Rsn, int CnfrmById, string CnfrmBy, int Fk_Userid, int Fk_BranchId, string ChngdOn)
        {
            var status = "";
            var CompanyHead = "";
            var BranchHead = "";
            var ConfirmBy = "";
            var CreatedBy = "";
            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@CnfrmById", CnfrmById);
                param[1] = new SqlParameter("@Fk_Userid", Fk_Userid);
                param[2] = new SqlParameter("@Fk_BranchId", Fk_BranchId);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_CmpnyBranchUserEmailId_Get]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    CompanyHead = ds.Tables[0].Rows[0]["CompanyHead"].ToString();
                    BranchHead = ds.Tables[0].Rows[0]["BranchHead"].ToString();
                    ConfirmBy = ds.Tables[0].Rows[0]["ConfirmBy"].ToString();
                    CreatedBy = ds.Tables[0].Rows[0]["CreatedBy"].ToString();
                }
                else
                {
                    status = "We Are facing some internal issue, Please try again later";
                }
                var emailStatus = DBCS.SendMail(new SendEmail
                {
                    Message = "FM Rate Changed Alert",
                    RecieverDisplayName = CnfrmBy,
                    RecieverEmailID = ConfirmBy,
                    RecieverBranchHeadEmail = BranchHead,
                    RecieverCompnayHeadEmail = CompanyHead,
                    EntryNo = EntryNo,
                    EntryDt = EntryDt,
                    FrghtInNmOf = FrghtInNmOf,
                    OwnBrkNm = OwnBrkNm,
                    FrtRt = FrtRt,
                    ChngdFrtRt = ChngdFrtRt,
                    Rsn = Rsn,
                    CnfrmBy = CnfrmBy,
                    CreatedBy = CreatedBy,
                    ChngdOn = ChngdOn,
                });

                if (emailStatus == "Email Sent Successfully ...!!!")
                {
                    api_Response.responseCode = 0;
                    api_Response.message = emailStatus;
                    api_Response.statusCode = 1;
                }
                else
                {
                    api_Response.responseCode = 0;
                    api_Response.message = "Facing Some Internal Issue, Please Try after some time";
                    api_Response.statusCode = 1;
                }
            }
            catch (Exception e)
            {
            }
            return api_Response;
        }
        [HttpPost]
        [Route("SendFmReceiptRateChangeAlert")]
        public Api_CommonResponse SendFmReceiptRateChangeAlert(string EntryNo, string EntryDt, string FrghtInNmOf, string OwnBrkNm, decimal BkngRt, decimal AdtnAmnt, decimal WvAmnt, string WavedBy, int Fk_Userid, int Fk_BranchId, string ChngdOn)
        {
            var status = "";
            var CompanyHead = "";
            var BranchHead = "";
            var ConfirmBy = "";
            var CreatedBy = "";
            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@CnfrmById", 0);
                param[1] = new SqlParameter("@Fk_Userid", Fk_Userid);
                param[2] = new SqlParameter("@Fk_BranchId", Fk_BranchId);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_CmpnyBranchUserEmailId_Get]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    CompanyHead = ds.Tables[0].Rows[0]["CompanyHead"].ToString();
                    BranchHead = ds.Tables[0].Rows[0]["BranchHead"].ToString();
                    ConfirmBy = ds.Tables[0].Rows[0]["ConfirmBy"].ToString();
                    CreatedBy = ds.Tables[0].Rows[0]["CreatedBy"].ToString();
                }
                else
                {
                    status = "We Are facing some internal issue, Please try again later";
                }
                var emailStatus = DBCS.SendMail(new SendEmail
                {
                    Message = "FM Receipt Rate Changed Alert",
                    RecieverDisplayName = WavedBy,
                    RecieverEmailID = ConfirmBy,
                    RecieverBranchHeadEmail = BranchHead,
                    RecieverCompnayHeadEmail = CompanyHead,
                    EntryNo = EntryNo,
                    EntryDt = EntryDt,
                    FrghtInNmOf = FrghtInNmOf,
                    OwnBrkNm = OwnBrkNm,
                    BkngRt = BkngRt,
                    AdtnAmnt = AdtnAmnt,
                    WvAmnt = WvAmnt,
                    WavedBy = WavedBy,
                    CreatedBy = CreatedBy,
                    ChngdOn = ChngdOn,
                });

                if (emailStatus == "Email Sent Successfully ...!!!")
                {
                    api_Response.responseCode = 0;
                    api_Response.message = emailStatus;
                    api_Response.statusCode = 1;
                }
                else
                {
                    api_Response.responseCode = 0;
                    api_Response.message = "Facing Some Internal Issue, Please Try after some time";
                    api_Response.statusCode = 1;
                }
            }
            catch (Exception e)
            {
            }
            return api_Response;
        }

        [HttpGet]
        [Route("GetCompanyDetailsForEdit")]
        public Api_CommonResponse GetCompanyDetailsForEdit(int companyID)
        {

            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@companyID", companyID);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_GetCompanyDetailsForEdit_Update]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {

                    CompanyMaster company = new CompanyMaster();
                    List<ContactDtls> cntct = new List<ContactDtls>();
                    AddressDtls address = new AddressDtls();
                    company.CmpnyMstId = Convert.ToInt32(ds.Tables[0].Rows[0]["iCmpnyMstId"]);
                    company.CompanyName = ds.Tables[0].Rows[0]["sCmpnyNme"].ToString();
                    company.RegisteredOn = ds.Tables[0].Rows[0]["dRegstrdOn"].ToString();
                    company.RegisteredDoc = ds.Tables[0].Rows[0]["sRegstrdDoc"].ToString();
                    company.RegisteredDocumentName = ds.Tables[0].Rows[0]["SRegstrdDocName"].ToString();
                    company.Logo = ds.Tables[0].Rows[0]["sCmpnyLogo"].ToString();
                    company.GSTNumber = ds.Tables[0].Rows[0]["sCmpnyGSTNum"].ToString();
                    company.CINNumber = ds.Tables[0].Rows[0]["iCmpnyCINNum"].ToString();
                    company.PanCardNumber = ds.Tables[0].Rows[0]["sPanCdNum"].ToString();
                    company.EmailURL = ds.Tables[0].Rows[0]["sEmail"].ToString();
                    company.WebsiteURL = ds.Tables[0].Rows[0]["sWebsiteURL"].ToString();
                    company.bIsActive = Convert.ToBoolean(ds.Tables[0].Rows[0]["bIsActive"].ToString());

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        for (int i = 0; i <= ds.Tables[1].Rows.Count - 1; i++)
                        {
                            ContactDtls member = new ContactDtls();
                            member.CntctPerson = ds.Tables[1].Rows[i]["sCntctDetlspersonNme"].ToString();
                            member.CntctNumber = ds.Tables[1].Rows[i]["sCntctNum"].ToString();
                            member.EmailId = ds.Tables[1].Rows[i]["sCntcEmail"].ToString();
                            member.Primary = Convert.ToBoolean(ds.Tables[1].Rows[i]["bPrimary"]);
                            cntct.Add(member);
                        }
                        company.ContactDtls = cntct;
                    }
                    else
                    {
                        company.ContactDtls = null;
                    }
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        address.AddrssDtlsId = Convert.ToInt16(ds.Tables[2].Rows[0]["iCmpnyMstAddressDtls"]);
                        address.Country = ds.Tables[2].Rows[0]["sAddressCntry"].ToString();
                        address.State = ds.Tables[2].Rows[0]["sAddressState"].ToString();
                        address.City = ds.Tables[2].Rows[0]["sAddressCity"].ToString();
                        address.Pincode = ds.Tables[2].Rows[0]["sPincd"].ToString();
                        address.Address = ds.Tables[2].Rows[0]["sAddress"].ToString();
                        company.AddressDtls = address;
                    }
                    else
                    {
                        company.AddressDtls = null;
                    }

                    api_Response.message = "Company Details";
                    api_Response.statusCode = 1;
                    api_Response.data = company;
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
            }
            return api_Response;
        }


        [HttpPost]
        [Route("UpdateCompanyDetails")]
        public Api_CommonResponse UpdateCompanyDetails(CompanyMaster com)
        {
            try
            {

                SqlParameter[] param = new SqlParameter[13];
                param[0] = new SqlParameter("@sCmpnyNme", com.CompanyName);
                param[1] = new SqlParameter("@sCmpnyLogo", com.Logo);
                param[2] = new SqlParameter("@dRegstrdOn", com.RegisteredOn);
                param[3] = new SqlParameter("@SRegstrdDocName", com.RegisteredDocumentName);
                param[4] = new SqlParameter("@sRegstrdDoc", com.RegisteredDoc);
                param[5] = new SqlParameter("@iCmpnyCINNum", com.CINNumber);
                param[6] = new SqlParameter("@sCmpnyGSTNum", com.GSTNumber);
                param[7] = new SqlParameter("@sPanCdNum", com.PanCardNumber);
                param[8] = new SqlParameter("@sWebsiteURL", com.WebsiteURL);
                param[9] = new SqlParameter("@sEmail", com.EmailURL);
                param[10] = new SqlParameter("@iCmpnyMstId", com.CmpnyMstId);
                param[11] = new SqlParameter("@iFinancialMstId", 0);
                param[12] = new SqlParameter("@bIsActive", com.bIsActive);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_UpdateCompanyDetails_Update]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {

                    if (com.ContactDtls != null && com.ContactDtls.Count > 0)
                    {
                        AddCompanyMembers(new ContactDtls(), com.CmpnyMstId, "D");

                        foreach (var item in com.ContactDtls)
                        {
                            AddCompanyMembers(item, com.CmpnyMstId, "I");
                        }
                        AddCompanyAddressDetails(com.AddressDtls, com.CmpnyMstId, "U");
                    }
                    api_Response.message = ds.Tables[0].Rows[0]["Message"].ToString();
                    api_Response.statusCode = Convert.ToInt32(ds.Tables[0].Rows[0]["StatusCode"]);
                    api_Response.responseCode = 1;
                }
                else
                {
                    api_Response.message = "No Data Available";
                    api_Response.statusCode = 400;
                    api_Response.responseCode = 0;
                }
            }
            catch (Exception e)
            {
                api_Response.message = e.Message.ToString();
                api_Response.statusCode = 400;
                api_Response.responseCode = 0;

            }
            return api_Response;
        }

        [HttpPost]
        [Route("DataControlPOMailSend")]
        public Api_CommonResponse DataControlPOMailSend(int subMenuId, string checkIds = "", int iFk_UserId = 0)
        {
            Api_CommonResponse api_Response = new Api_CommonResponse();
            try
            {
                // Split checkIds into a list of individual IDs
                List<string> poIds = checkIds.Split(',').ToList();

                foreach (var poId in poIds)
                {
                    if (string.IsNullOrWhiteSpace(poId))
                        continue; // Skip empty values

                    // Prepare SQL parameters for the stored procedure
                    SqlParameter[] param = new SqlParameter[3];
                    param[0] = new SqlParameter("@PoId", poId.Trim()); // Pass single ID
                    param[1] = new SqlParameter("@subMenuId", subMenuId);
                    param[2] = new SqlParameter("@iFk_UserId", iFk_UserId);

                    // Execute the stored procedure
                    DataSet ds = DBOperation.FillDataSet("[dbo].[USP_PO_GetEmailDetails_Get]", param);

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        var row = ds.Tables[0].Rows[0];

                        string? receiverMail = row["ReceiverMail"].ToString();
                        string? cc = row["CC"].ToString();
                        string? bcc = row["BCC"].ToString();
                        string? subject = row["SubjectEmail"].ToString();
                        string? body = row["Body"].ToString();
                        string? poNo = row["PONO"].ToString();
                        string? poDate = row["PODate"].ToString();
                        string? partyName = row["PartyName"].ToString();
                        string? prprdBy = row["sPreparedBy"].ToString();
                        string? aprvdBy = row["sApprovedBy"].ToString();

                        // Prepare placeholders for dynamic replacement
                        var replacements = new Dictionary<string, string>
{
    { "txnNumber", poNo },
    { "txnDate", poDate },
    { "party", partyName },
    { "prprdBy", prprdBy },
    { "aprvdBy", aprvdBy }
};

                        // Replace placeholders in subject and body
                        foreach (var replacement in replacements)
                        {
                            if (replacement.Key == "txnNumber" || replacement.Key == "txnDate")
                            {
                                subject = subject.Replace("${(" + replacement.Key + ")!\"}", replacement.Value);
                                body = body.Replace("${(" + replacement.Key + ")!\"}", replacement.Value);
                            }
                            else
                            {
                                string placeholder = $"${{{replacement.Key}!}}"; // Consistent placeholder format
                                subject = subject.Replace(placeholder, replacement.Value);
                                body = body.Replace(placeholder, replacement.Value);
                            }
                        }


                        // Prepare email content
                        var emailStatus = DBCS.SendPOMail(new SendEmail
                        {
                            Message = body,
                            Subject = subject,
                            ReceiverEmail = receiverMail,
                            CC = cc,
                            BCC = bcc,
                            PONO = poNo,
                            PODate = poDate,
                            PartyName = partyName
                        });

                        // Handle email sending response
                        if (emailStatus != "Email sent successfully!")
                        {
                            api_Response.responseCode = -1;
                            api_Response.message = "Facing Some Internal Issue, Please Try after some time";
                            api_Response.statusCode = 0;
                            return api_Response; // Stop processing if an error occurs
                        }
                    }
                }

                // If all emails are sent successfully
                api_Response.responseCode = 0;
                api_Response.message = "Emails sent successfully!";
                api_Response.statusCode = 1;
            }
            catch (Exception ex)
            {
                api_Response.responseCode = -1;
                api_Response.message = "An error occurred: " + ex.Message;
                api_Response.statusCode = 0;
            }

            return api_Response;
        }


        [HttpPost]
        [Route("POMailSend")]
        public Api_CommonResponse POMailSend(int subMenuId, int iFk_POId = 0, int iFk_UserId = 0, string EventType = "")
        {
            Api_CommonResponse api_Response = new Api_CommonResponse();

            try
            {
                // Prepare SQL parameters for the stored procedure
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@PoId", iFk_POId);
                param[1] = new SqlParameter("@subMenuId", subMenuId);
                param[2] = new SqlParameter("@iFk_UserId", iFk_UserId);
                param[3] = new SqlParameter("@EventType", EventType);

                // Execute the stored procedure
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_POSchedule_GetEmailDetails_Get]", param);

                // If no data found or error message exists, return early
                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0 ||
                    Convert.ToInt32(ds.Tables[0].Rows[0]["StatusCode"]) <= 0)
                {
                    api_Response.responseCode = -1;
                    api_Response.message = ds.Tables[0].Rows[0]["Message"].ToString();
                    api_Response.statusCode = 0;
                    return api_Response;
                }

                // Extract the first row
                var row = ds.Tables[0].Rows[0];

                string receiverMail = row["ReceiverMail"]?.ToString() ?? "";
                string cc = row["CC"]?.ToString() ?? "";
                string bcc = row["BCC"]?.ToString() ?? "";
                string subject = row["SubjectEmail"]?.ToString() ?? "";
                string body = row["Body"]?.ToString() ?? "";
                string poNo = row["PONO"]?.ToString() ?? "";
                string poDate = row["PODate"]?.ToString() ?? "";
                string partyName = row["PartyName"]?.ToString() ?? "";
                string prprdBy = row["sPreparedBy"]?.ToString() ?? "";
                string aprvdBy = row["sApprovedBy"]?.ToString() ?? "";

                // If no receiver email is found, return an error
                if (string.IsNullOrEmpty(receiverMail))
                {
                    api_Response.responseCode = -1;
                    api_Response.message = "Receiver email is missing. Cannot send email.";
                    api_Response.statusCode = 0;
                    return api_Response;
                }

                // Prepare placeholders for dynamic replacement
                var replacements = new Dictionary<string, string>
        {
            { "txnNumber", poNo },
            { "txnDate", poDate },
            { "party", partyName },
            { "prprdBy", prprdBy },
            { "aprvdBy", aprvdBy }
        };

                // Replace placeholders in subject and body
                foreach (var replacement in replacements)
                {
                    if (replacement.Key == "txnNumber" || replacement.Key == "txnDate")
                    {
                        subject = subject.Replace("${(" + replacement.Key + ")!\"}", replacement.Value);
                        body = body.Replace("${(" + replacement.Key + ")!\"}", replacement.Value);
                    }
                    else
                    {
                        string placeholder = $"${{{replacement.Key}!}}"; // Consistent placeholder format
                        subject = subject.Replace(placeholder, replacement.Value);
                        body = body.Replace(placeholder, replacement.Value);
                    }
                }

                // Send the email
                var emailStatus = DBCS.SendPOMail(new SendEmail
                {
                    Message = body,
                    Subject = subject,
                    ReceiverEmail = receiverMail,
                    CC = cc,
                    BCC = bcc,
                    PONO = poNo,
                    PODate = poDate,
                    PartyName = partyName
                });

                // Handle email sending response
                if (emailStatus != "Email sent successfully!")
                {
                    api_Response.responseCode = -1;
                    api_Response.message = "Email sending failed. Please try again later.";
                    api_Response.statusCode = 0;
                    return api_Response;
                }

                // Success response with ReceiverMail
                api_Response.responseCode = 0;
                api_Response.message = $"Email sent successfully to {receiverMail}";
                api_Response.statusCode = 1;
            }
            catch (Exception ex)
            {
                api_Response.responseCode = -1;
                api_Response.message = "An error occurred: " + ex.Message;
                api_Response.statusCode = 0;
            }

            return api_Response;
        }






        //[HttpGet]
        //[Route("ActiveInactiveCompany")]
        //public Api_CommonResponse ActiveInactiveCompany(int status, string userID)
        //{
        //    try
        //    {
        //        SqlParameter[] param = new SqlParameter[2];
        //        param[0] = new SqlParameter("@status", status);
        //        param[1] = new SqlParameter("@userID", userID);

        //        DataSet ds = DBOperation.FillDataSet("[dbo].[USP_MASTER_ChangeCompanyStatus_Update]", param);
        //        if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
        //        {
        //            api_Response.responseCode = 0;
        //            api_Response.message = ds.Tables[0].Rows[0]["Message"].ToString();
        //            api_Response.statusCode = Convert.ToInt32(ds.Tables[0].Rows[0]["StatusCode"].ToString());
        //            api_Response.data = null;
        //        }
        //        else
        //        {
        //            api_Response.responseCode = 1;
        //            api_Response.message = $"We are facing server issue at the moment please try again...";
        //            api_Response.statusCode = -1;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //    return api_Response;
        //}
    }
}
