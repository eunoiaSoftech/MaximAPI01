namespace Eunoia_UM_API.Model
{
    public class CustomerModel

    {

        public int? iPanStatus { get; set; }
        public int? iPk_PartyId { get; set; }
        public int? iFk_ReferredBy { get; set; }
        public string? sPanHolder { get; set; }
        public string? sPartyName { get; set; }
        public int? iStatus { get; set; }
        public int? iFk_Category { get; set; }
        public int? iFk_IndustryType { get; set; }

        public int? iFk_Type { get; set; }
        public string? sPANNo { get; set; }
        public string? sWebsite { get; set; }
        public string? sEmailID { get; set; }
        public int? iFk_PreparedBy { get; set; }

        public int? iFk_ApprovedBy { get; set; }
        public string? sCategory { get; set; }

        public int? iMAXID { get; set; }

        public int? iFk_CompanyID { get; set; }

        public int? iFk_BranchID { get; set; }
        public int? iFk_YearId { get; set; }
        public int? ifk_Userid { get; set; }

        public int? iIsActive { get; set; }
        public string? dtInsertDate { get; set; }

        //---- Added 23/02/2024 
        public string? sName { get; set; }
        public string? MSMERegNo { get; set; }
        public string? Group { get; set; }
        public string? NatureofPayment { get; set; }
        public string? DeducteeType { get; set; }
        public string? dtEffctvDate { get; set; }
        public string? sReason { get; set; }
        public int? bInActive { get; set; }
        public int iGrpLdgrId { get; set; }
        public int iIsLedgrTag { get; set; }
        public int iIsTdsApcbl { get; set; }
        public int iFk_NtPymtId { get; set; }
        public int iFk_DeductTypId { get; set; }



        public List<ContactDetails>? ContactDetails { get; set; }
        public List<Brnchdtls1>? Brnchdtls1 { get; set; }
        public List<TDSList>? TDSList { get; set; }


    }
    public class Brnchdtls1
    {
        //---- Added on 22/02/2024

        public int iBrnchmMstId { get; set; }
        public string? sBranchNme { get; set; }
        public int iFk_BranchId { get; set; }
        public string? sFk_BrnchId { get; set; }
    }
    public class ContactDetails
    {
        public int CntctDtlsId { get; set; }
        public int? CustMstId { get; set; }
        public string? CntctPerson { get; set; }
        public string? CntctNumber { get; set; }
        public string? EmailId { get; set; }
        public bool Primary { get; set; }
    }



}

