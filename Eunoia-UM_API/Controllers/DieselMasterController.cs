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
    public class DieselMasterController : Controller
    {
        Api_CommonResponse res = new Api_CommonResponse();
        [HttpPost]
        [Route("DieselMasterAdd")]
        public Api_CommonResponse DieselMasterAdd(DieselMaster MDieselMaster)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[10];
                param[0] = new SqlParameter("@iFk_PumpId", MDieselMaster.iFk_PumpId);
                param[1] = new SqlParameter("@dDiscRate", MDieselMaster.dDiscRate);
                param[2] = new SqlParameter("@dRate", MDieselMaster.dRate);
                param[3] = new SqlParameter("@bDiscount", MDieselMaster.bDiscount);
                param[4] = new SqlParameter("@sDiscountType", MDieselMaster.sDiscountType);
                param[5] = new SqlParameter("@dCalculatedValue", MDieselMaster.dCalculatedValue);
                param[6] = new SqlParameter("@dtEffectiveDate", MDieselMaster.dtEffectiveDate);
                param[7] = new SqlParameter("@iFk_CityId", MDieselMaster.iFk_CityId);
                param[8] = new SqlParameter("@bIsActive", MDieselMaster.bIsActive);
                param[9] = new SqlParameter("@iFk_FuelType", MDieselMaster.iFk_FuelType);
                DataSet ds = DBOperation.FillDataSet("USP_MASTER_DieselRate_Save", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    res.responseCode = 0;
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
        [Route("GetPumpsList")]
        public Api_CommonResponse GetPumpsList()
        {
            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_MASTER_PARTYMST_Get]");
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    res.message = "Pumps List";
                    res.statusCode = 1;
                    res.data = JsonConvert.SerializeObject(ds.Tables[0]);
                    res.responseCode = 1;
                }
                else
                {
                    res.message = "No Data Available";
                    res.statusCode = 1;
                    res.responseCode = 1;
                }
            }
            catch (Exception e)
            {
            }
            return res;
        }

        [HttpGet]
        [Route("GetDieselList")]
        public Api_CommonResponse GetDieselList()
        {
            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_MASTER_DieselRate_GetList]");
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    res.message = "Diesel List";
                    res.statusCode = 1;
                    res.data = JsonConvert.SerializeObject(ds.Tables[0]);
                    res.responseCode = 1;
                }
                else
                {
                    res.message = "No Data Available";
                    res.statusCode = 1;
                    res.responseCode = 1;
                }
            }
            catch (Exception e)
            {
            }
            return res;
        }

        [HttpPost]
        [Route("DieselMasterUpdate")]
        public Api_CommonResponse DieselMasterUpdate(DieselMaster MDieselMaster)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[11];
                param[0] = new SqlParameter("@iPK_Did", MDieselMaster.iPk_Did);
                param[1] = new SqlParameter("@iFk_PumpId", MDieselMaster.iFk_PumpId);
                param[2] = new SqlParameter("@dDiscRate", MDieselMaster.dDiscRate);
                param[3] = new SqlParameter("@dRate", MDieselMaster.dRate);
                param[4] = new SqlParameter("@bDiscount", MDieselMaster.bDiscount);
                param[5] = new SqlParameter("@sDiscountType", MDieselMaster.sDiscountType);
                param[6] = new SqlParameter("@dCalculatedValue", MDieselMaster.dCalculatedValue);
                param[7] = new SqlParameter("@dtEffectiveDate", MDieselMaster.dtEffectiveDate);
                param[8] = new SqlParameter("@iFk_CityId", MDieselMaster.iFk_CityId);
                param[9] = new SqlParameter("@bIsActive", MDieselMaster.bIsActive);
                param[10] = new SqlParameter("@iFk_FuelType", MDieselMaster.iFk_FuelType);
                DataSet ds = DBOperation.FillDataSet("USP_MASTER_DieselRate_Update", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    res.responseCode = 0;
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

        [HttpPost]
        [Route("DieselMasterDelete")]
        public Api_CommonResponse DieselMasterDelete(DieselMaster MDieselMaster)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@iPK_Did", MDieselMaster.iPk_Did);
                DataSet ds = DBOperation.FillDataSet("USP_MASTER_DieselRate_Delete", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    res.responseCode = 0;
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

        [HttpPost]
        [Route("ActiveInActiveItem")]
        public Api_CommonResponse ActiveInActiveItem(DieselMaster MDieselMaster)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@iPK_Did", MDieselMaster.iPk_Did);
                param[1] = new SqlParameter("@bIsActive", MDieselMaster.bIsActive);
                DataSet ds = DBOperation.FillDataSet("USP_MASTER_DieselRate_ActiveInActiveItem", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    res.responseCode = 0;
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
    }
}
