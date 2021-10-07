using LuckyDucky.Lottery.Interfaces;
using LuckyDucky.MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Diagnostics;

namespace LuckyDucky.MVC.Controllers
{
    public class LotteryController : Controller
    {
        ILotteryService _lotteryService;

        string _keyCookie = ".AlwaysTakePart";

        CookieOptions _coookieOptions = new CookieOptions() { Expires = DateTime.Now.AddDays(30), Secure = true, HttpOnly = true };

        ProjectSettingsModel _settingsModel { get; set; }

        public LotteryController(ILotteryService lotteryService, ProjectSettingsModel model)
        {
            _lotteryService = lotteryService;
            _settingsModel = model;
        }

        [HttpGet]
        public IActionResult Index()
        {
            TakePartModel model = new TakePartModel();

            if (HttpContext.Request.Cookies.ContainsKey(_keyCookie))
            {

                var cookie = HttpContext.Request.Cookies[_keyCookie];
                model = JsonConvert.DeserializeObject<TakePartModel>(cookie);
                HttpContext.Response.Cookies.Append(_keyCookie, cookie, _coookieOptions);

            }

            return View(new LotteryPageModel() { takePartModel = model, projectSettingsModel = DateTime.Today.CompareTo(_settingsModel.NextLotteryUtcDateTime) <= 0 ? _settingsModel : null });
        }

        [HttpPost]
        public IActionResult Index([Bind("Name", "Email", "IsAlwaysTakePartCheck")] TakePartModel model)
        {
            if (HttpContext.Request.Cookies.ContainsKey(_keyCookie))
            {
                //click "Do Not Take Part"

                model = JsonConvert.DeserializeObject<TakePartModel>(HttpContext.Request.Cookies[_keyCookie]);

                if (model.IsAlwaysTakePartCheck)
                {
                    HttpContext.Response.Cookies.Delete(_keyCookie);
                }

                //return LocalRedirect("/Lottery/");
            }
            else
            {
                //click "Take Part"

                if (ModelState.IsValid)
                {
                    _lotteryService.SubscribeUser(model.Name, model.Email);

                    if (model.IsAlwaysTakePartCheck)
                    {
                        HttpContext.Response.Cookies.Append(_keyCookie, JsonConvert.SerializeObject(model), _coookieOptions);
                        return View(new LotteryPageModel() { takePartModel = model, projectSettingsModel = DateTime.Today.CompareTo(_settingsModel.NextLotteryUtcDateTime) <= 0 ? _settingsModel : null });
                    }
                }
                else
                {
                    model.IsAlwaysTakePartCheck = false;
                }

            }
            return LocalRedirect("/Lottery/");
        }

       
    }
}