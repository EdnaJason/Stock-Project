﻿
using FinShark.Domain.Dtos.Stock;
using MediatR;

namespace FinShark.Service.Stocks.Command.Update
{
    public class UpdateStockCommand : IRequest<StockDto>
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public int Purchase { get; set; }
        public int LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }
    }
}
