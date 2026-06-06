using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;

namespace Eunoia_UM.Models
{
    public class FMRateDifferenceModel
    {

        public int iPk_FMRATEDIFId { get; set; }

        public int? iFk_MenuId { get; set; }

        public int? iFk_TransId { get; set; }


        public string sTransNo { get; set; }

        public DateTime? dtTransDate { get; set; }

        public int? iFk_VehicleId { get; set; }

        public int? iFk_PartyId { get; set; }


        public decimal? dFrtRate { get; set; }


        public decimal? dChangeRate { get; set; }

        public int? iFk_CreatedBy { get; set; }

        public DateTime? dtCreatedOn { get; set; }


        public int iIsApproved { get; set; } = 0;

        public int? iFk_ApprovedBy { get; set; }

        public DateTime? dtApprovedOn { get; set; }

        public string sURL { get; set; }

        // For Displaying information
        public string Menu { get; set; }
        public string TransactionDate { get; set; }
        public string sMktVehNo { get; set; }
        public string sPartyName { get; set; }
        public string sComodityName { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }


        public List<prchAprvll> prchAprvll { get; set; }
        public List<unaprvlList> unaprvlList { get; set; }



    }

    public class prchAprvll
    {
        public int iPk_FMRATEDIFId { get; set; }
        public int? iType { get; set; }
        public int? UserId { get; set; }
        public int? TransactionId { get; set; }
        public int? iTransType { get; set; }
        public int? iFk_MenuId { get; set; }
        public int iFk_LocationId { get; set; }
        public int iFk_BranchId { get; set; }
        public int iFk_YearId { get; set; }

    }
    public class unaprvlList
    {
        public int id { get; set; }
    }

    public class driverList
    {
        public string? DONo { get; set; }
        public int iIsAprvd { get; set; }
        public int iFk_VehicleId { get; set; }
        public int iPk_LoadingMstId { get; set; }
        public int iPk_VehDrvtId { get; set; }
        public string sDrvName { get; set; }
        public string dtLicsExpiry { get; set; }
        public string sLicenseNo { get; set; }
        public string contactNo { get; set; }
        public string TrailerNo { get; set; }
        public string sUnqIdNo { get; set; }
        public string TripNo { get; set; }
        public string Route { get; set; }
        public string Customer { get; set; }
        public string Distance { get; set; }
        public string TransitTime { get; set; }

        //11-12-2023
        public int VhclTyp { get; set; }
        //14-01-2024
        public int iPk_TrpExpMstId { get; set; }
        public int iFk_LoadingMstId { get; set; }
        public int iPk_VehplcId { get; set; }
        public string PlacementNo { get; set; }
        public string PlacementDate { get; set; }
        public string VehicleNo { get; set; }

        //22-1-2024
        public int iPk_FrghtMemId { get; set; }
        public int iFk_LoadingId { get; set; }
        //24-1-2024

        public string sConsignmntNo { get; set; }
        public string ConsignmntDt { get; set; }
        public string sCntrctName { get; set; }
        public string VehType { get; set; }
        public string DriverName { get; set; }
        public string TrailerType { get; set; }
        public string sPartyName { get; set; }
        public string sRouteName { get; set; }
        public decimal dTtlPlcmntQty { get; set; }
        //27-01-2024
        public int ifk_ExpHead { get; set; }
        public string ExpenseHead { get; set; }
        public decimal damount { get; set; }
        public int iPk_RouteID { get; set; }
        public int iFk_FrmStnId { get; set; }
        public int iFk_ToStnId { get; set; }
        public int iIsLoadEmpty { get; set; }
        public int iFk_PlcmntId { get; set; }
        public int Id { get; set; }

        //added on 19/02/2024
        public int iPk_TrpStlmntMstId { get; set; }
        public string SettlementNo { get; set; }
        public string SettlementDate { get; set; }
        public decimal Amount { get; set; }
        public string Remark { get; set; }
        public int iTypId { get; set; }
        public int iPk_PrchsBkngMstId { get; set; }
        public string dtEntryDate { get; set; }
        public string sRefLRNo { get; set; }

        public string Type { get; set; }
        public string ReferenceNo { get; set; }

        //--------change bishnu------
        public string sEntryNo { get; set; }
        public string EntryDate { get; set; }
        public string PostingDate { get; set; }
        //----------- Swap changes ---------
        public int iIsMltiLr { get; set; }
        public decimal InvoiceWt { get; set; }
        public string VehModel { get; set; }
        public string dLoadCap { get; set; }
        public string InvoiceDt { get; set; }
        public string InvoiceNo { get; set; }
        public decimal TtlExpGvn { get; set; }
        public int iFk_ContrctId { get; set; }
        public int iIsApproved { get; set; }
        public int iChngRqst { get; set; }
        //--------change Chinna------
        public int iIsPostingDone { get; set; }
        public int iIsBankVouchrDone { get; set; }
        public int iIsAgncyBillDn { get; set; }
        public int iIsBillDone { get; set; }
        public int isLRDone { get; set; }
        public int iBlncVrchPaid { get; set; }
        public int iIsWhtsDn { get; set; }


        public decimal dRate { get; set; }
        public decimal Qty { get; set; }
        public decimal dInvoicVal { get; set; }
        public decimal GstAmnt { get; set; }
        public decimal AmtinGst { get; set; }
        public decimal dVehicleKM { get; set; }
        public string sDocmntUrl { get; set; }


    }

}
