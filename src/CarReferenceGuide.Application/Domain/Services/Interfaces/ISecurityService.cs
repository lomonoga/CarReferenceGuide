using System.Net;
using System.Security.Claims;

namespace CarReferenceGuide.Application.Domain.Services.Interfaces;

public interface ISecurityService
{
    public ClaimsPrincipal? GetCurrentUser();
    public IPAddress? GetIdNotRegisteredUser();
}