using LuckyDucky.MVC.Models.API;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace LuckyDucky.MVC.Controllers.API
{
    [ApiController]
    public class FeaturedGoods : ControllerBase
    {
        private List<ShopAPIModel> featured = new List<ShopAPIModel> {
            new ShopAPIModel {id = Guid.NewGuid(), name = "DefaultName1"},
            new ShopAPIModel {id = Guid.NewGuid(),name = "DefaultName2"},
        };

        [HttpGet("/Goods/Featured")]
        [HttpGet("api/[controller]")]
        public IActionResult Index()
        {
            return Ok(new { featured });
        }
    }
}