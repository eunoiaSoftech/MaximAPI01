namespace Eunoia_UM_API.Model
{
    public class UserwiseDrvExpRightsModel
    {
        public int iId { get; set; }
        public int iEntryNo { get; set; }
        public string? dtEntryDate { get; set; }
        public int iExpenseHeadId { get; set; }
        public int iUserId { get; set; }
        public string? sRangeFrom { get; set; }
        public string? sRangeTo { get; set; }
        public string sApprovalLevel { get; set; }
    }
    public class DeleteRequest
    {
        public int iId { get; set; }
    }
}
