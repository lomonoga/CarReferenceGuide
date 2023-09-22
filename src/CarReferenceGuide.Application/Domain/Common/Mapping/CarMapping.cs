using CarReferenceGuide.Application.Domain.Common.DTO.Car;
using CarReferenceGuide.Data.Domain.Models;
using Mapster;

namespace CarReferenceGuide.Application.Domain.Common.Mapping;

public class CarMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AddCarRequest, Car>()
            .Ignore(x => x.Model)
            .Ignore(x => x.Country)
            .Ignore(x => x.Color)
            .Compile();
        
        config.NewConfig<Car, CarResponse>()
            .Map(r => r.Country, c => c.Country!.Name)
            .Map(r => r.Color, c => c.Color!.Name)
            .Map(r => r.Model, c => c.Model!.Name)
            .Map(r => r.Brand, c => c.Model!.Brand!.Name)
            .Compile();
    }
}