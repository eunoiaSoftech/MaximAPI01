using Eunoia_UM_API.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using Eunoia_UM_API.Model;
using System.Net;

namespace Eunoia_UM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        Api_CommonResponse api_Response = new Api_CommonResponse();

        [HttpGet]
        [Route("ActiveInactiveCard")]
        public Api_CommonResponse ActiveInactiveCard(int status, string cardId)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@IsActive", status);
                param[1] = new SqlParameter("@iPk_CardId", cardId);
                param[2] = new SqlParameter("@Flag", "6");

                DataSet ds = DBOperation.FillDataSet("[dbo].[CardMstAction]", param);
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

        [HttpPost]
        [Route("DeleteCard")]
        public Api_CommonResponse DeleteCard(int Id)
        {
            int Master = 0;
            try
            {

                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@Flag", "4");
                param[1] = new SqlParameter("@iPk_CardId", Id);
                DataTable DT = DBOperation.FillDataTable("[dbo].[CardMstAction]", param);
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
                api_Response.message = "Delete Card";
                api_Response.status = 400;
                api_Response.userID = "0";
                api_Response.responseCode = 0;
            }
            return api_Response;
        }

        [HttpGet]
        [Route("GetCardMaster")]
        public Api_CommonResponse GetCardMaster()
        {

            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Flag", "5");
                DataTable ds = DBOperation.FillDataTable("[dbo].[CardMstAction]", param);
                if (ds != null && ds != null && ds.Rows.Count > 0)
                {
                    api_Response.message = "Company List";
                    api_Response.statusCode = 1;
                    api_Response.data = JsonConvert.SerializeObject(ds);
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
        [Route("GetCardRecharge")]
        public Api_CommonResponse GetCardRecharge()
        {

            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Flag", "2");
                DataTable ds = DBOperation.FillDataTable("[dbo].[CardRechargeAction]", param);
                if (ds != null && ds != null && ds.Rows.Count > 0)
                {
                    api_Response.message = "Company List";
                    api_Response.statusCode = 1;
                    api_Response.data = JsonConvert.SerializeObject(ds);
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
        [Route("AddCard")]
        public Api_CommonResponse AddCard(CardMst card)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@sCardName", card.sCardName);
                param[1] = new SqlParameter("@dCardBalance", card.dCardBalance);
                param[2] = new SqlParameter("@dtCrtDate", card.dtCrtDate);
                param[3] = new SqlParameter("@IsActive", card.IsActive);
                param[4] = new SqlParameter("@iFk_Createdby", card.iFk_Createdby);
                param[5] = new SqlParameter("@iFk_ApprovedBy", card.iFk_ApprovedBy);

                DataSet ds = DBOperation.FillDataSet("[dbo].[CardMstAction]", param);
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
        [HttpPost]
        [Route("SaveCardRecharge")]
        public Api_CommonResponse SaveCardRecharge(List<CardRecharge> card)
        {
            try
            {
                DataSet ds = new DataSet();
                foreach(var rc in card)
                {
                    SqlParameter[] param = new SqlParameter[17];
                    param[0] = new SqlParameter("@iFk_CardType", rc.iFk_CardType   );
                    param[1] = new SqlParameter("@iFk_VehicleId", rc.iFk_VehicleId  );
                    param[2] = new SqlParameter("@iRechMode",       rc.iRechMode);
                    param[3] = new SqlParameter("@sCardNumber",     rc.sCardNumber);
                    param[4] = new SqlParameter("@dAmount",         rc.dAmount);
                    param[5] = new SqlParameter("@dtRechDate",      rc.dtRechDate);
                    param[6] = new SqlParameter("@iFk_CreatedBy",   rc.iFk_CreatedBy);
                    param[7] = new SqlParameter("@iFk_CheckedBy",   rc.iFk_CheckedBy);
                    param[8] = new SqlParameter("@iFk_ApprovedBy",  rc.iFk_ApprovedBy);
                    param[9] = new SqlParameter("@iFk_YearId",      rc.iFk_YearId);
                    param[10] = new SqlParameter("@iFk_BranchId",    rc.iFk_BranchId);
                    param[11] = new SqlParameter("@sIpAddress",      rc.sIpAddress);
                    param[12] = new SqlParameter("@sLongitude",      rc.sLongitude);
                    param[13] = new SqlParameter("@sLatitude",       rc.sLatitude);
                    param[14] = new SqlParameter("@sBrowser",        rc.sBrowser);
                    param[15] = new SqlParameter("@sDevice",         rc.sDevice);
                    param[16] = new SqlParameter("@iIsDeleted",      rc.iIsDeleted);

                    ds = DBOperation.FillDataSet("[dbo].[CardRechargeAction]", param);
                }
                
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
        [Route("GetFillDropDowns")]
        [HttpGet]
        public Api_CommonResponse GetFillDropDowns()
        {
            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_CustomFields_View]");
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
        [Route("GetDropDown")]
        [HttpGet]
        public Api_CommonResponse GetDropDown(string type)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@type", type);
                DataSet ds = DBOperation.FillDataSet("[dbo].[GetddlforCardRC]", param);
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
    }
}
