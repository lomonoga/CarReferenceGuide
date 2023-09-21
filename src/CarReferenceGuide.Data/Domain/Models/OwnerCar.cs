using System.Runtime.Serialization;
using CarReferenceGuide.Data.Domain.Interfaces;

namespace CarReferenceGuide.Data.Domain.Models;

public class OwnerCar : IEntity
{
    [IgnoreDataMember] public List<Car> Cars { get; set; } = default!;
    public string Fio { get; set; }
    public DateTime? Birthday { get; set; }
    public int? PassportNumber { get; set; }
    
    public Guid? Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
}