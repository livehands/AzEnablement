using AzSelfService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzSelfService.Services
{
    public interface ICosmosDbService
    {
        Task<IEnumerable<SubRequestViewModel>> GetItemsAsync(string query);
        Task<SubRequestViewModel> GetItemAsync(string id);
        Task AddItemAsync(SubRequestViewModel item);
        Task UpdateItemAsync(string id, SubRequestViewModel item);
        Task DeleteItemAsync(string id);
    }
}
