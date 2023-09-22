using AutoFilterer.Extensions;
using CarReferenceGuide.Application.Domain.Common.DTO.Car;
using CarReferenceGuide.Data;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CarReferenceGuide.Application.Handlers.Car;

public record GetAllCars(CarsFilter filter) : IRequest<List<CarResponse>>;

public class GetAllCarsHandler : IRequestHandler<GetAllCars, List<CarResponse>>
{
    private readonly CarReferenceGuideDbContext _context;
    private readonly IMemoryCache _cache;
    
    public GetAllCarsHandler(CarReferenceGuideDbContext context, IMemoryCache memoryCache)
    {
        _context = context;
        _cache = memoryCache;
    }
    
    public async Task<List<CarResponse>> Handle(GetAllCars request, CancellationToken token)
    {
        _cache.TryGetValue(request.filter.GetHashCode(), out List<Data.Domain.Models.Car>? carCash);
        if (carCash is not null) return carCash.Adapt<List<CarResponse>>();
        var cars = await _context.Cars
            .AsNoTracking()
            .ApplyFilter(request.filter)
            .ToListAsync(token);
        _cache.Set(request.filter.GetHashCode(), cars, new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
        
        return cars.Adapt<List<CarResponse>>();
    }
}
