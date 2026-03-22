🏥 Clinic Management System

A comprehensive Windows Forms application for managing clinic operations, patient records, appointments, billing, and medical history.

📋 Table of Contents
- [Overview](#overview)
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Prerequisites](#prerequisites)
- [Installation Guide](#installation-guide)
  - [Step 1: Install XAMPP](#step-1-install-xampp)
  - [Step 2: Database Setup](#step-2-database-setup)
  - [Step 3: Configure Application](#step-3-configure-application)
  - [Step 4: Build and Run](#step-4-build-and-run)
- [Project Structure](#project-structure)
- [Database Schema](#database-schema)
- [Screenshots](#screenshots)
- [Troubleshooting](#troubleshooting)
- [Contributing](#contributing)
- [License](#license)
- [Contact](#contact)
🎯 Overview

The **Clinic Management System** is a desktop application built with C# Windows Forms that helps clinics and medical practices manage their daily operations efficiently. It provides modules for patient registration, appointment scheduling, medical records management, billing, and reporting.

✨ Features

👥 Patient Management
- Register new patients
- Update patient information
- View patient history
- Search patients by name/ID

📅 Appointment Management
- Schedule appointments
- Cancel/reschedule appointments
- View daily/weekly schedule
- Appointment reminders

👨‍⚕️ Doctor Management
- Manage doctor profiles
- Assign specializations
- Track doctor schedules
- Consultation fee management

💊 Medical Records
- Maintain patient medical history
- Prescription management
- Allergy tracking
- Medical notes and diagnoses

💰 Billing & Payments
- Generate invoices
- Process payments
- Payment verification
- Receipt generation
- Track payment history

📊 Reports
- Patient reports
- Appointment reports
- Financial reports
- Revenue analytics

🔐 Security
- User authentication
- Role-based access control
- Secure password storage

🛠️ Technologies Used

| Technology | Purpose |
|------------|---------|
| **C#** | Programming Language |
| **Windows Forms** | Desktop UI Framework |
| **XAMPP** | Local Development Server |
| **MySQL** | Database Management System |
| **phpMyAdmin** | Database Administration |
| **ADO.NET** | Database Connectivity |
| **Visual Studio 2022** | Integrated Development Environment |

📦 Prerequisites

Before installing the application, ensure you have:

| Software | Version | Purpose |
|----------|---------|---------|
| **XAMPP** | 7.4 or higher | MySQL Server and phpMyAdmin |
| **Visual Studio 2022** | Community/Professional | IDE for C# development |
| **.NET Framework** | 4.7.2 or higher | Runtime environment |
| **Windows** | 10/11 | Operating System |

🚀 Installation Guide

### Step 1: Install XAMPP

1. Download XAMPP from [https://www.apachefriends.org/](https://www.apachefriends.org/)
2. Run the installer and follow the setup wizard
3. Launch **XAMPP Control Panel**
4. Start the following services:
   - ✅ **Apache** (optional, for phpMyAdmin)
   - ✅ **MySQL** (required for database)
5. Verify MySQL is running (port 3306)

Step 2: Database Setup

Method 1: Using phpMyAdmin (Recommended)

1. Open browser and go to: `http://localhost/phpmyadmin`
2. Click **New** to create a database
3. Enter database name: `clinicMmanagementSystem`
4. Choose **utf8_general_ci** collation
5. Click **Create**
6. Click **Import** tab
7. Select the `DatabaseSetup.sql` file
8. Click **Go** to import tables

Method 2: Using MySQL Command Line

1. Open Command Prompt
2. Navigate to MySQL bin folder:
   ```bash
   cd C:\xampp\mysql\bin
