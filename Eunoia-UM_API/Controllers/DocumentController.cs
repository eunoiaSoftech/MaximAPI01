using Eunoia_UM_API.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using Eunoia_UM_API.Model;

namespace Eunoia_UM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        Api_CommonResponse api_Response = new Api_CommonResponse();
        [HttpPost]
        [Route("UpdateDocumentDetails")]
        public Api_CommonResponse UpdateDocumentDetails(DocMst Doc)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@iFk_DcmntId", Doc.iFk_DcmntId);
                param[1] = new SqlParameter("@IsExpired", Doc.IsExpired);
                param[2] = new SqlParameter("@iNoOfDays", Doc.iNoOfDays);
                param[3] = new SqlParameter("@Flag", "2");
                param[4] = new SqlParameter("@iPk_VehDcmntId", Doc.iPk_VehDcmntId);

                DataSet ds = DBOperation.FillDataSet("[dbo].[DocMstAction]", param);
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
        [Route("GetDocumentForEdit")]
        public Api_CommonResponse GetDocumentForEdit(int ID = 0)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@iPk_VehDcmntId", ID);
                param[1] = new SqlParameter("@Flag", "3");
                DataSet ds = DBOperation.FillDataSet("[dbo].[DocMstAction]", param);

                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.statusCode = 1;
                    api_Response.responseCode = 0;
                    api_Response.message = "Data Fetched Successfully...";
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
        [Route("SaveDoc")]
        public Api_CommonResponse SaveDoc(DocMst Doc)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@iFk_DcmntId", Doc.iFk_DcmntId);
                param[1] = new SqlParameter("@IsExpired", Doc.IsExpired);
                param[2] = new SqlParameter("@iNoOfDays", Doc.iNoOfDays);
                param[3] = new SqlParameter("@dtCrtDate", Doc.dtCrtDate);
                param[4] = new SqlParameter("@IsActive", Doc.IsActive);
                param[5] = new SqlParameter("@iFk_Createdby", Doc.iFk_Createdby);
                param[6] = new SqlParameter("@iFk_ApprovedBy", Doc.iFk_ApprovedBy);

                DataSet ds = DBOperation.FillDataSet("[dbo].[DocMstAction]", param);
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
        [HttpPost]
        [Route("DeleteDoc")]
        public Api_CommonResponse DeleteDoc(int Id)
        {
            int Master = 0;
            try
            {

                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@Flag", "4");
                param[1] = new SqlParameter("@iPk_VehDcmntId", Id);
                DataTable DT = DBOperation.FillDataTable("[dbo].[DocMstAction]", param);
                if (DT != null)
                {
                    if (DT.Rows.Count > 0)
                    {
                        Master = Convert.ToInt32(DT.Rows[0]["StatusCode"]);
                    }
                    api_Response.message = "Delete Custom Enum";
                    api_Response.status = 200;
                    api_Response.userID = Master.ToString();
                    api_Response.responseCode = 1;
                }
            }
            catch (Exception e)
            {
                api_Response.message = "Delete Card";
                api_Response.status = 400;
                api_Response.userID = "0";
                api_Response.responseCode = 0;
            }
            return api_Response;
        }
        [HttpPost]
        [Route("DeleteDocumentRenewal")]
        public Api_CommonResponse DeleteDocumentRenewal(int Id)
        {
            int Master = 0;
            try
            {

                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@Flag", "3");
                param[1] = new SqlParameter("@iPk_DcmntRnwlId", Id);
                DataTable DT = DBOperation.FillDataTable("[dbo].[DocRenewalAction]", param);
                if (DT != null)
                {
                    if (DT.Rows.Count > 0)
                    {
                        Master = Convert.ToInt32(DT.Rows[0]["StatusCode"]);
                    }
                    api_Response.message = "Delete Custom Enum";
                    api_Response.status = 200;
                    api_Response.userID = Master.ToString();
                    api_Response.responseCode = 1;
                }
            }
            catch (Exception e)
            {
                api_Response.message = "Delete Card";
                api_Response.status = 400;
                api_Response.userID = "0";
                api_Response.responseCode = 0;
            }
            return api_Response;
        }

        [HttpGet]
        [Route("GetDocumentMst")]
        public Api_CommonResponse GetDocumentMst()
        {

            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Flag", "5");
                DataTable ds = DBOperation.FillDataTable("[dbo].[DocMstAction]", param);
                if (ds != null && ds != null && ds.Rows.Count > 0)
                {
                    api_Response.message = "Company List";
                    api_Response.statusCode = 1;
                    api_Response.data = JsonConvert.SerializeObject(ds);
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
        [Route("GetDocRenewal")]
        public Api_CommonResponse GetDocRenewal()
        {

            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Flag", "2");
                DataTable ds = DBOperation.FillDataTable("[dbo].[DocRenewalAction]", param);
                if (ds != null && ds != null && ds.Rows.Count > 0)
                {
                    api_Response.message = "Company List";
                    api_Response.statusCode = 1;
                    api_Response.data = JsonConvert.SerializeObject(ds);
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
        [Route("GetDropDown")]
        [HttpGet]
        public Api_CommonResponse GetDropDown(string type)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@type", type);
                param[1] = new SqlParameter("@Flag", "2");
                DataSet ds = DBOperation.FillDataSet("[dbo].[GetddlforCardRC]", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    api_Response.statusCode = 1;
                    api_Response.responseCode = 0;
                    api_Response.message = "Data Fetched Successfully...";
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
        [Route("SaveDocRenewal")]
        public Api_CommonResponse SaveDocRenewal(List<DocRenewal> doc)
        {
            try
            {
                DataSet ds = new DataSet();
                foreach (var rc in doc)
                {
                    SqlParameter[] param = new SqlParameter[20];
                    param[0] = new SqlParameter("@iFk_DocumentType ",rc.iFk_DocumentType   );
                    param[1] = new SqlParameter("@Flag ","1"   );
                    param[2] = new SqlParameter("@iFk_VehicleId    ",rc.iFk_VehicleId   );
                    param[3] = new SqlParameter("@iFK_AgentId      ",rc.iFK_AgentId   );
                    param[4] = new SqlParameter("@sDocNumber       ",rc.sDocNumber   );
                    param[5] = new SqlParameter("@dAmount          ",rc.dAmount   );
                    param[6] = new SqlParameter("@dtRenewalDate    ",rc.dtRenewalDate   );
                    param[7] = new SqlParameter("@dtExpiryDate     ",rc.dtExpiryDate   );
                    param[8] = new SqlParameter("@sAttchfile       ",rc.sAttchfile   );
                    param[9] = new SqlParameter("@iFk_CreatedBy    ",rc.iFk_CreatedBy   );
                    param[10] = new SqlParameter("@iFk_CheckedBy    ",rc.iFk_CheckedBy   );
                    param[11] = new SqlParameter("@iFk_ApprovedBy   ",rc.iFk_ApprovedBy   );
                    param[12] = new SqlParameter("@iFk_YearId       ",rc.iFk_YearId   );
                    param[13] = new SqlParameter("@iFk_BranchId     ",rc.iFk_BranchId   );
                    param[14] = new SqlParameter("@sIpAddress       ",rc.sIpAddress   );
                    param[15] = new SqlParameter("@sLongitude       ",rc.sLongitude  );
                    param[16] = new SqlParameter("@sLatitude        ",rc.sLatitude   );
                    param[17] = new SqlParameter("@sBrowser         ",rc.sBrowser   );
                    param[18] = new SqlParameter("@sDevice          ",rc.sDevice   );
                    param[19] = new SqlParameter("@iIsDeleted        ", rc.iIsDeleted   );

                ds = DBOperation.FillDataSet("[dbo].[DocRenewalAction]", param);
                }

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
