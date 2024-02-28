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
        // Read the JWT token from the environment variable
        string apiKey = Environment.GetEnvironmentVariable("OSMOX_SERVER_KEY") ?? throw new Exception("Server key Missing");
        string notificationApiUrl = _configuration["Notification:OsmoxServerUrl"] ?? throw new Exception("Server url Missing");

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
        Notification payload = new Notification
        {
            ChannelType = ChannelType.MailGun,
            Data = new NotificationData
            {
                From = _configuration["Notification:From"] ?? throw new Exception("From Address Missing"),
                To = email,
                Subject = _configuration["Emails:ForgetPassword:Subject"],
                Text = "Forget password Token",
                Html = ReadHtmlTemplate(_configuration["Emails:ForgetPassword:Path"]!, body)
            }
        };

        // Convert payload to JSON with lowercase property names
        string jsonBody = JsonConvert.SerializeObject(payload, new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        });

        using StringContent content = new(jsonBody, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PostAsync(notificationApiUrl, content).ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
        {
            // Notification sent successfully, handle the response as needed.
            string responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return responseContent;
        }
        else
        {
            // Handle the case where the notification was not sent successfully.
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
