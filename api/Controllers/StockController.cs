using FinShark.api.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using FinShark.api.Models;
using FinShark.api.Mappers;
using FinShark.api.Dtos.Stock;
using AutoMapper;           //assuming you have a Mappers folder with StockMappers.cs for mapping Stock to StockDto
using Microsoft.EntityFrameworkCore;
using FinShark.api.Interfaces;
using MediatR

namespace FinShark.api.Controllers
{
    [Route("api/Stock")]    //attribute routing
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;     //readonly field to make it immutable after initialization
        private readonly IMapper _mapper;
        private readonly IStockRepository _stockRepo;
        public StockController(ApplicationDBContext context, IMapper mapper, IStockRepository stockRepo)    //dependency injection : Rather than creating a new object with new() we are directly injecting the object through constructor
        {
            _context = context;
            _mapper = mapper; //if you need to use AutoMapper for mapping between entities and DTOs
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _stockRepo.GetAllAsync();  //fetch all stocks from the database. Differed execution: the query is not executed until the data is actually needed.
            var stockDto = _mapper.Map<List<StockDto>>(stocks); // Map<Destination>(Source) //dont forget to convert stockdto to list 

            //.Select(s => s.ToStockDto()); //Select is same as mapping
            return Ok(stockDto);   //return the list of stocks as a response with HTTP status code 200 (OK)
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById([FromRoute] int id)    //model binding: the id paramter(string) from route is extracted and converted to an int 
        {
            var stockDomain = await _stockRepo.GetByIdAsync(id);  //fetch the stock with the given id from the database using the repository method

            if (stockDomain == null)
            { 
                return NotFound();  //if stock with given id is not found, return HTTP status code 404 (Not Found)
            }
            var stockDto = _mapper.Map<StockDto>(stockDomain);
            return Ok(stockDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockmodel = _mapper.Map<Stock>(stockDto);    //During get function, were getting the info from the tables and mapping it to stockdto(hence the doman model(stock) is the source), whereas incase of post we map the data from postman to stockdto and it is further added to the table(hence, stockdto is the source)
            await _stockRepo.CreateAsync(stockmodel);
            var StockDtoResponse = _mapper.Map<StockDto>(stockmodel);
            //Console.WriteLine(nameof(GetById));
            return CreatedAtAction(nameof(GetById), new { id = StockDtoResponse.Id}, StockDtoResponse); //return the created stock as a response with HTTP status code 200 (OK)
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            var StockDomain = await _stockRepo.UpdateAsync(id, updateDto);
            if (StockDomain == null)
            {
                return NotFound();
            }
            var updateDtoResponse = _mapper.Map<UpdateStockRequestDto>(StockDomain);
            return Ok(updateDtoResponse); //map the updated stock domain model to StockDto
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var StockDomain = await _stockRepo.DeleteAsync(id); 
            if (StockDomain == null)
            {
                return NotFound();
            }
            return NoContent(); //return HTTP status code 204 (No Content) to indicate successful deletion without returning any content
        }
    }
}
