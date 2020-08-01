using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TextVenture.Core.Interfaces.Items;
using TextVenture.DAL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TextVenture.User.WebView.Controllers
{
    /// <summary>
    /// An admin panel controller to get and set item details
    /// </summary>
    [Route("api/[controller]/[action]")]
    public class ItemsController : Controller
    {
        private readonly ITextVentureDB _db; 
        public ItemsController()
        {
            _db = DbFactory.GetTextVentureDb();
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<IItem> Get()
        {
            return _db.GetAllItems();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IItem Get(int id)
        {
            return _db.GetItemById(id);
        }

        [HttpGet]
        public List<IItemsType> ItemTypes()
        {
            return _db.GetAllItemTypes();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _db.Dispose();
        }
    }
}
