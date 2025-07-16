using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eunoia_UM_API.Model
{
    public class InventoryOutwardWR
    {
        public int iPk_InventoryOutId { get; set; }
        public int iFk_LocationId { get; set; }
        public string sLocationName { get; set; }
        public DateTime dtIssueDate { get; set; }
        public int iFk_IssueOn { get; set; }
        public string sIssueOn { get; set; }
        public string sAssetNo { get; set; } 
        public int iFk_ItemName { get; set; }
        public int sItemName { get; set; }
        public int iFk_ItemBrandId { get; set; }
        public string sItemBrand { get; set; }
        public decimal dItemQty { get; set; }
        public string sCreateBy { get; set; }
        public int iFk_CreatedBy { get; set; }
        public int iFk_ApprovedBy { get; set; }
        public int iFk_YearId { get; set; }
        public int iFk_VoucherStyle { get; set; }
        public int iMaxno { get; set; }
        public int iFk_BranchId { get; set; }
        public DateTime dtCreateOn { get; set; }
        public string sIP { get; set; }
         public int iStatusId { get; set; }

        public int iFk_SupplierId { get; set; }
        public string sSupplierName { get; set; }


    }
}
