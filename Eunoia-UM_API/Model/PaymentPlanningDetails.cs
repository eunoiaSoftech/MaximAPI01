namespace Eunoia_UM_API.Model
{
    public class PaymentPlanningDetails
    {        public int iPk_PrchsInvPstngId { get; set; }
        public int iFk_InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceDate { get; set; }
        public string sInvoiceType { get; set; }
        public int iFk_PartyId { get; set; }
        public string Party { get; set; }
        public int iFk_PrtyBillAdrsId { get; set; }
        public string BillingAddress { get; set; }
        public decimal NetAmount { get; set; }
        public decimal TDSAmount { get; set; }
        public decimal GSTAmount { get; set; }
        public decimal GrossAmount { get; set; }
        public string PostingDate { get; set; }
        public int DueDays { get; set; }
        public string AccountNo { get; set; }
        public string ISFCNo { get; set; }
        public string MobileNo { get; set; }
        public string PymtMode { get; set; }
    }

        public class PaymentPlanningMaster
        {
            public int iPk_PymtplngMstId { get; set; }
            public string sEntryNo { get; set; }
            public string dtEntryDate { get; set; }
            public int iTransactionTyp { get; set; }
            public string dtPaymentDate { get; set; }
            public decimal dTtlNetAmt { get; set; }
            public decimal dTtlGrossAmt { get; set; }
            public int iMaxNo { get; set; }
            public int iVoucherStyle { get; set; }
            public int iFk_BranchId { get; set; }
            public int iFk_YearId { get; set; }
            public int iCrtdBy { get; set; }

            public List<PYMTPLNGDTL> PYMTPLNGDTL { get; set; }
            public List<PaymentPlanApproveDetail> PYMTPLNGDTL1 { get; set; }
        }

        public class PYMTPLNGDTL
        {
            public int iPk_PymtPlngDtlId { get; set; }
            public int iFk_PymtPlngMstId { get; set; }
            public int iFk_PrchsInvPstngId { get; set; }
            public int iFk_InvoiceId { get; set; }
            public string sInvoiceNo { get; set; }
            public string dtInvoiceDate { get; set; }
            public string sInvoiceType { get; set; }
            public int iFk_PartyId { get; set; }
            public int iFk_PrtyBilAdrsId { get; set; }
            public decimal dNetAmt { get; set; }
            public decimal dTdsAmt { get; set; }
            public decimal dGstAmt { get; set; }
            public decimal dGrossAmt { get; set; }
            public string dtPostingDate { get; set; }
            public int iDueDays { get; set; }
            public string PymtMode { get; set; }
        }

        public class PymtPlngMstIndex
        {
            public int iPk_PymtplngMstId { get; set; }
            public string EntryNo { get; set; }
            public string EntryDate { get; set; }
            public int iCrtdBy { get; set; }
            public string sUSRNME { get; set; }
            public string PaymentDate { get; set; }
            public decimal dTtlGrossAmt { get; set; }
        }
        public class PymtPlngEditModel
        {
            public int iPk_PymtplngMstId { get; set; }
            public string EntryNo { get; set; }
            public string EntryDate { get; set; }
            public string PaymentDate { get; set; }

            public int iTransactionTyp { get; set; }
            public string sTransactionTyp { get; set; }

            public int iPk_PymtPlngDtlId { get; set; }
            public int iFk_PymtPlngMstId { get; set; }
            public int iFk_PrchsInvPstngId { get; set; }
            public string InvoiceNumber { get; set; }
            public string InvoiceDate { get; set; }
            public string sInvoiceType { get; set; }
            public int iFk_PartyId { get; set; }
            public string sPartyName { get; set; }
            public int iFk_PrtyBilAdrsId { get; set; }
            public string StreetLane { get; set; }
            public decimal dNetAmt { get; set; }
            public decimal dTdsAmt { get; set; }
            public decimal dGstAmt { get; set; }
            public decimal? dGrossAmt { get; set; }
            public string PostingDate { get; set; }
            public int iDueDays { get; set; }
            public string dtPaymentDate { get; set; }

            public string AccountNo { get; set; }
            public string ISFCNo { get; set; }
            public string MobileNo { get; set; }
            public int iIsEmailDone { get; set; }
            public int iIsWhatsAppDone { get; set; }
            public string PymtMode { get; set; }
            public string VehicleNo { get; set; }
        }

        public class PaymentPlanningApprove
        {
            public int iPk_PrchsInvPstngId { get; set; }

            public decimal GrossAmount { get; set; }

            public string Party { get; set; }

            public string AccountNo { get; set; }

            public string ISFCNo { get; set; }

            public string MobileNo { get; set; }

            public string VehicleNo { get; set; }

            public string PostingDate { get; set; }

            public int DueDays { get; set; }
        }

        public class PaymentPlanApproveDetail
        {
            public int iPk_PrchsInvPstngId { get; set; }
            public int iIsPlaningDone { get; set; }
            public string dtPaymentDate { get; set; }
            public int iCrtdBy { get; set; }
        }

    }

