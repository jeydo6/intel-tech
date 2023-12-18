FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM common-packages:0.2.0 AS packages
WORKDIR /packages

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS publish
WORKDIR /app

COPY ["Directory.Build.props", ""]
COPY ["src/IntelTech.Users.Application/IntelTech.Users.Application.csproj", "src/IntelTech.Users.Application/"]
COPY ["src/IntelTech.Users.Presentation/IntelTech.Users.Presentation.csproj", "src/IntelTech.Users.Presentation/"]

COPY --from=packages . "/packages"

RUN dotnet nuget add source "/packages" --name local
RUN dotnet restore "src/IntelTech.Users.Presentation/IntelTech.Users.Presentation.csproj"

COPY . .

RUN dotnet publish "src/IntelTech.Users.Presentation/IntelTech.Users.Presentation.csproj" -c Release -o /app/publish --no-restore /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IntelTech.Users.Presentation.dll"]
