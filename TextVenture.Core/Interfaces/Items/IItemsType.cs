using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextVenture.Core.Interfaces.Items
{
    public interface IItemsType
    {
        int Id { get; }
        string Name { get; }
    }
}
