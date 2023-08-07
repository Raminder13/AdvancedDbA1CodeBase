using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public class SeedData
    {
        public async static Task Initialize(IServiceProvider serviceProvider)
        {
            StoreContext db = new StoreContext(serviceProvider.GetRequiredService<DbContextOptions<StoreContext>>());

            db.Database.EnsureDeleted();
            db.Database.Migrate();

            Brand dell = new Brand { Name = "Dell"};
            Brand toshiba = new Brand { Name = "Toshiba"};
            Brand asus = new Brand { Name = "Asus" };

            if (!db.Brands.Any())
            {
                db.Brands.Add(dell);
                db.Brands.Add(toshiba);
                db.Brands.Add(asus);
                db.SaveChanges();
            }

            Laptop xps = new Laptop { Model = "XPS 15", Brand = dell, Price = 600, Condition = LaptopCondition.New };
            Laptop latitude = new Laptop { Model = "latitude 34", Brand = dell, Price = 800, Condition = LaptopCondition.Rental };
            Laptop excellent = new Laptop { Model = "Excellent", Brand = toshiba, Price = 1000, Condition = LaptopCondition.Refurbished };
            Laptop satallite = new Laptop { Model = "Satallite", Brand = toshiba, Price = 400, Condition = LaptopCondition.New };
            Laptop rog = new Laptop { Model = "ROG", Brand = asus, Price = 700, Condition = LaptopCondition.Rental };
            Laptop tuf = new Laptop { Model = "TUF", Brand = asus, Price = 1100, Condition = LaptopCondition.Refurbished };

            if(!db.Laptops.Any()) 
            {
                db.Laptops.Add(xps);
                db.Laptops.Add(latitude);
                db.Laptops.Add(excellent);
                db.Laptops.Add(satallite);
                db.Laptops.Add(rog);
                db.Laptops.Add(tuf);
                db.SaveChanges ();

            }

            Store winnipeg = new Store { StreetAddress = "735 Scotland Ave", Province = CanadianProvince.Manitoba};
            Store brampton = new Store { StreetAddress = "757 Jackson Ave", Province = CanadianProvince.Ontario };
            Store calgary = new Store { StreetAddress = "919 Chancellor Drive", Province = CanadianProvince.Alberta };
            Store surrey = new Store { StreetAddress = "52 White rock", Province = CanadianProvince.BritishColumbia };

            if (!db.Stores.Any())
            {
                db.Stores.Add(winnipeg);
                db.Stores.Add(brampton);
                db.Stores.Add(calgary);
                db.Stores.Add(surrey);
                db.SaveChanges();
            }

            LaptopStore ls1 = new LaptopStore { Laptop = xps, Store = winnipeg, Quantity = 10 };
            LaptopStore ls2 = new LaptopStore { Laptop = xps, Store = brampton, Quantity = 5 };
            LaptopStore ls3 = new LaptopStore { Laptop = latitude, Store = surrey, Quantity = 7 };
            LaptopStore ls4 = new LaptopStore { Laptop = latitude, Store = calgary, Quantity = 5 };
            LaptopStore ls5 = new LaptopStore { Laptop = excellent, Store = winnipeg, Quantity = 12};
            LaptopStore ls6 = new LaptopStore { Laptop = excellent, Store = brampton, Quantity = 15 };
            LaptopStore ls7 = new LaptopStore { Laptop = satallite, Store = surrey, Quantity = 4 };
            LaptopStore ls8 = new LaptopStore { Laptop = satallite, Store = calgary, Quantity = 8};
            LaptopStore ls9 = new LaptopStore { Laptop = rog, Store = winnipeg, Quantity = 9 };
            LaptopStore ls10 = new LaptopStore { Laptop = rog, Store = brampton, Quantity = 10 };
            LaptopStore ls11 = new LaptopStore { Laptop = tuf, Store = calgary, Quantity = 12 };
            LaptopStore ls12 = new LaptopStore { Laptop = tuf, Store =  winnipeg, Quantity = 13};

            if (!db.LaptopStores.Any())
            {
                db.LaptopStores.Add(ls1);
                db.LaptopStores.Add(ls2);
                db.LaptopStores.Add(ls3);
                db.LaptopStores.Add(ls4);
                db.LaptopStores.Add(ls5);
                db.LaptopStores.Add(ls6);
                db.LaptopStores.Add(ls7);
                db.LaptopStores.Add(ls8);
                db.LaptopStores.Add(ls9);
                db.LaptopStores.Add(ls10);
                db.LaptopStores.Add(ls11);
                db.LaptopStores.Add(ls12);
                db.SaveChanges();
            }


        }
    }
}
