using Eunoia_UM_API.Helper;
using Eunoia_UM_API.Model;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace Eunoia_UM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        Api_CommonResponse api_Response = new Api_CommonResponse();


        [Route("GetFillDropDowns")]
        [HttpGet]
        public Api_CommonResponse GetFillDropDowns(string selectionType, int enumID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@selectionType", selectionType);
                param[1] = new SqlParameter("@id", enumID);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_CustomFields_View]", param);
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

                //ExceptionLogDL.SendExcepToDB(e, 0, "Class : AdminDL / Function : GetCustomEnum");
            }
            return api_Response;
        }


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

        [Route("GetDataForTable")]
        [HttpGet]
        public Api_CommonResponse GetDataForTable(string type, string? EnumName = null, string? ExtraId = null)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@type", type);
                param[1] = new SqlParameter("@EnumName", EnumName);
                param[2] = new SqlParameter("@ExtraId", ExtraId);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_GetMasterDataForTable_View]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.statusCode = 1;
                    api_Response.responseCode = 0;
                    api_Response.message = $"Data For {type} Fetched Successfully...";
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

                //ExceptionLogDL.SendExcepToDB(e, 0, "Class : AdminDL / Function : GetCustomEnum");
            }
            return api_Response;
        }

        [Route("GetCountryStateCity")]
        [HttpGet]
        public Api_CommonResponse GetCountryStateCity(int countryID, int stateID, string type)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@type", type);
                param[1] = new SqlParameter("@countryID", countryID);
                param[2] = new SqlParameter("@stateID", stateID);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_GetCountryStateCity_Select]", param);
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

                //ExceptionLogDL.SendExcepToDB(e, 0, "Class : AdminDL / Function : GetCustomEnum");
            }
            return api_Response;
        }

        [HttpPost]
        [Route("GetPermissionDetails")]
        //public Api_CommonResponse GetPermissionDetails(int RoleId, int DepartmentId)
        //{
        //    try
        //    {

        //        SqlParameter[] param = new SqlParameter[2];
        //        param[0] = new SqlParameter("@roleId", RoleId);
        //        param[1] = new SqlParameter("@deptId", DepartmentId);

        //        DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_PremissionDetails_View]", param);
        //        if (ds != null && ds.Tables != null)
        //        {
        //            api_Response.responseCode = 1;
        //            api_Response.data = JsonConvert.SerializeObject(ds.Tables[0]);
        //            api_Response.message = "User Pages Access";
        //            api_Response.statusCode = 1;
        //        }
        //        else
        //        {
        //            api_Response.responseCode = 0;
        //            api_Response.message = "No Data Available...";
        //            api_Response.statusCode = -1;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //    return api_Response;
        //}


        //usermgt new abhishek code
        public Api_CommonResponse GetPermissionDetails(int? RoleId, int? DepartmentId, int? BranchId, int? LocationId, string? sUSRCode)
        {
            try
            {

                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@roleId", RoleId);
                param[1] = new SqlParameter("@deptId", DepartmentId);
                param[2] = new SqlParameter("@branchId", BranchId);
                param[3] = new SqlParameter("@locationId", LocationId);
                param[4] = new SqlParameter("@usrcode", sUSRCode);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_PremissionDetails_View]", param);
                if (ds != null && ds.Tables != null)
                {
                    api_Response.responseCode = 1;
                    api_Response.data = JsonConvert.SerializeObject(ds.Tables[0]);
                    api_Response.message = "User Pages Access";
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

        [Route("GetLocationList")]
        [HttpGet]
        public Api_CommonResponse GetLocationList()
        {
            try
            {

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_GetLocation_List]");
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

                //ExceptionLogDL.SendExcepToDB(e, 0, "Class : AdminDL / Function : GetCustomEnum");
            }
            return api_Response;
        }

        [Route("VisitorsSummary")]
        [HttpGet]
        public Api_CommonResponse VisitorsSummary()
        {
            try
            {

                DataSet ds = DBOperation.FillDataSet("[dbo].[Usp_Visitors_Daily_Summary]");
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

                //ExceptionLogDL.SendExcepToDB(e, 0, "Class : AdminDL / Function : GetCustomEnum");
            }
            return api_Response;
        }


        [HttpGet]
        [Route("GetLocationListBranchwise")]
        public Api_CommonResponse GetLocationListBranchwise(int Fk_BranchId, int UserId)
        {

            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@Fk_BranchId", Fk_BranchId);
                param[1] = new SqlParameter("@UserId", UserId);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_GetLocationListBranchwise]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Location List";
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
        [Route("GetUserlistDataForDropdown")]
        public Api_CommonResponse GetUserlistDataForDropdown(string selectionType, int Id, string? ExtraID = null)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@type", selectionType);
                param[1] = new SqlParameter("@Id", Id);
                param[2] = new SqlParameter("@ExtraId", ExtraID);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Common_GetUserlistDataForDropdown]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.responseCode = 0;
                    api_Response.message = $"{selectionType} List";
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

        public static Dictionary<string, string> UploadChallanAttachment(string base64Data, string folderName, string fileName)
        {
            Dictionary<string, string> status = new Dictionary<string, string>();
            try
            {
                var relativPath = "\\wwwroot\\images\\Challan";
                var extention = ".jpg";

                string folderPath = Path.Combine(Directory.GetCurrentDirectory() + relativPath, folderName);

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var imageUploadPath = folderPath + "\\" + fileName;
                // Convert Base64 string to byte array
                byte[] fileData = Convert.FromBase64String(base64Data);


                if (System.IO.File.Exists(imageUploadPath))
                {
                    System.IO.File.Delete(imageUploadPath);
                    System.IO.File.WriteAllBytes(imageUploadPath, fileData);
                }
                else
                {
                    System.IO.File.WriteAllBytes(imageUploadPath, fileData);
                }

                var splittedRelativepath = relativPath.Split("\\");

                //var host = _httpContext.HttpContext.Request.Host;

                var serverImagePath = "/" + splittedRelativepath[2] + "/" + splittedRelativepath[3] + "/" + folderName + "/" + fileName; // /images/<filename>.jpg

                status.Add("DBPath", serverImagePath);
                status.Add("ABSPath", imageUploadPath);
            }
            catch (Exception ex)
            { }
            return status;
        }

    }
}


