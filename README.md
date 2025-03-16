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
