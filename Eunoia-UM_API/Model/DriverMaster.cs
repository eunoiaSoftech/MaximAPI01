namespace Eunoia_UM_API.Model
{
    public class DriverMaster
    {
        public int iPk_VehDrvtId { get; set; }
        public int iGrpLdgrId { get; set; }
        public int iIsLedgrTag { get; set; }
        public int iIsTdsApcbl { get; set; }
        public int iFk_NtPymtId { get; set; }
        public int iFk_DeductTypId { get; set; }
        public string? sRsgndAttch { get; set; }
        public string? sFirstName { get; set; }
        public string? sMiddletName { get; set; }
        public string? sLastName { get; set; }
        public string? dtDob { get; set; }
        public string? dtDoj { get; set; }
        public int iFk_BloodGroupId { get; set; }
        public string? BloodGroup { get; set; }
        public string? sFathername { get; set; }
        public string? sPFNo { get; set; }
        public string? sESICNo { get; set; }
        public string? iFk_GenderId { get; set; }
        public string? iPMobileNo { get; set; }
        public string? iOMobileNo { get; set; }
        public int iFk_CntryId { get; set; }
        public string? Country { get; set; }
        public int iFk_StateId { get; set; }
        public string? State { get; set; }
        public int iFk_CityId { get; set; }
        public string? City { get; set; }
        public string? sRAddress { get; set; }
        public string? sAadhaar { get; set; }
        public string? sPAN { get; set; }
        public string? sDrvPhoto { get; set; }
        public string? sDrvPhotoExt { get; set; }
        public string? sPanCardAttch { get; set; }
        public string? sPanCardAttchExt { get; set; }
        public string? sAdharCardAttch { get; set; }
        public string? sAdharCardAttchExt { get; set; }
        public string? sAddressProofAttch { get; set; }
        public string? sAddressProofAttchExt { get; set; }
        public string? sOtherAttch { get; set; }
        public string? sOtherAttchExt { get; set; }
        public string? dtSTrainig { get; set; }
        public string? sSNumber { get; set; }
        public string? dGrossSalary { get; set; }
        public string? sEducation { get; set; }
        public string? dtCrtDate { get; set; }
        public bool IsActive { get; set; }
        public bool isResign { get; set; }
        public string? dtRsgDt { get; set; }
        public string? iFk_FinYear { get; set; }
        public int iFk_BrnchId { get; set; }
        public int iMaxId { get; set; }
        public string? iVoucherStyle { get; set; }
        public string? iFk_Createdby { get; set; }
        public string? iFk_ApprovedBy { get; set; }
        public string? sIpAddress { get; set; }
        public string? sLongitude { get; set; }
        public string? sLatitude { get; set; }
        public string? sBrowser { get; set; }
        public string? sDevice { get; set; }
        public string? sPincode { get; set; }
        public List<DriverContactList>? driverContacts { get; set; }
        public List<DriverFamilyList>? driverFamilyList { get; set; }
        public List<DriverNomineeList>? DriverNomineeList { get; set; }
        public List<GuarantorDetailsList>? GuarantorDetailsList { get; set; }
        public List<LicenceDataSave>? licenceData { get; set; }

        public string? sUANNO { get; set; }
        public decimal dAmount { get; set; }
        public string? dtRejoiningDate { get; set; }
        public int driverID { get; set; }

        public LicenseResponseApi? Response { get; set; }

    }

    public class DriverContactList
    {
        public string? DriverMstId { get; set; }
        public string? sCntctDetlspersonNme { get; set; }
        public string? sCntctNum { get; set; }
        public int iCntcRelation { get; set; }
        public bool bPrimary { get; set; }
        public string? relationshipName { get; set; }
        public string? status { get; set; }
        public int iPk_DrvCntLst { get; set; }
    }
    public class DriverFamilyList
    {
        public string? DriverMstId { get; set; }
        public string? iFk_DriverMstId { get; set; }
        public string? sFamNme { get; set; }
        public int iRelation { get; set; }
        public int iPk_FmlyId { get; set; }

        public bool bPrimary { get; set; }
        public string? dtBirthDt { get; set; }
        public string? sAttchmnt { get; set; }
        public string? sFamilyNme { get; set; }
        public string? sAttachment { get; set; }
        public string? sbirthDate { get; set; }
        public int iFamilyRelation { get; set; }
    }

    public class DriverNomineeList
    {
        public string? DriverMstId { get; set; }
        public string? iFk_DriverMstId { get; set; }
        public string? sNomineeNme { get; set; }
        public int iAccNo { get; set; }
        public int iBankAccount { get; set; }
        public int iPk_NomineeId { get; set; }

        public bool bPrimary { get; set; }
        public string? dtDtOfBirth { get; set; }
        public string? sAttchmnt { get; set; }
        public string? dtbirthDateNom { get; set; }
        public string? sAttachment { get; set; }
        public string? sbirthDate { get; set; }
        public int iFamilyRelation { get; set; }
    }

    public class DriverLicence
    {
        public int iFk_VehDrvtId { get; set; }
        public int iPk_DrvLics { get; set; }
        public string? sLicsNumber { get; set; }
        public string? dtLicsIssue { get; set; }
        public string? dtLicsExpiry { get; set; }
        public string? sLcsAttch { get; set; }
        public string? driverName { get; set; }
        public string? sLcsAttchExt { get; set; }

        public int iType { get; set; }
        public string? sIssueBy { get; set; }

    }
    public class DriverMedical
    {
        public int iPk_DrvMdcl { get; set; }
        public int iFk_VehDrvtId { get; set; }
        public string? driverName { get; set; }
        public string? dtTestOn { get; set; }
        public string? dtNextTestOn { get; set; }
        public string? sMedicalAttch { get; set; }
        public string? sMedicalAttchExt { get; set; }
    }
    public class DriverBank
    {
        public int iPk_DrvBank { get; set; }
        public int iFk_VehDrvtId { get; set; }
        public int iAcntType { get; set; }
        public string? iAcntTypeName { get; set; }
        public string? driverName { get; set; }
        public string? sBankAcntNo { get; set; }
        public string? sBranch { get; set; }
        public int iBankNameId { get; set; }
        public string? iBankName { get; set; }
        public string? sOtherBankName { get; set; }
        public string? sIFSCCode { get; set; }
        public string? IsPrimary { get; set; }
        public string? sGpayId { get; set; }
        public string? sUPIId { get; set; }
        public string? isActive { get; set; }
    }

    public class GuarantorDetailsList
    {
        public int iPk_GurantorDtlId { get; set; }
        public string? sGuarantorNme { get; set; }
        public string? sCntctNum { get; set; }
        public string? sPresentAdd { get; set; }

    }

    public class LicenseResponseApi
    {
        public string DcLicNo { get; set; }
        public string DlTrValdFrDt { get; set; }
        public string DlTrValdToDt { get; set; }
        public string dlIssueDt { get; set; }
        public string biPhoto { get; set; }
    }

    public class LicenseDetailsResponse
    {
        public int Code { get; set; }
        public LicenseResult Result { get; set; }
    }

    public class LicenseResult
    {
        public string DlNumber { get; set; }
        public string Dob { get; set; }
        public List<BadgeDetail> BadgeDetails { get; set; }
        public DlValidity DlValidity { get; set; }
        public DetailsOfDrivingLicence DetailsOfDrivingLicence { get; set; }
    }

    public class BadgeDetail
    {
        public string BadgeIssueDate { get; set; }
        public string BadgeNo { get; set; }
        public List<string> ClassOfVehicle { get; set; }
    }

    public class DlValidity
    {
        public ValidityPeriod NonTransport { get; set; }
        public string HazardousValidTill { get; set; }
        public ValidityPeriod Transport { get; set; }
        public string HillValidTill { get; set; }
    }

    public class ValidityPeriod
    {
        public string To { get; set; }
        public string From { get; set; }
    }

    public class DetailsOfDrivingLicence
    {
        public string DateOfIssue { get; set; }
        public string DateOfLastTransaction { get; set; }
        public string Status { get; set; }
        public string LastTransactedAt { get; set; }
        public string Name { get; set; }
        public string FatherOrHusbandName { get; set; }
        public List<AddressList> AddressList { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        public SplitAddress SplitAddress { get; set; }
        public List<string> CovDetails { get; set; }
    }

    public class AddressList
    {
        public string CompleteAddress { get; set; }
        public string Type { get; set; }
        public SplitAddress SplitAddress { get; set; }
    }

    public class ApiSplitAddress
    {
        public List<string> District { get; set; }
        public List<List<string>> State { get; set; }
        public List<string> City { get; set; }
        public string Pincode { get; set; }
        public List<string> Country { get; set; }
        public string AddressLine { get; set; }
    }



    public class LicenceDataSave
    {
        public string? sLicsNumber { get; set; }
        public string? dtLicsIssue { get; set; }
        public string? dtLicsExpiry { get; set; }
        public string? sLcsAttch { get; set; }
        public string? driverName { get; set; }
        public string? sLcsAttchExt { get; set; }

        public int iType { get; set; }
        public string? sIssueBy { get; set; }
    }

    public class DriverDetails
    {
        public string FullName { get; set; }
        public string iPMobileNo { get; set; }
        public string sDriverCode { get; set; }
        public string sLicsNumber { get; set; }
        public string dtLicsExpiry { get; set; }
    }

}
