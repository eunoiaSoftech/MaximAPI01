using Eunoia_UM_API.Helper;
using Eunoia_UM_API.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using static iTextSharp.text.pdf.AcroFields;
using System.Text;

namespace Eunoia_UM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrokerController : Controller
    {
        Api_CommonResponse api_Response = new Api_CommonResponse();
        [HttpPost]
        [Route("AddBroker")]
        public Api_CommonResponse AddBroker(BrokerModel broker)
        {
            try
            {
                var brokerID = 0;
                SqlParameter[] param = new SqlParameter[31];
                param[0] = new SqlParameter("@sPartyName", broker.sPartyName);
                param[1] = new SqlParameter("@iFk_ReferredBy", broker.iFk_ReferredBy);
                param[2] = new SqlParameter("@iStatus", broker.iStatus);
                param[3] = new SqlParameter("@iFk_Category", broker.iFk_Category);
                param[4] = new SqlParameter("@iFk_IndustryType", broker.iFk_IndustryType);
                param[5] = new SqlParameter("@iFk_Type", broker.iFk_Type);
                param[6] = new SqlParameter("@sPANNo", broker.sPANNo);
                param[7] = new SqlParameter("@sWebsite", broker.sWebsite);
                param[8] = new SqlParameter("@sEmailID", broker.sEmailID);
                param[9] = new SqlParameter("@iFk_PreparedBy", broker.iFk_PreparedBy);
                param[10] = new SqlParameter("@iFk_ApprovedBy", broker.iFk_ApprovedBy);
                param[11] = new SqlParameter("@sCategory", broker.sCategory);
                param[12] = new SqlParameter("@iMAXID", broker.iMAXID);
                param[13] = new SqlParameter("@iFk_CompanyID", broker.iFk_CompanyID);
                param[14] = new SqlParameter("@iFk_BranchID", broker.iFk_BranchID);
                param[15] = new SqlParameter("@iFk_YearId", broker.iFk_YearId);
                param[16] = new SqlParameter("@ifk_Userid", broker.ifk_Userid);
                param[17] = new SqlParameter("@sName", broker.sName);
                param[18] = new SqlParameter("@iGrpLdgrId", broker.iGrpLdgrId);
                param[19] = new SqlParameter("@iIsLedgrTag", broker.iIsLedgrTag);
                param[20] = new SqlParameter("@iIsTdsApcbl", broker.iIsTdsApcbl);
                param[21] = new SqlParameter("@iFk_NtPymtId", broker.iFk_NtPymtId);
                param[22] = new SqlParameter("@iFk_DeductTypId", broker.iFk_DeductTypId);
                param[23] = new SqlParameter("@bInActive", broker.bInActive);
                param[24] = new SqlParameter("@sReason", broker.sReason);
                param[25] = new SqlParameter("@dtEffctvDate", broker.dtEffctvDate);
                param[26] = new SqlParameter("@userId", broker.userId);
                param[27] = new SqlParameter("@sPanHolder", broker.sPanHolder);
                param[28] = new SqlParameter("@iPanStatus", broker.iPanStatus);
                param[29] = new SqlParameter("@sPanAttach", broker.sPanAttach);
                param[30] = new SqlParameter("@sAaddharAttach", broker.sAaddharAttach);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_Broker_Save]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    brokerID = Convert.ToInt32(ds.Tables[0].Rows[0]["brokerID"]);

                    if (broker.ContactDtls != null && broker.ContactDtls.Count > 0)
                    {
                        foreach (var item in broker.ContactDtls)
                        {
                            AddBrokerMembers(item, brokerID, "Insert");
                        }
                        //AddSupplierAddressDetails(supp.AddressDtls, supplierID, "I");
                    }
                    if (broker.Brnchdtls2 != null && broker.Brnchdtls2.Count > 0)
                    {
                        foreach (var item in broker.Brnchdtls2)
                        {
                            AddBranchDetails(item, brokerID);
                        }
                        //AddSupplierAddressDetails(supp.AddressDtls, supplierID, "I");
                    }
                    if (broker.TDSList != null && broker.TDSList.Count > 0)
                    {
                        foreach (var item in broker.TDSList)
                        {
                            AddTDSDetails(item, brokerID);
                        }
                        //AddSupplierAddressDetails(supp.AddressDtls, supplierID, "I");
                    }
                    //api_Response.message = ds.Tables[0].Rows[0]["Message"].ToString();
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
        [Route("AddBrokerMembers")]
        public string AddBrokerMembers(BrokerContactDtls member, int partyId, string type)
        {
            var status = "";
            try
            {
                SqlParameter[] param = new SqlParameter[5];
                /*param[0] = new SqlParameter("@iPK_CntctDtlsId", member.iCntctDtlsId);*/
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

        [HttpPost]
        [Route("AddTDSDetails")]
        public string AddTDSDetails(TDSList member, int partyId)
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
        [HttpGet]
        [Route("GetBroker")]
        public Api_CommonResponse GetBroker()
        {
            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_Broker_Get]");
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Broker List";
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
            // TODO: use dropdown method
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
        [Route("GetBrokerBank")]
        public Api_CommonResponse GetBrokerBank(int brokerID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@FK_PartyID", brokerID);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_Supplier_GetSupplierBank]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Supplier bank List";
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
        [Route("SaveBrokerBank")]
        public Api_CommonResponse SaveBrokerBank([FromBody] BrokerBankRequest request)
        {
            try
            {
                bool allOperationsSuccessful = true;

                // Process the bank list for insert or update
                if (request.Bank != null && request.Bank.Count > 0)
                {
                    foreach (var item in request.Bank)
                    {
                        if (item.PK_PartyBankId > 0)
                        {
                            // Call function to update bank details
                            UpdateBankDetails(item, "Update");
                        }
                        else
                        {
                            // Call function to insert bank details
                            SaveBankDetails(item, "Insert");
                        }
                    }
                }

                // Process the bankDelete list for deletion
                if (request.BankDelete != null && request.BankDelete.Count > 0)
                {
                    foreach (var deleteItem in request.BankDelete)
                    {
                        DeleteBankDetails(deleteItem);
                    }
                }

                // If all operations succeeded, set the success message and status
                if (allOperationsSuccessful)
                {
                    // Set response values
                    api_Response.responseCode = 0;
                    api_Response.message = "Data Saved Successfully...";
                    api_Response.statusCode = 200;
                    api_Response.data = null; // No data needed, so just set it to null
                }
                else
                {
                    // If any operation failed, set failure response
                    api_Response.responseCode = 1;
                    api_Response.message = "We are facing server issue at the moment. Please try again...";
                    api_Response.statusCode = -1;
                    api_Response.data = null; // No data needed, so just set it to null
                }
            }
            catch (Exception e)
            {
                // Handle exception and log for debugging
                api_Response.responseCode = 1;
                api_Response.message = $"An error occurred: {e.Message}";
                api_Response.statusCode = -1;
                api_Response.data = null; // No data needed, so just set it to null
            }

            return api_Response;
        }




        [HttpDelete]
        [Route("DeleteBankDetails")]
        public void DeleteBankDetails(BrokerBankDetails item)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@PK_PartyBankId", item.PK_PartyBankId);

                // Execute stored procedure for deletion
                DBOperation.FillDataSet("USP_Party_DeletePartyBankDetails_Delete", param);
            }
            catch (Exception ex)
            {
                // Log or handle exception
                throw new Exception($"Error while deleting bank details for PK_PartyBankId: {item.PK_PartyBankId}. Details: {ex.Message}", ex);
            }
        }


        [HttpPost]
        [Route("UpdateBankDetails")]
        private void UpdateBankDetails(BrokerBankDetails item, string operation)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[11];
                param[0] = new SqlParameter("@PK_PartyBankId", item.PK_PartyBankId); // Primary Key
                param[1] = new SqlParameter("@FK_PartyID", item.FK_PartyID);
                param[2] = new SqlParameter("@FK_AccountType", item.FK_AccountType);
                param[3] = new SqlParameter("@AccountNo", item.AccountNo);
                param[4] = new SqlParameter("@BankName", item.BankName);
                param[5] = new SqlParameter("@BranchName", item.BranchName);
                param[6] = new SqlParameter("@ISFCNo", item.IFSCNo);
                param[7] = new SqlParameter("@IsPrimary", item.IsPrimary);
                param[8] = new SqlParameter("@IsActive", item.IsActive);
                param[9] = new SqlParameter("@sBnfcryName", item.sBnfcryName);
                param[10] = new SqlParameter("@sDocmntUrl", item.sDocmntUrl);

                // Execute stored procedure for updating
                DBOperation.FillDataSet("[dbo].[USP_Master_PRTYBNKDET_Update]", param);
            }
            catch (Exception ex)
            {
                // Log or handle exception
                throw new Exception($"Error while updating bank details for PK_PartyBankId: {item.PK_PartyBankId}. Details: {ex.Message}", ex);
            }
        }

        [HttpPost]
        [Route("SaveBankDetails")]

        private void SaveBankDetails(BrokerBankDetails item, string operation)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[10];
                param[0] = new SqlParameter("@FK_PartyID", item.FK_PartyID);
                param[1] = new SqlParameter("@FK_AccountType", item.FK_AccountType);
                param[2] = new SqlParameter("@AccountNo", item.AccountNo);
                param[3] = new SqlParameter("@BankName", item.BankName);
                param[4] = new SqlParameter("@BranchName", item.BranchName);
                param[5] = new SqlParameter("@ISFCNo", item.IFSCNo);
                param[6] = new SqlParameter("@IsPrimary", item.IsPrimary);
                param[7] = new SqlParameter("@IsActive", item.IsActive);
                param[8] = new SqlParameter("@sBnfcryName", item.sBnfcryName);
                param[9] = new SqlParameter("@sDocmntUrl", item.sDocmntUrl);

                // Execute stored procedure for inserting
                DBOperation.FillDataSet("[dbo].[USP_Master_PRTYBNKDET_Save]", param);
            }
            catch (Exception ex)
            {
                // Log or handle exception
                throw new Exception($"Error while saving bank details for FK_PartyID: {item.FK_PartyID}. Details: {ex.Message}", ex);
            }
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
        public Api_CommonResponse GetAdvDetails(int brokerID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@FK_PartyID", brokerID);
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

        [HttpPost]
        [Route("DeleteAdvDetails")]
        public Api_CommonResponse DeleteAdvDetails(AdvDetials adv)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@iPK_AdvDetailsId", adv.iPK_AdvDetailsId);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_MASTER_Supplier_AdvDet_Delete]", param);
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
        [Route("GetAdvDetailsForEdit")]
        public Api_CommonResponse GetAdvDetailsForEdit(int id)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@iPK_AdvDetailsId", id);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_Supplier_GetAdvDetailsForEdit]", param);
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
        [Route("AdvanceDetailsUpdate")]
        public Api_CommonResponse AdvanceDetailsUpdate(AdvDetials adv)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[9];
                // TODO: Check the params of sp and write sp for update query
                param[0] = new SqlParameter("@iPK_AdvDetailsId", adv.iPK_AdvDetailsId);
                param[1] = new SqlParameter("@sCreatedBy", adv.sCreatedBy);
                param[2] = new SqlParameter("@iFk_FinancialYrId", adv.iFk_FinancialYrId);
                param[3] = new SqlParameter("@iFk_BranchId", adv.iFk_BranchId);
                param[4] = new SqlParameter("@iAdvTruck", adv.iAdvTruck);
                param[5] = new SqlParameter("@iAdvTrailer", adv.iAdvTrailer);
                param[6] = new SqlParameter("@iCreditPeriod", adv.iCreditPeriod);
                param[7] = new SqlParameter("@dtEffectiveDate", adv.dtEffectiveDate);
                param[8] = new SqlParameter("@bIsActive", adv.bIsActive);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_MASTER_Supplier_AdvDet_Update]", param);
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
        [Route("BillingdetailsList")]
        public Api_CommonResponse BillingdetailsList(int brokerID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@FK_PartyID", brokerID);
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
        public Api_CommonResponse Savebilldetails(BrokerBillingDetails Billdata)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[15];
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
                param[14] = new SqlParameter("@GST_effdt", Billdata.GST_effdt);

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
        public Api_CommonResponse GetBillingDetailsForEdit(int billingDetId)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@id", billingDetId);
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
        public Api_CommonResponse UpdateBillingDetails(BrokerBillingDetails Billdata)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[16];
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
                param[15] = new SqlParameter("@GST_effdt", Billdata.GST_effdt);


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

        [HttpGet]
        [Route("GetBrokerDetailsForEdit")]
        public Api_CommonResponse GetBrokerDetailsForEdit(int brokerID)
        {
            try
            {
                BrokerModel customer = new BrokerModel();

                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@iPk_PartyId", brokerID);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_Supplier_GetDetailsForEdit]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<BrokerContactDtls> cntct = new List<BrokerContactDtls>();
                    List<TDSList> tds = new List<TDSList>();
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
                    customer.sPanHolder = ds.Tables[0].Rows[0]["sPanHolder"].ToString();
                    customer.iFk_ReferredBy = Convert.ToInt32(ds.Tables[0].Rows[0]["iFk_ReferredBy"].ToString());
                    customer.iStatus = Convert.ToInt32(ds.Tables[0].Rows[0]["iStatus"].ToString());
                    customer.iFk_Category = Convert.ToInt32(ds.Tables[0].Rows[0]["iFk_Category"].ToString());
                    customer.iFk_Type = Convert.ToInt32(ds.Tables[0].Rows[0]["iFk_Type"].ToString());
                    customer.iGrpLdgrId = Convert.ToInt32(ds.Tables[0].Rows[0]["iGrpLdgrId"].ToString());
                    customer.iFk_NtPymtId = Convert.ToInt32(ds.Tables[0].Rows[0]["iFk_NtPymtId"].ToString());
                    customer.iFk_DeductTypId = Convert.ToInt32(ds.Tables[0].Rows[0]["iFk_DeductTypId"].ToString());
                    customer.iFk_PreparedBy = Convert.ToInt32(ds.Tables[0].Rows[0]["iFk_PreparedBy"].ToString());
                    customer.iFk_ApprovedBy = Convert.ToInt32(ds.Tables[0].Rows[0]["iFk_ApprovedBy"].ToString());
                    customer.iMAXID = Convert.ToInt32(ds.Tables[0].Rows[0]["iMAXID"].ToString());
                    customer.iFk_CompanyID = Convert.ToInt32(ds.Tables[0].Rows[0]["iFk_CompanyID"].ToString());
                    customer.iFk_BranchID = Convert.ToInt32(ds.Tables[0].Rows[0]["iFk_BranchID"].ToString());
                    customer.ifk_Userid = Convert.ToInt32(ds.Tables[0].Rows[0]["iFk_PreparedBy"].ToString());
                    customer.bIsActive = Convert.ToInt32(ds.Tables[0].Rows[0]["iIsActive"].ToString());
                    customer.iIsLedgrTag = Convert.ToInt32(ds.Tables[0].Rows[0]["iIsLedgrTag"].ToString());
                    customer.iIsTdsApcbl = Convert.ToInt32(ds.Tables[0].Rows[0]["iIsTdsApcbl"].ToString());
                    customer.bInActive = Convert.ToInt32(ds.Tables[0].Rows[0]["bInActive"]);
                    customer.dtEffctvDate = ds.Tables[0].Rows[0]["dtEffctvDate"].ToString();
                    customer.iPK_USRID = Convert.ToInt32(ds.Tables[0].Rows[0]["iPK_USRID"].ToString());
                    customer.sUSRNME = ds.Tables[0].Rows[0]["sUSRNME"].ToString();
                    customer.iPanStatus = Convert.ToInt32(ds.Tables[0].Rows[0]["iPanStatus"]);
                    customer.sPanAttach = ds.Tables[0].Rows[0]["sPanAttach"].ToString();
                    customer.sAaddharAttach = ds.Tables[0].Rows[0]["sAaddharAttach"].ToString();


                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        for (int i = 0; i <= ds.Tables[1].Rows.Count - 1; i++)
                        {
                            BrokerContactDtls member = new BrokerContactDtls();
                            member.CntctPerson = ds.Tables[1].Rows[i]["CntctPerson"].ToString();
                            member.CntctNumber = ds.Tables[1].Rows[i]["CntctNumber"].ToString();
                            member.EmailId = ds.Tables[1].Rows[i]["EmailId"].ToString();
                            member.primaryName = ds.Tables[1].Rows[i]["primaryName"].ToString();
                            member.Primary = Convert.ToBoolean(ds.Tables[1].Rows[i]["Primary"]);
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
                    api_Response.message = "Supplier details";
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
                throw e;
            }
            return api_Response;
        }

        [HttpPost]
        [Route("UpdateBrokerDetails")]
        public Api_CommonResponse UpdateBrokerDetails(BrokerModel broker)
        {
            try
            {
                var brokerID = 0;
                SqlParameter[] param = new SqlParameter[27];
                param[0] = new SqlParameter("@iPk_PartyId", broker.iPk_PartyId);
                param[1] = new SqlParameter("@sPartyName", broker.sPartyName);
                param[2] = new SqlParameter("@iFk_ReferredBy", broker.iFk_ReferredBy);
                param[3] = new SqlParameter("@iStatus", broker.iStatus);
                param[4] = new SqlParameter("@iFk_Category", broker.iFk_Category);
                param[5] = new SqlParameter("@iFk_IndustryType", broker.iFk_IndustryType);
                param[6] = new SqlParameter("@iFk_Type", broker.iFk_Type);
                param[7] = new SqlParameter("@sPANNo", broker.sPANNo);
                param[8] = new SqlParameter("@sWebsite", broker.sWebsite);
                param[9] = new SqlParameter("@sEmailID", broker.sEmailID);
                param[10] = new SqlParameter("@iFk_PreparedBy", broker.iFk_PreparedBy);
                param[11] = new SqlParameter("@iFk_ApprovedBy", broker.iFk_ApprovedBy);
                param[12] = new SqlParameter("@sCategory", broker.sCategory);
                param[13] = new SqlParameter("@iMAXID", broker.iMAXID);
                param[14] = new SqlParameter("@iFk_CompanyID", broker.iFk_CompanyID);
                param[15] = new SqlParameter("@iFk_BranchID", broker.iFk_BranchID);
                param[16] = new SqlParameter("@iGrpLdgrId", broker.iGrpLdgrId);
                param[17] = new SqlParameter("@iIsLedgrTag", broker.iIsLedgrTag);
                param[18] = new SqlParameter("@iIsTdsApcbl", broker.iIsTdsApcbl);
                param[19] = new SqlParameter("@iFk_NtPymtId", broker.iFk_NtPymtId);
                param[20] = new SqlParameter("@iFk_DeductTypId", broker.iFk_DeductTypId);
                param[21] = new SqlParameter("@dtEffctvDate", broker.dtEffctvDate);
                param[22] = new SqlParameter("@userId", broker.userId);
                param[23] = new SqlParameter("@sPanHolder", broker.sPanHolder);
                param[24] = new SqlParameter("@iPanStatus", broker.iPanStatus);
                param[25] = new SqlParameter("@sPanAttach", broker.sPanAttach);
                param[26] = new SqlParameter("@sAaddharAttach", broker.sAaddharAttach);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_Broker_Update]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    brokerID = Convert.ToInt32(ds.Tables[0].Rows[0]["brokerID"]);

                    if (broker.ContactDtls != null && broker.ContactDtls.Count > 0)
                    {
                        foreach (var item in broker.ContactDtls)
                        {
                            AddBrokerMembers(item, brokerID, "Insert");
                        }
                        //AddSupplierAddressDetails(supp.AddressDtls, supplierID, "I");
                    }

                    if (broker.Brnchdtls2 != null && broker.Brnchdtls2.Count > 0)
                    {
                        foreach (var item in broker.Brnchdtls2)
                        {
                            AddBranchDetails(item, brokerID);
                        }
                        //AddSupplierAddressDetails(supp.AddressDtls, supplierID, "I");
                    }

                    if (broker.TDSList != null && broker.TDSList.Count > 0)
                    {
                        foreach (var item in broker.TDSList)
                        {
                            AddTDSDetails(item, brokerID);
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


        [HttpGet("HandlePanValidation")]
        public async Task<IActionResult> HandlePanValidation(string panNumber)
        {
            try
            {
                // Validate input
                if (string.IsNullOrEmpty(panNumber))
                {
                    return BadRequest(new { Message = "PAN number is required." });
                }

                // Set up the HTTP client
                using var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.invincibleocean.com/invincible/panPlus");

                // Add headers
                request.Headers.Add("secretKey", "coOZjG2IFrM0jMeYWWYo6Vm2rEVNpi8l2sCXnFsZ1i2eMX6qbyznBOirNpqC7O7lG");
                request.Headers.Add("clientId", "11f884fbc7de6705f0e5dad95dec25f6:d7bcd09c70213f190dac7cc24b07caf7");

                // Add JSON content
                var jsonContent = new { panNumber = panNumber };
                var content = new StringContent(JsonConvert.SerializeObject(jsonContent), Encoding.UTF8, "application/json");
                request.Content = content;

                // Send the HTTP request
                var response = await client.SendAsync(request);

                // Ensure success status code
                response.EnsureSuccessStatusCode();

                // Parse the response content
                var responseContent = await response.Content.ReadAsStringAsync();
                var panResponse = JsonConvert.DeserializeObject<PanValidationResponse>(responseContent);

                // Return the parsed response
                return Ok(panResponse);
            }
            catch (HttpRequestException httpEx)
            {
                // Handle HTTP errors
                return StatusCode(502, new { Message = "Error while connecting to the PAN validation API.", Details = httpEx.Message });
            }
            catch (JsonSerializationException jsonEx)
            {
                // Handle JSON parsing errors
                return StatusCode(500, new { Message = "Error parsing the API response.", Details = jsonEx.Message });
            }
            catch (Exception ex)
            {
                // Handle other unexpected errors
                return StatusCode(500, new { Message = "Unexpected error occurred.", Details = ex.Message });
            }
        }

    }
}
