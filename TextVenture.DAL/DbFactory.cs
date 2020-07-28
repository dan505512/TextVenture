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
        public static ITextVentureDB GetTextVentureDb()
        {
            return new PostgresTextVentureDB();
        }
    }
}
