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
using Tour_of_Heroes.Classes;

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

        [Route("List")]
        //[EnableCors("AllPolicy")]
        public JsonResult List()
        {
            string json = JsonConvert.SerializeObject(_heroHandler.List());
            return new JsonResult(json);
        }

        [Route("Get")]
        //[EnableCors("AllPolicy")]
        public JsonResult Get(int heroId)
        {
            string json = JsonConvert.SerializeObject(_heroHandler.Get(heroId));
            return new JsonResult(json);
        }

        [Route("Put")]
        [HttpPut]
        //[EnableCors("AllPolicy")]
        public JsonResult Put(Hero hero)
        {
            int heroId = 0;

            if (hero.heroId == 0) heroId = _heroHandler.Insert(hero);
            else _heroHandler.Update(hero);

            return new JsonResult(JsonConvert.SerializeObject(heroId));
        }

        // GET: HeroesController/Delete/5
        [Route("Delete")]
        //[EnableCors("AllPolicy")]
        public JsonResult Delete(int heroId)
        {
            _heroHandler.Delete(heroId);
            return new JsonResult(string.Empty);
        }

        [HttpOptions]
        public JsonResult Options()
        {
            return new JsonResult(string.Empty);
        }
    }
}
