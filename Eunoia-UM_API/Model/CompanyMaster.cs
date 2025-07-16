namespace Eunoia_UM_API.Model
{
    public class CompanyMaster
    {
        public int CmpnyMstId { get; set; }
        public string? Logo { get; set; }
        public string? CompanyName { get; set; }
        public string? RegisteredOn { get; set; }
        public string? RegisteredDocumentName { get; set; }
        public string? RegisteredDoc { get; set; }
        public string? CINNumber { get; set; }
        public string? GSTNumber { get; set; }
        public string? PanCardNumber { get; set; }
        public string? WebsiteURL { get; set; }
        public string? EmailURL { get; set; }
        public bool? bIsActive { get; set; }
        //---- Added 23/02/2024 
        public string? sName { get; set; }
        public int iGrpLdgrId { get; set; }
        public int iIsLedgrTag { get; set; }
        public int iIsTdsApcbl { get; set; }
        public int iFk_NtPymtId { get; set; }
        public int iFk_DeductTypId { get; set; }
        public string? CmpnyDesc { get; set; }
        public List<ContactDtls>? ContactDtls { get; set; }
        public List<Brnchdtls2>? Brnchdtls2 { get; set; }

        public AddressDtls? AddressDtls { get; set; }

        public int FinancialYear { get; set; }
    }
    public class ContactDtls
    {
        public int? CntctDtlsId { get; set; }
        public int? CmpnyMstId { get; set; }
        public string? CntctPerson { get; set; }
        public string? CntctNumber { get; set; }
        public string? EmailId { get; set; }
        public bool? Primary { get; set; }
    }
    public class AddressDtls
    {
        public int? AddrssDtlsId { get; set; }
        public int CmpnyMstId { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Pincode { get; set; }
        public string? Address { get; set; }
    }
}
