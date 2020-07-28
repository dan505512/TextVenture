using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using TextVenture.Core;
using TextVenture.Core.Implementations.Items;

namespace TextVenture.DAL
{
    /// <summary>
    /// An implementation of <see cref="ITextVentureDB"/> for Postgres databases
    /// </summary>
    class PostgresTextVentureDB : ITextVentureDB
    {
        /// <summary>
        /// A connection object to be used in queries
        /// </summary>
        private NpgsqlConnection _connection;

        /// <summary>
        /// Open a connection to the Postgres server.
        /// </summary>
        /// <param name="connectionString">The connection string for the DB</param>
        public void Connect(string connectionString)
        {
            _connection = new NpgsqlConnection(connectionString);
            _connection.Open();
        }

        public List<IItem> GetAllItems()
        {
            using var cmd = new NpgsqlCommand("Select i.\"ID\", i.\"Name\", t.\"Name\", i.\"Effect_Level\" from public.\"Items\" i JOIN public.\"Item_Type\" t on t.\"ID\" = i.\"Item_Type\"", _connection);
            var dataReader = cmd.ExecuteReader();
            var items = new List<IItem>();
            while (dataReader.Read())
            {
                items.Add(GetItemFromRow(dataReader));
            }

            return items;
        }

        /// <summary>
        /// Closes the connection and disposes of all resources
        /// </summary>
        public void Dispose()
        {
            _connection.Close();
            _connection.Dispose();
            _connection = null;
        }

        private IItem GetItemFromRow(NpgsqlDataReader reader)
        {
            var itemType = reader.GetString(2);
            switch (itemType)
            {
                case "Sword":
                    return new Sword(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(3));
                case "Shield":
                    return new Shield(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(3));
                case "Potion":
                    return new Potion(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(3));
                default:
                    throw new NotImplementedException("Given item type is not implemented");
            }
        }
    }
}
