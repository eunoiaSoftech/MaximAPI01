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
                SqlParameter[] param = new SqlParameter[34];
                param[0] = new SqlParameter("@sVehTypeName", vehicletype.sVehTypeName ?? "");
                param[1] = new SqlParameter("@iFk_VehMake", vehicletype.iFk_VehMake ?? 0);
                param[2] = new SqlParameter("@iFk_NoOfAxels", vehicletype.iFk_NoOfAxels ?? 0);
                param[3] = new SqlParameter("@iFk_UOM", vehicletype.iFk_UOM ?? 0);
                param[4] = new SqlParameter("@iShortName", vehicletype.iShortName ?? 0);
                param[5] = new SqlParameter("@iFk_VehModel", vehicletype.iFk_VehModel ?? 0);
                param[6] = new SqlParameter("@iTypeOfBody", vehicletype.iTypeOfBody ?? 0);
                param[7] = new SqlParameter("@dTrailerLen", vehicletype.dTrailerLen ?? 0);
                param[8] = new SqlParameter("@iFk_Classification", vehicletype.iFk_MkrClssn ?? 0);
                param[9] = new SqlParameter("@iFk_BranchID", vehicletype.iFk_BranchID ?? 0);
                param[10] = new SqlParameter("@iFk_YearID", vehicletype.iFk_YearID ?? 0);
                param[11] = new SqlParameter("@iFk_ClassificationId", vehicletype.iFk_ClasificationId ?? 0);
                param[12] = new SqlParameter("@sSubBodyType", vehicletype.sSubBodyType ?? "");
                param[13] = new SqlParameter("@dAvgHighway", vehicletype.dAvgHighway ?? 0);
                param[14] = new SqlParameter("@dWheelBase", vehicletype.dWheelBase ?? 0);
                param[15] = new SqlParameter("@dMaxHP", vehicletype.dMaxHP ?? 0);
                param[16] = new SqlParameter("@dCubeCapacity", vehicletype.dCubeCapacity ?? 0);  
                param[17] = new SqlParameter("@dGVW", vehicletype.dGVW ?? 0);
                param[18] = new SqlParameter("@dTareWeight", vehicletype.dTareWeight ?? 0);
                param[19] = new SqlParameter("@dLoadingCapacity", vehicletype.dLoadingCapacity ?? 0);
                param[20] = new SqlParameter("@dAvgCity", vehicletype.dAvgCity ?? 0);
                param[21] = new SqlParameter("@iSeatingCapacity", vehicletype.iSeatingCapacity ?? 0);
                param[22] = new SqlParameter("@dFrontTyre", vehicletype.dFrontTyre ?? 0);
                param[23] = new SqlParameter("@dRearTyre", vehicletype.dRearTyre ?? 0);
                param[24] = new SqlParameter("@dTotalTyre", vehicletype.dTotalTyres ?? 0);
                param[25] = new SqlParameter("@dBattery", vehicletype.dBattery ?? 0);
                param[26] = new SqlParameter("@iNoOfCylinders", vehicletype.dNoOfCylinder ?? 0);
                param[27] = new SqlParameter("@iFuelType", vehicletype.iFuelType ?? 0);
                param[28] = new SqlParameter("@dFTnk1Cap", vehicletype.dFTnk1Cap ?? 0);
                param[29] = new SqlParameter("@dFTnk2Cap", vehicletype.dFTnk2Cap ?? 0);
                param[30] = new SqlParameter("@iIsDef", vehicletype.iIsDef ?? false);
                param[31] = new SqlParameter("@dDefCap", vehicletype.dDefCap ?? 0);
                param[32] = new SqlParameter("@fuelSensor", vehicletype.fuelSensor ?? 0);
                param[33] = new SqlParameter("@sFuelSensCmpy", vehicletype.sFuelSensCmpy ?? "");

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
                    res.message = "We are facing server issue at the moment please try again...";
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
                SqlParameter[] param = new SqlParameter[35];
                param[0] = new SqlParameter("@iPk_VehTypeId", vehicletype.iPk_VehTypeId);
                param[1] = new SqlParameter("@sVehTypeName", vehicletype.sVehTypeName ?? "");
                param[2] = new SqlParameter("@iFk_VehMake", vehicletype.iFk_VehMake ?? 0);
                param[3] = new SqlParameter("@iFk_NoOfAxels", vehicletype.iFk_NoOfAxels ?? 0);
                param[4] = new SqlParameter("@iFk_UOM", vehicletype.iFk_UOM ?? 0);
                param[5] = new SqlParameter("@iShortName", vehicletype.iShortName ?? 0);
                param[6] = new SqlParameter("@iFk_VehModel", vehicletype.iFk_VehModel ?? 0);
                param[7] = new SqlParameter("@iTypeOfBody", vehicletype.iTypeOfBody ?? 0);
                param[8] = new SqlParameter("@dTrailerLen", vehicletype.dTrailerLen ?? 0);
                param[9] = new SqlParameter("@iFk_Classification", vehicletype.iFk_MkrClssn ?? 0);
                param[10] = new SqlParameter("@iFk_BranchID", vehicletype.iFk_BranchID ?? 0);
                param[11] = new SqlParameter("@iFk_YearID", vehicletype.iFk_YearID ?? 0);
                param[12] = new SqlParameter("@iFk_ClassificationId", vehicletype.iFk_ClasificationId ?? 0);
                param[13] = new SqlParameter("@sSubBodyType", vehicletype.sSubBodyType ?? "");
                param[14] = new SqlParameter("@dAvgHighway", vehicletype.dAvgHighway ?? 0);
                param[15] = new SqlParameter("@dWheelBase", vehicletype.dWheelBase ?? 0);
                param[16] = new SqlParameter("@dMaxHP", vehicletype.dMaxHP ?? 0);
                param[17] = new SqlParameter("@dCubeCapacity", vehicletype.dCubeCapacity ?? 0);
                param[18] = new SqlParameter("@dGVW", vehicletype.dGVW ?? 0);
                param[19] = new SqlParameter("@dTareWeight", vehicletype.dTareWeight ?? 0);
                param[20] = new SqlParameter("@dLoadingCapacity", vehicletype.dLoadingCapacity ?? 0);
                param[21] = new SqlParameter("@dAvgCity", vehicletype.dAvgCity ?? 0);
                param[22] = new SqlParameter("@iSeatingCapacity", vehicletype.iSeatingCapacity ?? 0);
                param[23] = new SqlParameter("@dFrontTyre", vehicletype.dFrontTyre ?? 0);
                param[24] = new SqlParameter("@dRearTyre", vehicletype.dRearTyre ?? 0);
                param[25] = new SqlParameter("@dTotalTyre", vehicletype.dTotalTyres ?? 0);
                param[26] = new SqlParameter("@dBattery", vehicletype.dBattery ?? 0);
                param[27] = new SqlParameter("@iNoOfCylinders", vehicletype.dNoOfCylinder ?? 0);
                param[28] = new SqlParameter("@iFuelType", vehicletype.iFuelType ?? 0);
                param[29] = new SqlParameter("@dFTnk1Cap", vehicletype.dFTnk1Cap ?? 0);
                param[30] = new SqlParameter("@dFTnk2Cap", vehicletype.dFTnk2Cap ?? 0);
                param[31] = new SqlParameter("@iIsDef", vehicletype.iIsDef ?? false);
                param[32] = new SqlParameter("@dDefCap", vehicletype.dDefCap ?? 0);
                param[33] = new SqlParameter("@fuelSensor", vehicletype.fuelSensor ?? 0);
                param[34] = new SqlParameter("@sFuelSensCmpy", vehicletype.sFuelSensCmpy ?? "");

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
                    res.message = "We are facing server issue at the moment please try again...";
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

        [HttpPost]

        [Route("VehicleTyreSave")]

        public Api_CommonResponse VehicleTyreSave(TyreMst Mst)

        {

            try

            {

                var vehId = Mst.iFk_VehId;

                var iId = Mst.iId;

                SqlParameter[] param = new SqlParameter[11];

                param[0] = new SqlParameter("@type", "Insert");

                param[1] = new SqlParameter("@iPk_TyreMstId", Mst.iPk_TyreMstId);

                param[2] = new SqlParameter("@iFk_VehId", Mst.iFk_VehId);

                param[3] = new SqlParameter("@iFrntTyrCnt", Mst.iFrntTyrCnt);

                param[4] = new SqlParameter("@iRearTyrCnt", Mst.iRearTyrCnt);

                param[5] = new SqlParameter("@iTotlTyrCnt", Mst.iTotlTyrCnt);

                param[6] = new SqlParameter("@iFk_BranchId", Mst.iFk_BranchId);

                param[7] = new SqlParameter("@iFk_YearId", Mst.iFk_YearId);

                param[8] = new SqlParameter("@iCrtdBy", Mst.iCrtdBy);

                param[9] = new SqlParameter("@iFk_LocationId", Mst.iFk_LocationId);

                param[10] = new SqlParameter("@iId", Mst.iId);

                DataSet ds = DBOperation.FillDataSet("[dbo].[Usp_Tyre_Mst_Save]", param);

                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)

                {

                    res.responseCode = Convert.ToInt32(ds.Tables[0].Rows[0]["Id"]);

                    res.message = ds.Tables[0].Rows[0]["Message"].ToString();

                    res.statusCode = Convert.ToInt32(ds.Tables[0].Rows[0]["statusCode"]);

                    foreach (var item in Mst.tyreDetails.ToList())

                    {

                        SqlParameter[] param1 = new SqlParameter[15];

                        param1[0] = new SqlParameter("@iFk_TyreMstId", res.responseCode);

                        param1[1] = new SqlParameter("@iFk_PositionId", item.position);

                        param1[2] = new SqlParameter("@sTyreNo", item.Id);

                        param1[3] = new SqlParameter("@iFk_TyrCatId", item.category);

                        param1[4] = new SqlParameter("@iFk_ManftrId", item.manufature);

                        param1[5] = new SqlParameter("@iFk_BrndId", item.brand);

                        param1[6] = new SqlParameter("@iFk_PatternId", item.pattern);

                        param1[7] = new SqlParameter("@iFk_TyrSize", item.size);

                        param1[8] = new SqlParameter("@iFk_Type", item.type);

                        param1[9] = new SqlParameter("@dNSD", item.nsd ?? 0);

                        param1[10] = new SqlParameter("@iTyreNo", item.no);

                        param1[11] = new SqlParameter("@iFk_TyreDesc", item.iFk_TyreDesc);

                        param1[12] = new SqlParameter("@iPK_DetId", item.iPk_DetId);

                        param1[13] = new SqlParameter("@vehId", vehId);

                        param1[14] = new SqlParameter("@iId", iId);

                        DataSet ds1 = DBOperation.FillDataSet("[dbo].[Usp_Tyre_Details_Save]", param1);

                    }

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


        [HttpPost]

        [Route("VehicleBatterySave")]

        public Api_CommonResponse VehicleBatterySave(BatteryVehicleMst Mst)

        {

            try

            {

                var vehId = Mst.iFk_VehId;

                var iId = Mst.iId;

                SqlParameter[] param = new SqlParameter[9];

                param[0] = new SqlParameter("@type", "Insert");

                param[1] = new SqlParameter("@iPk_BatteryMstId", Mst.iPk_BatteryMstId);

                param[2] = new SqlParameter("@iFk_VehId", Mst.iFk_VehId);

                param[3] = new SqlParameter("@iBatteryCount", Mst.iBatteryCount);

                param[4] = new SqlParameter("@iFk_BranchId", Mst.iFk_BranchId);

                param[5] = new SqlParameter("@iFk_YearId", Mst.iFk_YearId);

                param[6] = new SqlParameter("@iCrtdBy", Mst.iCrtdBy);

                param[7] = new SqlParameter("@iFk_LocationId", Mst.iFk_LocationId);

                param[8] = new SqlParameter("@iId", Mst.iId);

                DataSet ds = DBOperation.FillDataSet("[dbo].[Usp_Battery_Mst_Save]", param);

                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)

                {

                    res.responseCode = Convert.ToInt32(ds.Tables[0].Rows[0]["Id"]);

                    res.message = ds.Tables[0].Rows[0]["Message"].ToString();

                    res.statusCode = Convert.ToInt32(ds.Tables[0].Rows[0]["statusCode"]);

                    foreach (var item in Mst.batteryDetails.ToList())

                    {

                        SqlParameter[] param1 = new SqlParameter[7];

                        param1[0] = new SqlParameter("@iFk_BatteryMstId", res.responseCode);

                        param1[1] = new SqlParameter("@sBatteryGuidNo", item.Id);

                        param1[2] = new SqlParameter("@iBatteryNo", item.no);

                        param1[3] = new SqlParameter("@iFk_BatteryDesc", item.iFk_BatteryDesc);

                        param1[4] = new SqlParameter("@iPK_DetId", item.iPk_DetId);

                        param1[5] = new SqlParameter("@vehId", vehId);

                        param1[6] = new SqlParameter("@iId", iId);

                        DataSet ds1 = DBOperation.FillDataSet("[dbo].[Usp_Battery_Details_Save]", param1);

                    }

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
