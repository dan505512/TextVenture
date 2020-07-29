using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextVenture.DAL
{
    /// <summary>
    /// A factory creating the DAL to be used by the system. Enables replacement of the implementation of <see cref="ITextVentureDB"/>
    /// </summary>
    public class DbFactory
    {
        private static readonly Dictionary<string, ITextVentureDB> Dbs = new Dictionary<string, ITextVentureDB>();
        public static ITextVentureDB GetTextVentureDb(string connectionName)
        {
            if (!Dbs.ContainsKey(connectionName))
            {
                var db = new PostgresTextVentureDB();
                db.Connect("Host=localhost;Username=postgres;Password=Password1;Database=textventure");
                Dbs[connectionName] = db;
            }
            
            return Dbs[connectionName];
        }
    }
}
