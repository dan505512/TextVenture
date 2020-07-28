using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextVenture.Core
{
    public interface IItem
    {
        /// <summary>
        /// The name of the item
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// The level of effectiveness the item has in its relevant field.
        /// </summary>
        int EffectLevel { get; }

        /// <summary>
        /// The DB ID of this item
        /// </summary>
        int ID { get; }
    }
}
