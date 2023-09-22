using CarReferenceGuide.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CarReferenceGuide.Application.Handlers.DataBase;

public record GetCountCars : IRequest<int>;

public class GetCountCarsHandler : IRequestHandler<GetCountCars, int>
{
    private readonly CarReferenceGuideDbContext _context;
    private readonly IMemoryCache _cache;
    
    public GetCountCarsHandler(CarReferenceGuideDbContext context, IMemoryCache memoryCache)
    {
        _context = context;
        _cache = memoryCache;
    }
    
    public async Task<int> Handle(GetCountCars request, CancellationToken token)
    {
        _cache.TryGetValue("2d70f612-66f1-410a-b8ae-bd1d1bfd7c48", out List<Data.Domain.Models.Car>? carCash);
        if (carCash is not null) return carCash.Count;

        var cars = await _context.Cars
            .AsNoTracking()
            .ToListAsync(token);
        
        _cache.Set("2d70f612-66f1-410a-b8ae-bd1d1bfd7c48", cars, new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));

        return cars.Count;
    }
}
