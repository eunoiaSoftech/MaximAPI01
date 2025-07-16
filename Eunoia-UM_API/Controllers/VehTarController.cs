using Eunoia_UM_API.Helper;
using Eunoia_UM_API.Model;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;

namespace Eunoia_UM_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class VehTarController : Controller
    {
        Api_CommonResponse res = new Api_CommonResponse();

        [HttpPost]
        [Route("VehicleTargetSave")]
        public Api_CommonResponse VehicleTargetSave(VehicleTargetModel vehicletarget)
        {
            try
            {
                var PrID = 0;
                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@sEntryNo", vehicletarget.sEntryNo);
                param[1] = new SqlParameter("@dtEntryDt", vehicletarget.dtEntryDt);
                param[2] = new SqlParameter("@dtFrmDt", vehicletarget.dtFrmDt);
                param[3] = new SqlParameter("@dtToDt", vehicletarget.dtToDt);
                param[4] = new SqlParameter("@iFk_BranchId", vehicletarget.iFk_BranchId);
                param[5] = new SqlParameter("@iFk_YearId", vehicletarget.iFk_YearId);


                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_MST_VHCLTARGET_Save]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    PrID = Convert.ToInt32(ds.Tables[0].Rows[0]["iPk_TargetId"]);

                    if (vehicletarget.TrnVehicleList != null && vehicletarget.TrnVehicleList.Count > 0)
                    {
                        foreach (var item in vehicletarget.TrnVehicleList)
                        {
                            AddTrVehicletarget(item, PrID);
                        }
                    }
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


        [HttpPost]
        [Route("AddTrVehicletarget")]
        public Api_CommonResponse AddTrVehicletarget(TrnVehicle vehicletarget, int key)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@iFk_TargetId", key);
                param[1] = new SqlParameter("@iFk_VehclId", vehicletarget.iFk_VehclId);
                param[2] = new SqlParameter("@sVehiclNo", vehicletarget.sVehiclNo);
                param[3] = new SqlParameter("@dTarget", vehicletarget.dTarget);
                param[4] = new SqlParameter("@iStatus", vehicletarget.iStatus);
                param[5] = new SqlParameter("@iCrtdBy", vehicletarget.iCrtdBy);



                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_TRN_VHCLTARGDTL_Save]", param);
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



        /*[HttpPost]
        [Route("UpdateVehicleTarget")]
        public Api_CommonResponse UpdateVehicleTarget(VehicleMaster Tar)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@sEntryNo", Tar.sEntryNo);
                param[1] = new SqlParameter("@dtEntryDt", Tar.dtEntryDt);
                param[2] = new SqlParameter("@dtFrmDt", Tar.dtFrmDt);
                param[3] = new SqlParameter("@dtToDt", Tar.dtToDt);
                param[4] = new SqlParameter("@iFk_BranchId", Tar.iFk_BranchId);
                param[5] = new SqlParameter("@iFk_YearId", Tar.iFk_YearId);
                param[6] = new SqlParameter("@iStatus", Tar.iStatus);
                param[7] = new SqlParameter("@iCrtdBy", Tar.iCrtdBy);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_MST_VHCLTARGET_Update]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    res.message = ds.Tables[0].Rows[0]["Message"].ToString();
                    res.statusCode = Convert.ToInt32(ds.Tables[0].Rows[0]["StatusCode"]);
                    res.responseCode = 1;
                }
                else
                {
                    res.message = "No Data Available";
                    res.statusCode = 400;
                    res.responseCode = 0;
                }
            }
            catch (Exception e)
            {
                res.message = e.Message.ToString();
                res.statusCode = 400;
                res.responseCode = 0;

            }
            return res;
        }*/



        [HttpGet]
        [Route("GetVehicleNo")]
        public Api_CommonResponse GetVehicleNo()
        {
            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_GetVehicleNo]");
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
        [Route("GetVehicleTargetList")]
        public Api_CommonResponse GetVehicleTargetList()
        {
            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_GetVehicleTargetDetails_Get]");
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    res.message = "Vehicle Target List";
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



        [HttpGet]
        [Route("GetVehicleTargetForEdit")]
        public Api_CommonResponse GetVehicleTargetForEdit(int TargetID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@iPk_TargetId", TargetID);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_GetVehicleTargetDet_Get]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    res.message = "Tarloyee List";
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

        [HttpGet]
        [Route("GetVehicleTargetDetailsForEdit")]
        public Api_CommonResponse GetVehicleTargetDetailsForEdit(int TargetID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@iFk_TargetId", TargetID);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_TRN_VHCLTARGDTL_Get]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    res.message = "Tarloyee List";
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
        [Route("VehicleTargetUpdate")]
        public Api_CommonResponse VehicleTargetUpdate(VehicleTargetModel vehicletype)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@iPk_TargetId", vehicletype.iPk_TargetId);
                param[1] = new SqlParameter("@iPk_TargetDtlId", vehicletype.iPk_TargetDtlId);
                param[2] = new SqlParameter("@sEntryNo", vehicletype.sEntryNo);
                param[3] = new SqlParameter("@dtEntryDt", vehicletype.dtEntryDt);
                param[4] = new SqlParameter("@dtFrmDt", vehicletype.dtFrmDt);
                param[5] = new SqlParameter("@dtToDt", vehicletype.dtToDt);
                param[6] = new SqlParameter("@sVehiclNo", vehicletype.sVehiclNo);
                param[7] = new SqlParameter("@dTarget", vehicletype.dTarget);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_VehicleTarget_Update]", param);
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

