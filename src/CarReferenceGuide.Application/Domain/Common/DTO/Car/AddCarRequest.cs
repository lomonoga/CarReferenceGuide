namespace CarReferenceGuide.Application.Domain.Common.DTO.Car;

public class AddCarRequest
{
    //[Required(ErrorMessage = "Не указано ФИО")]
    public string FullName { get; set; }
    
}