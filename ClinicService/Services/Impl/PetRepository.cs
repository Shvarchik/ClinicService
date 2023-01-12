using ClinicService.Models;
using System.Data;
using System.Data.SQLite;

namespace ClinicService.Services.Impl
{
    public class PetRepository : IPetRepository
    {
        const string connectionString = "Data Source = clinic.db; Version = 3; Pooling = true; Max Pool Size = 100;";
        public int Create (Pet item)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = @"INSERT INTO pets(ClientId, Name, Birthday) VALUES (@ClientId, @Name, @Birthday)";
            command.Parameters.AddWithValue("@ClientId", item.ClientId);
            command.Parameters.AddWithValue("@Name", item.Name);
            command.Parameters.AddWithValue("@Birthday", item.Birthday.Ticks);
            command.Prepare();
            int res = command.ExecuteNonQuery();
            connection.Close();
            return res;
        }

        public int Delete (int item)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = "DELETE FROM pets WHERE PetId=@PetId";
            command.Parameters.AddWithValue("@PetId", item);
            command.Prepare();
            int res = command.ExecuteNonQuery();
            connection.Close();
            return res;
        }

        public IList<Pet> GetAll()
        {
            List<Pet> list = new List<Pet>();
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = "SELECT * FROM pets ORDER BY ClientId";
            SQLiteDataReader sQLiteDataReader   = command.ExecuteReader();
            while (sQLiteDataReader.Read())
            {
                Pet pet = new Pet();
                pet.PetId = sQLiteDataReader.GetInt32(0);
                pet.ClientId = sQLiteDataReader.GetInt32(1);
                pet.Name = sQLiteDataReader.GetString(2);
                pet.Birthday = new DateTime(sQLiteDataReader.GetInt64(3));
                list.Add(pet);
            }
            connection.Close();
            return list;

        }

        public Pet GetById(int id)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = "SELECT * FROM pets WHERE PetId=@PetId";
            command.Parameters.AddWithValue("@PetId", id);
            command.Prepare();
            SQLiteDataReader sQLiteDataReader = command.ExecuteReader();
            if (sQLiteDataReader.Read())
            {
                Pet pet = new Pet();
                pet.PetId = sQLiteDataReader.GetInt32(0);
                pet.ClientId = sQLiteDataReader.GetInt32(1);
                pet.Name = sQLiteDataReader.GetString(2);
                pet.Birthday = new DateTime(sQLiteDataReader.GetInt64(3));
                connection.Close();
                return pet;
            }
            else
            {   
                connection.Close();
                return null;
            }
        }

        public int Update(Pet item)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = "UPDATE pets SET ClientId = @ClientId, Name = @Name, Birthday = @Birthday WHERE PetId = @PetId";
            command.Parameters.AddWithValue("@ClientId", item.ClientId);
            command.Parameters.AddWithValue("@Name", item.Name);
            command.Parameters.AddWithValue("@Birthday", item.Birthday.Ticks);
            command.Parameters.AddWithValue("@PetId", item.PetId);
            command.Prepare();
            int res = command.ExecuteNonQuery();
            connection.Close();
            return res;
        }
    }
}

