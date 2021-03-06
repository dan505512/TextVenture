﻿using System;
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
using TextVenture.Core.Interfaces.Items;
using TextVenture.DAL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TextVenture.Admin.WebView.Controllers
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

        // POST api/<controller>
        [HttpPost]
        public HttpResponseMessage Add([FromBody]ItemRequest newItem)
        {
            var success = _db.InsertItem(newItem.Name, newItem.EffectLevel, newItem.TypeId);
            return new HttpResponseMessage(success ? HttpStatusCode.OK : HttpStatusCode.InternalServerError);
        }

        // PUT api/<controller>/5
        [HttpPut]
        public HttpResponseMessage Edit(int id, [FromBody]ItemRequest editedItem)
        {
            var success = _db.UpdateItem(id, editedItem.Name, editedItem.EffectLevel);
            return new HttpResponseMessage(success ? HttpStatusCode.OK : HttpStatusCode.InternalServerError);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _db.Dispose();
        }
    }
}
