namespace Eunoia_UM.Models
{
    public class TrVehiclePlacementModel
    {
        public int iPk_PlctoolId { get; set; }
        public int iFk_VehplcId { get; set; }
        public int iFk_CustEnum { get; set; }
        public int iFk_ToolId { get; set; }
        public decimal dPresentQty { get; set; }
        public decimal dGivenQty { get; set; }
        public int iIsReturn { get; set; }
        public string sRemark { get; set; }
        public string sToolNme { get; set; }
    }

    public class VehiclePlacementModel
    {
        public int iPk_VehplcId { get; set; }
        public int iFk_TrailerId { get; set; }
        public int iFk_DriverId { get; set; }
        public int iFk_BrnchId { get; set; }
        public int iFk_YearId { get; set; }
        public int iCrtdBy { get; set; }
        public int iStatus { get; set; }
        public int iMaxNo { get; set; }
        public int iVoucherStyle { get; set; }
        public int iFk_VehclId { get; set; }
        public int iMovemntTyp { get; set; }
        public int iFk_FrmStationId { get; set; }
        public int iFk_ToStationId { get; set; }
        public int iFk_RoutId { get; set; }
        public int? iVehicleType { get; set; }
        public decimal dDistance { get; set; }
        public decimal dTransitTime { get; set; }
        public decimal? dMinGuarantee { get; set; }
        public decimal? dBookingRate { get; set; }
        public string? sEntryNo { get; set; }
        public string? dtEntryDate { get; set; }
        public string? dtReleaseDt { get; set; }
        public string? dtCrdtOn { get; set; }
        public string? sIpAddress { get; set; }
        public string? sBrowsername { get; set; }
        public string? sLongitude { get; set; }
        public string? sLatitude { get; set; }
        public int? Pk_MaxTrpDetId { get; set; }
        public int? iIsEmptyRtrn { get; set; }
        public int? iPlacemntTyp { get; set; }
        public int? iFk_CstmrId { get; set; }
        public List<VehiclePlacementTools>? tools { get; set; }
    }

    public class VehiclePlacementTools
    {
        public int iPk_PlctoolId { get; set; }
        public int iFk_VehplcId { get; set; }
        public int iFk_CustEnum { get; set; }
        public int iFk_ToolId { get; set; }
        public decimal dPresentQty { get; set; }
        public decimal dGivenQty { get; set; }
        public int iIsReturn { get; set; }
        public string? sRemark { get; set; }
    }

    public class RouteCityForPlacement
    {
        public int? iPk_RouteID { get; set; }
        public string? sRouteName { get; set; }
        public int? iFk_FrmStnId { get; set; }
        public int? iFk_ToStnId { get; set; }
        public decimal? dTotalKMs { get; set; }
        public string? fromcity { get; set; }
        public string? tocity { get; set; }
    }

    public class DocRenewalForPaperPermit
    {
        public int iPk_DcmntRnwlId { get; set; }
        public int? iFk_DocumentType { get; set; }
        public int? iFk_VehicleId { get; set; }
        public int? iFK_AgentId { get; set; }
        public int? iFk_CreatedBy { get; set; }
        public int? iFk_CheckedBy { get; set; }
        public int? iFk_ApprovedBy { get; set; }
        public int? iFk_YearId { get; set; }
        public int? iFk_BranchId { get; set; }
        public int? iIsDeleted { get; set; }
        public decimal? dAmount { get; set; }
        public string? dtRenewalDate { get; set; }
        public string? dtExpiryDate { get; set; }
        public string? sAttchfile { get; set; }
        public string? sDocNumber { get; set; }
        public string? sIpAddress { get; set; }
        public string? sLongitude { get; set; }
        public string? sLatitude { get; set; }
        public string? sBrowser { get; set; }
        public string? sDevice { get; set; }
        public string? docName { get; set; }
    }

    public class DoModel
    {
        public int? iOrderMaterialId { get; set; }
        public int? cstmrId { get; set; }
        public int? cntrctId { get; set; }
        public decimal? dBalanceQuantity { get; set; }
        public string? sItemName { get; set; }
        public string? sDesc { get; set; }
        public decimal? dQty { get; set; }
    }

}
