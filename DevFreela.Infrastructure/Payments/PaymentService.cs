using DevFreela.Application.Services.Interfaces;
using DevFreela.Payments.API.Models;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using DevFreela.Core.Services;

namespace DevFreela.Application.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMessageBusService _messageBusService;
        private readonly string _paymentsBaseUrl;
        private const string QUEUE_PAYMENTS = "payments.devfreela";

        public PaymentService(IHttpClientFactory httpClientFactory, IConfiguration configuration, IMessageBusService messageBusService)
        {
            _httpClientFactory = httpClientFactory;
            _paymentsBaseUrl = configuration.GetSection("Services:Payments").Value;
            _messageBusService = messageBusService;
        }

        public async Task<bool> ProcessPaymentHttpClient(PaymentInfoDTO paymentInfoDTO)
        {
            var url = $"{_paymentsBaseUrl}/api/payments";
            var paymentInfoJson = JsonSerializer.Serialize(paymentInfoDTO);

            var paymentInfoContent = new StringContent(
                paymentInfoJson,
                Encoding.UTF8,
                "application/json");

            var httpClient = _httpClientFactory.CreateClient("Payments");
            var response = await httpClient.PostAsync(url, paymentInfoContent);

            return response.IsSuccessStatusCode;
        }

        public async Task ProcessPaymentRabbitMQ(PaymentInfoDTO paymentInfoDTO)
        {
            var paymentInfoJson = JsonSerializer.Serialize(paymentInfoDTO);
            var paymentInfoBytes = Encoding.UTF8.GetBytes(paymentInfoJson);

            _messageBusService.Publish(QUEUE_PAYMENTS, paymentInfoBytes);
        }
    }
}
