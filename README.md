#  E-Commerce Platform (ASP.NET Core)

<p align="center">
  <img src="https://img.shields.io/badge/.NET-Backend-512BD4?style=for-the-badge&logo=dotnet"/>
  <img src="https://img.shields.io/badge/Architecture-Onion-000000?style=for-the-badge"/>
  <img src="https://img.shields.io/badge/Cache-Redis-DC382D?style=for-the-badge&logo=redis&logoColor=white"/>
  <img src="https://img.shields.io/badge/Payments-Stripe-635BFF?style=for-the-badge&logo=stripe&logoColor=white"/>
</p>

---

##  Overview

A **production-ready E-Commerce backend system** built using **ASP.NET Core** following **Onion Architecture** principles.

Designed to handle real-world business scenarios including:
- Product management  
- Shopping basket with caching  
- Secure authentication & authorization  
- Order processing & payment integration  

---

##  Architecture

This project follows **Onion Architecture** to ensure:

- 🔹 Separation of concerns  
- 🔹 High maintainability  
- 🔹 Testability  
- 🔹 Scalability  

### Layers:
- **Domain Layer** → Core business entities & rules  
- **Application Layer** → Business logic & use cases  
- **Infrastructure Layer** → External services (Redis, Stripe, DB)  
- **Presentation Layer** → API Controllers  

---

## ⚙️ Key Features

### 🛍 Product Management
- Full CRUD operations  
- Filtering, sorting & pagination  
- Optimized queries  

### 🧺 Basket System (Redis)
- High-performance basket storage using **Redis**  
- Persistent cart experience  
- Fast read/write operations  

### 💳 Payment Integration (Stripe)
- Secure payment processing using **Stripe API**  
- Payment intent handling  
- Order confirmation after successful payment  

### 📦 Order Management
- Create and track orders  
- Store order details & delivery information  
- Payment status handling  

### 🔐 Authentication & Authorization
- Implemented using **ASP.NET Identity**  
- JWT-based authentication  
- Role-based authorization  

---

## 🚀 Tech Stack

### Backend
- ASP.NET Core Web API  
- Entity Framework Core  
- SQL Server  

### Architecture & Patterns
- Onion Architecture  
- Repository Pattern  
- Dependency Injection  

### Integrations
- Redis (Caching & Basket)  
- Stripe (Payments)  

### Security
- ASP.NET Identity  
- JWT Authentication  

---

## ⚡ Performance Highlights

- 🚀 Redis caching for fast basket operations  
- ⚡ Optimized database queries  
- 🔄 Asynchronous programming  
- 📉 Reduced response time  

---

## 🧪 API Documentation

- Swagger UI available at:
