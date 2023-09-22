using System.ComponentModel.DataAnnotations;
using CarReferenceGuide.Data.Domain.Enums;
using CarReferenceGuide.Data.Domain.Models;

namespace CarReferenceGuide.Application.Domain.Common.DTO.Car;

public class AddCarRequest
{
    [Required] public string StateNumber { get; set; }
    public int Weight { get; set; }
    public int YearOfRelease { get; set; }
    public int Mileage { get; set; }
    public double? EngineVolume { get; set; }
    [Required] public TransmissionBox TransmissionBox { get; set; }
    [Required] public Gasoline Gasoline { get; set; }
    [Required] public Drive Drive { get; set; }
    public List<FileData>? Photos { get; set; }
    [Required] public string? Color { get; set; }
    [Required] public string? Country { get; set; }
    [Required] public string Model { get; set; }
    [Required] public string Brand { get; set; }
}