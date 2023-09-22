using CarReferenceGuide.Application.Domain.Common.DTO.Car;
using CarReferenceGuide.Application.Domain.Exceptions;
using CarReferenceGuide.Data;
using CarReferenceGuide.Data.Domain.Models;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarReferenceGuide.Application.Handlers.Car;

public record AddCar(AddCarRequest AddCarRequest) : IRequest<Unit>;

public class AddUserHandler : IRequestHandler<AddCar, Unit>
{
    private readonly CarReferenceGuideDbContext _context;
    
    public AddUserHandler(CarReferenceGuideDbContext context)
    {
        _context = context;
    }
    
    public async Task<Unit> Handle(AddCar request, CancellationToken token)
    {
        // Mapping Car
        var entityCar = request.AddCarRequest.Adapt<Data.Domain.Models.Car>();
        
        // Check for the existence of a car
        var existedCar = await _context.Cars.AsNoTracking().FirstOrDefaultAsync(c =>
            c.StateNumber == entityCar.StateNumber, token);
        if (existedCar is not null && !existedCar.IsDeleted)
            throw new UserFriendlyException("Автомобиль с таким гос номером уже существует!");
        
        // Check for the existence of a brand and added if not existed
        var brand = await _context.BrandsCars
            .FirstOrDefaultAsync(b => b.Name == request.AddCarRequest.Brand, token) 
            ?? (await _context.BrandsCars
                .AddAsync(new BrandCar {Name = request.AddCarRequest.Brand}, token)).Entity;

        // Check for the existence of a model and added if not existed
        var model = await _context.ModelsCars
            .FirstOrDefaultAsync(b => b.Name == request.AddCarRequest.Model, token) 
            ?? (await _context.ModelsCars
                .AddAsync(new ModelCar {Name = request.AddCarRequest.Model}, token)).Entity;
        model.Brand = brand;
        
        // Check for the existence of a color and added if not existed
        var color = await _context.Colors
            .FirstOrDefaultAsync(c => c.Name == request.AddCarRequest.Color, token) 
            ?? (await _context.Colors
            .AddAsync(new Color {Name = request.AddCarRequest.Color}, token)).Entity;
        
        // Check for the existence of a country and added if not existed
        var country = await _context.Countries
            .FirstOrDefaultAsync(c => c.Name == request.AddCarRequest.Country, token) 
            ?? (await _context.Countries
            .AddAsync(new Country {Name = request.AddCarRequest.Model}, token)).Entity;
        
        // Filling in the model
        entityCar.Model = model;
        entityCar.Color = color;
        entityCar.Country = country;
        
        await _context.Cars.AddAsync(entityCar, token);
        
        await _context.SaveChangesAsync(token);
        _context.Entry(entityCar).State = EntityState.Detached;
        
        return Unit.Value;
    }
}