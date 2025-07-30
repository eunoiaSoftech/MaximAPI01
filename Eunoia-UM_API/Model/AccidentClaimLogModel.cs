namespace Eunoia_UM_API.Model
{
    public class AccidentClaimLogModel
    {
        public string? sCurrentLocation { get; set; }
        public DateTime? dtDateOfAccident { get; set; }
        public string? sPersonVisited { get; set; }
        public string? dSettlementAmount { get; set; }
        public string? dDebitToDriverAmount { get; set; }
        public string? sAccidentRemark { get; set; }
        public string? sOnSpotPersonSignature { get; set; }  // base64 string or path
        public string? sSpotPersonSignature { get; set; }    // base64 string or path
        public bool? bIsConfirmed { get; set; }

        // For File upload
        public List<IFormFile>? Images { get; set; }
        //public List<CraneDetails>? CraneDetails { get; set; }
    }
    public class CraneDetails
    {
        public string sCraneType { get; set; }
        public int? iCraneCount { get; set; }
        public decimal? dCraneAmount { get; set; }
    }

}
