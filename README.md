# 🎟️ Event Booking System - Backend

This is the backend API for the Event Booking System project, built using ASP.NET Core. It handles user authentication, event management, and event booking logic.  
This project was developed as part of the Event Booking System Challenge — Areeb Technology.

---

## 📌 Features

- ✅ User & Admin Authentication (JWT)
- ✅ Role-based Access Control (Admin, User)
- ✅ CRUD APIs for Events (Admin only)
- ✅ Book events (User only)
- ✅ Prevent duplicate bookings
- ✅ FluentValidation for inputs
- ✅ API Versioning
- ✅ Swagger UI with version support
- ✅ Clean Architecture + Unit of Work + Repository Pattern
- ✅ SQL Server + EF Core Migrations

---

## 🏗️ Tech Stack

| Layer        | Tech Used                     |
|--------------|-------------------------------|
| Language     | C# .NET 8                     |
| Framework    | ASP.NET Core Web API          |
| Database     | SQL Server                    |
| ORM          | Entity Framework Core         |
| Auth         | JWT Bearer Tokens             |
| Docs         | Swagger (Swashbuckle)         |
| Validation   | FluentValidation              |
| Patterns     | Clean Architecture, CQRS      |

---

## 📄 API Endpoints

### 🔐 Account APIs

| Method | Endpoint                            | Access   | Description                  |
|--------|-------------------------------------|----------|------------------------------|
| POST   | /api/v1/Account/Register            | Public   | Register new user/admin      |
| POST   | /api/v1/Account/Login               | Public   | Login and get JWT            |
| POST   | /api/v1/Account/RefreshToken        | Public   | Refresh JWT token            |
| POST   | /api/v1/Account/ResetPassword       | Public   | Send password reset email    |
| GET    | /api/v1/Account/ConfirmEmail        | Public   | Confirm user email           |

### 📅 Event APIs

| Method | Endpoint                            | Access        | Description             |
|--------|-------------------------------------|----------------|-------------------------|
| POST   | /api/v1/Event/CreateEvent           | Admin         | Create new event        |
| PUT    | /api/v1/Event/UpdateEvent           | Admin         | Update event            |
| DELETE | /api/v1/Event/DeleteEvent?id=       | Admin         | Delete event by ID      |
| GET    | /api/v1/Event/GetAllEvents          | Admin, User   | Get all events          |
| GET    | /api/v1/Event/GetPaginatedEvents    | Admin, User   | Get paginated events    |
| GET    | /api/v1/Event/GetEventById?id=      | Admin, User   | Get event by ID         |

### 📥 Booking APIs

| Method | Endpoint                                  | Access       | Description                    |
|--------|-------------------------------------------|--------------|--------------------------------|
| POST   | /api/v1/Booking/CreateBooking             | User, Admin  | Book a new event               |
| PUT    | /api/v1/Booking/UpdateBooking             | Admin        | Update a booking               |
| DELETE | /api/v1/Booking/DeleteBooking?id=         | Admin        | Delete a booking by ID         |
| GET    | /api/v1/Booking/GetAllBookings            | Admin        | Get all bookings               |
| GET    | /api/v1/Booking/GetPaginatedBookings      | Admin        | Get paginated bookings         |
| GET    | /api/v1/Booking/GetBookingById?id=        | Admin        | Get booking by ID              |
| GET    | /api/v1/Booking/GetAllBookingByUserId     | User         | Get all bookings by user ID    |

### 📧 Email API

| Method | Endpoint                        | Access   | Description         |
|--------|----------------------------------|----------|---------------------|
| POST   | /api/v1/Email/SendEmail         | Public   | Send custom email   |

---

## 🧪 How to Run Locally

### 1️⃣ Clone the Repository

```bash
git clone https://github.com/Adel-Mohamed25/ATC_01143254939.git


## 🧩 Project Structure

```bash
EventBooking
│
├── API               # Entry point
├── Application       # DTOs, Validators, CQRS Handlers
├── Domain            # Entities and Interfaces
├── Infrastructure    # JWT Auth, Services
└── Persistence       # DbContext, EF Repositories

## ✍️ Author

- 👨‍💻 Developed by: **Adel Mohamed**
- 📧 Email: adelmohammedfayed@gmail.com



