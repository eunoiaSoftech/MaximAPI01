using System;

namespace Eunoia_UM_API.Model
{
    public class SupplierModel
    {
        public int iPanStatus { get; set; }
        public int iPk_PartyId { get; set; }
        public int iFk_PtrlUndr { get; set; }
        public string? sPtrlUnder { get; set; }
        public string? sPanHolder { get; set; }
        public string? sPartyName { get; set; }
        public string? dtEffctvDate { get; set; }
        public string? sLongitude { get; set; }
        public string? sLatitude { get; set; }
        public int? iFk_ReferredBy { get; set; }
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
        public bool? bIsActive { get; set; }
        public string? dtInsertDate { get; set; }
        //---- Added 23/02/2024 
        public string? sName { get; set; }
        public string? Group { get; set; }
        public string? sReason { get; set; }
        public int? bInActive { get; set; }
        public string? MSMERegNo { get; set; }
        public string? NatureofPayment { get; set; }
        public string? DeducteeType { get; set; }
        public int iGrpLdgrId { get; set; }
        public int iIsLedgrTag { get; set; }
        public int iIsTdsApcbl { get; set; }
        public int iFk_NtPymtId { get; set; }
        public int iFk_DeductTypId { get; set; }
        public List<SupplierContactDtls>? ContactDtls { get; set; }
        public List<Brnchdtls>? Brnchdtls { get; set; }
        public List<TDSList>? TDSList { get; set; }
        public SupplierAddressDetails? AddressDetails { get; set; }

    }
    public class Brnchdtls
    {
        //---- Added on 22/02/2024

        public int iBrnchmMstId { get; set; }
        public string? sBranchNme { get; set; }
        public int iFk_BranchId { get; set; }
        public string? sFk_BrnchId { get; set; }
    }
    public class SupplierContactDtls
    {

        public int? CntctDtlsId { get; set; }
        public int? CmpnyMstId { get; set; }
        public string? CntctPerson { get; set; }
        public string? CntctNumber { get; set; }
        public string? EmailId { get; set; }
        public bool? Primary { get; set; }
    }

    public class SupplierBankDetails
    {
        public int PK_PartyBankId { get; set; }
        public int? FK_PartyID { get; set; }
        public int? FK_AccountType { get; set; }
        public string? AccountNo { get; set; }
        public string? BankName { get; set; }
        public string? BranchName { get; set; }
        public string? BankAddress { get; set; }
        public string? SwiftNo { get; set; }
        public string? IFSCNo { get; set; }
        public bool? IsActive { get; set; }
        public string? sOtherBankName { get; set; }
        public bool? IsPrimary { get; set; }
        public string? sGpayID { get; set; }
        public string? sUPIID { get; set; }
    }
    public class AdvDetials
    {
        public int iPK_AdvDetailsId { get; set; }
        public int FK_PartyID { get; set; }
        public DateTime? dtCreatedOn { get; set; }
        public string? sCreatedBy { get; set; }
        public int? iFk_FinancialYrId { get; set; }
        public int? iFk_BranchId { get; set; }
        public int? iAdvTruck { get; set; }
        public int? iAdvTrailer { get; set; }
        public int? iCreditPeriod { get; set; }
        public DateTime? dtEffectiveDate { get; set; }

        public bool? bIsActive { get; set; }
    }

    public class SupplierBillingDetails
    {
        public int PK_PartyDespatchDetails { get; set; }
        public int? FK_PartyID { get; set; }
        public int? FK_DespatchType { get; set; }
        public string? ContactPersone { get; set; }
        public string? StreetLane { get; set; }
        public int? FK_CountryID { get; set; }
        public int? FK_StateID { get; set; }
        public int? FK_CityID { get; set; }
        public string? FK_AreaID { get; set; }
        public int? PinCode { get; set; }
        public string? TelephoneNo { get; set; }
        public string? EmailID { get; set; }
        public int? FK_RegistrationID { get; set; }
        public int? FK_GstParty_TypeID { get; set; }
        public string? GstIdentificationNo { get; set; }
        public DateTime? GST_effdt { get; set; }
    }


    public class SupplierAddressDetails
    {
        public int AddrssDtlsId { get; set; }
        public int CmpnyMstId { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Pincode { get; set; }
        public string? Address { get; set; }
    }

}