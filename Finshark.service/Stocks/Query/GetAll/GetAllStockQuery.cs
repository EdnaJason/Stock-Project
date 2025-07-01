//using FinShark.api.Dtos.Stock;
//using MediatR;


using FinShark.Domain.Dtos.Stock;
using MediatR;

namespace FinShark.Service.Stocks.Query.GetAll
{
    public class GetAllStockQuery : IRequest<List<StockDto>>    //Here we are using MediatR library to implement CQRS pattern. IRequest is a marker interface that indicates this class is a request that can be handled by a handler.
    {

    }
}
