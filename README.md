
# Leave Management System API

This is a sample Leave Management System API built using ASP.NET Core 8, EF Core with SQLite, and Docker. The application supports CRUD operations, filtering, reporting, and basic business rules for managing employee leave requests.

## 🚀 Features
- CRUD operations for leave requests
- Filtering, sorting, searching and pagination
- Business rules (no overlap, annual day limit, sick leave validation)
- Reporting by year and department
- Docker and Docker Compose support
- Uses design patterns (Repository, Strategy, Factory, Singleton)
- Ready to deploy

## 🛠️ Tech Stack
- ASP.NET Core 8
- Entity Framework Core (with SQLite)
- AutoMapper & DTO
- Docker & Docker Compose
- LINQ + Predicate Builder

---

## ⚙️ Project Setup

```bash
# Clone the repository
git clone https://github.com/ikramaitkaddi/leaveManagementSystem.git
cd leaveManagementSystem

# Apply database migrations
dotnet ef database update

# Run the application
dotnet run
```

Access the API at: `https://localhost:port/swagger/index.html`

---

## 🐳 Docker Setup

### Prerequisites
- Docker Desktop installed

### Run with Docker Compose

```bash
docker-compose up --build
```

### Services
- `app`: ASP.NET Core Web API (listening on port `8080`) & SQLite file volume mounted inside container

Visit: `http://localhost:8080/swagger/index.html`

---

## 📬 Postman Collection

A Postman collection is available in the repository for easy testing of:
- CRUD endpoints
- Filtering endpoint (`/api/leaverequests/filter`)
- Report endpoint (`/api/leaverequests/report`)
- Admin approval (`/api/leaverequests/{id}/approve`)

### Import
1. Open Postman.
2. Click on `Import` > `File` and select `LeaveManagementSystem.postman_collection.json`.

---

## 🧩 Design Patterns Used

- **Repository Pattern**: Abstracts data access via interfaces.
- **Strategy Pattern**: Handles different validation logic for leave types.
- **Factory Pattern**: Generates report in desired format (JSON, CSV).
- **Singleton Pattern**: Manages centralized config/logging service.

---

## ✅ To Do (optional)
- Add unit tests with xUnit
- Implement authentication/authorization
- Enhance UI for end-user interaction

---

## 📄 License
This project is licensed under the MIT License.

---

**Developed by: Ikram Ait Kaddi – Software Engineer**

