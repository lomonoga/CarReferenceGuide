using AutoFilterer.Types;
using CarReferenceGuide.Data.Domain.Enums;

namespace CarReferenceGuide.Application.Domain.Common.DTO.Car;

public class CarsFilter : FilterBase
{
    public int? Weight { get; set; }
    public int? YearOfRelease { get; set; }
    public int? Mileage { get; set; }
    public double? EngineVolume { get; set; }
    public TransmissionBox? TransmissionBox { get; set; }
    public Gasoline? Gasoline { get; set; }
    public Drive? Drive { get; set; }

    public override int GetHashCode()
    {
        return HashCode.Combine(
            Weight * 102_879 + 127_848, 
            YearOfRelease * 4019 + 678_463, 
            Mileage * 2029 + 718_073, 
            EngineVolume * 5639 + 446_863, 
            TransmissionBox + 780_000, 
            Gasoline + 526_111,  
            Drive + 5_389_641,
            CombineWith.GetHashCode() * 1_789);
    }
}