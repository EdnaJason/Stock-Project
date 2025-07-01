using FinShark.Domain.Dtos.Stock;
using MediatR;

namespace FinShark.Service.Stocks.Query.GetById
{
    public class GetByIdStockQuery : IRequest<StockDto>
    {
        public int Id { get; set; }
        //public GetByIdStockQuery(int id)
        //{
        //    Id = id;
        //}
    }
}
