using CarReferenceGuide.Application.Domain.Common.DTO.Car;
using CarReferenceGuide.Application.Domain.Exceptions;
using CarReferenceGuide.Data;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CarReferenceGuide.Application.Handlers.Car;

public record GetCarById(Guid Id) : IRequest<CarResponse>;

public class GetCarByIdHandler : IRequestHandler<GetCarById, CarResponse>
{
    private readonly CarReferenceGuideDbContext _context;
    private readonly IMemoryCache _cache;
    
    public GetCarByIdHandler(CarReferenceGuideDbContext context, IMemoryCache memoryCache)
    {
        _context = context;
        _cache = memoryCache;
    }
    
    public async Task<CarResponse> Handle(GetCarById request, CancellationToken token)
    {
        _cache.TryGetValue(request.Id, out Data.Domain.Models.Car? carCash);
        if (carCash is not null) return carCash.Adapt<CarResponse>();
        var car = await _context.Cars
            .Include(c => c.Country)
            .Include(c => c.Color)
            .Include(c => c.Model)
            .ThenInclude(m => m!.Brand)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == request.Id && !c.IsDeleted, token);
        if (car is null) throw new UserFriendlyException("Такого автомобиля нет!");
        
        // Caching the car
        _cache.Set(car.Id!, car, new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromHours(2)));
        
        return car.Adapt<CarResponse>();
    }
}