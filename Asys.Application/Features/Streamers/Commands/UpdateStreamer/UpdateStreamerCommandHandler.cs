using Asys.Application.Contracts.Persistence;
using Asys.Application.Exeptions;
using Asys.Domain;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Asys.Application.Features.Streamers.Commands.UpdateStreamer
{
    public class UpdateStreamerCommandHandler : IRequestHandler<UpdateStreamerCommand>
    {

        private readonly IUnitOfWork   _unitOfWork;
        private readonly IMapper _mapper;
        private ILogger<UpdateStreamerCommandHandler> _logger;

        public UpdateStreamerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateStreamerCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }        
        public async Task Handle(UpdateStreamerCommand request, CancellationToken cancellationToken)
        {
            var streamerToUpdate = await _unitOfWork.StreamerRepository.GetByIdAsync(request.Id);

            if (streamerToUpdate == null)
            {
                _logger.LogError($"Streamer {request.Id} does not exist in the database");
                throw new NotFoundException(nameof(Streamer), request.Id);
            }

            _mapper.Map(request, streamerToUpdate, typeof(UpdateStreamerCommand), typeof(Streamer));

            _unitOfWork.StreamerRepository.UpdateEntity(streamerToUpdate);

            await _unitOfWork.Complete();

            _logger.LogInformation($"La operación de actualización de Streamer {request.Id} fue exitosa.");
        }
    }

}