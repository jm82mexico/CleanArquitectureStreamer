using Asys.Application.Contracts.Persistence;
using Asys.Application.Infrastructure;
using Asys.Application.Models;
using Asys.Domain;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Asys.Application.Features.Streamers.Commands.CreateStreamer
{
    public class CreateStreamerCommandHandler : IRequestHandler<CreateStreamerCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CreateStreamerCommandHandler> _logger;
        public CreateStreamerCommandHandler( IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService, ILogger<CreateStreamerCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<int> Handle(CreateStreamerCommand request, CancellationToken cancellationToken)
        {
            var streamerEntity = _mapper.Map<Streamer>(request);

            _unitOfWork.StreamerRepository.AddEntity(streamerEntity);

            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                throw new Exception("Failed to create the streamer");
            }

            string message = $"New streamer {streamerEntity.Nombre} was created at {DateTime.Now}";
            _logger.LogInformation(message);

            return streamerEntity.Id;
        }

        private async Task SendEmail(Streamer streamer)
        {
           var Email = new Email()
           {
                To = "jmdelan2012@gmail.com",
                Body = $"New streamer {streamer.Nombre} was created",
                Subject = "New Streamer"
           };

           try
           {
               await _emailService.SendEmail(Email);
           }
           catch (Exception ex)
           {
               _logger.LogError($"Mailing about the streamer {streamer.Nombre} failed due to an error with the mail service: {ex.Message}");
           }
        }
    }

}