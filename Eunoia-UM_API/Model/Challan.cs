namespace Eunoia_UM_API.Model
{
    public class Challan
    {
        public string? sRemarks { get; set; }
        public string? entryNo { get; set; }
        public string? entryDate { get; set; }
        public int vehicleID { get; set; }
        public string? challanNo { get; set; }
        public string? challanDate { get; set; }
        public int challanTypeID { get; set; }
        public int challanLocationID { get; set; }
        public double challanAmount { get; set; }
        public int authorityID { get; set; }
        public string? challanAttachmentPath { get; set; }
        public string? challanAttachment { get; set; }
        public string? challanAttachmentEXT { get; set; }
        public string? paymentDate { get; set; }
        public int paymentModeID { get; set; }
        public int bankID { get; set; }
        public double payAmount { get; set; }
        public string? receiptNumber { get; set; }
        public int debitAccount { get; set; }
        public double debitAmount { get; set; }
        public string? paymentAttachment { get; set; }
        public string? paymentAttachmentPath { get; set; }
        public string? paymentAttachmentEXT { get; set; }
        public int iMaxNo { get; set; }
        public int voucherStyleID { get; set; }
        public int iYearID { get; set; }
        public int iBranchID { get; set; }
        public int createdByID { get; set; }
    }

    public class ChallanList
    {
        public int iPk_ChallanMStId { get; set; }
        public string? EntryNo { get; set; }
        public string? EntryDate { get; set; }
        public string? VehicleNo { get; set; }
        public string? VehicleType { get; set; }
        public string? DriverName { get; set; }
        public string? dChallanAmt { get; set; }
        public string? Payment { get; set; }

    }

    public class ChallanEditDetails
    {
        public string? sRemarks { get; set; }
        public string? entryNo { get; set; }
        public string? challanAttachment { get; set; }
        public string? challanAttachmentEXT { get; set; }
        public int iPk_ChallanMStId { get; set; }
        public string? sEntryNo { get; set; }
        public string? EntryDate { get; set; }
        public int iFk_VehclId { get; set; }
        public string? VehicleNo { get; set; }
        public string? sChallanNo { get; set; }
        public string? ChallanDate { get; set; }
        public int? iChallanType { get; set; }
        public string? ChallanType { get; set; }
        public int iFk_ChallanLocId { get; set; }
        public string? ChallanLocation { get; set; }
        public decimal dChallanAmt { get; set; }
        public int iFk_AuthorityId { get; set; }
        public string? Authority { get; set; }
        public string? sChallanAttchmnt { get; set; }
        public string? PaymentDate { get; set; }
        public int? iPaymtMode { get; set; }
        public string? PaymentMode { get; set; }
        public int? iFk_BankCash { get; set; }
        public string? BankCash { get; set; }
        public decimal? dAmt { get; set; }
        public string? sReceiptNo { get; set; }
        public int iFk_DebitAccnt { get; set; }
        public string? DebitAccount { get; set; }
        public decimal? dDebitAmt { get; set; }
        public string? sPaymtAtchmnt { get; set; }
        public string? paymentAttachmentPath { get; set; }
        public string? paymentAttachmentEXT { get; set; }
        public string? VehicleType { get; set; }
        public string? ChassisNo { get; set; }
        public string? Description { get; set; }
        public string? EngineNo { get; set; }
        public string? DriverName { get; set; }
        public string? License { get; set; }
        public string? ExpiringOn { get; set; }
        public int iMaxNo { get; set; }
        public int voucherStyleID { get; set; }
        public int iYearID { get; set; }
        public int iBranchID { get; set; }
        public int createdByID { get; set; }
    }
}
