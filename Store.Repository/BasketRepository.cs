﻿using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using Store.Core.Entities;
using Store.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.Repository
{
    public class BasketRepository : IBasketRepository
    {
        
        private readonly IDatabase database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            database = redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
            => await database.KeyDeleteAsync(basketId);

        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            var basket = await database.StringGetAsync(basketId);
            return basket.IsNull ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket customerBasket)
        {
            var createdOrUpdated = await database.StringSetAsync(customerBasket.Id, JsonSerializer.Serialize(customerBasket), TimeSpan.FromDays(1));
            if (!createdOrUpdated)
                return null;
            return await GetBasketAsync(customerBasket.Id);
        }
    }
}
