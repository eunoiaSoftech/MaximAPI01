using Eunoia_UM_API.Helper;
using Eunoia_UM_API.Model;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;

namespace Eunoia_UM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommodityController : ControllerBase
    {
        Api_CommonResponse res = new Api_CommonResponse();
        [HttpPost]
        [Route("CommoditySave")]
        public Api_CommonResponse CommoditySave(commoditymodel CommdityMaster)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@pk_id", CommdityMaster.pk_Id);
                param[1] = new SqlParameter("@sName", CommdityMaster.sName);
                param[2] = new SqlParameter("@sType", CommdityMaster.sType);
                param[3] = new SqlParameter("@Fk_BranchId", CommdityMaster.iFk_BranchId);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_MASTER_Commodity_Save]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    res.responseCode = 0;
                    res.data = ds.Tables[0];
                    res.message = ds.Tables[0].Rows[0]["Message"].ToString();
                    res.statusCode = Convert.ToInt32(ds.Tables[0].Rows[0]["StatusCode"]);
                }
                else
                {
                    res.responseCode = 1;
                    res.message = $"We are facing server issue at the moment please try again...";
                    res.statusCode = -1;
                    res.data = null;
                }
            }
            catch (Exception ex)
            {
                res.statusCode = 402;
            }
            return res;
        }
        [HttpGet]
        [Route("GetCommodityList")]
        public Api_CommonResponse GetCommodityList()
        {
            Api_CommonResponse response = new Api_CommonResponse();
            try
            {
                SqlParameter[] param = new SqlParameter[0];
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_MASTER_Commodity_GetList]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    var data = ds.Tables[0];
                    var data1 = data.Rows.OfType<DataRow>()
                               .Select(row => data.Columns.OfType<DataColumn>()
                                .ToDictionary(col => col.ColumnName, c => row[c]));
                    response.data = JsonConvert.SerializeObject(data1);
                    response.statusCode = 200;
                }
            }
            catch (Exception ex)
            {
                response.statusCode = 402;
            }
            return response;
        }

        [HttpPost]
        [Route("CommodityUpdate")]
        public Api_CommonResponse CommodityUpdate(commoditymodel CommodityMaster)
        {
            Api_CommonResponse response = new Api_CommonResponse();
            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@pk_Id", CommodityMaster.pk_Id);
                param[1] = new SqlParameter("@sName", CommodityMaster.sName);
                param[2] = new SqlParameter("@sType", CommodityMaster.sType);
                DataSet ds = DBOperation.FillDataSet("USP_MASTER_Commodity_Update", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    var data = ds.Tables[0];
                    var data1 = data.Rows.OfType<DataRow>()
                               .Select(row => data.Columns.OfType<DataColumn>()
                                .ToDictionary(col => col.ColumnName, c => row[c]));
                    response.data = JsonConvert.SerializeObject(data1);
                    response.statusCode = 200;
                }
            }
            catch (Exception ex)
            {
                response.statusCode = 402;
            }
            return response;
        }
    }
}