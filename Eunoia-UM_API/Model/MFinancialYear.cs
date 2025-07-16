namespace Eunoia_UM_API.Model
{
    public class MFinancialYear
    {
        public int? iPk_FinId { get; set; }
        public int? iPk_TrpexpmstId { get; set; }
        public string? dtStartDate { get; set; }
        public string? dtEndDate { get; set; }
        public bool? bIsActive { get; set; }
        public int? iFk_BranchId { get; set; }
    }
}
