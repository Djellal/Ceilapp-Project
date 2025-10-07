# Ceilapp Project

## Project Overview

Ceilapp is a .NET 9.0 web application built with ASP.NET Core Blazor Server. It appears to be an educational management system that handles course management, student registration, evaluations, and administrative functions. The application uses Entity Framework Core with PostgreSQL as the database backend and implements role-based authentication with user roles including ADMIN, STUDENT, and TEACHER.

Key technologies and features:
- ASP.NET Core 9.0 with Blazor Server
- Entity Framework Core with PostgreSQL
- Entity Framework migrations for database management
- Role-based authentication with ASP.NET Core Identity
- Radzen Blazor components for UI
- OData support for data querying
- PDF generation (QuestPDF)
- PuppeteerSharp for web scraping/printing
- CSV and Excel export capabilities
- Multi-language support with localization
- Algerian location data (states and municipalities)

## Architecture

The application follows a standard ASP.NET Core MVC/Blazor architecture with:

- **Models**: Entity Framework models for the database entities
- **Data**: DbContext and database context configurations
- **Services**: Business logic services (CeilappService, SecurityService, ReportService)
- **Components**: Blazor UI components
- **Controllers**: API controllers with OData support
- **Filters**: Custom filters
- **wwwroot**: Static assets (CSS, JS, images)

## Main Entities

The core domain entities include:
- Courses and Course Types
- Course Components and Levels 
- Sessions for course scheduling
- Groups for student organization
- Course Registrations for student enrollments
- Evaluations for student assessments
- Professions for career information
- States and Municipalities (Algerian locations)
- ApplicationUsers with role-based access

## Building and Running

### Prerequisites
- .NET 9.0 SDK
- PostgreSQL database

### Setup
1. Configure the database connection string in `appsettings.json` (currently configured to connect to `Host=127.0.0.1;User ID=postgres;Password=ufaspg2017;Database=ceilapp`)
2. Run database migrations (automatically handled by `Program.cs`)

### Running the Application
The application is configured to run on `http://localhost:5111`:
```bash
dotnet run
```

### Development Commands
```bash
# Build the project
dotnet build

# Run the project
dotnet run

# Run with specific URL
dotnet run --urls=http://localhost:5111
```

## Database Migrations

The application uses Entity Framework Core migrations (located in `Data\Migrations`) to manage database schema changes. The application automatically runs pending migrations on startup.

## Key Services

- **CeilappService**: Primary business logic service with CRUD operations for all entities
- **SecurityService**: Handles authentication and authorization
- **ReportService**: Generates reports and handles document operations
- **ApplicationAuthenticationStateProvider**: Manages authentication state for Blazor components

## Authentication and Authorization

The application implements role-based authentication using ASP.NET Core Identity with three predefined roles:
- ADMIN
- STUDENT
- TEACHER

Password policies are configured with minimum length of 6 characters and no additional requirements.

## Environment Configuration

The application is configured to run in both development and production environments:
- Development: Uses localhost:5111 as the URL
- Production: Uses HSTS with default 30-day configuration
- SMTP settings configured for email delivery

## Special Features

- Course registration with fee management
- Multi-language support (English/Arabic based on field names)
- Algerian location data integration (states and municipalities)
- Evaluation system for student assessments
- Group management for organizing students
- Data export to Excel and CSV formats
- PDF generation capabilities