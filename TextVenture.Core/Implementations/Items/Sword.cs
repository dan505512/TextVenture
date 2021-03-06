﻿using TextVenture.Core.Interfaces.Items;

namespace TextVenture.Core.Implementations.Items
{
    public class Sword : IItem
    {
        /// <summary>
        /// Creates a new shield
        /// </summary>
        /// /// <param name="id">The DB ID of the sword</param>
        /// <param name="effectLevel">The damage this sword will add</param>
        /// <param name="name">The name of this sword</param>
        public Sword(int id, string name, int effectLevel)
        {
            Name = name;
            EffectLevel = effectLevel;
            ID = id;
        }
        public string Name { get; }
        public int EffectLevel { get; }
        public int ID { get; }
        public string ItemType => "Sword";
        public string ItemEffect => "Attack";
    }
}
