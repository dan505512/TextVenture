using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextVenture.Core.Interfaces.Characters;

namespace TextVenture.Core.Implementations.Characters
{
    public class StandardEnemy : IEnemy
    {
        public StandardEnemy(int id, string name, int health, int minDamage, int maxDamage)
        {
            Id = id;
            Name = name;
            Health = health;
            MinDamage = minDamage;
            MaxDamage = maxDamage;
        }

        public int Id { get; }
        public string Name { get; set; }
        public int Health { get; set; }
        public int MinDamage { get; set; }
        public int MaxDamage { get; set;  }
        public int AttackPlayer(int currentPlayerHealth)
        {
            var random = new Random();
            return currentPlayerHealth - random.Next(MinDamage, MaxDamage);
        }

        public void AttackEnemy(int damage)
        {
            Health -= damage;
        }
    }
}
