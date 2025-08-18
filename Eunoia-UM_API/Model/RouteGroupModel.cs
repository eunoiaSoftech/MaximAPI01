
namespace Eunoia_UM_API.Model
{
    public class RouteGroupModel
    {
        public int iGroupId { get; set; }
        public string sEntryNo { get; set; }
        public string dtEntryDate { get; set; }
        public string sGroupName { get; set; }
        public List<int>? SelectedRouteIDs { get; set; }
    }
}
