# Sales Lead Management API

## 1. Project Overview

This project is a Sales Lead Management API built using ASP.NET Core 8.

It allows users to create leads, view and update customer details, and add activities to track customer interactions.

## 2. Technologies Used

- C#
- ASP.NET Core 8 Web API
- Entity Framework Core
- SQLite
- Swagger

## 3. Project Structure

```text
SalesLeadManagement/
├── Controllers/
├── Data/
├── DTO/
├── Models/
├── Services/
├── Program.cs
└── appsettings.json
```

## 4. Architecture

I followed a simple layered architecture to keep the code organized.

- **Controller:** Handles API requests and responses.
- **Service:** Contains the application logic.
- **AppDbContext:** Handles database operations using EF Core.
- **SQLite:** Stores leads and their activities.

### Request Flow

Client → Controller → Service → AppDbContext → SQLite

## 5. Database Design

The application has two main entities:

**Lead:** Stores customer name, email, phone number, notes, and other lead details.

**LeadActivity:** Stores activity type, description, and creation date.

One lead can have multiple activities. Each activity belongs to one lead.

## 6. Features

- Create a new lead.
- View all leads.
- Get lead details by ID.
- Update lead information.
- Add activities to a lead.
- Store lead and activity details in SQLite.
- Handle unexpected errors.

## 7. API Endpoints

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/SaleLead` | Get all leads |
| GET | `/api/SaleLead/{id}` | Get lead by ID |
| POST | `/api/SaleLead` | Create a lead |
| PUT | `/api/SaleLead/{id}` | Update a lead |
| POST | `/api/SaleLead/{id}/activities` | Add an activity |

## 8. How to Run the Project

### Prerequisites

- .NET 8 SDK
- Visual Studio 2022

### Steps

**1. Clone the repository**

```bash
git clone <YOUR_GITHUB_REPOSITORY_URL>
```

**2. Restore packages**

```bash
dotnet restore
```

**3. Update the database**

```bash
dotnet ef database update
```

**4. Run the application**

```bash
dotnet run
```

Open the Swagger URL displayed in the terminal to test the APIs.

## 9. Error Handling

I added middleware to handle unexpected errors. It logs the error and returns a common error response instead of exposing internal exception details.

## 10. AI Collaboration

I used ChatGPT during development to understand concepts, discuss the project structure, review code, and troubleshoot errors.

I reviewed the suggestions, made changes where needed, and tested the implementation before using the code in my project.

AI helped me understand different approaches and save development time, but I was responsible for the final implementation and verification.

## 11. Future Improvements

- Add automated unit and integration tests.
- Add authentication and authorization.
- Add pagination and sorting.
- Improve logging and monitoring.
