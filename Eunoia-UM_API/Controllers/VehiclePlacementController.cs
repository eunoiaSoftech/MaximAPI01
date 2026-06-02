using Eunoia_UM.Models;
using Eunoia_UM_API.Helper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace Eunoia_UM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclePlacementController : Controller
    {
        Api_CommonResponse api_Response = new Api_CommonResponse();

        [HttpGet]
        [Route("GetVehiclePlacementList")]
        public Api_CommonResponse GetVehiclePlacementList(int id)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@id", id);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_VEHPLCMNTMST_GET]");
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Vehicle Placement List";
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
                throw e;
            }
            return api_Response;
        }

        [HttpGet]
        [Route("GetVehiclePlacementTools")]
        public Api_CommonResponse GetVehiclePlacementTools(int id)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@id", id);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_VEHPLCMNTTOOLDET_GET]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Vehicle Placement Tools List";
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
                throw e;
            }
            return api_Response;
        }

        [HttpGet]
        [Route("GetRouteList")]
        public Api_CommonResponse GetRouteList()
        {
            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_ROUTEMST_Get]");
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Route List";
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
                throw e;
            }
            return api_Response;
        }

        [HttpGet]
        [Route("GetVehList")]
        public Api_CommonResponse GetVehList(int? BranchId = 0, int? YearId = 0, int? UserId = 0)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[3];

                param[0] = new SqlParameter("@BranchId", BranchId);
                param[1] = new SqlParameter("@YearId", YearId);
                param[2] = new SqlParameter("@UserId", UserId);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_MST_Veh_GetForDDL]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Route List";
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
                throw e;
            }
            return api_Response;
        }

        [HttpGet]
        [Route("GetDOAllocationVehList")]
        public Api_CommonResponse GetDOAllocationVehList(int id)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];

                param[0] = new SqlParameter("@iFK_VehlPlcmnId", id);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Operation_DoAllocation_GetVehicle]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Route List";
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
                throw e;
            }
            return api_Response;
        }

        [HttpGet]
        [Route("GetDoList")]
        public Api_CommonResponse GetDoList()
        {
            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_TRN_ORDERMTRL_Get]");
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "DO List";
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
                throw e;
            }
            return api_Response;
        }

        [HttpPost]
        [Route("AddVehiclePlacement")]
        public Api_CommonResponse AddVehiclePlacement(VehiclePlacementModel veh)
        {
            try
            {
                int iPk_VehplcId = 0;
                SqlParameter[] param = new SqlParameter[41];

                param[0] = new SqlParameter("@iFk_VehclId", veh.iFk_VehclId);
                param[1] = new SqlParameter("@iMovemntTyp", veh.iMovemntTyp);
                param[2] = new SqlParameter("@iFk_FrmStationId", veh.iFk_FrmStationId);
                param[3] = new SqlParameter("@iFk_ToStationId", veh.iFk_ToStationId);
                param[4] = new SqlParameter("@iFk_RoutId", veh.iFk_RoutId);
                param[5] = new SqlParameter("@iFk_TrailerId", veh.iFk_TrailerId);
                param[6] = new SqlParameter("@iFk_DriverId", veh.iFk_DriverId);
                param[7] = new SqlParameter("@iFk_BrnchId", veh.iFk_BrnchId);
                param[8] = new SqlParameter("@iFk_YearId", veh.iFk_YearId);
                param[9] = new SqlParameter("@iCrtdBy", veh.iCrtdBy);
                param[10] = new SqlParameter("@iStatus", veh.iStatus);
                param[11] = new SqlParameter("@iMaxNo", veh.iMaxNo);
                param[12] = new SqlParameter("@iVoucherStyle", veh.iVoucherStyle);
                param[13] = new SqlParameter("@dtEntryDate", veh.dtEntryDate);
                param[14] = new SqlParameter("@dtReleaseDt", veh.dtReleaseDt);
                param[15] = new SqlParameter("@dDistance", veh.dDistance);
                param[16] = new SqlParameter("@dTransitTime", veh.dTransitTime);
                param[17] = new SqlParameter("@sEntryNo", veh.sEntryNo);
                param[18] = new SqlParameter("@sIpAddress", veh.sIpAddress);
                param[19] = new SqlParameter("@sBrowsername", veh.sBrowsername);
                param[20] = new SqlParameter("@sLongitude", veh.sLongitude);
                param[21] = new SqlParameter("@sLatitude", veh.sLatitude);
                param[22] = new SqlParameter("@iVehicleType", veh.iVehicleType);
                param[23] = new SqlParameter("@dMinGuarantee", veh.dMinGuarantee);
                param[24] = new SqlParameter("@dBookingRate", veh.dBookingRate);
                param[25] = new SqlParameter("@Pk_MaxTrpDetId", veh.Pk_MaxTrpDetId);
                param[26] = new SqlParameter("@iIsEmptyRtrn", veh.iIsEmptyRtrn);
                param[27] = new SqlParameter("@iPlacemntTyp", veh.iPlacemntTyp);
                param[28] = new SqlParameter("@iFk_CstmrId", veh.iFk_CstmrId);
                param[29] = new SqlParameter("@sDrvrNm", veh.sDrvrNm);
                param[30] = new SqlParameter("@sCntctNo", veh.sCntctNo);
                param[31] = new SqlParameter("@iFk_LocationId", veh.iFk_LocationId);
                param[32] = new SqlParameter("@iDeviceTye", veh.iDeviceTye);
                param[33] = new SqlParameter("@sDeviceNo", veh.sDeviceNo);
                param[34] = new SqlParameter("@iFittedBy", veh.iFittedBy);
                param[35] = new SqlParameter("@sLcsncNo", veh.sLcsncNo);
                param[36] = new SqlParameter("@dtLicExpDt", veh.dtLicExpDt);
                param[37] = new SqlParameter("@sRemarks", veh.sRemarks);
                param[38] = new SqlParameter("@iFk_MktBrokerId", veh.iFk_MktBrokerId);
                param[39] = new SqlParameter("@iPlacedBy", veh.iPlacedBy);
                param[40] = new SqlParameter("@iPlcmntNatureTyp", veh.iPlcmntNatureTyp);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_VEHPLCMNTMST_Save]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    iPk_VehplcId = Convert.ToInt32(ds.Tables[0].Rows[0]["iPk_VehplcId"]);
                    if (veh.tools != null && veh.tools.Count > 0 && iPk_VehplcId > 0)
                    {
                        foreach (var item in veh.tools)
                        {
                            AddVehicleToolsPlacement(item, iPk_VehplcId);
                        }
                    }
                    api_Response.responseCode = 0;
                    api_Response.message = ds.Tables[0].Rows[0]["Message"].ToString();
                    api_Response.statusCode = Convert.ToInt32(ds.Tables[0].Rows[0]["StatusCode"]);
                    api_Response.data = iPk_VehplcId;
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

        [HttpPost]
        [Route("AddVehicleToolsPlacement")]
        public string AddVehicleToolsPlacement(VehiclePlacementTools tools, int id)
        {
            var status = "";
            try
            {
                SqlParameter[] param = new SqlParameter[12];
                param[0] = new SqlParameter("@iFk_VehplcId", id);
                param[1] = new SqlParameter("@iFk_CustEnum", tools.iFk_CustEnum);
                param[2] = new SqlParameter("@iFk_ToolId", tools.iFk_ToolId);
                param[3] = new SqlParameter("@dPresentQty", tools.dPresentQty);
                param[4] = new SqlParameter("@dGivenQty", tools.dGivenQty);
                param[5] = new SqlParameter("@iFk_BrndId", tools.iFk_BrndId);
                param[6] = new SqlParameter("@dRtrnQty", tools.dRtrnQty);
                param[7] = new SqlParameter("@dScrpQty", tools.dScrpQty);
                param[8] = new SqlParameter("@dMsngQty", tools.dMsngQty);
                param[9] = new SqlParameter("@iFk_MsngRsn", tools.iFk_MsngRsn);
                param[10] = new SqlParameter("@dAvailblQty", tools.dAvailblQty);
                param[11] = new SqlParameter("@dClsngQty", tools.dClsngQty);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_VEHPLCMNTTOOLDET_Save]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    status = ds.Tables[0].Rows[0]["Message"].ToString();
                    api_Response.statusCode = Convert.ToInt32(ds.Tables[0].Rows[0]["StatusCode"]);
                }
                else
                {
                    status = "We Are facing some internal issue, Please try again later";
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return status;
        }

        [HttpPost]
        [Route("UpdateVehiclePlacement")]
        public Api_CommonResponse UpdateVehiclePlacement(VehiclePlacementModel veh)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[32];
                param[0] = new SqlParameter("@iPk_VehplcId", veh.iPk_VehplcId);
                param[1] = new SqlParameter("@sEntryNo", veh.sEntryNo);

                param[2] = new SqlParameter("@iPlacemntTyp", veh.iPlacemntTyp);
                param[3] = new SqlParameter("@iFk_VehclId", veh.iFk_VehclId);
                param[4] = new SqlParameter("@iFk_CstmrId", veh.iFk_CstmrId);
                param[5] = new SqlParameter("@iMovemntTyp", veh.iMovemntTyp);
                param[6] = new SqlParameter("@iFk_FrmStationId", veh.iFk_FrmStationId);
                param[7] = new SqlParameter("@iFk_ToStationId", veh.iFk_ToStationId);
                param[8] = new SqlParameter("@iFk_RoutId", veh.iFk_RoutId);
                param[9] = new SqlParameter("@dDistance", veh.dDistance);
                param[10] = new SqlParameter("@dTransitTime", veh.dTransitTime);
                param[11] = new SqlParameter("@iFk_TrailerId", veh.iFk_TrailerId);
                param[12] = new SqlParameter("@iFk_DriverId", veh.iFk_DriverId);
                param[13] = new SqlParameter("@iFk_BrnchId", veh.iFk_BrnchId);
                param[14] = new SqlParameter("@iFk_YearId", veh.iFk_YearId);
                param[15] = new SqlParameter("@iCrtdBy", veh.iCrtdBy);
                param[16] = new SqlParameter("@dtCrdtOn", veh.dtCrdtOn);
                param[17] = new SqlParameter("@iStatus", veh.iStatus);
                param[18] = new SqlParameter("@sDrvrNm", veh.sDrvrNm);
                param[19] = new SqlParameter("@sCntctNo", veh.sCntctNo);
                param[20] = new SqlParameter("@sIpAddress", veh.sIpAddress);
                param[21] = new SqlParameter("@sBrowsername", veh.sBrowsername);
                param[22] = new SqlParameter("@sLongitude", veh.sLongitude);
                param[23] = new SqlParameter("@sLatitude", veh.sLatitude);
                param[24] = new SqlParameter("@sLcsncNo", veh.sLcsncNo);
                param[25] = new SqlParameter("@dtLicExpDt", veh.dtLicExpDt);
                param[26] = new SqlParameter("@iVehicleType", veh.iVehicleType);
                param[27] = new SqlParameter("@sRemarks", veh.sRemarks);
                param[28] = new SqlParameter("@dtReleaseDt", veh.dtReleaseDt);
                param[29] = new SqlParameter("@iPlacedBy", veh.iPlacedBy);
                param[30] = new SqlParameter("@iFk_MktBrokerId", veh.iFk_MktBrokerId);
                param[31] = new SqlParameter("@iPlcmntNatureTyp", veh.iPlcmntNatureTyp);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_VEHPLCMNTMST_Update]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (veh.tools != null && veh.tools.Count > 0)
                    {
                        foreach (var item in veh.tools)
                        {
                            UpdateVehicleToolsPlacement(item);
                        }
                    }
                    api_Response.responseCode = 0;
                    api_Response.message = ds.Tables[0].Rows[0]["Message"].ToString();
                    api_Response.statusCode = Convert.ToInt32(ds.Tables[0].Rows[0]["StatusCode"]);
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

        [HttpPost]
        [Route("UpdateVehicleToolsPlacement")]
        public string UpdateVehicleToolsPlacement(VehiclePlacementTools tools)
        {
            var status = "";
            try
            {
                SqlParameter[] param = new SqlParameter[12];
                param[0] = new SqlParameter("@iPk_PlctoolId", tools.iPk_PlctoolId);
                param[1] = new SqlParameter("@iFk_VehplcId", tools.iFk_VehplcId);
                param[2] = new SqlParameter("@iFk_ToolId", tools.iFk_ToolId);
                param[3] = new SqlParameter("@dAvailblQty", tools.dAvailblQty);
                param[4] = new SqlParameter("@dPresentQty", tools.dPresentQty);
                param[5] = new SqlParameter("@iFk_BrndId", tools.iFk_BrndId);
                param[6] = new SqlParameter("@dGivenQty", tools.dGivenQty);
                param[7] = new SqlParameter("@dRtrnQty", tools.dRtrnQty);
                param[8] = new SqlParameter("@dScrpQty", tools.dScrpQty);
                param[9] = new SqlParameter("@dMsngQty", tools.dMsngQty);
                param[10] = new SqlParameter("@dClsngQty", tools.dClsngQty);
                param[11] = new SqlParameter("@iFk_MsngRsn", tools.iFk_MsngRsn);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_VEHPLCMNTTOOLDET_Update]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    status = ds.Tables[0].Rows[0]["Message"].ToString();
                    api_Response.statusCode = Convert.ToInt32(ds.Tables[0].Rows[0]["StatusCode"]);
                }
                else
                {
                    status = "We Are facing some internal issue, Please try again later";
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return status;
        }

        [HttpGet]
        [Route("GetPaperPermitIndex")]
        public Api_CommonResponse GetPaperPermitIndex(int id)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];

                param[0] = new SqlParameter("@id", id);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_DCMNTRNWL_Get]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Doc List";
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
                throw e;
            }
            return api_Response;
        }

        [HttpGet]
        [Route("GetInspectionVehicleList")]
        public Api_CommonResponse GetInspectionVehicleList()
        {
            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_INSPCTNMST_Status_Get]");
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Doc List";
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
                throw e;
            }
            return api_Response;
        }

        [HttpGet]
        [Route("GetVehDetails")]
        public Api_CommonResponse GetVehDetails(int id)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@id", id);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_GetVehTrlrDet]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Doc List";
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
                throw e;
            }
            return api_Response;
        }

        [HttpGet]
        [Route("GetDriverDetails")]
        public Api_CommonResponse GetDriverDetails(int id)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@id", id);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_DRVMST_Get]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Doc List";
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
                throw e;
            }
            return api_Response;
        }

        [HttpGet]
        [Route("GetCustomerForDDL")]
        public Api_CommonResponse GetCustomerForDDL()
        {
            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_prtymst_GetForDDL]");
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Doc List";
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
                throw e;
            }
            return api_Response;
        }

        [HttpGet]
        [Route("GetContractForDDL")]
        public Api_CommonResponse GetContractForDDL()
        {
            try
            {

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Contract_DynamicCNTRCTMST_Get]");
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "COntract List";
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
                throw e;
            }
            return api_Response;
        }

    }
}
