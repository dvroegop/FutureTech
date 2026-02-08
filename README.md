# FutureTech - Ticket Management System

A modern .NET 10 ASP.NET Core Web API backend for managing tickets/cases, built with Clean Architecture principles.

## 🏗️ Architecture

This project follows Clean Architecture with a clear separation of concerns:

```
FutureTech/
├── src/
│   ├── FutureTech.Domain/          # Core business entities and value objects
│   ├── FutureTech.Application/     # Business logic, CQRS commands/queries
│   ├── FutureTech.Infrastructure/  # Data access, EF Core, SQLite
│   └── FutureTech.Api/             # REST API controllers, configuration
```

### Layer Dependencies

- **Domain**: No dependencies (pure business logic)
- **Application**: Depends on Domain
- **Infrastructure**: Depends on Application
- **Api**: Depends on Application and Infrastructure

## 🚀 Features

- **Clean Architecture** - Organized in 4 solution folders for maintainability
- **CQRS Pattern** - Using MediatR for command and query separation
- **Entity Framework Core** - With SQLite for data persistence
- **Auto-migrations** - Database is automatically created and seeded in Development
- **RESTful API** - Complete CRUD operations for tickets and comments
- **Swagger/OpenAPI** - Interactive API documentation
- **Validation** - FluentValidation for request validation
- **Seed Data** - Pre-populated sample tickets and comments in Development mode

## 📋 Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) (10.0.102 or later)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (recommended) or Visual Studio Code
- Any modern web browser (for Swagger UI)

## 🛠️ Getting Started

### Option 1: Using Visual Studio (F5)

1. **Clone the repository**
   ```bash
   git clone https://github.com/dvroegop/FutureTech.git
   cd FutureTech
   ```

2. **Open the solution**
   - Double-click `FutureTech.sln` or open it from Visual Studio
   - Visual Studio will automatically restore NuGet packages

3. **Set the startup project**
   - Right-click on `FutureTech.Api` project → Set as Startup Project

4. **Run the application**
   - Press **F5** or click the **Run** button
   - The API will start and automatically open Swagger UI in your browser
   - Database will be automatically created and seeded with sample data

### Option 2: Using Command Line

1. **Clone the repository**
   ```bash
   git clone https://github.com/dvroegop/FutureTech.git
   cd FutureTech
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Build the solution**
   ```bash
   dotnet build
   ```

4. **Run the API**
   ```bash
   cd src/FutureTech.Api
   dotnet run
   ```

5. **Access Swagger UI**
   - Open your browser and navigate to: `https://localhost:7XXX/swagger` (port may vary)
   - Or use the URL displayed in the console output

## 📚 API Endpoints

### Tickets

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/tickets` | Get all tickets with their comments |
| GET | `/api/tickets/{id}` | Get a specific ticket by ID |
| POST | `/api/tickets` | Create a new ticket |
| PUT | `/api/tickets/{id}` | Update an existing ticket |
| DELETE | `/api/tickets/{id}` | Delete a ticket |

### Comments

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/comments/ticket/{ticketId}` | Get all comments for a ticket |
| POST | `/api/comments` | Create a new comment on a ticket |
| PUT | `/api/comments/{id}` | Update an existing comment |
| DELETE | `/api/comments/{id}` | Delete a comment |

## 📊 Data Models

### Ticket
- **Id**: Unique identifier
- **Title**: Ticket title (required, max 200 chars)
- **Description**: Detailed description (optional, max 2000 chars)
- **Status**: Open, InProgress, Resolved, Closed
- **Priority**: Low, Medium, High, Critical
- **CreatedAt**: Timestamp when created
- **UpdatedAt**: Timestamp when last updated
- **Comments**: Collection of associated comments

### Comment
- **Id**: Unique identifier
- **Content**: Comment text (required, max 1000 chars)
- **Author**: Comment author name (optional, max 100 chars)
- **TicketId**: Associated ticket ID
- **CreatedAt**: Timestamp when created
- **UpdatedAt**: Timestamp when last updated

## 🔧 Configuration

### Database

The application uses SQLite by default. The connection string is configured in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=ticketmanagement.db"
  }
}
```

The database file (`ticketmanagement.db`) will be created automatically in the API project directory when you first run the application in Development mode.

### Development Mode

In Development environment, the application:
- Automatically applies EF Core migrations
- Seeds the database with sample data
- Enables Swagger UI at `/swagger`
- Provides detailed error messages

## 🧪 Sample Data

When running in Development mode, the following sample tickets are automatically created:

1. **Setup development environment** (Closed, High priority)
2. **Implement user authentication** (In Progress, Critical priority)
3. **Create API documentation** (Open, Medium priority)
4. **Fix bug in comment deletion** (Open, High priority)
5. **Add unit tests** (Open, Low priority)

Sample comments are also added to demonstrate the relationship between tickets and comments.

## 📁 Project Structure

### Domain Layer
- `Entities/` - Core business entities (Ticket, Comment)
- `Common/` - Shared base classes and interfaces

### Application Layer
- `DTOs/` - Data Transfer Objects
- `Tickets/Commands/` - Ticket command handlers (Create, Update, Delete)
- `Tickets/Queries/` - Ticket query handlers (GetAll, GetById)
- `Comments/Commands/` - Comment command handlers
- `Comments/Queries/` - Comment query handlers
- `Common/Interfaces/` - Application interfaces

### Infrastructure Layer
- `Data/` - EF Core DbContext and database seeding
- Database migrations (auto-applied in Development)

### API Layer
- `Controllers/` - REST API controllers
- `Program.cs` - Application configuration and startup

## 🔒 Security Note

This is a demo application. For production use, consider adding:
- Authentication and authorization
- Input validation and sanitization
- Rate limiting
- CORS configuration
- HTTPS enforcement
- Logging and monitoring
- Error handling middleware

## 🛡️ What's NOT Included

This project intentionally does NOT include:
- ❌ AI features
- ❌ LLM integrations
- ❌ OpenAI services
- ❌ MCP (Model Context Protocol)
- ❌ Frontend application
- ❌ Authentication/Authorization

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## 📮 Support

For issues and questions, please use the GitHub Issues page.
