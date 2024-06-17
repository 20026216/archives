
CREATE DATABASE JCM_ictprg431;
USE JCM_ictprg431;
CREATE USER JCM_ictprg431_user1@localhost IDENTIFIED BY '123qweasdzxc';
-- DROP USER JCM_ictprg431_user1@localhost;
GRANT SELECT, UPDATE, DELETE, CREATE, DROP, ALTER, INSERT, REFERENCES, 
EXECUTE ON JCM_ictprg431.* TO JCM_ictprg431_user1@localhost;
-- SHOW GRANTS FOR JCM_ictprg431_user1@localhost;
-- --------------------------------------------------------------------
-- Q02 EMPLOYEES TABLE
-- --------------------------------------------------------------------
CREATE TABLE employees (
 employee_id BIGINT UNSIGNED Not NULL AUTO_INCREMENT PRIMARY KEY,
 given_name VARCHAR(64),
 family_name VARCHAR(64) NOT NULL,
 date_of_birth date,
 gender_identity char(1),
 gross_salary int DEFAULT '0',
 supervisor_id bigint DEFAULT '0',
 branch_id bigint DEFAULT '0', 
 created_at timestamp NOT NULL DEFAULT NOW(), -- For some reason you can only have one thing on current timestamp???
 updated_at timestamp NULL ON UPDATE CURRENT_TIMESTAMP
);
INSERT INTO `employees` 
 (`date_of_birth`, `employee_id`, `family_name`, `branch_id`, `supervisor_id`, `given_name`, `gross_salary`, `gender_identity`) 
VALUES
 ('1967-11-17', 100, 'Wallace', 1, NULL, 'David', 25000, 'M'),
 ('1967-05-11', 101, 'Levinson', 1, 100, 'Jan', 110000, 'F' ),
 ('1964-03-15', 102, 'Scott', 2, 100, 'Michael', 75000, 'O'),
 ('1971-06-25', 103, 'Martin', 2, 102, 'Angela', 63000, 'F'),
 ('1980-02-05', 104, 'Kapoor', 2, 102, 'Kelly', 55000, 'O'),
 ('1958-02-19', 105, 'Hudson', 2, 102, 'Stanley', 69000, 'M'),
 ('1969-09-05', 106, 'Porter', 3, 100, 'Josh', 78000, 'M'),
 ('1973-07-22', 107, 'Bernard', 3, 106, 'Andy', 65000, 'M'),
 ('1978-10-01', 108, 'Halpert', 3, 106, 'Jen', 71000, 'F');
-- DROP TABLE employees;
-- --------------------------------------------------------------------
-- Q03 BRANCHES TABLE
-- --------------------------------------------------------------------
CREATE TABLE `branches` (
 `branch_id` BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
 `branch_name` VARCHAR(64) NOT NULL DEFAULT 'ERROR',
 `manager_id` BIGINT DEFAULT '0',
 `manager_started_at` DATE DEFAULT '1970-01-01',
 `created_at` TIMESTAMP NOT NULL DEFAULT NOW(),
 `updated_at` TIMESTAMP NULL ON UPDATE CURRENT_TIMESTAMP
);
INSERT INTO branches(branch_id,branch_name,manager_id,manager_started_at)
VALUES
 (1, 'Corporate', 100, "2006-02-09"),
 (2, 'Scranton', 102, "1992-04-06"),
 (3, 'Stamford', 106, "1998-02-13");
-- DROP TABLE branches;
-- --------------------------------------------------------------------
-- Q04 CLIENTS TABLE
-- --------------------------------------------------------------------
CREATE TABLE clients (
 client_id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
 client_name VARCHAR(64),
 branch_id INT,
 created_at TIMESTAMP NOT NULL DEFAULT NOW(),
 updated_at TIMESTAMP NULL ON UPDATE CURRENT_TIMESTAMP
);
INSERT INTO clients (client_id, client_name, branch_id)
VALUES 
(400, 'Dunmore Hoghschool', 2),
(401, 'Lackawana Country', 2),
(402, 'FedEx', 3),
(403, 'John Daly', 3),
(404, 'Scranton Whitepages', 2),
(405, 'Times Newspaper', 3),
(406, 'FedEx', 2);
-- DROP TABLE clients;
-- --------------------------------------------------------------------
-- Q05 WORKING WITH TABLE
-- --------------------------------------------------------------------
CREATE TABLE working_with (
 employee_id BIGINT UNSIGNED Not NULL,
 client_id BIGINT UNSIGNED NOT NULL,
 total_sales BIGINT DEFAULT '0',
 created_at TIMESTAMP NOT NULL DEFAULT NOW(),
 updated_at TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
 CONSTRAINT fk_work_employ FOREIGN KEY (employee_id) REFERENCES employees (employee_id),
 CONSTRAINT fk_work_client FOREIGN KEY (client_id) REFERENCES clients (client_id)
);
INSERT INTO working_with(employee_id,client_id,total_sales)
VALUES
 (105, 400, 55000),
 (102, 401, 267000),
 (108, 402, 22500),
 (107, 403, 5000),
 (108, 403, 12000),
 (105, 404, 33000),
 (107, 405, 26000),
 (102, 406, 15000),
 (105, 406, 130000);
-- DROP TABLE working_with;
-- --------------------------------------------------------------------
-- Q06 BRANCH SUPPLIER TABLE 
-- --------------------------------------------------------------------
CREATE TABLE branch_supplier (
branch_id BIGINT UNSIGNED NOT NULL,
supplier_name VARCHAR (64),
product_supplied VARCHAR (64),
created_at TIMESTAMP NOT NULL DEFAULT NOW(),
updated_at TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
CONSTRAINT fk_bs_branch FOREIGN KEY (branch_id) REFERENCES branches (branch_id)
);
INSERT INTO branch_supplier (branch_id, supplier_name, product_supplied)
VALUES
(2,'Hammer Mill', 'Paper'),
(2, 'Uni-Ball', 'Writing Instruments'),
(3, 'Patriot Paper', 'Paper'),
(2, 'J.T. Forms & Labels', 'Custom Forms'),
(3, 'Uni-Ball', 'Writing Instruments'),
(3, 'Hammer Mill', 'Paper'),
(3, 'Stamford Labels', 'Custom Forms');
-- --------------------------------------------------------------------
-- Q07 DUMMY TABLE
-- --------------------------------------------------------------------
CREATE TABLE dummy (
employee_id INT,
employee_name VARCHAR(20)
);
-- --------------------------------------------------------------------
-- Q08 ALTERING WORKS WITH TABLE STRUCTURE
-- --------------------------------------------------------------------
ALTER TABLE working_with ADD COLUMN profit INT;
-- --------------------------------------------------------------------
-- Q09 RENAME THE DUMMY TABLE
-- --------------------------------------------------------------------
RENAME TABLE dummy TO almost_dummy;
-- --------------------------------------------------------------------
-- Q10 REMOVING A FIELD FROM A TABLE
-- --------------------------------------------------------------------
ALTER TABLE working_with DROP COLUMN profit;

-- Updated Table from other portfolios

UPDATE clients SET client_name = 'John Daly Law, LLC' WHERE client_id = 403;
ALTER TABLE employees MODIFY COLUMN branch_id BIGINT UNSIGNED NOT NULL;
ALTER TABLE employees ADD CONSTRAINT fk_employee_branch FOREIGN KEY (branch_id) REFERENCES branches(branch_id);
ALTER TABLE clients MODIFY COLUMN branch_id BIGINT UNSIGNED NOT NULL;
ALTER TABLE clients ADD CONSTRAINT fk_client_branch FOREIGN KEY (branch_id) REFERENCES branches(branch_id);
ALTER TABLE branches MODIFY COLUMN manager_id BIGINT UNSIGNED NOT NULL;
ALTER TABLE branches ADD CONSTRAINT fk_manager_employee FOREIGN KEY (manager_id) REFERENCES employees(employee_id);

