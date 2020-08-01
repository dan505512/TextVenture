using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TextVenture.Core.Interfaces.Characters;
using TextVenture.DAL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TextVenture.User.WebView.Controllers
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

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _db.Dispose();
        }
    }
}
