using api.Data;
using api.Dtos.Stock;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/stock")]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            //.Select funciona como o .map() do JavaScript
            var stocks = _context.Stocks.ToList()
                .Select(s => s.ToStockDto());

            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetStockById([FromRoute] Guid id)
        {
            //.Find permite buscar pela PK
            var stock = _context.Stocks.Find(id);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public IActionResult CreateStock([FromBody] CreateStockRequestDto stock)
        {
            var stockModel = stock.ToStockFromCreateStockRequestDto();
            _context.Stocks.Add(stockModel);
            _context.SaveChanges();

            //irá executar o GetStockById, passando o Id e retornando no formato StockDto
            return CreatedAtAction(nameof(GetStockById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateStock([FromRoute] Guid id, [FromBody] UpdateStockDto updateStock)
        {
            var stock = _context.Stocks.Find(id);

            if (stock == null)
            {
                return NotFound();
            }

            bool allFieldsAreNull = updateStock.Symbol == null && updateStock.CompanyName == null && updateStock.Industry == null && updateStock.Purchase == null && updateStock.LastDiv == null && updateStock.MarketCap == null;

            if (allFieldsAreNull)
            {
                return BadRequest(new { error = "All fields are null! " });
            }

            if (updateStock.Symbol != null)
            {
                stock.Symbol = updateStock.Symbol;
            }
            if (updateStock.CompanyName != null)
            {
                stock.CompanyName = updateStock.CompanyName;
            }
            if (updateStock.Industry != null)
            {
                stock.Industry = updateStock.Industry;
            }
            if (updateStock.Purchase != null)
            {
                stock.Purchase = (decimal)updateStock.Purchase;
            }
            if (updateStock.LastDiv != null)
            {
                stock.LastDiv = (decimal)updateStock.LastDiv;
            }
            if (updateStock.MarketCap != null)
            {
                stock.MarketCap = (long)updateStock.MarketCap;
            }

            _context.SaveChanges();

            return Ok(stock.ToStockDto());

        }
    }
}