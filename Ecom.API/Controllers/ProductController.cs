using AutoMapper;
using Ecom.API.Helper;
using Ecom.Core.DTO;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    public class ProductController : BaseController
    {
        public ProductController(IUnitofWork work, IMapper mapper) : base(work, mapper)
        {
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var products = await work.ProductRepository
                    .GetAllAsync(x=> x.Category, x => x.Photos);
                var result = mapper.Map<List<ProductDTO>>(products);
                if (products == null)
                {
                    return BadRequest(new ResponseAPI(400));
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var product = await work.ProductRepository
                    .GetByIdAsync(id, x => x.Category, x => x.Photos);
                var result = mapper.Map<ProductDTO>(product);
                if (product == null)
                {
                    return BadRequest(new ResponseAPI(400, "This Product Not Found"));
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add(AddProductDTO productDTO)
        {
            try
            {

                await work.ProductRepository.AddAsync(productDTO);

                return Ok(new ResponseAPI(200, "Product added succssfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI (400,ex.Message));
            }
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateProductDTO productDTO)
        {
            try
            {
                await work.ProductRepository.UpdateAsync(productDTO);
                return Ok(new ResponseAPI(200, "Product updated succssfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var product = await work.ProductRepository
                    .GetByIdAsync(id, x => x.Category, x => x.Photos);
                await work.ProductRepository.DeleteAsync(product);
                return Ok(new ResponseAPI(200, "Product deleted succssfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }
    }
}
