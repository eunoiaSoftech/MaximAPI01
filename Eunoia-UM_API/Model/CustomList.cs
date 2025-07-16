namespace Eunoia_UM_API.Model
{
    public class CustomList
    {
        public int Id { get; set; }
        public string? text { get; set; }
        public string? Type { get; set; }
        public string? Name { get; set; }
    }
    public class setting
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Nullable<int> IsActive { get; set; }
    }
    public class CustomListRequest
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public string? StrId { get; set; }
    }
    public class CustomEnum
    {
        public int CustomEnumId { get; set; }
        public int? EnumNo { get; set; }
        public string? Name { get; set; }

        public string? EnumName { get; set; }
        public string? text { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? PartyId { get; set; }
        public int? Id { get; set; }
        public int? IsActive { get; set; }
    }
    public class Settings
    {
        public int Id { get; set; }
        public string? SettingName { get; set; }
        public int IsActive { get; set; }
        public string? Type { get; set; }
    }
}
