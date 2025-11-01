using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Eunoia_UM_API.Model
{
    public class AccidentClaimLogModel
    {
        [JsonProperty("vehicleNo")]
        public string sVehicleNo { get; set; }

        [JsonProperty("accidentNo")]
        public string? iAccidentNo { get; set; }

        [JsonProperty("accidentDate")]
        public DateTime? dtDateOfAccident { get; set; }

        [JsonProperty("accidentLocation")]
        public string sCurrentLocation { get; set; }

        [JsonProperty("cityName")]
        public string sCityName { get; set; }

        [JsonProperty("tripId")]
        public string sTripId { get; set; }

        [JsonProperty("driverName")]
        public string sDriverName { get; set; }

        [JsonProperty("driverId")]
        public string sDriverCode { get; set; }

        [JsonProperty("personVisited")]
        public int? iPersonVisitedId { get; set; }

        [JsonProperty("settlementAmount")]
        public decimal? dSettlementAmount { get; set; }

        [JsonProperty("debitToDriverAmount")]
        public decimal? dDebitToDriverAmount { get; set; }

        [JsonProperty("craneDetails")]
        public List<CraneDetails> CraneDetails { get; set; }

        [JsonProperty("remark")]
        public string sAccidentRemark { get; set; }

        [JsonProperty("confirmation")]
        public bool? bIsConfirmed { get; set; }

        [JsonProperty("onSpotSignature")]
        public string sOnSpotPersonSignature { get; set; }

        [JsonProperty("spotSignature")]
        public string sSpotPersonSignature { get; set; }
        [JsonProperty("transactionType")]
        public int? itransactionType { get; set; }

        // Optional additional properties
        public string? sMobileNo { get; set; }
        public string? sLicenseNo { get; set; }
        public DateTime? dtLicenseValidity { get; set; }
        public string? sEwayBillNo { get; set; }
        public DateTime? dtEwayExpiry { get; set; }
        public DateTime? dtEntryDate { get; set; }
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

    public class AccidentClaimResponseModel
    {
        public int accidentClaimId { get; set; }  // maps from iPk_AccidentClaimLogId

        public string vehicleNo { get; set; }
        public int accidentNo { get; set; }
        public DateTime accidentDate { get; set; }
        public string accidentLocation { get; set; }
        public string cityName { get; set; }
        public string tripId { get; set; }
        public string driverName { get; set; }
        public string driverId { get; set; }
        public int personVisited { get; set; }
        public decimal settlementAmount { get; set; }
        public decimal debitToDriverAmount { get; set; }
        public string remark { get; set; }
        public string onSpotSignature { get; set; }
        public string spotSignature { get; set; }
        public bool confirmation { get; set; }

        //public string mobileNo { get; set; }
        public string licenseNo { get; set; }
        public DateTime licenseValidity { get; set; }
        public string ewayBillNo { get; set; }
        public DateTime ewayExpiry { get; set; }
        public DateTime entryDate { get; set; }
        public List<CraneDetails> craneDetails { get; set; }
    }

}
