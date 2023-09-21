using System.Net;
using CarReferenceGuide.Application.Domain.Services.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace CarReferenceGuide.Application.Domain.Services;

public class SecurityService : ISecurityService
{
    private readonly IHttpContextAccessor _accessor;
    
    public SecurityService(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }
        
    /// <summary>
    /// Getting current user in system  
    /// </summary>
    /// <returns>User claims</returns>
    public ClaimsPrincipal? GetCurrentUser()
    {
        return _accessor.HttpContext?.User;
    }
    
    /// <summary>
    /// Getting Ip address not registered user
    /// </summary>
    /// <returns>IP Address</returns>
    public IPAddress? GetIdNotRegisteredUser()
    {
        return _accessor.HttpContext?.Connection.RemoteIpAddress;
    }
}