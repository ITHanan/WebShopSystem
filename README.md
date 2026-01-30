# 📚 Course Management Platform (.NET & React)

A web-based course management system developed using **ASP.NET (C#)** for the backend and **React** for the frontend. The platform supports user registration, course browsing, enrollment, and comprehensive admin course management.

---

## 📌 Features

### 👤 User Features
- Register, login, and secure authentication (hashed passwords)
- Browse and search available courses
- View course details and descriptions
- Enroll in courses and track enrollment
- Manage personal profile and enrolled courses
- View and download course materials (PDFs or external links)

### 🛠️ Admin Features
- Create, update, and delete courses
- Add course materials (PDFs or links)
- Add and update teacher information
- View enrolled participants per course
- Edit detailed course descriptions

-----

## 🧱 Entities

### 1. **User**
- `UserId`
- `UserName`
- `Password` (Hashed)
- `MobileNumber`
- `List<Course>`

### 2. **Course**
- `CourseId`
- `Title`
- `Description`
- `CourseLevel`
- `Time`
- `Teacher`
- `Category`
- `UserId` (FK to User)

### 3. **Admin**
- `UserName`
- `Password` (Hashed)

---

## ✅ Functional Requirements

### User Authentication & Registration
- User registration with name, email, password
- Secure login/logout functionality
- Passwords are hashed and stored securely

### Course Browsing
- List all available courses
- Filter/search by course level or keyword
- View detailed course descriptions

### Enrollment
- Enroll in available courses
- Receive confirmation upon successful enrollment

### Profile Management
- Update personal information
- View enrolled courses and progress

### Admin Course Management
- Add/edit/delete course details
- Upload PDFs or add external resources
- Add teacher information and detailed course descriptions
- View enrolled users per course

---

## 🔐 Security & Access Control

- Admin-only access for course creation and management
- Only authenticated users can enroll or view course materials
- Unauthorized access is restricted

---

## 🌐 Non-Functional Requirements

- API response time < 500ms
- Support for at least 50 concurrent users
- Responsive and accessible design

---

## 🎯 MoSCoW Prioritization

### Must Have
- Admin CRUD operations on courses
- Course materials management
- Course filtering and description view
- User registration and enrollment

### Should Have
- User download access to materials
- Admin role restrictions enforced
- Fast loading course list and filters

### Could Have
- Multi-format support for uploads (e.g., DOCX, PPT)
- Email notifications/reminders
- Advanced search by teacher or location

### Won't Have (Now)
- Integrated quizzes or assessments
- Chat or forums for users
- Real-time support chat

---

## 👥 User Stories

| Role        | User Story                                                                 |
|-------------|----------------------------------------------------------------------------|
| Admin       | Create and manage course information                                       |
| Admin       | Upload PDFs or add links for materials                                     |
| Admin       | Add teacher bios and contact info                                          |
| Participant | View, filter, and browse courses                                           |
| Participant | Register and receive confirmation                                          |
| Participant | Download/view course materials                                             |
| Participant | Read detailed course descriptions                                          |
| Participant | Manage personal profile and enrolled courses                               |

---

## 🧭 Use Cases

### 1. Create a New Course
**Actor**: Admin  
**Flow**:
- Navigate to course creation page  
- Fill out details (title, level, time, location)  
- Submit to save course  

### 2. Upload Course Materials
**Actor**: Admin  
**Flow**:
- Select a course  
- Upload a PDF or paste a link  
- System associates it with the course  

### 3. Register for Course
**Actor**: User  
**Flow**:
- Login and browse courses  
- Click on a course → Register  
- System confirms registration  

### 4. UC-13: Receive Booking for Assigned Branch (Shop Manager)
**Actor**: Shop Manager  
**Flow**:
- Shop manager logs in with assigned branch  
- Navigate to bookings page  
- View list of bookings filtered to their assigned branch only  
- Bookings sorted by appointment date (ascending)  
- Click on booking to view detailed information  
- System validates branch ownership before displaying details  

**Branch-Scoped Visibility Rules**:
- Shop managers can ONLY see bookings for their assigned branch
- Attempts to access bookings from other branches are blocked
- System logs unauthorized access attempts (via ILogger)
- Empty state displayed when no bookings exist

**Read-Only Nature at Intake Stage**:
- Bookings are read-only at this stage
- No editing allowed until explicit actions (assignment, inspection)
- Booking integrity is protected

---

## 🗄️ Database Schema

### Entities

**Branch**
- BranchId (PK)
- Name
- Address
- PhoneNumber
- CreatedAt

**Booking**
- BookingId (PK)
- VehiclePlateNumber
- ServiceType
- AppointmentDate
- Status (Pending, Assigned, InProgress, Completed, Cancelled)
- CustomerName
- CustomerPhone
- CustomerEmail
- Notes
- BranchId (FK → Branch)
- CreatedAt
- UpdatedAt

**User** (Extended)
- UserID (PK)
- UserName
- Email
- PasswordHash
- Role (Participant, ShopManager, Admin)
- BranchId (FK → Branch, nullable)
- LanguageId (FK)
- CreatedAt

**Indexes**:
- `IX_Booking_BranchId_AppointmentDate` - Optimizes booking retrieval per branch

---

## 🔒 Security & Authorization

### JWT Token Claims
- `NameIdentifier`: User ID
- `Name`: Username
- `Email`: User email
- `Role`: User role (Participant, ShopManager, Admin)
- `BranchId`: Assigned branch ID (for shop managers)

### API Endpoints

**Booking Endpoints** (Requires `ShopManager` role):

```
GET /api/booking
- Returns bookings for the authenticated shop manager's assigned branch
- Sorted by appointment date (ascending)
- Returns 400 if manager has no branch assignment

GET /api/booking/{bookingId}
- Returns detailed booking information
- Validates that booking belongs to manager's assigned branch
- Returns 403 Forbidden if accessing another branch's booking
- Returns 404 if booking not found
```

---

## 🛠️ Tech Stack

- **Backend**: ASP.NET Core (C#)
- **Frontend**: React.js
- **Database**: SQL Server (EF Core)
- **Authentication**: ASP.NET Identity / JWT
- **Hosting**: IIS / Azure (optional)

---

## Contributors: 

- Hanan Ahmed BE 
- Paria Taba FE 
- Betul Demir FE 
- Albin BE 
- Naghamm UX 

