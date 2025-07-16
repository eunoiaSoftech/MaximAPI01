namespace Eunoia_UM.Models
{
    public class DriverBlacklistModel
    {
        public int iPk_BlkLstId { get; set; }
        public string? sEntryNo { get; set; }
        public string? dtEntryDt { get; set; }
        public int? iFk_DrvId { get; set; }
        public int? iFK_ReasonId { get; set; }
        public string? sReasonDesc { get; set; }
        public bool bIsCancel { get; set; }
        public string? dtCancelDt { get; set; }
        public string? sCancelReasn { get; set; }
        public string? dtCrtdOn { get; set; }
        public int? iCrtdBy { get; set; }
        public string? sIpAddress { get; set; }
        public string? sLongitude { get; set; }
        public string? sLattitude { get; set; }
        public int? iFk_BrnchId { get; set; }
        public int? iFk_YearId { get; set; }
        public int? iMaxNo { get; set; }
        public int? iVoucherStyle { get; set; }
        public string? sBrowserNme { get; set; }
    }

    public class DriverDetailsForBlacklist
    {
        public int? id { get; set; }
        public string? dName { get; set; }
        public string? licenseNo { get; set; }
        public string? licenseRenewal { get; set; }
        public string? sDrvCode { get; set; }
    }

}