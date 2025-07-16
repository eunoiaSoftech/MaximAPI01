using System.Text;

namespace Eunoia_UM_API.Model
{
    public class VisitorMaster
    {
        public int? MstVst_Id { get; set; }
        public string? sVstNam { get; set; }
        public string? sVstCtNo { get; set; }
        public string? sVstComFrm { get; set; }
        public int? iVstLct { get; set; }
        public int? iVstMetId { get; set; }
        public int? iVstPur { get; set; }
        public string? sVstPur { get; set; }
        public int? iVstMebCut { get; set; }
        public string? sCkInTme { get; set; }
        public string? sCkOtTme { get; set; }
        public int? iVstMetSts { get; set; }
        public string? sAtta { get; set; }
        public string? iRestoVst { get; set; }
        public string? sRestoVstMsg { get; set; }
        public string? sVstPic { get; set; }
        public string? sVstMeet { get; set; }
         
    }
    public class VistorView : VisitorMaster
    {
        public string? UserName { get; set; }
        public string? VisitLocation { get; set; }
        public string? MeetPur { get; set; }
        public string? RespondName { get; set; }
        public string? CreateDate { get; set; }
    }
}
