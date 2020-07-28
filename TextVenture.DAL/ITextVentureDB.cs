using System;
using System.Collections.Generic;
using TextVenture.Core;

namespace TextVenture.DAL
{
    public interface ITextVentureDB : IDisposable
    {
        void Connect(string connectionString);

        List<IItem> GetAllItems();
    }
}
