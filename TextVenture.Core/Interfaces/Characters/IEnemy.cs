using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextVenture.Core.Interfaces.Characters
{
    public interface IEnemy
    {
        /// <summary>
        /// The DB ID of the enemy
        /// </summary>
        int Id { get; }
        /// <summary>
        /// The name of the enemy
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// The health left for the enemy
        /// </summary>
        int Health { get; set; }
        /// <summary>
        /// Min amount of damage the enemy is capable of doing
        /// </summary>
        int MinDamage { get; set; }
        /// <summary>
        /// Max amount of damage the enemy is capable of doing
        /// </summary>
        int MaxDamage { get; set; }
        /// <summary>
        /// Performs an attack on the player
        /// </summary>
        /// <param name="currentPlayerHealth">The players health before the attack</param>
        /// <returns>The players health left after the attack</returns>
        int AttackPlayer(int currentPlayerHealth);
        /// <summary>
        /// Performs an attack from the player on the enemy
        /// </summary>
        /// <param name="damage">The damage to be inflicted on the enemy</param>
        void AttackEnemy(int damage);
    }
}
