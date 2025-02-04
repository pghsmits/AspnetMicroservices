﻿using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services;

namespace Shopping.Aggregator.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]

    public class ShoppingController : ControllerBase
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;

        public ShoppingController(ICatalogService catalogService, IBasketService basketService, IOrderService orderService)
        {
            _catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
            _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService)); 
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }
    
        [HttpGet("{userName}", Name = "GetShopping")]
        [ProducesResponseType(typeof(ShoppingModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingModel>> GetShopping(string userName)
        {
            // get basket with username
            // iterate baket items ansd consume products with basket item productId member
            // map product related members into basketitem dto with extended colums
            // consume ordering microservices in order to retrieve order list
            // return root ShoppingModel dto class which includes all reponses

            var basket = await _basketService.GetBasket(userName);

            foreach (var Item in basket.Items)
            {
                var product = await _catalogService.GetCatalog(Item.ProductId);

                // set additional product fileds into baket item
                Item.ProductName = product.Name;
                Item.Category = product.Category;   
                Item.Description = product.Description; 
                Item.ImageFile = product.ImageFile;    
                Item.Summary = product.Summary;
            }

            var orders = await _orderService.GetOrdersByUserName(userName);
            var shoppingModel = new ShoppingModel
            {
                UserName = userName,
                BasketWithProducts = basket,
                Orders = orders,
            };
            return Ok(shoppingModel);


        }
    }
}
