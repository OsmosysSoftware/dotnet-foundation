using DotnetFoundation.Application.Interfaces.Integrations;
using DotnetFoundation.Domain.Entities;
using DotnetFoundation.Domain.Enums;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace DotnetFoundation.Infrastructure.Integrations;

public class EmailService : IEmailService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public EmailService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<string> SendForgetPasswordEmailAsync(string email, string body)
    {
        string templatePath = _configuration["Emails:ForgetPassword:Path"] ?? throw new Exception("ForgetPassword template path Missing");
        string subject = _configuration["Emails:ForgetPassword:Subject"] ?? throw new Exception("ForgetPassword subject Missing");

        Notification payload = CreateNotificationPayload(email, subject, ReadHtmlTemplate(templatePath, body));

        return await SendNotificationAsync(payload).ConfigureAwait(false);
    }
    public async Task<string> SendChangePasswordEmailAsync(string email)
    {
        string templatePath = _configuration["Emails:PasswordChange:Path"] ?? throw new Exception("PasswordChange template path Missing");
        string subject = _configuration["Emails:PasswordChange:Subject"] ?? throw new Exception("PasswordChange subject Missing");

        Notification payload = CreateNotificationPayload(email, subject, ReadHtmlTemplate(templatePath, ""));

        return await SendNotificationAsync(payload).ConfigureAwait(false);
    }
    private Notification CreateNotificationPayload(string to, string subject, string htmlBody)
    {
        return new Notification
        {
            ChannelType = ChannelType.MailGun,
            Data = new NotificationData
            {
                From = _configuration["Notification:From"] ?? throw new Exception("From Address Missing"),
                To = to,
                Subject = subject,
                Text = "Notification Text",
                Html = htmlBody
            }
        };
    }
    private async Task<string> SendNotificationAsync(Notification payload)
    {
        string apiKey = Environment.GetEnvironmentVariable("OSMOX_SERVER_KEY") ?? throw new Exception("Server key Missing");
        string notificationApiUrl = _configuration["Notification:OsmoxServerUrl"] ?? throw new Exception("Server url Missing");

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

        string jsonBody = JsonConvert.SerializeObject(payload, new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        });

        using StringContent content = new(jsonBody, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PostAsync(notificationApiUrl, content).ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
        {
            string responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return responseContent;
        }
        else
        {
            throw new Exception($"Failed to send notification. Status code: {response.StatusCode}");
        }
    }

    private static string ReadHtmlTemplate(string templatePath, string body)
    {
        string htmlContent = File.ReadAllText(templatePath);
        // Replace placeholder in the template with the actual value
        htmlContent = htmlContent.Replace("{body}", body);
        return htmlContent;
    }
}
