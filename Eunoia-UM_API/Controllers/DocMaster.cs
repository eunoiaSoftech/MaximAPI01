using Eunoia_UM_API.Helper;
using Microsoft.AspNetCore.Mvc;
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
    public class DocMaster : Controller
    {
        Api_CommonResponse res = new Api_CommonResponse();

        [HttpPost]
        [Route("DocumentSave")]
        public Api_CommonResponse DocumentSave(DocumentMasterModel DocConfig)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@iFK_EnumNo",  DocConfig.iFK_EnumNo);
                param[1] = new SqlParameter("@sName",  DocConfig.sName);
                param[2] = new SqlParameter("@idays",  DocConfig.idays);
                param[3] = new SqlParameter("@iIsActv",  DocConfig.iIsActv);
                param[4] = new SqlParameter("@bActive", DocConfig.bActive);
                DataSet ds = DBOperation.FillDataSet("DOCMASTER", param);
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
        [Route("GetDocumentMaster")]
        public Api_CommonResponse GetDocumentMaster()
        {

            try
            {
                DataSet ds = DBOperation.FillDataSet("[dbo].[Usp_Master_Document_Master_List]");
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    res.message = "Doc List";
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
            }
            return res;
        }

        [HttpGet]
        [Route("GetDocumentList")]
        public Api_CommonResponse GetDocumentList()
        {

            try
            {
                DataSet ds = DBOperation.FillDataSet("USP_GetDocList");
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    res.message = "Doc List";
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
            }
            return res;
        }

        [HttpPost]
        [Route("DeleteDOCItem")]
        public Api_CommonResponse DeleteDOCItem(DocumentMasterModel DocConfig)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@iFK_EnumNo", DocConfig.iFK_EnumNo);
               
                DataSet ds = DBOperation.FillDataSet("USP_DeleteDocItem", param);
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
    }
}
