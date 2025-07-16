namespace Eunoia_UM_API.Model
{
    public class TrailerMaster
    {
        public int? iPk_TrlrMstId { get; set; }
        public string? sEntryNo { get; set; }
        public string? sTrailerAssetId { get; set; }
        public DateTime? dtEntryDate { get; set; }
        public int? iFk_NoAxles { get; set; }
        public int? iFk_AxleType { get; set; }
        public int? iFk_PreparedBy { get; set; }
        public int? iFk_ApprovedBy { get; set; }
        public int? iFk_CheckedBy { get; set; }
        public int? iFk_Branchid { get; set; }
        public int? iMaxID { get; set; }
        public int? Fk_SuspensionCapacity { get; set; }

        public int? iVoucherStyle { get; set; }
        public int? iFk_YearID { get; set; }
        public decimal? dLength { get; set; }
        public decimal? dWidth { get; set; }
        public decimal? dHeight { get; set; }
        public decimal? dCapacity { get; set; }
        public int? iAssetType { get; set; }
        public decimal? dWeight { get; set; }
        public int? iSideWall { get; set; }
        public string? sLongitude { get; set; }
        public string? sLatitude { get; set; }
        public string? sBrowser { get; set; }
        public string? sIpAddress { get; set; }
        public string? IVersion { get; set; }
        public DateTime? LstModifiedDate { get; set; }
        public int? LstModifiedBy { get; set; }
        public int? SuspensionType { get; set; }
        public string? leafspring { get; set; }
        public string? DescriptionOrRemarks { get; set; }
        public string? TrailerNo { get; set; }
        public int? TrailerType { get; set; }
        public int? AxleMake { get; set; }
        public string? steelGrade { get; set; }
        public string? invoiceno { get; set; }
        public string? InvoiceDate { get; set; }
        public int? SupplierId { get; set; }

        public int? SuspensionMake { get; set; }
        public int? WeightUOM { get; set; }

        public decimal? dTotalVolume { get; set; }
        //change
        public string? dtRegDate { get; set; }
        public string? dtRegVldDate { get; set; }
        public int? iFk_OwnerName { get; set; }
        public string? sYrOfMfg { get; set; }
        public string? sChassisNo { get; set; }
        public string? sEngineNo { get; set; }
        public string? sColor { get; set; }
        public int? iFk_VehClass { get; set; }
        public int? iVehStatus { get; set; }
        public int? iFk_VehModel { get; set; }
        public int? iFk_BodyType { get; set; }
        public decimal? dNoOfCylndrs { get; set; }
        public decimal? dHrsPwr { get; set; }
        public int? iSeat { get; set; }
        public decimal? dCbCpcty { get; set; }
        public decimal? dLdnWgt { get; set; }
        public decimal? dUnldnWgt { get; set; }
        public decimal? dWhlBse { get; set; }
        public decimal? dFlrArea { get; set; }
        public decimal? dTaxAmnt { get; set; }
        public string? dTaxPdUpto { get; set; }
        public int? iFk_Fuel { get; set; }
        public string? dtFitNessUpto { get; set; }
        public string? sVehNorms { get; set; }
        public string? sOwnerSrlNo { get; set; }
        public string? sPrsntAddress { get; set; }
        public string? sMobileNo { get; set; }
        public string? sEmailId { get; set; }
        public string? sRegnNo { get; set; }
        public int? iFk_OwnrShpTyp { get; set; }
        public int? iFk_VehMaker { get; set; }
        public int? iFk_LnkVehNo { get; set; }

    }


    public class TrailerProductDetails
    {
        public int iPk_TrlrDetId { get; set; }
        public int iFK_TrlrMstId { get; set; }
        public int iFk_ProductId { get; set; }
        public decimal ProductCpcty { get; set; }
    }
}
