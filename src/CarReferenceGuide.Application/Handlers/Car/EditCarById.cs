using CarReferenceGuide.Application.Domain.Common.DTO.Car;
using CarReferenceGuide.Application.Domain.Exceptions;
using CarReferenceGuide.Data;
using CarReferenceGuide.Data.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CarReferenceGuide.Application.Handlers.Car;

public record EditCarById(EditCarRequest Car) : IRequest<Unit>;

public class EditCarByIdHandler : IRequestHandler<EditCarById, Unit>
{
    private readonly CarReferenceGuideDbContext _context;
    private readonly IMemoryCache _cache;
    
    public EditCarByIdHandler(CarReferenceGuideDbContext context, IMemoryCache memoryCache)
    {
        _context = context;
        _cache = memoryCache;
    }
    
    public async Task<Unit> Handle(EditCarById request, CancellationToken token)
    {
        var car = await _context.Cars
            .Include(c => c.Country)
            .Include(c => c.Color)
            .Include(c => c.Model)
            .ThenInclude(m => m!.Brand)
            .FirstOrDefaultAsync(c => c.Id == request.Car.Id && !c.IsDeleted, token);
        if (car is null) throw new UserFriendlyException("Такого автомобиля не существует!");
        
        // Edit related values
        if (request.Car.Model is not null && !car.Model!.Name.Equals(request.Car.Model))
        {
            var model = await _context.ModelsCars
                            .Include(modelCar => modelCar.Brand!)
                            .FirstOrDefaultAsync(c => c.Id == car.Id, token)
                        ?? (await _context.ModelsCars
                            .AddAsync(new ModelCar {Name = request.Car.Model} ,token)).Entity;
            car.Model = model;
        }
        if (request.Car.Brand is not null 
            && (car.Model!.Brand is null 
            || !car.Model!.Brand!.Name.Equals(request.Car.Brand)))
        {
            var brand = await _context.BrandsCars
                .FirstOrDefaultAsync(c => c.Id == car.Id, token)
                        ?? (await _context.BrandsCars
                    .AddAsync(new BrandCar {Name = request.Car.Brand} ,token)).Entity;
            car.Model!.Brand = brand;
        }
        if (request.Car.Color is not null && !car.Color!.Name!.Equals(request.Car.Color))
        {
            var color = await _context.Colors
                            .FirstOrDefaultAsync(c => c.Id == car.Id, token)
                        ?? (await _context.Colors
                            .AddAsync(new Color {Name = request.Car.Color} ,token)).Entity;
            car.Color = color;
        }
        if (request.Car.Country is not null && !car.Country!.Name.Equals(request.Car.Color))
        {
            var country = await _context.Countries
                            .FirstOrDefaultAsync(c => c.Id == car.Id, token)
                        ?? (await _context.Countries
                            .AddAsync(new Country {Name = request.Car.Country} ,token)).Entity;
            car.Country = country;
        }
        
        // Edit free values
        car.Drive = request.Car.Drive ?? car.Drive;
        car.Photos= request.Car.Photos ?? car.Photos;
        car.Gasoline = request.Car.Gasoline ?? car.Gasoline;
        car.Mileage = request.Car.Mileage ?? car.Mileage;
        car.Weight = request.Car.Weight ?? car.Weight;
        car.EngineVolume = request.Car.EngineVolume ?? car.EngineVolume;
        car.StateNumber = request.Car.StateNumber ?? car.StateNumber;
        car.TransmissionBox = request.Car.TransmissionBox ?? car.TransmissionBox;
        car.YearOfRelease = request.Car.YearOfRelease ?? car.YearOfRelease;

        _cache.Set(car.Id!, car, new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
            
        await _context.SaveChangesAsync(token);
        _context.Entry(car).State = EntityState.Detached;
        
        return Unit.Value;
    }
}