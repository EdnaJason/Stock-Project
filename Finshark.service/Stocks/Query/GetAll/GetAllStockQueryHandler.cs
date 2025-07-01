
using AutoMapper;
using FinShark.Domain.Dtos.Stock;
using FinShark.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinShark.Service.Stocks.Query.GetAll
{
    public class GetAllStockQueryHandler : IRequestHandler<GetAllStockQuery, List<StockDto>>
    {
        private readonly IStockRepository _stockRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllStockQueryHandler> _logger;

        public GetAllStockQueryHandler(IStockRepository stockRepo, IMapper mapper, ILogger<GetAllStockQueryHandler> logger)
        {
            _stockRepo = stockRepo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<StockDto>> Handle(GetAllStockQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Fetching all the stocks");
            var stocks = await _stockRepo.GetAllAsync(); // Fetch all stocks from the repository
            _logger.LogInformation($"All stocks fetched successfully");
            return _mapper.Map<List<StockDto>>(stocks); // Map the stock entities to StockDto
        }
    }
}
