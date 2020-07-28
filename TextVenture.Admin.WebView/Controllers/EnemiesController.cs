using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TextVenture.Core.Interfaces.Characters;
using TextVenture.Core;
using TextVenture.DAL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TextVenture.Admin.WebView.Controllers
{
    [Route("api/[controller]")]
    public class EnemiesController : Controller
    {
        private ITextVentureDB _db; 
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
        public void Post([FromBody]string name, [FromBody]int health, [FromBody]int minDamage, [FromBody]int maxDamage)
        {
            _db.InsertEnemy(name, health, minDamage, maxDamage);
        }

        // PUT api/<controller>/5
        [HttpPut]
        public void Put(int id, [FromBody]IEnemy enemy)
        {
            throw new NotImplementedException();
        }
    }
}
