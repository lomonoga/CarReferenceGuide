using CarReferenceGuide.Application.Handlers.DataBase;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarReferenceGuide.Controllers;

/// <summary>
/// Controller for the database
/// </summary>
[ApiController]
[Route("api/data-base")]
public class DataBaseController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public DataBaseController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    #region swaggerGetCountCars
    
    /// <summary>
    /// Allows you to get count of cars
    /// </summary>
    /// <returns>Count cars</returns>
    
    #endregion
    
    [HttpGet("get-count-cars")]
    public async Task<IActionResult> GetCountCars(CancellationToken token)
    {
        var count = await _mediator.Send(new GetCountCars(), token);
        return Ok(count);
    }
    
    #region swaggerGetDateLastAddedCar
    
    /// <summary>
    /// Allows you to get date and time last added car
    /// </summary>
    /// <returns>DateTme</returns>
    
    #endregion
    
    [HttpGet("get-datetime-last-car")]
    public async Task<IActionResult> GetDateTimeLastAddedCar(CancellationToken token)
    {
        var car = await _mediator.Send(new GetDateLastAddedCar(), token);
        return Ok(car);
    }
}