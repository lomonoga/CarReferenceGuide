namespace CarReferenceGuide.Data.Domain.Interfaces;

public interface IGuidEntity
{
    Guid? Id { get; set; }
}

public interface IHistoricalEntity : IGuidEntity
{
    bool IsDeleted { get; set; }
}

public interface ITimedEntity : IHistoricalEntity
{
    DateTime? CreatedOn { get; set; }
    DateTime? ModifiedOn { get; set; }
}

public interface IEntity : ITimedEntity
{
}