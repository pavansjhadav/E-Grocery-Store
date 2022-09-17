using E_Grocery_Store.Common.CustomException;
using E_Grocery_Store.Models;
using E_Grocery_Store.Repository.GroceryManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Grocery_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroceryController : ControllerBase
    {
        private readonly IGroceryRepo groceryRepo;
        private readonly ILogger<GroceryController> logger;

        public GroceryController(IGroceryRepo groceryRepo, ILogger<GroceryController> logger)
        {
            this.groceryRepo = groceryRepo;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<Response>> AddGrocery(Grocery newGrocery)
        {
            Response response;
            try
            {
                await groceryRepo.AddGrocery(newGrocery);
                response = new Response()
                {
                    IsSuccess = true,
                    Message = "Grocery Added successfully"
                };
                return Ok(response);
            }
            catch (RequestException rex)
            {
                response = new Response()
                {
                    IsSuccess = false,
                    Message = rex.Message
                };
                logger.LogError(rex.Message);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    IsSuccess = false,
                    Message = "Something went wrong"
                };
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPut]
        public async Task<ActionResult<Response>> UpdateGrocery(Grocery grocery)
        {
            Response response;
            try
            {
                await groceryRepo.UpdateGrocery(grocery);
                response = new Response()
                {
                    IsSuccess = true,
                    Message = "Grocery updated successfully"
                };
                return Ok(response);
            }
            catch (RequestException rex)
            {
                response = new Response()
                {
                    IsSuccess = false,
                    Message = rex.Message
                };
                logger.LogError(rex.Message);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    IsSuccess = false,
                    Message = "Something went wrong"
                };
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }


        [HttpGet]
        public async Task<ActionResult<Grocery>> GetGroceries()
        {
            try
            {
                var groceries = await groceryRepo.GetGroceries();
                return Ok(groceries);
            }
            catch (ResponseException ex)
            {
                logger.LogError(ex.Message);
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
            }
        }
        [HttpGet("vendorgroceries/{id:int}")]
        public async Task<ActionResult<Grocery>> GetVendorGroceries(int id)
        {
            try
            {
                var groceries = await groceryRepo.GetVendorGroceries(id);
                return Ok(groceries);
            }
            catch (ResponseException ex)
            {
                logger.LogError(ex.Message);
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Grocery>> GetGrocery(int id)
        {
            try
            {
                var groceries = await groceryRepo.GetGrocery(id);
                return Ok(groceries);
            }
            catch (ResponseException ex)
            {
                logger.LogError(ex.Message);
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
            }
        }
        [HttpGet("categories")]
        public async Task<ActionResult<GroceryCategory>> GetGroceryCategories()
        {
            try
            {
                var groceryCategories = await groceryRepo.GetGroceryCategories();
                return Ok(groceryCategories);
            }
            catch (ResponseException ex)
            {
                logger.LogError(ex.Message);
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Response>> DeleteGrocery(int id)
        {
            Response response;
            try
            {
                await groceryRepo.DeleteGrocery(id);
                response = new Response()
                {
                    IsSuccess = true,
                    Message = "Grocery deleted successfully"
                };
                return Ok(response);
            }
            catch (RequestException rex)
            {
                response = new Response()
                {
                    IsSuccess = false,
                    Message = rex.Message
                };
                logger.LogError(rex.Message);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    IsSuccess = false,
                    Message = "Something went wrong"
                };
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
