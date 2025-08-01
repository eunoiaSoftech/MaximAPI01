namespace Eunoia_UM_API.Model
{
    public class TyreGRNModel
    {
        public int iPk_PartyId { get; set; }
        public string sPartyName { get; set; }
    }
    public class PONOsDetailTyreGRN
    {
        public string? sPONO { get; set; }
        public decimal? Quantity { get; set; }
    }
    public class TyrenDescriptionTyreGRN
    {
        public string sDescription { get; set; }
        public string sTyreNo { get; set; }
    }
    public class SaveTyreGRNRequest
    {
        public int iPk_PartyId { get; set; }
        public string? sInvoiceNumber { get; set; }
        public DateTime? dtInvoiceDate { get; set; }
        public PONOsDetailTyreGRN PONOsDetails { get; set; }
        public List<TyrenDescriptionTyreGRN> TyreDetails { get; set; }
    }
}
