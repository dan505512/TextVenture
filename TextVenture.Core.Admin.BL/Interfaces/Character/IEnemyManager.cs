using System.Collections.Generic;
using TextVenture.Core.Interfaces.Characters;

namespace TextVenture.Admin.BL.Interfaces.Character
{
    public interface IEnemyManager
    {
        /// <summary>
        /// Gets a list of all the enemies
        /// </summary>
        /// <returns>A list of all the enemies</returns>
        List<IEnemy> GetAllEnemies();

        /// <summary>
        /// Gets a specific enemy by its ID
        /// </summary>
        /// <param name="id">The enemies ID</param>
        /// <returns>The specific enemy</returns>
        IEnemy GetEnemyById(int id);
        /// <summary>
        /// Changes an enemy's health
        /// </summary>
        /// <param name="id">The ID of the enemy to change</param>
        /// <param name="newHealth">The new value of the health</param>
        /// <returns>True if success. False otherwise</returns>
        bool ChangeEnemyHealth(int id, int newHealth);

        /// <summary>
        /// Changes an enemy's min damage
        /// </summary>
        /// <param name="id">The ID of the enemy to change</param>
        /// <param name="newMinDamage">The new value of the min damage</param>
        /// <returns>True if success. False otherwise</returns>
        bool ChangeEnemyMinDamage(int id, int newMinDamage);

        /// <summary>
        /// Changes an enemy's max damage
        /// </summary>
        /// <param name="id">The ID of the enemy to change</param>
        /// <param name="newMaxDamage">The new value of the max damage</param>
        /// <returns>True if success. False otherwise</returns>
        bool ChangeEnemyMaxDamage(int id, int newMaxDamage);

        /// <summary>
        /// Changes an enemy's name
        /// </summary>
        /// <param name="id">The ID of the enemy to change</param>
        /// <param name="newName">The new value of the name</param>
        /// <returns>True if success. False otherwise</returns>
        bool ChangeEnemyName(int id, string newName);
        
        /// <summary>
        /// Creates a new enemy
        /// </summary>
        /// <param name="name">The enemy's name</param>
        /// <param name="health">The enemy's health</param>
        /// <param name="minDamage">The enemy's min damage</param>
        /// <param name="maxDamage">The enemy's max damage</param>
        /// <returns>True if success. False otherwise</returns>
        bool CreateNewEnemy(string name, int health, int minDamage, int maxDamage);
    }
}