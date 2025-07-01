using FinShark.Domain.Dtos.Stock;
using MediatR;

namespace FinShark.Service.Stocks.Command.Create
{
    public class CreateStockCommand : IRequest<StockDto>
    {
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public int Purchase { get; set; }
        public int LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }
    }
}
