# 🍕 Pizza Ordering Web App

Ứng dụng web đặt món cho nhà hàng chuyên các món:
- Pizza
- Mì Ý
- Salad
- Món khai vị (súp bí đỏ, bánh mì bơ tỏi,...)
- Bít tết
- Món tráng miệng (sữa chua, trái cây,...)
- Nước uống (có cồn & không cồn)

---

## 🚀 Hướng dẫn sử dụng

### 1. ⚙️ Cấu hình chuỗi kết nối CSDL

Mở file `appsettings.json` và sửa phần `ConnectionStrings`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=PizzaOrdering;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

📌 **Lưu ý:**
- Nếu dùng SQL Server Authentication:

  ```json
  "DefaultConnection": "Server=localhost;Database=PizzaOrdering;User Id=sa;Password=yourpassword;TrustServerCertificate=True;"
  ```

- Đảm bảo SQL Server đang chạy và có quyền tạo database.

---

### 2. 🛠️ Cập nhật database bằng Entity Framework Core

Mở terminal tại thư mục chứa `.csproj` và chạy:

```bash
dotnet ef database update
```

Lệnh này sẽ:
- Tự động tạo database (nếu chưa có)
- Tạo bảng dựa theo các migration đã có

> ⚠️ Nếu chưa có migration nào, bạn có thể tạo bằng:
> ```bash
> dotnet ef migrations add InitialCreate
> ```

---

### 3. ▶️ Chạy ứng dụng

```bash
dotnet run
```

Mở trình duyệt và truy cập `https://localhost:port` để bắt đầu sử dụng ứng dụng.

---

## ✅ Yêu cầu

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server
- Entity Framework Core CLI (`dotnet tool install --global dotnet-ef`)
