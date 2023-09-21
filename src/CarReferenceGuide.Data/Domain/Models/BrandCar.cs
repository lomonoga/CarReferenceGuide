using System.Runtime.Serialization;
using CarReferenceGuide.Data.Domain.Interfaces;

namespace CarReferenceGuide.Data.Domain.Models;

public class BrandCar : IEntity
{
    [IgnoreDataMember] public List<ModelCar> Models { get; set; } = default!;
    [IgnoreDataMember] public Country? Country { get; set; }
    public string Name { get; set; }
    public Guid? Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
}