namespace Eunoia_UM_API.Helper
{
    public class SendEmail
    {
        public string? RecieverEmailID { get; set; }
        public string? RecieverDisplayName { get; set; }
        public string? SenderID { get; set; }
        public string? SenderDisplayName { get; set; }
        public string? SenderIdPassword { get; set; }
        public string? URLToBeSend { get; set; }
        public string? URLToBeSendLogin { get; set; }
        public string? SMTPHost { get; set; }
        public int Port { get; set; }
        public string? Message { get; set; }
        public string? Subject { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Mobile { get; set; }
        public string? RoleName { get; set; }
        public int mobileOTP { get; set; }
        public int emailOTP { get; set; }

        //swapnil Added Parameters for FM Rate Chnage
        public string? EntryNo { get; set; }
        public string? EntryDt { get; set; }
        public string? FrghtInNmOf { get; set; }
        public string? OwnBrkNm { get; set; }
        public decimal FrtRt { get; set; }
        public decimal ChngdFrtRt { get; set; }
        public decimal BkngRt { get; set; }
        public decimal AdtnAmnt { get; set; }
        public decimal WvAmnt { get; set; }
        public string? Rsn { get; set; }
        public int CnfrmById { get; set; }
        public string? CnfrmBy { get; set; }
        public int Fk_Userid { get; set; }
        public int Fk_BranchId { get; set; }
        public string? ChngdOn { get; set; }
        public string? RecieverBranchHeadEmail { get; set; }
        public string? RecieverCompnayHeadEmail { get; set; }
        public string? CreatedBy { get; set; }
        public string? WavedBy { get; set; }

        //parameters added by Vinit
        public string? SenderEmail { get; set; }
        public string? ReceiverEmail { get; set; }
        public string? CC { get; set; }
        public string? BCC { get; set; }
        public string? PONO { get; set; }
        public string? PODate { get; set; }
        public string? PartyName { get; set; }

    }

}
