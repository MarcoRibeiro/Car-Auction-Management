# 🚗 Vehicle and Auction Management API

This project is a **RESTful API** built with **ASP.NET Core**, designed to manage vehicles and their associated auctions. It follows **Clean Architecture principles**, with clear separation of concerns and a strong focus on maintainability and testability.

---

## 🧱 Architecture & Technologies

- **ASP.NET Core Web API** with versioned controllers (e.g., `V1`)
- **MediatR** for CQRS-style separation between controllers and business logic
- **FluentValidation** for expressive, rule-based command validation
- **Custom domain exceptions** to clearly represent business rule violations
- **In-memory generic repository** for fast iteration and testability
- **Unit tests** written with:
  - `xUnit`
  - `NSubstitute`
  - `FluentAssertions`
  - `FluentValidation.TestHelper`

---

## 🧪 Testing

The project includes comprehensive unit tests for:

- ✅ Command handlers (`AddVehicle`, `StartAuction`, `PlaceBid`, etc.)
- ✅ Command validators, including conditional logic based on vehicle type
- ✅ `VehiclesController` — tested in isolation using `ISender` mock
- ✅ Generic in-memory repository (`MemoryRepository<T>`) — all operations covered

---

## 🔧 Key Features

- Add vehicles with **conditional validation** depending on type (e.g., `Truck`, `SUV`)
- Search vehicles by model, manufacturer, year, and type
- Start, stop, and place bids on vehicle-related auctions