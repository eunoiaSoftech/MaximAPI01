namespace Eunoia_UM_API.Model
{
    public class VehicleType
    {
        public int iPk_VehTypeId { get; set; }
        public string? sVehTypeId { get; set; }
        public string? sVehTypeName { get; set; }
        public string? VehicleTypes { get; set; }
        public string? Maker { get; set; }
        public string? Model { get; set; }
        public string? NAxel { get; set; }
        public string? TrailerLength { get; set; }

        public int? iFk_VehMake { get; set; }
        public int? iFk_VehModel { get; set; }
        public int? iFk_TrailerType { get; set; }
        public decimal? dTrailerLen { get; set; }
        public int? iFk_UOM { get; set; }
        public int? iFk_NoOfAxels { get; set; }
        public int? iFk_PreparedBy { get; set; }
        public int? iFk_CheckedBy { get; set; }
        public int? iFk_ApprovedBy { get; set; }
        public int? iFk_BranchID { get; set; }
        public int? iMaxID { get; set; }
        public int? iVoucherStyle { get; set; }
        public int? iFk_YearID { get; set; }
        public string? sShortName { get; set; }
        public int? iFk_vehicleTypeId { get; set; }
        public int? iTotalTyre { get; set; }
        public int? iTotalTyreStepney { get; set; }
        public int? iTypeOfBody { get; set; }
        public int? IsActive { get; set; }
        public int? iFk_MkrClssn { get; set; }
    }
}
