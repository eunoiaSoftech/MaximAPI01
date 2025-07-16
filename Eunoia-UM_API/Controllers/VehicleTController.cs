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
    public class VehicleTController : Controller
    {
        Api_CommonResponse res = new Api_CommonResponse();

        [HttpPost]
        [Route("VehicleTypeSave")]
        public Api_CommonResponse VehicleTypeSave(VehicleType vehicletype)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[11];
                param[0] = new SqlParameter("@sVehTypeName", vehicletype.sVehTypeName);
                param[1] = new SqlParameter("@iFk_VehMake", vehicletype.iFk_VehMake);
                param[2] = new SqlParameter("@iFk_NoOfAxels", vehicletype.iFk_NoOfAxels);
                param[3] = new SqlParameter("@iFk_UOM", vehicletype.iFk_UOM);
                param[4] = new SqlParameter("@sShortName", vehicletype.sShortName);
                param[5] = new SqlParameter("@iFk_VehModel", vehicletype.iFk_VehModel);
                param[6] = new SqlParameter("@iTypeOfBody", vehicletype.iTypeOfBody);
                param[7] = new SqlParameter("@dTrailerLen", vehicletype.dTrailerLen);
                param[8] = new SqlParameter("@iFk_Classification", vehicletype.iFk_MkrClssn);
                param[9] = new SqlParameter("@iFk_BranchID", vehicletype.iFk_BranchID);
                param[10] = new SqlParameter("@iFk_YearID", vehicletype.iFk_YearID);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_MasterVEHTYP_Save]", param);
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
            catch (Exception e)
            {
                throw e;
            }
            return res;
        }

        [HttpGet]
        [Route("GetVehicleTypeList")]
        public Api_CommonResponse GetVehicleTypeList()
        {
            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_GetVehicleTypeList_Get]");
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    var data = ds.Tables[0];

                    res.data = JsonConvert.SerializeObject(data);
                    res.statusCode = 200;
                }
            }
            catch (Exception ex)
            {
                res.statusCode = 402;
            }
            return res;
        }


        [HttpGet]
        [Route("GetVehicleForEdit")]
        public Api_CommonResponse GetVehicleForEdit(int id)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@id", id);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_MasterVEHTYP_GetForEdit]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    res.message = "Advance details list";
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
                throw e;
            }
            return res;
        }


        [HttpPost]
        [Route("VehicleTypeUpdate")]
        public Api_CommonResponse VehicleTypeUpdate(VehicleType vehicletype)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[10];
                param[0] = new SqlParameter("@iPk_VehTypeId", vehicletype.iPk_VehTypeId);
                param[1] = new SqlParameter("@sVehTypeName", vehicletype.sVehTypeName);
                param[2] = new SqlParameter("@iFk_VehMake", vehicletype.iFk_VehMake);
                param[3] = new SqlParameter("@iFk_NoOfAxels", vehicletype.iFk_NoOfAxels);
                param[4] = new SqlParameter("@iFk_UOM", vehicletype.iFk_UOM);
                param[5] = new SqlParameter("@sShortName", vehicletype.sShortName);
                param[6] = new SqlParameter("@iFk_VehModel", vehicletype.iFk_VehModel);
                param[7] = new SqlParameter("@iTypeOfBody", vehicletype.iTypeOfBody);
                param[8] = new SqlParameter("@dTrailerLen", vehicletype.dTrailerLen);
                param[9] = new SqlParameter("@iFk_Classification", vehicletype.iFk_MkrClssn);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_VEHTYP_Update]", param);
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
            catch (Exception e)
            {
                throw e;
            }
            return res;
        }


    }
}
