using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using TextVenture.Admin.WebView.Models;
using TextVenture.Core.Implementations.Adventure;
using TextVenture.Core.Interfaces.Adventure;
using TextVenture.DAL;

namespace TextVenture.Admin.WebView.Controllers
{
    /// <summary>
    /// An admin panel controller to get and set adventure details
    /// </summary>
    [Route("api/[controller]")]
    public class AdventuresController : Controller
    {
        private readonly ITextVentureDB _db;
        public AdventuresController()
        {
            _db = DbFactory.GetTextVentureDb("adventures");
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

        // POST api/<controller>
        [HttpPost]
        public HttpResponseMessage Post([FromBody]AdventureRequest newAdventure)
        {
            var success =_db.InsertAdventure(newAdventure.Name, newAdventure.Description, newAdventure.StartingLocation);
            return new HttpResponseMessage(success ? HttpStatusCode.OK : HttpStatusCode.InternalServerError);
        }

        // PUT api/<controller>/5
        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody]AdventureRequest editedAdventure)
        {
            var adventure = new Adventure(id, editedAdventure.Name, editedAdventure.Description, editedAdventure.StartingLocation);
            var success = _db.UpdateAdventure(adventure);
            return new HttpResponseMessage(success ? HttpStatusCode.OK : HttpStatusCode.InternalServerError);
        }
    }
}