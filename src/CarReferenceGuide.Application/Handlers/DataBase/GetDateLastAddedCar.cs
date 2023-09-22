using CarReferenceGuide.Application.Domain.Exceptions;
using CarReferenceGuide.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarReferenceGuide.Application.Handlers.DataBase;


public record GetDateLastAddedCar : IRequest<DateTime>;

public class GetDateLastAddedCarHandler : IRequestHandler<GetDateLastAddedCar, DateTime>
{
    private readonly CarReferenceGuideDbContext _context;
    
    public GetDateLastAddedCarHandler(CarReferenceGuideDbContext context)
    {
        _context = context;
    }
    
    public async Task<DateTime> Handle(GetDateLastAddedCar request, CancellationToken token)
    {
        var lastCar = _context.Cars.AsNoTracking().Max(x => x.CreatedOn);
        if (lastCar is null) 
            throw new UserFriendlyException("Записей ещё нет");

        return (DateTime) lastCar;
    }
}