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
        public int? iFk_ClasificationId { get; set; }
        public string? sSubBodyType { get; set; }

        public decimal? dAvgHighway { get; set; }

        public decimal? dWheelBase { get; set; }

        public decimal? dMaxHP { get; set; }

        public decimal? dCubeCapacity { get; set; }

        public decimal? dGVW { get; set; }

        public decimal? dTareWeight { get; set; }

        public decimal? dLoadingCapacity { get; set; }
        public int? iShortName { get; set; }
        public decimal? dAvgCity { get; set; }

        public int? iSeatingCapacity { get; set; }
        public decimal? dFrontTyre { get; set; }
        public decimal? dRearTyre { get; set; }
        public decimal? dTotalTyres { get; set; }
        public decimal? dBattery { get; set; }
        public int? dNoOfCylinder { get; set; }
        public bool? iIsDef { get; set; }
        public int? iFuelType { get; set; }
        public decimal? dFTnk1Cap { get; set; }
        public decimal? dFTnk2Cap { get; set; }
        public string? sFuelSensCmpy { get; set; }
        public int? fuelSensor { get; set; }
        public decimal? dDefCap { get; set; }
    }

    public class TyreDetail

    {

        public string? Id { get; set; }

        public int? position { get; set; }

        public int? category { get; set; }

        public int? manufature { get; set; }

        public int? brand { get; set; }

        public int? pattern { get; set; }

        public int? size { get; set; }

        public int? type { get; set; }

        public decimal? nsd { get; set; }

        public string? no { get; set; }

        public int? iFk_TyreDesc { get; set; }

        public int? iPk_DetId { get; set; }

    }

    public class TyreMst

    {

        public int? iId { get; set; }

        public int? iPk_TyreMstId { get; set; }

        public int? iFk_VehId { get; set; }

        public int? iFrntTyrCnt { get; set; }

        public int? iRearTyrCnt { get; set; }

        public int? iTotlTyrCnt { get; set; }

        public int? iFk_BranchId { get; set; }

        public int? iFk_LocationId { get; set; }

        public int? iFk_YearId { get; set; }

        public int? iCrtdBy { get; set; }

        public List<TyreDetail>? tyreDetails { get; set; }

    }

    public class BatteryVehicleDetail

    {

        public string? Id { get; set; }

        public int? iPk_DetId { get; set; }

        public decimal? nsd { get; set; }

        public string? no { get; set; }

        public int? iFk_BatteryDesc { get; set; }

        public int? iFk_BatteryStatus { get; set; }

    }

    public class BatteryVehicleMst

    {

        public int? iId { get; set; }

        public int? iPk_BatteryMstId { get; set; }

        public int? iFk_VehId { get; set; }

        public int? iBatteryCount { get; set; }

        public int? iFk_BranchId { get; set; }

        public int? iFk_LocationId { get; set; }

        public int? iFk_YearId { get; set; }

        public int? iCrtdBy { get; set; }

        public List<BatteryVehicleDetail>? batteryDetails { get; set; }

    }



}
