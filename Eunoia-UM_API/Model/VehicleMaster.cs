namespace Eunoia_UM_API.Model
{
    public class VehicleMaster
    {
        public int iPK_ID { get; set; }
        public string? sUnqIdNo { get; set; }
        public string? sVehName { get; set; }
        public string? sVehImg { get; set; }
        public string? sLicePlate { get; set; }
        public string? sAstType { get; set; }
        public int iVehType { get; set; }
        public string? VehTypeNamw { get; set; }
        public string? sYrOfMfg { get; set; }
        public string? sVehStatus { get; set; }
        public int iMaker { get; set; }
        public string? MakerName { get; set; }
        public int iModel { get; set; }
        public string? ModelName { get; set; }
        public string? sTrim { get; set; }
        public string? sAssgnBrnch { get; set; }
        public string? sChassisNo { get; set; }
        public string? dtLastReadingAt { get; set; }
        public string? sColor { get; set; }
        public int iBdType { get; set; }
        public string? BdTypeName { get; set; }
        public string? sSubBdType { get; set; }
        public decimal dLength { get; set; }
        public decimal dHeight { get; set; }
        public decimal dWidth { get; set; }
        public decimal dGVW_RC { get; set; }
        public decimal dGVW_Act { get; set; }
        public int iGrossUOM { get; set; }
        public string? GrossUOMName { get; set; }
        public decimal dTareWt_RC { get; set; }
        public decimal dTare_Act { get; set; }
        public int iTareUOM { get; set; }
        public string? TareUOMName { get; set; }
        public int iVehClass { get; set; }
        public string? VehClassName { get; set; }
        public int iTripExpCat { get; set; }
        public string? TripExpCatName { get; set; }
        public int iTrailerType { get; set; }
        public string? TrailerTypeName { get; set; }
        public decimal dLoadCap { get; set; }
        public decimal dEPA_City { get; set; }
        public decimal dEPA_Highway { get; set; }
        public decimal dEPA_Empty { get; set; }
        public string? sEngNo { get; set; }
        public string? sEngDesc { get; set; }
        public decimal dMaxHP { get; set; }
        public int iFuelType { get; set; }
        public string? FuelTypeName { get; set; }
        public decimal dFTnk1Cap { get; set; }
        public decimal dFTnk2Cap { get; set; }
        public bool iIsDef { get; set; }
        public decimal dDefCap { get; set; }
        public int iPurVndr { get; set; }
        public string? PurVndrName { get; set; }
        public decimal dPurPrice { get; set; }
        public string? dtPur { get; set; }
        public int iRegLctn { get; set; }
        public int iVehOAM { get; set; }
        public int iFk_Branchid { get; set; }
        public int iFk_PreparedBy { get; set; }
        public int iFk_YearID { get; set; }

        public string? RegLctnName { get; set; }
        public DateTime dtCrtdOn { get; set; }
        public string? sCrtdBy { get; set; }
        public string? sRCUploads { get; set; }
        public string? dtRCExpDate { get; set; }
        public bool isActive { get; set; }
        public string dtRegdate { get; set; }
        public int iNoOfCylinders { get; set; }
        public int iRegisterAuthority { get; set; }
        public int iSeatCapcty { get; set; }

    }
}
