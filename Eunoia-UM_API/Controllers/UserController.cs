using Eunoia_UM_API.Helper;
using Eunoia_UM_API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Generators;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Eunoia_UM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly JwtSettings _jwtSettings;

        Api_CommonResponse api_Response = new Api_CommonResponse();

        public UserController(IConfiguration configuration, JwtSettings jwtSettings)
        {
            _configuration = configuration;
            _jwtSettings = jwtSettings;
        }

        [HttpPost]
        [Route("AddNewUser")]
        public Api_CommonResponse AddNewUser(UserMaster party)
        {
            try
            {
                var pass = "";
                if (party.sUSRCode == null)
                {
                    pass = DBCS.Encrypt(party.bPassword);
                }

                //var encryptedPass = DBCS.Encrypt(party.bPassword);
                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@FirstName", party.sNME);
                param[1] = new SqlParameter("@MobileNumber", party.sPhone);
                param[2] = new SqlParameter("@EmailId", party.sEmail);
                param[3] = new SqlParameter("@Type", party.iDeptID);
                param[4] = new SqlParameter("@Password", pass);
                param[5] = new SqlParameter("@roleId", party.iRoleID);
                param[6] = new SqlParameter("@userName", party.sUSRNME);
                param[7] = new SqlParameter("@userCode", party.sUSRCode);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_MASTER_SystemUser_Save]", param);
                if (ds != null && ds.Tables != null)
                {
                    api_Response.responseCode = 1;
                    api_Response.message = ds.Tables[0].Rows[0][1].ToString();
                    api_Response.userCode = ds.Tables[0].Rows[0]["RoleName"].ToString();
                    api_Response.statusCode = 1;
                    //encryptedPass = ds.Tables[0].Rows[0]["encrytedPass"].ToString();
                    //if (party.sUSRCode == null || party.sUSRCode == "")
                    //{
                    //    var data = DBCS.SendMail(new SendEmail { RecieverDisplayName = party.sNME, Username = party.sEmail, RecieverEmailID = party.sEmail, Message = "Registration", Password = party.bPassword });
                    //}
                }
                else
                {
                    api_Response.responseCode = 0;
                    api_Response.message = "No Data Available...";
                    api_Response.statusCode = -1;
                    api_Response.userID = "";
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return api_Response;
        }

        [HttpPost]
        [Route("UpdateuserDetails")]
        public Api_CommonResponse UpdateuserDetails(UserMasterDetails _user)
        {
            try
            {
                string? userCode = "";
                SqlParameter[] param = new SqlParameter[13];
                param[0] = new SqlParameter("@sUSRCode", _user.sUSRCode);
                param[1] = new SqlParameter("@sNME", _user.sNME);
                param[2] = new SqlParameter("@sSurNME", _user.sSurNME);
                param[3] = new SqlParameter("@dtDOB", _user.dtDOB);
                param[4] = new SqlParameter("@sPhone", _user.sPhone);
                param[5] = new SqlParameter("@iCountryID", _user.iCountryID);
                param[6] = new SqlParameter("@iStateID", _user.iStateID);
                param[7] = new SqlParameter("@iCityID", _user.iCityID);
                param[8] = new SqlParameter("@sPincode", _user.sPincode);
                param[9] = new SqlParameter("@sAadhaar", DBCS.Masked(_user.sAadhaar));
                param[10] = new SqlParameter("@sPan", _user.sPan);
                param[11] = new SqlParameter("@sAddress", _user.sAddress);
                param[12] = new SqlParameter("@dtAnnvsryDate", _user.dtAnniversary);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_MASTER_UpdateUserDetails_Update]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.statusCode = 1;
                    api_Response.responseCode = 0;
                    api_Response.message = ds.Tables[0].Rows[0]["Message"].ToString();
                    api_Response.userCode = userCode = ds.Tables[0].Rows[0]["userCode"].ToString();
                }
                else
                {
                    api_Response.statusCode = 1;
                    api_Response.responseCode = 0;
                    api_Response.message = "No Data Available...";
                }

                if (_user.panImg != null)
                {
                    UploadPan(userCode, _user.panImg, "MyProfile");
                }
                if (_user.aadhaarImg != null)
                {
                    UploadAadhar(userCode, _user.aadhaarImg, "MyProfile");
                }
                if (_user.profileImg != null)
                {
                    UploadProfile(userCode, _user.profileImg, "MyProfile");
                }
            }
            catch (Exception e)
            {
                api_Response.statusCode = 0;
                api_Response.responseCode = 0;
                api_Response.message = e.Message;
                //ExceptionLogDL.SendExcepToDB(e, 0, "Class : AdminDL / Function : GetCustomEnum");
            }
            return api_Response;
        }

        [HttpPost]
        [Route("UploadProfile")]
        public int UploadProfile(string partyId, byte[] image, string formname)
        {
            var status = 0;
            try
            {
                var relativPath = "\\wwwroot\\images";
                var extention = ".jpg";

                var imageUploadPath = Path.Combine(Directory.GetCurrentDirectory() + relativPath, partyId + "Profile" + extention);

                if (System.IO.File.Exists(imageUploadPath))
                {
                    System.IO.File.Delete(imageUploadPath);
                    System.IO.File.WriteAllBytes(imageUploadPath, image);
                }
                else
                {
                    System.IO.File.WriteAllBytes(imageUploadPath, image);
                }

                var splittedRelativepath = relativPath.Split("\\");

                //var host = _httpContext.HttpContext.Request.Host;

                var serverImagePath = "/" +
                    splittedRelativepath[2] + "/" + partyId +
                    "Profile" + extention; // /images/<filename>.jpg

                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@PartyId", partyId);
                param[1] = new SqlParameter("@UploadDocumentUrl", serverImagePath);
                param[2] = new SqlParameter("@DocumentType", DBCS.GetProfileDocumentType());
                param[3] = new SqlParameter("@formName", formname);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_MASTER_DocumentUpload_Save]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.responseCode = 0;
                    api_Response.message = "Details Saved...";
                    api_Response.statusCode = 1;
                    status = 1;
                }
                else
                {
                    api_Response.responseCode = 0;
                    api_Response.message = "Details Not Saved...";
                    api_Response.statusCode = -1;
                    status = -1;
                }
            }
            catch (Exception ex)
            { }
            return status;
        }

        [HttpPost]
        [Route("UploadPan")]
        public int UploadPan(string partyId, byte[] image, string formname)
        {
            var status = 0;
            try
            {
                var relativPath = "\\wwwroot\\images";
                var extention = ".jpg";

                var imageUploadPath = Path.Combine(Directory.GetCurrentDirectory() + relativPath, partyId + "PAN" + extention);

                if (System.IO.File.Exists(imageUploadPath))
                {
                    System.IO.File.Delete(imageUploadPath);
                    System.IO.File.WriteAllBytes(imageUploadPath, image);
                }
                else
                {
                    System.IO.File.WriteAllBytes(imageUploadPath, image);
                }

                var splittedRelativepath = relativPath.Split("\\");

                //var host = _httpContext.HttpContext.Request.Host;

                var serverImagePath = "/" +
                    splittedRelativepath[2] + "/" + partyId +
                    "PAN" + extention; // /images/<filename>.jpg

                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@PartyId", partyId);
                param[1] = new SqlParameter("@UploadDocumentUrl", serverImagePath);
                param[2] = new SqlParameter("@DocumentType", DBCS.GetPANDocumentType());
                param[3] = new SqlParameter("@formName", formname);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_MASTER_DocumentUpload_Save]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.responseCode = 0;
                    api_Response.message = "Details Saved...";
                    api_Response.statusCode = 1;
                    status = 1;
                }
                else
                {
                    api_Response.responseCode = 0;
                    api_Response.message = "Details Not Saved...";
                    api_Response.statusCode = -1;
                    status = -1;
                }
            }
            catch (Exception ex)
            { }
            return status;
        }

        [HttpPost]
        [Route("UploadReciept")]
        public int UploadReciept(string partyId, byte[] image, string formname)
        {
            var status = 0;
            try
            {
                var relativPath = "\\wwwroot\\images";
                var extention = ".jpg";

                var imageUploadPath = Path.Combine(Directory.GetCurrentDirectory() + relativPath, partyId + "PaymentReciept" + extention);

                if (System.IO.File.Exists(imageUploadPath))
                {
                    System.IO.File.Delete(imageUploadPath);
                    System.IO.File.WriteAllBytes(imageUploadPath, image);
                }
                else
                {
                    System.IO.File.WriteAllBytes(imageUploadPath, image);
                }

                var splittedRelativepath = relativPath.Split("\\");

                //var host = _httpContext.HttpContext.Request.Host;

                var serverImagePath = "/" +
                    splittedRelativepath[2] + "/" + partyId +
                    "PaymentReciept" + extention; // /images/<filename>.jpg

                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@PartyId", partyId);
                param[1] = new SqlParameter("@UploadDocumentUrl", serverImagePath);
                param[2] = new SqlParameter("@DocumentType", DBCS.GetPaymentRecieptDocumentType());
                param[3] = new SqlParameter("@formName", formname);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_MASTER_DocumentUpload_Save]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.responseCode = 0;
                    api_Response.message = "Details Saved...";
                    api_Response.statusCode = 1;
                    status = 1;
                }
                else
                {
                    api_Response.responseCode = 0;
                    api_Response.message = "Details Not Saved...";
                    api_Response.statusCode = -1;
                    status = -1;
                }
            }
            catch (Exception ex)
            { }
            return status;
        }
        [HttpPost]
        [Route("UploadAadhar")]
        public int UploadAadhar(string partyId, byte[] image, string formname)
        {
            var status = 0;
            try
            {
                var relativPath = "\\wwwroot\\images";
                var extention = ".jpg";

                var imageUploadPath = Path.Combine(Directory.GetCurrentDirectory() + relativPath, partyId + "AADHAR" + extention);

                if (System.IO.File.Exists(imageUploadPath))
                {
                    System.IO.File.Delete(imageUploadPath);
                    System.IO.File.WriteAllBytes(imageUploadPath, image);
                }
                else
                {
                    System.IO.File.WriteAllBytes(imageUploadPath, image);
                }
                var splittedRelativepath = relativPath.Split("\\");

                //var host = _httpContext.HttpContext.Request.Host;

                var serverImagePath = "/" +
                    splittedRelativepath[2] + "/" + partyId +
                    "AADHAR" + extention; // /images/<filename>.jpg

                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@PartyId", partyId);
                param[1] = new SqlParameter("@UploadDocumentUrl", serverImagePath);
                param[2] = new SqlParameter("@DocumentType", DBCS.GetAadhaarDocumentType());
                param[3] = new SqlParameter("@formName", formname);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_MASTER_DocumentUpload_Save]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.responseCode = 0;
                    api_Response.message = "Details Saved...";
                    api_Response.statusCode = 1;
                    status = 1;

                }
                else
                {
                    api_Response.responseCode = 0;
                    api_Response.message = "Details Not Saved...";
                    api_Response.statusCode = -1;
                    status = -1;
                }
            }
            catch (Exception ex)
            { }
            return status;
        }

        [HttpPost]
        [Route("userLogin")]
        public Api_CommonResponse Login(LoginModal login)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@EmailId", login.Username);
                param[1] = new SqlParameter("@Passwordstr", DBCS.Encrypt(login.Password));
                param[2] = new SqlParameter("@BranchId", login.BranchId);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_MASTER_UserLoginCheck_Select]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    string username = login.Username;
                    int userId = Convert.ToInt32(ds.Tables[0].Rows[0]["iPK_USRID"]);

                    //  JWT generate
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, username),
                        new Claim(ClaimTypes.Role, "admin") // or role from the DB
                    };

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(claims),
                        Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_jwtSettings.TokenExpiryMinutes)),
                        Issuer = _jwtSettings.Issuer,
                        Audience = _jwtSettings.Audience,
                        SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
                    };


                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var jwt = tokenHandler.WriteToken(token);

                    string clientIP = GetClientIpAddress();

                    //  Save token + expiry date to DB
                    DateTime expiry = DateTime.UtcNow.AddDays(_jwtSettings.TokenExpiryDays);
                    InsertJwtToken(userId, jwt, expiry, clientIP);

                    // Save login history
                    InsertUserLoginHistory(userId, clientIP);

                    api_Response.responseCode = 0;
                    api_Response.message = ds.Tables[0].Rows[0]["Message"].ToString();
                    api_Response.data = JsonConvert.SerializeObject(ds.Tables[0]);
                    api_Response.statusCode = Convert.ToInt32(ds.Tables[0].Rows[0]["StatusCode"].ToString());
                    api_Response.data1 = JsonConvert.DeserializeObject<List<LoginMobile>>(api_Response.data.ToString());
                    api_Response.Rights = JsonConvert.DeserializeObject<List<RightsDet>>(JsonConvert.SerializeObject(ds.Tables[1]).ToString());

                    api_Response.TokenId = jwt;
                }
                else
                {

                    //api_Response.responseCode = -1;
                    //api_Response.message = ds.Tables[0].Rows[0]["Message"].ToString();
                    //api_Response.statusCode = -1;

                    api_Response.responseCode = -1;
                    api_Response.message = (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        ? ds.Tables[0].Rows[0]["Message"].ToString()
                        : "Invalid login or system error.";
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
        [Route("Reset")]
        public Api_CommonResponse ResetPassword(ResetPassword resetpassword)
        {
            try
            {

                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@email", resetpassword.Email);
                param[1] = new SqlParameter("@password", (resetpassword.Type != "Reset") ? "" : DBCS.Encrypt(resetpassword.Password));
                param[2] = new SqlParameter("@type", resetpassword.Type);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_ResetUsersPassword_SelectUpdate]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.responseCode = 0;
                    api_Response.message = ds.Tables[0].Rows[0]["Message"].ToString();
                    api_Response.statusCode = Convert.ToInt32(ds.Tables[0].Rows[0]["StatusCode"]);
                }
                else
                {
                    api_Response.responseCode = 1;
                    api_Response.message = "Not Saved...";
                    api_Response.statusCode = -1;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return api_Response;
        }

        [Authorize]
        [HttpGet]
        [Route("GetuserMasterDetails")]
        public Api_CommonResponse GetuserMasterDetails(string userID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@UserID", userID);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_UserDetails_select]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.responseCode = 0;
                    api_Response.message = "User Details";
                    api_Response.data = JsonConvert.SerializeObject(ds.Tables[0]);
                    api_Response.statusCode = 1;
                }
                else
                {
                    api_Response.responseCode = 1;
                    api_Response.message = "User Details Not Found...";
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
        [Route("OTPAtResetPassword")]
        public Api_CommonResponse SendOTPWhileResetpassword(ResetPassword resetpassword)
        {
            try
            {
                if (resetpassword != null)
                {
                    var mobileOTP = DBCS.GetOTP();
                    var emailOTP = DBCS.GetOTP();

                    var displayName = resetpassword.Email.Split('@')[0].ToString();

                    var emailStatus = DBCS.SendMail(new SendEmail { Message = "EmailOTP", RecieverDisplayName = displayName, RecieverEmailID = resetpassword.Email, emailOTP = emailOTP, mobileOTP = mobileOTP });

                    if (emailStatus == "Email Sent Successfully ...!!!")
                    {
                        api_Response.responseCode = 0;
                        api_Response.message = emailStatus;
                        api_Response.statusCode = 1;
                        api_Response.mobileOTP = mobileOTP;
                        api_Response.emailOTP = emailOTP;
                    }
                    else
                    {
                        api_Response.responseCode = 0;
                        api_Response.message = emailStatus;
                        api_Response.statusCode = 1;
                        api_Response.mobileOTP = 0;
                        api_Response.emailOTP = 0;
                    }
                }
                else
                {
                    api_Response.responseCode = 0;
                    api_Response.message = "Details Not Found...";
                    api_Response.statusCode = 1;
                    api_Response.mobileOTP = 0;
                    api_Response.emailOTP = 0;
                }

            }
            catch (Exception e)
            {
                throw e;
            }
            return api_Response;
        }

        [HttpGet]
        [Route("GetApprovalData")]
        public Api_CommonResponse GetApprovalData()
        {
            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_GetApprovalDetails_Get]");
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Approval List";
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

        //get data for submenu
        [HttpGet]
        [Route("GetSubCategory")]
        public Api_CommonResponse GetSubCategory(int iFk_Category)
        {
            Api_CommonResponse res = new Api_CommonResponse();
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@iFk_Category", iFk_Category);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_ServiceRate_SubCtgry_Get]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    res.statusCode = 1;
                    res.responseCode = 0;
                    res.message = "Data Fetched Successfully...";
                    res.data = JsonConvert.SerializeObject(ds.Tables[0]);
                }
                else
                {
                    res.statusCode = 1;
                    res.responseCode = 0;
                    res.message = "No Data Available...";
                }
            }
            catch (Exception e)
            {
                res.statusCode = 0;
                res.responseCode = 0;
                res.message = e.Message;
            }
            return res;
        }
        [HttpGet]
        [Route("GetCntrctDtls")]
        public Api_CommonResponse GetCntrctDtls(int id)
        {
            Api_CommonResponse res = new Api_CommonResponse();
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@id", id);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_CntrctDtl_Get]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    res.statusCode = 1;
                    res.responseCode = 0;
                    res.message = "Data Fetched Successfully...";
                    res.data = JsonConvert.SerializeObject(ds.Tables[0]);
                }
                else
                {
                    res.statusCode = 1;
                    res.responseCode = 0;
                    res.message = "No Data Available...";
                }
            }
            catch (Exception e)
            {
                res.statusCode = 0;
                res.responseCode = 0;
                res.message = e.Message;
            }
            return res;
        }
        [Route("GetDriverDetails")]
        [HttpGet]
        public Api_CommonResponse GetDriverDetails(int id)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@id", id);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_DRVMST_Get]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.statusCode = 1;
                    api_Response.responseCode = 0;
                    api_Response.message = "Data Fetched Successfully...";
                    api_Response.data = JsonConvert.SerializeObject(ds.Tables[0]);
                }
                else
                {
                    api_Response.statusCode = 1;
                    api_Response.responseCode = 0;
                    api_Response.message = "No Data Available...";
                }
            }
            catch (Exception e)
            {
                api_Response.statusCode = 0;
                api_Response.responseCode = 0;
                api_Response.message = e.Message;
            }
            return api_Response;
        }

        [Route("GetTrailerDetails")]
        [HttpGet]
        public Api_CommonResponse GetTrailerDetails(int id, int id1)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@id", id);
                param[1] = new SqlParameter("@id1", id1);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_GetTrlMst_Get]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.statusCode = 1;
                    api_Response.responseCode = 0;
                    api_Response.message = "Data Fetched Successfully...";
                    api_Response.data = JsonConvert.SerializeObject(ds.Tables[0]);
                }
                else
                {
                    api_Response.statusCode = 1;
                    api_Response.responseCode = 0;
                    api_Response.message = "No Data Available...";
                }
            }
            catch (Exception e)
            {
                api_Response.statusCode = 0;
                api_Response.responseCode = 0;
                api_Response.message = e.Message;
            }
            return api_Response;
        }

        //trip expense *****************

        [HttpGet]
        [Route("GetcardNumber")]
        public Api_CommonResponse GetcardNumber(int id)
        {
            Api_CommonResponse res = new Api_CommonResponse();
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@SelectedCardTypeId", id);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_GetCardNumbers_Get]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    res.statusCode = 1;
                    res.responseCode = 0;
                    res.message = "Data Fetched Successfully...";
                    res.data = JsonConvert.SerializeObject(ds.Tables[0]);
                }
                else
                {
                    res.statusCode = 1;
                    res.responseCode = 0;
                    res.message = "No Data Available...";
                }
            }
            catch (Exception e)
            {
                res.statusCode = 0;
                res.responseCode = 0;
                res.message = e.Message;
            }
            return res;
        }
        //get entry data
        [Route("EntryDate")]
        [HttpGet]
        public Api_CommonResponse EntryDate(int id)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@id", id);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_GetEntrydate_Get]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.statusCode = 1;
                    api_Response.responseCode = 0;
                    api_Response.message = "Data Fetched Successfully...";
                    api_Response.data = JsonConvert.SerializeObject(ds.Tables[0]);
                }
                else
                {
                    api_Response.statusCode = 1;
                    api_Response.responseCode = 0;
                    api_Response.message = "No Data Available...";
                }
            }
            catch (Exception e)
            {
                api_Response.statusCode = 0;
                api_Response.responseCode = 0;
                api_Response.message = e.Message;
            }
            return api_Response;
        }



        [HttpPost]
        [Route("UpdateAddUserDetails")]
        public Api_CommonResponse UpdateAddUserDetails(UserMaster party)
        {
            try
            {
                var pass = "";
                if (party.sUSRCode != null)
                {
                    pass = DBCS.Encrypt(party.bPassword);
                }

                //var encryptedPass = DBCS.Encrypt(party.bPassword);
                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@FirstName", party.sNME);
                param[1] = new SqlParameter("@MobileNumber", party.sPhone);
                param[2] = new SqlParameter("@EmailId", party.sEmail);
                param[3] = new SqlParameter("@Type", party.iDeptID);
                param[4] = new SqlParameter("@Password", pass);
                param[5] = new SqlParameter("@roleId", party.iRoleID);
                param[6] = new SqlParameter("@userName", party.sUSRNME);
                param[7] = new SqlParameter("@userCode", party.sUSRCode);


                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_MASTER_SystemUser_Update]", param);
                if (ds != null && ds.Tables != null)
                {
                    api_Response.responseCode = 1;
                    api_Response.message = ds.Tables[0].Rows[0][1].ToString();
                    api_Response.userCode = ds.Tables[0].Rows[0]["RoleName"].ToString();
                    api_Response.statusCode = 1;
                    //encryptedPass = ds.Tables[0].Rows[0]["encrytedPass"].ToString();
                    //if (party.sUSRCode == null || party.sUSRCode == "")
                    //{
                    //    var data = DBCS.SendMail(new SendEmail { RecieverDisplayName = party.sNME, Username = party.sEmail, RecieverEmailID = party.sEmail, Message = "Registration", Password = party.bPassword });
                    //}
                }
                else
                {
                    api_Response.responseCode = 0;
                    api_Response.message = "No Data Available...";
                    api_Response.statusCode = -1;
                    api_Response.userID = "";
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return api_Response;
        }

        private void InsertJwtToken(int userId, string jwt, DateTime expiry, string clientIP)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@iUserId", userId),
                new SqlParameter("@sJwtToken", jwt),
                new SqlParameter("@dtExpiry", expiry),
                new SqlParameter("@sIPAddress", clientIP)
            };

            DBOperation.ExecuteQuery("USP_InsertJWTToken", param);
        }

        private string GetClientIpAddress()
        {
            // Attempt to get client IP from 'X-Forwarded-For' header (used by proxies/load balancers)
            string ip = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();

            if (string.IsNullOrEmpty(ip))
            {
                // Fallback: Get the remote IP from the connection context
                ip = HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
            }

            return ip;
        }

        private void InsertUserLoginHistory(int userId, string clientIP)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@iUserId", userId),
                new SqlParameter("@sIPAddress", clientIP)
            };

            DBOperation.ExecuteQuery("USP_InsertUserLoginIPLogs", param);
        }


    }
}
