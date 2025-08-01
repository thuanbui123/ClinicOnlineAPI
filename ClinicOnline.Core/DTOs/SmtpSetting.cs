﻿namespace ClinicOnline.Core.DTOs;

public class SmtpSetting
{
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public string User { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string From { get; set; } = string.Empty;
}
