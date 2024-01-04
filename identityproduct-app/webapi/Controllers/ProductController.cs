using AutoMapper;
using identityproduct_app.Domain.Dto.Create;
using identityproduct_app.Domain.Dto.Read;
using identityproduct_app.Domain.Models;
using identityproduct_app.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace identityproduct_app.webapi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        private IMapper _mapper;

        public ProductController(IProductService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<ProductReadDto>>> GetAllAlunos()
        {
            var lstProduct = await _service.GetAllProducts();
            if (lstProduct.Count() == 0)
            {
                return NotFound("No Product data was found in the list");
            }
            var response = _mapper.Map<List<ProductReadDto>>(lstProduct).OrderBy(x => x.ProductId).ToList();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductReadDto>> GetById(int id)
        {
            Product product = await _service.GetProductById(id);
            if (product == null)
            {
                return NotFound($"Product with Id {id} not found");
            }
            ProductReadDto response = _mapper.Map<ProductReadDto>(product);
            return Ok(response);
        }


        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ProductCreateDto productDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Product product = _mapper.Map<Product>(productDto);
                await _service.CreateProduct(product);
                return Ok("Product added successfully.");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception ocurred: {ex.Message}");
                return StatusCode(500, "An internal error occurred while processing the request.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductCreateDto productDto)
        {
            try
            {
                Product existingProduct = await _service.GetProductById(id);
                if (existingProduct == null)
                {
                    return NotFound($"Product with Id {id} not found");
                }
                if (ModelState.IsValid)
                {
                    _mapper.Map(productDto, existingProduct);
                    await _service.UpdateProduct(existingProduct);
                    return Ok("Product updated successfuly.");
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception ocurred: {ex.Message}");
                return StatusCode(500, "An internal error occurred while processing the request.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Product product = await _service.GetProductById(id);
            if (product == null)
            {
                return NotFound($"Product with Id {id} not found");
            }
            await _service.DeleteProduct(product);
            return NoContent();
        }
    }
}
