namespace DotnetFoundation.Domain.Enums;

/// <summary>
/// Defines the various channel types for notifications.
/// </summary>
public enum ChannelType
{
    /// <summary>
    /// Represents the SMTP email service.
    /// </summary>
    SMTP = 1,
    /// <summary>
    /// Represents the MailGun email service.
    /// </summary>
    MailGun = 2,
    /// <summary>
    /// Represents the WhatsApp messaging service.
    /// </summary>
    WhatsApp = 3
}
