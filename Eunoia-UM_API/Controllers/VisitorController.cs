using Eunoia_UM_API.Helper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using Eunoia_UM_API.Model;
using Microsoft.OpenApi.Models;
using RestSharp;
using RestSharp.Authenticators;

namespace Eunoia_UM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorController : Controller
    {
        Api_CommonResponse api_Response = new Api_CommonResponse();
        [HttpPost]
        [Route("AddVisitor")]
        public Api_CommonResponse AddVisitor(VisitorMaster Mst)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[10];
                param[0] = new SqlParameter("@MstVst_Id", Mst.MstVst_Id);
                param[1] = new SqlParameter("@sVstNam", Mst.sVstNam);
                param[2] = new SqlParameter("@sVstCtNo", Mst.sVstCtNo);
                param[3] = new SqlParameter("@sVstComFrm", Mst.sVstComFrm);
                param[4] = new SqlParameter("@iVstLct", Mst.iVstLct);
                param[5] = new SqlParameter("@iVstMetId", Mst.iVstMetId);
                param[6] = new SqlParameter("@iVstPur", Mst.iVstPur);
                param[7] = new SqlParameter("@sVstPur", Mst.sVstPur);
                param[8] = new SqlParameter("@iVstMebCut", Mst.iVstMebCut);

                param[9] = new SqlParameter("@sVstPic", Mst.sVstPic);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_AddVisitorMaster_Insert]", param);
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
            catch (Exception e)
            {
                throw e;
            }
            return api_Response;
        }

        [HttpPost]
        [Route("WhatsAppIntegration")]
        public async Task<Api_CommonResponse> WhatsAppIntegration(VisitorMaster Mst)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[10];
                param[0] = new SqlParameter("@MstVst_Id", Mst.MstVst_Id);
                param[1] = new SqlParameter("@sVstNam", Mst.sVstNam);
                param[2] = new SqlParameter("@sVstCtNo", Mst.sVstCtNo);
                param[3] = new SqlParameter("@sVstComFrm", Mst.sVstComFrm);
                param[4] = new SqlParameter("@iVstLct", Mst.iVstLct);
                param[5] = new SqlParameter("@iVstMetId", Mst.iVstMetId);
                param[6] = new SqlParameter("@iVstPur", Mst.iVstPur);
                param[7] = new SqlParameter("@sVstPur", Mst.sVstPur);
                param[8] = new SqlParameter("@iVstMebCut", Mst.iVstMebCut);

                param[9] = new SqlParameter("@sVstPic", Mst.sVstPic);

                var options = new RestClientOptions("https://api.maytapi.com")
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("/api/0a89371e-e118-4b43-afb6-1ab5e57bdcd7/29146/sendMessage", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("x-maytapi-key", "69b7cdfb-0970-4b80-b4da-d645accf05a4");

                var body = @"{" + "\n" +
                @"    ""to_number"": 91" + Mst.sVstCtNo + "," + "\n" + @" ""type"": ""text""," + "\n" +
                @"    ""message"":""खम्मा घणी                                                                                                                 Dear  Guest,                                                                                                            Welcome to Tanushree Logistics Pvt Ltd.                                                         Have a great day.                                                        Regards,                                                                                                                          TLPL Team""
" + "\n" +
@"}";
                request.AddStringBody(body, DataFormat.Json);
                RestResponse response = await client.ExecuteAsync(request);

                var Tonumber = "0";

                SqlParameter[] param2 = new SqlParameter[1];
                param2[0] = new SqlParameter("@Id", Mst.iVstMetId);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Admin_GetUserContactNo]", param2);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.responseCode = 0;
                    api_Response.message = ds.Tables[0].Rows[0]["Message"].ToString();
                    Tonumber = ds.Tables[0].Rows[0]["sPhone"].ToString();
                }
                else
                {
                    api_Response.responseCode = 1;
                    api_Response.message = $"We are facing server issue at the moment please try again...";
                    api_Response.statusCode = -1;
                    api_Response.data = null;
                }

                var options2 = new RestClientOptions("https://api.maytapi.com")
                {
                    MaxTimeout = -1,
                };
                var client2 = new RestClient(options2);
                var request2 = new RestRequest("/api/0a89371e-e118-4b43-afb6-1ab5e57bdcd7/29146/sendMessage", Method.Post);
                request2.AddHeader("Content-Type", "application/json");
                request2.AddHeader("x-maytapi-key", "69b7cdfb-0970-4b80-b4da-d645accf05a4");
                var body2 = @"{
" + "\n" +
                @"    ""to_number"": 91" + Tonumber + "," + "\n" +
                @"    ""type"": ""media"",
" + "\n" +
                @"    ""message"":  " + '"' + Mst.sVstPic + '"' + "," + "\n" +
@"    ""text"": ""Hello Sir/Madam My Self  " + Mst.sVstNam + " , From " + Mst.sVstComFrm + "  .I come at your office to meet you." + '"' + "\n" + @"}";


                request2.AddStringBody(body2, DataFormat.Json);
                RestResponse response2 = await client.ExecuteAsync(request2);



            }
            catch (Exception e)
            {
                throw e;
            }
            return api_Response;
        }



        [Route("GetVisitorView")]
        [HttpGet]
        public Api_CommonResponse GetVisitorView()
        {
            try
            {

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_VISITOR_VIEW]");
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.statusCode = 1;
                    api_Response.responseCode = 0;
                    api_Response.message = $"Data For Fetched Successfully...";
                    api_Response.data = JsonConvert.SerializeObject(ds.Tables[0]);
                }
                else
                {
                    api_Response.statusCode = 1;
                    api_Response.responseCode = 0;
                    api_Response.message = "No Data Available...";
                }
            }
            catch (Exception e)
            {
                api_Response.statusCode = 0;
                api_Response.responseCode = 0;
                api_Response.message = e.Message;

                //ExceptionLogDL.SendExcepToDB(e, 0, "Class : AdminDL / Function : GetCustomEnum");
            }
            return api_Response;
        }

        [HttpPost]
        [Route("CeckOut")]
        public Api_CommonResponse CeckOut(int Id)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Id", Id);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_Visitor_Check]", param);
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
            catch (Exception e)
            {
                throw e;
            }
            return api_Response;
        }

        [HttpPost]
        [Route("SaveRespond")]
        public Api_CommonResponse SaveRespond(int Id, int iRestoVst, string sRestoVstMsg)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@Id", Id);
                param[1] = new SqlParameter("@iRestoVst", iRestoVst);
                param[2] = new SqlParameter("@sRestoVstMsg", sRestoVstMsg);
                param[3] = new SqlParameter("@type", "Response");

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_Visitor_Respond_Save]", param);
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
            catch (Exception e)
            {
                throw e;
            }
            return api_Response;
        }

        [HttpPost]
        [Route("SaveMeetingRespond")]
        public Api_CommonResponse SaveMeetingRespond(VisitorMaster Mst)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@Id", Mst.MstVst_Id);
                param[1] = new SqlParameter("@sAttament", Mst.sAtta);
                param[2] = new SqlParameter("@sRestoVstMsg", Mst.sVstPur);
                param[3] = new SqlParameter("@type", "ResponseMeeting");
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_ADMIN_Visitor_Respond_Save]", param);
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
            catch (Exception e)
            {
                throw e;
            }
            return api_Response;
        }


        [HttpPost]
        [Route("GetPrevData")]
        public Api_CommonResponse GetPrevData(string MobileNo)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@MobileNo", MobileNo);

                DataSet ds = DBOperation.FillDataSet("[dbo].[Usp_Admin_Visitor_GetPrevData]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.responseCode = 0;
                    api_Response.message = ds.Tables[0].Rows[0]["Message"].ToString();
                    api_Response.statusCode = Convert.ToInt32(ds.Tables[0].Rows[0]["StatusCode"]);
                    api_Response.data = JsonConvert.SerializeObject(ds.Tables[0]);
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
