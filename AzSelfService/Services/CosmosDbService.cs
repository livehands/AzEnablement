using AzSelfService.Models;
using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzSelfService.Services
{
    public class CosmosDbService : ICosmosDbService
    {
        private Container _container;

        public CosmosDbService(CosmosClient dbClient, string databaseName, string containerName)
        {
            _container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddItemAsync(SubRequestViewModel item)
        {
            await _container.CreateItemAsync(item, new PartitionKey(item.Id));
        }

        public async Task DeleteItemAsync(string id)
        {
            await _container.DeleteItemAsync<SubRequestViewModel>(id, new PartitionKey(id));
        }

        public async Task<IEnumerable<SubRequestViewModel>> GetItemsAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<SubRequestViewModel>(new QueryDefinition(queryString));
            List<SubRequestViewModel> results = new List<SubRequestViewModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task<SubRequestViewModel> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<SubRequestViewModel> response = await _container.ReadItemAsync<SubRequestViewModel>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        public async Task UpdateItemAsync(string id, SubRequestViewModel item)
        {
            await _container.UpsertItemAsync(item, new PartitionKey(id));
        }

    }
}
