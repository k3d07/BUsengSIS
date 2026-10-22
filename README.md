<<<<<<< Updated upstream
# PKMG Student Information System

A desktop Student Information System built with C# Windows Forms and MySQL.
Midterm project for CS 106 (Applications Development), Bicol University.

**Group 5: PogiKamiMgaBuseng (PKMG)**

## Team

| Member | Role |
|---|---|
| Keith Bryan Belmonte | MySQL Setup & Database Connection |
| Sebastian Elli Ludovice | UI Layout & DataGridView Display |
| Kim Edrich Custodio | CRUD Logic (Add, Edit, Delete) |
| Julius Carullo | Error Handling & CherryTree Documentation |

## Features (planned)

- Add, view, edit, and delete student records
- Search by name or student ID
- Input validation and error handling
- Extras to be decided: login screen, quick filters, photo upload

## Tech Stack

- C# .NET Windows Forms (Visual Studio)
- MySQL
- Git & GitHub
- CherryTree (documentation)

## Getting Started

1. Clone the repo: `git clone https://github.com/k3d07/BUsengSIS.git`
2. Open the `.sln` file in Visual Studio
3. Import `database/schema.sql` into MySQL (coming soon)
4. Update the connection string with your own MySQL credentials
5. Press F5 to run

## Git Workflow

- Never commit directly to `main`
- Create a branch per task: `feature/<name>`
- Use commit messages like `feat: ...`, `fix: ...`, `docs: ...`
- Open a Pull Request and get one teammate's review before merging

## Status

🚧 In development. Deadline: October 23, 2026.
=======
BUseng SIS - Student Information System
Group 5: PogiKamiMgaBuseng (PKMG)
CS 106 Midterm Project - Bicol University (BSCS-2B)
GitHub: https://github.com/k3d07/BUsengSIS

MEMBERS AND ROLES
- Keith Bryan Belmonte: MySQL setup and database connection
- Sebastian Elli Ludovice: UI layout and DataGridView display
- Kim Edrich Custodio (Team Lead): CRUD logic, login back end, activity log, photos, CSV export, repo admin
- Julius Carullo: error handling, testing, documentation, demo video

REQUIREMENTS
- Windows
- Visual Studio (recent) with ".NET desktop development"; project targets .NET 10 (.slnx)
- MySQL Server and MySQL Workbench

HOW TO RUN
1. Unzip into a short folder path (for example C:\Projects\).
2. In MySQL Workbench: File > Open SQL Script > database\schema.sql > run it.
   This creates student_db, 3 tables, and 12 sample students.
3. In StudentInformationSystem\StudentInformationSystem\, copy
   App.config.example and rename the copy to App.config.
   Open it and replace YOUR_USER and YOUR_PASSWORD with your MySQL login.
4. Open StudentInformationSystem\StudentInformationSystem.slnx in Visual Studio.
5. Right-click the solution > Restore NuGet Packages, then Build > Rebuild Solution.
6. Press F5.

DEFAULT LOGIN (classroom demo account)
Username: admin
Password: admin123
(The app creates this account on first run, stored as a salted hash.)

NOTES
- Keep the "photos" folder next to "database" and "StudentInformationSystem"
  so student photos are found.
- If login fails because of an old admin row, run  DELETE FROM users;  once
  in MySQL Workbench and restart the app.
- Full documentation: docs\PKMG_SIS_Documentation.ctb (open with CherryTree)
- Demo video: included in the zip.
>>>>>>> Stashed changes
