using Eunoia_UM_API.Helper;
using Eunoia_UM_API.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;

namespace Eunoia_UM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : Controller
    {
        Api_CommonResponse api_Response = new Api_CommonResponse();
        [HttpPost]
        [Route("AddSupplier")]
        public Api_CommonResponse AddSupplier(SupplierModel supplier)
        {
            try
            {
                var supplierID = 0;
                SqlParameter[] param = new SqlParameter[30];
                param[0] = new SqlParameter("@sPartyName", supplier.sPartyName);
                param[1] = new SqlParameter("@iFk_ReferredBy", supplier.iFk_ReferredBy);
                param[2] = new SqlParameter("@iStatus", supplier.iStatus);
                param[3] = new SqlParameter("@iFk_Category", supplier.iFk_Category);
                param[4] = new SqlParameter("@iFk_IndustryType", supplier.iFk_IndustryType);
                param[5] = new SqlParameter("@iFk_Type", supplier.iFk_Type);
                param[6] = new SqlParameter("@sPANNo", supplier.sPANNo);
                param[7] = new SqlParameter("@sWebsite", supplier.sWebsite);
                param[8] = new SqlParameter("@sEmailID", supplier.sEmailID);
                param[9] = new SqlParameter("@iFk_PreparedBy", supplier.iFk_PreparedBy);
                param[10] = new SqlParameter("@iFk_ApprovedBy", supplier.iFk_ApprovedBy);
                param[11] = new SqlParameter("@sCategory", supplier.sCategory);
                param[12] = new SqlParameter("@iMAXID", supplier.iMAXID);
                param[13] = new SqlParameter("@iFk_CompanyID", supplier.iFk_CompanyID);
                param[14] = new SqlParameter("@iFk_BranchID", supplier.iFk_BranchID);
                param[15] = new SqlParameter("@iFk_YearId", supplier.iFk_YearId);
                param[16] = new SqlParameter("@ifk_Userid", supplier.ifk_Userid);
                param[17] = new SqlParameter("@sName", supplier.sName);
                param[18] = new SqlParameter("@iGrpLdgrId", supplier.iGrpLdgrId);
                param[19] = new SqlParameter("@iIsLedgrTag", supplier.iIsLedgrTag);
                param[20] = new SqlParameter("@iIsTdsApcbl", supplier.iIsTdsApcbl);
                param[21] = new SqlParameter("@iFk_NtPymtId", supplier.iFk_NtPymtId);
                param[22] = new SqlParameter("@iFk_DeductTypId", supplier.iFk_DeductTypId);
                param[23] = new SqlParameter("@bInActive", supplier.bInActive);
                param[24] = new SqlParameter("@sReason", supplier.sReason);
                param[25] = new SqlParameter("@MSMERegNo", supplier.MSMERegNo);
                param[26] = new SqlParameter("@dtEffctvDate", supplier.dtEffctvDate);
                param[27] = new SqlParameter("@iFk_PtrlUndr", supplier.iFk_PtrlUndr);
                param[28] = new SqlParameter("@sPanHolder", supplier.sPanHolder);
                param[29] = new SqlParameter("@iPanStatus", supplier.iPanStatus);


                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_Supplier_Save]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    supplierID = Convert.ToInt32(ds.Tables[0].Rows[0]["SupplierID"]);

                    if (supplier.ContactDtls != null && supplier.ContactDtls.Count > 0)
                    {
                        foreach (var item in supplier.ContactDtls)
                        {
                            AddSupplierMembers(item, supplierID, "Insert");
                        }
                        //AddSupplierAddressDetails(supp.AddressDtls, supplierID, "I");
                    }
                    if (supplier.Brnchdtls != null && supplier.Brnchdtls.Count > 0)
                    {
                        foreach (var item in supplier.Brnchdtls)
                        {
                            AddBranchDetails(item, supplierID);
                        }
                        //AddSupplierAddressDetails(supp.AddressDtls, supplierID, "I");
                    }
                    if (supplier.TDSList != null && supplier.TDSList.Count > 0)
                    {
                        foreach (var item in supplier.TDSList)
                        {
                            AddSupplierTDSDetails(item, supplierID);
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
        [Route("AddSupplierTDSDetails")]
        public string AddSupplierTDSDetails(TDSList member, int partyId)
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
        [Route("AddSupplierMembers")]
        public string AddSupplierMembers(SupplierContactDtls member, int partyId, string type)
        {
            var status = "";
            try
            {
                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@iFK_PartyId", partyId);
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
        public string AddBranchDetails(Brnchdtls member, int partyId)
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
        [Route("GetSupplier")]
        public Api_CommonResponse GetSupplier()
        {
            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_Supplier_Get]");
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

        [HttpGet]
        [Route("GetIndTypeList")]
        public Api_CommonResponse GetIndTypeList()
        {
            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_MASTER_Supplier_IndType_Get]");
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Industry List";
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
        [Route("GetSupplierBank")]
        public Api_CommonResponse GetSupplierBank(int supplierID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@FK_PartyID", supplierID);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_Supplier_GetSupplierBank]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Supplier bank List";
                    api_Response.statusCode = 200;
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
        [Route("SaveSupplierBank")]
        public Api_CommonResponse SaveSupplierBank(List<SupplierBankDetails> bank)
        {
            try
            {
                DataSet ds = new DataSet();
                if (bank != null && bank.Count > 0)
                {
                    foreach (var item in bank)
                    {
                        SqlParameter[] param = new SqlParameter[8];
                        param[0] = new SqlParameter("@FK_PartyID", item.FK_PartyID);
                        param[1] = new SqlParameter("@FK_AccountType", item.FK_AccountType);
                        param[2] = new SqlParameter("@AccountNo", item.AccountNo);
                        param[3] = new SqlParameter("@BankName", item.BankName);
                        param[4] = new SqlParameter("@BranchName", item.BranchName);
                        param[5] = new SqlParameter("@ISFCNo", item.IFSCNo);
                        param[6] = new SqlParameter("@IsPrimary", item.IsPrimary);
                        param[7] = new SqlParameter("@IsActive", item.IsActive);

                        ds = DBOperation.FillDataSet("[dbo].[USP_Master_PRTYBNKDET_Save]", param);
                    }
                }
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
            catch (Exception e)
            {
                throw e;
            }
            return api_Response;
        }

        [HttpPost]
        [Route("AddAdvDetails")]
        public Api_CommonResponse AddAdvDetails(AdvDetials adv)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@FK_PartyID", adv.FK_PartyID);
                param[1] = new SqlParameter("@sCreatedBy", adv.sCreatedBy);
                param[2] = new SqlParameter("@iFk_FinancialYrId", adv.iFk_FinancialYrId);
                param[3] = new SqlParameter("@iFk_BranchId", adv.iFk_BranchId);
                param[4] = new SqlParameter("@iAdvTruck", adv.iAdvTruck);
                param[5] = new SqlParameter("@iAdvTrailer", adv.iAdvTrailer);
                param[6] = new SqlParameter("@iCreditPeriod", adv.iCreditPeriod);
                param[7] = new SqlParameter("@dtEffectiveDate", adv.dtEffectiveDate);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_MASTER_Supplier_AdvDet_Save]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
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

        [HttpGet]
        [Route("GetAdvDetails")]
        public Api_CommonResponse GetAdvDetails(int supplierID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@FK_PartyID", supplierID);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_Supplier_GetAdvDetails]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Advance details list";
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
        [Route("BillingdetailsList")]
        public Api_CommonResponse BillingdetailsList(int supplierID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@FK_PartyID", supplierID);
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
        public Api_CommonResponse Savebilldetails(SupplierBillingDetails Billdata)
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
        [Route("GetBillingDetailsForEdit")]
        public Api_CommonResponse GetBillingDetailsForEdit(int id)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@id", id);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_PRTYBILLINDET_GetDetailsForEdit]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Advance details list";
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
        [Route("UpdateBillingDetails")]
        public Api_CommonResponse UpdateBillingDetails(SupplierBillingDetails Billdata)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[15];
                param[0] = new SqlParameter("@PK_PartyDespatchDetails", Billdata.PK_PartyDespatchDetails);
                param[1] = new SqlParameter("@FK_PartyID", Billdata.FK_PartyID);
                param[2] = new SqlParameter("@FK_DespatchType", Billdata.FK_DespatchType);
                param[3] = new SqlParameter("@ContactPersone", Billdata.ContactPersone);
                param[4] = new SqlParameter("@StreetLane", Billdata.StreetLane);
                param[5] = new SqlParameter("@FK_CountryID", Billdata.FK_CountryID);
                param[6] = new SqlParameter("@FK_StateID", Billdata.FK_StateID);
                param[7] = new SqlParameter("@FK_CityID", Billdata.FK_CityID);
                param[8] = new SqlParameter("@FK_AreaID", Billdata.FK_AreaID);
                param[9] = new SqlParameter("@PinCode", Billdata.PinCode);
                param[10] = new SqlParameter("@TelephoneNo", Billdata.TelephoneNo);
                param[11] = new SqlParameter("@EmailID", Billdata.EmailID);
                param[12] = new SqlParameter("@FK_RegistrationID", Billdata.FK_RegistrationID);
                param[13] = new SqlParameter("@FK_GstParty_TypeID", Billdata.FK_GstParty_TypeID);
                param[14] = new SqlParameter("@GstIdentificationNo", Billdata.GstIdentificationNo);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_MasterPRTYBILLINGDET_Update]", param);
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

        [HttpPost]
        [Route("UpdateCustomer")]
        public Api_CommonResponse UpdateCustomer(SupplierModel com)
        {
            try
            {
                var customerID = 0;

                SqlParameter[] param = new SqlParameter[16];
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

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_PRTYMST_Update]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    /*customerID = Convert.ToInt32(ds.Tables[0].Rows[0]["PK_PartyContactDetails"]);*/

                    if (com.ContactDtls != null && com.ContactDtls.Count > 0)
                    {
                        foreach (var item in com.ContactDtls)
                        {
                            UpdateSupplierMembers(item, customerID, "Insert");
                        }
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
        [Route("UpdateSupplierMembers")]
        public string UpdateSupplierMembers(SupplierContactDtls member, int customerId, string type)
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
        [Route("GetSupplierDetailsForEdit")]
        public Api_CommonResponse GetSupplierDetailsForEdit(int supplierId)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@iPk_PartyId", supplierId);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_MASTER_Customer_Get_For_Edit]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {

                    SupplierModel customer = new SupplierModel();
                    List<TDSList> tds = new List<TDSList>();
                    List<SupplierContactDtls> cntct = new List<SupplierContactDtls>();
                    customer.iPk_PartyId = Convert.ToInt32(ds.Tables[0].Rows[0]["iPk_PartyId"]);
                    customer.iFk_IndustryType = Convert.ToInt32(ds.Tables[0].Rows[0]["iFk_IndustryType"]);
                    customer.sPartyName = ds.Tables[0].Rows[0]["sPartyName"].ToString();
                    customer.sName = ds.Tables[0].Rows[0]["sName"].ToString();
                    customer.Group = ds.Tables[0].Rows[0]["Group"].ToString();
                    customer.NatureofPayment = ds.Tables[0].Rows[0]["NatureofPayment"].ToString();
                    customer.DeducteeType = ds.Tables[0].Rows[0]["DeducteeType"].ToString();
                    customer.dtInsertDate = ds.Tables[0].Rows[0]["dtInsertDate"].ToString();
                    customer.sPANNo = ds.Tables[0].Rows[0]["sPANNo"].ToString();
                    customer.sWebsite = ds.Tables[0].Rows[0]["sWebsite"].ToString();
                    customer.sEmailID = ds.Tables[0].Rows[0]["sEmailID"].ToString();
                    customer.MSMERegNo = ds.Tables[0].Rows[0]["MSMERegNo"].ToString();
                    customer.dtEffctvDate = ds.Tables[0].Rows[0]["dtEffctvDate"].ToString();
                    customer.sPtrlUnder = ds.Tables[0].Rows[0]["sPtrlUnder"].ToString();
                    customer.sPanHolder = ds.Tables[0].Rows[0]["sPanHolder"].ToString();
                    customer.iFk_NtPymtId = Convert.ToInt32(ds.Tables[0].Rows[0]["iFk_NtPymtId"]);
                    customer.iFk_DeductTypId = Convert.ToInt32(ds.Tables[0].Rows[0]["iFk_DeductTypId"]);
                    customer.iGrpLdgrId = Convert.ToInt32(ds.Tables[0].Rows[0]["iGrpLdgrId"]);
                    customer.iIsTdsApcbl = Convert.ToInt32(ds.Tables[0].Rows[0]["iIsTdsApcbl"]);
                    customer.iIsLedgrTag = Convert.ToInt32(ds.Tables[0].Rows[0]["iIsLedgrTag"]);
                    customer.bInActive = Convert.ToInt32(ds.Tables[0].Rows[0]["bInActive"]);
                    customer.iFk_PtrlUndr = Convert.ToInt32(ds.Tables[0].Rows[0]["iFk_PtrlUndr"]);
                    customer.iFk_ApprovedBy = Convert.ToInt32(ds.Tables[0].Rows[0]["iFk_ApprovedBy"].ToString());
                    customer.iPanStatus = Convert.ToInt32(ds.Tables[0].Rows[0]["iPanStatus"]);



                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        for (int i = 0; i <= ds.Tables[1].Rows.Count - 1; i++)
                        {
                            SupplierContactDtls member = new SupplierContactDtls();
                            member.CntctPerson = ds.Tables[1].Rows[i]["CntctPerson"].ToString();
                            member.CntctNumber = ds.Tables[1].Rows[i]["CntctNumber"].ToString();
                            member.EmailId = ds.Tables[1].Rows[i]["EmailId"].ToString();
                            cntct.Add(member);
                        }
                        customer.ContactDtls = cntct;
                    }
                    else
                    {
                        customer.ContactDtls = null;
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

        [HttpPost]
        [Route("UpdateSupplierDetails")]
        public Api_CommonResponse UpdateSupplierDetails(SupplierModel supplier)
        {
            try
            {
                var supplierID = 0;

                SqlParameter[] param = new SqlParameter[28];
                param[0] = new SqlParameter("@iPk_PartyId", supplier.iPk_PartyId);
                param[1] = new SqlParameter("@sPartyName", supplier.sPartyName);
                param[2] = new SqlParameter("@iFk_ReferredBy", supplier.iFk_ReferredBy);
                param[3] = new SqlParameter("@iStatus", supplier.iStatus);
                param[4] = new SqlParameter("@iFk_Category", supplier.iFk_Category);
                param[5] = new SqlParameter("@iFk_IndustryType", supplier.iFk_IndustryType);
                param[6] = new SqlParameter("@iFk_Type", supplier.iFk_Type);
                param[7] = new SqlParameter("@sPANNo", supplier.sPANNo);
                param[8] = new SqlParameter("@sWebsite", string.IsNullOrWhiteSpace(supplier.sWebsite) ? null : supplier.sWebsite);
                param[9] = new SqlParameter("@sEmailID", string.IsNullOrWhiteSpace(supplier.sEmailID) ? null : supplier.sEmailID);
                param[10] = new SqlParameter("@iFk_PreparedBy", supplier.iFk_PreparedBy);
                param[11] = new SqlParameter("@iFk_ApprovedBy", supplier.iFk_ApprovedBy);
                param[12] = new SqlParameter("@sCategory", supplier.sCategory);
                param[13] = new SqlParameter("@iMAXID", supplier.iMAXID);
                param[14] = new SqlParameter("@iFk_CompanyID", supplier.iFk_CompanyID);
                param[15] = new SqlParameter("@iFk_BranchID", supplier.iFk_BranchID);
                param[16] = new SqlParameter("@iGrpLdgrId", supplier.iGrpLdgrId);
                param[17] = new SqlParameter("@iIsLedgrTag", supplier.iIsLedgrTag);
                param[18] = new SqlParameter("@iIsTdsApcbl", supplier.iIsTdsApcbl);
                param[19] = new SqlParameter("@iFk_NtPymtId", supplier.iFk_NtPymtId);
                param[20] = new SqlParameter("@iFk_DeductTypId", supplier.iFk_DeductTypId);
                param[21] = new SqlParameter("@bInActive", supplier.bInActive);
                param[22] = new SqlParameter("@sReason", supplier.sReason);
                param[23] = new SqlParameter("@MSMERegNo", string.IsNullOrWhiteSpace(supplier.MSMERegNo) ? null : supplier.MSMERegNo);
                param[24] = new SqlParameter("@dtEffctvDate", supplier.dtEffctvDate);
                param[25] = new SqlParameter("@iFk_PtrlUndr", supplier.iFk_PtrlUndr);
                param[26] = new SqlParameter("@sPanHolder", supplier.sPanHolder);
                param[27] = new SqlParameter("@iPanStatus", supplier.iPanStatus);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_Supplier_Update]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    supplierID = Convert.ToInt32(ds.Tables[0].Rows[0]["SupplierID"]);

                    if (supplier.ContactDtls != null && supplier.ContactDtls.Count > 0)
                    {
                        foreach (var item in supplier.ContactDtls)
                        {
                            AddSupplierMembers(item, supplierID, "Update");
                        }
                        //AddSupplierAddressDetails(supp.AddressDtls, supplierID, "I");
                    }

                    if (supplier.Brnchdtls != null && supplier.Brnchdtls.Count > 0)
                    {
                        foreach (var item in supplier.Brnchdtls)
                        {
                            AddBranchDetails(item, supplierID);
                        }
                        //AddSupplierAddressDetails(supp.AddressDtls, supplierID, "I");
                    }
                    if (supplier.TDSList != null && supplier.TDSList.Count > 0)
                    {
                        foreach (var item in supplier.TDSList)
                        {
                            AddSupplierTDSDetails(item, supplierID);
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

    }
}
