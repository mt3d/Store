This is a full-stack e-commerce application built with ASP.NET Core MVC, Razor Pages, and Blazor. It provides a  user interface for browsing products, managing carts, and placing orders.

# Features
- Product Catalog: Add, edit, and manage products with categories and details.
- Shopping Cart & Checkout: Fully functional cart with order placement.
- Storefront UI: User-friendly interface built with MVC + Razor + Blazor.
- Data Persistence: Powered by Entity Framework Core with SQL Server.

# Planned Features
- User Accounts: Register, log in, and manage profiles.
- Authentication & Authorization: Secured endpoints and role-based access.
- REST API: Access store functionality programmatically.
- Docker Deployment

# Tech Stack
- Backend: ASP.NET Core 9 (MVC, Razor Pages, Blazor)
- Database: Microsoft SQL Server with Entity Framework Core
- Authentication: ASP.NET Core Identity
- Frontend: Blazor components + Razor views for hybrid UI

# Local Build
- Clone the repository:

`git clone https://github.com/mt3d/Store.git
cd Store/OnlineStore
`

- Update connection string in appsettings.json to point to your SQL Server.

- Apply EF Core migrations:

`dotnet ef database update`

- Run:

`dotnet run`

- Navigate to:
  - Storefront UI: `http://localhost:5000`
  - Adminstration UI: `http://localhost:5000/admin`