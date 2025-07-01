using AutoMapper;
using FinShark.Domain.Dtos.Stock;
using FinShark.Domain.Repositories;
//using Azure.Core;
//using FinShark.api.Dtos.Stock;
//using FinShark.api.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinShark.Service.Stocks.Command.Update
{
    public class UpdateStockCommandHandler : IRequestHandler<UpdateStockCommand, StockDto>
    {
        private readonly IStockRepository _stockRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateStockCommandHandler> _logger;

        public UpdateStockCommandHandler(IStockRepository stockRepo, IMapper mapper, ILogger<UpdateStockCommandHandler> logger)
        {
            _stockRepo = stockRepo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<StockDto> Handle(UpdateStockCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Updating stock with id {request.Id}");
            var updateDto = _mapper.Map<UpdateStockCommandDto>(request);
            var StockDomain = await _stockRepo.UpdateAsync(updateDto);
            if (StockDomain == null)
            {
                _logger.LogWarning($"Stock with id {request.Id} not found");
                throw new KeyNotFoundException($"Stock with id {request.Id} not found");
            }
            _logger.LogInformation($"Successfully updated stock with id {request.Id}");
            return _mapper.Map<StockDto>(StockDomain);
        }
    }
}
