using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextVenture.Core.Interfaces.Items;

namespace TextVenture.Core.Implementations.Items
{
    public class Potion : IItem
    {
        /// <summary>
        /// Creates a new potion
        /// </summary>
        /// <param name="id">The DB ID of the potion</param>
        /// <param name="name">The name of the potion</param>
        /// <param name="effectLevel">The amount of health restored by the potion</param>
        public Potion(int id, string name, int effectLevel)
        {
            Name = name;
            EffectLevel = effectLevel;
            ID = id;
        }

        public string Name { get; }
        public int EffectLevel { get; }
        public int ID { get; }
        public string ItemType => "Potion";
        public string ItemEffect => "Health";
    }
}
