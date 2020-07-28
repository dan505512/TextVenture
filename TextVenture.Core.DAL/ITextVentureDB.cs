using System;
using System.Collections.Generic;
using TextVenture.Core;
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
        /// Gets a list of all the adventures in the DB
        /// </summary>
        /// <returns>All the v in the DB</returns>
        List<IAdventure> GetAllAdventures();

        /// <summary>
        /// Gets an <see cref="IAdventure"/> from the db by its ID
        /// </summary>
        /// <param name="id">The DB ID of the wanted adventure</param>
        /// <returns>The adventure from the DB</returns>

        IAdventure GetAdventureById(int id);
    }
}
