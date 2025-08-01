using ClinicOnline.Core.Entities;

namespace ClinicOnline.Infrastructure.Test.Mocks;

public class DoctorMock
{
    public static Doctor Data (string speciality, string hospital, string degree, int experienceYears, string bio, string certificateFile, bool isDeleted) 
    {
        return new Doctor
        {
            Speciality = speciality,
            Hospital = hospital,
            Degree = degree,
            ExperienceYears = experienceYears,
            Bio = bio,
            CertificateFile = certificateFile,
            IsDeleted = isDeleted,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            CreatedBy = "Admin",
            UpdateBy = "Admin"
        };
    }
}
