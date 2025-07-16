using Eunoia_UM_API.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Eunoia_UM_API.Model;
using Newtonsoft.Json;

namespace Eunoia_UM_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FinancialYear : Controller
    {

        Api_CommonResponse res = new Api_CommonResponse();
         
        [HttpPost]
        [Route("FinacialYearSave")]
        public Api_CommonResponse FinacialYearSave(MFinancialYear   FinYearModel)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@iPk_FinId", FinYearModel.iPk_FinId);
                param[1] = new SqlParameter("@dtStartDate", FinYearModel.dtStartDate);
                param[2] = new SqlParameter("@dtEndDate", FinYearModel.dtEndDate);               
                param[3] = new SqlParameter("@bIsActive", FinYearModel.bIsActive);
                param[4] = new SqlParameter("@iFk_BranchId", FinYearModel.iFk_BranchId);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Master_FinancialYear_Save]", param);
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
            catch (Exception ex)
            {
                res.statusCode = 402;
            }
            return res;
        }


        [HttpPost]
        [Route("FinacialYearUpdate")]
        public Api_CommonResponse FinacialYearUpdate(MFinancialYear FinYearModel)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@iPk_FinId", FinYearModel.iPk_FinId);
                param[1] = new SqlParameter("@dtStartDate", FinYearModel.dtStartDate);
                param[2] = new SqlParameter("@dtEndDate", FinYearModel.dtEndDate);
                param[3] = new SqlParameter("@bIsActive", FinYearModel.bIsActive);
                param[4] = new SqlParameter("@iFk_BranchId", FinYearModel.iFk_BranchId);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_Admin_FinancialYear_Update]", param);
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
            catch (Exception ex)
            {
                res.statusCode = 402;
            }
            return res;
        }



        [HttpGet]
        [Route("GetFinancialYear")]
        public Api_CommonResponse GetFinancialYear()
        {
            Api_CommonResponse response = new Api_CommonResponse();
            try
            {
               
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_FinancialYear_List]");
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    var data = ds.Tables[0];
                   
                    response.data = JsonConvert.SerializeObject(data);
                    response.statusCode = 200;
                }
            }
            catch (Exception ex)
            {
                response.statusCode = 402;
            }
            return response;
        }

        [HttpGet]
        [Route("GetFinancialYearEdit")]
        public Api_CommonResponse GetFinancialYearEdit( int iPk_FinYrId)
        {
            Api_CommonResponse response = new Api_CommonResponse();
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@iPk_FinYrId", iPk_FinYrId);
                DataSet ds = DBOperation.FillDataSet("[dbo].[USP_FinancialYear_List]",param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    var data = ds.Tables[0];

                    response.data = JsonConvert.SerializeObject(data);
                    response.statusCode = 200;
                }
            }
            catch (Exception ex)
            {
                response.statusCode = 402;
            }
            return response;
        }




    }
}
