namespace TextVenture.Core.Interfaces.Items
{
    interface IHealthItem : IItem
    {
        /// <summary>
        /// Adds health to the player
        /// </summary>
        /// <param name="currentHealth">Health before this item was used</param>
        /// <returns>Health after this item has been used</returns>
        int AddHealth(int currentHealth);
    }
}
