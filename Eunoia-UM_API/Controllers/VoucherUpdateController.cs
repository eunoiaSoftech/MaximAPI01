using Eunoia_UM_API.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using Eunoia_UM_API.Model;
using System.Net;

namespace Eunoia_UM_API.Controllers
{
    public class VoucherUpdateController : Controller
    {
        Api_CommonResponse response = new Api_CommonResponse();

        [HttpPost]
        [Route("api/SaveDeliveryDetails")]
        public Api_CommonResponse SaveDeliveryDetails([FromBody] VoucherUpdateModel voucherUpdateModel)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[15];
                param[0] = new SqlParameter("@iDeliveryNo", voucherUpdateModel.iDeliveryNo ?? (object)DBNull.Value);
                param[1] = new SqlParameter("@sConsignmntNo", voucherUpdateModel.sConsignmntNo ?? (object)DBNull.Value);
                param[2] = new SqlParameter("@iOrderTyp", voucherUpdateModel.iOrderTyp ?? (object)DBNull.Value);
                param[3] = new SqlParameter("@dtDoDate", voucherUpdateModel.dtDoDate ?? (object)DBNull.Value);
                param[4] = new SqlParameter("@sNewConsignmntNo", (object?)voucherUpdateModel.sNewConsignmntNo ?? DBNull.Value);
                param[5] = new SqlParameter("@dtConsignmntDt", voucherUpdateModel.dtConsignmntDt ?? (object)DBNull.Value);
                param[6] = new SqlParameter("@dGrossWt", voucherUpdateModel.dGrossWt ?? (object)DBNull.Value);
                param[7] = new SqlParameter("@dNetWt", voucherUpdateModel.dNetWt ?? (object)DBNull.Value);
                param[8] = new SqlParameter("@dInvoicWt", voucherUpdateModel.dInvoicWt ?? (object)DBNull.Value);
                param[9] = new SqlParameter("@sInvoiceNo", (object?)voucherUpdateModel.sInvoiceNo ?? DBNull.Value);
                param[10] = new SqlParameter("@dtInvoicDt", voucherUpdateModel.dtInvoicDt ?? (object)DBNull.Value);
                param[11] = new SqlParameter("@iShortg", voucherUpdateModel.iShortg ?? (object)DBNull.Value);
                param[12] = new SqlParameter("@sCityName", voucherUpdateModel.cityName ?? (object)DBNull.Value);
                param[13] = new SqlParameter("@sCatName", voucherUpdateModel.catName ?? (object)DBNull.Value);
                param[14] = new SqlParameter("@sproductName", voucherUpdateModel.productName ?? (object)DBNull.Value);

                DataSet ds = DBOperation.FillDataSet("USP_UpdateDeliveryDetails", param);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    response.responseCode = 0;
                    response.message = ds.Tables[0].Rows[0]["Message"].ToString();
                    response.statusCode = (int)HttpStatusCode.OK;
                }
                else
                {
                    response.responseCode = 1;
                    response.message = $"We are facing server issue at the moment please try again...";
                    response.statusCode = -1;
                    response.data = null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }
    }
}
    