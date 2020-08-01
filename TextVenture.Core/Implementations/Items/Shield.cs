using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextVenture.Core.Interfaces.Items;

namespace TextVenture.Core.Implementations.Items
{
    public class Shield : IItem
    {
        /// <summary>
        /// Creates a new shield
        /// </summary>
        /// /// <param name="id">The DB ID of the shield</param>
        /// <param name="effectLevel">The damage this shield will absorb</param>
        /// <param name="name">The name of this shield</param>
        public Shield(int id, string name, int effectLevel)
        {
            EffectLevel = effectLevel;
            Name = name;
            ID = id;
        }

        public string Name { get; }
        public int EffectLevel { get; }
        public int ID { get; }
        public string ItemType => "Shield";
        public string ItemEffect => "Defense";
    }
}
