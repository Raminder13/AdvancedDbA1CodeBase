using System.Security.Cryptography.X509Certificates;

namespace WebApplication2.Models
{
    public class LaptopStore
    {
        public Guid LaptopStoreId { get; set; }
        public int Quantity { get; set; }
        public Laptop Laptop { get; set; }
        public Guid LaptopId { get; set; }
        public Store Store { get; set; }
        public Guid StoreId { get; set; }
    }
}
