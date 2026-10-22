# Docs Content Guide – CherryTree + README.txt

Paste-ready text for every CherryTree node and for `README.txt`. Whoever fills the file (Julius, or Kim if Julius misses his deadline) follows this top to bottom.

**Rules**
- The file is `docs/PKMG_SIS_Documentation.ctb`.
- It needs **exactly 4 top-level nodes**, with these exact names: `Team & Overview`, `Database Info`, `Setup Guide`, `App Walkthrough`. Sub-nodes inside them are fine.
- Only **one person** edits and commits the `.ctb` at a time. It can't be merged by hand.
- Write only what the app really does. Export, import, and the status bar are not guaranteed to be shown in the video, so mention only what you tested.

---

## Part A. Fix one thing in "Team & Overview" first

The current "Project Description" says the app organizes *"courses and grades through connected database tables"*. **That's not true.** The database has only 3 tables: `students`, `users`, `activity_log`. A grader who opens the schema will notice. Replace that paragraph with the one below.

### Node 1: `Team & Overview`

Keep the existing Project Information block (title, course, instructor, group, repo link). Replace the description and features with:

> **Project Description**
> BUseng SIS is a desktop Student Information System built with C# Windows Forms (.NET 10) and a MySQL database. An administrator logs in and can add, view, search, filter, edit, and delete student records. The app validates all input, protects against SQL injection with parameterized queries, stores passwords as salted hashes, and shows friendly messages instead of crashing when something goes wrong.
>
> **Key Features**
> - Login with a salted-hash (PBKDF2) password, and Logout
> - Add a student (duplicate Student ID is blocked)
> - View all students in a grid
> - Search by name or Student ID (partial, case-insensitive)
> - Filter by Year Level and Program, with Reset
> - Edit a student (click a row, change the fields, Update)
> - Delete with an "Are you sure?" Yes/No confirmation
> - Input validation (blank, spaces only, too long, wrong format)
> - Optional email field
> - Student photo upload with preview
> - Dashboard cards (total and count per year level) and a "Showing X of Y" counter
> - Activity log (LOGIN, ADD, EDIT, DELETE, EXPORT) with a View Log screen
> - Export the current grid to CSV

**Tools:** C# .NET 10 Windows Forms, MySQL Server and Workbench, MySqlConnector, Visual Studio, Git and GitHub, CherryTree.

**Team Members and Tasks**

| Member | Role | Main tasks |
|---|---|---|
| Keith Bryan Belmonte | MySQL Setup & Database Connection | Database schema and sample data, `DbHelper`, connection config, database testing |
| Sebastian Elli Ludovice | UI Layout & DataGridView Display | Main window and login screen layout, grid, branding, filters UI |
| Kim Edrich Custodio (Team Lead) | CRUD Logic & Repo Admin | Student model, repository queries, Add/Update/Delete, validation, login back end, activity log, photos, CSV export, repository management, final package |
| Julius Carullo | Error Handling & Documentation | Error handling review, manual testing, CherryTree documentation, README, demo video |

> Edit this table if the real split differs. It must match what each person actually did.

---

## Part B. Fill the other 3 nodes

### Node 2: `Database Info`

Keep the two sub-nodes ("Database Overview" and "SQL Script") if they're there.

**In "Database Overview", paste:**

> **Database:** `student_db` (character set `utf8mb4`)
> **Engine:** MySQL 8, InnoDB
>
> The database has 3 tables.
>
> **`students`**: the student records
>
> | Column | Type | Rules |
> |---|---|---|
> | `student_id` | VARCHAR(20) | PRIMARY KEY |
> | `name` | VARCHAR(100) | NOT NULL |
> | `gender` | VARCHAR(10) | NOT NULL |
> | `year_level` | VARCHAR(20) | NOT NULL |
> | `program` | VARCHAR(100) | NOT NULL |
> | `contact_number` | VARCHAR(20) | NOT NULL |
> | `email` | VARCHAR(100) | NULL allowed (optional) |
> | `photo_path` | VARCHAR(255) | NULL allowed (file name only) |
>
> **`users`**: the login account
>
> | Column | Type | Rules |
> |---|---|---|
> | `user_id` | INT | PRIMARY KEY, AUTO_INCREMENT |
> | `username` | VARCHAR(50) | NOT NULL, UNIQUE |
> | `password_hash` | VARCHAR(255) | NOT NULL (salted PBKDF2 hash, never plain text) |
>
> **`activity_log`**: who did what and when
>
> | Column | Type | Rules |
> |---|---|---|
> | `log_id` | INT | PRIMARY KEY, AUTO_INCREMENT |
> | `username` | VARCHAR(50) | NOT NULL |
> | `action` | VARCHAR(20) | NOT NULL (LOGIN, ADD, EDIT, DELETE, EXPORT) |
> | `student_id` | VARCHAR(20) | NULL allowed |
> | `logged_at` | DATETIME | NOT NULL, DEFAULT CURRENT_TIMESTAMP |
>
> **Design decisions**
> - `student_id` is the primary key, so two students can never share an ID.
> - `activity_log` has **no foreign key** to `students`, so deleting a student never deletes or blocks the log history.
> - `email` and `photo_path` are optional, so they allow NULL. An empty email is saved as NULL.
> - `schema.sql` contains no password. The app creates the `admin` account on first run and stores it as a salted hash.
> - The script can be run more than once. Only `students` is dropped and recreated (this resets the sample data); `users` and `activity_log` are never dropped.
> - The sample data has 12 students, including one with an apostrophe (`O'Brien`) to prove special characters are handled.

**In "SQL Script", do this:**
1. Open `database/schema.sql` from the repo.
2. Select all, copy.
3. In CherryTree, use **Insert → Code Box** (set the syntax to SQL) and paste the whole script.

**Screenshots to add** (from Keith, or take them yourself in MySQL Workbench):
- `db-tables.png`: the Navigator showing the 3 tables under `student_db`
- `db-students-structure.png`: the `students` table structure (right-click the table → Table Inspector or Alter Table)
- `db-students-data.png`: `SELECT * FROM students;` showing the 12 rows

### Node 3: `Setup Guide`

Paste:

> **Requirements**
> - Windows
> - Visual Studio (recent version) with the **.NET desktop development** workload. The project targets **.NET 10** and uses the `.slnx` solution format, so older versions can't open it.
> - MySQL Server and MySQL Workbench
> - The `MySqlConnector` NuGet package (restored automatically)
>
> **Steps**
> 1. **Get the project.** Unzip the submission (or clone `https://github.com/k3d07/BUsengSIS.git`) into a short folder path, for example `C:\Projects\`.
> 2. **Create the database.** Open MySQL Workbench and connect to your local server. Choose **File → Open SQL Script**, select `database/schema.sql`, and run it with the lightning bolt button. It creates `student_db`, the 3 tables, and 12 sample students.
> 3. **Check the data.** Run `SELECT * FROM students;` and confirm there are 12 rows.
> 4. **Set the connection.** In `StudentInformationSystem\StudentInformationSystem\`, copy `App.config.example` and rename the copy to `App.config`. Open it and replace `YOUR_USER` and `YOUR_PASSWORD` with your own MySQL login. Check that `Server`, `Port`, and `Database=student_db` match your setup.
> 5. **Open the solution.** Open `StudentInformationSystem\StudentInformationSystem.slnx` in Visual Studio.
> 6. **Restore and build.** Right-click the solution → **Restore NuGet Packages**, then **Build → Rebuild Solution**. It should show 0 errors.
> 7. **Run.** Press **F5**.
> 8. **Log in** with the default account below.
>
> | Username | Password |
> |---|---|
> | `admin` | `admin123` |
>
> This is a classroom demo account. The app creates it automatically on first run.
>
> **Troubleshooting**
>
> | Problem | Fix |
> |---|---|
> | "Could not connect to the database" | Start MySQL, then check the user, password, port, and database name in `App.config` |
> | Login fails after an old run | In Workbench run `DELETE FROM users;` once, then restart the app (it recreates the admin) |
> | The grid is empty | Close the app, confirm `schema.sql` ran (12 rows), then run the app again |
> | Can't open the `.slnx` | Update Visual Studio |
> | Build error about `MySqlConnector` | Right-click the solution → **Restore NuGet Packages**, then rebuild |

**Before you finish this node:** have someone who isn't Kim follow these steps once on a clean folder, and fix any step that fails. Do not paste a real MySQL password anywhere.

### Node 4: `App Walkthrough`

Make one sub-node per screen or feature (or one long page with headings). For each item, add the screenshot and a one- or two-line caption.

| # | Item | Screenshot file | Caption to write |
|---|---|---|---|
| 1 | Login | `ui-login.png` | Login screen. Wrong or empty credentials show a friendly message. Passwords are checked against a salted hash. |
| 2 | Main window | `ui-main-form.png` | Main window: dashboard cards, Student Details form, search, filters, grid, and counter. |
| 3 | Add student | `ui-add.png` | Adding a student. A blank field or a duplicate ID is blocked with a message. |
| 4 | Validation | `ui-validation.png` | Example of a validation error (bad contact number or email). |
| 5 | Search | `ui-search.png` | Search by name or Student ID. Partial and case-insensitive. |
| 6 | Filters | `ui-filters.png` | Filter by Year Level and Program, combined with search. Reset clears them. |
| 7 | Edit | `ui-edit.png` | Clicking a row fills the form and locks the Student ID. Update saves the changes. |
| 8 | Delete | `ui-delete.png` | Delete asks "Are you sure?" with Yes/No. No is the default. |
| 9 | Photo | `ui-photo.png` | Browse and preview a student photo (JPG/PNG, up to 2 MB). |
| 10 | View Log | `ui-viewlog.png` | Activity log: time, user, action, and student ID, newest first. |
| 11 | Export CSV | `ui-export.png` | Export saves exactly what the grid shows to a CSV file that opens in Excel. |
| 12 | Error handling | `ui-mysql-stopped.png` | With MySQL stopped, the app shows a message and does not crash. |

Skip any row for something that isn't working or wasn't built.

**Code snippets** (use **Insert → Code Box**, set the syntax to C#, add a one-line caption). Copy these from the real files:

| Snippet | File to copy from |
|---|---|
| Parameterized insert (stops SQL injection) | `Data\StudentRepository.cs`, the `AddStudent` method |
| Add handler with validation and try-catch | `MainForm.Crud.cs`, `btnAdd_Click` |
| Delete confirmation (Yes/No) | `MainForm.Crud.cs`, `btnDelete_Click` |
| Input validation rules | `Helpers\Validator.cs`, `IsValidStudent` |
| Password hashing | `Security\PasswordHasher.cs` |

Add this note under the snippets:

> **Safety design:** every query uses parameters, never string-joined SQL (prevents SQL injection and apostrophe bugs such as `O'Brien`). The repository does not catch errors; the button handlers catch `MySqlException` and general exceptions and show friendly messages. Startup flow: check the database, then Login, then Main window, and Logout returns to Login.

---

## Part C. Screenshot checklist (collect these first)

Sebastian takes the UI ones. Name the files exactly as in the tables.

- [ ] `ui-login.png`
- [ ] `ui-main-form.png`, with data loaded
- [ ] `ui-add.png` (filled form)
- [ ] `ui-validation.png` (an error message)
- [ ] `ui-search.png`
- [ ] `ui-filters.png`
- [ ] `ui-edit.png` (a row selected, ID locked)
- [ ] `ui-delete.png` (confirmation popup)
- [ ] `ui-photo.png`
- [ ] `ui-viewlog.png`
- [ ] `ui-export.png`
- [ ] `ui-mysql-stopped.png`
- [ ] `db-tables.png`, `db-students-structure.png`, `db-students-data.png` (Keith)

---

## Part D. `README.txt` (for the zip)

Copy everything between the lines into a plain text file named `README.txt` at the top level of the zip.

```
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
```

---

## Part E. Order of work (about 1 to 1.5 hours)

1. Fix the false sentence in **Team & Overview** (Part A). 5 min
2. Fill **Database Info**: paste the text, add the SQL code box, add the 3 DB screenshots. 15 min
3. Fill **Setup Guide**: paste, and have a teammate test it once. 15 min
4. Fill **App Walkthrough**: add the screenshots with captions, and the code snippets. 30 min
5. Create `README.txt` (Part D) and commit it. 5 min
6. Save the `.ctb`, then commit and push on the docs branch, and open a PR.
7. Final check: the file opens, there are exactly 4 top-level nodes, and no real password appears anywhere.

**Commit one part at a time** (for example `docs: add database info`, `docs: add setup guide`, `docs: add README.txt`). Real small commits count toward the minimum.