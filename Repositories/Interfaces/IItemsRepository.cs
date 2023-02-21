using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Play.Catalog.Service.Models.Entities;

namespace Play.Catalog.Service.Repositories.Interfaces
{
    public interface IItemsRepository
    {
        Task CreateAsync(Item item);
        Task<IReadOnlyCollection<Item>> GetAllAsync();
        Task<Item> GetAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(Item item);
    }
}