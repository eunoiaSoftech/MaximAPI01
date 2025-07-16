using Eunoia_UM_API.Helper;
using Eunoia_UM_API.Model;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics.Metrics;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Eunoia_UM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrailerController : Controller
    {
        Api_CommonResponse api_Response = new Api_CommonResponse();

        [HttpPost]
        [Route("AddTrailer")]
        public Api_CommonResponse AddTrailer(TrailerMaster trailer)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[56];
                param[0] = new SqlParameter("@dLength", trailer.dLength);
                param[1] = new SqlParameter("@dHeight", trailer.dHeight);
                param[2] = new SqlParameter("@dWidth", trailer.dWidth);
                param[3] = new SqlParameter("@SuspensionCapacity", trailer.Fk_SuspensionCapacity);
                param[4] = new SqlParameter("@dCapacity", trailer.dCapacity);
                param[5] = new SqlParameter("@steelGrade", trailer.steelGrade);
                param[6] = new SqlParameter("@sTrailerAssetId", trailer.TrailerNo);
                param[7] = new SqlParameter("@DescriptionOrRemarks", trailer.DescriptionOrRemarks);
                param[8] = new SqlParameter("@TrailerNo", trailer.TrailerNo);
                param[9] = new SqlParameter("@TrailerType", trailer.TrailerType);
                param[10] = new SqlParameter("@iFk_AxleType", trailer.iFk_AxleType);
                param[11] = new SqlParameter("@iFk_NoAxles", trailer.iFk_NoAxles);
                param[12] = new SqlParameter("@AxleMake", trailer.AxleMake);
                param[13] = new SqlParameter("@iAssetType", trailer.TrailerType);
                param[14] = new SqlParameter("@invoiceno", trailer.invoiceno);
                param[15] = new SqlParameter("@InvoiceDate", trailer.InvoiceDate);
                param[16] = new SqlParameter("@SupplierId", trailer.SupplierId);
                param[17] = new SqlParameter("@dTotalVolume", trailer.dTotalVolume);
                param[18] = new SqlParameter("@dWeight", trailer.dWeight);
                param[19] = new SqlParameter("@SuspensionMake", trailer.SuspensionMake);

                param[20] = new SqlParameter("@iFk_BranchId", trailer.iFk_Branchid);
                param[21] = new SqlParameter("@iFk_UserId", trailer.iFk_PreparedBy);
                param[22] = new SqlParameter("@iFk_yearId", trailer.iFk_YearID);
                param[23] = new SqlParameter("@WeightUOM", trailer.WeightUOM);
                //change
                param[24] = new SqlParameter("@dtRegDate", trailer.dtRegDate);
                param[25] = new SqlParameter("@dtRegVldDate", trailer.dtRegVldDate);
                param[26] = new SqlParameter("@iFk_OwnerName", trailer.iFk_OwnerName);
                param[27] = new SqlParameter("@sYrOfMfg", trailer.sYrOfMfg);
                param[28] = new SqlParameter("@sChassisNo", trailer.sChassisNo);
                param[29] = new SqlParameter("@sEngineNo", trailer.sEngineNo);
                param[30] = new SqlParameter("@sColor", trailer.sColor);
                param[31] = new SqlParameter("@iFk_VehClass", trailer.iFk_VehClass);
                param[32] = new SqlParameter("@iVehStatus", trailer.iVehStatus);
                param[33] = new SqlParameter("@iFk_VehModel", trailer.iFk_VehModel);
                param[34] = new SqlParameter("@iFk_BodyType", trailer.iFk_BodyType);
                param[35] = new SqlParameter("@dNoOfCylndrs", trailer.dNoOfCylndrs);
                param[36] = new SqlParameter("@dHrsPwr", trailer.dHrsPwr);
                param[37] = new SqlParameter("@iSeat", trailer.iSeat);
                param[38] = new SqlParameter("@dCbCpcty", trailer.dCbCpcty);
                param[39] = new SqlParameter("@dLdnWgt", trailer.dLdnWgt);
                param[40] = new SqlParameter("@dUnldnWgt", trailer.dUnldnWgt);
                param[41] = new SqlParameter("@dWhlBse", trailer.dWhlBse);
                param[42] = new SqlParameter("@dFlrArea", trailer.dFlrArea);
                param[43] = new SqlParameter("@dTaxAmnt", trailer.dTaxAmnt);
                param[44] = new SqlParameter("@dTaxPdUpto", trailer.dTaxPdUpto);
                param[45] = new SqlParameter("@iFk_Fuel", trailer.iFk_Fuel);
                param[46] = new SqlParameter("@dtFitNessUpto", trailer.dtFitNessUpto);
                param[47] = new SqlParameter("@sVehNorms", trailer.sVehNorms);
                param[48] = new SqlParameter("@sOwnerSrlNo", trailer.sOwnerSrlNo);
                param[49] = new SqlParameter("@iFk_OwnrShpTyp", trailer.iFk_OwnrShpTyp);
                param[50] = new SqlParameter("@sPrsntAddress", trailer.sPrsntAddress);
                param[51] = new SqlParameter("@iFk_LnkVehNo", trailer.iFk_LnkVehNo);
                param[52] = new SqlParameter("@sMobileNo", trailer.sMobileNo);
                param[53] = new SqlParameter("@sEmailId", trailer.sEmailId);
                param[54] = new SqlParameter("@iFk_VehMaker", trailer.iFk_VehMaker);
                param[55] = new SqlParameter("@sRegnNo", trailer.sRegnNo);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_TRLRMST_Save]", param);
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
            catch (SqlException e)
            {
                if (e.Number == 2627) // <-- but this will
                {
                    api_Response.responseCode = 1;
                    api_Response.message = $"Trailer Number already exists...";
                    api_Response.statusCode = -1;
                    api_Response.data = null;
                }
            }
            return api_Response;
        }

        [HttpGet]
        [Route("GetTrailerList")]
        public Api_CommonResponse GetTrailerList(int iFK_BranchId)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@iFK_BranchId", iFK_BranchId);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_TRLRMST_GetList]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Supplier bank List";
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
            }
            return api_Response;
        }

        [HttpGet]
        [Route("GetProductDetailsList")]
        public Api_CommonResponse GetProductDetailsList(int trailerId)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@iFK_TrlrMstId", trailerId);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_TRLRDET_GetList]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Supplier bank List";
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
            }
            return api_Response;
        }

        [HttpGet]
        [Route("GetProducts")]
        public Api_CommonResponse GetProducts()
        {
            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_CMDTYMST_GetList]");
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Supplier bank List";
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
            }
            return api_Response;
        }

        [HttpPost]
        [Route("AddProdDetails")]
        public Api_CommonResponse AddProdDetails(TrailerProductDetails det)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@iFK_TrlrMstId", det.iFK_TrlrMstId);
                param[1] = new SqlParameter("@iFk_ProductId", det.iFk_ProductId);
                param[2] = new SqlParameter("@ProductCpcty", det.ProductCpcty);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_MASTER_TRLRDET_Save]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = ds.Tables[0].Rows[0]["Message"].ToString();
                    api_Response.statusCode = Convert.ToInt32(ds.Tables[0].Rows[0]["StatusCode"]);
                    api_Response.responseCode = 1;
                }
                else
                {
                    api_Response.message = "No Data Available";
                    api_Response.statusCode = 400;
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

        [HttpGet]
        [Route("GetTrialerForEdit")]
        public Api_CommonResponse GetTrialerForEdit(int id)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@id", id);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_TRLRMST_GetTrailerForEdit]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Advance details list";
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
        [Route("UpdateTrailer")]
        public Api_CommonResponse UpdateTrailer(TrailerMaster trailer)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[57];
                param[0] = new SqlParameter("@dLength", trailer.dLength);
                param[1] = new SqlParameter("@dHeight", trailer.dHeight);
                param[2] = new SqlParameter("@dWidth", trailer.dWidth);
                param[3] = new SqlParameter("@SuspensionCapacity", trailer.SuspensionType);
                param[4] = new SqlParameter("@dCapacity", trailer.dCapacity);
                param[5] = new SqlParameter("@steelGrade", trailer.steelGrade);
                param[6] = new SqlParameter("@sTrailerAssetId", trailer.TrailerNo);
                param[7] = new SqlParameter("@DescriptionOrRemarks", trailer.DescriptionOrRemarks);
                param[8] = new SqlParameter("@TrailerNo", trailer.TrailerNo);
                param[9] = new SqlParameter("@TrailerType", trailer.TrailerType);
                param[10] = new SqlParameter("@iFk_AxleType", trailer.iFk_AxleType);
                param[11] = new SqlParameter("@iFk_NoAxles", trailer.iFk_NoAxles);
                param[12] = new SqlParameter("@AxleMake", trailer.AxleMake);
                param[13] = new SqlParameter("@iAssetType", trailer.TrailerType);
                param[14] = new SqlParameter("@invoiceno", trailer.invoiceno);
                param[15] = new SqlParameter("@InvoiceDate", trailer.InvoiceDate);
                param[16] = new SqlParameter("@SupplierId", trailer.SupplierId);
                param[17] = new SqlParameter("@dTotalVolume", trailer.dTotalVolume);
                param[18] = new SqlParameter("@dWeight", trailer.dWeight);
                param[19] = new SqlParameter("@SuspensionMake", trailer.SuspensionMake);

                param[20] = new SqlParameter("@iFk_BranchId", trailer.iFk_Branchid);
                param[21] = new SqlParameter("@iFk_UserId", trailer.iFk_PreparedBy);
                param[22] = new SqlParameter("@iFk_yearId", trailer.iFk_YearID);
                param[23] = new SqlParameter("@WeightUOM", trailer.WeightUOM);
                param[24] = new SqlParameter("@iPk_TrlrMstId", trailer.iPk_TrlrMstId);
                //change
                param[25] = new SqlParameter("@dtRegDate", trailer.dtRegDate);
                param[26] = new SqlParameter("@dtRegVldDate", trailer.dtRegVldDate);
                param[27] = new SqlParameter("@iFk_OwnerName", trailer.iFk_OwnerName);
                param[28] = new SqlParameter("@sYrOfMfg", trailer.sYrOfMfg);
                param[29] = new SqlParameter("@sChassisNo", trailer.sChassisNo);
                param[30] = new SqlParameter("@sEngineNo", trailer.sEngineNo);
                param[31] = new SqlParameter("@sColor", trailer.sColor);
                param[32] = new SqlParameter("@iFk_VehClass", trailer.iFk_VehClass);
                param[33] = new SqlParameter("@iVehStatus", trailer.iVehStatus);
                param[34] = new SqlParameter("@iFk_VehModel", trailer.iFk_VehModel);
                param[35] = new SqlParameter("@iFk_BodyType", trailer.iFk_BodyType);
                param[36] = new SqlParameter("@dNoOfCylndrs", trailer.dNoOfCylndrs);
                param[37] = new SqlParameter("@dHrsPwr", trailer.dHrsPwr);
                param[38] = new SqlParameter("@iSeat", trailer.iSeat);
                param[39] = new SqlParameter("@dCbCpcty", trailer.dCbCpcty);
                param[40] = new SqlParameter("@dLdnWgt", trailer.dLdnWgt);
                param[41] = new SqlParameter("@dUnldnWgt", trailer.dUnldnWgt);
                param[42] = new SqlParameter("@dWhlBse", trailer.dWhlBse);
                param[43] = new SqlParameter("@dFlrArea", trailer.dFlrArea);
                param[44] = new SqlParameter("@dTaxAmnt", trailer.dTaxAmnt);
                param[45] = new SqlParameter("@dTaxPdUpto", trailer.dTaxPdUpto);
                param[46] = new SqlParameter("@iFk_Fuel", trailer.iFk_Fuel);
                param[47] = new SqlParameter("@dtFitNessUpto", trailer.dtFitNessUpto);
                param[48] = new SqlParameter("@sVehNorms", trailer.sVehNorms);
                param[49] = new SqlParameter("@sOwnerSrlNo", trailer.sOwnerSrlNo);
                param[50] = new SqlParameter("@iFk_OwnrShpTyp", trailer.iFk_OwnrShpTyp);
                param[51] = new SqlParameter("@sPrsntAddress", trailer.sPrsntAddress);
                param[52] = new SqlParameter("@iFk_LnkVehNo", trailer.iFk_LnkVehNo);
                param[53] = new SqlParameter("@sMobileNo", trailer.sMobileNo);
                param[54] = new SqlParameter("@sEmailId", trailer.sEmailId);
                param[55] = new SqlParameter("@iFk_VehMaker", trailer.iFk_VehMaker);
                param[56] = new SqlParameter("@sRegnNo", trailer.sRegnNo);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_TRLRMST_Update]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = ds.Tables[0].Rows[0]["Message"].ToString();
                    api_Response.statusCode = Convert.ToInt32(ds.Tables[0].Rows[0]["StatusCode"]);
                    api_Response.responseCode = 1;
                }
                else
                {
                    api_Response.message = "No Data Available";
                    api_Response.statusCode = 400;
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

        [HttpGet]
        [Route("GetProdDetListForEdit")]
        public Api_CommonResponse GetProdDetListForEdit(int id)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@id", id);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_TRLRDET_GetDetailsForEdit]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = "Advance details list";
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
        [Route("ProductDetailsUpdate")]
        public Api_CommonResponse ProductDetailsUpdate(TrailerProductDetails det)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@iPk_TrlrDetId", det.iPk_TrlrDetId);
                param[1] = new SqlParameter("@iFK_TrlrMstId", det.iFK_TrlrMstId);
                param[2] = new SqlParameter("@iFk_ProductId", det.iFk_ProductId);
                param[3] = new SqlParameter("@ProductCpcty", det.ProductCpcty);

                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_MASTER_TRLRDET_Update]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.message = ds.Tables[0].Rows[0]["Message"].ToString();
                    api_Response.statusCode = Convert.ToInt32(ds.Tables[0].Rows[0]["StatusCode"]);
                    api_Response.responseCode = 1;
                }
                else
                {
                    api_Response.message = "No Data Available";
                    api_Response.statusCode = 400;
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
    }
}
