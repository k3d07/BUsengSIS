# BUseng SIS – Student Information System

A desktop Student Information System built with C# Windows Forms and MySQL.
Midterm project for **CS 106 (Applications Development & Emerging Technologies)**, Bicol University.

**Group 5: PogiKamiMgaBuseng (PKMG)**
Repository: https://github.com/k3d07/BUsengSIS

---

## Team

| Member | Role |
|---|---|
| Keith Bryan Belmonte | MySQL Setup & Database Connection |
| Sebastian Elli Ludovice | UI Layout & DataGridView Display |
| Kim Edrich Custodio (Team Lead) | CRUD Logic, Repo Admin |
| Julius Carullo | Error Handling & CherryTree Documentation |

---

## Features

- **Login** with a salted-hash (PBKDF2) password and **Logout**
- **Add** a student (duplicate Student ID is blocked)
- **View** all students in a grid
- **Search** by name or Student ID (partial, case-insensitive)
- **Filter** by Year Level and Program, with a Reset button
- **Edit** a student (click a row, change the fields, click Update)
- **Delete** with an "Are you sure?" Yes/No confirmation
- **Input validation** (blank fields, spaces only, too long, bad format) with friendly messages
- **Error handling** on every database action; the app shows a message instead of crashing, even if MySQL is stopped
- **Profile photo** upload with preview (JPG/PNG, up to 2 MB)
- **Dashboard cards** (total students and count per year level) and a "Showing X of Y" counter
- **Export CSV** of exactly what the grid shows (respects search and filters; opens in Excel)
- **Activity log** (LOGIN, ADD, EDIT, DELETE, EXPORT) with a View Log screen
- **Status bar** showing the logged-in user and the date/time
- Optional **email** field, validated only when filled

---

## Requirements

- Windows
- Visual Studio (recent version with the **.NET desktop development** workload; the project targets **.NET 10** and uses the `.slnx` solution format)
- MySQL Server and MySQL Workbench
- NuGet package: `MySqlConnector` (restored automatically)

---

## How to run

1. **Get the code.** Clone the repo or unzip the submission:
   `git clone https://github.com/k3d07/BUsengSIS.git`
2. **Create the database.** Open MySQL Workbench, then **File → Open SQL Script** → `database/schema.sql` → run it (lightning bolt). It creates `student_db`, the 3 tables, and 12 sample students. You can run it more than once.
3. **Set your connection.** In `StudentInformationSystem/StudentInformationSystem/`, copy `App.config.example` and rename the copy to `App.config`. Open it and replace `YOUR_USER` and `YOUR_PASSWORD` with your own MySQL login. Check that `Server`, `Port`, and `Database=student_db` match your setup.
4. **Open the solution.** Open `StudentInformationSystem/StudentInformationSystem.slnx` in Visual Studio.
5. **Build.** **Build → Rebuild Solution** (it should show 0 errors). If NuGet packages are missing, right-click the solution → **Restore NuGet Packages**.
6. **Run.** Press **F5**.

### Default login

| Username | Password |
|---|---|
| `admin` | `admin123` |

This is a classroom demo account. The app creates it automatically on the first run (stored as a salted hash, never as plain text).

> If login fails and the `users` table already has an old `admin` row from an earlier version, run `DELETE FROM users;` in Workbench once, then restart the app. It recreates the admin.

---

## Database

Database: `student_db` (`utf8mb4`)

| Table | Purpose |
|---|---|
| `students` | Student records (`student_id` is the primary key; `email` and `photo_path` are optional) |
| `users` | The admin account (username and salted password hash) |
| `activity_log` | Who did what and when (no foreign key, so deleting a student never deletes log entries) |

Dropdown values used by the app, the database, and validation:

- **Gender:** Male, Female, Other
- **Year Level:** 1st Year, 2nd Year, 3rd Year, 4th Year
- **Program:** BS Computer Science, BS Information Technology, BS Information Systems, BS Computer Engineering

---

## Project structure

```
BUsengSIS/
├── README.md
├── database/                  schema.sql (tables and sample data)
├── docs/                      CherryTree documentation
├── photos/                    student photos (saved as <student_id>.<ext>)
└── StudentInformationSystem/
    ├── StudentInformationSystem.slnx
    └── StudentInformationSystem/
        ├── Models/            Student.cs
        ├── Data/              DbHelper, StudentRepository, UserRepository, ActivityLogRepository
        ├── Helpers/           Validator, PhotoStore
        ├── Security/          PasswordHasher, Session
        ├── MainForm*.cs       main window (layout, logic, CRUD handlers)
        ├── LoginForm.cs       login screen
        ├── LogForm.cs         activity log screen
        ├── Program.cs         startup flow
        └── App.config.example connection string template
```

**Startup flow:** check the database → Login → Main window → Logout returns to Login.

---

## Notes

- `App.config` holds your own MySQL password and is **not** committed to Git. Always copy it from `App.config.example`.
- The `photos/` folder must stay next to `database/` and `StudentInformationSystem/` so uploaded photos are found.
- The photo files themselves are not tracked by Git (only `.gitkeep` is).

---

## Documentation

Full technical documentation (team overview, database info, setup guide, app walkthrough with screenshots) is in the CherryTree file:
`docs/PKMG_SIS_Documentation.ctb`

---

## Git workflow used

- Nobody commits directly to `main`; each member works on their own branch
- Commit messages follow `type: description` (`feat`, `fix`, `docs`, `style`, `refactor`, `test`, `chore`)
- Every change goes through a Pull Request with one teammate's approval