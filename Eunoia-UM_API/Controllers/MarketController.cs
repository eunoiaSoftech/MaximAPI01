using Eunoia_UM_API.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using Eunoia_UM_API.Model;
using System.Text;

namespace Eunoia_UM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketController : ControllerBase
    {
        Api_CommonResponse api_Response = new Api_CommonResponse();


        [HttpGet]
        [Route("GetModelList")]

        public Api_CommonResponse GetModelList()
        {

            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_VehicleType_Dropdownfill]");
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Model List";
                    api_Response.statusCode = 1;
                    api_Response.data = JsonConvert.SerializeObject(ds.Tables[1]);
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
        [Route("GetVehicleType")]

        public Api_CommonResponse GetVehicleType()
        {

            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_VehicleType_Dropdownfill]");
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Vehicle Type";
                    api_Response.statusCode = 1;
                    api_Response.data = JsonConvert.SerializeObject(ds.Tables[5]);
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
        [Route("GetBodyType")]

        public Api_CommonResponse GetBodyType()
        {

            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_VehicleType_Dropdownfill]");
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Body Type";
                    api_Response.statusCode = 1;
                    api_Response.data = JsonConvert.SerializeObject(ds.Tables[3]);
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
        [Route("GetUOM")]

        public Api_CommonResponse GetUOM()
        {

            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_VehicleType_Dropdownfill]");
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "UOM";
                    api_Response.statusCode = 1;
                    api_Response.data = JsonConvert.SerializeObject(ds.Tables[4]);
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
        [Route("GetOwnerList")]

        public Api_CommonResponse GetOwnerList()
        {

            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_VehicleType_Dropdownfill]");
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Owner List";
                    api_Response.statusCode = 1;
                    api_Response.data = JsonConvert.SerializeObject(ds.Tables[6]);
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
        [Route("AddVehicleDetails")]

        public Api_CommonResponse AddVehicleDetails(Market market)


        {
            try
            {
                var Pk_MktVehId = 0;

                SqlParameter[] param = new SqlParameter[22];
                param[0] = new SqlParameter("@iPk_MktVehId", market.Pk_MktVehId);
                param[1] = new SqlParameter("@iVehMode", market.VehMode);
                param[2] = new SqlParameter("@iFk_OwnerName", market.Fk_OwnerName);
                param[3] = new SqlParameter("@sMktVehNo", market.MktVehNo);
                param[4] = new SqlParameter("@dtRegDate", market.RegDate);
                param[5] = new SqlParameter("@sDescription", "");

                param[6] = new SqlParameter("@iFk_ModelNo", market.Fk_ModelNo);
                param[7] = new SqlParameter("@sEngineNo", market.EngineNo);
                param[8] = new SqlParameter("@sChassisNo", market.ChassisNo);
                param[9] = new SqlParameter("@iFk_VehType", market.Fk_VehType);
                param[10] = new SqlParameter("@iIsGsp", market.IsGsp);
                param[11] = new SqlParameter("@iIsActive", market.IsActive);
                param[12] = new SqlParameter("@dGVWWt", market.GVWWt);
                param[13] = new SqlParameter("@dTareWt", market.TareWt);
                param[14] = new SqlParameter("@iIsNp", market.IsNp);
                param[15] = new SqlParameter("@dtNpExpdt", market.NpExpdt);
                param[16] = new SqlParameter("@iIsInsurance", market.IsInsurance);
                param[17] = new SqlParameter("@dtInsuranceExpdt", market.InsuranceExpdt);
                param[18] = new SqlParameter("@IsFiteness", market.IsFiteness);
                param[19] = new SqlParameter("@dtFitenessDt", market.FitenessDt);
                param[20] = new SqlParameter("@dNetwt", market.Netwt);
                param[21] = new SqlParameter("@aRcFile", market.RcFile);

                DataSet ds = DBOperation.FillDataSet("[dbo].[Usp_Market_Vehicle_Save]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    Pk_MktVehId = Convert.ToInt32(ds.Tables[0].Rows[0]["Pk_MktVehId"]);


                    api_Response.message = ds.Tables[0].Rows[0]["Message"].ToString();
                    api_Response.statusCode = Convert.ToInt32(ds.Tables[0].Rows[0]["StatusCode"]);
                    api_Response.responseCode = 1;

                }
                else
                {
                    api_Response.message = "No Data Available";
                    api_Response.statusCode = 200;
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

        [HttpGet("GetVehicleDetails")]
        public async Task<IActionResult> GetVehicleDetails(string vehicleNumber)
        {
            try
            {
                if (string.IsNullOrEmpty(vehicleNumber))
                {
                    return BadRequest(new { Message = "Vehicle number is required." });
                }

                using var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.invincibleocean.com/invincible/vehicleRcV6");

                // Add headers
                request.Headers.Add("secretKey", "coOZjG2IFrM0jMeYWWYo6Vm2rEVNpi8l2sCXnFsZ1i2eMX6qbyznBOirNpqC7O7lG");
                request.Headers.Add("clientId", "11f884fbc7de6705f0e5dad95dec25f6:d7bcd09c70213f190dac7cc24b07caf7");

                // Add JSON content
                var jsonContent = new { vehicleNumber = vehicleNumber };
                var content = new StringContent(JsonConvert.SerializeObject(jsonContent), Encoding.UTF8, "application/json");
                request.Content = content;

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<VehicleApiResponse>(responseContent);

                return Ok(apiResponse?.Result?.Data);
            }
            catch (HttpRequestException httpEx)
            {
                return StatusCode(502, new { Message = "Error while connecting to the vehicle API.", Details = httpEx.Message });
            }
            catch (JsonSerializationException jsonEx)
            {
                return StatusCode(500, new { Message = "Error parsing the API response.", Details = jsonEx.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Unexpected error occurred.", Details = ex.Message });
            }
        }


    }
}
