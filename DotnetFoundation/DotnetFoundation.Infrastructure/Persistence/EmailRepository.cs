using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DotnetFoundation.Application.Interfaces.Persistence;
using DotnetFoundation.Application.Interfaces.Services;
using DotnetFoundation.Domain.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DotnetFoundation.Infrastructure.Persistence;

public class EmailRepository : IEmailRepository
{
    private readonly HttpClient _httpClient;
    public EmailRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; // Ensure secure connection
    }

    public async Task<string> SendForgetPasswordEmailAsync(string email, string subject, string body)
    {
        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.7ZK6fb6h52dNdy_");
        string notificationApiUrl = "https://notify.osmosys.co/notifications";
        Notification payload = new Notification
        {
            ChannelType = ChannelTypeEnum.MailGun,
            Data = new NotificationData
            {
                From = "FoundationX@osmosys.com",
                To = email,
                Subject = "Forget password Token",
                Text = "Forget password Token",
                Html = $"<p>Token {body}</p>"
            }
        };

        // Convert payload to JSON with lowercase property names
        string jsonBody = JsonConvert.SerializeObject(payload, new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        });

        StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
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
}