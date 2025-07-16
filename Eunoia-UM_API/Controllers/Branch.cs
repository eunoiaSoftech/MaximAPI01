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
    public class Branch : Controller
    {
        Api_CommonResponse api_Response = new Api_CommonResponse();


        [HttpGet]
        [Route("GetCompanyList")]
        public Api_CommonResponse GetCompanyList()
        {

            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_GetCompanyList]");
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
        [Route("GetBranchList")]
        public Api_CommonResponse GetBranchList()
        {

            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_GetBranchList]");
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Branch List";
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
        [Route("GetConsigneeMaster")]
        public Api_CommonResponse GetConsigneeMaster()
        {

            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_GetConsigneeList]");
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Branch List";
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
        [Route("GetBranchMasterUserwise")]
        public Api_CommonResponse GetBranchMasterUserwise(int UserId)
        {

            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Fk_UserId", UserId);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_GetBranchMasterUserwise]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Branch List";
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
        [Route("AddBranch")]
        public Api_CommonResponse AddBranch(BranchMaster Branch)
        {
            try
            {
                var branchID = 0;

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@BranchName", Branch.BranchName);
                param[1] = new SqlParameter("@sBranchLogo", Branch.Logo);
                param[2] = new SqlParameter("@sPanCdNum", Branch.PanCardNumber);
                param[3] = new SqlParameter("@sEmail", Branch.EmailURL);
                param[4] = new SqlParameter("@CmpnyMstId", Branch.CmpnyMstId);
                param[5] = new SqlParameter("@RegisteredOn", Branch.RegisteredOn);

                DataSet ds = DBOperation.FillDataSet("[dbo].[Usp_Admin_Branch_Save]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    branchID = Convert.ToInt32(ds.Tables[0].Rows[0]["BranchId"]);

                    if (Branch.ContactDtls != null && Branch.ContactDtls.Count > 0)
                    {
                        foreach (var item in Branch.ContactDtls)
                        {
                            AddBranchContact(item, branchID, "Insert");
                        }
                        AddBranchAddressDetails(Branch.AddressDtls, branchID, "I");

                    }

                    if (Branch.LocationDtls != null)
                    {
                        foreach (var item in Branch.LocationDtls)
                        {
                            AddBranchLocation(item, branchID, "Insert");
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
        [Route("AddBranchContact")]
        public string AddBranchContact(BranchContactDtls member, int branchId, string type)
        {
            var status = "";
            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@iCmpnyMstId", branchId);
                param[1] = new SqlParameter("@sCntctDetlspersonNme", member.CntctPerson);
                param[2] = new SqlParameter("@sCntctNum", member.CntctNumber);
                param[3] = new SqlParameter("@sCntcEmail", member.EmailId);
                param[4] = new SqlParameter("@bPrimary", member.Primary);
                param[5] = new SqlParameter("@Flag", type);

                DataSet ds = DBOperation.FillDataSet("[dbo].[Usp_Admin_BranchContactDtls_Save]", param);
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
        [Route("AddBranchAddressDetails")]
        public string AddBranchAddressDetails(BranchAddressDtls Address, int branchId, string type)
        {
            var status = "";
            try
            {

                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@iBranchMstId", branchId);
                param[1] = new SqlParameter("@sAddressCntry", Address.Country);
                param[2] = new SqlParameter("@sAddressState", Address.State);
                param[3] = new SqlParameter("@sAddressCity", Address.City);
                param[4] = new SqlParameter("@sPincd", Address.Pincode);
                param[5] = new SqlParameter("@sAddress", Address.Address);
                param[6] = new SqlParameter("@sGSTNO", Address.GSTNO);
                param[7] = new SqlParameter("@Flag", type);

                DataSet ds = DBOperation.FillDataSet("[dbo].[Usp_Admin_BranchAddressdtls_Save]", param);
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
        [Route("AddBranchLocation")]
        public string AddBranchLocation(BranchLocationDtls Location, int branchId, string type)
        {
            var status = "";
            try
            {

                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@iBranchMstId", branchId);
                param[1] = new SqlParameter("@iFk_LocId", Location.LocationId);

                DataSet ds = DBOperation.FillDataSet("[dbo].[Usp_Admin_BranchLocdtls_Save]", param);
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
        [Route("GetBranchDetailsForEdit")]
        public Api_CommonResponse GetBranchDetailsForEdit(int branchID)
        {

            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@branchID", branchID);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_GetBranchDetailsForEdit_get]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {

                    BranchMaster branch = new BranchMaster();
                    List<BranchContactDtls> cntct = new List<BranchContactDtls>();
                    BranchAddressDtls address = new BranchAddressDtls();
                    branch.iBrnchmMstId = Convert.ToInt32(ds.Tables[0].Rows[0]["iBrnchmMstId"]);
                    branch.CmpnyMstId = Convert.ToInt32(ds.Tables[0].Rows[0]["iCmpnyMstId"]);
                    branch.BranchName = ds.Tables[0].Rows[0]["sBranchNme"].ToString();
                    branch.RegisteredOn = ds.Tables[0].Rows[0]["dRegstrdOn"].ToString();
                    branch.CompanyName = ds.Tables[0].Rows[0]["sCmpnyNme"].ToString();
                    branch.Logo = ds.Tables[0].Rows[0]["sBranchLogo"].ToString();

                    branch.CINNumber = ds.Tables[0].Rows[0]["iCmpnyCINNum"].ToString();
                    branch.PanCardNumber = ds.Tables[0].Rows[0]["sPanCdNum"].ToString();
                    branch.EmailURL = ds.Tables[0].Rows[0]["sEmail"].ToString();
                    branch.WebsiteURL = ds.Tables[0].Rows[0]["sWebsiteURL"].ToString();


                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        for (int i = 0; i <= ds.Tables[1].Rows.Count - 1; i++)
                        {
                            BranchContactDtls member = new BranchContactDtls();
                            member.CntctPerson = ds.Tables[1].Rows[i]["sCntctDetlspersonNme"].ToString();
                            member.CntctNumber = ds.Tables[1].Rows[i]["sCntctNum"].ToString();
                            member.EmailId = ds.Tables[1].Rows[i]["sCntcEmail"].ToString();
                            member.Primary = Convert.ToBoolean(ds.Tables[1].Rows[i]["bPrimary"]);
                            cntct.Add(member);
                        }
                        branch.ContactDtls = cntct;
                    }
                    else
                    {
                        branch.ContactDtls = null;
                    }
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        address.AddrssDtlsId = Convert.ToInt32(ds.Tables[2].Rows[0]["iPK_AddDetId"]);
                        address.Country = ds.Tables[2].Rows[0]["iFk_CntryId"].ToString();
                        address.State = ds.Tables[2].Rows[0]["iFk_StateId"].ToString();
                        address.City = ds.Tables[2].Rows[0]["iFk_CityId"].ToString();
                        address.Pincode = ds.Tables[2].Rows[0]["sPincode"].ToString();
                        address.Address = ds.Tables[2].Rows[0]["sAddress"].ToString();
                        branch.AddressDtls = address;
                    }
                    else
                    {
                        branch.AddressDtls = null;
                    }

                    api_Response.message = "Company Details";
                    api_Response.statusCode = 1;
                    api_Response.data = branch;
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
        [Route("UpdateBranchDetails")]
        public Api_CommonResponse UpdateBranchDetails(BranchMaster Branch)
        {
            try
            
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@BranchName", Branch.BranchName);
                param[1] = new SqlParameter("@sBranchLogo", Branch.Logo);
                param[2] = new SqlParameter("@sPanCdNum", Branch.PanCardNumber);
                param[3] = new SqlParameter("@sEmail", Branch.EmailURL);
                param[4] = new SqlParameter("@CmpnyMstId", Branch.CmpnyMstId);
                param[5] = new SqlParameter("@dRegstrdOn", Branch.RegisteredOn);
                param[6] = new SqlParameter("@iBrnchmMstId", Branch.iBrnchmMstId);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Admin_BranchMst_Update]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {

                    if (Branch.ContactDtls != null && Branch.ContactDtls.Count > 0)
                    {
                        AddBranchContact(new BranchContactDtls(), Branch.iBrnchmMstId, "D");

                        foreach (var item in Branch.ContactDtls)
                        {
                            AddBranchContact(item, Branch.CmpnyMstId, "I");
                        }
                        AddBranchAddressDetails(Branch.AddressDtls, Branch.iBrnchmMstId, "U");
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
