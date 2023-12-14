FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

COPY ["Directory.Build.props", ""]
COPY ["src/IntelTech.Users.Application/IntelTech.Users.Application.csproj", "src/IntelTech.Users.Application/"]
COPY ["src/IntelTech.Users.Presentation/IntelTech.Users.Presentation.csproj", "src/IntelTech.Users.Presentation/"]

RUN dotnet restore "src/IntelTech.Users.Presentation/IntelTech.Users.Presentation.csproj"
COPY . .

WORKDIR /app/src/IntelTech.Users.Presentation
RUN dotnet build "IntelTech.Users.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IntelTech.Users.Presentation.csproj" -c Release -o /app/publish --no-restore /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IntelTech.Users.Presentation.dll"]