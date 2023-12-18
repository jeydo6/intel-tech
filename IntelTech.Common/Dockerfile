FROM scratch AS base
WORKDIR /packages

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS pack
WORKDIR /app

COPY ["Directory.Build.props", ""]
COPY ["src/IntelTech.Common.Bus/IntelTech.Common.Bus.csproj", "src/IntelTech.Common.Bus/"]
COPY ["src/IntelTech.Common.Bus.RabbitMQ/IntelTech.Common.Bus.RabbitMQ.csproj", "src/IntelTech.Common.Bus.RabbitMQ/"]
COPY ["src/IntelTech.Common.Mediator/IntelTech.Common.Mediator.csproj", "src/IntelTech.Common.Mediator/"]
COPY ["src/IntelTech.Common.Migrations/IntelTech.Common.Migrations.csproj", "src/IntelTech.Common.Migrations/"]
COPY ["src/IntelTech.Common.Testing/IntelTech.Common.Testing.csproj", "src/IntelTech.Common.Testing/"]

RUN dotnet restore "src/IntelTech.Common.Bus/IntelTech.Common.Bus.csproj"
RUN dotnet restore "src/IntelTech.Common.Bus.RabbitMQ/IntelTech.Common.Bus.RabbitMQ.csproj"
RUN dotnet restore "src/IntelTech.Common.Mediator/IntelTech.Common.Mediator.csproj"
RUN dotnet restore "src/IntelTech.Common.Migrations/IntelTech.Common.Migrations.csproj"
RUN dotnet restore "src/IntelTech.Common.Testing/IntelTech.Common.Testing.csproj"

COPY . .

RUN dotnet pack "src/IntelTech.Common.Bus/IntelTech.Common.Bus.csproj" -c Release -o /app/packages
RUN dotnet pack "src/IntelTech.Common.Bus.RabbitMQ/IntelTech.Common.Bus.RabbitMQ.csproj" -c Release -o /app/packages
RUN dotnet pack "src/IntelTech.Common.Mediator/IntelTech.Common.Mediator.csproj" -c Release -o /app/packages
RUN dotnet pack "src/IntelTech.Common.Migrations/IntelTech.Common.Migrations.csproj" -c Release -o /app/packages
RUN dotnet pack "src/IntelTech.Common.Testing/IntelTech.Common.Testing.csproj" -c Release -o /app/packages

FROM base AS final
WORKDIR /packages
COPY --from=pack /app/packages .
