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