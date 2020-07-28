namespace TextVenture.Core.Interfaces.Items
{
    interface IDefenseItem : IItem
    {
        /// <summary>
        /// Calculates the damage to be done after this items effect has been used.
        /// </summary>
        /// <param name="currentDamage">The damage before this item has been used</param>
        /// <returns>The damage after this item has been used</returns>
        int ReduceDamage(int currentDamage);
    }
}
