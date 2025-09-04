namespace Eunoia_UM_API.Model
{
    public class VoucherUpdateModel
    {
        public string iDeliveryNo { get; set; }        // Delivery No
        public string sConsignmntNo { get; set; }      // Existing Consignment No (hidden/old)
        public int? iOrderTyp { get; set; }            // Order Type
        public DateTime? dtDoDate { get; set; }        // Delivery Date
        public string sNewConsignmntNo { get; set; }   // New CNote No
        public DateTime? dtConsignmntDt { get; set; }  // New CNote Date
        public decimal? dGrossWt { get; set; }         // Gross Weight
        public decimal? dNetWt { get; set; }           // Net Weight
        public decimal? dInvoicWt { get; set; }        // Invoice Weight
        public string sInvoiceNo { get; set; }         // Invoice No
        public DateTime? dtInvoicDt { get; set; }      // Invoice Date
        public decimal? iShortg { get; set; }          // Shortage
        public string? cityName { get; set; }      // Destination
        public string? catName { get; set; }     // Category
        public string? productName { get; set; }
    }
}
