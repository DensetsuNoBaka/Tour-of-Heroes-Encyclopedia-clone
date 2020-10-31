using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Tour_of_Heroes.Entities;
using Newtonsoft.Json;
using Tour_of_Heroes.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Tour_of_Heroes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[EnableCors("AllPolicy")]
    public class HeroesController : Controller
    {
        private readonly IHandler<Hero> _heroHandler;
        public HeroesController(IHandler<Hero> heroHandler)
        {
            _heroHandler = heroHandler;
        }
        
        // GET: HeroesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: HeroesController/Details/5
        public ActionResult GetFull(int id)
        {
            return View();
        }

        [Route("Get")]
        //[EnableCors("AllPolicy")]
        public JsonResult Get(int? heroId)
        {
            string json = JsonConvert.SerializeObject(_heroHandler.Get(heroId));
            return new JsonResult(json);
        }

        [Route("Put")]
        [HttpPut]
        public JsonResult Put(Hero hero)
        {
            return new JsonResult(JsonConvert.SerializeObject(""));
        }

        // POST: HeroesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HeroesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HeroesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HeroesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HeroesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
