using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Tour_of_Heroes.Entities;
using Tour_of_Heroes.Interfaces;

namespace Tour_of_Heroes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[EnableCors("AllPolicy")]
    public class UniverseController : Controller
    {
        private readonly IHandler<Universe> _universeHandler;
        public UniverseController(IHandler<Universe> universeHandler)
        {
            _universeHandler = universeHandler;
        }

        [Route("List")]
        //[EnableCors("AllPolicy")]
        public async Task<JsonResult> List()
        {
            string json = JsonConvert.SerializeObject(await _universeHandler.List(null));
            return new JsonResult(json);
        }

        [Route("Get")]
        //[EnableCors("AllPolicy")]
        public async Task<JsonResult> Get(int universeId)
        {
            string json = JsonConvert.SerializeObject(await _universeHandler.Get(universeId));
            return new JsonResult(json);
        }

        [Route("Put")]
        [HttpPut]
        //[EnableCors("AllPolicy")]
        public async Task<JsonResult> Put(Universe universe)
        {
            int universeId = 0;

            if (universe.universeId == 0) universeId = await _universeHandler.Insert(universe);
            else 
            {
                universeId = universe.universeId;
                await _universeHandler.Update(universe);
            }

            return new JsonResult(JsonConvert.SerializeObject(universeId));
        }
    }
}
