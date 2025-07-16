namespace Eunoia_UM_API.Model
{
    public class Billingdetails
    {
        public int PK_PartyDespatchDetails { get; set; }
        public int? FK_PartyID { get; set; }
        public int? FK_DespatchType { get; set; }
        public string? ContactPersone { get; set; }
        public string? StreetLane { get; set; }
        public int? FK_CountryID { get; set; }
        public int? FK_StateID { get; set; }
        public int? FK_CityID { get; set; }
        public string? FK_AreaID { get; set; }
        public int? PinCode { get; set; }
        public string? TelephoneNo { get; set; }
        public string? EmailID { get; set; }
        public string? RegisteredOn { get; set; }
        public int? FK_RegistrationID { get; set; }
        public int? FK_GstParty_TypeID { get; set; }


        public int? SCustomerName { get; set; }

        public string? sPartyName { get; set; }

        public string? GstIdentificationNo { get; set; }
        public string? GST_effdt { get; set; }
    }
}
