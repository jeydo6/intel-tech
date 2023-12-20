# intel-tech

Test task from [IntelTech](https://lahta-spb.ru) company.

## Tech stack:

    - .NET Core 3.1
    - PostgreSQL, MSSQL
    - Entity Framework
    - FluentValidation
    - MassTransit, RabbitMQ
    - MediatR
    - AutoMapper
    - Serilog
    - Docker

## Description:

### Within `Docker` run:

    - Bus (RabbitMQ)
    - 2 services that use RabbitMQ to message with each other
    - Database

### `Service # 1`:

    - Accept POST requests (first name, last name, middle name, phone number, e-mail)
    - Send the accepted data to the 'Service # 2' using the bus.
    - Validate the all fields except the middle name.
    - Log the accepted data.

### `Service # 2`:

    - Consume the message with data from 'Service # 1' and log the message into 'Console'.
    - Accept POST request that link a 'User' with 'Organization'.
    - Accept POST request that returns paginated users by an organization (using AutoMapper).
    - Seed data into 'Database' when the application starts if it is empty.

    - Table 'Users': UserId, OrganizationId, UserInfo from 'Service # 1'
    - Table 'Organizations': Id, Name

    - Write Unit-tests

## How to run the project

### Step 1. Build `IntelTeck.Common` `Docker`-image:

```shell
# Add new NuGet-source
dotnet nuget add source ~/Packages --name local

# Pack the packages
cd $pathToTheSolution/IntelTech.Common
find src -type d -depth 1 -exec dotnet pack {} --output ~/Packages \;

# Build the image
docker build --tag common-packages:0.2.0 .
```

### Step 2. Run the `docker-compose.yml`:

```shell
cd $pathToTheSolution
docker compose up -d
```