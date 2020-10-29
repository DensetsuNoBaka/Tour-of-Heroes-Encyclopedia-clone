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

namespace Tour_of_Heroes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[EnableCors("AllPolicy")]
    public class HeroesController : Controller
    {
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

            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyKey"].ConnectionString;

            List<Hero> heroes = new List<Hero>();

            string json = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    //string query = @"SELECT [Hero_ID], [Hero_Name], [Power_Level], [Picture_Url] FROM [Hero]";
                    //define the SqlCommand object
                    SqlCommand cmd = new SqlCommand("Hero_Get", conn);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Hero_ID", heroId);

                    //open connection
                    conn.Open();

                    //execute the SQLCommand
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            heroes.Add(new Hero{
                                heroId = dr.GetInt32(0),
                                heroName = dr.GetString(1),
                                powerLevel = dr.GetString(2),
                                pictureUrl = dr.GetString(3)
                            });
                        }

                        json = JsonConvert.SerializeObject(heroes);
                    }
                    else
                    {
                        Console.WriteLine("No data found.");
                    }

                    //close data reader
                    dr.Close();

                    //close connection
                    conn.Close();
                }
            } catch (Exception ex)
            {
                //display error message
                Console.WriteLine("Exception: " + ex.Message);
            }

            //Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return new JsonResult(json);
            //new string[] { "value1", "value2" }
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
