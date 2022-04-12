﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Services;

namespace Shopping.Aggregator.Models
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _client;

        public CatalogService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalog()
        {
            var response = await _client.GetAsync("/api/v1/catalog");
            return await response.ReadContentAs<List<CatalogModel>>();
        }

        public async Task<CatalogModel> GetCatalog(string id)
        {

            var response = await _client.GetAsync($"/api/v1/catalog/{id}");
            return await response.ReadContentAs<CatalogModel>();
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category)
        {
            var response = await _client.GetAsync($"/api/v1/catalog/GetProductByCategory/{category}");
            return await response.ReadContentAs<List<CatalogModel>>(); throw new System.NotImplementedException();
        }
    }
}
