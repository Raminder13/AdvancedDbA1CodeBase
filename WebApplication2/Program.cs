using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebApplication2.Data;
using WebApplication2.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("LPSConnectionString"));
});

builder.Services.Configure<JsonOptions>(options => {
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});


var app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    IServiceProvider serviceProvider = scope.ServiceProvider;

    await SeedData.Initialize(serviceProvider);
}


// Get average price of laptop in specific store
app.MapGet("/brands/averagePrice/{Id}", (StoreContext db, Guid Id) =>
{
    try
    {
        HashSet<Laptop> laptopWithBrand = db.Laptops.Where(l => l.BrandId == Id).ToHashSet();

        if (laptopWithBrand.Count == 0)
        {
            throw new Exception("No laptops found.");
        }

        int numberOfLaptop = laptopWithBrand.Count;
        decimal averagePrice = laptopWithBrand.Average(l => l.Price);

        return Results.Ok(new { NumberOfLaptop = numberOfLaptop, AveragePrice = averagePrice });
    } catch (Exception ex)
    {
        return Results.NotFound(ex.Message);
    }
});


// Sort Stores by province
app.MapGet("/stores/ByProvince", (StoreContext db) =>
{
    HashSet<Store> storesByProvince = db.Stores
        .Where(sl => sl.laptopStores.Any())
        .ToHashSet();

    Dictionary<CanadianProvince, List<Store>> storeInProvince = storesByProvince
        .GroupBy(sl => sl.Province)
        .ToDictionary(group => group.Key, group => group.ToList());

    return Results.Ok(storeInProvince);
});


// Get store inventory searching by storeId
app.MapGet("/stores/inventory/{storeId}", (StoreContext context, Guid storeId) =>
{
    try
    {

        HashSet<LaptopStore> laptopInStore = context.LaptopStores.Where
        (l => l.StoreId == storeId && l.Quantity > 0)
        .Include(l => l.Laptop)
        .ThenInclude(l => l.Brand)
        .ToHashSet();

        return Results.Ok(laptopInStore); ;
    }
    catch (InvalidOperationException ex)
    {
        return Results.NotFound(ex.Message);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

// Post (increment 0r decrement) new stock value to specic laptop in specific store
app.MapPost("stores/{storeId}/{laptopId}/{changeQuantity}", (StoreContext db, Guid storeId, Guid laptopId, int changeQuantity) =>
{
    try
    {
        LaptopStore laptop = db.LaptopStores.FirstOrDefault(
           ls => ls.LaptopId == laptopId && ls.StoreId == storeId);

        if (laptop == null)
        {
            throw new ArgumentOutOfRangeException("No laptop found");
        }

        laptop.Quantity += changeQuantity;
        db.SaveChanges();

        return Results.Ok(laptop);
    }
    catch (InvalidOperationException ex)
    {
        return Results.NotFound(ex.Message);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});


// Get laptops with some filters
app.MapGet("/laptops/search", (StoreContext db, decimal? priceAbove, decimal? priceBelow, Guid? storeId,
    LaptopCondition? condition, Guid? brandId, string? searchPhrase) =>
{
    HashSet<Laptop> laptops = db.Laptops.ToHashSet();
    try
    {
        if (priceAbove < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(priceAbove));
        }

        if (priceBelow < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(priceBelow));
        }

        if (priceAbove.HasValue)
        {
            laptops = db.Laptops.Where(l => l.Price > priceAbove.Value).ToHashSet();
        }

        if (priceBelow.HasValue)
        {
            laptops = db.Laptops.Where(l => l.Price < priceBelow.Value).ToHashSet();
        }

        if (storeId.HasValue)
        {
            laptops = db.Laptops.Where(l => l.laptopStores.Any(ls => ls.StoreId == storeId.Value && ls.Quantity > 0)).ToHashSet();
        }

        if (condition.HasValue)
        {
            laptops = db.Laptops.Where(l => l.Condition == condition.Value).ToHashSet();
        }

        if (brandId.HasValue)
        {
            laptops = db.Laptops.Where(l => l.BrandId == brandId.Value).ToHashSet();
        }

        if (!string.IsNullOrEmpty(searchPhrase))
        {
            laptops = db.Laptops.Where(l => l.Model.Contains(searchPhrase)).ToHashSet();
        }

        return Results.Ok(laptops);

    }
    catch (InvalidOperationException ex)
    {
        return Results.BadRequest(ex.Message);
    } catch (Exception ex)
    {
        return Results.NotFound(ex.Message);
    }
});



app.Run();
