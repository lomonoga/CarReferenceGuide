using CarReferenceGuide.Application.Domain.Common.DTO.Car;
using CarReferenceGuide.Application.Domain.Exceptions;
using CarReferenceGuide.Data;
using CarReferenceGuide.Data.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CarReferenceGuide.Application.Handlers.Car;

public record DeleteCarById(Guid Id) : IRequest<Unit>;

public class DeleteCarByIdHandler : IRequestHandler<DeleteCarById, Unit>
{
    private readonly CarReferenceGuideDbContext _context;
    private readonly IMemoryCache _cache;
    
    public DeleteCarByIdHandler(CarReferenceGuideDbContext context, IMemoryCache memoryCache)
    {
        _context = context;
        _cache = memoryCache;
    }
    
    public async Task<Unit> Handle(DeleteCarById request, CancellationToken token)
    {
        var car = await _context.Cars
            .FirstOrDefaultAsync(c => c.Id == request.Id && !c.IsDeleted, token);
        if (car is null) throw new UserFriendlyException("Такого автомобиля не существует!");

        _cache.Remove(car.Id!);
        _context.Cars.Remove(car);
        
        await _context.SaveChangesAsync(token);
        _context.Entry(car).State = EntityState.Detached;
        
        return Unit.Value;
    }
}