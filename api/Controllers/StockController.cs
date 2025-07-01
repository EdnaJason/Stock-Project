using AutoMapper;
using FinShark.api.Data;
using FinShark.Domain.Repositories;
using FinShark.Service.Stocks.Command.Create;
using FinShark.Service.Stocks.Command.Delete;
using FinShark.Service.Stocks.Command.Update;
using FinShark.Service.Stocks.Query.GetAll;
using FinShark.Service.Stocks.Query.GetById;
using MediatR;

using Microsoft.AspNetCore.Mvc;
using System.Linq;
//using FinShark.api.Models;
//using FinShark.api.Mappers;
//using FinShark.api.Dtos.Stock;
//using AutoMapper;           //assuming you have a Mappers folder with StockMappers.cs for mapping Stock to StockDto
using Microsoft.EntityFrameworkCore;
//using FinShark.api.Interfaces;
//using MediatR;
//using FinShark.api.Features.StockFeature.Query.GetAll;
//using FinShark.api.Features.StockFeature.Query.GetById;
//using FinShark.api.Features.StockFeature.Command.Create;
//using FinShark.api.Features.StockFeature.Command.Update;
//using FinShark.api.Features.StockFeature.Command.Delete;


namespace FinShark.api.Controllers
{
    [Route("api/Stock")]    //attribute routing
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;     //readonly field to make it immutable after initialization
        private readonly IMapper _mapper;
        private readonly IStockRepository _stockRepo;
        private readonly IMediator _mediator; //using MediatR for CQRS pattern
        public StockController(ApplicationDBContext context, IMapper mapper, IStockRepository stockRepo, IMediator mediator)    //dependency injection : Rather than creating a new object with new() we are directly injecting the object through constructor
        {
            _context = context;
            _mapper = mapper; //if you need to use AutoMapper for mapping between entities and DTOs
            _stockRepo = stockRepo;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //var stocks = await _stockRepo.GetAllAsync();  //fetch all stocks from the database. Differed execution: the query is not executed until the data is actually needed.
            //var stockDto = _mapper.Map<List<StockDto>>(stocks); // Map<Destination>(Source) //dont forget to convert stockdto to list 
            var stockDto = await _mediator.Send(new GetAllStockQuery()); //using MediatR to send the query and get the list of StockDto

            //.Select(s => s.ToStockDto()); //Select is same as mapping
            return Ok(stockDto);   //return the list of stocks as a response with HTTP status code 200 (OK)
        }

        [HttpGet]
        [Route("getById")]
        public async Task<IActionResult> GetById([FromBody] GetByIdStockQuery stockDto)    //model binding: the id paramter(string) from route is extracted and converted to an int 
        {
            //var stockDomain = await _stockRepo.GetByIdAsync(id);  //fetch the stock with the given id from the database using the repository method
            var stockDtoResponce = await _mediator.Send(stockDto); //using MediatR to send the query and get the StockDto by id
            //if (stockDomain == null)
            //{ 
            //    return NotFound();  //if stock with given id is not found, return HTTP status code 404 (Not Found)
            //}
            //var stockDto = _mapper.Map<StockDto>(stockDomain);
            return Ok(stockDtoResponce);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CreateStockCommand stockDto)
        {
            //var stockmodel = _mapper.Map<Stock>(stockDto);    //During get function, were getting the info from the tables and mapping it to stockdto(hence the doman model(stock) is the source), whereas incase of post we map the data from postman to stockdto and it is further added to the table(hence, stockdto is the source)
            //await _stockRepo.CreateAsync(stockmodel);
            //var StockDtoResponse = _mapper.Map<StockDto>(stockmodel);
            ////Console.WriteLine(nameof(GetById));
            var StockDtoResponse = await _mediator.Send(stockDto); //using MediatR to send the command and create a new stock
            return CreatedAtAction(nameof(GetById), new { id = StockDtoResponse.Id}, StockDtoResponse); //return the created stock as a response with HTTP status code 200 (OK)
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] UpdateStockCommand updateDto)
        {
            //var StockDomain = await _stockRepo.UpdateAsync(id, updateDto);
            //if (StockDomain == null)
            //{
            //    return NotFound();
            //}
            //var updateDtoResponse = _mapper.Map<UpdateStockRequestDto>(StockDomain);
            var updateDtoResponse = await _mediator.Send(updateDto); //using MediatR to send the command and update the stock
            return Ok(updateDtoResponse); //map the updated stock domain model to StockDto
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteStockCommand deleteDto)
        {
            //var StockDomain = await _stockRepo.DeleteAsync(id); 
            //if (StockDomain == null)
            //{
            //    return NotFound();
            //}
            var stockDto = await _mediator.Send(deleteDto);
            return Ok(stockDto); //return HTTP status code 204 (No Content) to indicate successful deletion without returning any content
        }
    }
}
