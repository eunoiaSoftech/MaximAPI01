namespace Eunoia_UM.Models
{
    public class VehicleInspectionModel
    {
        public int iPk_InspectionId { get; set; }
        public int? iFk_TrailerId { get; set; }
        public int? iFk_DriverId { get; set; }
        public int? iMaxNo { get; set; }
        public int? iVoucherStyle { get; set; }
        public int? iFk_VehicleNo { get; set; }
        public int? iFk_YearId { get; set; }
        public int? iFk_BrnchId { get; set; }
        public int? iCrtdBy { get; set; }
        public bool? bIsInspectDone { get; set; }
        public string? sEntryNo { get; set; }
        public string? dtEntryDate { get; set; }
        public string? dtCrtdOn { get; set; }
        public string? sIpAddress { get; set; }
        public string? sBrowsername { get; set; }
        public string? sLongitude { get; set; }
        public string? sLatitude { get; set; }
        public int? iFk_VehicelId { get; set; }
        public List<VehicleInspectionToolDetails>? tools { get; set; }
    }

    public class VehicleInspectionToolDetails
    {
        public int iPk_InspectToolId { get; set; }
        public int? iFk_InspectionId { get; set; }
        public int? iFk_ToolId { get; set; }
        public decimal? dAvailblQty { get; set; }
        public bool? bIsOk { get; set; }
        public bool? bIsNotOk { get; set; }
        public string? sRemark { get; set; }
        public string? sAttachmnt { get; set; }
    }

    public class VehicleInspectionDetails
    {
        public int iPk_VehCndId { get; set; }
        public int? iFk_InspectId { get; set; }
        public int? iFk_VehPartId { get; set; }
        public int? iLoadingId { get; set; }
        public bool? bIsOk { get; set; }
        public bool? bIsNotOk { get; set; }
        public string? sRemark { get; set; }
        public string? sAttachmnt { get; set; }

        // required for getting the proper filename
        public string? sVehiclPartNme { get; set; }
        public string? sAttachmntExt { get; set; }
    }

}