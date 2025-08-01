﻿namespace ClinicOnline.Core.Entities;

/// <summary>
/// Quản lý lịch hẹn
/// </summary>
public partial class Appointment : BaseEntity
{
    public Guid PatientId { get; set; }

    public Guid DoctorId { get; set; }

    public Guid ScheduleId { get; set; }

    /// <summary>
    /// Pending, Confirmed, Cancelled, Completed
    /// </summary>
    public string Status { get; set; } = null!;

    /// <summary>
    /// (triệu chứng, yêu cầu...)
    /// </summary>
    public string? Note { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    public virtual Patient Patient { get; set; } = null!;

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();

    public virtual Schedule Schedule { get; set; } = null!;
}
