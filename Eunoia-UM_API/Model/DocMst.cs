namespace Eunoia_UM_API.Model
{
    public class DocMst
    {
        public int? iPk_VehDcmntId { get; set; }
        public int? iFk_DcmntId { get; set; }
        public int? IsExpired { get; set; }
        public int? iNoOfDays { get; set; }
        public string? dtCrtDate { get; set; }
        public bool? IsActive { get; set; }
        public int? iFk_FinYear { get; set; }
        public string? iFk_Createdby { get; set; }
        public string? iFk_ApprovedBy { get; set; }
    }
    public class DocRenewal
    {
        public int? iPk_DcmntRnwlId { get; set; }
        public int? iFk_DocumentType { get; set; }
        public int? iFk_VehicleId { get; set; }
        public int? iFK_AgentId { get; set; }
        public string? sDocNumber { get; set; }
        public decimal? dAmount { get; set; }
        public string? dtRenewalDate { get; set; }
        public string? dtExpiryDate { get; set; }
        public string? sAttchfile { get; set; }
        public string? iFk_CreatedBy { get; set; }
        public string? iFk_CheckedBy { get; set; }
        public string? iFk_ApprovedBy { get; set; }
        public int? iFk_YearId { get; set; }
        public int? iFk_BranchId { get; set; }
        public string? sIpAddress { get; set; }
        public string? sLongitude { get; set; }
        public string? sLatitude { get; set; }
        public string? sBrowser { get; set; }
        public string? sDevice { get; set; }
        public int? iIsDeleted { get; set; }
    }
}
