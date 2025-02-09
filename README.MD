# OrgChart Test Task

This task is designed to assess your ability to design and implement a layered solution that manages a hierarchical organizational structure. Your goal is to implement the repository and service layers so that the provided integration tests pass.

## Instructions

1. **Clone the Repository:**  
   Clone this repository to your local machine.
  
2. **Create a New Branch:**  
   Create a new branch with your name (e.g. `john-petrov`). All your changes should be committed to this branch.

3. **Implement the Solution:**  
   Implement the repository and service layers in the `OrgChart.Core` and `OrgChart.Infrastructure` projects. You can add new classes, interfaces, or methods as needed.

4. **Run the Tests:**
    Run the integration tests to verify that your implementation is correct.

5. **Commit Your Changes:**
    Commit your changes to the branch and push it to the remote repository.

6. **Create a Pull Request:**  
   Create a pull request from your branch to the `main` branch. Send the pull request to the reviewer.

7. **Review:**  
   The reviewer will review your code and provide feedback.

## Overview

The API manages employees in an organization and enforces the following business rules:

- **Cycle Prevention:**  
  When assigning or updating a manager, the solution must detect cycles at any level (e.g. if emp1 is a manager of emp2, and emp2 is set as a manager for emp1 or any other indirect cycle, the operation must fail).

- **Maximum Hierarchy Depth:**  
  The maximum allowed depth for the employee hierarchy is **5 levels**. Any insertion or update that would exceed this depth must be rejected.

- **Subordinate Count:**  
  The API should return an employee along with the aggregated count of all its direct and indirect subordinates.

- **Deletion Handling:**  
  When an employee is deleted, their subordinates are reassigned to the deleted employee’s manager.

## Database Structure

The solution uses a PostgreSQL database with a single table:

- **orgchart.employees**  
  - **id:** `int` (primary key, auto-increment)  
  - **name:** `text`  
  - **manager_id:** `int` (nullable, foreign key referencing `employees.Id`)

## Project Structure

```text
OrgChart/
├── src/
│   ├── OrgChart.API           // ASP.NET Core Web API (Controllers, Program.cs, Middleware, etc.)
│   ├── OrgChart.Core          // Domain models, business logic, use cases, exceptions, constants, DI registration, etc.
│   └── OrgChart.Infrastructure // EF Core DbContext, migrations, repository implementations, etc.
└── └── tests/
        └──OrgChart.IntegrationTests // Integration tests (using Testcontainers & Docker)
```

## Requirements

- **.NET 9 SDK**  
  This project requires the .NET 9 SDK.

- **Docker**  
  Docker is needed for running integration tests, which use Testcontainers to spin up a PostgreSQL instance.

## Running the Application

1. **Ensure Docker Is Running:**  
   Make sure Docker is installed and running on your machine.

2. **Build the Solution:**

   ```bash
   dotnet build
   ```

3. **Apply Migrations:**  
   The API is configured to apply migrations automatically on startup. You can also run migrations manually:

   ```bash
   dotnet ef database update --project OrgChart.Infrastructure --startup-project OrgChart.API
   ```

4. **Run the API:**

   ```bash
   dotnet run --project OrgChart.API
   ```

## Running the Tests

The integration tests use Testcontainers to automatically start a PostgreSQL container.

1. **Navigate to the Tests Folder:**

   ```bash
   cd tests/OrgChart.IntegrationTests
   ```

2. **Run the Tests:**

   ```bash
   dotnet test
   ```

The tests cover the following scenarios:

- **Creation:**  
  - Valid employee creation.
  - Creation with an invalid manager.
  - Creation when maximum depth is exceeded.
  
- **Retrieval (GET):**  
  - Retrieving an existing employee (with subordinate count).
  - Handling a non-existent employee (404).

- **Update:**  
  - Successful updates.
  - Updates with an invalid manager.
  - Cycle detection (both immediate and deeper cycles).

- **Deletion:**  
  - Successful deletion and subsequent 404 on retrieval.
  - Deletion of a non-existent employee.
