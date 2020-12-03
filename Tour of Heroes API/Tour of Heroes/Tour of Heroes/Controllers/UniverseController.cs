﻿using System;
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
        public JsonResult List(int? universeId)
        {
            string json = JsonConvert.SerializeObject(_universeHandler.List(universeId));
            return new JsonResult(json);
        }
    }
}