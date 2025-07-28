# Bulky - ASP.NET Core MVC E-commerce Project

## 📖 Giới thiệu

Dự án **Bulky** là một ứng dụng web e-commerce (cửa hàng sách trực tuyến) được xây dựng bằng **ASP.NET Core MVC** (.NET 8) với mục đích học tập từ khóa học .NET Core MVC trên Udemy.

## 🏗️ Kiến trúc dự án

Dự án được tổ chức theo **Clean Architecture** với 4 layer chính:

```
Bulky/
├── BulkyWeb/              # Web Layer (Controllers, Views, wwwroot)
├── Bulky.Models/          # Domain Models (Entities)
├── Bulky.DataAccess/      # Data Access Layer (DbContext, Repositories)
└── Bulky.Utility/         # Utilities và Constants
```

## 🎯 Chức năng chính

### 📚 **Quản lý danh mục sách (Category Management)**

- CRUD operations cho danh mục sách
- Validation và error handling
- UI responsive với Bootstrap

### 📖 **Quản lý sản phẩm (Product Management)**

- Quản lý thông tin sách: Title, Author, Description, ISBN
- Hệ thống giá bậc thang:
  - List Price (giá niêm yết)
  - Price (1-50 cuốn)
  - Price50 (50-100 cuốn)
  - Price100 (>100 cuốn)

## 🛠️ Technology Stack

- **Framework**: ASP.NET Core MVC 8.0
- **Database**: MySQL với Entity Framework Core
- **UI**: Bootstrap 5, Razor Views
- **Architecture Patterns**: Repository Pattern, Unit of Work
- **Development**: VS Code với custom tasks

## 🚀 Cách chạy dự án

### Yêu cầu hệ thống:

- .NET 8 SDK
- MySQL Server
- VS Code (khuyến nghị)

### 1. Clone repository

```bash
git clone <repository-url>
cd Bulky
```

### 2. Cấu hình connection string

Cập nhật `appsettings.json` trong `BulkyWeb`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;database=BulkyDb;user=root;password=yourpassword;"
  }
}
```

### 3. Chạy migrations (trong VS Code)

```bash
# Cách 1: Sử dụng VS Code Task
Ctrl+Shift+P → "Tasks: Run Task" → "ef-update-database"

# Cách 2: Command line
dotnet ef database update --project Bulky.DataAccess --startup-project BulkyWeb
```

### 4. Chạy ứng dụng

```bash
# VS Code Task (khuyến nghị - có hot reload)
Ctrl+Shift+P → "Tasks: Run Task" → "watch"

# Hoặc command line
dotnet run --project BulkyWeb
```

### 5. Truy cập ứng dụng

- **URL**: `https://localhost:7001` hoặc `http://localhost:5000`
- **Admin Area**: `/Admin/Category` hoặc `/Admin/Product`

## 📋 VS Code Tasks có sẵn

### 🏗️ **Build & Run Tasks**

- `build` - Build dự án chính
- `run` - Chạy ứng dụng
- `watch` - Chạy với hot reload (khuyến nghị)
- `clean` - Clean build artifacts
- `restore` - Restore NuGet packages

### 🗄️ **Entity Framework Tasks**

- `ef-add-migration` - Tạo migration mới
- `ef-update-database` - Apply migrations
- `ef-drop-database` - Xóa database
- `ef-remove-migration` - Xóa migration cuối
- `ef-list-migrations` - Liệt kê migrations
- `migrate-and-run` - Update DB + Run app

> Chi tiết xem file [EF_TASKS_GUIDE.md](./EF_TASKS_GUIDE.md)

## 🎨 Features đã implement

- ✅ Category CRUD với validation
- ✅ Product CRUD với complex form
- ✅ Repository Pattern + Unit of Work
- ✅ Areas (Admin/Customer separation)
- ✅ Bootstrap responsive UI
- ✅ Entity Framework Code First
- ✅ Data seeding
- ✅ Error handling và notifications

## 📁 Cấu trúc Database

### Categories Table

- `Id` (Primary Key)
- `Name` (Required, MaxLength: 30)
- `DisplayOrder` (Range: 1-100)

### Products Table

- `Id` (Primary Key)
- `Title` (Required)
- `Description`
- `ISBN`
- `Author`
- `ListPrice`, `Price`, `Price50`, `Price100` (Pricing tiers)

## 🔧 Development Workflow

### 1. Thêm feature mới:

1. Tạo/cập nhật Model trong `Bulky.Models`
2. Cập nhật DbContext trong `Bulky.DataAccess`
3. Tạo migration: Task `ef-add-migration`
4. Apply migration: Task `ef-update-database`
5. Implement Repository nếu cần
6. Tạo Controller và Views trong `BulkyWeb`

### 2. Development daily:

```bash
# Quick start
Ctrl+Shift+P → "Tasks: Run Task" → "migrate-and-run"
```

## 📚 Kiến thức học được

- ASP.NET Core MVC pattern
- Entity Framework Core
- Repository & Unit of Work patterns
- Clean Architecture principles
- Bootstrap responsive design
- VS Code development workflow
- MySQL database design

## 🤝 Contributing

Dự án này phục vụ mục đích học tập. Mọi đóng góp và cải tiến đều được hoan nghênh!

## 📄 License

Dự án học tập - không có license thương mại.
