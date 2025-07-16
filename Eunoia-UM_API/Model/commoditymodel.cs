namespace Eunoia_UM_API.Model
{
    public class commoditymodel
    {
        public int iPk_CmdtyId { get; set; }
        public int pk_Id { get; set; }
        public string sName { get; set; }
        public string sType { get; set; }
        public int iFk_BranchId { get; set; }
    }
}
