using AutoMapper;
//using FinShark.api.Dtos.Stock;
using FinShark.Domain.Dtos.Stock;
//using FinShark.api.Interfaces;
using FinShark.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinShark.Service.Stocks.Command.Delete
{
    public class DeleteStockCommandHandler : IRequestHandler<DeleteStockCommand, StockDto>
    {
        private readonly IStockRepository _stockRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteStockCommandHandler> _logger;

        public DeleteStockCommandHandler(IStockRepository stockRepo, IMapper mapper, ILogger<DeleteStockCommandHandler> logger)
        {
            _stockRepo = stockRepo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<StockDto> Handle(DeleteStockCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Deleting stock with id {request.Id}");
            var StockDomain = await _stockRepo.DeleteAsync(request.Id);
            if (StockDomain == null)
            {
                _logger.LogWarning($"Stock with id {request.Id} not found");
                throw new KeyNotFoundException($"Stock with id {request.Id} not found");
            }
            _logger.LogInformation($"Successfully deleted stock with id {request.Id}");
            return _mapper.Map<StockDto>(StockDomain);
        }
    }
}
