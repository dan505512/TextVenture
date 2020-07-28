using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextVenture.Core.Interfaces.Adventure;

namespace TextVenture.Core.Implementations.Adventure
{
    public class StandardLocation : ILocation
    {
        public StandardLocation(int id, string name, string description, int? north, int? south, int? east, int? west, int? item, int? enemy)
        {
            Id = id;
            Name = name;
            Description = description;
            North = north;
            South = south;
            East = east;
            West = west;
            Item = item;
            Enemy = enemy;
        }

        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public int? North { get; }
        public int? South { get; }
        public int? East { get; }
        public int? West { get; }
        public int? Item { get; private set; }
        public int? Enemy { get; private set; }

        public void MarkItemAsTaken()
        {
            Item = null;
        }

        public void MarkEnemyAsKilled()
        {
            Enemy = null;
        }
    }
}
