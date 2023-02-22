using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Extensions;
using Play.Catalog.Service.Models.Dtos;
using Play.Catalog.Service.Models.Entities;
using Play.Common.Repositories.Interfaces;

namespace Play.Catalog.Service.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IRepository<Item> itemsRepository;

        public ItemsController(IRepository<Item> itemsRepository)
        {
            this.itemsRepository = itemsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetAllAsync()
        {
            var items = (await itemsRepository.GetAllAsync()).Select(item => item.AsDto());
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetByIdAsync(Guid id)
        {
            var item = await itemsRepository.GetAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> PostAsync(CreateItemDto createItemDto)
        {
            var item = new Item
            {
                Name = createItemDto.Name,
                Description = createItemDto.Description,
                Price = createItemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await itemsRepository.CreateAsync(item);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateItemDto updateItemDto)
        {
            var existingItem = await itemsRepository.GetAsync(id);

            if (existingItem == null)
            {
                var item = new Item
                {
                    Name = updateItemDto.Name,
                    Description = updateItemDto.Description,
                    Price = updateItemDto.Price,
                    CreatedDate = DateTimeOffset.UtcNow
                };

                await itemsRepository.CreateAsync(item);

                return CreatedAtAction(nameof(GetByIdAsync), new { id = item.Id }, item);
            }

            existingItem.Name = updateItemDto.Name;
            existingItem.Description = updateItemDto.Description;
            existingItem.Price = updateItemDto.Price;

            await itemsRepository.UpdateAsync(existingItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var item = await itemsRepository.GetAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            await itemsRepository.RemoveAsync(item.Id);

            return NoContent();
        }
    }
}