# CovidApi
# COVID-19 API with PostgreSQL and Dev Containers

This project is a C# Web API built with **.NET 8**, using **Entity Framework Core** and **PostgreSQL** as the database. The development environment is containerized using **VS Code Dev Containers** for a seamless setup.

---

## **Project Features**
- **ASP.NET Core Web API**
- **PostgreSQL Database Integration (via Entity Framework Core)**
- **Docker-based Dev Container for easy setup**
- **Swagger UI for API Documentation**

---

## **Prerequisites**
Before running this project, make sure you have installed:
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [Visual Studio Code](https://code.visualstudio.com/) 
- [VS Code Dev Containers Extension](https://marketplace.visualstudio.com/items?itemName=ms-vscode-remote.remote-containers) 🔗
- PostgreSQL Database (Local or Docker)

---

## **Project Setup & Running the API**

### **Step 1: Clone the Repository**
```bash
git clone https://github.com/your-username/CovidApi.git
cd CovidApi
```
### **Step 2: Open in Dev Container**
1. Open **VS Code** in the project folder.
2. Press `Ctrl+Shift+P` (Windows/Linux) or `Cmd+Shift+P` (Mac).
3. Select **"Dev Containers: Reopen in Container"**.
4. Wait for the container to build and start.
   
### **Step 3: Running the API**
Inside the Dev Container terminal, run:
```bash
dotnet run
```
The API should start successfully and be available at:
- **Swagger UI**: {CovidApi_HostAddress}/swagger
- **Test API endpoint**:
```bash
GET {CovidApi_HostAddress}/api/Country/countries
```
- **Alternative: Test API Using .http File in VS Code**
1. Open the CovidApi.http file inside VS Code.
2. Click "Send Request" above any API request to test it.
**Example request inside CovidApi.http:**
```bash
### Get all countries
GET {CovidApi_HostAddress}/api/Country/countries
Accept: application/json
```
---

## **Environment Configuration**
The database connection is set in appsettings.json:

```bash
"ConnectionStrings": {
    "PostgreSqlConnection": "Host=host.docker.internal;Port=5432;Database=CovidReportSystem;Username=postgres;Password=123456"
}
```
### **Important Notes**
- **Inside Dev Container**: Use host.docker.internal instead of localhost.
- **For Local Development**: Change host.docker.internal to localhost.

---

## **Note: Updating Models from the Database (Database-First Approach)**
If the database schema changes and you need to regenerate the models in C#:

```bash
dotnet ef dbcontext scaffold "Host=host.docker.internal;Port=5432;Database=CovidReportSystem;Username=postgres;Password=123456" Npgsql.EntityFrameworkCore.PostgreSQL -o Models --context CovidDbContext --context-dir Data --force
```
**What This Does**:
- Auto-generates entity classes inside Data/Models/
-  Updates CovidDbContext.cs with the latest database schema
-  Use --force if you want to overwrite existing files

**Important**:
- Only run this command if the database schema has changed.
- Always review generated models before committing to Git.

---
## Design Patterns in This Project
In this project, I use two important design patterns: Repository and Unit of Work.

### **1. Repository Pattern**

#### What is Repository?
The repository pattern helps manage database logic separately from other parts of your app. It means your app doesn’t talk directly to the database. Instead, it uses a special class called a "repository" to handle all database actions.

#### Why use Repository?
- Keeps database code organized.
- Makes the code easy to manage.
- Easy to test.

#### Example:
- `ICovidCaseRepository` is the interface for database actions.
- `CovidCaseRepository` is the actual class that talks to the database using Entity Framework Core.
```bash
public interface ICovidCaseRepository : IRepository<CovidCase>
{
    IEnumerable<CovidCase> GetLatestCasesByCountry(string country);
}
```

### 2. Unit of Work Pattern

#### What is Unit of Work?
The Unit of Work groups many database actions together in one single transaction. This means all changes to the database happen together, making your data safer.

#### Why use Unit of Work?
- Helps keep your data safe by doing all database actions at once.
- Avoids repeating database connection code.
- Makes your code clear and organized.

#### Example:
- `IUnitOfWork` interface manages different repositories and save changes to the database.
- `UnitOfWork` class uses repositories and does the database transaction.
```bash
public interface IUnitOfWork : IDisposable
{
    ICovidCaseRepository CovidCases { get; }
    int Complete();
}
```
### Benefits of These Patterns:
- Clear Code: Easy to understand.
- Maintainable: Easy to change or update.
- Testable: Simple to write tests for your app.
- Safe Database Transactions: Ensures all database operations complete or fail together.

### How the Layers Work Together:
``` Controller → Service → Unit of Work → Repository → Database ```
| Layer  | What it Does |
| ------------- | ------------- |
| Controller  | Gets user requests and sends responses.  |
| Service  | Handles business logic and data mapping.  |
| Unit of Work  | Manages repositories and transactions.  |
| Repository  | Deals directly with the database.  |
| Database  | Stores all data.  |
