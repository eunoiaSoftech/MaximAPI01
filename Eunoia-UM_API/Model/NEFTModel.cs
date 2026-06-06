namespace Eunoia_UM_API.Model
{
    public class NEFTModel
    {
        public List<NEFTMaster> NEFTMasters { get; set; }
        public List<NEFTEdit> editData { get; set; }
        public List<NEFTEdit> NEFTForUnApprove { get; set; }
    }

    public class NEFTMaster
    {
        public int iPk_NeftExportMstId { get; set; }
        public string sEntryNo { get; set; }
        /*  public DateTime dtEntryDate { get; set; }
          public DateTime dtFromDate { get; set; }
          public DateTime dtToDate { get; set; }*/

        public string dtEntryDate { get; set; }
        public string dtFromDate { get; set; }
        public string dtToDate { get; set; }
        public int iFormat { get; set; }
        public int iMaxNo { get; set; }
        public int iVoucherStyle { get; set; }
        public int iFk_YearId { get; set; }
        public int iFk_BranchId { get; set; }
        public int iCrtdBy { get; set; }
        public decimal dTotlCount { get; set; }
        public decimal dTotlAmt { get; set; }
        public string sChquNo { get; set; }
        public string dtChquDt { get; set; }
        public string sChquAtchmnt { get; set; }
        public int isApprove { get; set; }
        public int iIsWhtsDn { get; set; }
        public int iFk_ChequeBookName { get; set; }




        public List<NEFTMaster> NEFTMasters { get; set; }
    }

    public class NEFTEdit
    {


        public int iFk_ChequeBookName { get; set; }
        public string sChequeBookName { get; set; }

        public string Format { get; set; }
        public string EntryDate { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }
        public int iPk_NeftExportMstId { get; set; }
        public string sEntryNo { get; set; }
        /*  public DateTime dtEntryDate { get; set; }
          public DateTime dtFromDate { get; set; }
          public DateTime dtToDate { get; set; }*/

        public string dtEntryDate { get; set; }
        public string dtFromDate { get; set; }
        public string dtToDate { get; set; }
        public int iFormat { get; set; }
        public int iMaxNo { get; set; }
        public int iVoucherStyle { get; set; }
        public int iFk_YearId { get; set; }
        public int iFk_BranchId { get; set; }
        public int iCrtdBy { get; set; }
        public decimal dTotlCount { get; set; }
        public decimal dTotlAmt { get; set; }


        public string TransactionType { get; set; }
        public string BeneficiaryCode { get; set; }
        public string BeneficiaryAccountNumber { get; set; }
        public decimal InstrumentAmount { get; set; }
        public string BeneficiaryName { get; set; }
        public string PrintLocation { get; set; }
        public string BeneAddress1 { get; set; }
        public string BeneAddress2 { get; set; }
        public string BeneAddress3 { get; set; }
        public string BeneAddress4 { get; set; }
        public string BeneAddress5 { get; set; }
        public string InstructionReferenceNumber { get; set; }
        public string CustomerReferenceNumber { get; set; }
        public string Paymentdetails1 { get; set; }
        public string Paymentdetails2 { get; set; }
        public string Paymentdetails3 { get; set; }
        public string Paymentdetails4 { get; set; }
        public string Paymentdetails5 { get; set; }
        public string Paymentdetails6 { get; set; }
        public string Paymentdetails7 { get; set; }
        public string ChequeNumber { get; set; }
        public string ChequeDate { get; set; }
        public string MICRNO { get; set; }
        public string IFSCCode { get; set; }
        public string BeneBank { get; set; }
        public string BeneBank1 { get; set; }
        public string BeneBranch { get; set; }
        public string BeneEmailId { get; set; }

        public string sTransactionType { get; set; }
        public string sBeneficiaryCode { get; set; }
        public string sBeneficiaryAccNo { get; set; }
        public decimal dInstrumentAmount { get; set; }
        public string sBeneficiaryName { get; set; }
        public string sDraweeLocation { get; set; }
        public string sPrintLocation { get; set; }
        public string sBeneAddress1 { get; set; }
        public string sBeneAddress2 { get; set; }
        public string sBeneAddress3 { get; set; }
        public string sBeneAddress4 { get; set; }
        public string sBeneAddress5 { get; set; }

        public string sInstructionRefNumber { get; set; }
        public string sCustomerRefNumber { get; set; }


        public string sPaymentdetails1 { get; set; }
        public string sPaymentdetails2 { get; set; }
        public string sPaymentdetails3 { get; set; }
        public string sPaymentdetails4 { get; set; }
        public string sPaymentdetails5 { get; set; }

        public string sPaymentdetails6 { get; set; }
        public string sPaymentdetails7 { get; set; }

        public string sChequeNumber { get; set; }
        public string dtChequeDate { get; set; }
        public string sMICRNO { get; set; }
        public string sIFSCCOD { get; set; }
        public string sBENEBANK1 { get; set; }
        public string sBeneBank2 { get; set; }
        public string sBeneBranch { get; set; }
        public string sBeneEmailId { get; set; }
        public int iFrmtTyp { get; set; }
        public string sChquNo { get; set; }
        public string dtChquDt { get; set; }
        public string sChquAtchmnt { get; set; }
        public int iFk_BankVouchrMstId { get; set; }
        public int iPk_NeftExportDtlId { get; set; }
        public int isReversed { get; set; }


    }
}
