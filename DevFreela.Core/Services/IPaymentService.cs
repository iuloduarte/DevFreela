using DevFreela.Payments.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> ProcessPaymentHttpClient(PaymentInfoDTO paymentInfoDTO);
        Task ProcessPaymentRabbitMQ(PaymentInfoDTO paymentInfoDTO);
    }
}
