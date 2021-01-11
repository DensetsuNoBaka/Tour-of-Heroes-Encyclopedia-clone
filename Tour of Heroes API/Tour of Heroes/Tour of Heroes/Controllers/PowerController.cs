using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Tour_of_Heroes.Entities;
using Tour_of_Heroes.Interfaces;

namespace Tour_of_Heroes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[EnableCors("AllPolicy")]
    public class PowerController : Controller
    {
        private readonly IHandler<Power> _powerHandler;
        public PowerController(IHandler<Power> powerHandler)
        {
            _powerHandler = powerHandler;
        }
        [Route("List")]
        //[EnableCors("AllPolicy")]
        public async Task<JsonResult> List()
        {
            string json = JsonConvert.SerializeObject(await _powerHandler.List(null));
            return new JsonResult(json);
        }

        [Route("Get")]
        //[EnableCors("AllPolicy")]
        public async Task<JsonResult> Get(int universeId)
        {
            string json = JsonConvert.SerializeObject(await _powerHandler.Get(universeId));
            return new JsonResult(json);
        }
    }
}
