using CarReferenceGuide.Application.Domain.Common.DTO.Car;
using CarReferenceGuide.Application.Domain.Services.Interfaces;
using CarReferenceGuide.Application.Handlers.Car;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarReferenceGuide.Controllers;

/// <summary>
/// Controller for the car
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CarController : ControllerBase
{
    private readonly ILogger<CarController> _logger;
    private readonly IMediator _mediator;
    private readonly ISecurityService _securityService;
    
    public CarController(ILogger<CarController> logger, IMediator mediator, ISecurityService securityService)
    {
        _logger = logger;
        _mediator = mediator;
        _securityService = securityService;
    }

    #region swaggerAddCar
    
    /// <summary>
    /// Allows you to add a car
    /// </summary>
    /// <returns>Status code</returns>
    
    #endregion
    
    [HttpPost("add-car")]
    public async Task<IActionResult> AddCar([FromBody] AddCarRequest request, CancellationToken token)
    {
        LogInfo("AddCar");
        await _mediator.Send(new AddCar(request), token);
        return Ok();
    }
    
    #region swaggerGetCar
    
    /// <summary>
    /// Allows you to get the car by id
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Information about the car</response>
    /// <response code="400">Exception</response>

    #endregion
    
    [HttpGet("get-car-by-id")]
    public async Task<IActionResult> GetCarById(Guid request, CancellationToken token)
    {
        LogInfo("GetCarById");
        var car = await _mediator.Send(new GetCarById(request), token);
        return Ok(car);
    }
    
    #region swaggerGetAllCars
    
    /// <summary>
    /// Allows you to get all cars
    /// </summary>
    /// <returns>All cars</returns>
    /// <response code="200">Information about cars</response>
    /// <response code="400">Exception</response>

    #endregion
    
    [HttpGet("get-all-cars")]
    public async Task<IActionResult> GetAllCars([FromQuery]CarsFilter filter,CancellationToken token)
    {
        LogInfo("GetAllCars");
        return Ok();
    }
    
    #region swaggerDeleteCarById
    
    /// <summary>
    /// Allows you to delete car by id
    /// </summary>
    /// <returns>Status code</returns>

    #endregion
    
    [HttpDelete("delete-car-by-id")]
    public async Task<IActionResult> DeleteCarById(Guid request, CancellationToken token)
    {
        LogInfo("DeleteCarById");
        return Ok();
    }
    
    #region swaggerEditCarById
    
    /// <summary>
    /// Allows you to delete car by id
    /// </summary>
    /// <returns>Status code</returns>

    #endregion
    
    [HttpPatch("edit-car-by-id")]
    public async Task<IActionResult> EditCarById([FromBody]EditCarRequest request, CancellationToken token)
    {
        LogInfo("EditCarById");
        return Ok();
    }

    /// <summary>
    /// Logging the user's ip and its actions
    /// </summary>
    /// <param name="nameOfAction"></param>
    private void LogInfo(string nameOfAction = "Non selected")
    {
        _logger.LogInformation(
            "The IP Address: " + _securityService.GetIdNotRegisteredUser()
            + " called " + nameOfAction);
    }
}