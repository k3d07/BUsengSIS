CREATE DATABASE IF NOT EXISTS student_db
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_unicode_ci;

USE student_db;

-- Only students may be dropped on re-run
DROP TABLE IF EXISTS students;

CREATE TABLE students (
    student_id VARCHAR(20) PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    gender VARCHAR(10) NOT NULL,
    year_level VARCHAR(20) NOT NULL,
    program VARCHAR(100) NOT NULL,
    contact_number VARCHAR(20) NOT NULL,
    email VARCHAR(100) NULL,
    photo_path VARCHAR(255) NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS users (
    user_id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(50) NOT NULL UNIQUE,
    password_hash VARCHAR(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS activity_log (
    log_id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(50) NOT NULL,
    action VARCHAR(20) NOT NULL,
    student_id VARCHAR(20) NULL,
    logged_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

INSERT INTO students
(student_id, name, gender, year_level, program, contact_number, email, photo_path)
VALUES
('2025-01-0001', 'Keith Belmonte', 'Male', '1st Year', 'BS Computer Science', '09171112233', 'belmonte@example.com', NULL),
('2025-01-0002', 'Vina Victoria', 'Female', '2nd Year', 'BS Information Technology', '09172223344', NULL, NULL),
('2025-01-0003', 'Basti Ludovice', 'Male', '3rd Year', 'BS Information Systems', '09173334455', 'ludovice@example.com', NULL),
('2025-01-0004', 'Kim Edrich Custodio', 'Male', '4th Year', 'BS Computer Engineering', '09174445566', NULL, NULL),
('2025-01-0005', 'Julius Carullo', 'Male', '1st Year', 'BS Information Technology', '09175556677', 'carullo@example.com', NULL),
('2025-01-0006', 'Detective Siesta', 'Female', '2nd Year', 'BS Computer Science', '09176667788', NULL, NULL),
('2025-01-0007', 'Rene Baterbonia', 'Male', '3rd Year', 'BS Information Systems', '09177778899', 'mvp@example.com', NULL),
('2025-01-0008', 'Cyril Gian Anonnuevo', 'Other', '4th Year', 'BS Computer Engineering', '09178889900', NULL, NULL),
('2025-01-0009', 'Kirk Gian Madronero', 'Male', '1st Year', 'BS Computer Engineering', '09179990011', 'madronero@example.com', NULL),
('2025-01-0010', 'Arlecchino O''Brien', 'Female', '2nd Year', 'BS Information Systems', '09170001122', NULL, NULL),
('2025-01-0011', 'Charlie Kirk', 'Male', '3rd Year', 'BS Computer Science', '09171112345', 'kirk@example.com', NULL),
('2025-01-0012', 'Ronova', 'Female', '4th Year', 'BS Information Technology', '09172223456', NULL, NULL);