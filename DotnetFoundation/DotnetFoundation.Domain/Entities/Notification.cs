namespace DotnetFoundation.Domain.Entities;

public enum ChannelTypeEnum
{
    SMTP = 1,
    MailGun = 2,
    WhatsApp = 3
}
public enum EmailEvents
{
    ForgetPassword
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
public class EmailTemplate
{
    public string? TemplatePath { get; set; }
    public string? Subject { get; set; }
}

public static class EmailConfig
{
    public static Dictionary<EmailEvents, EmailTemplate> EmailTemplatesDictionary = new()
    {
        {
            EmailEvents.ForgetPassword,
            new EmailTemplate
            {
                TemplatePath = "../DotnetFoundation.Domain/Templates/Emails/ForgetPasswordTemplate.html",
                Subject = "Forget password"
            }
        }

    };
}
