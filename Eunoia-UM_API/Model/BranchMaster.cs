namespace Eunoia_UM_API.Model
{
    public class BranchMaster
    {
        public int CmpnyMstId { get; set; }


        public int iBrnchmMstId { get; set; }
        public int FinancialYear { get; set; }
        public string? Logo { get; set; }
        public string? BranchName { get; set; }
        public string? RegisteredOn { get; set; }
        public string? CINNumber { get; set; }
        public string? PanCardNumber { get; set; }
        public string? WebsiteURL { get; set; }
        public string? EmailURL { get; set; }
        public string? CompanyName { get; set; }

        public int UserId { get; set; }
        public string? RegistraionNo { get; set; }
        public bool bIsActive { get; set; }
        public string? RegisteredDoc { get; set; }
        public string? RegisteredDocumentName { get; set; }
        public List<BranchContactDtls>? ContactDtls { get; set; }
        public BranchAddressDtls? AddressDtls { get; set; }

        public List<BranchLocationDtls>? LocationDtls { get; set; }
    }

    public class BranchContactDtls
    {
        public int? CntctDtlsId { get; set; }
        public int? CmpnyMstId { get; set; }
        public string? CntctPerson { get; set; }
        public string? CntctNumber { get; set; }
        public string? EmailId { get; set; }
        public bool Primary { get; set; }
    }
    public class BranchAddressDtls
    {
        public int AddrssDtlsId { get; set; }
        public int CmpnyMstId { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Pincode { get; set; }
        public string? Address { get; set; }
        public string? GSTNO { get; set; }
    }

    public class BranchLocationDtls
    {
        public int? LocationId { get; set; }
    }

}
