using CarReferenceGuide;
using CarReferenceGuide.Application;
using CarReferenceGuide.Application.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

//Add components
builder.Services.AddApi(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
//builder.Services.AddDataAccess(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(c => c.Run(async context =>
{
    var exception = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;
    var exceptionMessage = exception is UserFriendlyException ? exception.Message : "Internal Server Error";
    context.Response.StatusCode = StatusCodes.Status400BadRequest;
    await context.Response.WriteAsJsonAsync(new UserFriendlyExceptionResponse(exceptionMessage));
}));

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.MapControllers();

app.Run();