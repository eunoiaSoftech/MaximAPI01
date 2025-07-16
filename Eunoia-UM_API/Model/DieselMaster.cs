namespace Eunoia_UM_API.Model
{
    public class DieselMaster
    {
        public int iPk_Did { get; set; }
        public int iFk_PumpId { get; set; }
        public int iFk_FuelType { get; set; }
        public int iFk_CityId { get; set; }
        public string? sPumpName { get; set; }
        public decimal? dPrevRate { get; set; }
        public decimal? dDiscRate { get; set; }
        public decimal dRate{ get; set; }
        public bool bDiscount { get; set; }
        public string? sDiscountType { get; set; }
        public decimal dCalculatedValue { get; set; }
        public string? dtEffectiveDate { get; set; }
        public string? sCity { get; set; }
        public bool bIsActive { get; set; }
    }
}
