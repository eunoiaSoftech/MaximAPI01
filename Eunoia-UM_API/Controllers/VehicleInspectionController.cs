using Eunoia_UM_API.Helper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using Eunoia_UM.Models;

namespace Eunoia_UM_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class VehicleInspectionController : Controller
    {
        Api_CommonResponse api_Response = new Api_CommonResponse();

        [HttpGet]
        [Route("GetInspectionListForIndex")]
        public Api_CommonResponse GetInspectionListForIndex()
        {
            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_INSPCTNMST_Get]");
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
        [Route("GetVehiclePlacementList")]
        public Api_CommonResponse GetVehiclePlacementList()
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@crntStatus", '2');
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_VEHPLCMNTMST_GET]", param);
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
        [Route("GetVehiclePlacementListForInsp")]
        public Api_CommonResponse GetVehiclePlacementListForInsp()
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@crntStatus", '0');
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_VEHPLCMNTMST_GET]", param);
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

        // TODO: Get this sp made from suhas
        [HttpGet]
        [Route("GetVehiclePlacementToolsList")]
        public Api_CommonResponse GetVehiclePlacementToolsList(int id)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@id", id);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_VEHPLCMNTTOOLDET_GET]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Vehicle Placement details";
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
        [Route("GetLRList")]
        public Api_CommonResponse GetLRList(int id)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@id", id);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_VehInspectionLRDET_GET]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Vehicle Placement details";
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

        [Route("InspectionLRList")]
        public Api_CommonResponse InspectionLRList(int id)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@id", id);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_VEHPLCMNTTOOLDET_GET]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Vehicle Placement details";
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
        [Route("GetInspectionToolsList")]
        public Api_CommonResponse GetInspectionToolsList(int id)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@id", id);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_INPCTNTOOLDET_Get]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Inspection tools details";
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
        [Route("GetInspectionToolsForEdit")]
        public Api_CommonResponse GetInspectionToolsForEdit(int inspId, int plcmntId)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@InspectionId", inspId);
                param[1] = new SqlParameter("@VehiclePlacementId", plcmntId);

                DataSet ds = DBOperation.FillDataSet("[dbo].[GetToolData]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Inspection tools details";
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
        [Route("AddVehicleInspection")]
        public Api_CommonResponse AddVehicleInspection(VehicleInspectionModel veh)
        {
            try
            {
                int iPk_InspectionId = 0;
                SqlParameter[] param = new SqlParameter[16];
                param[0] = new SqlParameter("@sEntryNo", veh.sEntryNo);
                param[1] = new SqlParameter("@dtEntryDate", veh.dtEntryDate);
                param[2] = new SqlParameter("@iFk_VehicleNo", veh.iFk_VehicleNo);
                param[3] = new SqlParameter("@bIsInspectDone", veh.bIsInspectDone);
                param[4] = new SqlParameter("@iFk_YearId", veh.iFk_YearId);
                param[5] = new SqlParameter("@iFk_BrnchId", veh.iFk_BrnchId);
                param[6] = new SqlParameter("@iCrtdBy", veh.iCrtdBy);
                param[7] = new SqlParameter("@iFk_TrailerId", veh.iFk_TrailerId);
                param[8] = new SqlParameter("@iFk_DriverId", veh.iFk_DriverId);
                param[9] = new SqlParameter("@iMaxNo", veh.iMaxNo);
                param[10] = new SqlParameter("@iVoucherStyle", veh.iVoucherStyle);
                param[11] = new SqlParameter("@sIpAddress", veh.sIpAddress);
                param[12] = new SqlParameter("@sBrowsername", veh.sBrowsername);
                param[13] = new SqlParameter("@sLongitude", veh.sLongitude);
                param[14] = new SqlParameter("@sLatitude", veh.sLatitude);
                param[15] = new SqlParameter("@iFk_VehicelId", veh.iFk_VehicelId);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_INSPCTNMST_Save]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    iPk_InspectionId = Convert.ToInt32(ds.Tables[0].Rows[0]["iPk_InspectionId"]);
                    if (veh.tools != null && veh.tools.Count > 0)
                    {
                        foreach (var item in veh.tools)
                        {
                            AddVehicleToolsInspection(item, iPk_InspectionId);
                        }
                    }
                    api_Response.responseCode = 0;
                    api_Response.message = ds.Tables[0].Rows[0]["Message"].ToString();
                    api_Response.statusCode = Convert.ToInt32(ds.Tables[0].Rows[0]["StatusCode"]);
                    api_Response.data = iPk_InspectionId;
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
        [Route("AddVehicleToolsInspection")]
        public string AddVehicleToolsInspection(VehicleInspectionToolDetails tools, int inspectionId)
        {
            var status = "";
            try
            {
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@iFk_InspectionId", inspectionId);
                param[1] = new SqlParameter("@iFk_ToolId", tools.iFk_ToolId);
                param[2] = new SqlParameter("@dAvailblQty", tools.dAvailblQty);
                param[3] = new SqlParameter("@bIsOk", tools.bIsOk);
                param[4] = new SqlParameter("@bIsNotOk", tools.bIsNotOk);
                param[5] = new SqlParameter("@sRemark", tools.sRemark);
                param[6] = new SqlParameter("@sAttachmnt", tools.sAttachmnt);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_INPCTNTOOLDET_Save]", param);
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
        [Route("EditVehicleInspection")]
        public Api_CommonResponse EditVehicleInspection(VehicleInspectionModel veh)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@iPk_InspectionId", veh.iPk_InspectionId);
                param[1] = new SqlParameter("@sEntryNo", veh.sEntryNo);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_INSPCTNMST_Update]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (veh.tools != null && veh.tools.Count > 0)
                    {
                        foreach (var item in veh.tools)
                        {
                            if (item.iPk_InspectToolId != 0)
                            {
                                EditVehicleToolsInspection(item, veh.iPk_InspectionId);
                            }
                            else
                            {
                                AddVehicleToolsInspection(item, veh.iPk_InspectionId);
                            }
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
        [Route("EditVehicleToolsInspection")]
        public string EditVehicleToolsInspection(VehicleInspectionToolDetails tools, int iFk_InspectionId)
        {
            var status = "";
            try
            {
                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@iPk_InspectToolId", tools.iPk_InspectToolId);
                param[1] = new SqlParameter("@iFk_InspectionId", iFk_InspectionId);
                param[2] = new SqlParameter("@iFk_ToolId", tools.iFk_ToolId);
                param[3] = new SqlParameter("@dAvailblQty", tools.dAvailblQty);
                param[4] = new SqlParameter("@bIsOk", tools.bIsOk);
                param[5] = new SqlParameter("@bIsNotOk", tools.bIsNotOk);
                param[6] = new SqlParameter("@sRemark", tools.sRemark);
                param[7] = new SqlParameter("@sAttachmnt", tools.sAttachmnt);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_INPCTNTOOLDET_Update]", param);
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
        [Route("AddVehicleCondition")]
        public Api_CommonResponse AddVehicleCondition(List<VehicleInspectionDetails> conditionlist)
        {
            try
            {
                foreach (var vehicle in conditionlist)
                {
                    var vehicleAttachment = "";

                    if (vehicle.sAttachmnt != null)
                    {
                        var foldername = vehicle.iFk_InspectId.ToString();
                        var fileName = foldername + vehicle.sVehiclPartNme + DateTime.Now.ToString("ddMMyyyyhhmm");
                        vehicleAttachment = DocumentUploader.UploadDocuments(fileName, foldername, Convert.FromBase64String(vehicle.sAttachmnt.Split(",")[1]), vehicle.sAttachmntExt);
                    }

                    SqlParameter[] param = new SqlParameter[7];

                    param[0] = new SqlParameter("@iFk_InspectId", vehicle.iFk_InspectId);
                    param[1] = new SqlParameter("@iFk_VehPartId", vehicle.iFk_VehPartId);
                    param[2] = new SqlParameter("@bIsOk", vehicle.bIsOk);
                    param[3] = new SqlParameter("@bIsNotOk", vehicle.bIsNotOk);
                    param[4] = new SqlParameter("@sRemark", vehicle.sRemark);
                    param[5] = new SqlParameter("@iLoadingId", vehicle.iLoadingId);
                    param[6] = new SqlParameter("@sAttchmnt", vehicleAttachment);

                    DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_INPCTNVEHDET_Save]", param);
                    if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                    {
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
            }
            catch (Exception e)
            {
                throw e;
            }
            return api_Response;
        }

        [HttpGet]
        [Route("GetVehicleCondition")]
        public Api_CommonResponse GetVehicleCondition(int id)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@id", id);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_INPCTNVEHDET_Get]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Vehicle Placement details";
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
        [Route("EditVehicleCondition")]
        public Api_CommonResponse EditVehicleCondition(List<VehicleInspectionDetails> conditionlist)
        {
            try
            {
                foreach (var vehicle in conditionlist)
                {
                    var vehicleAttachment = "";

                    if (vehicle.sAttachmnt != null)
                    {
                        var foldername = vehicle.iFk_InspectId.ToString();
                        var fileName = foldername + vehicle.sVehiclPartNme + DateTime.Now.ToString("ddMMyyyyhhmm");
                        vehicleAttachment = DocumentUploader.UploadDocuments(fileName, foldername, Convert.FromBase64String(vehicle.sAttachmnt.Split(",")[1]), vehicle.sAttachmntExt);
                    }

                    SqlParameter[] param = new SqlParameter[8];
                    param[0] = new SqlParameter("@iPk_VehCndId", vehicle.iPk_VehCndId);
                    param[1] = new SqlParameter("@iFk_InspectId", vehicle.iFk_InspectId);
                    param[2] = new SqlParameter("@iFk_VehPartId", vehicle.iFk_VehPartId);
                    param[3] = new SqlParameter("@bIsOk", vehicle.bIsOk);
                    param[4] = new SqlParameter("@bIsNotOk", vehicle.bIsNotOk);
                    param[5] = new SqlParameter("@sRemark", vehicle.sRemark);
                    param[6] = new SqlParameter("@sAttchmnt", vehicleAttachment);
                    param[7] = new SqlParameter("@iLoadingId", vehicle.iLoadingId);

                    DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_INPCTNVEHDET_Update]", param);
                    if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                    {
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
            }
            catch (Exception e)
            {
                throw e;
            }
            return api_Response;
        }

        [HttpGet]
        [Route("GetAreaList")]
        public Api_CommonResponse GetAreaList()
        {
            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_AREAMST_Get]");
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Area List";
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
