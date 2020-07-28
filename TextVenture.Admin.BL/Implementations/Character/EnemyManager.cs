using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextVenture.Admin.BL.Interfaces.Character;
using TextVenture.Core.Interfaces.Characters;
using TextVenture.DAL;

namespace TextVenture.Admin.BL
{
    public class EnemyManager : IEnemyManager
    {
        private ITextVentureDB _dbManager;
        public EnemyManager(ITextVentureDB db)
        {
            _dbManager = db;
        }
        public List<IEnemy> GetAllEnemies()
        {
            return _dbManager.GetAllEnemies();
        }

        public IEnemy GetEnemyById(int id)
        {
            return _dbManager.GetEnemyById(id);
        }

        public bool ChangeEnemyHealth(int id, int newHealth)
        {
            var enemy = GetEnemyById(id);
            enemy.Health = newHealth;
            return _dbManager.UpdateEnemy(enemy);
        }

        public bool ChangeEnemyMinDamage(int id, int newMinDamage)
        {
            var enemy = GetEnemyById(id);
            enemy.MinDamage = newMinDamage;
            return _dbManager.UpdateEnemy(enemy);
        }

        public bool ChangeEnemyMaxDamage(int id, int newMaxDamage)
        {
            var enemy = GetEnemyById(id);
            enemy.MaxDamage = newMaxDamage;
            return _dbManager.UpdateEnemy(enemy);
        }

        public bool ChangeEnemyName(int id, string newName)
        {
            var enemy = GetEnemyById(id);
            enemy.Name = newName;
            return _dbManager.UpdateEnemy(enemy);
        }

        public bool CreateNewEnemy(string name, int health, int minDamage, int maxDamage)
        {
            return _dbManager.InsertEnemy(name, health, minDamage, maxDamage);
        }
    }
}
