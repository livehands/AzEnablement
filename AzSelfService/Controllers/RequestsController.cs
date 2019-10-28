using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AzSelfService.Services;
using AzSelfService.Models;

namespace AzSelfService.Controllers
{
    public class RequestsController : Controller
    {
        private readonly ICosmosDbService _cosmosDbService;
        public RequestsController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [ActionName("Index")]
        public async Task<IActionResult> Index()
        {
            return View(await _cosmosDbService.GetItemsAsync("Select * from c"));
        }

        [ActionName("Create")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([Bind("Id,SubscriptionName,SubscriptionType,CostId,DescriptionId")] SubRequestViewModel item)
        {
            if (ModelState.IsValid)
            {
                item.Id = Guid.NewGuid().ToString();
                await _cosmosDbService.AddItemAsync(item);
                return RedirectToAction("Index");
            }

            return View(item);
        }
           
    }
}