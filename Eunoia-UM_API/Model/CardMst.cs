using System.Net;

namespace Eunoia_UM_API.Model
{
    public class CardMst
    {
        public int? iPk_CardId { get; set; }
        public string? sCardName { get; set; }
        public decimal? dCardBalance { get; set; }
        public string? dtCrtDate { get; set; }
        public bool? IsActive { get; set; }
        public int? iFk_FinYear { get; set; }
        public string? iFk_Createdby { get; set; }
        public string? iFk_ApprovedBy { get; set; }
    }
    public class CardRecharge
    {
        public int? Pk_CARDRCHRG     {get;set;}
        public int? iFk_CardType     {get;set;}
        public int? iFk_VehicleId    {get;set;}
        public int? iRechMode        {get;set;}
        public string? sCardNumber   {get;set;}
        public decimal? dAmount      {get;set;}
        public string? dtRechDate    {get;set;}
        public string? iFk_CreatedBy {get;set;}
        public string? iFk_CheckedBy {get;set;}
        public string? iFk_ApprovedBy{get;set;}
        public int? iFk_YearId    {get;set;}
        public int? iFk_BranchId     {get;set;}
        public string? sIpAddress    {get;set;}
        public string? sLongitude    {get;set;}
        public string? sLatitude     {get;set;}
        public string? sBrowser      {get;set;}
        public string? sDevice       {get;set;}
        public int? iIsDeleted { get; set; }

    }
}
