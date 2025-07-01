using AutoMapper;
//using FinShark.api.Dtos.Stock;
//using FinShark.api.Features.StockFeature.Command.Create;
//using FinShark.api.Models;
using FinShark.Domain.Dtos.Stock;
using FinShark.Domain.Entities;
using FinShark.Service.Stocks.Command.Create;


namespace FinShark.api.Mappers
{
    public class StockMappers : Profile    // Mapper class to convert Stock model to StockDto
    {
        //public static StockDto ToStockDto(this Stock StockModel)    //extension method //Stock here is the model that we are converting to a DTO
        //{                                //this keyword refers to the instance of the class that the method is being called on                            
        //    return new StockDto                 //you can omitting things you dont want
        //    {
        //        Id = StockModel.Id,
        //        Symbol = StockModel.Symbol,
        //        CompanyName = StockModel.CompanyName,
        //        Purchase = StockModel.Purchase,
        //        LastDiv = StockModel.LastDiv,
        //        Industry = StockModel.Industry,
        //        MarketCap = StockModel.MarketCap
        //    };
        //    // When you add Dtos to your project, only the properties that are added will be displayed in the API response.
        //}

        public StockMappers()
        {
            CreateMap<Stock, StockDto>();
            CreateMap<Stock, CreateStockRequestDto>();
            CreateMap<CreateStockRequestDto, Stock>();
            CreateMap<Stock, UpdateStockRequestDto>();
            CreateMap<UpdateStockRequestDto, Stock>();
            //.ForMember(d => d.Id, opt => opt.Ignore()); ;
            CreateMap<CreateStockCommand, Stock>();
            //CreateMap<Source, Destination>();
        }

    }
}
