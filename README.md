# Personal Finance Tracker

## About
This is my semester project for Advanced Programming (COSC-5136). I built a desktop application in C# that helps users manage their personal finances — tracking expenses, setting budgets, and viewing spending summaries.

## Tech Stack
- C# .NET Framework 4.7.2
- Windows Forms
- SQL Server + ADO.NET
- LiveCharts2 for charts

## Project Structure
The project follows the architecture we learned in class — two separate projects in one solution:

- `App.Core` — contains all the business logic
  - Models (Expense, Category, Budget)
  - Contracts/Interfaces
  - InMemory and Database service implementations
  
- `PersonalFinanceTracker` — the Windows Forms UI
  - Views for each module
  - Popup forms for Add/Edit operations

## Features
- Add, edit, delete expense categories
- Track expenses with search and category filter
- Set budgets per category with date ranges
- View total expenses, budget, and remaining balance
- Charts showing spending by category and by month

## Database
Create a SQL Server database named `FinanceTrackerDB` with these tables:

```sql
CREATE TABLE Categories (
    CategoryId NVARCHAR(50) PRIMARY KEY,
    CategoryName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255)
)

CREATE TABLE Expenses (
    ExpenseId NVARCHAR(50) PRIMARY KEY,
    CategoryId NVARCHAR(50) NOT NULL,
    Description NVARCHAR(255),
    Amount DECIMAL(18,2) NOT NULL,
    ExpenseDate DATE NOT NULL
)

CREATE TABLE Budgets (
    BudgetId NVARCHAR(50) PRIMARY KEY,
    CategoryId NVARCHAR(50) NOT NULL,
    BudgetAmount DECIMAL(18,2) NOT NULL,
    SpentAmount DECIMAL(18,2) NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL
)
```
## How to Run
1. Clone this repo
2. Open FinanceTrackerSystem.sln in Visual Studio
3. Set up the database using the SQL above
4. Update the connection string in App.config to match your SQL Server
5. Build the solution and run

## Course
Advanced programming
