using System.ComponentModel.DataAnnotations;

namespace Eunoia_UM_API.Model
{
    public class AccidentClaimLogModel
    {
        public string? sCurrentLocation { get; set; }
        public string? sDriverName { get; set; }
        public string? sCityName { get; set; }
        public string? sMobileNo { get; set; }
        public string? sLicenseNo { get; set; }
        public DateTime? dtLicenseValidity { get; set; }
        public string? sDriverCode { get; set; }
        public string? sTripId { get; set; }
        public string? sEwayBillNo { get; set; }
        public DateTime? dtEwayExpiry { get; set; }
        public DateTime? dtEntryDate { get; set; }
        public int? iAccidentNo { get; set; }
        public string? sVehicleNo { get; set; }
        public DateTime? dtDateOfAccident { get; set; }
        public string? sPersonVisited { get; set; }
        public decimal? dSettlementAmount { get; set; }
        public decimal? dDebitToDriverAmount { get; set; }
        public string? sAccidentRemark { get; set; }
        public string? sOnSpotPersonSignature { get; set; }
        public string? sSpotPersonSignature { get; set; }
        public bool? bIsConfirmed { get; set; }

        [Required]
        public List<CraneDetails> CraneDetails { get; set; }
    }
    public class CraneDetails
    {
        public string sCraneType { get; set; }
        public int? iCraneCount { get; set; }
        public decimal? dCraneAmount { get; set; }
    }
    public class AccidentDetailsVehicleNo
    {
        public string AccidentNo { get; set; }
        public string AccidentDate { get; set; }
        public string AccidentLocation{ get; set; }
        public string CityName{ get; set; }

    }

}
