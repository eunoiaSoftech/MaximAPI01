namespace Eunoia_UM_API.Model
{
    public class Role
    {
    }
    public class MenuMasters
    {
        public int MenuId { get; set; }
        public string? ControllerName { get; set; }
        public string? ActionMethod { get; set; }
        public string? MenuName { get; set; }
        public int Status { get; set; }
        public int ihierarchyActive { get; set; }
    }
    public class SecSubMenu
    {
        public int iId { get; set; } = 0;
        public int iFK_Mnuid { get; set; }
        public int iFK_SubMnuId { get; set; }
        public string? sCtrNme { get; set; }
        public string? sActNme { get; set; }
        public int iStatus { get; set; } = 0;
        public int? iDisOrdId { get; set; }
        public string? sSecSubMenu { get; set; }
    }
    public class SubMenuMaster
    {
        public int iSubMnuId { get; set; }
        public string? sCntrolName { get; set; }
        public string? sActnMthd { get; set; }
        public int iStatus { get; set; }
        public int iFK_MnuId { get; set; }
        public string? sMenuName { get; set; }
        public string? sSubMnuName { get; set; }
        public int? ihierarchyActive { get; set; }

    }
    public class DeptRoleMnuMapping
    {
        public int? iPk_DeptRoleMnuMappingId { get; set; } = 0;
        public int? iDeptId { get; set; }
        public string? sDeptName { get; set; }
        public int? iRoleId { get; set; }
        public string? sRoleName { get; set; }
        public int? iMnuId { get; set; }
        public string? sMnuName { get; set; }
        public int? iSubMnuId { get; set; }
        public string? sSubMnuName { get; set; }
        public int? iSecSubMnuId { get; set; }
        public string? sSecSubMnuId { get; set; }
        public int? iActive { get; set; }
        public string? sCtrDate { get; set; }
        public int? iOrderid { get; set; }

    }
    public class RateMaster
    {

        public long iPk_RateMstId { get; set; }
        public int iMnuid { get; set; }
        public int iSbMnu { get; set; }
        public string? sMnuNme { get; set; }
        public string? sSbMnuNme { get; set; }
        public decimal dRate { get; set; }
        public int iTyp { get; set; }
        public string? dtInsertStrDt { get; set; }
        public string? dtInsertEndDt { get; set; }
        public int iStts { get; set; }
        public int iClnId { get; set; }
    }
    public class ChangesSetting
    {
        public int? MapId { get; set; }
        public int? Id { get; set; }
        public int? status { get; set; }
        public int? deptid { get; set; }
        public int? RoleId { get; set; }
        public int? BranchId { get; set; }
        public int? LocationId { get; set; }
        public string Typedata { get; set; }
    }
    public class BranchLocMapping
    {
        public int? Id { get; set; }
        public int? BranchId { get; set; }
        public int? LocationId { get; set; }
        public int? iStatus { get; set; }
        public string? BranchName { get; set; }
        public string? LocationName { get; set; }

    }
    public class AddDepartment
    {
        public int DepartmentID { get; set; }
        public string? DepartmentName { get; set; }
        public string? PartyId { get; set; }
        public string? Status { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? Type { get; set; }
    }
    public class Activeclass
    {
        public string? Tablename { get; set; }
        public int Id { get; set; }
        public int status { get; set; }
        public string? Display { get; set; }
        public string? Guid { get; set; }
    }
    public class RoleMastertable
    {
        public int iPk_RolId { get; set; }
        public string? sRolName { get; set; }
        public int iStatus { get; set; }
        public string? sCrtdByPrtyCode { get; set; }


    }
    public class AddGroup
    {
        public int ID { get; set; }
        public int GroupID { get; set; }
        public string? GroupName { get; set; }
        public int MenuID { get; set; }
        public string? Menu { get; set; }
        public int SubmenuId { get; set; }
        public string? Submenu { get; set; }
        public string? Status { get; set; }
        public string? PartyId { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? Type { get; set; }

        public int iRoleId { get; set; }
        public string? sRoleName { get; set; }
    }
    public class MappingRoleWithDepartmentandGroup
    {
        public int iPK_RoleDeptId { get; set; }
        public int iDeptId { get; set; }
        public int iGrpId { get; set; }
        public int iRoleId { get; set; }
        public int iStatus { get; set; }
        public string? PartyId { get; set; }
        public string? DepartmentName { get; set; }
        public string? GroupName { get; set; }
        public string? RoleName { get; set; }
    }
    public class Permissionclass
    {
        public string? Type { get; set; }
        public int MappingId { get; set; }
        public int MstGroupId { get; set; }
        public int PermissionId { get; set; }
        public int status { get; set; }
        public string? PartyId { get; set; }
        public int Fk_BranchId { get; set; }
        public int Fk_LocationId { get; set; }
    }
}
