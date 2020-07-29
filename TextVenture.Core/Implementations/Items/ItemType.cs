using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextVenture.Core.Interfaces.Items;

namespace TextVenture.Core.Implementations.Items
{
    public class ItemType: IItemsType
    {
        public ItemType(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public int Id { get; }
        public string Name { get; }
    }
}
