using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextVenture.Core.Interfaces
{
    public interface IAttackItem : IItem
    {
        /// <summary>
        /// Adds damage to the attack by this items level
        /// </summary>
        /// <param name="currentDamage">The damage before this item has been used</param>
        /// <returns>The damage after this item has been used</returns>
        int AddDamage(int currentDamage);
    }
}
