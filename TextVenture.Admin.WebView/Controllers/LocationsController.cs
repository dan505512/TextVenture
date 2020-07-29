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
using TextVenture.Core.Implementations.Adventure;
using TextVenture.Core.Implementations.Characters;
using TextVenture.Core.Interfaces.Adventure;
using TextVenture.DAL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TextVenture.Admin.WebView.Controllers
{
    /// <summary>
    /// An admin panel controller to get and set location details
    /// </summary>
    [Route("api/[controller]")]
    public class LocationsController : Controller
    {
        private readonly ITextVentureDB _db; 
        public LocationsController()
        {
            _db = DbFactory.GetTextVentureDb();
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<ILocation> Get()
        {
            return _db.GetAllLocations();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ILocation Get(int id)
        {
            return _db.GetLocationById(id);
        }

        // POST api/<controller>
        [HttpPost]
        public HttpResponseMessage Post([FromBody]LocationRequest newLocation)
        {
            _db.InsertLocation(newLocation.Name, newLocation.Description, newLocation.North, newLocation.South, newLocation.East, newLocation.West, newLocation.Enemy, newLocation.Item);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        // PUT api/<controller>/5
        [HttpPut]
        public void Put(int id, [FromBody]LocationRequest editedLocation)
        {
            var location = new StandardLocation(id, editedLocation.Name, editedLocation.Description, editedLocation.North,
                editedLocation.South, editedLocation.East, editedLocation.West, editedLocation.Item,
                editedLocation.Enemy);
            _db.UpdateLocation(location);
        }
    }
}
