using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextVenture.Core.Interfaces.Adventure
{
    public interface IAdventure
    {
        /// <summary>
        /// The DB ID of the adventure
        /// </summary>
        int ID { get; }
        /// <summary>
        /// The name of the adventure
        /// </summary>
        string Name { get; }
        /// <summary>
        /// The description of the adventure
        /// </summary>
        string Description { get; }
        /// <summary>
        /// The ID of the location at which the adventure begins
        /// </summary>
        int StartingLocation { get; }
    }
}
