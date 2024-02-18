namespace DotnetFoundation.Domain.Entities;

public enum ChannelTypeEnum
{
    SMTP = 1,
    MailGun = 2,
    WhatsApp = 3
}

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
    public ChannelTypeEnum ChannelType { get; set; }
    public NotificationData? Data { get; set; }
}
