using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoWebApi.Models;
using TodoWebApi.Repository;

namespace TodoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ITodoRepository _repository;

        public ItemController(ITodoRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Item
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _repository.GetAllAsync();
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to get tasks!");
            }
        }

        // GET: api/Item/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _repository.GetByIdAsync(id);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to get task!");
            }
        }

        // POST: api/Item
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TodoItemModel model)
        {
            try
            {
                var result = await _repository.CreateAsync(model);
                model.Id = result;
                return Ok(model);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to create task!");
            }
        }

        // PUT: api/Item/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TodoItemModel model)
        {
            try
            {
                var result = await _repository.EditAsync(id, model);
                return Ok(model);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update task!");
            }
        }

        // DELETE: api/Item/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _repository.DeleteAsync(id);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete task!");
            }
        }

        // DELETE: api/Item/5
        [HttpPut("{id}/{status}")]
        public async Task<IActionResult> Complete(int id, bool status)
        {
            try
            {
                var result = await _repository.CompleteAsync(id, status);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to complete task!");
            }
        }
    }
}
