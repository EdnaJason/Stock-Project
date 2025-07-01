using AutoMapper;
using FinShark.Domain.Dtos.Stock;
using FinShark.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinShark.Service.Stocks.Query.GetById
{
    public class GetByIdStockQueryHandler : IRequestHandler<GetByIdStockQuery, StockDto>
    {
        private readonly IStockRepository _stockRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<GetByIdStockQueryHandler> _logger;

        public GetByIdStockQueryHandler(IStockRepository stockRepo, IMapper mapper,ILogger<GetByIdStockQueryHandler> logger)
        {
            _stockRepo = stockRepo;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<StockDto> Handle(GetByIdStockQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Fetching stock with id {request.Id}");
            var stockDomain = await _stockRepo.GetByIdAsync(request.Id);
            if (stockDomain == null)
            {
                _logger.LogWarning($"Stck with id {request.Id} not found");
                throw new KeyNotFoundException($"Stock with id {request.Id} not found");
            }
            _logger.LogInformation($"Stock with id {request.Id} fetched successfully");
            return _mapper.Map<StockDto>(stockDomain);
        }
    }
}
