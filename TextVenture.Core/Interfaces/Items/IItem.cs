namespace TextVenture.Core.Interfaces.Items
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
