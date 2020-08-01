using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TextVenture.Core.Interfaces.Adventure;
using TextVenture.DAL;

namespace TextVenture.User.WebView.Controllers
{
    [Route("api/[controller]")]
    public class AdventuresController : Controller
    {
        private readonly ITextVentureDB _db;
        public AdventuresController()
        {
            _db = DbFactory.GetTextVentureDb();
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<IAdventure> Get()
        {
            return _db.GetAllAdventures();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IAdventure Get(int id)
        {
            return _db.GetAdventureById(id);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _db.Dispose();
        }
    }
}