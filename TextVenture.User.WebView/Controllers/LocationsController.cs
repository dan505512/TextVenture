using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using TextVenture.Core.Implementations.Adventure;
using TextVenture.Core.Interfaces.Adventure;
using TextVenture.DAL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TextVenture.User.WebView.Controllers
{
    /// <summary>
    /// An admin panel controller to get location details
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

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _db.Dispose();
        }
    }
}
