//using FinShark.api.Dtos.Stock;
using FinShark.Domain.Dtos.Stock;
using MediatR;

namespace FinShark.Service.Stocks.Command.Delete
{
    public class DeleteStockCommand : IRequest<StockDto>
    {
        public int Id { get; set; }
    }
}
