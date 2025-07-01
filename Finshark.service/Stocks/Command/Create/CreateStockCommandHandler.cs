using AutoMapper;
using FinShark.Domain.Dtos.Stock;
using FinShark.Domain.Repositories;
//using FinShark.api.Models;
using FinShark.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinShark.Service.Stocks.Command.Create
{
    public class CreateStockCommandHandler : IRequestHandler<CreateStockCommand, StockDto>
    {
        private readonly IStockRepository _stockRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateStockCommandHandler> _logger;

        public CreateStockCommandHandler(IStockRepository stockRepo, IMapper mapper, ILogger<CreateStockCommandHandler> logger)
        {
            _stockRepo = stockRepo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<StockDto> Handle(CreateStockCommand request, CancellationToken calncellationToken)
        {
            _logger.LogInformation($"Creating a stock with company name {request.CompanyName}");
            var stockDomain = _mapper.Map<Stock>(request);    //During get function, were getting the info from the tables and mapping it to stockdto(hence the doman model(stock) is the source), whereas incase of post we map the data from postman to stockdto and it is further added to the table(hence, stockdto is the source)
            stockDomain = await _stockRepo.CreateAsync(stockDomain);
            _logger.LogInformation($"Stock Created Successfully");
            return _mapper.Map<StockDto>(stockDomain);
        }
    }
}
