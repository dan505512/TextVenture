using TextVenture.Core.Interfaces.Adventure;

namespace TextVenture.Core.Implementations.Adventure
{
    public class Adventure : IAdventure
    {
        public Adventure(int id, string name, string description, int startingLocation)
        {
            ID = id;
            Name = name;
            Description = description;
            StartingLocation = startingLocation;
        }

        public int ID { get; }
        public string Name { get; }
        public string Description { get; }
        public int StartingLocation { get; }
    }
}