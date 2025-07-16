namespace Eunoia_UM_API.Model
{
    public class RouteMt
    {
        public int iPk_RouteID { get; set; }
        public string sRouteCode { get; set; }
        public string sRouteName { get; set; }
        public DateTime dtCreationDate { get; set; }
        public int iFk_FrmStnId { get; set; }
        public int iFk_ToStnId { get; set; }
        public string sShortRoute { get; set; }

        public decimal dTotalKMs { get; set; }
        public int iFk_PreparedBy { get; set; }
        public int iFk_CheckedBy { get; set; }
        public int iMaxID { get; set; }
        public int iVoucherStyle { get; set; }
        public int iFk_BranchID { get; set; }
        public int iFk_YearID { get; set; }
        public int iFk_Approvedby { get; set; }
        public decimal iTransitTime { get; set; }
    }
}
