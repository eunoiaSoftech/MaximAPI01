using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Eunoia_UM_API.Helper;
using System.Data;
using System.Data.SqlClient;
using Eunoia_UM_API.Model;
using System;
using System.Security.Cryptography;

namespace Eunoia_UM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleMasterController : ControllerBase
    {
        Api_CommonResponse api_Response = new Api_CommonResponse();

        [Route("InsertMenuMaster")]
        [HttpPost]
        public Api_CommonResponse InsertMenuMaster(MenuMasters obj)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@MenuId", obj.MenuId);
                param[1] = new SqlParameter("@MenuName", obj.MenuName);
                param[2] = new SqlParameter("@ControllerName", obj.ControllerName);
                param[3] = new SqlParameter("@ActionName", obj.ActionMethod);
                param[4] = new SqlParameter("@Status", obj.Status);
                param[5] = new SqlParameter("@ihierarchyActive", obj.ihierarchyActive);

                DataTable DT = DBOperation.FillDataTable("[dbo].[USP_ADMIN_ManageMenus_SaveUpdateView]", param);
                if (DT != null)
                {
                    if (DT.Rows.Count > 0)
                    {
                        api_Response.statusCode = Convert.ToInt32(DT.Rows[0]["StatusCode"]);
                        api_Response.message = DT.Rows[0]["Message"].ToString();

                    }
                }
                else
                {
                    api_Response.statusCode = 0;
                    api_Response.message = "Failed";
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
        [Route("GetMenuMasterList")]
        public Api_CommonResponse GetMenuMasterList()
        {
            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_DisplayMenu_View]");

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
        [Route("GetSubMenuMasterList")]
        public Api_CommonResponse GetSubMenuMasterList()
        {


            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_GetSystemSubmenu_View]");

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
        [Route("InsertSubMenuMaster")]
        public Api_CommonResponse InsertSubMenuMaster(SubMenuMaster obj)
        {
            try
            {

                //var json = JsonConvert.SerializeObject(obj);
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@SubMenuId", obj.iSubMnuId);
                param[1] = new SqlParameter("@MenuId", obj.iFK_MnuId);
                param[2] = new SqlParameter("@SubMenuName", obj.sSubMnuName);
                param[3] = new SqlParameter("@ControllerName", obj.sCntrolName);
                param[4] = new SqlParameter("@ActionMethod", obj.sActnMthd);
                param[5] = new SqlParameter("@Status", obj.iStatus);
                param[6] = new SqlParameter("@ihierarchyActive", obj.ihierarchyActive);
                DataTable DT = DBOperation.FillDataTable("[dbo].[USP_ADMIN_ManageSubMenus_SaveUpdateView]", param);
                if (DT != null)
                {
                    if (DT.Rows.Count > 0)
                    {
                        api_Response.statusCode = Convert.ToInt32(DT.Rows[0]["StatusCode"]);
                        api_Response.message = DT.Rows[0]["Message"].ToString();

                    }
                }
                else
                {
                    api_Response.statusCode = 0;
                    api_Response.message = "Failed";
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
        [Route("GetMenulist")]
        public Api_CommonResponse GetMenulist(int MenuId)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@MenuId", MenuId);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_GetSUBMenuByMenuId_Select]", param);

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

        [Route("InsertRateMaster")]
        [HttpPost]
        public Api_CommonResponse InsertRateMaster(RateMaster obj)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[11];
                param[0] = new SqlParameter("@iPk_RateMstId", obj.iPk_RateMstId);
                param[1] = new SqlParameter("@iMnuid", obj.iMnuid);
                param[2] = new SqlParameter("@iSbMnu", obj.iSbMnu);
                param[3] = new SqlParameter("@sMnuNme", obj.sMnuNme);
                param[4] = new SqlParameter("@sSbMnuNme", obj.sSbMnuNme);
                param[5] = new SqlParameter("@dRate", obj.dRate);
                param[6] = new SqlParameter("@dtStrDt", obj.dtInsertStrDt);


                param[7] = new SqlParameter("@iTyp", obj.iTyp);
                param[8] = new SqlParameter("@iStts", obj.iStts);
                param[9] = new SqlParameter("@iClnId", obj.iClnId);
                param[10] = new SqlParameter("@dtEndDt", obj.dtInsertEndDt);

                DataTable DT = DBOperation.FillDataTable("[dbo].[Usp_Admin_Rate_Mst_Save]", param);
                if (DT != null)
                {
                    if (DT.Rows.Count > 0)
                    {
                        api_Response.statusCode = Convert.ToInt32(DT.Rows[0]["StatusCode"]);
                        api_Response.message = DT.Rows[0]["Message"].ToString();

                    }
                }
                else
                {
                    api_Response.statusCode = 0;
                    api_Response.message = "Failed";
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
        [Route("GetRateMaterList")]
        public Api_CommonResponse GetRateMaterList()
        {


            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@QueryType", "Select");
                DataSet ds = DBOperation.FillDataSet("[dbo].[Usp_Admin_Rate_Mst_Save]", param);

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
        [Route("GetData")]

        public Api_CommonResponse GetData(string Type, string? MenuId = null)

        //public Api_CommonResponse GetData(string Type, string? MenuId=null,string? extraId=null)

        {
            try
            {

                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@type", Type);
                param[1] = new SqlParameter("@EnumName", MenuId);
                //param[2] = new SqlParameter("@ExtraId", extraId);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_GetMasterDataForDropdown_View]", param);
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
        [Route("AddDepartment")]
        public Api_CommonResponse AddDepartment(AddDepartment department)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@DepartmentId", department.DepartmentID);
                param[1] = new SqlParameter("@DepartmentName", department.DepartmentName);
                param[2] = new SqlParameter("@PartyId", department.PartyId);
                param[3] = new SqlParameter("@CreatedBy", department.PartyId);
                param[4] = new SqlParameter("@type", department.Type);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_CreateNewDepartment_Save]", param);
                if (ds != null && ds.Tables != null)
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
        [Route("ChangeStatus")]
        public Api_CommonResponse ChangeStatus(Activeclass obj)
        {
            try
            {

                DataTable DT = new DataTable();
                SqlParameter[] param = null;
                if (obj.status == 2 || obj.Tablename != "RateMaster")
                {
                    param = new SqlParameter[5];
                    param[0] = new SqlParameter("@tablename", obj.Tablename);
                    param[1] = new SqlParameter("@Id", obj.Id);
                    param[2] = new SqlParameter("@type", obj.status);
                    param[3] = new SqlParameter("@Display", obj.Display);
                    param[4] = new SqlParameter("@Guid", obj.Guid);
                    DT = DBOperation.FillDataTable("[dbo].[USP_MASTER_ActivateDeactivateMastersForm_Update_new]", param);
                }
                else
                {
                    param = new SqlParameter[1];
                    param[0] = new SqlParameter("@RatemasterId", obj.Id);
                    DT = DBOperation.FillDataTable("[dbo].[USP_ADMIN_ManageRateStatus_Update]", param);
                }
                if (DT != null)
                {
                    if (DT.Rows.Count > 0)
                    {
                        api_Response.statusCode = Convert.ToInt32(DT.Rows[0]["StatusCode"]);
                        api_Response.responseCode = 0;
                        api_Response.message = DT.Rows[0]["Message"].ToString();


                    }
                }
                else
                {
                    api_Response.statusCode = 0;
                    api_Response.responseCode = 0;
                    api_Response.message = "No Changes";
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


        [HttpGet]
        [Route("GetRoleMasterInformation")]
        public Api_CommonResponse GetRoleMasterInformation(int Id = 0, string? PartyId = null)
        {
            //List<RoleMastertable> rolelist = new List<RoleMastertable>();
            try
            {

                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@Id", Id);
                param[1] = new SqlParameter("@PartyId", PartyId);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_GetUserwiseRole_Select]", param);
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
        [Route("InsertRoleMasterCreate")]
        public Api_CommonResponse InsertRoleMasterCreate(RoleMastertable obj)
        {
            try
            {

                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@RoleId", obj.iPk_RolId);
                param[1] = new SqlParameter("@RoleName", obj.sRolName);
                param[2] = new SqlParameter("@Status", obj.iStatus);
                param[3] = new SqlParameter("@PartyId", obj.sCrtdByPrtyCode);

                DataTable DT = DBOperation.FillDataTable("[dbo].[USP_ADMIN_NewRole_SaveUpdate]", param);
                if (DT != null)
                {
                    if (DT.Rows.Count > 0)
                    {
                        api_Response.statusCode = Convert.ToInt32(DT.Rows[0]["StatusCode"]);
                        api_Response.responseCode = 0;
                        api_Response.message = DT.Rows[0]["Message"].ToString();

                    }
                }
                else
                {
                    api_Response.statusCode = 0;
                    api_Response.responseCode = 0;
                    api_Response.message = "No Changes";
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
        [Route("AddGroup")]
        public Api_CommonResponse AddGroup(AddGroup group)
        {

            try
            {

                SqlParameter[] param = new SqlParameter[9];
                param[0] = new SqlParameter("@GroupId", group.GroupID);
                param[1] = new SqlParameter("@GroupName", group.GroupName);

                param[2] = new SqlParameter("@RoleId", group.iRoleId);
                param[3] = new SqlParameter("@RoleName", group.sRoleName);
                param[4] = new SqlParameter("@MenuID", group.MenuID);
                param[5] = new SqlParameter("@SubmenuId", group.SubmenuId);
                param[6] = new SqlParameter("@PartyId", group.PartyId);
                param[7] = new SqlParameter("@CreatedBy", group.PartyId);
                param[8] = new SqlParameter("@type", group.Type);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_CreateNewGroup_SaveUpdate]", param);

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

        [HttpGet]
        [Route("GetRoleMappingDepartmentandGroup")]
        public Api_CommonResponse GetRoleMappingDepartmentandGroup(string? Id = null)
        {
            DataSet ds = new DataSet();
            try
            {

                List<MappingRoleWithDepartmentandGroup> rolelist = new List<MappingRoleWithDepartmentandGroup>();
                SqlParameter[] param = null;

                if (!string.IsNullOrEmpty(Id))
                {
                    param = new SqlParameter[1];
                    param[0] = new SqlParameter("@Id", Id);
                    ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_GetDepartmentGroupRolesMapping_Select]", param);
                }
                else
                {
                    ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_GetDepartmentGroupRolesMapping_Select]");
                }
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

        [HttpGet]
        [Route("FillDepartmentandGroupMaster")]
        public Api_CommonResponse FillDepartmentandGroupaMaster(string Type, string? PartyId = null)
        {
            try
            {

                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@type", Type);
                param[1] = new SqlParameter("@PartyId", PartyId);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_GetMaster_Depart_Role_Group_View]", param);
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
        [Route("InsertMappingRoleWithDepartmentandGroup")]
        public Api_CommonResponse InsertMappingRoleWithDepartmentandGroup(MappingRoleWithDepartmentandGroup obj)
        {
            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@Id", obj.iPK_RoleDeptId);
                param[1] = new SqlParameter("@DepartmentId", obj.iDeptId);
                param[2] = new SqlParameter("@GroupId", obj.iGrpId);
                param[3] = new SqlParameter("@RoleId", obj.iRoleId);
                param[4] = new SqlParameter("@partyId", obj.PartyId);
                param[5] = new SqlParameter("@Status", obj.iStatus);
                DataTable DT = DBOperation.FillDataTable("[dbo].[USP_ADMIN_MapRoleDepartmentGroup_SaveUpdate]", param);
                if (DT != null)
                {
                    if (DT.Rows.Count > 0)
                    {
                        api_Response.statusCode = Convert.ToInt32(DT.Rows[0]["StatusCode"]);
                        api_Response.message = DT.Rows[0]["Message"].ToString();
                    }
                }
                else
                {
                    api_Response.statusCode = 0;
                    api_Response.message = "Failed";
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

        [HttpGet]
        [Route("GetUserPagingPermission")]
        public Api_CommonResponse GetUserPagingPermission(int MappingId, int GroupId)
        {
            try
            {

                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@MappingId", MappingId);
                param[1] = new SqlParameter("@GroupId", GroupId);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_PageAccessRightsUserwise_View]", param);
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
        [Route("OperationStatus")]
        public Api_CommonResponse OperationStatus(Permissionclass obj)
        {
            try
            {

                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@Type", obj.Type);
                param[1] = new SqlParameter("@MappingId", obj.MappingId);
                param[2] = new SqlParameter("@PermissionId", obj.PermissionId);
                param[3] = new SqlParameter("@MstGroupID", obj.MstGroupId);
                param[4] = new SqlParameter("@status", obj.status);
                param[5] = new SqlParameter("@PartyId", obj.PartyId);
                param[6] = new SqlParameter("@Fk_BranchId", obj.Fk_BranchId);
                param[7] = new SqlParameter("@Fk_LocationId", obj.Fk_LocationId);

                DataTable DT = DBOperation.FillDataTable("[dbo].[USP_ADMIN_UserwiseRoleAccessRights_Save]", param);
                if (DT != null)
                {
                    if (DT.Rows.Count > 0)
                    {
                        api_Response.statusCode = Convert.ToInt32(DT.Rows[0]["StatusCode"]);
                        api_Response.message = DT.Rows[0]["Message"].ToString();

                    }
                }
                else
                {
                    api_Response.statusCode = 0;
                    api_Response.message = "Failed";
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
        [Route("OperationStatusUserRights")]
        public Api_CommonResponse OperationStatusUserRights(Permissionclass obj)
        {
            try
            {

                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@Type", obj.Type);
                param[1] = new SqlParameter("@MappingId", obj.MappingId);
                param[2] = new SqlParameter("@PermissionId", obj.PermissionId);
                param[3] = new SqlParameter("@MstGroupID", obj.MstGroupId);
                param[4] = new SqlParameter("@status", obj.status);
                param[5] = new SqlParameter("@PartyId", obj.PartyId);
                param[6] = new SqlParameter("@Fk_BranchId", obj.Fk_BranchId);
                param[7] = new SqlParameter("@Fk_LocationId", obj.Fk_LocationId);

                DataTable DT = DBOperation.FillDataTable("[dbo].[USP_ADMIN_UserCustomAccessRights_Save]", param);
                if (DT != null)
                {
                    if (DT.Rows.Count > 0)
                    {
                        api_Response.statusCode = Convert.ToInt32(DT.Rows[0]["StatusCode"]);
                        api_Response.message = DT.Rows[0]["Message"].ToString();

                    }
                }
                else
                {
                    api_Response.statusCode = 0;
                    api_Response.message = "Failed";
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
        [Route("OperationDropdownUserRights")]
        public Api_CommonResponse OperationDropdownUserRights(Permissionclass obj)
        {
            try
            {

                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@Type", obj.Type);
                param[1] = new SqlParameter("@MappingId", obj.MappingId);
                param[2] = new SqlParameter("@PermissionId", obj.PermissionId);
                param[3] = new SqlParameter("@MstGroupID", obj.MstGroupId);
                param[4] = new SqlParameter("@status", obj.status);
                param[5] = new SqlParameter("@PartyId", obj.PartyId);
                param[6] = new SqlParameter("@Fk_BranchId", obj.Fk_BranchId);
                param[7] = new SqlParameter("@Fk_LocationId", obj.Fk_LocationId);

                DataTable DT = DBOperation.FillDataTable("[dbo].[USP_ADMIN_UserDropDownAccessRights_Save]", param);
                if (DT != null)
                {
                    if (DT.Rows.Count > 0)
                    {
                        api_Response.statusCode = Convert.ToInt32(DT.Rows[0]["StatusCode"]);
                        api_Response.message = DT.Rows[0]["Message"].ToString();

                    }
                }
                else
                {
                    api_Response.statusCode = 0;
                    api_Response.message = "Failed";
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
        [Route("OperationStatusAll")]
        public Api_CommonResponse OperationStatusAll(Permissionclass obj)
        {
            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@Type", obj.Type);
                param[1] = new SqlParameter("@MappingId", obj.MappingId);
                param[2] = new SqlParameter("@PermissionId", obj.PermissionId);
                param[3] = new SqlParameter("@MstGroupID", obj.MstGroupId);
                param[4] = new SqlParameter("@status", obj.status);
                param[5] = new SqlParameter("@PartyId", obj.PartyId);

                DataTable DT = DBOperation.FillDataTable("[dbo].[USP_ADMIN_UserwiseRoleAccessRightsAll_Save]", param);
                if (DT != null)
                {
                    if (DT.Rows.Count > 0)
                    {
                        api_Response.statusCode = Convert.ToInt32(DT.Rows[0]["StatusCode"]);
                        api_Response.message = DT.Rows[0]["Message"].ToString();

                    }
                }
                else
                {
                    api_Response.statusCode = 0;
                    api_Response.message = "Failed";
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


        // //17/10/2023 abhishek sir
        [HttpGet]
        [Route("GetEditMenuMasterList")]
        public Api_CommonResponse GetEditMenuMasterList(int Id)
        {
            try
            {

                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Id", Id);


                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_GetEditMenuMasterListById]", param);
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
        //18/10/2023 abhishek sir
        [HttpGet]
        [Route("GetSubMenuMaster")]

        public Api_CommonResponse GetSubMenuMaster(int Id)
        {
            try
            {

                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Id", Id);


                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_GetEditSubMenuMasterListById]", param);
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
        //20/10/2023
        [HttpGet]
        [Route("GetSubMenu")]
        public Api_CommonResponse GetSubMenu(int MenuId, string type = "Alldata")
        {


            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@MenuId", MenuId);
                param[1] = new SqlParameter("@type", type);
                DataSet ds = DBOperation.FillDataSet("[dbo].[Usp_Admin_SubMenu_By_MenuId_Get]", param);

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

        //23/10/2023
        [HttpPost]
        [Route("InsertSecondSubMenuMaster")]
        public Api_CommonResponse InsertSecondSubMenuMaster(SecSubMenu obj)
        {
            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@iId", obj.iId);
                param[1] = new SqlParameter("@iFK_Mnuid", obj.iFK_Mnuid);
                param[2] = new SqlParameter("@iFK_SubMnuId", obj.iFK_SubMnuId);
                param[3] = new SqlParameter("@sSecSubMenu", obj.sSecSubMenu);
                param[4] = new SqlParameter("@iStatus", obj.iStatus);
                param[5] = new SqlParameter("@sCtrNme", obj.sCtrNme);
                param[6] = new SqlParameter("@sActNme", obj.sActNme);
                DataTable DT = DBOperation.FillDataTable("[dbo].[USP_SecSubMenu_SaveUpdate]", param);
                if (DT != null)
                {
                    if (DT.Rows.Count > 0)
                    {
                        api_Response.statusCode = Convert.ToInt32(DT.Rows[0]["StatusCode"]);
                        api_Response.message = DT.Rows[0]["Message"].ToString();
                    }
                }
                else
                {
                    api_Response.statusCode = 0;
                    api_Response.message = "Failed";
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

        //24/10/2023
        [HttpGet]
        [Route("GetSecSubMenuMasterList")]
        public Api_CommonResponse GetSecSubMenuMasterList(int Id)
        {


            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Id", Id);
                DataSet ds = DBOperation.FillDataSet("[dbo].[Usp_Admin_SecSubMenu_List_Or_By_Id]", param);

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
        [Route("GetSecSubMenu")]
        public Api_CommonResponse GetSecSubMenu(int SubMenuId, int MenuId)
        {


            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@SubMenuId", SubMenuId);
                param[1] = new SqlParameter("@MenuId", MenuId);
                DataSet ds = DBOperation.FillDataSet("[dbo].[Usp_Admin_SecSubMenu_By_SubMenuId_Get]", param);

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
        [Route("InsertDeptRoleMenuMapping")]
        public Api_CommonResponse InsertDeptRoleMenuMapping(DeptRoleMnuMapping obj)
        {
            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@iId", obj.iPk_DeptRoleMnuMappingId);
                param[1] = new SqlParameter("@iMnuId", obj.iMnuId);
                param[2] = new SqlParameter("@iSubMnuId", obj.iSubMnuId);
                param[3] = new SqlParameter("@iDeptId", obj.iDeptId);
                param[4] = new SqlParameter("@iRoleId", obj.iRoleId);
                param[5] = new SqlParameter("@iSecSubMnuId", obj.iSecSubMnuId);

                DataTable DT = DBOperation.FillDataTable("[dbo].[USP_DeptRoleMnuMapping_SaveUpdate]", param);
                if (DT != null)
                {
                    if (DT.Rows.Count > 0)
                    {
                        api_Response.statusCode = Convert.ToInt32(DT.Rows[0]["StatusCode"]);
                        api_Response.message = DT.Rows[0]["Message"].ToString();
                    }
                }
                else
                {
                    api_Response.statusCode = 0;
                    api_Response.message = "Failed";
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

        [HttpGet]
        [Route("GetDeptRoleMenuMapping")]
        public Api_CommonResponse GetDeptRoleMenuMapping(int Deptid, int Roleid)
        {


            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@Deptid", Deptid);
                param[1] = new SqlParameter("@Roleid", Roleid);
                DataSet ds = DBOperation.FillDataSet("[dbo].[Usp_Admin_DeptRoleMenuMapping_Get]", param);

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
        [Route("InsertBranchLocationMapp")]
        public Api_CommonResponse InsertBranchLocationMapp(BranchLocMapping obj)
        {
            try
            {

                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@Id", obj.Id);
                param[1] = new SqlParameter("@BranchId", obj.BranchId);
                param[2] = new SqlParameter("@LocationId", obj.LocationId);


                DataTable DT = DBOperation.FillDataTable("[dbo].[USP_BranchLocMapping_SaveUpdate]", param);
                if (DT != null)
                {
                    if (DT.Rows.Count > 0)
                    {
                        api_Response.statusCode = Convert.ToInt32(DT.Rows[0]["StatusCode"]);
                        api_Response.message = DT.Rows[0]["Message"].ToString();
                    }
                }
                else
                {
                    api_Response.statusCode = 0;
                    api_Response.message = "Failed";
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

        [HttpGet]
        [Route("GetBranchLocMappingList")]
        public Api_CommonResponse GetBranchLocMappingList()
        {


            try
            {

                DataSet ds = DBOperation.FillDataSet("[dbo].[Usp_Admin_GetBranchLocMapping_Get]");

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
        [Route("GetDropdown")]
        public Api_CommonResponse GetDropdown(int Id = 0, string? Table = null)
        {
            //List<RoleMastertable> rolelist = new List<RoleMastertable>();
            try
            {

                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@SelectId", Id);
                param[1] = new SqlParameter("@Table", Table);

                DataSet ds = DBOperation.FillDataSet("[dbo].[Usp_GetDropDown]", param);
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

        [HttpGet]
        [Route("GetPermissionList")]
        public Api_CommonResponse GetPermissionList(int DepartmentId = 0, int RoleId = 0, int BranchId = 0, int LocationId = 0)
        {
            //List<RoleMastertable> rolelist = new List<RoleMastertable>();
            try
            {

                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@DeptId", DepartmentId);
                param[1] = new SqlParameter("@RoleId", RoleId);
                param[2] = new SqlParameter("@BranchId", BranchId);
                param[3] = new SqlParameter("@LocationId", LocationId);

                DataSet ds = DBOperation.FillDataSet("[dbo].[Usp_OperationMapping]", param);
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
        [Route("OperationStatusNew")]
        public Api_CommonResponse OperationStatusNew(ChangesSetting obj)
        {
            try
            {

                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@Type", obj.Typedata);
                param[1] = new SqlParameter("@MapId", obj.MapId);
                param[2] = new SqlParameter("@status", obj.status);
                param[3] = new SqlParameter("@BranchId", obj.BranchId);
                param[4] = new SqlParameter("@Id", obj.Id);
                param[5] = new SqlParameter("@LocationId", obj.LocationId);
                param[6] = new SqlParameter("@deptid", obj.deptid);
                param[7] = new SqlParameter("@RoleId", obj.RoleId);

                DataTable DT = DBOperation.FillDataTable("[dbo].[USP_ADMIN_UserwiseRoleAccessRights_Save_New]", param);
                if (DT != null)
                {
                    if (DT.Rows.Count > 0)
                    {
                        api_Response.statusCode = Convert.ToInt32(DT.Rows[0]["StatusCode"]);
                        api_Response.message = DT.Rows[0]["Message"].ToString();

                    }
                }
                else
                {
                    api_Response.statusCode = 0;
                    api_Response.message = "Failed";
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
        [Route("ChangeStatusUserlist")]
        public Api_CommonResponse ChangeStatusUserlist(Activeclass obj)
        {
            try
            {

                DataTable DT = new DataTable();
                SqlParameter[] param = null;

                param = new SqlParameter[3];
                param[0] = new SqlParameter("@tablename", obj.Tablename);
                param[1] = new SqlParameter("@Id", obj.Id);
                param[2] = new SqlParameter("@type", obj.status);

                DT = DBOperation.FillDataTable("[dbo].[USP_ActivateDeactivate_CustomList]", param);


                if (DT != null)
                {
                    if (DT.Rows.Count > 0)
                    {
                        api_Response.statusCode = Convert.ToInt32(DT.Rows[0]["StatusCode"]);
                        api_Response.responseCode = 0;
                        api_Response.message = DT.Rows[0]["Message"].ToString();


                    }
                }
                else
                {
                    api_Response.statusCode = 0;
                    api_Response.responseCode = 0;
                    api_Response.message = "No Changes";
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

    }


}
