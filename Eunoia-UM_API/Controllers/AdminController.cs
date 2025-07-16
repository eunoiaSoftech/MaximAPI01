using Eunoia_UM_API.Helper;
using Eunoia_UM_API.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;

namespace Eunoia_UM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        Api_CommonResponse api_Response = new Api_CommonResponse();

        [HttpGet]
        [Route("GetSetting")]
        public Api_CommonResponse GetSetting()
        {
            List<setting> objlist = new List<setting>();
            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_CustomFieldEnumSetting_View]");
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    objlist = new List<setting>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        setting objdoc = new setting();
                        objdoc.Id = Convert.ToInt32(ds.Tables[0].Rows[i]["iSttngId"]);
                        objdoc.Name = ds.Tables[0].Rows[i]["sName"].ToString();
                        objdoc.IsActive = Convert.ToInt32(ds.Tables[0].Rows[i]["iIsActv"]);
                        objlist.Add(objdoc);
                    }

                    api_Response.message = "Settings List";
                    api_Response.status = 200;
                    api_Response.data = objlist;
                    api_Response.responseCode = 1;
                }
                else
                {
                    api_Response.message = "No Data Available";
                    api_Response.status = 400;
                    api_Response.responseCode = 0;
                }
            }
            catch (Exception e)
            {
                api_Response.message = e.Message.ToString();
                api_Response.status = 400;
                api_Response.responseCode = 0;

            }
            return api_Response;
        }

        [HttpGet]
        [Route("GetCustomEnum")]
        public Api_CommonResponse GetCustomEnum()
        {
            List<CustomEnum> objlist = new List<CustomEnum>();
            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_CustomFields_View]");
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    objlist = new List<CustomEnum>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CustomEnum objdoc = new CustomEnum();
                        objdoc.CustomEnumId = Convert.ToInt32(ds.Tables[0].Rows[i]["iPK_CustEnum"]);
                        objdoc.EnumNo = Convert.ToInt32(ds.Tables[0].Rows[i]["iFK_EnumNo"]);

                        objdoc.EnumName = Convert.ToString(ds.Tables[0].Rows[i]["EnumName"]);
                        objdoc.Name = ds.Tables[0].Rows[i]["sName"].ToString();
                        objdoc.IsActive = Convert.ToInt32(ds.Tables[0].Rows[i]["Active"]);
                        objlist.Add(objdoc);

                    }
                    api_Response.message = "Enum Setting List";
                    api_Response.status = 200;
                    api_Response.data = objlist;
                    api_Response.responseCode = 1;
                }
                else
                {
                    api_Response.message = "No Data Available";
                    api_Response.status = 400;
                    api_Response.responseCode = 0;
                }
            }
            catch (Exception e)
            {
                api_Response.message = "No Data Available";
                api_Response.status = 400;
                api_Response.responseCode = 0;
            }
            return api_Response;

        }

        [HttpPost]
        [Route("InsertNewCustomEnumRow")]
        public Api_CommonResponse InsertNewCustomEnumRow(int Id)
        {
            int master = 0;
            try
            {

                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@EnumNo", Id);
                param[1] = new SqlParameter("@Type", 1);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_NewFieldinEnum_SaveUpdateDelete]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        master = Convert.ToInt32(ds.Tables[0].Rows[i]["iPK_CustEnum"]);
                    }
                    api_Response.message = "New Enum Entry";
                    api_Response.status = 200;
                    api_Response.userID = master.ToString();
                    api_Response.responseCode = 1;
                }
            }
            catch (Exception e)
            {
                api_Response.message = "New Enum Entry";
                api_Response.status = 400;
                api_Response.userID = "0";
                api_Response.responseCode = 0;
            }
            return api_Response;
        }

        [HttpPost]
        [Route("DeleteCustomSentence")]
        public Api_CommonResponse DeleteCustomSentence(int Id)
        {
            int Master = 0;
            try
            {

                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@EnumNo", Id);
                param[1] = new SqlParameter("@Type", 2);
                DataTable DT = DBOperation.FillDataTable("[dbo].[USP_ADMIN_NewFieldinEnum_SaveUpdateDelete]", param);
                if (DT != null)
                {
                    if (DT.Rows.Count > 0)
                    {
                        Master = Convert.ToInt32(DT.Rows[0]["StatusCode"]);
                    }
                    api_Response.message = "Delete Custom Enum";
                    api_Response.status = 200;
                    api_Response.userID = Master.ToString();
                    api_Response.responseCode = 1;
                }
            }
            catch (Exception e)
            {
                api_Response.message = "Delete Custom Enum";
                api_Response.status = 400;
                api_Response.userID = "0";
                api_Response.responseCode = 0;
            }
            return api_Response;
        }

        [HttpPost]
        [Route("UpdateSubjectLines")]
        public Api_CommonResponse UpdateSubjectLines(int Id, string Text)
        {
            int Master = 0;
            try
            {

                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@Id", Id);
                param[1] = new SqlParameter("@Text", Text);
                DataTable DT = DBOperation.FillDataTable("[dbo].[USP_MASTER_UpdateSettingTitle_Update]", param);
                if (DT != null)
                {
                    if (DT.Rows.Count > 0)
                    {
                        Master = Convert.ToInt32(DT.Rows[0]["StatusCode"]);
                    }
                    api_Response.message = "Update Custom Enum";
                    api_Response.status = 200;
                    api_Response.userID = Master.ToString();
                    api_Response.responseCode = 1;
                }
            }
            catch (Exception e)
            {
                api_Response.message = "Updated Custom Enum";
                api_Response.status = 400;
                api_Response.userID = "0";
                api_Response.responseCode = 0;
            }
            return api_Response;
        }

        [HttpGet]
        [Route("GetAllSettings")]
        public Api_CommonResponse GetAllSettings(string Id = null)
        {
            try
            {

                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Id", Id);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_CustomFieldSetting_View]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.responseCode = 1;
                    if (Id == null)
                        api_Response.message = "Settings  List";
                    else
                        api_Response.message = "Setting";

                    api_Response.statusCode = 1;
                    api_Response.data = JsonConvert.SerializeObject(ds.Tables[0]);
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
            }
            return api_Response;
        }

        [HttpPost]
        [Route("UpdateSetting")]
        public Api_CommonResponse UpdateSetting(Settings settings)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@id", settings.Id);
                param[1] = new SqlParameter("@name", settings.SettingName);
                param[2] = new SqlParameter("@status", settings.IsActive);
                param[3] = new SqlParameter("@type", settings.Type);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_MASTER_ManageMainSetting_Update]", param);
                if (ds != null && ds.Tables != null)
                {
                    api_Response.responseCode = 1;
                    if (settings.Type == "Search")
                    {
                        api_Response.data = JsonConvert.SerializeObject(ds.Tables[0]);
                        api_Response.message = ds.Tables[1].Rows[0][1].ToString();
                    }
                    else
                    {
                        //api_Response.data = new DataTable();
                        api_Response.message = ds.Tables[0].Rows[0][1].ToString();
                    }

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
            }
            return api_Response;
        }

        [HttpGet]
        [Route("UpdateStatus")]
        public Api_CommonResponse ActiveInactiveEnum(int status, string userID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@status", status);
                param[1] = new SqlParameter("@userID", userID);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_MASTER_ChangeEnumStatus_Update]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.responseCode = 0;
                    api_Response.message = ds.Tables[0].Rows[0]["Message"].ToString();
                    api_Response.statusCode = Convert.ToInt32(ds.Tables[0].Rows[0]["StatusCode"].ToString());
                    api_Response.data = null;
                }
                else
                {
                    api_Response.responseCode = 1;
                    api_Response.message = $"We are facing server issue at the moment please try again...";
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
        [Route("GetMasterDataForDropdown")]
        public Api_CommonResponse GetMasterDataForDropdown(string type, string? EnumName = null, string? ExtraID = null)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@type", type);
                param[1] = new SqlParameter("@EnumName", EnumName);
                param[2] = new SqlParameter("@ExtraId", ExtraID);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_GetMasterDataForDropdown_View]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.responseCode = 0;
                    api_Response.message = $"{type} List";
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

    }
}
