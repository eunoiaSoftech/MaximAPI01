using Eunoia_UM_API.Helper;
using Eunoia_UM_API.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace Eunoia_UM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Api : Controller
    {

        Api_CommonResponse api_Response = new Api_CommonResponse();

        [HttpGet]
        [Route("TallyexportTripExp")]
        public Api_CommonResponse TallyexportTripExp(string sFormdate, string sTodate)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@fromdate", sFormdate);
                param[1] = new SqlParameter("@todate", sTodate);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Accounts_TalleyExport_TripExpense]", param);

                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<Dictionary<string, object>> dataList = new List<Dictionary<string, object>>();
                    if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                    {
                        foreach (DataTable table in ds.Tables)
                        {
                            foreach (DataRow row in table.Rows)
                            {
                                var dataRow = new Dictionary<string, object>();
                                foreach (DataColumn col in table.Columns)
                                {
                                    dataRow[col.ColumnName] = row[col];
                                }
                                dataList.Add(dataRow);
                            }
                        }
                    }

                    api_Response.responseCode = dataList.Count > 0 ? 0 : 1;
                    api_Response.message = api_Response.responseCode == 0 ? "Talley export tripexp" : "Details Not Found...";
                    api_Response.statusCode = api_Response.responseCode == 0 ? 1 : -1;
                    api_Response.data = api_Response.responseCode == 0 ? dataList : null;
                }
                else
                {
                    api_Response.responseCode = 1;
                    api_Response.message = "Details Not Found...";
                    api_Response.statusCode = -1;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return api_Response;
        }

        [HttpGet]
        [Route("TallyexportTripData")]
        public Api_CommonResponse TallyexportTripData(string sFormdate, string sTodate)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@fromdate", sFormdate);
                param[1] = new SqlParameter("@todate", sTodate);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Operation_FreightIncome_Export]", param);


                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<Dictionary<string, object>> dataList = new List<Dictionary<string, object>>();
                    if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                    {
                        foreach (DataTable table in ds.Tables)
                        {
                            foreach (DataRow row in table.Rows)
                            {
                                var dataRow = new Dictionary<string, object>();
                                foreach (DataColumn col in table.Columns)
                                {
                                    dataRow[col.ColumnName] = row[col];
                                }
                                dataList.Add(dataRow);
                            }
                        }
                    }

                    api_Response.responseCode = dataList.Count > 0 ? 0 : 1;
                    api_Response.message = api_Response.responseCode == 0 ? "Talley export FrtIncome" : "Details Not Found...";
                    api_Response.statusCode = api_Response.responseCode == 0 ? 1 : -1;
                    api_Response.data = api_Response.responseCode == 0 ? dataList : null;
                }
                else
                {
                    api_Response.responseCode = 1;
                    api_Response.message = "Details Not Found...";
                    api_Response.statusCode = -1;
                }


            }
            catch (Exception e)
            {
                throw e;
            }
            return api_Response;
        }

        [HttpGet]
        [Route("LegDataforGPS")]
        public Api_CommonResponse LegDataforGPS(string sFormdate, string sTodate)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@fromdate", sFormdate);
                param[1] = new SqlParameter("@todate", sTodate);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Operation_GPS_LegData]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.responseCode = 1;
                    api_Response.message = "Synch Successfully";
                    api_Response.data = JsonConvert.SerializeObject(ds.Tables[0]);
                    api_Response.statusCode = 200;
                }
                else
                {
                    api_Response.responseCode = 1;
                    api_Response.message = "Details Not Found...";
                    api_Response.statusCode = -1;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return api_Response;
        }

        [HttpPost]
        [Route("GetVehicleDetailsForAccident")]
        public Api_CommonResponse GetVehicleDetailsForAccident(int iFk_UserType, string iFk_UserId)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@iFk_UserType", iFk_UserType);
                param[1] = new SqlParameter("@iFk_UserId", iFk_UserId);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_GetVehicleListForAccident_MobileApp]", param);

                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.responseCode = 200;
                    api_Response.message = "Data Get Successfully";
                    api_Response.statusCode = 0;
                    api_Response.data = null;
                    api_Response.data1 = JsonConvert.DeserializeObject<List<VehicleDet>>(JsonConvert.SerializeObject(ds.Tables[0]));

                }
                else
                {
                    api_Response.responseCode = 400;
                    api_Response.message = "API Down...";
                    api_Response.statusCode = -1;
                }

            }
            catch (Exception ex)
            {
                api_Response.responseCode = 500;
                api_Response.message = "Internal server error: " + ex.Message;
                api_Response.statusCode = -1;
            }
            return api_Response;
        }

        [HttpPost]
        [Route("GetDriversByVehicleId")]
        public Api_CommonResponse GetDriversByVehicleId(int vehicleId)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@VehicleId", vehicleId);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_GetDriversByVehicleId]", param);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.responseCode = 200;
                    api_Response.message = "Data retrieved successfully";
                    api_Response.statusCode = 0;
                    api_Response.data1 = JsonConvert.DeserializeObject<List<DriverDetails>>(
                        JsonConvert.SerializeObject(ds.Tables[0])
                    );
                }
                else
                {
                    api_Response.responseCode = 404;
                    api_Response.message = "No drivers found for the given vehicle ID";
                    api_Response.statusCode = -1;
                }
            }
            catch (Exception ex)
            {
                // Log ex if needed
                api_Response.responseCode = 500;
                api_Response.message = "Internal server error: " + ex.Message;
                api_Response.statusCode = -1;
            }

            return api_Response;
        }

        [HttpPost]
        [Route("GetLatestLRByVehicleId")]
        public Api_CommonResponse GetLatestLRByVehicleId(int vehicleId)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@VehicleId", vehicleId);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_GetLatestLR_ByVehicleAndDriver]", param);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    var lrData = JsonConvert.DeserializeObject<List<LRDetails>>(JsonConvert.SerializeObject(ds.Tables[0]));

                    api_Response.responseCode = 200;
                    api_Response.message = "LR record fetched successfully.";
                    api_Response.statusCode = 0;
                    api_Response.data = null;
                    api_Response.data1 = lrData;
                }
                else
                {
                    api_Response.responseCode = 404;
                    api_Response.message = "No LR record found for the given vehicle.";
                    api_Response.statusCode = -1;
                }
            }
            catch (Exception ex)
            {
                api_Response.responseCode = 500;
                api_Response.message = "Internal server error: " + ex.Message;
                api_Response.statusCode = -1;
            }
            return api_Response;
        }

        [HttpGet]
        [Route("GetPersonVisited")]
        public Api_CommonResponse GetPersonVisited()
        {
            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_GetPersonVisited]", null);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.responseCode = 200;
                    api_Response.message = "Data retrieved successfully";
                    api_Response.statusCode = 0;

                    api_Response.data = JsonConvert.DeserializeObject<List<PersonVisitedModel>>(
                        JsonConvert.SerializeObject(ds.Tables[0]));
                }
                else
                {
                    api_Response.responseCode = 404;
                    api_Response.message = "No records found";
                    api_Response.statusCode = -1;
                }
            }
            catch (Exception ex)
            {
                api_Response.responseCode = 500;
                api_Response.message = "Internal server error: " + ex.Message;
                api_Response.statusCode = -1;
            }

            return api_Response;
        }

        [HttpPost]
        [Route("SaveAccidentClaim")]
        //public async Task<Api_CommonResponse> SaveAccidentClaim(AccidentClaimLogModel input, List<IFormFile> Images)
        public async Task<Api_CommonResponse> SaveAccidentClaim([FromForm] List<IFormFile> files, [Required][FromForm] string jsonData)
        {
            try
            {
                AccidentClaimLogModel input = null;
                if (jsonData != null)
                {
                    input = JsonConvert.DeserializeObject<AccidentClaimLogModel>(jsonData);

                }
                var parameters = new[]
                {
                    new SqlParameter("@sCurrentLocation", input.sCurrentLocation ?? (object)DBNull.Value),
                    new SqlParameter("@sDriverName", input.sDriverName ?? (object)DBNull.Value),
                    new SqlParameter("@sCityName", input.sCityName ?? (object)DBNull.Value),
                    new SqlParameter("@sMobileNo", input.sMobileNo ?? (object)DBNull.Value),
                    new SqlParameter("@sLicenseNo", input.sLicenseNo ?? (object)DBNull.Value),
                    new SqlParameter("@dtLicenseValidity", input.dtLicenseValidity ?? (object)DBNull.Value),
                    new SqlParameter("@sDriverCode", input.sDriverCode ?? (object)DBNull.Value),
                    new SqlParameter("@sTripId", input.sTripId ?? (object)DBNull.Value),
                    new SqlParameter("@sEwayBillNo", input.sEwayBillNo ?? (object)DBNull.Value),
                    new SqlParameter("@dtEwayExpiry", input.dtEwayExpiry ?? (object)DBNull.Value),
                    new SqlParameter("@dtEntryDate", input.dtEntryDate ?? (object)DBNull.Value),
                    new SqlParameter("@iAccidentNo", input.iAccidentNo ?? (object)DBNull.Value),
                    new SqlParameter("@sVehicleNo", input.sVehicleNo ?? (object)DBNull.Value),
                    new SqlParameter("@dtDateOfAccident", input.dtDateOfAccident ?? (object)DBNull.Value),
                    new SqlParameter("@iPersonVisitedId", input.iPersonVisitedId ?? (object)DBNull.Value),
                    new SqlParameter("@dSettlementAmount", input.dSettlementAmount ?? (object)DBNull.Value),
                    new SqlParameter("@dDebitToDriverAmount", input.dDebitToDriverAmount ?? (object)DBNull.Value),
                    new SqlParameter("@sAccidentRemark", input.sAccidentRemark ?? (object)DBNull.Value),
                    new SqlParameter("@sOnSpotPersonSignature", input.sOnSpotPersonSignature ?? (object)DBNull.Value),
                    new SqlParameter("@sSpotPersonSignature", input.sSpotPersonSignature ?? (object)DBNull.Value),
                    new SqlParameter("@bIsConfirmed", input.bIsConfirmed ?? false)
                };

                var ds = DBOperation.FillDataSet("dbo.USP_MobileApp_AccidentClaim_Save", parameters);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    int accidentClaimLogId = Convert.ToInt32(ds.Tables[0].Rows[0]["iPk_AccidentClaimLogId"]);

                    //var dataTable = ds.Tables[0];
                    //var row = dataTable.Rows[0];
                    // Optionally map DataRow to a model or dictionary to return
                    //var savedRecord = new Dictionary<string, object>();

                    //foreach (System.Data.DataColumn col in dataTable.Columns)
                    //{
                    //    savedRecord[col.ColumnName] = dataTable.Rows[0][col];
                    //}

                    //var savedRecord = new AccidentClaimResponseModel
                    //{
                    //    accidentClaimId = Convert.ToInt32(row["iPk_AccidentClaimLogId"]),
                    //    vehicleNo = row["sVehicleNo"]?.ToString(),
                    //    accidentNo = Convert.ToInt32(row["iAccidentNo"]),
                    //    accidentDate = Convert.ToDateTime(row["dtDateOfAccident"]),
                    //    accidentLocation = row["sCurrentLocation"]?.ToString(),
                    //    cityName = row["sCityName"]?.ToString(),
                    //    tripId = row["sTripId"]?.ToString(),
                    //    driverName = row["sDriverName"]?.ToString(),
                    //    driverId = row["sDriverCode"]?.ToString(),
                    //    personVisited = Convert.ToInt32(row["sPersonVisited"]),
                    //    settlementAmount = Convert.ToDecimal(row["dSettlementAmount"]),
                    //    debitToDriverAmount = Convert.ToDecimal(row["dDebitToDriverAmount"]),
                    //    remark = row["sAccidentRemark"]?.ToString(),
                    //    onSpotSignature = row["sOnSpotPersonSignature"]?.ToString(),
                    //    spotSignature = row["sSpotPersonSignature"]?.ToString(),
                    //    confirmation = Convert.ToBoolean(row["bIsConfirmed"]),

                    //    //mobileNo = row["sMobileNo"]?.ToString(),
                    //    licenseNo = row["sLicenseNo"]?.ToString(),
                    //    licenseValidity = Convert.ToDateTime(row["dtLicenseValidity"]),
                    //    ewayBillNo = row["sEwayBillNo"]?.ToString(),
                    //    ewayExpiry = Convert.ToDateTime(row["dtEwayExpiry"]),
                    //    entryDate = Convert.ToDateTime(row["dtEntryDate"]),
                    //};

                    //int accidentClaimLogId = savedRecord.accidentClaimId;

                    if (input.CraneDetails != null && input.CraneDetails.Count > 0)
                    {
                        foreach (var crane in input.CraneDetails)
                        {
                            SqlParameter[] craneParams = new SqlParameter[]
                            {
                                new SqlParameter("@iFk_AccidentClaimLogId", accidentClaimLogId),
                                new SqlParameter("@sCraneType", crane.sCraneType ?? (object)DBNull.Value),
                                new SqlParameter("@iCraneCount", crane.iCraneCount ?? (object)DBNull.Value),
                                new SqlParameter("@dCraneAmount", crane.dCraneAmount ?? (object)DBNull.Value)
                            };
                            DBOperation.FillDataSet("dbo.USP_MobileApp_CraneDetailAccidentClaim_Save", craneParams);
                        }
                    }

                    //2. Retrieve CraneDetails to include in response
                    //var craneFetchParams = new[]
                    //{
                    //    new SqlParameter("@iFk_AccidentClaimLogId", accidentClaimLogId)
                    //};

                    //var craneDs = DBOperation.FillDataSet("dbo.USP_MobileApp_CraneDetailAccidentClaim_Get", craneFetchParams);

                    //if (craneDs != null && craneDs.Tables.Count > 0 && craneDs.Tables[0].Rows.Count > 0)
                    //{
                    //    var craneList = new List<CraneDetails>();

                    //    foreach (DataRow craneRow in craneDs.Tables[0].Rows)
                    //    {
                    //        craneList.Add(new CraneDetails
                    //        {
                    //            sCraneType = craneRow["sCraneType"]?.ToString(),
                    //            iCraneCount = craneRow["iCraneCount"] != DBNull.Value ? Convert.ToInt32(craneRow["iCraneCount"]) : (int?)null,
                    //            dCraneAmount = craneRow["dCraneAmount"] != DBNull.Value ? Convert.ToDecimal(craneRow["dCraneAmount"]) : (decimal?)null
                    //        });
                    //    }

                    //    savedRecord.craneDetails = craneList;
                    //}

                    // If there are images then save 
                    if (files != null && files.Count > 0)
                    {
                        string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/AccidentClaimImages");
                        Directory.CreateDirectory(uploadsFolder);

                        foreach (var file in files)
                        {
                            if (file.Length > 0)
                            {
                                string uniqueFileName = Guid.NewGuid() + "_" + file.FileName;
                                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    await file.CopyToAsync(stream);
                                }

                                SqlParameter[] imgParam = new SqlParameter[]
                                {
                                    new SqlParameter("@iFk_AccidentClaimLogId", accidentClaimLogId),
                                    new SqlParameter("@sImagePath", filePath)
                                };
                                DBOperation.FillDataSet("dbo.USP_MobileApp_AccidentClaim_Attachment_Save", imgParam);
                            }
                        }
                    }

                    api_Response.statusCode = 0;
                    api_Response.responseCode = 200;
                    api_Response.message = "Accident claim log saved successfully.";
                    api_Response.data = accidentClaimLogId;
                }
                else
                {
                    api_Response.statusCode = 1;
                    api_Response.responseCode = 500;
                    api_Response.message = "Failed to save accident claim log.";
                    api_Response.data = null;
                }
            }
            catch (Exception ex)
            {
                api_Response.statusCode = 1;
                api_Response.responseCode = 500;
                api_Response.message = "Internal server error: " + ex.Message;
                api_Response.data = null;
            }

            return api_Response;
        }

        [HttpGet]
        [Route("GetAccidentDetailsByVehicleNo")]
        public Api_CommonResponse GetAccidentDetailsByVehicleNo(int vehicleNo)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@VehicleNo", vehicleNo);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_GetAccidentsByVehicleNo]", param);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.responseCode = 200;
                    api_Response.message = "Data retrieved successfully";
                    api_Response.statusCode = 0;
                    api_Response.data1 = JsonConvert.DeserializeObject<List<AccidentDetailsVehicleNo>>(
                        JsonConvert.SerializeObject(ds.Tables[0])
                    );
                }
                else
                {
                    api_Response.responseCode = 404;
                    api_Response.message = "No acccident details found for the given vehicle ID";
                    api_Response.statusCode = -1;
                }
            }
            catch (Exception ex)
            {
                // Log ex if needed
                api_Response.responseCode = 500;
                api_Response.message = "Internal server error: " + ex.Message;
                api_Response.statusCode = -1;
            }

            return api_Response;
        }

        [HttpGet]
        [Route("GetCityNames")]
        public Api_CommonResponse GetCityNames()
        {
            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_GetCityNames]", null);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.responseCode = 200;
                    api_Response.message = "Data retrieved successfully";
                    api_Response.statusCode = 0;

                    api_Response.data = JsonConvert.DeserializeObject<List<CityNameModel>>(
                        JsonConvert.SerializeObject(ds.Tables[0]));
                }
                else
                {
                    api_Response.responseCode = 404;
                    api_Response.message = "No records found";
                    api_Response.statusCode = -1;
                }
            }
            catch (Exception ex)
            {
                api_Response.responseCode = 500;
                api_Response.message = "Internal server error: " + ex.Message;
                api_Response.statusCode = -1;
            }

            return api_Response;
        }


    }
}
