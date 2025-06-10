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

---

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

