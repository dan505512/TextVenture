using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextVenture.Admin.BL.Interfaces.Character;
using TextVenture.DAL;

namespace TextVenture.Admin.BL
{
    public static class ManagerFactory
    {
        private static readonly ITextVentureDB DbManager = DbFactory.GetTextVentureDb();

        public static IEnemyManager GetEnemyManager()
        {
            return new EnemyManager(DbManager);
        }
    }
}
