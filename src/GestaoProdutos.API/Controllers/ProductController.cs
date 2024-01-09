using AutoMapper;
using Canducci.Pagination;
using FluentValidation;
using GestaoProdutos.Application.Produto;
using GestaoProdutos.Domain.Entities;
using GestaoProdutos.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace GestaoProdutos.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IValidator<Product> _productValidator;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IValidator<Product> productValidator, IMapper mapper)
        {
            _productService = productService;
            _productValidator = productValidator;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet]
        public IActionResult GetPagedProducts([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string filter = "")
        {
            var products = _productService.GetPagedProducts(filter)
                .ToPaginatedRest(page, pageSize);

            return Ok(products);
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody] ProductDto productDto)
        {
            var validationResult = _productValidator.Validate(_mapper.Map<Product>(productDto));

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
            }

            _productService.AddProduct(productDto);

            return CreatedAtAction(nameof(GetProductById), new { id = productDto.Id }, productDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] ProductDto productDto)
        {
            if (id != productDto.Id)
            {
                return BadRequest("ID do produto informado é inválido.");
            }

            var validationResult = _productValidator.Validate(_mapper.Map<Product>(productDto));

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
            }

            _productService.UpdateProduct(productDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                _productService.SoftDeleteProduct(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
