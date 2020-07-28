using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextVenture.Core.Interfaces.Adventure
{
    public interface ILocation
    {
        /// <summary>
        /// The DB ID of the location
        /// </summary>
        int Id { get; }
        /// <summary>
        /// The name of the location
        /// </summary>
        string Name { get; }
        /// <summary>
        /// The description of this location to be displayed to the user
        /// </summary>
        string Description { get; }
        /// <summary>
        /// The ID of the location to the north of this location. If null there is no such location
        /// </summary>
        int? North { get; }
        /// <summary>
        /// The ID of the location to the south of this location. If null there is no such location
        /// </summary>
        int? South { get; }
        /// <summary>
        /// The ID of the location to the east of this location. If null there is no such location
        /// </summary>
        int? East { get; }
        /// <summary>
        /// The ID of the location to the west of this location. If null there is no such location
        /// </summary>
        int? West { get; }
        /// <summary>
        /// The ID of the item in this location.
        /// </summary>
        int? Item { get; }
        /// <summary>
        /// The ID of the enemy in this location. If null there is no such enemy
        /// </summary>
        int? Enemy { get; }
        /// <summary>
        /// Makes the item null. This will be used in location caching so a user can't come back and take the item again
        /// </summary>
        void MarkItemAsTaken();
        /// <summary>
        /// Makes the enemy null. This will be used in location caching so if the user comes back they won't have to fight again
        /// </summary>
        void MarkEnemyAsKilled();
    }
}
