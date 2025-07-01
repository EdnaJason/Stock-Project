using AutoMapper;
using FinShark.Domain.Dtos.Stock;
using FinShark.Service.Stocks.Command.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinShark.Service.Mappers
{
    public class ServiceStockMappers : Profile
    {
        public ServiceStockMappers() {
            CreateMap<UpdateStockCommand, UpdateStockCommandDto>();
        }
    }
}
