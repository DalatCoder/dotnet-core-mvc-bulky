# Entity Framework Core Tasks Guide

Dự án Bulky đã được cấu hình với các VS Code tasks để làm việc với Entity Framework Core một cách dễ dàng.

## 📋 Danh sách EF Core Tasks

### 🔨 **ef-add-migration**

- **Mục đích**: Tạo migration mới
- **Cách dùng**: `Ctrl+Shift+P` → "Tasks: Run Task" → "ef-add-migration"
- **Input**: Nhập tên migration (ví dụ: "AddProductTable")

### 🚀 **ef-update-database**

- **Mục đích**: Apply migrations vào database
- **Cách dùng**: `Ctrl+Shift+P` → "Tasks: Run Task" → "ef-update-database"

### 🗑️ **ef-drop-database**

- **Mục đích**: Xóa toàn bộ database (cẩn thận!)
- **Cách dùng**: `Ctrl+Shift+P` → "Tasks: Run Task" → "ef-drop-database"

### ↩️ **ef-remove-migration**

- **Mục đích**: Xóa migration cuối cùng (chưa apply)
- **Cách dùng**: `Ctrl+Shift+P` → "Tasks: Run Task" → "ef-remove-migration"

### 📋 **ef-list-migrations**

- **Mục đích**: Xem danh sách tất cả migrations
- **Cách dùng**: `Ctrl+Shift+P` → "Tasks: Run Task" → "ef-list-migrations"

### 📜 **ef-script-migration**

- **Mục đích**: Tạo SQL script từ migrations
- **Cách dùng**: `Ctrl+Shift+P` → "Tasks: Run Task" → "ef-script-migration"
- **Output**: File `migration-script.sql` sẽ được tạo

### 🎯 **ef-update-to-migration**

- **Mục đích**: Update database đến một migration cụ thể
- **Cách dùng**: `Ctrl+Shift+P` → "Tasks: Run Task" → "ef-update-to-migration"
- **Input**: Nhập tên migration target (hoặc "0" để rollback hết)

### 🔄 **migrate-and-run** (Compound Task)

- **Mục đích**: Update database và chạy ứng dụng với hot reload
- **Cách dùng**: `Ctrl+Shift+P` → "Tasks: Run Task" → "migrate-and-run"

## 🎯 Workflow thông dụng

### 1. **Tạo migration mới khi thay đổi model**

```bash
# Chạy task: ef-add-migration
# Nhập tên: "AddProductFields"
```

### 2. **Apply migration vào database**

```bash
# Chạy task: ef-update-database
```

### 3. **Quick start development**

```bash
# Chạy task: migrate-and-run
# Sẽ tự động update DB và start app với hot reload
```

### 4. **Reset database hoàn toàn**

```bash
# 1. Chạy: ef-drop-database
# 2. Chạy: ef-update-database
```

### 5. **Rollback database về trạng thái trống**

```bash
# Chạy: ef-update-to-migration
# Nhập: "0"
```

## 🔧 Cấu hình

Tất cả các task đã được cấu hình để:

- **Project**: `Bulky.DataAccess` (nơi chứa DbContext)
- **Startup Project**: `BulkyWeb` (nơi chứa connection string)
- **Working Directory**: Root của workspace

## 💡 Tips

- Luôn build project trước khi chạy EF commands
- Kiểm tra connection string trong `appsettings.json`
- Backup database trước khi chạy `ef-drop-database`
- Sử dụng tên migration có ý nghĩa (VD: "AddProductTable", "UpdateCategoryFields")
