using CarReferenceGuide.Data.Domain.Enums;
using CarReferenceGuide.Data.Domain.Models;

namespace CarReferenceGuide.Application.Domain.Common.DTO.Car;

public sealed record CarResponse(
    string StateNumber, 
    int Weight, 
    int YearOfRelease, 
    int Mileage, 
    double EngineVolume,
    TransmissionBox TransmissionBox,
    Gasoline Gasoline,
    Drive Drive,
    List<FileData>? Photos,
    string Color,
    string Country,
    string Model,
    string Brand
    );