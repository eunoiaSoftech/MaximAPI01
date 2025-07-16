namespace Eunoia_UM_API.Model
{
    public class VehicleTargetModel
    {
        public int iPk_TargetId { get; set; }
        public string? sEntryNo { get; set; }
        public string? dtEntryDt { get; set; }
        public string? dtFrmDt { get; set; }
        public string? dtToDt { get; set; }
        public string? VehicleNo { get; set; }
        public string? EntryNo { get; set; }
        public string? EntryDate { get; set; }
        public string? FromDt { get; set; }
        public string? ToDt { get; set; }
        public string? CreatedDate { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? TargetD { get; set; }
        public int? iFk_BranchId { get; set; }
        public int? iFk_YearId { get; set; }
        public int? iStatus { get; set; }
        public int? iCrtdBy { get; set; }


        public string? sVehiclNo { get; set; }
        public decimal? dTarget { get; set; }

        public int? iPk_TargetDtlId { get; set; }


        public List<TrnVehicle>? TrnVehicleList { get; set; }

    }
    public class TrnVehicle
    {
        public int iPk_TargetDtlId { get; set; }
        public int? iFk_TargetId { get; set; }
        public int? iFk_VehclId { get; set; }
        public string? sVehiclNo { get; set; }
        public decimal? dTarget { get; set; }
        public int? iStatus { get; set; }
        public int? iCrtdBy { get; set; }
        public string? dtCrtdOn { get; set; }
    }
}


