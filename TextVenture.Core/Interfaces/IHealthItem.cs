using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextVenture.Core.Interfaces
{
    interface IHealthItem : IItem
    {
        /// <summary>
        /// Adds health to the player
        /// </summary>
        /// <param name="currentHealth">Health before this item was used</param>
        /// <returns>Health after this item has been used</returns>
        int AddHealth(int currentHealth);
    }
}
