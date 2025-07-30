using ClinicOnline.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClinicOnline.Infrastructure.DatabaseContexts;

public partial class ClinicManagementContext : DbContext
{
    public ClinicManagementContext()
    {
    }

    public ClinicManagementContext(DbContextOptions<ClinicManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<ArticleComment> ArticleComments { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<MedicalRecord> MedicalRecords { get; set; }

    public virtual DbSet<Medicine> Medicines { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Prescription> Prescriptions { get; set; }

    public virtual DbSet<PrescriptionItem> PrescriptionItems { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<StockTransaction> StockTransactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=clinic_management;Uid=postgres;Pwd=Thuan0101#;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pgcrypto");

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Appointments_pkey");

            entity.ToTable(tb => tb.HasComment("Quản lý lịch hẹn"));

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.Note).HasComment("(triệu chứng, yêu cầu...)");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasComment("Pending, Confirmed, Cancelled, Completed");
            entity.Property(e => e.UpdateBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Appointments_DoctorId_fkey");

            entity.HasOne(d => d.Patient).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Appointments_PatientId_fkey");

            entity.HasOne(d => d.Schedule).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.ScheduleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Appointments_ScheduleId_fkey");
        });

        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Articles_pkey");

            entity.ToTable(tb => tb.HasComment("Bài viết tư vấn"));

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.Content).HasComment("Nội dung chính");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasComment("Tiêu đề bài viết");
            entity.Property(e => e.UpdateBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Articles)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Articles_DoctorId_fkey");
        });

        modelBuilder.Entity<ArticleComment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ArticleComments_pkey");

            entity.ToTable(tb => tb.HasComment("bảng lưu các bình luận về bài viết"));

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.ArticleId).HasComment("Bài viết nào");
            entity.Property(e => e.Comment).HasComment("Nội dung bình luận");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.ParentCommentId).HasComment("Lưu Id của bình luận gốc nếu đây là trả lời (reply)");
            entity.Property(e => e.UpdateBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.UserId).HasComment("Ai bình luận");

            entity.HasOne(d => d.Article).WithMany(p => p.ArticleComments)
                .HasForeignKey(d => d.ArticleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ArticleComments_ArticleId_fkey");

            entity.HasOne(d => d.ParentComment).WithMany(p => p.InverseParentComment)
                .HasForeignKey(d => d.ParentCommentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_parent_comment");

            entity.HasOne(d => d.User).WithMany(p => p.ArticleComments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ArticleComments_UserId_fkey");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Doctors_pkey");

            entity.ToTable(tb => tb.HasComment("Bảng lưu thông tin bác sĩ"));

            entity.HasIndex(e => e.UserId, "Doctors_UserId_key").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.Bio).HasComment("Mô tả bản thân, kinh nghiệm,…");
            entity.Property(e => e.CertificateFile).HasComment("URL file chứng chỉ, văn bằng");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.Degree)
                .HasMaxLength(100)
                .HasComment("Học vị: Cử nhân, Thạc sĩ, Tiến sĩ,…");
            entity.Property(e => e.ExperienceYears).HasComment("Số năm kinh nghiệm hành nghề");
            entity.Property(e => e.Hospital)
                .HasMaxLength(100)
                .HasComment("Nơi làm việc chính");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.ProfilePhoto).HasComment("URL ảnh đại diện");
            entity.Property(e => e.Speciality)
                .HasMaxLength(100)
                .HasComment("Chuyên khoa: Nội, Nhi, Tim mạch,…");
            entity.Property(e => e.UpdateBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.User).WithOne(p => p.Doctor)
                .HasForeignKey<Doctor>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Doctors_UserId_fkey");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Invoices_pkey");

            entity.ToTable(tb => tb.HasComment("Hóa đơn"));

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.Amount)
                .HasPrecision(10, 2)
                .HasComment("Số tiền cần trả");
            entity.Property(e => e.AppointmentId).HasComment("Liên kết lịch khám");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.PatientId).HasComment("Ai thanh toán");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(30)
                .HasComment("MoMo, ZaloPay, Tiền mặt…");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasComment("Pending, Paid, Failed");
            entity.Property(e => e.UpdateBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.Appointment).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Invoices_AppointmentId_fkey");

            entity.HasOne(d => d.Patient).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Invoices_PatientId_fkey");
        });

        modelBuilder.Entity<MedicalRecord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MedicalRecords_pkey");

            entity.ToTable(tb => tb.HasComment("Hồ sơ khám bệnh"));

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.AppointmentId).HasComment("Gắn với cuộc hẹn tương ứng");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.Diagnosis).HasComment("Chẩn đoán bệnh chính");
            entity.Property(e => e.FollowUpDate).HasComment("Ngày tái khám (nếu có)");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.Symptoms).HasComment("Triệu chứng mô tả");
            entity.Property(e => e.TreatmentPlan).HasComment("Kế hoạch điều trị hoặc tư vấn");
            entity.Property(e => e.UpdateBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.Appointment).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MedicalRecords_AppointmentId_fkey");

            entity.HasOne(d => d.Doctor).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MedicalRecords_DoctorId_fkey");

            entity.HasOne(d => d.Patient).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MedicalRecords_PatientId_fkey");
        });

        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Medicines_pkey");

            entity.ToTable(tb => tb.HasComment("Danh mục thuốc"));

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.Description).HasComment("Mô tả công dụng");
            entity.Property(e => e.ExpiryDate).HasComment("Ngày hết hạn sử dụng");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasComment("Tên thuốc");
            entity.Property(e => e.Unit)
                .HasMaxLength(20)
                .HasComment("Đơn vị thuốc");
            entity.Property(e => e.UpdateBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Messages_pkey");

            entity.ToTable(tb => tb.HasComment("Tin nhắn giữa người dùng"));

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.Content).HasComment("Nội dung tin nhắn");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.IsRead)
                .HasDefaultValue(false)
                .HasComment("Đã đọc hay chưa");
            entity.Property(e => e.ReceiverId).HasComment("Người nhận");
            entity.Property(e => e.SenderId).HasComment("Người gửi");
            entity.Property(e => e.SentAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("Thời điểm gửi")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.UpdateBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.Receiver).WithMany(p => p.MessageReceivers)
                .HasForeignKey(d => d.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Messages_ReceiverId_fkey");

            entity.HasOne(d => d.Sender).WithMany(p => p.MessageSenders)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Messages_SenderId_fkey");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Notifications_pkey");

            entity.ToTable(tb => tb.HasComment("Ghi nhận gửi thông báo"));

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.Content).HasComment("Nội dung tin nhắn/email");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.SentAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("Thời điểm gửi")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.Type)
                .HasMaxLength(10)
                .HasComment("Email hoặc Web");
            entity.Property(e => e.UpdateBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.UserId).HasComment("Ai nhận thông báo");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Notifications_UserId_fkey");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Patients_pkey");

            entity.ToTable(tb => tb.HasComment("Hồ sơ bệnh nhân"));

            entity.HasIndex(e => e.UserId, "Patients_UserId_key").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.Address).HasComment("Địa chỉ cư trú");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.Dob).HasComment("Ngày sinh");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasComment("Giới tính");
            entity.Property(e => e.InsuranceNumber)
                .HasMaxLength(50)
                .HasComment("Mã bảo hiểm y tế nếu có");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.MedicalHistory).HasComment("Tiền sử bệnh án");
            entity.Property(e => e.UpdateBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.User).WithOne(p => p.Patient)
                .HasForeignKey<Patient>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Patients_UserId_fkey");
        });

        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Prescriptions_pkey");

            entity.ToTable(tb => tb.HasComment("Đơn thuốc"));

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.AppointmentId).HasComment("Gắn với lịch hẹn khám");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.DoctorNote).HasComment("Ghi chú đơn thuốc");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.UpdateBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.Appointment).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Prescriptions_AppointmentId_fkey");
        });

        modelBuilder.Entity<PrescriptionItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PrescriptionItems_pkey");

            entity.ToTable(tb => tb.HasComment("Các dòng thuốc trong đơn"));

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.PrescriptionId).HasComment("Gắn với đơn thuốc");
            entity.Property(e => e.Quantity).HasComment("Liều lượng kê");
            entity.Property(e => e.UpdateBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.Usage).HasComment("Hướng dẫn dùng thuốc (uống 2 lần/ngày, sau ăn…)");

            entity.HasOne(d => d.Medicine).WithMany(p => p.PrescriptionItems)
                .HasForeignKey(d => d.MedicineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PrescriptionItems_MedicineId_fkey");

            entity.HasOne(d => d.Prescription).WithMany(p => p.PrescriptionItems)
                .HasForeignKey(d => d.PrescriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PrescriptionItems_PrescriptionId_fkey");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Schedules_pkey");

            entity.ToTable(tb => tb.HasComment("Lịch làm việc bác sĩ"));

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.IsAvailable)
                .HasDefaultValue(true)
                .HasComment("Còn trống không? Nếu false là đã có người đặt");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.TimeSlot)
                .HasMaxLength(20)
                .HasComment("Khung giờ cụ thể (ví dụ: \"08:00-08:30\")");
            entity.Property(e => e.UpdateBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.WorkDate).HasComment("Ngày làm việc");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Schedules_DoctorId_fkey");
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Stocks_pkey");

            entity.ToTable(tb => tb.HasComment("Tồn kho thuốc"));

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.Location)
                .HasMaxLength(100)
                .HasComment("Vị trí kho hoặc chi nhánh lưu thuốc");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(0)
                .HasComment("Số lượng tồn kho hiện tại");
            entity.Property(e => e.UpdateBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.Medicine).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.MedicineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Stocks_MedicineId_fkey");
        });

        modelBuilder.Entity<StockTransaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("StockTransactions_pkey");

            entity.ToTable(tb => tb.HasComment("Lịch sử nhập/xuất thuốc"));

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.Quantity).HasComment("Số lượng thay đổi");
            entity.Property(e => e.Reason).HasComment("Lý do (kê đơn, hỏng, hết hạn…)");
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .HasComment("Import, Export, Use – loại giao dịch");
            entity.Property(e => e.UpdateBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.Medicine).WithMany(p => p.StockTransactions)
                .HasForeignKey(d => d.MedicineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("StockTransactions_MedicineId_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Users_pkey");

            entity.HasIndex(e => e.Email, "Users_Email_key").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Role).HasMaxLength(20);
            entity.Property(e => e.Status).HasDefaultValue(true);
            entity.Property(e => e.UpdateBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
