# 🏥 ClinicOnline – Hệ thống quản lý phòng khám trực tuyến

**ClinicOnline** là một hệ thống quản lý phòng khám hiện đại, hỗ trợ đầy đủ quy trình nghiệp vụ như đặt lịch khám, theo dõi hồ sơ bệnh án, quản lý thuốc, và tương tác giữa bệnh nhân và bác sĩ. Dự án được thiết kế theo **kiến trúc Clean Architecture kết hợp Microservices định hướng mở rộng** – sẵn sàng cho hệ thống quy mô lớn.

---

## 🚀 Tính năng nổi bật

- 📅 Đặt lịch khám và quản lý lịch hẹn cho từng bác sĩ  
- 💬 Chat, nhắn tin & hỏi đáp giữa bệnh nhân và bác sĩ  
- 🧑‍⚕️ Quản lý hồ sơ bệnh nhân, bác sĩ, nhân viên, vai trò  
- 💊 Quản lý kho thuốc, đơn thuốc điện tử  
- 📝 Bài viết y khoa & hệ thống bình luận  
- 📈 Báo cáo thống kê, doanh thu, lượt khám  
- 🔐 Hệ thống xác thực phân quyền Role-based & Policy-based (JWT)  

---

## 🧱 Kiến trúc dự án

### Dự án được tổ chức theo chuẩn **Clean Architecture**, tách biệt rõ ràng từng tầng:

#### 📁 ClinicOnline.API --> Tầng Presentation (Web API)
#### 📁 ClinicOnline.Core --> Tầng Domain & Application (Business logic, DTOs, Events)
#### 📁 ClinicOnline.Infrastructure --> Tầng triển khai hạ tầng (DB, Messaging, Repository)

---

## 🧪 Hướng dẫn chạy nhanh


### 📦 1. Clone project

```bash
git clone https://github.com/yourname/ClinicOnline.git
cd ClinicOnline
```

### 🐋 2. Chạy môi trường Docker

```bash
docker-compose up -d
```

### 🚀 3. Chạy ứng dụng

```bash
dotnet run --project ClinicOnline.API
```
📎 Truy cập Swagger: https://localhost:5001/swagger

---

## 🔐 Phân quyền hệ thống

Hệ thống hỗ trợ nhiều vai trò:

- **Admin**: quản trị toàn bộ hệ thống  
- **Doctor**: khám bệnh, viết bài tư vấn  
- **Patient**: đặt lịch, tra cứu toa thuốc, hỏi bác sĩ  

---

## 🧠 Điểm nổi bật kiến trúc

✅ **Clean Architecture**: tách biệt domain với tầng hạ tầng  
✅ **Event-driven design**: mở rộng logic mà không ảnh hưởng hệ thống cũ  
✅ **Dễ mở rộng theo hướng Microservice**  
✅ **Tối ưu cho phỏng vấn**: trình bày kiến trúc rõ ràng, có chiều sâu kỹ thuật  

---

## 📬 Thông tin liên hệ
#### 📧 Email: thuanbui31819@gmail.com

#### 💼 LinkedIn: https://www.linkedin.com/in/thuan-bui-a36244233/

#### 🧑 GitHub: https://github.com/thuanbui123
