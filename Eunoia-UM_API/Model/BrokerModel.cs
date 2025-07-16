using Newtonsoft.Json;

namespace Eunoia_UM_API.Model
{
    public class BrokerModel
    {
        public int iPanStatus { get; set; }
        public int iPk_PartyId { get; set; }
        public int? userId { get; set; }
        public int? iPK_USRID { get; set; }
        public string? sPanAttach { get; set; }
        public string? sAaddharAttach { get; set; }
        public string? sUSRNME { get; set; }
        public string? sPanHolder { get; set; }
        public string? sPartyName { get; set; }
        public int? iFk_ReferredBy { get; set; }
        public int? iStatus { get; set; }
        public int? iFk_Category { get; set; }
        public int? iFk_IndustryType { get; set; }
        public int? iFk_Type { get; set; }
        public string? sPANNo { get; set; }
        public string? sReason { get; set; }
        public string? sWebsite { get; set; }
        public string? sEmailID { get; set; }
        public int? iFk_PreparedBy { get; set; }
        public int? bInActive { get; set; }
        public int? iFk_ApprovedBy { get; set; }
        public string? sCategory { get; set; }
        public int? iMAXID { get; set; }
        public int? iFk_CompanyID { get; set; }
        public int? iFk_BranchID { get; set; }
        public int? iFk_YearId { get; set; }
        public int? ifk_Userid { get; set; }
        public int bIsActive { get; set; }
        public string? dtInsertDate { get; set; }

        //---- Added 23/02/2024 
        public string? sName { get; set; }
        public string? Group { get; set; }
        public string? NatureofPayment { get; set; }
        public string? DeducteeType { get; set; }
        public string? dtEffctvDate { get; set; }
        public int iGrpLdgrId { get; set; }
        public int iIsLedgrTag { get; set; }
        public int iIsTdsApcbl { get; set; }
        public int iFk_NtPymtId { get; set; }
        public int iFk_DeductTypId { get; set; }
        public List<BrokerContactDtls>? ContactDtls { get; set; }
        public List<Brnchdtls2>? Brnchdtls2 { get; set; }
        public List<TDSList>? TDSList { get; set; }

        public BrokerAddressDetails? AddressDetails { get; set; }
        //public PanValidationResponse Response { get; set; }
    }
    public class Brnchdtls2
    {
        //---- Added on 22/02/2024

        public int iBrnchmMstId { get; set; }
        public string? sBranchNme { get; set; }
        public int iFk_BranchId { get; set; }
        public string? sFk_BrnchId { get; set; }
    }
    public class BrokerContactDtls
    {
        public int iCntctDtlsId { get; set; }
        public int? iFK_PartyId { get; set; }
        public string? CntctPerson { get; set; }
        public string? CntctNumber { get; set; }
        public string? EmailId { get; set; }
        public string? primaryName { get; set; }
        public bool bIsPrimary { get; set; }
        public bool Primary { get; set; }
    }

    public class BrokerBankRequest
    {
        public List<BrokerBankDetails> Bank { get; set; }
        public List<BrokerBankDetails> BankDelete { get; set; }
    }


    public class BrokerBankDetails
    {
        public int PK_PartyBankId { get; set; }
        public int? FK_PartyID { get; set; }
        public int? FK_AccountType { get; set; }
        public string? sBnfcryName { get; set; }
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
        public string? sDocmntUrl { get; set; }
    }

    public class BrokerBillingDetails
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
        public string? GST_effdt { get; set; }
    }

    public class BrokerAddressDetails
    {
        public int AddrssDtlsId { get; set; }
        public int CmpnyMstId { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Pincode { get; set; }
        public string? Address { get; set; }
    }
    public class BrokerAdvDetials
    {
        public int iPK_AdvDetailsId { get; set; }
        public int FK_PartyID { get; set; }
        public DateTime? dtCreatedOn { get; set; }
        public int? sCreatedBy { get; set; }
        public int? iFk_FinancialYrId { get; set; }
        public int? iFk_BranchId { get; set; }
        public int? iAdvTruck { get; set; }
        public int? iAdvTrailer { get; set; }
        public int? iCreditPeriod { get; set; }
        public DateTime? dtEffectiveDate { get; set; }
        public bool? bIsActive { get; set; }
    }

    public class TDSList
    {
        //---- Added on 22/02/2024
        public string? dtEffctvDate { get; set; }
        public string? sDocmntUrl { get; set; }
    }

    public class PanValidationResponse
    {
        public int Code { get; set; }
        public PanResult Result { get; set; }

        public class PanResult
        {
            public string PAN { get; set; }
            public string FIRST_NAME { get; set; }
            public string MIDDLE_NAME { get; set; }
            public string LAST_NAME { get; set; }
            public string AADHAR_NUM { get; set; }
            public string EMAIL { get; set; }
            public string DOB { get; set; }
            public string GENDER { get; set; }
            public string IDENTITY_TYPE { get; set; }
            public string MOBILE_NO { get; set; }
            public string ADDRESS_1 { get; set; }
            public string ADDRESS_2 { get; set; }
            public string ADDRESS_3 { get; set; }
            public string PINCODE { get; set; }
            public string CITY { get; set; }
            public string STATE { get; set; }
            public string COUNTRY { get; set; }
            public bool AADHAR_LINKED { get; set; }
            public bool DOB_CHECK { get; set; }
            public bool DOB_VERIFIED { get; set; }
        }
    }



}
