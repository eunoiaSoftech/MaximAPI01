namespace Eunoia_UM_API.Model
{
    public class NotificationConfig
    {
    }

    public class NotificationConfigAdd
    {
        public int iPk_AltertsId { get; set; }
        public int menuID { get; set; }
        public int submenuID { get; set; }
        public int secondSubmenuID { get; set; }
        public bool isWhatsApp { get; set; }
        public bool isEmail { get; set; }
        public bool isSMS { get; set; }
        public string? subject { get; set; }
        public string? body { get; set; }
        public string? branchID { get; set; }
        public string? to { get; set; }
        public string? cc { get; set; }
        public string? bcc { get; set; }
    }
    public class NotificationEmail
    {
        public string? To { get; set; }
        public string? CC { get; set; }
        public string? CCEmailOnly { get; set; }
        public string? BCC { get; set; }
        public string? BCCEmailOnly { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public string? AttachmentBase64 { get; set; }
        public string? AttachmentBase64Ext { get; set; }
        public string? AttachmentName { get; set; }
    }
}
