# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Solution and project files for restore caching
COPY FinalChallengeSA.slnx ./
COPY FinalChallengeSA.Domain/FinalChallengeSA.Domain.csproj FinalChallengeSA.Domain/
COPY FinalChallengeSA.Application/FinalChallengeSA.Application.csproj FinalChallengeSA.Application/
COPY FinalChallengeSA.Infra.Data/FinalChallengeSA.Infra.Data.csproj FinalChallengeSA.Infra.Data/
COPY FinalChallengeSA.Infra.IoC/FinalChallengeSA.Infra.IoC.csproj FinalChallengeSA.Infra.IoC/
COPY FinalChallengeSA.Api/FinalChallengeSA.Api.csproj FinalChallengeSA.Api/

RUN dotnet restore FinalChallengeSA.sln

# Application source
COPY . .

# API publish output
RUN dotnet publish FinalChallengeSA.Api/FinalChallengeSA.Api.csproj \
    -c Release \
    -o /app/publish \
    --no-restore \
    /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "FinalChallengeSA.Api.dll"]