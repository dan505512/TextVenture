using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using TextVenture.Core;
using TextVenture.Core.Implementations.Adventure;
using TextVenture.Core.Implementations.Characters;
using TextVenture.Core.Implementations.Items;
using TextVenture.Core.Interfaces.Adventure;
using TextVenture.Core.Interfaces.Characters;
using TextVenture.Core.Interfaces.Items;

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

        public bool IsConnected => _connection != null;

        public string GetPasswordHashForUser(string user)
        {
            using var query = new NpgsqlCommand(USER_PASSWORD_QUERY, _connection);

            query.Parameters.AddWithValue("user", user);
            query.Prepare();

            using var dataReader = query.ExecuteReader();
            return dataReader.Read() ? dataReader.GetString(0) : null;
        }

        public List<IItem> GetAllItems()
        {
            return PerformGenericGetAll(ITEMS_QUERY, GetItemFromRow);
        }

        public IItem GetItemById(int id)
        {
            using var query = new NpgsqlCommand(ITEMS_QUERY + " WHERE i.\"ID\" = @id", _connection);
            
            query.Parameters.AddWithValue("id", id);
            query.Prepare();

            using var dataReader = query.ExecuteReader();
            return dataReader.Read() ? GetItemFromRow(dataReader) : null;
        }

        public List<IItemsType> GetAllItemTypes()
        {
            return PerformGenericGetAll(ITEM_TYPES_QUERY, GetItemsTypeFromRow);
        }

        public bool InsertItem(string name, int effectLevel, int type)
        {
            try
            {
                using var query = new NpgsqlCommand(ITEM_INSERT_QUERY, _connection);
                query.Parameters.AddWithValue("name", name);
                query.Parameters.AddWithValue("type", type);
                query.Parameters.AddWithValue("effectLevel", effectLevel);
                query.Prepare();

                query.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateItem(int id, string name, int effectLevel)
        {
            try
            {
                using var query = new NpgsqlCommand(ITEM_UPDATE_QUERY, _connection);
                query.Parameters.AddWithValue("name", name);
                query.Parameters.AddWithValue("effectLevel", effectLevel);
                query.Parameters.AddWithValue("id", id);
                query.Prepare();

                query.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<IEnemy> GetAllEnemies()
        {
            return PerformGenericGetAll(ENEMIES_QUERY, GetEnemyFromRow);
        }

        public IEnemy GetEnemyById(int id)
        {
            return PerformGenericGetById(ENEMIES_QUERY, GetEnemyFromRow, id);
        }

        public bool UpdateEnemy(IEnemy enemy)
        {
            try
            {
                using var query = new NpgsqlCommand(ENEMY_UPDATE_QUERY, _connection);
                query.Parameters.AddWithValue("enemyName", enemy.Name);
                query.Parameters.AddWithValue("health", enemy.Health);
                query.Parameters.AddWithValue("minDamage", enemy.MinDamage);
                query.Parameters.AddWithValue("maxDamage", enemy.MaxDamage);
                query.Parameters.AddWithValue("id", enemy.Id);
                query.Prepare();

                query.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool InsertEnemy(string name, int health, int minDamage, int maxDamage)
        {
            try
            {
                using var query = new NpgsqlCommand(ENEMY_INSERT_QUERY, _connection);
                query.Parameters.AddWithValue("name", name);
                query.Parameters.AddWithValue("health", health);
                query.Parameters.AddWithValue("minDamage", minDamage);
                query.Parameters.AddWithValue("maxDamage", maxDamage);
                query.Prepare();

                query.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<ILocation> GetAllLocations()
        {
            return PerformGenericGetAll(LOCATION_QUERY, GetLocationFromRow);
        }

        public ILocation GetLocationById(int id)
        {
            return PerformGenericGetById(LOCATION_QUERY, GetLocationFromRow, id);
        }

        public bool InsertLocation(string name, string description, int? north, int? south, int? east, int? west, int? enemy,
            int? item)
        {
            try
            {
                using var query = new NpgsqlCommand(LOCATION_INSERT_QUERY, _connection);
                query.Parameters.AddWithValue("name", name);
                query.Parameters.AddWithValue("description", description);
                AddParamOrNull("north", north, query);
                AddParamOrNull("south", south, query);
                AddParamOrNull("east", east, query);
                AddParamOrNull("west", west, query);
                AddParamOrNull("item", item, query);
                AddParamOrNull("enemy", enemy, query);

                query.Prepare();

                query.ExecuteNonQuery();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateLocation(ILocation location)
        {
            try
            {
                using var query = new NpgsqlCommand(LOCATION_UPDATE_QUERY, _connection);
                query.Parameters.AddWithValue("name", location.Name);
                query.Parameters.AddWithValue("description", location.Description);
                AddParamOrNull("north", location.North, query);
                AddParamOrNull("south", location.South, query);
                AddParamOrNull("east", location.East, query);
                AddParamOrNull("west", location.West, query);
                AddParamOrNull("item", location.Item, query);
                AddParamOrNull("enemy", location.Enemy, query);
                query.Parameters.AddWithValue("id", location.Id);

                query.Prepare();

                query.ExecuteNonQuery();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<IAdventure> GetAllAdventures()
        {
            return PerformGenericGetAll(ADVENTURES_QUERY, getAdventureFromRow);
        }

        public IAdventure GetAdventureById(int id)
        {
            return PerformGenericGetById(ADVENTURES_QUERY, getAdventureFromRow, id);
        }

        public bool InsertAdventure(string name, string description, in int startingLocation)
        {
            try
            {
                using var query = new NpgsqlCommand(ADVENTURE_INSERT_QUERY, _connection);
                query.Parameters.AddWithValue("name", name);
                query.Parameters.AddWithValue("description", description);
                query.Parameters.AddWithValue("startingLocation", startingLocation);
                query.Prepare();

                query.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateAdventure(IAdventure adventure)
        {
            try
            {
                using var query = new NpgsqlCommand(ADVENTURE_UPDATE_QUERY, _connection);
                query.Parameters.AddWithValue("name", adventure.Name);
                query.Parameters.AddWithValue("description", adventure.Description);
                query.Parameters.AddWithValue("startingLocation", adventure.StartingLocation);
                query.Parameters.AddWithValue("id", adventure.ID);
                query.Prepare();

                query.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
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

        private List<T> PerformGenericGetAll<T>(string queryString, GetObjectFromDbRow<T> resolver)
        {
            using var query = new NpgsqlCommand(queryString, _connection);
            using var dataReader = query.ExecuteReaderAsync().Result;
            var itemList = new List<T>();

            while (dataReader.Read())
            {
                itemList.Add(resolver(dataReader));
            }

            return itemList;
        }

        private T PerformGenericGetById<T>(string queryString, GetObjectFromDbRow<T> resolver, int id)
        {
            using var query = new NpgsqlCommand(queryString + " WHERE i.\"ID\" = @id", _connection);

            query.Parameters.AddWithValue("id", id);
            query.Prepare();

            using var dataReader = query.ExecuteReader();
            dataReader.Read();
            return resolver(dataReader);
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

        private IItemsType GetItemsTypeFromRow(NpgsqlDataReader reader)
        {
            var id = reader.GetInt32(0);
            var name = reader.GetString(1);

            return new ItemType(id, name);
        }

        private IEnemy GetEnemyFromRow(NpgsqlDataReader reader)
        {
            var id = reader.GetInt32(0);
            var name = reader.GetString(1);
            var health = reader.GetInt32(2);
            var minDamage = reader.GetInt32(3);
            var maxDamage = reader.GetInt32(4);
            return new StandardEnemy(id, name, health, minDamage, maxDamage);
        }

        private ILocation GetLocationFromRow(NpgsqlDataReader reader)
        {
            var id = reader.GetInt32(0);
            var name = reader.GetString(1);
            var description = reader.GetString(2);
            var north = reader.GetFieldValue<int?>(3);
            var south = reader.GetFieldValue<int?>(4);
            var east = reader.GetFieldValue<int?>(5);
            var west = reader.GetFieldValue<int?>(6);
            var item = reader.GetInt32(7);
            var enemy = reader.GetFieldValue<int?>(8);
            return new StandardLocation(id, name, description, north, south, east, west, item, enemy);
        }

        private IAdventure getAdventureFromRow(NpgsqlDataReader reader)
        {
            var id = reader.GetInt32(0);
            var name = reader.GetString(1);
            var description = reader.GetString(2);
            var startingLocation = reader.GetInt32(3);
            return new Adventure(id, name, description, startingLocation);
        }

        private void AddParamOrNull(string paramId, object param, NpgsqlCommand query)
        {
            if (param == null)
            {
                query.Parameters.AddWithValue(paramId, DBNull.Value);
            }
            else
            {
                query.Parameters.AddWithValue(paramId, param);
            }
        }

        #region Queries

        #region Get
        /// <summary>
        /// This query returns all items. Items have {ID, Name, Type, Effect_Level}
        /// </summary>
        private const string ITEMS_QUERY = "Select i.\"ID\", i.\"Name\", t.\"Name\", i.\"Effect_Level\" from public.\"Items\" i JOIN public.\"Item_Type\" t on t.\"ID\" = i.\"Item_Type\"";

        private const string ENEMIES_QUERY =
            "Select \"ID\", \"Name\", \"Health\", \"Min_Damage\", \"Max_Damage\" from public.\"Enemies\" i";

        private const string LOCATION_QUERY =
            "SELECT i.\"ID\", i.\"Name\", i.\"Description\", i.\"North\", i.\"South\", i.\"East\", i.\"West\", i.\"Item\", i.\"Enemy\" from public.\"Location\" i";

        private const string ADVENTURES_QUERY =
            "SELECT i.\"ID\", i.\"Name\", i.\"Description\", i.\"Starting_Location\" from public.\"Advanture\" i"; 

        private const string ITEM_TYPES_QUERY = "SELECT i.\"ID\", i.\"Name\"from public.\"Item_Type\" i";

        private const string USER_PASSWORD_QUERY = "SELECT \"Password\"\r\n\tFROM public.\"Admins\"\r\n\tWHERE \"Username\" = @user;";

        #endregion

        #region Update

        private const string ENEMY_UPDATE_QUERY =
            "UPDATE public.\"Enemies\"\r\n\tSET \"Name\"=@enemyName, \"Health\"=@health, \"Min_Damage\"=@minDamage," +
            " \"Max_Damage\"=@maxDamage\r\n\tWHERE \"ID\"=@id;";

        private const string ITEM_UPDATE_QUERY =
            "UPDATE public.\"Items\"\r\n\tSET \"Name\"=@name, \"Effect_Level\"=@effectLevel\r\n\tWHERE \"ID\" = @id;";

        private const string LOCATION_UPDATE_QUERY =
            "UPDATE public.\"Location\"\r\n\tSET \"Name\"=@name, \"Description\"=@description, \"North\"=@north, \"South\"=@south, \"East\"=@east, \"West\"=@west, \"Item\"=@item, \"Enemy\"=@enemy\r\n\tWHERE \"ID\"=@id;";

        private const string ADVENTURE_UPDATE_QUERY =
            "UPDATE public.\"Advanture\"\r\n\tSET \"Name\"=@name, \"Description\"=@description, \"Starting_Location\"=@startingLocation\r\n\tWHERE \"ID\"=@id;";

        #endregion

        #region Insert

        private const string ENEMY_INSERT_QUERY =
            "INSERT INTO public.\"Enemies\"(\r\n\t\"Name\", \"Health\", \"Min_Damage\", \"Max_Damage\")\r\n\tVALUES (@name, @health, @minDamage, @maxDamage);";

        private const string ITEM_INSERT_QUERY =
            "INSERT INTO public.\"Items\"(\r\n\t\"Name\", \"Item_Type\", \"Effect_Level\")\r\n\tVALUES (@name, @type, @effectLevel);";

        private const string LOCATION_INSERT_QUERY =
            "INSERT INTO public.\"Location\"(\r\n\t\"Name\", \"Description\", \"North\", \"South\", \"East\", \"West\", \"Item\", \"Enemy\")\r\n\tVALUES (@name, @description, @north, @south, @east, @west, @item, @enemy);";

        private const string ADVENTURE_INSERT_QUERY =
            "INSERT INTO public.\"Advanture\"(\r\n\t\"Name\", \"Description\", \"Starting_Location\")\r\n\tVALUES (@name, @description, @startingLocation);";
        #endregion

        #endregion

        private delegate T GetObjectFromDbRow<T>(NpgsqlDataReader reader);
    }

    
}
