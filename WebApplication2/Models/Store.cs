namespace WebApplication2.Models
{
    public class Store
    {
        public Guid StoreNumber { get; set; }
        public string StreetAddress { get; set; }
        public CanadianProvince Province { get; set; }
        public HashSet<LaptopStore> laptopStores { get; set; } = new HashSet<LaptopStore>();
    }

    public enum CanadianProvince
    {
        Alberta,
        BritishColumbia,
        Manitoba,
        NewBrunswick,
        NewfoundlandAndLabrador,
        NovaScotia,
        Ontario,
        PrinceEdwardIsland,
        Quebec,
        Saskatchewan,
        NorthwestTerritories,
        Nunavut,
        Yukon
    }
}
