## Bulky eCommerce (ASP.NET Core MVC)

Bulky is a sample eCommerce web app built with ASP.NET Core MVC, Entity Framework Core, Identity, and Stripe checkout. It uses Areas for `Admin`, `Customer`, and `Identity` and follows a clean repository + unit of work pattern.

### What does the store do?
- Customer users can browse products (books), view details, and add items to a shopping cart.
- They can proceed through a checkout flow and pay using Stripe (test mode).
- Admin users can manage the catalog (products, categories, images) and oversee orders from the Admin area.

### Prerequisites
- .NET SDK 8.0 (installed). Verify:
```bash
dotnet --version
```
- SQL Server (Developer/Express) running locally, or update the connection string accordingly.
- Optional: Node not required; static assets are included.

### Solution Structure
```
Bulky_eCommerce/
├─ BulkyWeb/                  # Web UI (MVC + Razor Pages for Identity)
│  ├─ Areas/
│  │  ├─ Admin/
│  │  ├─ Customer/
│  │  └─ Identity/           # Scaffoled Identity UI
│  ├─ Views/Shared            # _Layout, partials, shared views
│  └─ Program.cs              # App startup & middleware
├─ Bulky.DataAccess/          # EF Core DbContext, Repositories, Migrations
├─ Bulky.Models/              # Entity and ViewModel classes
├─ Bulky.Utility/             # Helpers (Email sender, constants, settings)
└─ Bulky.sln
```

### Configuration
1. App settings: `BulkyWeb/appsettings.json`
   - Connection string `DefaultConnection` points to local SQL Server by default.
   - Stripe keys under `Stripe` section. Use your own test keys for local development.
2. Target framework: All projects target `net8.0`.
3. Default routes: Area route is set to make Customer Home the default landing page.

### Database
This project uses Entity Framework Core Code‑First with migrations stored in `Bulky.DataAccess/Migrations`.

- Create/update database (from solution root):
```powershell
dotnet ef database update --project Bulky.DataAccess --startup-project BulkyWeb
```
If EF tools are missing:
```powershell
dotnet tool install --global dotnet-ef
```

- Add a new migration (if you change the model):
```powershell
dotnet ef migrations add <MigrationName> --project Bulky.DataAccess --startup-project BulkyWeb
 dotnet ef database update --project Bulky.DataAccess --startup-project BulkyWeb
```

A database initializer runs at startup to ensure base data and Identity roles exist.

### Build and Run
From the repository root:
```powershell
# Restore & build
dotnet restore
dotnet build

# Run the web app
dotnet run --project BulkyWeb
```
The app starts on URLs similar to:
- https://localhost:7177
- http://localhost:5033

Navigate to the site root (Customer area) or directly to:
- https://localhost:7177/Customer/Home/Index

### Identity & Roles
- ASP.NET Core Identity is configured with cookies and default token providers.
- Roles and basic seeding are performed via `IDbInitializer` at app startup.
- Login/Register is available via the Identity area.

### Features
- Product catalog with images, categories, and pricing tiers
- Shopping cart with session support
- Checkout flow integrated with Stripe (test mode)
- Admin area for managing products, categories, orders
- Repository + Unit of Work pattern

### Stripe
Set your Stripe test keys in `BulkyWeb/appsettings.json`:
```json
"Stripe": {
  "SecretKey": "sk_test_...",
  "PublishableKey": "pk_test_..."
}
```
These are read in `Program.cs` and used for checkout.

### Troubleshooting
- View not found errors
  - Confirm files exist under `Areas/Customer/Views/<Controller>/<Action>.cshtml`.
  - In development, runtime compilation is enabled to ease view changes.
- Database errors
  - Verify SQL Server is running and the `DefaultConnection` is correct.
  - Run `dotnet ef database update` as shown above.
- .NET SDK mismatch
  - This solution targets .NET 8.0. Install .NET 8 SDK if missing.

### Scripts (common)
```powershell
# Clean & rebuild UI project
Remove-Item -Recurse -Force BulkyWeb\bin, BulkyWeb\obj -ErrorAction SilentlyContinue
dotnet clean
dotnet build
```

### License
For educational and demo purposes.
