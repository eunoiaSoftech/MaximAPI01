using Eunoia_UM_API.Helper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using Eunoia_UM_API.Model;
using System.Text;

namespace Eunoia_UM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverMasterController : ControllerBase
    {
        Api_CommonResponse api_Response = new Api_CommonResponse();

        [HttpPost]
        [Route("AddDriver")]
        public Api_CommonResponse addDriver(DriverMaster driver)
        {
            try
            {
                var driverPhotoURL = "";
                var driverAadhaarURL = "";
                var driverPANURL = "";
                var driverAddressProofURL = "";
                var driverOtherDocURL = "";
                var createdDriverID = 0;

                var driverName = driver.sFirstName + " " + driver.sMiddletName + " " + driver.sLastName;
                driverName = "D_" + driverName.Replace(" ", "_");
                #region Driver Documents
                /*if (driver.sDrvPhoto != null)
                {
                    var fileName = "ProfilePicture_" + DateTime.Now.ToString("ddMMyyyyhhmm");
                    driverPhotoURL = DocumentUploader.UploadDocuments(fileName, driverName, Convert.FromBase64String(driver.sDrvPhoto.Split(",")[1]), driver.sDrvPhotoExt);
                }
                if (driver.sAdharCardAttch != null)
                {
                    var fileName = "AadhaarCard_" + DateTime.Now.ToString("ddMMyyyyhhmm");
                    driverAadhaarURL = DocumentUploader.UploadDocuments(fileName, driverName, Convert.FromBase64String(driver.sAdharCardAttch.Split(",")[1]), driver.sAdharCardAttchExt);
                }
                if (driver.sPanCardAttch != null)
                {
                    var fileName = "PANCard_" + DateTime.Now.ToString("ddMMyyyyhhmm");
                    driverPANURL = DocumentUploader.UploadDocuments(fileName, driverName, Convert.FromBase64String(driver.sPanCardAttch.Split(",")[1]), driver.sPanCardAttchExt);
                }
                if (driver.sAddressProofAttch != null)
                {
                    var fileName = "AddressProof_" + DateTime.Now.ToString("ddMMyyyyhhmm");
                    driverAddressProofURL = DocumentUploader.UploadDocuments(fileName, driverName, Convert.FromBase64String(driver.sAddressProofAttch.Split(",")[1]), driver.sAddressProofAttchExt);
                }
                if (driver.sOtherAttch != null)
                {
                    var fileName = "OtherDocument" + DateTime.Now.ToString("ddMMyyyyhhmm");
                    driverOtherDocURL = DocumentUploader.UploadDocuments(fileName, driverName, Convert.FromBase64String(driver.sOtherAttch.Split(",")[1]), driver.sOtherAttchExt);
                }*/
                #endregion
                SqlParameter[] param = new SqlParameter[41];
                param[0] = new SqlParameter("@sFirstName", driver.sFirstName);
                param[1] = new SqlParameter("@sMiddletName", driver.sMiddletName);
                param[2] = new SqlParameter("@sLastName", driver.sLastName);
                param[3] = new SqlParameter("@dtDob", driver.dtDob);
                param[4] = new SqlParameter("@dtDoj", driver.dtDoj);
                param[5] = new SqlParameter("@iFk_BloodGroupId", driver.iFk_BloodGroupId);
                param[6] = new SqlParameter("@iFk_GenderId", driver.iFk_GenderId);
                param[7] = new SqlParameter("@iPMobileNo", driver.iPMobileNo);
                param[8] = new SqlParameter("@iOMobileNo", driver.iOMobileNo);
                param[9] = new SqlParameter("@iFk_CntryId", driver.iFk_CntryId);
                param[10] = new SqlParameter("@iFk_StateId", driver.iFk_StateId);
                param[11] = new SqlParameter("@iFk_CityId", driver.iFk_CityId);
                param[12] = new SqlParameter("@sRAddress", driver.sRAddress);
                param[13] = new SqlParameter("@sDrvPhoto", driver.sDrvPhoto);
                param[14] = new SqlParameter("@sPanCardAttch", driver.sPanCardAttch);
                param[15] = new SqlParameter("@sAdharCardAttch", driver.sAdharCardAttch);
                param[16] = new SqlParameter("@sAddressProofAttch", driver.sAddressProofAttch);
                param[17] = new SqlParameter("@sOtherAttch", driver.sOtherAttch);
                param[18] = new SqlParameter("@sAadhaar", driver.sAadhaar);
                param[19] = new SqlParameter("@sPAN", driver.sPAN);
                param[20] = new SqlParameter("@dtSTrainig", driver.dtSTrainig);
                param[21] = new SqlParameter("@sSNumber", driver.sSNumber);
                param[22] = new SqlParameter("@isResign", driver.isResign);
                param[23] = new SqlParameter("@dtRsgDt", driver.dtRsgDt);
                param[24] = new SqlParameter("@iFk_Createdby", driver.iFk_Createdby);
                param[25] = new SqlParameter("@sLongitude", driver.sLongitude);
                param[26] = new SqlParameter("@sLatitude", driver.sLatitude);
                param[27] = new SqlParameter("@sPincode", driver.sPincode);
                param[28] = new SqlParameter("@branchID", driver.iFk_BrnchId);
                param[29] = new SqlParameter("@finyearID", driver.iFk_FinYear);
                param[30] = new SqlParameter("@sFathername", driver.sFathername);
                param[31] = new SqlParameter("@sPFNo", driver.sPFNo);
                param[32] = new SqlParameter("@sESICNo", driver.sESICNo);
                param[33] = new SqlParameter("@sUANNO", driver.sUANNO);
                param[34] = new SqlParameter("@dAmount", driver.dAmount);
                param[35] = new SqlParameter("@sRsgndAttch", driver.sRsgndAttch);
                param[36] = new SqlParameter("@iGrpLdgrId", driver.iGrpLdgrId);
                param[37] = new SqlParameter("@iIsLedgrTag", driver.iIsLedgrTag);
                param[38] = new SqlParameter("@iIsTdsApcbl", driver.iIsTdsApcbl);
                param[39] = new SqlParameter("@iFk_NtPymtId", driver.iFk_NtPymtId);
                param[40] = new SqlParameter("@iFk_DeductTypId", driver.iFk_DeductTypId);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_AddDriverData_Insert]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    createdDriverID = Convert.ToInt32(ds.Tables[0].Rows[0]["userID"]);
                    if (driver.licenceData != null && driver.licenceData.Count > 0)
                    {
                        foreach (var licenceData in driver.licenceData)
                        {
                            api_Response = addLicenceDetail(licenceData, createdDriverID);
                        }
                    }
                    else
                    {
                        api_Response.responseCode = 0;
                        api_Response.message = ds.Tables[0].Rows[0]["Message"].ToString();
                        api_Response.statusCode = Convert.ToInt32(ds.Tables[0].Rows[0]["StatusCode"]);
                    }
                    if (driver.driverContacts != null && driver.driverContacts.Count > 0)
                    {
                        foreach (var driverContact in driver.driverContacts)
                        {
                            api_Response = addDriverContactDetails(driverContact, createdDriverID);
                        }
                    }
                    else
                    {
                        api_Response.responseCode = 0;
                        api_Response.message = ds.Tables[0].Rows[0]["Message"].ToString();
                        api_Response.statusCode = Convert.ToInt32(ds.Tables[0].Rows[0]["StatusCode"]);
                    }
                    if (driver.driverFamilyList != null && driver.driverFamilyList.Count > 0)
                    {
                        foreach (var driverFamily in driver.driverFamilyList)
                        {
                            api_Response = addDriverFamilyDetails(driverFamily, createdDriverID);
                        }
                    }
                    else
                    {
                        api_Response.responseCode = 0;
                        api_Response.message = ds.Tables[0].Rows[0]["Message"].ToString();
                        api_Response.statusCode = Convert.ToInt32(ds.Tables[0].Rows[0]["StatusCode"]);
                    }
                    if (driver.DriverNomineeList != null && driver.DriverNomineeList.Count > 0)
                    {
                        foreach (var driverNominee in driver.DriverNomineeList)
                        {
                            api_Response = addDriverNomineeDetails(driverNominee, createdDriverID);
                        }
                    }
                    else
                    {
                        api_Response.responseCode = 0;
                        api_Response.message = ds.Tables[0].Rows[0]["Message"].ToString();
                        api_Response.statusCode = Convert.ToInt32(ds.Tables[0].Rows[0]["StatusCode"]);
                    }
                    if (driver.GuarantorDetailsList != null && driver.GuarantorDetailsList.Count > 0)
                    {
                        foreach (var driverContact in driver.GuarantorDetailsList)
                        {
                            api_Response = addGurantorDetails(driverContact, createdDriverID);
                        }
                    }
                    else
                    {
                        api_Response.responseCode = 0;
                        api_Response.message = ds.Tables[0].Rows[0]["Message"].ToString();
                        api_Response.statusCode = Convert.ToInt32(ds.Tables[0].Rows[0]["StatusCode"]);
                    }
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
        [Route("UpdateDriver")]
        public Api_CommonResponse UpdateDriver(DriverMaster driver)
        {
            try
            {
                var driverPhotoURL = "";
                var driverAadhaarURL = "";
                var driverPANURL = "";
                var driverAddressProofURL = "";
                var driverOtherDocURL = "";
                var createdDriverID = 0;

                var driverName = driver.sFirstName + " " + driver.sMiddletName + " " + driver.sLastName;
                driverName = "D_" + driverName.Replace(" ", "_");
                #region Driver Documents
                /*if (driver.sDrvPhoto != null)
                {
                    var fileName = "ProfilePicture_" + DateTime.Now.ToString("ddMMyyyyhhmm");
                    driverPhotoURL = DocumentUploader.UploadDocuments(fileName, driverName, Convert.FromBase64String(driver.sDrvPhoto.Split(",")[1]), driver.sDrvPhotoExt);
                }
                if (driver.sAdharCardAttch != null)
                {
                    var fileName = "AadhaarCard_" + DateTime.Now.ToString("ddMMyyyyhhmm");
                    driverAadhaarURL = DocumentUploader.UploadDocuments(fileName, driverName, Convert.FromBase64String(driver.sAdharCardAttch.Split(",")[1]), driver.sAdharCardAttchExt);
                }
                if (driver.sPanCardAttch != null)
                {
                    var fileName = "PANCard_" + DateTime.Now.ToString("ddMMyyyyhhmm");
                    driverPANURL = DocumentUploader.UploadDocuments(fileName, driverName, Convert.FromBase64String(driver.sPanCardAttch.Split(",")[1]), driver.sPanCardAttchExt);
                }
                if (driver.sAddressProofAttch != null)
                {
                    var fileName = "AddressProof_" + DateTime.Now.ToString("ddMMyyyyhhmm");
                    driverAddressProofURL = DocumentUploader.UploadDocuments(fileName, driverName, Convert.FromBase64String(driver.sAddressProofAttch.Split(",")[1]), driver.sAddressProofAttchExt);
                }
                if (driver.sOtherAttch != null)
                {
                    var fileName = "OtherDocument" + DateTime.Now.ToString("ddMMyyyyhhmm");
                    driverOtherDocURL = DocumentUploader.UploadDocuments(fileName, driverName, Convert.FromBase64String(driver.sOtherAttch.Split(",")[1]), driver.sOtherAttchExt);
                }*/
                #endregion
                SqlParameter[] param = new SqlParameter[41];
                param[0] = new SqlParameter("@sFirstName", driver.sFirstName);
                param[1] = new SqlParameter("@sMiddletName", driver.sMiddletName);
                param[2] = new SqlParameter("@sLastName", driver.sLastName);
                param[3] = new SqlParameter("@dtDob", driver.dtDob);
                param[4] = new SqlParameter("@dtDoj", driver.dtDoj);
                param[5] = new SqlParameter("@iFk_BloodGroupId", driver.iFk_BloodGroupId);
                param[6] = new SqlParameter("@iFk_GenderId", driver.iFk_GenderId);
                param[7] = new SqlParameter("@iPMobileNo", driver.iPMobileNo);
                param[8] = new SqlParameter("@iOMobileNo", driver.iOMobileNo);
                param[9] = new SqlParameter("@iFk_CntryId", driver.iFk_CntryId);
                param[10] = new SqlParameter("@iFk_StateId", driver.iFk_StateId);
                param[11] = new SqlParameter("@iFk_CityId", driver.iFk_CityId);
                param[12] = new SqlParameter("@sRAddress", driver.sRAddress);
                param[13] = new SqlParameter("@sDrvPhoto", driver.sDrvPhoto);
                param[14] = new SqlParameter("@sPanCardAttch", driver.sPanCardAttch);
                param[15] = new SqlParameter("@sAdharCardAttch", driver.sAdharCardAttch);
                param[16] = new SqlParameter("@sAddressProofAttch", driver.sAddressProofAttch);
                param[17] = new SqlParameter("@sOtherAttch", driver.sOtherAttch);
                param[18] = new SqlParameter("@sAadhaar", driver.sAadhaar);
                param[19] = new SqlParameter("@sPAN", driver.sPAN);
                param[20] = new SqlParameter("@dtSTrainig", driver.dtSTrainig);
                param[21] = new SqlParameter("@sSNumber", driver.sSNumber);
                param[22] = new SqlParameter("@isResign", driver.isResign);
                param[23] = new SqlParameter("@dtRsgDt", driver.dtRsgDt);
                param[24] = new SqlParameter("@iFk_Createdby", driver.iFk_Createdby);
                param[25] = new SqlParameter("@sLongitude", driver.sLongitude);
                param[26] = new SqlParameter("@sLatitude", driver.sLatitude);
                param[27] = new SqlParameter("@sPincode", driver.sPincode);
                param[28] = new SqlParameter("@branchID", driver.iFk_BrnchId);
                param[29] = new SqlParameter("@finyearID", driver.iFk_FinYear);
                param[30] = new SqlParameter("@Pk_DrvId", driver.iPk_VehDrvtId);
                param[31] = new SqlParameter("@sFathername", driver.sFathername);
                param[32] = new SqlParameter("@sPFNo", driver.sPFNo);
                param[33] = new SqlParameter("@sESICNo", driver.sESICNo);
                param[34] = new SqlParameter("@sUANNO", driver.sUANNO);
                param[35] = new SqlParameter("@dAmount", driver.dAmount);
                param[36] = new SqlParameter("@iGrpLdgrId", driver.iGrpLdgrId);
                param[37] = new SqlParameter("@iIsLedgrTag", driver.iIsLedgrTag);
                param[38] = new SqlParameter("@iIsTdsApcbl", driver.iIsTdsApcbl);
                param[39] = new SqlParameter("@iFk_NtPymtId", driver.iFk_NtPymtId);
                param[40] = new SqlParameter("@iFk_DeductTypId", driver.iFk_DeductTypId);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_Driver_Update]", param);


                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    createdDriverID = Convert.ToInt32(ds.Tables[0].Rows[0]["userID"]);
                    if (driver.driverContacts != null && driver.driverContacts.Count > 0)
                    {
                        foreach (var driverContact in driver.driverContacts)
                        {
                            api_Response = UpdateDriverContactDetails(driverContact, createdDriverID);

                        }
                        api_Response.responseCode = 1;
                        api_Response.message = "Update Sucessfully";
                        api_Response.statusCode = 200;
                    }
                    else
                    {
                        api_Response.responseCode = 0;
                        api_Response.message = ds.Tables[0].Rows[0]["Message"].ToString();
                        api_Response.statusCode = Convert.ToInt32(ds.Tables[0].Rows[0]["StatusCode"]);
                    }
                    if (driver.driverFamilyList != null && driver.driverFamilyList.Count > 0)
                    {
                        foreach (var driverFamily in driver.driverFamilyList)
                        {
                            api_Response = UpdateDriveFamilyDetails(driverFamily, createdDriverID);

                        }
                        api_Response.responseCode = 1;
                        api_Response.message = "Update Sucessfully";
                        api_Response.statusCode = 200;
                    }
                    else
                    {
                        api_Response.responseCode = 0;
                        api_Response.message = ds.Tables[0].Rows[0]["Message"].ToString();
                        api_Response.statusCode = Convert.ToInt32(ds.Tables[0].Rows[0]["StatusCode"]);
                    }
                    if (driver.DriverNomineeList != null && driver.DriverNomineeList.Count > 0)
                    {
                        foreach (var driverNominee in driver.DriverNomineeList)
                        {
                            api_Response = UpdateDriverNomineeDetails(driverNominee, createdDriverID);

                        }
                        api_Response.responseCode = 1;
                        api_Response.message = "Update Sucessfully";
                        api_Response.statusCode = 200;
                    }
                    else
                    {
                        api_Response.responseCode = 0;
                        api_Response.message = ds.Tables[0].Rows[0]["Message"].ToString();
                        api_Response.statusCode = Convert.ToInt32(ds.Tables[0].Rows[0]["StatusCode"]);
                    }
                    if (driver.GuarantorDetailsList != null && driver.GuarantorDetailsList.Count > 0)
                    {
                        foreach (var driverContact in driver.GuarantorDetailsList)
                        {
                            api_Response = UpdateDriverGurantorDetails(driverContact, createdDriverID);

                        }
                        api_Response.responseCode = 1;
                        api_Response.message = "Update Sucessfully";
                        api_Response.statusCode = 200;
                    }
                    else
                    {
                        api_Response.responseCode = 0;
                        api_Response.message = ds.Tables[0].Rows[0]["Message"].ToString();
                        api_Response.statusCode = Convert.ToInt32(ds.Tables[0].Rows[0]["StatusCode"]);
                    }
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
        [Route("AddDriverFamily")]
        public Api_CommonResponse addDriverFamilyDetails(DriverFamilyList familyLists, int driverID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@iFk_DriverMstId", driverID);
                param[1] = new SqlParameter("@sFamNme", familyLists.sFamilyNme);
                param[2] = new SqlParameter("@dtBirthDt", familyLists.sbirthDate);
                param[3] = new SqlParameter("@iRelation", familyLists.iFamilyRelation);
                param[4] = new SqlParameter("@sAttchmnt", familyLists.sAttachment);
                param[5] = new SqlParameter("iPk_FmlyId", familyLists.iPk_FmlyId);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_DriverFamilyData_Update]", param);
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
        [Route("AddDriverNominee")]
        public Api_CommonResponse addDriverNomineeDetails(DriverNomineeList nomineeLists, int driverID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@iFk_DriverMstId", driverID);
                param[1] = new SqlParameter("@sNomineeNme", nomineeLists.sNomineeNme);
                param[2] = new SqlParameter("@dtDtOfBirth", nomineeLists.dtbirthDateNom);
                param[3] = new SqlParameter("@iAccNo", nomineeLists.iBankAccount);
                param[4] = new SqlParameter("@sAttchmnt", nomineeLists.sAttachment);
                param[5] = new SqlParameter("iPk_NomineeId", nomineeLists.iPk_NomineeId);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_DriverNomineeData_Update]", param);
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
        [Route("AddDriverContact")]
        public Api_CommonResponse addDriverContactDetails(DriverContactList contactLists, int driverID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@iFk_DriverMstId", driverID);
                param[1] = new SqlParameter("@sCntctDetlspersonNme", contactLists.sCntctDetlspersonNme);
                param[2] = new SqlParameter("@sCntctNum", contactLists.sCntctNum);
                param[3] = new SqlParameter("@iCntcRelation", contactLists.iCntcRelation);
                param[4] = new SqlParameter("@bPrimary", contactLists.bPrimary);
                param[5] = new SqlParameter("@status", contactLists.status);
                param[6] = new SqlParameter("@iPk_DrvCntLst", contactLists.iPk_DrvCntLst);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_DriverContactData_Update]", param);
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
        [Route("addGurantorDetails")]
        public Api_CommonResponse addGurantorDetails(GuarantorDetailsList contactLists, int driverID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@iFk_DriverMstId", driverID);
                param[1] = new SqlParameter("@sGuarantorNme", contactLists.sGuarantorNme);
                param[2] = new SqlParameter("@sCntctNum ", contactLists.sCntctNum);
                param[3] = new SqlParameter("@sPresentAdd", contactLists.sPresentAdd);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_DriverGuarantor_Save]", param);
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

        [HttpGet]
        [Route("getDriverContact")]
        public Api_CommonResponse getDriverContact(int driverID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@driverID", driverID);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_GetDriverContact_Editmode]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.responseCode = 0;
                    api_Response.message = "License List";
                    api_Response.statusCode = 1;
                    api_Response.data = JsonConvert.SerializeObject(ds.Tables[0]);
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
        [HttpGet]
        [Route("getDriverFamily")]
        public Api_CommonResponse getDriverFamily(int driverID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@driverID", driverID);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_GetDriverFamily_Editmode]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.responseCode = 0;
                    api_Response.message = "License List";
                    api_Response.statusCode = 1;
                    api_Response.data = JsonConvert.SerializeObject(ds.Tables[0]);
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
        [HttpGet]
        [Route("getDriverNominee")]
        public Api_CommonResponse getDriverNominee(int driverID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@driverID", driverID);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_GetDriverNominee_Editmode]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.responseCode = 0;
                    api_Response.message = "License List";
                    api_Response.statusCode = 1;
                    api_Response.data = JsonConvert.SerializeObject(ds.Tables[0]);
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
        [HttpGet]
        [Route("getDriverGetGurantorDet")]
        public Api_CommonResponse getDriverGetGurantorDet(int driverID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@driverID", driverID);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_GetDriverGurantor_Editmode]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.responseCode = 0;
                    api_Response.message = "License List";
                    api_Response.statusCode = 1;
                    api_Response.data = JsonConvert.SerializeObject(ds.Tables[0]);
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
        [Route("UpdateDriverGurantorDetails")]
        public Api_CommonResponse UpdateDriverGurantorDetails(GuarantorDetailsList contactLists, int driverID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@iFk_DriverMstId", driverID);
                param[1] = new SqlParameter("@sGuarantorNme", contactLists.sGuarantorNme);
                param[2] = new SqlParameter("@sCntctNum ", contactLists.sCntctNum);
                param[3] = new SqlParameter("@sPresentAdd", contactLists.sPresentAdd);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_DriverGuarantor_Save]", param);

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
        [Route("UpdateDriverFamilyDetails")]
        public Api_CommonResponse UpdateDriveFamilyDetails(DriverFamilyList contactLists, int driverID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@iFk_DriverMstId", driverID);
                param[1] = new SqlParameter("@sFamNme", contactLists.sFamilyNme);
                param[2] = new SqlParameter("@dtBirthDt", contactLists.sbirthDate);
                param[3] = new SqlParameter("@iRelation", contactLists.iFamilyRelation);
                param[4] = new SqlParameter("@sAttchmnt", contactLists.sAttachment);
                param[5] = new SqlParameter("iPk_FmlyId", contactLists.iPk_FmlyId);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_DriverFamilyData_Update]", param);
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
        [Route("UpdateDriverNomineeDetails")]
        public Api_CommonResponse UpdateDriverNomineeDetails(DriverNomineeList nomineeLists, int driverID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@iFk_DriverMstId", driverID);
                param[1] = new SqlParameter("@sNomineeNme", nomineeLists.sNomineeNme);
                param[2] = new SqlParameter("@dtDtOfBirth", nomineeLists.dtbirthDateNom);
                param[3] = new SqlParameter("@iAccNo", nomineeLists.iBankAccount);
                param[4] = new SqlParameter("@sAttchmnt", nomineeLists.sAttachment);
                param[5] = new SqlParameter("iPk_NomineeId", nomineeLists.iPk_NomineeId);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_DriverNomineeData_Update]", param);
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
        [Route("UpdateDriverContactDetails")]
        public Api_CommonResponse UpdateDriverContactDetails(DriverContactList contactLists, int driverID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@iFk_DriverMstId", driverID);
                param[1] = new SqlParameter("@sCntctDetlspersonNme", contactLists.sCntctDetlspersonNme);
                param[2] = new SqlParameter("@sCntctNum", contactLists.sCntctNum);
                param[3] = new SqlParameter("@iCntcRelation", contactLists.iCntcRelation);
                param[4] = new SqlParameter("@bPrimary", contactLists.bPrimary);
                param[5] = new SqlParameter("@status", contactLists.status);
                param[5] = new SqlParameter("@iPk_DrvCntLst", contactLists.iPk_DrvCntLst);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_DriverContactData_Update]", param);
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

        [HttpGet]
        [Route("GetDriverList")]
        public Api_CommonResponse GetDriverList(int iFK_BranchId)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@iFK_BranchId", iFK_BranchId);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_GetDriverList_Select]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.responseCode = 0;
                    api_Response.message = "Driver List";
                    api_Response.statusCode = 1;
                    api_Response.data = JsonConvert.SerializeObject(ds.Tables[0]);
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
        [Route("addDriverLicense")]
        public Api_CommonResponse addDriverLicense(DriverLicence driverLicence)
        {
            try
            {
                var driverLicenseURL = "";

                var driverName = driverLicence.driverName;
                driverName = "D_" + driverName.Replace(" ", "_");
                #region Driver License Documents
                if (driverLicence.sLcsAttch != null)
                {
                    var fileName = "LicenseCopy_" + DateTime.Now.ToString("ddMMyyyyhhmm");
                    driverLicenseURL = DocumentUploader.UploadDocuments(fileName, driverName, Convert.FromBase64String(driverLicence.sLcsAttch.Split(",")[1]), driverLicence.sLcsAttchExt);
                }

                #endregion
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@iFk_VehDrvtId", driverLicence.iFk_VehDrvtId);
                param[1] = new SqlParameter("@sLicsNumber", driverLicence.sLicsNumber);
                param[2] = new SqlParameter("@dtLicsIssue", driverLicence.dtLicsIssue);
                param[3] = new SqlParameter("@dtLicsExpiry", driverLicence.dtLicsExpiry);

                param[4] = new SqlParameter("@sIssueBy", driverLicence.sIssueBy);

                param[5] = new SqlParameter("@iType", driverLicence.iType);
                param[6] = new SqlParameter("@sLcsAttch", driverLicenseURL);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_AddDriverLicenseData_Insert]", param);
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

        [HttpGet]
        [Route("getDriverLicense")]
        public Api_CommonResponse getDriverLicense(int driverID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@driverID", driverID);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_GetDriverLicenseList_Select]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.responseCode = 0;
                    api_Response.message = "License List";
                    api_Response.statusCode = 1;
                    api_Response.data = JsonConvert.SerializeObject(ds.Tables[0]);
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
        [Route("addDriverMedical")]
        public Api_CommonResponse addDriverMedical(DriverMedical driverMedical)
        {
            try
            {
                var driverMedicalReportURL = "";

                var driverName = driverMedical.driverName;
                driverName = "D_" + driverName.Replace(" ", "_");
                #region Driver License Documents
                if (driverMedical.sMedicalAttch != null)
                {
                    var fileName = "MedicalReport_" + DateTime.Now.ToString("ddMMyyyyhhmm");
                    driverMedicalReportURL = DocumentUploader.UploadDocuments(fileName, driverName, Convert.FromBase64String(driverMedical.sMedicalAttch.Split(",")[1]), driverMedical.sMedicalAttchExt);
                }

                #endregion
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@iFk_VehDrvtId", driverMedical.iFk_VehDrvtId);
                param[1] = new SqlParameter("@dtTestOn", driverMedical.dtTestOn);
                param[2] = new SqlParameter("@dtNextTestOn", driverMedical.dtNextTestOn);
                param[3] = new SqlParameter("@sMedicalAttch", driverMedicalReportURL);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_AddDriverMedicalData_Insert]", param);
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

        [HttpGet]
        [Route("getDriverMedical")]
        public Api_CommonResponse getDriverMedical(int driverID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@driverID", driverID);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_GetDriverMedicalList_Select]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.responseCode = 0;
                    api_Response.message = "Medical List";
                    api_Response.statusCode = 1;
                    api_Response.data = JsonConvert.SerializeObject(ds.Tables[0]);
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
        [Route("addDriverBank")]
        public Api_CommonResponse addDriverBank(List<DriverBank> bank)
        {
            try
            {
                DataSet ds = new DataSet();
                if (bank != null && bank.Count > 0)
                {
                    foreach (var item in bank)
                    {
                        SqlParameter[] param = new SqlParameter[7];
                        param[0] = new SqlParameter("@iFk_VehDrvtId", item.iFk_VehDrvtId);
                        param[1] = new SqlParameter("@iAcntType", item.iAcntType);
                        param[2] = new SqlParameter("@sBankAcntNo", item.sBankAcntNo);
                        param[3] = new SqlParameter("@sBranch", item.sBranch);
                        param[4] = new SqlParameter("@iBankNameId", item.iBankNameId);
                        //param[5] = new SqlParameter("@sOtherBankName", item.sOtherBankName);
                        param[5] = new SqlParameter("@sIFSCCode", item.sIFSCCode);
                        param[6] = new SqlParameter("@IsPrimary", item.IsPrimary);
                        //param[7] = new SqlParameter("@sGpayId", item.sGpayId);
                        //param[8] = new SqlParameter("@sUPIId", item.sUPIId);

                        ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_AddDriverBankData_Insert]", param);
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

        [HttpGet]
        [Route("getDriverBank")]
        public Api_CommonResponse getDriverBank(int driverID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@driverID", driverID);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_GetDriverBankList_Select]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.responseCode = 0;
                    api_Response.message = "Bank List";
                    api_Response.statusCode = 1;
                    api_Response.data = JsonConvert.SerializeObject(ds.Tables[0]);
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
        [Route("DriverRejoiningSave")]
        public Api_CommonResponse DriverRejoiningSave(DriverMaster driver)
        {
            try
            {

                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@iFk_DriverMstId", driver.driverID);
                param[1] = new SqlParameter("@dtRejoiningDate", driver.dtRejoiningDate);
                param[2] = new SqlParameter("@dAmount", driver.dAmount);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_DriverRejoining_Save]", param);
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

        [HttpGet("GetLicenseDetails")]
        public async Task<IActionResult> GetLicenseDetails(string licenseNumber, string dob)
        {
            try
            {
                if (string.IsNullOrEmpty(licenseNumber) || string.IsNullOrEmpty(dob))
                {
                    return BadRequest(new { Message = "License number and date of birth are required." });
                }

                using var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.invincibleocean.com/invincible/drivingLicenceV2");

                request.Headers.Add("secretKey", "coOZjG2IFrM0jMeYWWYo6Vm2rEVNpi8l2sCXnFsZ1i2eMX6qbyznBOirNpqC7O7lG");
                request.Headers.Add("clientId", "11f884fbc7de6705f0e5dad95dec25f6:d7bcd09c70213f190dac7cc24b07caf7");

                var jsonContent = new
                {
                    number = licenseNumber,
                    dob = dob
                };

                var content = new StringContent(JsonConvert.SerializeObject(jsonContent), Encoding.UTF8, "application/json");
                request.Content = content;

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                var licenseDetailsResponse = JsonConvert.DeserializeObject<LicenseDetailsResponse>(responseContent);

                return Ok(licenseDetailsResponse);
            }
            catch (HttpRequestException httpEx)
            {
                return StatusCode(502, new { Message = "Error connecting to License API.", Details = httpEx.Message });
            }
            catch (JsonSerializationException jsonEx)
            {
                return StatusCode(500, new { Message = "Error parsing API response.", Details = jsonEx.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpPost]
        [Route("addLicenceDetail")]
        public Api_CommonResponse addLicenceDetail(LicenceDataSave data, int driverID)
        {
            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@iFk_VehDrvtId", driverID);
                param[1] = new SqlParameter("@sLicsNumber", data.sLicsNumber);
                param[2] = new SqlParameter("@dtLicsIssue", data.dtLicsIssue);
                param[3] = new SqlParameter("@dtLicsExpiry", data.dtLicsExpiry);
                param[4] = new SqlParameter("@sIssueBy", data.sIssueBy);
                param[5] = new SqlParameter("@iType", data.iType);
                param[6] = new SqlParameter("@sLcsAttch", data.sLcsAttch);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Driver_DriverLicenceData_Save]", param);
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
        [Route("ReturnLicenseDocx")]
        public Api_CommonResponse ReturnLicenseDocx(DriverLicence driverLicence)
        {
            try
            {
                var driverLicenseURL = "";

                var driverName = driverLicence.driverName;
                driverName = "D_" + driverName.Replace(" ", "_");
                #region Driver License Documents
                if (driverLicence.sLcsAttch != null)
                {
                    var fileName = "LicenseCopy_" + DateTime.Now.ToString("ddMMyyyyhhmm");
                    driverLicenseURL = DocumentUploader.UploadDocuments(fileName, driverName, Convert.FromBase64String(driverLicence.sLcsAttch.Split(",")[1]), driverLicence.sLcsAttchExt);
                }

                #endregion
                api_Response.message = driverLicenseURL;
                api_Response.statusCode = 1;
            }
            catch (Exception e)
            {
                throw e;
            }
            return api_Response;
        }

        [HttpPost]
        [Route("CheckDuplicateLicNo")]
        public Api_CommonResponse CheckDuplicateLicNo(DriverLicence driverLicence)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@sLicsNumber", driverLicence.sLicsNumber);


                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Driver_DriverLicenceCheck_Get]", param);
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

    }
}
