using DevFreela.Application.Services.Interfaces;
using DevFreela.Core.Repositories;
using DevFreela.Payments.API.Models;
using MediatR;

namespace DevFreela.Application.Commands.Projects
{
    public class FinishProjectCommandHandler : IRequestHandler<FinishProjectCommand, bool>
    {
        private readonly IProjectRepository _repository;
        private readonly IPaymentService _paymentService;

        public FinishProjectCommandHandler(IProjectRepository repository, IPaymentService paymentService)
        {
            _repository = repository;
            _paymentService = paymentService;
        }

        public async Task<bool> Handle(FinishProjectCommand request, CancellationToken cancellationToken)
        {
            const bool processUsingRabbitMQ = true;

            var project = await _repository.GetProjectByIdAsync(request.Id);
            var paymentDTO = new PaymentInfoDTO(project.Id, request.CreditCardNumber, request.Cvv, request.ExpiresAt, request.FullName, project.TotalCost);

            if (processUsingRabbitMQ)
            {
                #region RabbitMQ

                project.SetPaymentPending();
                await _paymentService.ProcessPaymentRabbitMQ(paymentDTO);
                await _repository.SaveChangesAsync();
                return true; // Handle() could return void

                #endregion
            }
            else
            {
                #region HttpClient

                project.Finish();

                var paymentResult = await _paymentService.ProcessPaymentHttpClient(paymentDTO);
                if (!paymentResult) project.SetPaymentPending();
                await _repository.SaveChangesAsync();

                return paymentResult;

                #endregion
            }
        }
    }
}
