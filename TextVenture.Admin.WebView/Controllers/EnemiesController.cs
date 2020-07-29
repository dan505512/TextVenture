using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using TextVenture.Admin.WebView.Models;
using TextVenture.Core.Interfaces.Characters;
using TextVenture.Core;
using TextVenture.Core.Implementations.Characters;
using TextVenture.DAL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TextVenture.Admin.WebView.Controllers
{
    /// <summary>
    /// An admin panel controller to get and set enemy details
    /// </summary>
    [Route("api/[controller]")]
    public class EnemiesController : Controller
    {
        private readonly ITextVentureDB _db; 
        public EnemiesController()
        {
            _db = DbFactory.GetTextVentureDb();
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<IEnemy> Get()
        {
            return _db.GetAllEnemies();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IEnemy Get(int id)
        {
            return _db.GetEnemyById(id);
        }

        // POST api/<controller>
        [HttpPost]
        public HttpResponseMessage Post([FromBody]EnemyRequest newEnemy)
        {
            var success = _db.InsertEnemy(newEnemy.Name, newEnemy.Health, newEnemy.MinDamage, newEnemy.MaxDamage);
            return new HttpResponseMessage(success ? HttpStatusCode.OK : HttpStatusCode.InternalServerError);
        }

        // PUT api/<controller>/5
        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody]EnemyRequest editedEnemy)
        {
            var enemy = new StandardEnemy(id, editedEnemy.Name, editedEnemy.Health, editedEnemy.MinDamage, editedEnemy.MaxDamage);
            
            var success = _db.UpdateEnemy(enemy);
            return new HttpResponseMessage(success ? HttpStatusCode.OK : HttpStatusCode.InternalServerError);
        }
    }
}
