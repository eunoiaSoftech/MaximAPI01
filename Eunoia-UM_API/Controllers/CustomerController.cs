using Eunoia_UM_API.Helper;
using Eunoia_UM_API.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel.Design;
using Eunoia_UM.Models;

namespace Eunoia_UM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {


        Api_CommonResponse api_Response = new Api_CommonResponse();

        [HttpPost]
        [Route("AddCustomer")]
        public Api_CommonResponse AddCustomer(CustomerModel com)
        {
            try
            {
                var customerID = 0;

                SqlParameter[] param = new SqlParameter[28];
                param[0] = new SqlParameter("@sPartyName", com.sPartyName);
                param[1] = new SqlParameter("@iFk_ReferredBy", com.iFk_ReferredBy);
                param[2] = new SqlParameter("@iStatus", com.iStatus);
                param[3] = new SqlParameter("@iFk_Category", com.iFk_Category);
                param[4] = new SqlParameter("@iFk_IndustryType", com.iFk_IndustryType);
                param[5] = new SqlParameter("@iFk_Type", com.iFk_Type);
                param[6] = new SqlParameter("@sPANNo", com.sPANNo);
                param[7] = new SqlParameter("@sWebsite", com.sWebsite);
                param[8] = new SqlParameter("@sEmailID", com.sEmailID);
                param[9] = new SqlParameter("@iFk_PreparedBy", com.iFk_PreparedBy);
                param[10] = new SqlParameter("@iFk_ApprovedBy", com.iFk_ApprovedBy);
                param[11] = new SqlParameter("@sCategory", com.sCategory);
                param[12] = new SqlParameter("@iMAXID", com.iMAXID);
                param[13] = new SqlParameter("@iFk_CompanyID", com.iFk_CompanyID);
                param[14] = new SqlParameter("@iFk_BranchID", com.iFk_BranchID);
                param[15] = new SqlParameter("@iFk_YearId", com.iFk_YearId);
                param[16] = new SqlParameter("@ifk_Userid", com.ifk_Userid);
                param[17] = new SqlParameter("@sName", com.sName);
                param[18] = new SqlParameter("@iGrpLdgrId", com.iGrpLdgrId);
                param[19] = new SqlParameter("@iIsLedgrTag", com.iIsLedgrTag);
                param[20] = new SqlParameter("@iIsTdsApcbl", com.iIsTdsApcbl);
                param[21] = new SqlParameter("@iFk_NtPymtId", com.iFk_NtPymtId);
                param[22] = new SqlParameter("@iFk_DeductTypId", com.iFk_DeductTypId);
                param[23] = new SqlParameter("@bInActive", com.bInActive);
                param[24] = new SqlParameter("@sReason", com.sReason);
                param[25] = new SqlParameter("@dtEffctvDate", com.dtEffctvDate);
                param[26] = new SqlParameter("@sPanHolder", com.sPanHolder);
                param[27] = new SqlParameter("@iPanStatus", com.iPanStatus);
                DataSet ds = DBOperation.FillDataSet("USP_Master_Customer_Save", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    customerID = Convert.ToInt32(ds.Tables[0].Rows[0]["CustomerID"]);

                    if (com.ContactDetails != null && com.ContactDetails.Count > 0)
                    {
                        foreach (var item in com.ContactDetails)
                        {
                            AddCustomerMembers(item, customerID, "Insert");
                        }
                    }
                    if (com.Brnchdtls1 != null && com.Brnchdtls1.Count > 0)
                    {
                        foreach (var item in com.Brnchdtls1)
                        {
                            AddBranchDetails(item, customerID);
                        }
                        //AddSupplierAddressDetails(supp.AddressDtls, supplierID, "I");
                    }
                    if (com.TDSList != null && com.TDSList.Count > 0)
                    {
                        foreach (var item in com.TDSList)
                        {
                            AddCustomerTDSDetails(item, customerID);
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
        [Route("AddCustomerTDSDetails")]
        public string AddCustomerTDSDetails(TDSList member, int partyId)
        {
            var status = "";
            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@iFK_PartyId", partyId);
                param[1] = new SqlParameter("@dtEffctvDate", member.dtEffctvDate);
                param[2] = new SqlParameter("@sTDSUrl", member.sDocmntUrl);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Party_PRTYTDSDECLR_Save]", param);
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
        [Route("AddCustomerMembers")]
        public string AddCustomerMembers(ContactDetails member, int customerId, string type)
        {
            var status = "";
            try
            {

                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@iFK_PartyId", customerId);
                param[1] = new SqlParameter("@sCntctpersonNme", member.CntctPerson);
                param[2] = new SqlParameter("@sCntctNumber", member.CntctNumber);
                param[3] = new SqlParameter("@sEmailId", member.EmailId);
                param[4] = new SqlParameter("@bIsPrimary", member.Primary);


                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_PRTYCNTCDET_Save]", param);
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
        public string AddBranchDetails(Brnchdtls1 member, int partyId)
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

        [HttpPost]
        [Route("UpdateCustomer")]
        public Api_CommonResponse UpdateCustomer(CustomerModel com)
        {
            try
            {
                var customerID = 0;

                SqlParameter[] param = new SqlParameter[26];
                param[0] = new SqlParameter("@iPk_PartyId", com.iPk_PartyId);
                param[1] = new SqlParameter("@sPartyName", com.sPartyName);
                param[2] = new SqlParameter("@iFk_ReferredBy", com.iFk_ReferredBy);
                param[3] = new SqlParameter("@iStatus", com.iStatus);
                param[4] = new SqlParameter("@iFk_Category", com.iFk_Category);
                param[5] = new SqlParameter("@iFk_IndustryType", com.iFk_IndustryType);
                param[6] = new SqlParameter("@iFk_Type", com.iFk_Type);
                param[7] = new SqlParameter("@sPANNo", com.sPANNo);
                param[8] = new SqlParameter("@sWebsite", com.sWebsite);
                param[9] = new SqlParameter("@sEmailID", com.sEmailID);
                param[10] = new SqlParameter("@iFk_PreparedBy", com.iFk_PreparedBy);
                param[11] = new SqlParameter("@iFk_ApprovedBy", com.iFk_ApprovedBy);
                param[12] = new SqlParameter("@sCategory", com.sCategory);
                param[13] = new SqlParameter("@iMAXID", com.iMAXID);
                param[14] = new SqlParameter("@iFk_CompanyID", com.iFk_CompanyID);
                param[15] = new SqlParameter("@iFk_BranchID", com.iFk_BranchID);
                param[16] = new SqlParameter("@iGrpLdgrId", com.iGrpLdgrId);
                param[17] = new SqlParameter("@iIsLedgrTag", com.iIsLedgrTag);
                param[18] = new SqlParameter("@iIsTdsApcbl", com.iIsTdsApcbl);
                param[19] = new SqlParameter("@iFk_NtPymtId", com.iFk_NtPymtId);
                param[20] = new SqlParameter("@iFk_DeductTypId", com.iFk_DeductTypId);
                param[21] = new SqlParameter("@bInActive", com.bInActive);
                param[22] = new SqlParameter("@sReason", com.sReason);
                param[23] = new SqlParameter("@dtEffctvDate", com.dtEffctvDate);
                param[24] = new SqlParameter("@sPanHolder", com.sPanHolder);
                param[25] = new SqlParameter("@iPanStatus", com.iPanStatus);


                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_PRTYMST_Update]", param);

                customerID = Convert.ToInt32(com.iPk_PartyId);

                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    /*customerID = Convert.ToInt32(ds.Tables[0].Rows[0]["PK_PartyContactDetails"]);*/

                    if (com.ContactDetails != null && com.ContactDetails.Count > 0)
                    {
                        foreach (var item in com.ContactDetails)
                        {
                            UpdateCustomerMembers(item, customerID, "Insert");
                        }
                    }

                    if (com.Brnchdtls1 != null && com.Brnchdtls1.Count > 0)
                    {
                        foreach (var item in com.Brnchdtls1)
                        {
                            AddBranchDetails(item, customerID);
                        }
                        //AddSupplierAddressDetails(supp.AddressDtls, supplierID, "I");
                    }

                    if (com.TDSList != null && com.TDSList.Count > 0)
                    {
                        foreach (var item in com.TDSList)
                        {
                            AddCustomerTDSDetails(item, customerID);
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
        [Route("UpdateCustomerMembers")]
        public string UpdateCustomerMembers(ContactDetails member, int customerId, string type)
        {
            var status = "";
            try
            {

                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@iCustMstId", customerId);
                param[1] = new SqlParameter("@CntctPerson", member.CntctPerson);
                param[2] = new SqlParameter("@CntctNumber", member.CntctNumber);
                param[3] = new SqlParameter("@EmailId", member.EmailId);
                param[4] = new SqlParameter("@Primary", member.Primary);


                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_PRTYCNTCDET_Update]", param);
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
        [Route("GetCustomerMaster")]
        public Api_CommonResponse GetCustomerMaster()
        {
            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_MASTER_Customer_Get]");
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
                throw e;
            }
            return api_Response;
        }


        [HttpGet]
        [Route("GetConsigneeMaster")]
        public Api_CommonResponse GetConsigneeMaster()
        {
            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_MASTER_Consignee_Get]");
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Consignee List";
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
                    company.FinancialYear = Convert.ToInt32(ds.Tables[0].Rows[0]["iFinancialMstId"].ToString());
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

        [HttpGet]
        [Route("BillingdetailsList")]
        public Api_CommonResponse BillingdetailsList(int customerID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@FK_PartyID", customerID);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_MasterGetPRTYBILLINGDET_Get]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Billing Details List";
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
        [Route("Savebilldetails")]
        public Api_CommonResponse Savebilldetails(Billingdetails Billdata)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[14];
                param[0] = new SqlParameter("@FK_PartyID", Billdata.FK_PartyID);
                param[1] = new SqlParameter("@FK_DespatchType", Billdata.FK_DespatchType);
                param[2] = new SqlParameter("@ContactPersone", Billdata.ContactPersone);
                param[3] = new SqlParameter("@StreetLane", Billdata.StreetLane);
                param[4] = new SqlParameter("@FK_CountryID", Billdata.FK_CountryID);
                param[5] = new SqlParameter("@FK_StateID", Billdata.FK_StateID);
                param[6] = new SqlParameter("@FK_CityID", Billdata.FK_CityID);
                param[7] = new SqlParameter("@FK_AreaID", Billdata.FK_AreaID);
                param[8] = new SqlParameter("@PinCode", Billdata.PinCode);
                param[9] = new SqlParameter("@TelephoneNo", Billdata.TelephoneNo);
                param[10] = new SqlParameter("@EmailID", Billdata.EmailID);
                param[11] = new SqlParameter("@FK_RegistrationID", Billdata.FK_RegistrationID);
                param[12] = new SqlParameter("@FK_GstParty_TypeID", Billdata.FK_GstParty_TypeID);
                param[13] = new SqlParameter("@GstIdentificationNo", Billdata.GstIdentificationNo);
                //param[14] = new SqlParameter("@GST_effdt", Billdata.GST_effdt);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_MasterPRTYBILLINGDET_Save]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.responseCode = 0;
                    api_Response.message = ds.Tables[0].Rows[0]["Message"].ToString();
                    api_Response.statusCode = Convert.ToInt32(ds.Tables[0].Rows[0]["StatusCode"]);
                }
                else
                {
                    api_Response.responseCode = 1;
                    api_Response.message = $"We are facing server issue at the moment please try again...";
                    api_Response.statusCode = -1;
                    api_Response.data = null;
                }
            }
            catch (Exception ex)
            {
                api_Response.statusCode = 402;
            }
            return api_Response;
        }

        [HttpGet]
        [Route("GetGSTPartytype")]
        public Api_CommonResponse GetGSTPartytype()
        {

            try
            {
                DataSet ds = DBOperation.FillDataSet("USP_GetGSTPartyType");
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Doc List";
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

        [HttpGet]
        [Route("GetGSTRegistrationtype")]
        public Api_CommonResponse GetGSTRegistrationtype()
        {

            try
            {
                DataSet ds = DBOperation.FillDataSet("USP_GetGSTRegistrationType");
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Doc List";
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

        [HttpGet]
        [Route("getConsigneeNames")]
        public Api_CommonResponse getConsigneeNames()
        {

            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_GetConsignee_Name]");
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Doc List";
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

        [HttpGet]
        [Route("getCustomerDataForEdit")]
        public Api_CommonResponse getCustomerDataForEdit(int customerID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@iPk_PartyId", customerID);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_MASTER_Customer_Get_For_Edit]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {

                    CustomerModel customer = new CustomerModel();
                    List<ContactDetails> cntct = new List<ContactDetails>();
                    List<TDSList> tds = new List<TDSList>();

                    AddressDtls address = new AddressDtls();
                    customer.iPk_PartyId = Convert.ToInt32(ds.Tables[0].Rows[0]["iPk_PartyId"]);
                    customer.iFk_IndustryType = Convert.ToInt32(ds.Tables[0].Rows[0]["iFk_IndustryType"]);
                    //customer.iGrpLdgrId = Convert.ToInt32(ds.Tables[0].Rows[0]["iGrpLdgrId"]);
                    customer.sPartyName = ds.Tables[0].Rows[0]["sPartyName"].ToString();
                    customer.sName = ds.Tables[0].Rows[0]["sName"].ToString();
                    customer.Group = ds.Tables[0].Rows[0]["Group"].ToString();
                    customer.NatureofPayment = ds.Tables[0].Rows[0]["NatureofPayment"].ToString();
                    customer.DeducteeType = ds.Tables[0].Rows[0]["DeducteeType"].ToString();
                    customer.dtInsertDate = ds.Tables[0].Rows[0]["dtInsertDate"].ToString();
                    customer.sPANNo = ds.Tables[0].Rows[0]["sPANNo"].ToString();
                    customer.sWebsite = ds.Tables[0].Rows[0]["sWebsite"].ToString();
                    customer.sEmailID = ds.Tables[0].Rows[0]["sEmailID"].ToString();
                    customer.sPanHolder = ds.Tables[0].Rows[0]["sPanHolder"].ToString();
                    customer.iIsLedgrTag = Convert.ToInt32(ds.Tables[0].Rows[0]["iIsLedgrTag"].ToString());
                    customer.iIsTdsApcbl = Convert.ToInt32(ds.Tables[0].Rows[0]["iIsTdsApcbl"].ToString());
                    customer.dtEffctvDate = ds.Tables[0].Rows[0]["dtEffctvDate"].ToString();
                    customer.iGrpLdgrId = Convert.ToInt32(ds.Tables[0].Rows[0]["iGrpLdgrId"].ToString());
                    customer.iFk_NtPymtId = Convert.ToInt32(ds.Tables[0].Rows[0]["iFk_NtPymtId"].ToString());
                    customer.iFk_DeductTypId = Convert.ToInt32(ds.Tables[0].Rows[0]["iFk_DeductTypId"].ToString());
                    customer.iFk_ApprovedBy = Convert.ToInt32(ds.Tables[0].Rows[0]["iFk_ApprovedBy"].ToString());
                    customer.bInActive = Convert.ToInt32(ds.Tables[0].Rows[0]["bInActive"]);
                    customer.iPanStatus = Convert.ToInt32(ds.Tables[0].Rows[0]["iPanStatus"]);



                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        for (int i = 0; i <= ds.Tables[1].Rows.Count - 1; i++)
                        {
                            ContactDetails member = new ContactDetails();
                            member.CntctPerson = ds.Tables[1].Rows[i]["CntctPerson"].ToString();
                            member.CntctNumber = ds.Tables[1].Rows[i]["CntctNumber"].ToString();
                            member.EmailId = ds.Tables[1].Rows[i]["EmailId"].ToString();
                            cntct.Add(member);
                        }
                        customer.ContactDetails = cntct;
                    }
                    else
                    {
                        customer.ContactDetails = null;
                    }
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        for (int i = 0; i <= ds.Tables[2].Rows.Count - 1; i++)
                        {
                            TDSList member = new TDSList();
                            member.dtEffctvDate = ds.Tables[2].Rows[i]["dtEffctvDate"].ToString();
                            member.sDocmntUrl = ds.Tables[2].Rows[i]["sDocmntUrl"].ToString();
                            tds.Add(member);
                        }
                        customer.TDSList = tds;
                    }
                    else
                    {
                        customer.TDSList = null;
                    }

                    api_Response.message = "Customer Details";
                    api_Response.statusCode = 1;
                    api_Response.data = customer;
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


        [HttpGet]
        [Route("GetState")]
        public Api_CommonResponse GetState()
        {
            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_MasterGet_State]");
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
                throw e;
            }
            return api_Response;
        }


        [HttpGet]
        [Route("GetCity")]
        public Api_CommonResponse GetCity()
        {
            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_MasterGet_State_City]");
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
                throw e;
            }
            return api_Response;
        }


        [HttpGet]
        [Route("GetPlantdetailsList")]
        public Api_CommonResponse GetPlantdetailsList(int customerID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@customerID", customerID);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_GetPLNTDET_Get]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Billing Details List";
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
        [Route("GetTagConsigneeList")]
        public Api_CommonResponse GetTagConsigneeList()
        {
            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_MASTER_Get_Data_For_Consignee_Index]");
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Consignee name List";
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
    }
}
