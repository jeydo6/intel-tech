FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

COPY ["Directory.Build.props", ""]
COPY ["src/IntelTech.Common.Bus/IntelTech.Common.Bus.csproj", "src/IntelTech.Common.Bus/"]
COPY ["src/IntelTech.Common.Mediator/IntelTech.Common.Mediator.csproj", "src/IntelTech.Common.Mediator/"]
COPY ["src/IntelTech.Common.Migrations/IntelTech.Common.Migrations.csproj", "src/IntelTech.Common.Migrations/"]
COPY ["src/IntelTech.Organizations.Application/IntelTech.Organizations.Application.csproj", "src/IntelTech.Organizations.Application/"]
COPY ["src/IntelTech.Organizations.Domain/IntelTech.Organizations.Domain.csproj", "src/IntelTech.Organizations.Domain/"]
COPY ["src/IntelTech.Organizations.Infrastructure/IntelTech.Organizations.Infrastructure.csproj", "src/IntelTech.Organizations.Infrastructure/"]
COPY ["src/IntelTech.Organizations.Presentation/IntelTech.Organizations.Presentation.csproj", "src/IntelTech.Organizations.Presentation/"]

RUN dotnet restore "src/IntelTech.Organizations.Presentation/IntelTech.Organizations.Presentation.csproj"
COPY . .

WORKDIR /app/src/IntelTech.Organizations.Presentation
RUN dotnet build "IntelTech.Organizations.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IntelTech.Organizations.Presentation.csproj" -c Release -o /app/publish --no-restore /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IntelTech.Organizations.Presentation.dll"]
