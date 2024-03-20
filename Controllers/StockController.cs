using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/stock")]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;
        public StockController(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stocks = await _stockRepository.GetAllAsync(query);

            var stocksDto = stocks.Select(s => s.ToStockDto());

            return Ok(stocksDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetStockById([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stock = await _stockRepository.GetByIdAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] CreateStockRequestDto stock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stockModel = stock.ToStockFromCreateStockRequestDto();

            await _stockRepository.CreateAsync(stockModel);

            return CreatedAtAction(nameof(GetStockById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateStock([FromRoute] Guid id, [FromBody] UpdateStockDto updateStock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool allFieldsAreNull = updateStock.Symbol == null && updateStock.CompanyName == null && updateStock.Industry == null && updateStock.Purchase == null && updateStock.LastDiv == null && updateStock.MarketCap == null;

            if (allFieldsAreNull)
            {
                return BadRequest(new { error = "All fields are null! " });
            }

            var stockModel = await _stockRepository.UpdateAsync(id, updateStock);

            if (stockModel == null)
            {
                return NotFound();
            }

            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteStock([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stockModel = await _stockRepository.DeleteAsync(id);

            if (stockModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}