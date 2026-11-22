USE student_db;

CREATE USER IF NOT EXISTS 'capstone'@'localhost' IDENTIFIED BY 'capstone_pass';
CREATE USER IF NOT EXISTS 'capstone'@'%' IDENTIFIED BY 'capstone_pass';

GRANT SELECT, INSERT, UPDATE, DELETE ON student_db.* TO 'capstone'@'localhost';
GRANT SELECT, INSERT, UPDATE, DELETE ON student_db.* TO 'capstone'@'%';

FLUSH PRIVILEGES;
