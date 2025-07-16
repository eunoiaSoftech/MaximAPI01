namespace Eunoia_UM_API.Model
{
    public class Market
    {
        public int Pk_MktVehId { get; set; }

        public int Fk_OwnerName { get; set; }

        public int VehMode { get; set; }

        public string MktVehNo { get; set; }

        public string RegDate { get; set; }
        public string Description { get; set; }
        public int Fk_ModelNo { get; set; }
        public string EngineNo { get; set; }

        public string ChassisNo { get; set; }
        public int Fk_VehType { get; set; }
        public int IsGsp { get; set; }
        public string IsActive { get; set; }
        public decimal GVWWt { get; set; }
        public decimal TareWt { get; set; }
        public int IsNp { get; set; }

        public string NpExpdt { get; set; }

        public int IsInsurance { get; set; }

        public string InsuranceExpdt { get; set; }

        public int IsFiteness { get; set; }
        public string FitenessDt { get; set; }

        public decimal Netwt { get; set; }

        public string RcFile { get; set; }

    }

    public class SplitAddress
    {
        public string AddressLine { get; set; }
        public string Pincode { get; set; }
        public string[] District { get; set; }
        public string[][] State { get; set; }
        public string[] City { get; set; }
        public string[] Country { get; set; }
    }

    public class VehicleApiResponse
    {
        public int Code { get; set; }
        public VehicleResult Result { get; set; }
    }

    public class VehicleResult
    {
        public VehicleApiFetch Data { get; set; }
    }

    public class VehicleApiFetch
    {
        public string RegNo { get; set; }
        public string Class { get; set; }
        public string Chassis { get; set; }
        public string Engine { get; set; }
        public string VehicleManufacturerName { get; set; }
        public string Model { get; set; }
        public string VehicleColour { get; set; }
        public string Type { get; set; }
        public string NormsType { get; set; }
        public string BodyType { get; set; }
        public string OwnerCount { get; set; }
        public string Owner { get; set; }
        public string OwnerFatherName { get; set; }
        public string MobileNumber { get; set; }
        public string Status { get; set; }
        public string StatusAsOn { get; set; }
        public string RegAuthority { get; set; }
        public string RegDate { get; set; }
        public string VehicleManufacturingMonthYear { get; set; }
        public string RcExpiryDate { get; set; }
        public string VehicleTaxUpto { get; set; }
        public string VehicleInsuranceCompanyName { get; set; }
        public string VehicleInsuranceUpto { get; set; }
        public string VehicleInsurancePolicyNumber { get; set; }
        public string RcFinancer { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string VehicleCubicCapacity { get; set; }
        public string GrossVehicleWeight { get; set; }
        public string UnladenWeight { get; set; }
        public string VehicleCategory { get; set; }
        public string RcStandardCap { get; set; }
        public string VehicleCylindersNo { get; set; }
        public string VehicleSeatCapacity { get; set; }
        public string VehicleSleeperCapacity { get; set; }
        public string VehicleStandingCapacity { get; set; }
        public string Wheelbase { get; set; }
        public string VehicleNumber { get; set; }
        public string PuccNumber { get; set; }
        public string PuccUpto { get; set; }
        public string BlacklistStatus { get; set; }
        public string[] BlacklistDetails { get; set; }
        public string PermitIssueDate { get; set; }
        public string PermitNumber { get; set; }
        public string PermitType { get; set; }
        public string PermitValidFrom { get; set; }
        public string PermitValidUpto { get; set; }
        public string NonUseStatus { get; set; }
        public string NonUseFrom { get; set; }
        public string NonUseTo { get; set; }
        public string NationalPermitNumber { get; set; }
        public string NationalPermitUpto { get; set; }
        public string NationalPermitIssuedBy { get; set; }
        public bool IsCommercial { get; set; }
        public string NocDetails { get; set; }
    }


}
