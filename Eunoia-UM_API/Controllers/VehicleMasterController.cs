using Eunoia_UM_API.Helper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using Eunoia_UM_API.Model;

namespace Eunoia_UM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleMasterController : ControllerBase
    {
        Api_CommonResponse api_Response = new Api_CommonResponse();

        [HttpPost]
        [Route("AddVehicle")]
        public Api_CommonResponse AddVehicle(VehicleMaster veh)
        {
            try
            {
                var PK_Id = 0;
                SqlParameter[] param = new SqlParameter[47];
                param[0] = new SqlParameter("@sUnqIdNo", veh.sUnqIdNo.ToUpper());
                param[1] = new SqlParameter("@sVehImg", veh.sVehImg);
                param[2] = new SqlParameter("@iVehType", veh.iVehType);
                param[3] = new SqlParameter("@sYrOfMfg", veh.sYrOfMfg);
                param[4] = new SqlParameter("@sVehStatus", veh.sVehStatus);
                param[5] = new SqlParameter("@iMaker", veh.iMaker);
                param[6] = new SqlParameter("@iModel", veh.iModel);
                param[7] = new SqlParameter("@sChassisNo", veh.sChassisNo);
                param[8] = new SqlParameter("@iBdType", veh.iBdType);
                param[9] = new SqlParameter("@sSubBdType", veh.sSubBdType);
                param[10] = new SqlParameter("@dLength", veh.dLength);
                param[11] = new SqlParameter("@dHeight", veh.dHeight);
                param[12] = new SqlParameter("@dWidth", veh.dWidth);
                param[13] = new SqlParameter("@dGVW_RC", veh.dGVW_RC);
                param[14] = new SqlParameter("@dGVW_Act", veh.dGVW_Act);
                param[15] = new SqlParameter("@iGrossUOM", veh.iGrossUOM);
                param[16] = new SqlParameter("@dTareWt_RC", veh.dTareWt_RC);
                param[17] = new SqlParameter("@dTare_Act", veh.dTare_Act);
                param[18] = new SqlParameter("@iTareUOM", veh.iTareUOM);
                param[19] = new SqlParameter("@iVehClass", veh.iVehClass);
                param[20] = new SqlParameter("@iTripExpCat", veh.iTripExpCat);
                param[21] = new SqlParameter("@iTrailerType", veh.iTrailerType);
                param[22] = new SqlParameter("@dLoadCap", veh.dLoadCap);
                param[23] = new SqlParameter("@dEPA_City", veh.dEPA_City);
                param[24] = new SqlParameter("@dEPA_Highway", veh.dEPA_Highway);
                param[25] = new SqlParameter("@sEngNo", veh.sEngNo);
                param[26] = new SqlParameter("@sEngDesc", veh.sEngDesc);
                param[27] = new SqlParameter("@dMaxHP", veh.dMaxHP);
                param[28] = new SqlParameter("@iFuelType", veh.iFuelType);
                param[29] = new SqlParameter("@dFTnk1Cap", veh.dFTnk1Cap);
                param[30] = new SqlParameter("@dFTnk2Cap", veh.dFTnk2Cap);
                param[31] = new SqlParameter("@iIsDef", veh.iIsDef);
                param[32] = new SqlParameter("@dDefCap", veh.dDefCap);
                param[33] = new SqlParameter("@iPurVndr", veh.iPurVndr);
                param[34] = new SqlParameter("@dPurPrice", veh.dPurPrice);
                param[35] = new SqlParameter("@dtPur", veh.dtPur);
                param[36] = new SqlParameter("@iRegLctn", veh.iRegLctn);
                param[37] = new SqlParameter("@iVehOAM", veh.iVehOAM);
                param[38] = new SqlParameter("@iFk_Branchid", veh.iFk_Branchid);
                param[39] = new SqlParameter("@iFk_PreparedBy", veh.iFk_PreparedBy);
                param[40] = new SqlParameter("@iFk_YearID", veh.iFk_YearID);
                param[41] = new SqlParameter("@dtRegdate", veh.dtRegdate);
                param[42] = new SqlParameter("@iRegisterAuthority", veh.iRegisterAuthority);
                param[43] = new SqlParameter("@iNoOfCylinders", veh.iNoOfCylinders);
                param[44] = new SqlParameter("@iSeatCapcty", veh.iSeatCapcty);
                param[45] = new SqlParameter("@sRCUploads", veh.sRCUploads);
                param[46] = new SqlParameter("@dtRCExpDate", veh.dtRCExpDate);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_AttachAddVehicleMaster_Insert]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    PK_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["PK_Id"]);
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


    }
}
