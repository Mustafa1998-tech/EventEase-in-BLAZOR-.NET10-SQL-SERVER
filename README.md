# EventEase

EventEase is a Blazor Server web app for managing events (workshops, conferences, personal events) with a clean, production‑ready structure.

## Features
- Create / Read / Update / Delete (CRUD) events
- Pagination for fast lists
- Search and filters
- SQL Server + EF Core with indexes
- Password hashing + AES email encryption
- Reusable Blazor components
- Responsive UI (Bootstrap 5)

## Tech Stack
- Frontend: Blazor Server
- Backend: .NET 10 (C#)
- Database: SQL Server
- ORM: Entity Framework Core
- UI: Bootstrap 5

## Project Structure
```
EventEase
├── Models
│   ├── Event.cs
│   ├── User.cs
│   └── FutureOrTodayDateAttribute.cs
├── Data
│   ├── EventDbContext.cs
│   └── DbSeeder.cs
├── Services
│   ├── EventService.cs
│   ├── UserService.cs
│   └── EncryptionService.cs
├── Components
│   ├── Shared
│   │   ├── EventCard.razor
│   │   ├── EventForm.razor
│   │   └── AlertMessage.razor
│   ├── Pages
│   │   ├── EventList.razor
│   │   ├── AddEvent.razor
│   │   ├── EditEvent.razor
│   │   ├── EventDetails.razor
│   │   └── Login.razor
│   └── Layout
│       ├── MainLayout.razor
│       └── NavMenu.razor
└── wwwroot
    └── app.css
```

## Getting Started

### Prerequisites
- .NET 10 SDK
- SQL Server (or LocalDB)

### Configure
Update the connection string and seed admin in:
- `appsettings.json`
- `appsettings.Development.json`

Example:
```
"ConnectionStrings": {
  "DefaultConnection": "Server=DESKTOP-OKFV1DJ;Database=EventEaseDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
},
"SeedAdmin": {
  "Name": "Admin",
  "Email": "admin@eventease.local",
  "Password": "Admin@123"
}
```

### Run
```
dotnet restore
dotnet ef database update
dotnet run
```

For auto‑reload during development:
```
dotnet watch run --urls "http://127.0.0.1:5090"
```

## Default Admin
- Email: `admin@eventease.local`
- Password: `Admin@123`

## Performance
Indexes:
```
CREATE INDEX IX_Events_Date ON Events(EventDate);
CREATE INDEX IX_Events_Title ON Events(Title);
```

Pagination:
```
_context.Events
    .OrderBy(e => e.EventDate)
    .Skip((page - 1) * pageSize)
    .Take(pageSize);
```

## Security
- Password hashing with `PasswordHasher<T>`
- Email encryption with AES (encrypt at rest, decrypt only when needed)

## Future Enhancements
- Authentication & Authorization (roles)
- Advanced search and filters
- Export to PDF/Excel
- REST API + separate frontend

---
Author: Mustafa Ahmed
