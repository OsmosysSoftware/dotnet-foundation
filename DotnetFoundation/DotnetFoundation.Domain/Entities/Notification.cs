using DotnetFoundation.Domain.Enums;

namespace DotnetFoundation.Domain.Entities;

public class NotificationData
{
    public string? From { get; set; }
    public string? To { get; set; }
    public string? Subject { get; set; }
    public string? Text { get; set; }
    public string? Html { get; set; }
}

public class Notification
{
    public ChannelType ChannelType { get; set; }
    public NotificationData? Data { get; set; }
}

public class EmailTemplate
{
    public string? TemplatePath { get; set; }
    public string? Subject { get; set; }
}

