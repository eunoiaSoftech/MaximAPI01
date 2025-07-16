namespace Eunoia_UM_API.Model
{
    public class OverloadE
    {
        public int iPk_OverLoadId { get; set; }
        public int iFk_RouteId { get; set; }
        public int iFk_VehTypId { get; set; }
        public decimal dFromWt { get; set; }
        public decimal dToWt { get; set; }
        public int iFk_UomId { get; set; }
        public decimal dQty { get; set; }
        public decimal dAmount { get; set; }
    }
}
