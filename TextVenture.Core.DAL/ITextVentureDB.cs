using System;
using System.Collections.Generic;
using TextVenture.Core;
using TextVenture.Core.Implementations.Adventure;
using TextVenture.Core.Interfaces.Adventure;
using TextVenture.Core.Interfaces.Characters;
using TextVenture.Core.Interfaces.Items;

namespace TextVenture.DAL
{
    public interface ITextVentureDB : IDisposable
    {
        /// <summary>
        /// Initializes a connection to the DB
        /// </summary>
        /// <param name="connectionString">The connection string to the relevant DB</param>
        void Connect(string connectionString);

        bool IsConnected { get; }

        string GetPasswordHashForUser(string user);

        /// <summary>
        /// Gets a list of all the items in the DB
        /// </summary>
        /// <returns>All the items in the DB</returns>
        List<IItem> GetAllItems();

        /// <summary>
        /// Gets an <see cref="IItem"/> from the db by its ID
        /// </summary>
        /// <param name="id">The DB ID of the wanted item</param>
        /// <returns>The item from the DB</returns>
        IItem GetItemById(int id);

        /// <summary>
        /// Gets all the item types from the DB
        /// </summary>
        /// <returns>All the item types in the DB</returns>
        List<IItemsType> GetAllItemTypes();

        /// <summary>
        /// Inserts a new item into the DB
        /// </summary>
        /// <param name="name">The name of the item to insert</param>
        /// <param name="effectLevel">The effect level of the item</param>
        /// <param name="type">THe ID of the item type from the DB</param>
        /// <returns>True if success. False otherwise</returns>
        bool InsertItem(string name, int effectLevel, int type);

        /// <summary>
        /// Updates a given item.
        /// </summary>
        /// <param name="id">The DB ID of the edited item</param>
        /// <param name="name">The name of the edited item</param>
        /// <param name="effectLevel">The effect level of the edited item</param>
        /// <returns>True if success. False otherwise</returns>
        bool UpdateItem(int id, string name, int effectLevel);

        /// <summary>
        /// Gets a list of all the enemies in the DB
        /// </summary>
        /// <returns>All the enemies in the DB</returns>
        List<IEnemy> GetAllEnemies();
        
        /// <summary>
        /// Gets an <see cref="IEnemy"/> from the db by its ID
        /// </summary>
        /// <param name="id">The DB ID of the wanted enemy</param>
        /// <returns>The enemy from the DB</returns>

        IEnemy GetEnemyById(int id);

        /// <summary>
        /// Updates an enemy using the ID and new props in the given <see cref="IEnemy"/> object
        /// </summary>
        /// <param name="enemy">The updated enemy</param>
        /// <returns>True if success. Otherwise false</returns>
        bool UpdateEnemy(IEnemy enemy);

        /// <summary>
        /// Inserts a new enemy into the DB.
        /// </summary>
        /// <param name="name">The new enemy's name</param>
        /// <param name="health">The new enemy's health</param>
        /// <param name="minDamage">The new enemy's min damage</param>
        /// <param name="maxDamage">The new enemy's max damage</param>
        /// <returns>True if success. Otherwise false</returns>
        bool InsertEnemy(string name, int health, int minDamage, int maxDamage);

        /// <summary>
        /// Gets a list of all the locations in the DB
        /// </summary>
        /// <returns>All the locations in the DB</returns>
        List<ILocation> GetAllLocations();

        /// <summary>
        /// Gets an <see cref="ILocation"/> from the db by its ID
        /// </summary>
        /// <param name="id">The DB ID of the wanted location</param>
        /// <returns>The location from the DB</returns>

        ILocation GetLocationById(int id);

        /// <summary>
        /// Inserts a new location into the DB
        /// </summary>
        /// <param name="name">The name of the location</param>
        /// <param name="description">A description of the location</param>
        /// <param name="north">The ID of the location to the north of this location</param>
        /// <param name="south">The ID of the location to the south of this location</param>
        /// <param name="east">The ID of the location to the east of this location</param>
        /// <param name="west">The ID of the location to the west of this location</param>
        /// <param name="enemy">An enemy at this location</param>
        /// <param name="item">An item at this location</param>
        /// <returns>True if success. False otherwise</returns>
        bool InsertLocation(string name, string description, int? north, int? south, int? east, int? west, int? enemy, int? item);

        /// <summary>
        /// Updates a location in the DB
        /// </summary>
        /// <param name="location">The updated location</param>
        /// <returns>True if success. False otherwise</returns>
        bool UpdateLocation(ILocation location);

        /// <summary>
        /// Gets a list of all the adventures in the DB
        /// </summary>
        /// <returns>All the adventures in the DB</returns>
        List<IAdventure> GetAllAdventures();

        /// <summary>
        /// Gets an <see cref="IAdventure"/> from the db by its ID
        /// </summary>
        /// <param name="id">The DB ID of the wanted adventure</param>
        /// <returns>The adventure from the DB</returns>

        IAdventure GetAdventureById(int id);

        /// <summary>
        /// Inserts a new adventure into the Db
        /// </summary>
        /// <param name="name">The name of the new adventure</param>
        /// <param name="description">The description of the new adventure</param>
        /// <param name="startingLocation">The ID of the starting location of this adventure</param>
        /// <returns>True if success. False otherwise</returns>
        bool InsertAdventure(string name, string description, in int startingLocation);

        /// <summary>
        /// Updates an adventure in the DB
        /// </summary>
        /// <param name="adventure">The updated adventure</param>
        /// <returns>True is success. False otherwise</returns>
        bool UpdateAdventure(IAdventure adventure);
    }
}
