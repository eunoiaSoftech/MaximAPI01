namespace Eunoia_UM_API.Model
{
    public class LRDetails
    {
        public int iPk_LrId { get; set; }
        public string sEntryNo { get; set; }
        public DateTime dtEntryDate { get; set; }
        public int iFk_Customerid { get; set; }
        public string sTokanNo { get; set; }
        public int? iFk_VehicleId { get; set; }
        public string sEayBillNo { get; set; }
        public DateTime? dtEwayBildt { get; set; }
        public DateTime? dtEwayBillExpdt { get; set; }
        public int? iFk_RouteId { get; set; }
        public int? iFk_ConsigneeId { get; set; }
        public int? iFk_AdvanceMode { get; set; }
        public int? iFk_DriverId { get; set; }
        public int? iFk_TrailerId { get; set; }
        public int? iMaxNo { get; set; }
        public int? iFk_BranchId { get; set; }
        public int? iFk_YearID { get; set; }
        public int? iVoucherStyle { get; set; }
        public int? iType { get; set; }
        public bool? bIsReceipt { get; set; }
        public bool? bIsPosting { get; set; }
        public DateTime? dtAdvanceDt { get; set; }
    }
}
