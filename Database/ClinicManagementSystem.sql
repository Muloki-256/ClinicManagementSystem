-- Create Database
CREATE DATABASE IF NOT EXISTS ClinicManagementSystem;
USE ClinicManagementSystem;

-- Drop existing tables if they exist (clean setup)
DROP TABLE IF EXISTS OrderStatusHistory;
DROP TABLE IF EXISTS OrderItems;
DROP TABLE IF EXISTS Orders;
DROP TABLE IF EXISTS Payments;
DROP TABLE IF EXISTS Bills;
DROP TABLE IF EXISTS Prescriptions;
DROP TABLE IF EXISTS MedicalRecords;
DROP TABLE IF EXISTS Appointments;
DROP TABLE IF EXISTS DoctorSchedules;
DROP TABLE IF EXISTS Inventory;
DROP TABLE IF EXISTS LoginAttempts;
DROP TABLE IF EXISTS Users;
DROP TABLE IF EXISTS Doctors;
DROP TABLE IF EXISTS Patients;
DROP TABLE IF EXISTS Tablets;
DROP TABLE IF EXISTS Persons;

-- Persons Table
CREATE TABLE Persons (
    PersonId INT PRIMARY KEY AUTO_INCREMENT,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    DateOfBirth DATE NOT NULL,
    Gender ENUM('Male', 'Female', 'Other') NOT NULL,
    Phone VARCHAR(20),
    Email VARCHAR(100),
    Address TEXT,
    EmergencyContact VARCHAR(20),
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    INDEX idx_person_name (FirstName, LastName),
    INDEX idx_person_email (Email)
);

-- Patients Table
CREATE TABLE Patients (
    PatientId INT PRIMARY KEY AUTO_INCREMENT,
    PersonId INT NOT NULL,
    BloodType ENUM('A+', 'A-', 'B+', 'B-', 'AB+', 'AB-', 'O+', 'O-'),
    Allergies TEXT,
    MedicalHistory TEXT,
    InsuranceProvider VARCHAR(100),
    InsurancePolicyNumber VARCHAR(50),
    IsActive BOOLEAN DEFAULT TRUE,
    RegistrationDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (PersonId) REFERENCES Persons(PersonId) ON DELETE CASCADE,
    INDEX idx_patient_active (IsActive)
);

-- Doctors Table
CREATE TABLE Doctors (
    DoctorId INT PRIMARY KEY AUTO_INCREMENT,
    PersonId INT NOT NULL,
    Specialization VARCHAR(100) NOT NULL,
    LicenseNumber VARCHAR(50) NOT NULL UNIQUE,
    Qualifications TEXT,
    Department VARCHAR(100),
    IsActive BOOLEAN DEFAULT TRUE,
    ConsultationFee DECIMAL(10,2) DEFAULT 0,
    ExperienceYears INT DEFAULT 0,
    FOREIGN KEY (PersonId) REFERENCES Persons(PersonId) ON DELETE CASCADE,
    INDEX idx_doctor_specialization (Specialization),
    INDEX idx_doctor_department (Department),
    INDEX idx_doctor_active (IsActive)
);

-- Users Table
CREATE TABLE Users (
    UserId INT PRIMARY KEY AUTO_INCREMENT,
    Username VARCHAR(50) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL,
    Role ENUM('Admin', 'Doctor', 'Receptionist', 'Nurse') NOT NULL,
    PersonId INT NOT NULL,
    IsActive BOOLEAN DEFAULT TRUE,
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    LastLogin DATETIME,
    FOREIGN KEY (PersonId) REFERENCES Persons(PersonId) ON DELETE CASCADE,
    INDEX idx_user_role (Role),
    INDEX idx_user_active (IsActive)
);

-- Appointments Table
CREATE TABLE Appointments (
    AppointmentId INT PRIMARY KEY AUTO_INCREMENT,
    PatientId INT NOT NULL,
    DoctorId INT NOT NULL,
    AppointmentDate DATETIME NOT NULL,
    Status ENUM('Scheduled', 'Completed', 'Cancelled', 'NoShow') DEFAULT 'Scheduled',
    Reason TEXT,
    Notes TEXT,
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedDate DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (PatientId) REFERENCES Patients(PatientId) ON DELETE CASCADE,
    FOREIGN KEY (DoctorId) REFERENCES Doctors(DoctorId) ON DELETE CASCADE,
    INDEX idx_appointment_date (AppointmentDate),
    INDEX idx_appointment_status (Status),
    INDEX idx_appointment_doctor (DoctorId, AppointmentDate)
);

-- MedicalRecords Table
CREATE TABLE MedicalRecords (
    RecordId INT PRIMARY KEY AUTO_INCREMENT,
    PatientId INT NOT NULL,
    DoctorId INT NOT NULL,
    AppointmentId INT,
    Diagnosis TEXT,
    Symptoms TEXT,
    TreatmentNotes TEXT,
    TestsPerformed TEXT,
    TestResults TEXT,
    RecordDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    FollowUpDate DATETIME,
    IsCritical BOOLEAN DEFAULT FALSE,
    FOREIGN KEY (PatientId) REFERENCES Patients(PatientId) ON DELETE CASCADE,
    FOREIGN KEY (DoctorId) REFERENCES Doctors(DoctorId) ON DELETE CASCADE,
    FOREIGN KEY (AppointmentId) REFERENCES Appointments(AppointmentId) ON DELETE SET NULL,
    INDEX idx_record_date (RecordDate),
    INDEX idx_record_patient (PatientId),
    INDEX idx_record_critical (IsCritical)
);

-- Tablets Table
CREATE TABLE Tablets (
    TabletId INT PRIMARY KEY AUTO_INCREMENT,
    TabletName VARCHAR(100) NOT NULL,
    Description TEXT,
    Manufacturer VARCHAR(100),
    CostPerUnit DECIMAL(10,2) NOT NULL,
    StockQuantity INT DEFAULT 0,
    MinimumStockLevel INT DEFAULT 10,
    IsActive BOOLEAN DEFAULT TRUE,
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    INDEX idx_tablet_name (TabletName),
    INDEX idx_tablet_stock (StockQuantity),
    INDEX idx_tablet_active (IsActive)
);

-- Prescriptions Table
CREATE TABLE Prescriptions (
    PrescriptionId INT PRIMARY KEY AUTO_INCREMENT,
    RecordId INT NOT NULL,
    TabletId INT NOT NULL,
    Dosage VARCHAR(50) NOT NULL,
    Duration VARCHAR(50) NOT NULL,
    Quantity INT NOT NULL,
    Instructions TEXT,
    PrescribedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (RecordId) REFERENCES MedicalRecords(RecordId) ON DELETE CASCADE,
    FOREIGN KEY (TabletId) REFERENCES Tablets(TabletId) ON DELETE CASCADE,
    INDEX idx_prescription_date (PrescribedDate)
);

-- Bills Table
CREATE TABLE Bills (
    BillId INT PRIMARY KEY AUTO_INCREMENT,
    PatientId INT NOT NULL,
    AppointmentId INT,
    BillType ENUM('Consultation', 'Medicine', 'Test', 'Procedure', 'Hospitalization') NOT NULL,
    Description TEXT,
    TotalAmount DECIMAL(10,2) NOT NULL,
    PaidAmount DECIMAL(10,2) DEFAULT 0,
    BillDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    PaymentDate DATETIME,
    PaymentMethod ENUM('Cash', 'Card', 'Insurance', 'Online'),
    Status ENUM('Pending', 'Paid', 'Partial') DEFAULT 'Pending',
    DueDate DATE,
    LastPaymentDate DATETIME,
    FOREIGN KEY (PatientId) REFERENCES Patients(PatientId) ON DELETE CASCADE,
    FOREIGN KEY (AppointmentId) REFERENCES Appointments(AppointmentId) ON DELETE SET NULL,
    INDEX idx_bill_date (BillDate),
    INDEX idx_bill_status (Status),
    INDEX idx_bill_patient (PatientId)
);

-- Payments Table (Enhanced for verification)
CREATE TABLE Payments (
    PaymentId INT PRIMARY KEY AUTO_INCREMENT,
    BillId INT NOT NULL,
    Amount DECIMAL(10,2) NOT NULL,
    PaymentMethod ENUM('Cash', 'Card', 'Insurance', 'Online') NOT NULL,
    TransactionId VARCHAR(100),
    Status ENUM('Pending', 'Verified', 'Completed', 'Failed', 'Refunded') DEFAULT 'Pending',
    PaymentDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    Notes TEXT,
    ReceiptNumber VARCHAR(50),
    VerifiedDate DATETIME,
    VerifiedBy INT,
    FOREIGN KEY (BillId) REFERENCES Bills(BillId) ON DELETE CASCADE,
    INDEX idx_payment_date (PaymentDate),
    INDEX idx_payment_status (Status),
    INDEX idx_payment_receipt (ReceiptNumber)
);

-- LoginAttempts Table (for security)
CREATE TABLE LoginAttempts (
    AttemptId INT PRIMARY KEY AUTO_INCREMENT,
    Username VARCHAR(50) NOT NULL,
    Success BOOLEAN NOT NULL,
    AttemptDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    IPAddress VARCHAR(45),
    UserAgent TEXT,
    INDEX idx_login_date (AttemptDate),
    INDEX idx_login_success (Success)
);

-- Inventory Table (for medicine stock management)
CREATE TABLE Inventory (
    InventoryId INT PRIMARY KEY AUTO_INCREMENT,
    TabletId INT NOT NULL,
    QuantityChange INT NOT NULL,
    ChangeType ENUM('Purchase', 'Sale', 'Adjustment', 'Expired') NOT NULL,
    Reason TEXT,
    ChangedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    ChangedBy INT,
    FOREIGN KEY (TabletId) REFERENCES Tablets(TabletId) ON DELETE CASCADE,
    INDEX idx_inventory_date (ChangedDate),
    INDEX idx_inventory_type (ChangeType)
);

-- DoctorSchedules Table
CREATE TABLE DoctorSchedules (
    ScheduleId INT PRIMARY KEY AUTO_INCREMENT,
    DoctorId INT NOT NULL,
    DayOfWeek ENUM('Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday') NOT NULL,
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL,
    IsActive BOOLEAN DEFAULT TRUE,
    FOREIGN KEY (DoctorId) REFERENCES Doctors(DoctorId) ON DELETE CASCADE,
    INDEX idx_schedule_doctor (DoctorId),
    INDEX idx_schedule_day (DayOfWeek)
);

-- Orders Table (Allows NULL for PatientId for guest orders)
CREATE TABLE Orders (
    OrderId INT PRIMARY KEY AUTO_INCREMENT,
    PatientId INT NULL,
    OrderDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    Status ENUM('Pending', 'Confirmed', 'Processing', 'Ready', 'Completed', 'Cancelled') DEFAULT 'Pending',
    TotalAmount DECIMAL(10,2) DEFAULT 0,
    Notes TEXT,
    CreatedBy INT,
    IsGuestOrder BOOLEAN DEFAULT FALSE,
    GuestInfo TEXT,
    FOREIGN KEY (PatientId) REFERENCES Patients(PatientId) ON DELETE SET NULL,
    INDEX idx_order_date (OrderDate),
    INDEX idx_order_status (Status),
    INDEX idx_order_patient (PatientId),
    INDEX idx_order_guest (IsGuestOrder)
);

-- OrderItems Table
CREATE TABLE OrderItems (
    OrderItemId INT PRIMARY KEY AUTO_INCREMENT,
    OrderId INT NOT NULL,
    TabletId INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(10,2) NOT NULL,
    TotalPrice DECIMAL(10,2) AS (Quantity * UnitPrice) STORED,
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId) ON DELETE CASCADE,
    FOREIGN KEY (TabletId) REFERENCES Tablets(TabletId) ON DELETE CASCADE,
    INDEX idx_orderitem_order (OrderId)
);

-- OrderStatusHistory Table (for tracking order status changes)
CREATE TABLE OrderStatusHistory (
    HistoryId INT PRIMARY KEY AUTO_INCREMENT,
    OrderId INT NOT NULL,
    Status ENUM('Pending', 'Confirmed', 'Processing', 'Ready', 'Completed', 'Cancelled') NOT NULL,
    ChangedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    ChangedBy INT,
    Notes TEXT,
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId) ON DELETE CASCADE,
    INDEX idx_status_history (OrderId, ChangedDate)
);

-- Insert Sample Persons (FIXED DATE FORMATS)
INSERT INTO Persons (FirstName, LastName, DateOfBirth, Gender, Phone, Email, Address, EmergencyContact) VALUES
('Muloki', 'Derrick', '2002-12-24', 'Male', '0758626307', 'mkderick@gmail.com', 'Luwumu Street, City', '0779979370'),
('Mukiibi', 'Deogracius', '2001-08-22', 'Male', '0703059640', 'mudoe@gmail.com', 'William Street, Town', '0778899405'),
('Lunkuse', 'Hasifah', '2000-12-10', 'Female', '0753945521', 'hasi@gmail.com', 'Ben Kiwanuka, Village', '0702474201'),
('Namayanja', 'Susan', '1998-03-30', 'Female', '0767011814', 'susie@gmail.com', 'Masaka, City', '0752029957'),
('Sserwanja', 'Allan', '1999-07-18', 'Male', '0758939476', 'allan4@gmail.com', 'Entebbe, Town', '0778899405'),
('Namugenyi', 'Josephine', '2004-09-14', 'Female', '0778899405', 'jossie@gmail.com', 'Kampala, City', '0758626307'),
('Nakaggwa', 'Esther', '1995-11-03', 'Female', '0753945521', 'nakaggwae@gmail.com', 'Kyotera, Town', '0767011814'); -- Fixed gender to Female

-- Insert Sample Patients
INSERT INTO Patients (PersonId, BloodType, Allergies, MedicalHistory, InsuranceProvider, InsurancePolicyNumber) VALUES
(1, 'A+', 'Penicillin', 'Hypertension, Diabetes', 'HealthCare Inc', 'HCI-001234'),
(2, 'O-', 'Shellfish, Peanuts', 'Asthma', 'MediCover', 'MC-005678'),
(3, 'B+', 'None', 'None', 'SecureHealth', 'SH-009876'),
(4, 'AB-', 'Ibuprofen', 'Migraine', 'HealthCare Inc', 'HCI-003210'),
(5, 'A-', 'Dust', 'Arthritis', 'MediCover', 'MC-007654'),
(6, 'O+', 'Latex', 'High Cholesterol', 'Wellness Plus', 'WP-004321'),
(7, 'B-', 'Sulfa Drugs', 'Thyroid Issues', 'HealthGuard', 'HG-008765');

-- Insert Sample Doctors
INSERT INTO Doctors (PersonId, Specialization, LicenseNumber, Qualifications, Department, ConsultationFee, ExperienceYears) VALUES
(1, 'Cardiology', 'CARD-001', 'MD, Cardiology Specialist', 'Cardiology', 150.00, 15),
(2, 'Pediatrics', 'PED-002', 'MD, Pediatric Specialist', 'Pediatrics', 120.00, 12),
(3, 'Orthopedics', 'ORTH-003', 'MD, Orthopedic Surgeon', 'Orthopedics', 200.00, 18),
(4, 'Dermatology', 'DERM-004', 'MD, Dermatology Specialist', 'Dermatology', 130.00, 10),
(5, 'General Medicine', 'GEN-005', 'MD, General Practitioner', 'General Medicine', 100.00, 8),
(6, 'Neurology', 'NEURO-006', 'MD, Neurologist', 'Neurology', 180.00, 14),
(7, 'Psychiatry', 'PSY-007', 'MD, Psychiatrist', 'Psychiatry', 160.00, 11);

-- Insert Sample Users (Password: password123)
INSERT INTO Users (Username, PasswordHash, Role, PersonId) VALUES
('admin', 'ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f', 'Admin', 1),
('Dr.Mukiibi', 'ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f', 'Doctor', 1),
('Dr.Derrick', 'ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f', 'Doctor', 2),
('Reception', 'ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f', 'Receptionist', 3),
('Nurse.Susan', 'ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f', 'Nurse', 4);

-- Insert Sample Tablets
INSERT INTO Tablets (TabletName, Description, Manufacturer, CostPerUnit, StockQuantity, MinimumStockLevel) VALUES
('Paracetamol', 'Pain reliever and fever reducer', 'PharmaCorp', 5.00, 100, 20),
('Amoxicillin', 'Antibiotic for bacterial infections', 'MediLab', 15.00, 50, 15),
('Metformin', 'Diabetes medication', 'HealthPharm', 12.00, 75, 25),
('Lisinopril', 'Blood pressure medication', 'CardioMed', 18.00, 60, 20),
('Atorvastatin', 'Cholesterol medication', 'CardioMed', 22.00, 45, 15),
('Chloroquine', 'Antimalarial medication', 'TropiPharm', 25.00, 30, 10),
('Ibuprofen', 'Anti-inflammatory pain reliever', 'PharmaCorp', 8.00, 80, 15),
('Omeprazole', 'Acid reflux medication', 'DigestHealth', 22.00, 40, 10),
('Levothyroxine', 'Thyroid medication', 'EndoCare', 28.00, 35, 12),
('Sertraline', 'Antidepressant', 'NeuroPharm', 32.00, 25, 8);

-- Insert Sample Appointments
INSERT INTO Appointments (PatientId, DoctorId, AppointmentDate, Status, Reason) VALUES
(1, 1, '2024-01-15 10:00:00', 'Completed', 'Regular checkup'),
(2, 2, '2024-01-15 11:00:00', 'Completed', 'Child vaccination'),
(3, 3, '2024-01-16 14:00:00', 'Scheduled', 'Knee pain consultation'),
(4, 4, '2024-01-16 15:30:00', 'Scheduled', 'Skin rash examination'),
(5, 5, '2024-01-17 09:00:00', 'Scheduled', 'General health checkup'),
(6, 6, '2024-01-17 11:00:00', 'Scheduled', 'Headache evaluation'),
(7, 7, '2024-01-18 10:30:00', 'Scheduled', 'Mental health consultation');

-- Insert Sample Medical Records
INSERT INTO MedicalRecords (PatientId, DoctorId, AppointmentId, Diagnosis, Symptoms, TreatmentNotes, TestsPerformed, TestResults) VALUES
(1, 1, 1, 'Hypertension', 'High blood pressure, headaches', 'Prescribed medication and lifestyle changes', 'Blood pressure monitoring', 'BP: 150/95'),
(2, 2, 2, 'Routine Vaccination', 'No symptoms - preventive care', 'Administered required vaccines', 'None', 'All vaccines up to date'),
(6, 6, 6, 'Migraine', 'Severe headaches, sensitivity to light', 'Prescribed pain management and lifestyle adjustments', 'Neurological exam', 'Normal neurological function');

-- Insert Sample Prescriptions
INSERT INTO Prescriptions (RecordId, TabletId, Dosage, Duration, Quantity, Instructions) VALUES
(1, 3, '500mg twice daily', '30 days', 60, 'Take with meals'),
(1, 4, '10mg once daily', '30 days', 30, 'Take in the morning'),
(3, 1, '500mg as needed', '15 days', 15, 'Take for headache pain'),
(3, 7, '400mg every 6 hours', '10 days', 40, 'Take with food');

-- Insert Sample Bills
INSERT INTO Bills (PatientId, AppointmentId, BillType, Description, TotalAmount, PaidAmount, PaymentMethod, Status, DueDate) VALUES
(1, 1, 'Consultation', 'Cardiology consultation and tests', 200.00, 200.00, 'Card', 'Paid', '2024-02-15'),
(2, 2, 'Consultation', 'Pediatric vaccination', 150.00, 100.00, 'Cash', 'Partial', '2024-02-20'),
(3, NULL, 'Medicine', 'Pain medication prescription', 85.00, 0.00, NULL, 'Pending', '2024-02-25'),
(6, 6, 'Consultation', 'Neurology consultation', 180.00, 180.00, 'Insurance', 'Paid', '2024-02-18');

-- Insert Sample Payments
INSERT INTO Payments (BillId, Amount, PaymentMethod, TransactionId, Status, PaymentDate, Notes, ReceiptNumber) VALUES
(1, 200.00, 'Card', 'TXN-001', 'Completed', '2024-01-15 10:45:00', 'Full payment received', 'RCP-20240115104500-1234'),
(2, 100.00, 'Cash', NULL, 'Completed', '2024-01-15 11:30:00', 'Partial payment - balance due', 'RCP-20240115113000-5678'),
(4, 180.00, 'Insurance', 'INS-001', 'Completed', '2024-01-17 12:00:00', 'Insurance claim processed', 'RCP-20240117120000-9012');

-- Insert Sample Doctor Schedules
INSERT INTO DoctorSchedules (DoctorId, DayOfWeek, StartTime, EndTime) VALUES
(1, 'Monday', '09:00:00', '17:00:00'),
(1, 'Wednesday', '09:00:00', '17:00:00'),
(1, 'Friday', '09:00:00', '17:00:00'),
(2, 'Tuesday', '08:00:00', '16:00:00'),
(2, 'Thursday', '08:00:00', '16:00:00'),
(2, 'Saturday', '09:00:00', '13:00:00'),
(3, 'Monday', '10:00:00', '18:00:00'),
(3, 'Tuesday', '10:00:00', '18:00:00'),
(3, 'Thursday', '10:00:00', '18:00:00'),
(4, 'Wednesday', '09:00:00', '17:00:00'),
(4, 'Friday', '09:00:00', '17:00:00'),
(5, 'Monday', '08:00:00', '16:00:00'),
(5, 'Wednesday', '08:00:00', '16:00:00'),
(5, 'Friday', '08:00:00', '16:00:00');

-- Insert Sample Orders (including guest orders)
INSERT INTO Orders (PatientId, OrderDate, Status, TotalAmount, Notes, CreatedBy, IsGuestOrder, GuestInfo) VALUES
(1, '2024-01-15 10:30:00', 'Completed', 75.00, 'Regular prescription refill', 1, FALSE, NULL),
(NULL, '2024-01-16 14:20:00', 'Completed', 45.00, 'Walk-in customer', 3, TRUE, 'Mukiibi Deogracius - Phone: 0758-62-6307'),
(3, '2024-01-16 15:00:00', 'Processing', 120.00, 'New prescription', 1, FALSE, NULL),
(NULL, '2024-01-17 09:15:00', 'Pending', 60.00, 'Over-the-counter purchase', 3, TRUE, 'Muloki Derrick - Phone: 0779-979-370');

-- Insert Sample Order Items
INSERT INTO OrderItems (OrderId, TabletId, Quantity, UnitPrice) VALUES
(1, 2, 5, 15.00),  -- Amoxicillin
(2, 1, 5, 5.00),   -- Paracetamol
(2, 7, 2, 8.00),   -- Ibuprofen
(3, 4, 4, 18.00),  -- Lisinopril
(3, 5, 2, 22.00),  -- Atorvastatin
(4, 1, 8, 5.00),   -- Paracetamol
(4, 7, 2, 8.00);   -- Ibuprofen

-- Insert Sample Inventory Records
INSERT INTO Inventory (TabletId, QuantityChange, ChangeType, Reason, ChangedBy) VALUES
(1, 100, 'Purchase', 'Initial stock', 1),
(2, 50, 'Purchase', 'Initial stock', 1),
(1, -5, 'Sale', 'Order #2', 3),
(2, -5, 'Sale', 'Order #1', 1);

-- Drop existing views if they exist
DROP VIEW IF EXISTS PatientSummary;
DROP VIEW IF EXISTS DoctorScheduleSummary;

-- Views
CREATE VIEW PatientSummary AS
SELECT 
    p.PatientId,
    per.FirstName,
    per.LastName,
    per.Gender,
    per.Phone,
    per.Email,
    p.BloodType,
    p.Allergies,
    p.InsuranceProvider,
    COUNT(DISTINCT a.AppointmentId) as TotalAppointments,
    COUNT(DISTINCT mr.RecordId) as TotalRecords
FROM Patients p
JOIN Persons per ON p.PersonId = per.PersonId
LEFT JOIN Appointments a ON p.PatientId = a.PatientId
LEFT JOIN MedicalRecords mr ON p.PatientId = mr.PatientId
WHERE p.IsActive = TRUE
GROUP BY p.PatientId;

CREATE VIEW DoctorScheduleSummary AS
SELECT 
    d.DoctorId,
    per.FirstName,
    per.LastName,
    d.Specialization,
    d.Department,
    GROUP_CONCAT(DISTINCT ds.DayOfWeek ORDER BY ds.DayOfWeek) as WorkingDays,
    COUNT(DISTINCT a.AppointmentId) as TotalAppointments
FROM Doctors d
JOIN Persons per ON d.PersonId = per.PersonId
LEFT JOIN DoctorSchedules ds ON d.DoctorId = ds.DoctorId AND ds.IsActive = TRUE
LEFT JOIN Appointments a ON d.DoctorId = a.DoctorId
WHERE d.IsActive = TRUE
GROUP BY d.DoctorId;

-- Sample data verification query
SELECT 'Database setup completed successfully!' as Status;
SELECT COUNT(*) as TotalPersons FROM Persons;
SELECT COUNT(*) as TotalPatients FROM Patients;
SELECT COUNT(*) as TotalDoctors FROM Doctors;
SELECT COUNT(*) as TotalTablets FROM Tablets;
SELECT COUNT(*) as TotalOrders FROM Orders;
SELECT COUNT(*) as TotalPayments FROM Payments;