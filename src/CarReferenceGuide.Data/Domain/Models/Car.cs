using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using CarReferenceGuide.Data.Domain.Enums;
using CarReferenceGuide.Data.Domain.Interfaces;

namespace CarReferenceGuide.Data.Domain.Models;

public class Car : IEntity
{
    public string StateNumber { get; set; }
    public int Weight { get; set; }
    public int YearOfRelease { get; set; }
    public int Mileage { get; set; }
    public double EngineVolume { get; set; }
    public TransmissionBox TransmissionBox { get; set; }
    public Gasoline Gasoline { get; set; }
    public Drive Drive { get; set; }
    [Column(TypeName = "jsonb")] public List<FileData>? Photos { get; set; }
    
    [IgnoreDataMember] public ColorCar? Color { get; set; }
    [IgnoreDataMember] public Country? CountryOfManufacture { get; set; }
    [IgnoreDataMember] public ModelCar Model { get; set; }
    [IgnoreDataMember] public List<OwnerCar> Owners { get; set; } = default!;
    
    public Guid? Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
}