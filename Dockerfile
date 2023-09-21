# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ENV DOTNET_URLS=http://+:80
WORKDIR /source
COPY . .
RUN dotnet restore "./src/CarReferenceGuide.Api/CarReferenceGuide.Api.csproj"
RUN dotnet publish "./src/CarReferenceGuide.Api/CarReferenceGuide.Api.csproj" -c release -o /app --no-restore

# Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app ./

EXPOSE 80

ENTRYPOINT ["dotnet", "CarReferenceGuide.Api.dll"]