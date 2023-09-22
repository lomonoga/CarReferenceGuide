using System.ComponentModel.DataAnnotations;
using CarReferenceGuide.Data.Domain.Enums;
using CarReferenceGuide.Data.Domain.Models;

namespace CarReferenceGuide.Application.Domain.Common.DTO.Car;

public class EditCarRequest
{
    [Required] public Guid Id { get; set; }
    public string? StateNumber { get; set; }
    public int? Weight { get; set; }
    public int? YearOfRelease { get; set; }
    public int? Mileage { get; set; }
    public double? EngineVolume { get; set; }
    public TransmissionBox? TransmissionBox { get; set; }
    public Gasoline? Gasoline { get; set; }
    public Drive? Drive { get; set; }
    public List<FileData>? Photos { get; set; }
    public string? Color { get; set; }
    public string? Country { get; set; }
    public string? Model { get; set; }
    public string? Brand { get; set; }
}