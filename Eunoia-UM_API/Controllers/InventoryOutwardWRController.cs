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
    public class InventoryOutwardWRController : Controller
    {
        Api_CommonResponse api_Response = new Api_CommonResponse();


        [HttpGet]
        [Route("GetSupplierList")]
        public Api_CommonResponse GetSupplierList(string sFormName, int Fk_BranchId, int Fk_LocationId)
        {

            try
            {

                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@sFormName", sFormName);
                param[1] = new SqlParameter("@iFk_Branchid", Fk_BranchId);
                param[2] = new SqlParameter("@iFk_LocationId", Fk_LocationId);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_GetSupplierList]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Supplier List";
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
        [HttpGet]
        [Route("GetProductList")]
        public Api_CommonResponse GetProductList(string sFormName, int Fk_BranchId, int Fk_LocationId)
        {

            try
            {

                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@sFormName", sFormName);
                param[1] = new SqlParameter("@iFk_Branchid", Fk_BranchId);
                param[2] = new SqlParameter("@iFk_LocationId", Fk_LocationId);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_GetProductList]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Product List";
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
        [Route("GetBrandList")]
        public Api_CommonResponse GetBrandList(int Fk_ItemId)
        {

            try
            {

                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Fk_ItemId", Fk_ItemId);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_GetBrand_Itemwise]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Brand List";
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
        [Route("SaveData")]
        public Api_CommonResponse SaveData(InventoryOutwardWR inventory)
        {
            try
            {
                var branchID = 0;

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@iFk_SupplierId", inventory.iFk_SupplierId);
                param[1] = new SqlParameter("@sAssetNo", inventory.sAssetNo);
                param[2] = new SqlParameter("@dtIssueDate", inventory.dtIssueDate);
                param[3] = new SqlParameter("@iFk_LocationId", inventory.iFk_LocationId);
                param[4] = new SqlParameter("@iFk_ItemName", inventory.iFk_ItemName);
                param[5] = new SqlParameter("@iFk_ItemBrandId", inventory.iFk_ItemBrandId);
                param[6] = new SqlParameter("@dItemQty", inventory.dItemQty);
                 
                DataSet ds = DBOperation.FillDataSet("[dbo].[Usp_Store_InventoryOutward_Save]", param);
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

    }
}
